using System;
using System.Data;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using DevExpress.Spreadsheet;

namespace SmartMES.Models.BM
{
    /// <summary>B2B EDI 엑셀 파일 처리</summary>
    internal static class BM_EdiExcel
    {
        internal sealed class Row
        {
            public string Sheet, Customer, SourceCode, SourceName, Option, OrderNo, Center, ShipTo, Unit, Error;
            public int Number;
            public bool UseComponentPrice;
            public decimal? Qty, Amount;
            public DateTime? OrderDate, DeliveryDate;
        }

        internal static string Normalize(string value)
        {
            return Regex.Replace(value ?? "", @"\s+", "").ToUpperInvariant();
        }

        /// <summary>기존 거래처명과 대조, 가장 구체적인 이름이 하나인 경우만 연결</summary>
        private static string FindCustomer(System.Data.DataTable customers, string value)
        {
            string text = Regex.Replace(Normalize(value), @"[^\p{L}\p{N}]", "");

            // 명시한 EDI 이름을 우선 사용, 일반 거래처명으로 인한 다른 채널 연결 방지
            foreach (string field in new[] { "EdiMatchNames", "CustName" })
            {
                var matches = customers.AsEnumerable().SelectMany(row =>
                    Convert.ToString(row[field]).Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(name => new
                        {
                            Code = Convert.ToString(row["CustCode"]),
                            Name = Regex.Replace(Normalize(name), @"[^\p{L}\p{N}]", "")
                        }))
                    .Where(row => row.Name.Length > 0 && text.Contains(row.Name))
                    .OrderByDescending(row => row.Name.Length).ToArray();

                if (matches.Length == 0)
                    continue;

                string[] codes = matches.Where(row => row.Name.Length == matches[0].Name.Length)
                    .Select(row => row.Code).Distinct().ToArray();

                return codes.Length == 1 ? codes[0] : "";
            }
            return "";
        }

        private static decimal? Number(string value)
        {
            decimal number;

            // 원화 표시 형식(₩20,860)도 숫자로 변환
            string text = Regex.Replace((value ?? "").Trim(), @"^[₩￦$\\]\s*", "").Replace(",", "");

            var match = Regex.Match(text, @"^-?\d+(\.\d+)?");

            return match.Success && decimal.TryParse(match.Value, NumberStyles.Number, CultureInfo.InvariantCulture, out number) ? number : (decimal?)null;
        }

        private static DateTime? Date(string value)
        {
            DateTime date;
            string digits = Regex.Replace(value ?? "", @"\D", "");

            if (digits.Length >= 8 && DateTime.TryParseExact(digits.Substring(0, 8), "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                return date;

            return DateTime.TryParse(value, out date) ? date.Date : (DateTime?)null;
        }

        /// <summary>상세 시트 선택과 취소 주문 제외</summary>
        internal static List<Row> Read(byte[] bytes, string file)
        {
            bool useFileDate;

            return Read(bytes, file, out useFileDate);
        }

        internal static List<Row> Read(byte[] bytes, string file, out bool useFileDate, string selectedCustomer = "")
        {
            useFileDate = false;
            var result = new List<Row>();
            using (var customers = BM_EdiImport.Customers())
            using (var workbook = new Workbook())

            using (var stream = new MemoryStream(bytes))
            {
                workbook.LoadDocument(stream, Path.GetExtension(file).Equals(".xls", StringComparison.OrdinalIgnoreCase) ? DocumentFormat.Xls : DocumentFormat.Xlsx);

                string fileTitle = Path.GetFileNameWithoutExtension(file);
                string customer = FindCustomer(customers, fileTitle);
                // 파일명 등록일과 표 안의 주문 생성일·납품일을 구분
                DateTime fileDate;
                if (fileTitle.Length < 8 || !DateTime.TryParseExact(fileTitle.Substring(0, 8), "yyyyMMdd",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out fileDate))
                    throw new InvalidOperationException("파일명 앞에 출고일 8자리(예: 20261109)가 필요합니다.");

                useFileDate = true;
                if (!string.IsNullOrEmpty(selectedCustomer) && !customers.AsEnumerable().Any(row => Convert.ToString(row["CustCode"]) == selectedCustomer))
                    throw new InvalidOperationException("사용 중인 B2B·B2C 거래처를 선택해 주세요.");

                // 편집본·피벗·숨김 목록은 제외하고 실제 상세 양식 한 개만 선택
                foreach (Worksheet sheet in workbook.Worksheets)
                {
                    var range = sheet.GetUsedRange();

                    if (range.RowCount > 100000 || range.ColumnCount > 500)
                        throw new InvalidOperationException("지원 범위를 초과한 시트: " + sheet.Name);

                    for (int h = 0; h < Math.Min(12, range.BottomRowIndex + 1); h++)
                    {
                        var headers = Enumerable.Range(0, range.RightColumnIndex + 1).Select(c => Normalize(sheet.Cells[h, c].DisplayText)).ToArray();
                        string profile = Profile(headers);

                        if (profile == "")
                            continue;
                        if (profile == "common" && sheet.Name == "발주서" && workbook.Worksheets.Any(s => s.Name == "1발주서"))
                            continue;
                        if (profile == "costonline" && sheet.Name != "PurchaseOrderDetail (2)")
                            continue;

                        Parse(sheet, h, headers, profile, customer, customers, result);

                        if (profile == "costonline")
                            ReadCostcoAmounts(workbook, result);

                        if (profile == "common" && (fileTitle.Contains("제이앤에이치") || sheet.Name == "1발주서"))
                        {
                            RemoveCancelledRows(workbook, result);
                        }

                        if (result.Count == 0)
                            throw new InvalidOperationException("상세 행이 없는 파일: " + file);

                        if (!string.IsNullOrEmpty(selectedCustomer))
                        {
                            foreach (var row in result)
                            {
                                row.Customer = selectedCustomer;
                            }
                        }
                                
                        // 모든 양식의 EDI 등록일·출고일·포케스팅 반영일은 파일명 날짜
                        foreach (var row in result)
                        {
                            row.OrderDate = fileDate;
                        }
                            
                        return SplitSets(result);
                    }
                }
            }

            throw new InvalidOperationException("인식할 수 없는 발주서 양식: " + file);
        }

        /// <summary>확인된 혼합 세트의 구성품 분리, 일반 세트는 원본 총액·선물세트는 거래처별 금액 적용</summary>
        internal static List<Row> SplitSets(List<Row> rows)
        {
            var result = new List<Row>();

            foreach (var row in rows)
            {
                var parts = new List<(string Name, int Qty)>();
                string name = Normalize(row.SourceName);
                string option = Normalize(row.Option);
                bool useComponentPrice = false;

                if (name.Contains("혼합베스트5종구성") && name.Contains("플레인+소프트+블루베리+알룰로스+크리스피그래놀라"))
                {
                    useComponentPrice = true; // 지마켓 수주 상세의 상품금액 적용, 주문 할인 제외
                    parts.Add(("플레인 그릭요거트 100g", 1));
                    parts.Add(("소프트 그릭요거트 100g", 1));
                    parts.Add(("플레인 블루베리 130g", 1));
                    parts.Add(("알룰로스 블랜드 100g", 1));
                    parts.Add(("벨베티 크리스피 120g", 1));
                }
                else if (name == Normalize("저당 그릭요거트 토핑 선물세트 (콩포트/그래놀라 택2)") && option.StartsWith("그릭요거트선택:플레인(3개)+소프트(3개),"))
                {
                    useComponentPrice = true; // 수주 상세 기준 거래처별 구성품 금액
                    parts.Add(("플레인 그릭요거트 100g", 3));
                    parts.Add(("소프트 그릭요거트 100g", 3));

                    var toppings = new Dictionary<string, string>
                    {
                        { "카카오그래놀라토핑", "카카오그래놀라 토핑 (20g x 3개)" },
                        { "초코소보로그래놀라토핑", "초코소보로그래놀라 토핑 (20g x 3개)" },
                        { "크리스피그래놀라토핑", "크리스피그래놀라 토핑 (20g x 3개)" },
                        { "블루베리콩포트토핑", "블루베리콩포트 토핑 (30g x 3개)" }
                    };

                    var matches = Regex.Matches(option, @"토핑선택[12]:([^,]+)\(3개입\)(?:,|$)");

                    foreach (Match match in matches)
                    {
                        string topping;

                        if (toppings.TryGetValue(match.Groups[1].Value, out topping))
                            parts.Add((topping, 1)); // 3개입 토핑은 기존 묶음 품목 1개
                    }
                    if (matches.Count != 2 || parts.Count != 4)
                        parts.Clear(); // 확인되지 않은 옵션은 원본 그대로 유지
                }
                // 스토어팜 공통 물류코드(3PL)는 품목코드가 아니므로 선택 옵션의 실제 구성으로 분리
                if (row.SourceCode == "3PL" && parts.Count == 0)
                {
                    var products = new Dictionary<string, string>
                    {
                        { "플레인", "플레인 그릭요거트 100g" }, { "소프트", "소프트 그릭요거트 100g" },
                        { "블루베리", "플레인 블루베리 130g" }, { "WITH블루베리", "플레인 블루베리 130g" },
                        { "알룰로스", "알룰로스 블랜드 100g" }, { "말차", "말차그릭요거트 100g" },
                        { "WITH애플", "플레인 애플시나몬 130g" }, { "푸룬", "플레인 푸룬 130g" },
                        { "블루토핑", "블루베리콩포트 토핑 (30g x 3개)" },
                        { "크리토핑", "크리스피그래놀라 토핑 (20g x 3개)" },
                        { "초코토핑", "초코소보로그래놀라 토핑 (20g x 3개)" },
                        { "꿀스틱", "아카시아 꿀(허니스틱) 10포" }
                    };
                    if (option == "옵션선택:플/소/알/블/애/딸/크/카(8개)")
                    {
                        foreach (string product in new[] { "플레인 그릭요거트 100g", "소프트 그릭요거트 100g",
                            "알룰로스 블랜드 100g", "플레인 블루베리 130g", "플레인 애플시나몬 130g",
                            "플레인 딸기 130g", "벨베티 크리스피 120g", "플레인 카카오 120g" })
                            parts.Add((product, 1));
                    }
                    else
                    {
                        string selections = option.Substring(option.IndexOf(':') + 1);

                        foreach (string segment in selections.Split(new[] { '+', '/' }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            string choice = segment.Substring(segment.IndexOf(':') + 1);

                            if (choice.StartsWith("플레인WITH")) choice = choice.Substring(3);

                            string key = products.Keys.OrderByDescending(value => value.Length).FirstOrDefault(value => choice.StartsWith(value));

                            if (key == null) { parts.Clear(); break; }

                            var count = Regex.Match(choice, @"\((\d+)개(?:입)?\)");
                            int quantity = count.Success ? int.Parse(count.Groups[1].Value) : 1;

                            if (key.EndsWith("토핑"))
                            {
                                if (!count.Success && choice.Contains("증정")) quantity = 3;
                                if (quantity % 3 != 0) { parts.Clear(); break; }

                                quantity /= 3; // MES 토핑 품목의 판매단위는 3개입
                            }

                            parts.Add((products[key], quantity));
                        }
                    }
                }
                if (parts.Count == 0 || !row.Qty.HasValue || row.Qty <= 0)
                {
                    result.Add(row);
                    continue;
                }

                parts = parts.GroupBy(part => part.Name).Select(group => (Name: group.Key, Qty: group.Sum(part => part.Qty))).ToList();

                decimal allocated = 0m;
                int total = parts.Sum(part => part.Qty);

                for (int index = 0; index < parts.Count; index++)
                {
                    var part = parts[index];
                    decimal? amount = null;

                    if (row.Amount.HasValue && !useComponentPrice)
                    {
                        amount = index == parts.Count - 1 ? row.Amount.Value - allocated : Math.Round(row.Amount.Value * part.Qty / total, 0, MidpointRounding.AwayFromZero);
                        allocated += amount.Value;
                    }
                    result.Add(new Row
                    {
                        Sheet = row.Sheet,
                        Number = row.Number,
                        Customer = row.Customer,
                        SourceCode = "",
                        SourceName = part.Name,
                        Option = "세트 분리: " + row.SourceName + " | " + row.Option,
                        OrderNo = row.OrderNo,
                        Center = row.Center,
                        ShipTo = row.ShipTo,
                        Unit = "EA",
                        Error = row.Error,
                        Qty = row.Qty * part.Qty,
                        Amount = amount,
                        UseComponentPrice = useComponentPrice,
                        OrderDate = row.OrderDate,
                        DeliveryDate = row.DeliveryDate
                    });
                }
            }

            return result;
        }

        /// <summary>코스트코 온라인의 별도 금액 시트를 주문번호·상품코드·수량으로 대조</summary>
        private static void ReadCostcoAmounts(Workbook workbook, List<Row> rows)
        {
            Worksheet sheet = workbook.Worksheets.FirstOrDefault(s => s.Name == "PurchaseOrderDetail");

            if (sheet == null)
                return;

            var range = sheet.GetUsedRange();
            var headers = Enumerable.Range(0, range.RightColumnIndex + 1).Select(c => Normalize(sheet.Cells[0, c].DisplayText)).ToList();
            int order = headers.IndexOf(Normalize("회원 주문"));
            int code = headers.IndexOf(Normalize("Vendor SKU"));
            int quantity = headers.IndexOf(Normalize("합계"));
            int amount = headers.IndexOf(Normalize("총액 (₩)"));

            if (new[] { order, code, quantity, amount }.Any(c => c < 0))
                return;

            var amounts = Enumerable.Range(2, Math.Max(0, range.BottomRowIndex - 1))
                .Select(r => new
                {
                    Order = sheet.Cells[r, order].DisplayText.Trim(),
                    Code = sheet.Cells[r, code].DisplayText.Trim(),
                    Qty = Number(sheet.Cells[r, quantity].DisplayText),
                    Amount = Number(sheet.Cells[r, amount].DisplayText)
                })
                .ToLookup(r => new { r.Order, r.Code, r.Qty });

            foreach (Row row in rows)
            {
                var matches = amounts[new { Order = row.OrderNo, Code = row.SourceCode, Qty = row.Qty }].ToArray();

                // 중복 주문은 임의 배분하지 않고 기존 거래처 단가 계산으로 처리
                if (!row.Amount.HasValue && matches.Length == 1)
                    row.Amount = matches[0].Amount;
            }
        }

        /// <summary>제이앤에이치 발주서의 빨간 취소 표시를 주문번호·품명으로 대조</summary>
        private static void RemoveCancelledRows(Workbook workbook, List<Row> rows)
        {
            var cancelled = new HashSet<string>();
            foreach (Worksheet source in workbook.Worksheets.Where(s => s.Name == "발주서" || s.Name == "1발주서"))
            {
                for (int r = 2; r <= source.GetUsedRange().BottomRowIndex; r++)
                {
                    bool red = false;
                    for (int c = 0; c < 6; c++)
                    {
                        var color = source.Cells[r, c].Fill.BackgroundColor;
                        var font = source.Cells[r, c].Font.Color;
                        red |= color.R > 200 && color.G < 100 && color.B < 100;
                        red |= font.R > 200 && font.G < 100 && font.B < 100;
                    }

                    string order = Normalize(source.Cells[r, 0].DisplayText);
                    if (red && order.Length > 0)
                        cancelled.Add(order + "|" + Normalize(source.Cells[r, 4].DisplayText));
                }
            }

            rows.RemoveAll(row => cancelled.Contains(Normalize(row.OrderNo) + "|" + Normalize(row.SourceName)));
        }

        /// <summary>머리글 조합으로 발주서 양식 판별</summary>
        private static string Profile(string[] headers)
        {
            bool Has(string name) => headers.Contains(Normalize(name));
            if (Has("문서명") && Has("점포(센터)"))
                return "lotte";
            if (Has("상품명") && Has("Diamond Line") && Has("낱개수량") && !Has("발주번호"))
                return "costco";
            if (Has("상품바코드") && Has("발주수량"))
                return "coupang";
            if (Has("SKU코드") && Has("요청수량"))
                return "bmart";
            if (Has("발주상품코드") && Has("총 발주수량"))
                return "kurly";
            if (Has("구매문서") && Has("오더수량"))
                return "eland";
            if (Has("KAN코드") && Has("확정수량"))
                return "hyundai";
            if (Has("센터 코드") && Has("총수량"))
                return "cu";
            if (Has("상품코드") && Has("낱개수량") && Has("발주번호"))
                return Has("TPND") ? "homeplus" : "gs";
            if (Has("판매코드") && Has("주문수량"))
                return "seven";
            if (Has("BOX 입수량") && Has("주문번호"))
                return "emart24";
            if (Has("센터입하일자") && Has("수량"))
                return "everyday";
            if (Has("EAN코드") && Has("발주량"))
                return "welstory";
            if (Has("현 발주수량") && Has("배송처코드"))
                return "ourhome";
            if (Has("택배구분(새벽/일반)") && Has("품명"))
                return "common";
            if (Has("상품주문번호") && Has("옵션정보") && Has("수량"))
                return "naver";
            if (Has("주문상품명") && Has("공급처상품코드"))
                return "nonghyup";
            if (Has("출하지시예정일자") && Has("상품마스터번호"))
                return "olive";
            if (Has("판매아이디") && Has("판매자 관리코드"))
                return "gmarket";
            if (Has("채널상품번호") && Has("옵션"))
                return "kakao";
            if (Has("품목별 주문번호") && Has("주문상품명(기본)"))
                return "cafe";
            if (Has("vendorItem.vendorSKU") && Has("Total"))
                return "costonline";
            if (Has("주문 상품명") && Has("상품수량"))
                return "pudding";
            return "";
        }

        /// <summary>거래처별 원본 컬럼을 공통 상세 항목으로 변환</summary>
        private static void Parse(Worksheet sheet, int header, string[] headers, string profile, string customer, System.Data.DataTable customers, List<Row> output)
        {
            var range = sheet.GetUsedRange();
            // 같은 머리글이 반복되면 기존과 같이 첫 번째 열을 사용
            var columns = new Dictionary<string, int>();
            for (int column = 0; column < headers.Length; column++)
            {
                if (!columns.ContainsKey(headers[column]))
                    columns.Add(headers[column], column);
            }
            string orderNo = "", center = "";
            var shippingOrders = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            DateTime? orderDate = null, delivery = null;
            if (profile == "costco")
                orderNo = sheet.Cells[2, 2].DisplayText;
            for (int r = header + 1; r <= range.BottomRowIndex; r++)
            {
                // 현재 행(r)의 지정 열에서 표시 문자열을 읽고 앞뒤 공백 제거
                string At(int column) => column < 0 || column > range.RightColumnIndex
                    ? "" : sheet.Cells[r, column].DisplayText.Trim();

                // 별칭 우선순위를 유지하고 값이 비어 있으면 다음 이름으로 조회
                string Pick(params string[] names)
                {
                    foreach (string name in names)
                    {
                        if (!columns.TryGetValue(Normalize(name), out int column))
                            continue;

                        string value = At(column);
                        if (value != "")
                            return value;
                    }

                    return "";
                }

                if (profile == "lotte" && At(0) == "ORDERS")
                {
                    orderNo = At(1);
                    orderDate = Date(At(4));
                    center = At(5);
                    delivery = Date(At(7));
                    continue;
                }

                if (profile == "costco" && At(0) == "배달일시")
                {
                    orderNo = sheet.Cells[r + 1, 2].DisplayText;
                    r++;
                    continue;
                }

                if (profile == "costco" && At(0) == "발주일자")
                {
                    r++;
                    continue;
                }

                var row = new Row
                {
                    Sheet = sheet.Name,
                    Number = r + 1,
                    Customer = customer,
                    Option = "",
                    Error = "",
                    Center = "",
                    ShipTo = "",
                    Unit = "",
                    SourceCode = "",
                    SourceName = "",
                    OrderNo = ""
                };

                string qty = "";
                switch (profile)
                {
                    case "lotte":
                        if (Number(At(0)) == null || Number(At(1)) == null)
                            continue;
                        row.SourceCode = At(1);
                        row.SourceName = At(2);
                        qty = At(6);
                        row.Center = center;
                        row.ShipTo = At(3);
                        row.OrderNo = orderNo;
                        row.OrderDate = orderDate;
                        row.DeliveryDate = delivery;
                        row.Amount = Number(At(8));
                        row.Qty = Number(qty);
                        if (qty.IndexOf("BOX", StringComparison.OrdinalIgnoreCase) >= 0)
                            row.Qty = Number(At(5)) > 0 ? row.Qty * Number(At(5)) : null;
                        break;

                    case "costco":
                        if (Number(At(1)) == null || string.IsNullOrWhiteSpace(At(0)))
                            continue;
                        row.SourceCode = At(1);
                        row.SourceName = At(0);
                        qty = At(7);
                        row.OrderNo = orderNo;
                        row.Center = At(12);
                        row.ShipTo = At(3);
                        row.OrderDate = Date(At(13));
                        row.DeliveryDate = Date(At(14));
                        row.Amount = Number(At(11));
                        break;

                    default:
                        row.SourceName = Pick(
                            "상품이름", "SKU명", "상품명", "단품명",
                            "품목명", "품명", "주문상품명", "주문상품명(기본)",
                            "vendorItem.description");
                        row.SourceCode = Pick(
                            "상품바코드", "대체코드(88코드)", "KAN코드", "EAN코드",
                            "판매코드", "Vendor SKU", "vendorItem.vendorSKU", "판매자 상품코드",
                            "판매자 관리코드", "공급처상품코드", "자체상품코드", "상품코드",
                            "SKU코드", "품목코드", "상품마스터번호", "판매자상품번호");
                        row.Option = Pick("옵션정보", "상품옵션", "옵션", "단품명");
                        if (row.Option == row.SourceName)
                            row.Option = "";
                        // 농협몰의 기본단품은 상품명이 아닌 옵션 표기
                        if (profile == "nonghyup")
                        {
                            row.SourceName = Pick("주문상품명", "전시상품명");
                            row.Option = Pick("단품명");

                            if (row.Option == "기본단품" || row.Option == row.SourceName)
                                row.Option = "";
                        }

                        row.OrderNo = Pick(
                            "품목별 주문번호", "상품주문번호", "발주상세ID", "발주상품코드",
                            "주문번호", "발주번호", "구매문서", "문서번호",
                            "회원 주문");
                        row.Center = Pick(
                            "센터코드", "점포코드", "납품플랜트", "물류센터",
                            "센터", "입고센터", "입고지");
                        row.ShipTo = Pick(
                            "배송처", "점포명", "센터명", "센터이름",
                            "플랜트명", "배송처명", "사업장명");
                        row.OrderDate = Date(Pick(
                            "발주일자", "발주일", "주문일자", "주문일시",
                            "주문일", "발주등록일시", "발주생성일", "구매오더일",
                            "결제일시(입금확인일)"));
                        // 아워홈은 표 안의 공급일자를 EDI 등록일로 사용
                        if (profile == "ourhome" && !row.OrderDate.HasValue)
                            row.OrderDate = Date(Pick("공급일자"));
                        row.DeliveryDate = Date(Pick(
                            "납품일자", "납품예정일자", "입고예정일", "납품예정일",
                            "납품요청일", "납기일자", "공급일자", "센터입하일자",
                            "센터입고일", "배송예정일", "발송 마감일"));
                        row.Unit = Pick("단위", "단위(UOM)", "발주단위");
                        row.Amount = Number(Pick(
                            "최초 상품별 총 주문금액",
                            "공급가액", "공급가", "발주금액", "주문금액",
                            "금액", "최종 상품별 총 주문금액", "상품구매금액(KRW)", "판매금액"));
                        // 컬리의 총 합계는 원본 행 금액
                        if (profile == "kurly")
                            row.Amount = Number(Pick("총 합계"));

                        // 카카오의 할인 반영 상품 금액, 배송비·수수료 제외
                        if (profile == "kakao")
                            row.Amount = Number(Pick("정산기준금액"));

                        qty = Pick(
                            "낱개수량", "총 발주수량", "총수량", "주문수량",
                            "오더수량", "요청수량", "현 발주수량", "발주수량",
                            "발주량", "상품수량", "수량", "Total");
                        if (profile == "emart24")
                        {
                            qty = At(18);
                            row.Amount = Number(At(19));
                        }

                        if (profile == "common")
                        {
                            string name = Pick("거래처");
                            row.Customer = FindCustomer(customers, name);
                            row.ShipTo = name;
                        }

                        if (profile == "homeplus")
                        {
                            bool express = Pick("센터구분").IndexOf("EXP", StringComparison.OrdinalIgnoreCase) >= 0
                                || row.ShipTo.Contains("익스");
                            row.Customer = FindCustomer(customers, express ? "홈플러스 EX" : "홈플러스");
                        }
                        string state = Pick("주문상태", "발주상태", "상태");
                        if (state.Contains("취소") || state.Contains("반품"))
                            row.Error = "취소·반품 행 확인 필요";
                        break;
                }

                if (string.IsNullOrWhiteSpace(row.SourceName) || new[] { "상품명", "품목명", "품명", "합계", "총합계", "총계", "VENDORITEM.DESCRIPTION" }.Contains(Normalize(row.SourceName)))
                    continue;

                if (profile != "lotte")
                    row.Qty = Number(qty);

                if (profile == "welstory" && row.Unit.Equals("BOX", StringComparison.OrdinalIgnoreCase))
                {
                    // BOX 원본은 품명에 명시된 입수만 사용, 입수 불명확 시 확인 대기
                    var pack = Regex.Match(row.SourceName, @"\*(\d+)\s*EA", RegexOptions.IgnoreCase);
                    row.Qty = pack.Success ? row.Qty * decimal.Parse(pack.Groups[1].Value, CultureInfo.InvariantCulture) : null;
                }

                // 쿠팡 금액은 부가세가 포함된 원본 총발주 매입금
                if (!string.IsNullOrWhiteSpace(Pick("총발주 매입금")))
                    row.Amount = Number(Pick("총발주 매입금"));

                // 푸딩팩토리는 주문 전체 합계 대신 상품별 판매가와 옵션 금액 사용
                if (profile == "pudding" && !row.Amount.HasValue)
                    row.Amount = (Number(Pick("판매가")) + (Number(Pick("옵션 금액")) ?? 0m)
                        + (Number(Pick("텍스트옵션 금액")) ?? 0m)) * row.Qty;

                // 금액이 비어 있고 개당 단가가 있는 양식만 계산, 원본 0원은 유지
                if (!row.Amount.HasValue)
                    row.Amount = Number(Pick("단가", "공급단가", "발주단가", "판매단가", "낱개당 단가", "납품원가", "구매원가")) * row.Qty;

                // 아워홈 BOX 발주를 낱개 수량으로 환산, 금액은 박스 구매원가 기준 유지
                if (profile == "ourhome")
                {
                    var pack = Regex.Match(Pick("단위 및 규격"), @"BOX\([^)]*\*(\d+)EA\)", RegexOptions.IgnoreCase);
                    if (pack.Success)
                        row.Qty *= decimal.Parse(pack.Groups[1].Value, CultureInfo.InvariantCulture);
                }
                if (row.Qty == null)
                    row.Error = "수량 확인 필요";
                if (row.Qty < 0)
                    row.Error = "음수 수량 확인 필요";
                output.Add(row);

                // 배송비는 주문별 한 번만 추가, 상품 수량·금액과 분리
                decimal shipping = Number(Pick("총 배송비(KRW) (첫 품목에만 표시)", "배송비", "총 배송비(KRW)")) ?? 0m;
                string shippingOrder = Pick("주문번호");
                string shippingKey = row.Customer + "|" + shippingOrder;
                if (shipping > 0 && (shippingOrder == "" || shippingOrders.Add(shippingKey)))
                {
                    output.Add(new Row
                    {
                        Sheet = row.Sheet,
                        Number = row.Number,
                        Customer = row.Customer,
                        SourceCode = "9DL00000001",
                        SourceName = "배송료",
                        Option = "",
                        OrderNo = shippingOrder == "" ? row.OrderNo : shippingOrder,
                        Center = row.Center,
                        ShipTo = row.ShipTo,
                        Error = row.Error,
                        Qty = 0m,
                        Amount = shipping,
                        Unit = "",
                        OrderDate = row.OrderDate,
                        DeliveryDate = row.DeliveryDate
                    });
                }
            }
        }
    }
}
