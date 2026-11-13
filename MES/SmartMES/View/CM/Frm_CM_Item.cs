using baseSMT;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.CM;
using SMT_MES.View.CM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SmartMES.View.CM
{
    public partial class Frm_CM_Item : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();
        private string FormType = "";    //"" :전체 품목, Product : 제품정보, Material : 원자재정보, "10" :특정 품목구분 조회
        private string[] CustDiv = { "05", "10", "20", "30", "40", "50" }; //거래처구분 리스트(05 : 매입/매출, 10 : 매입, 20 : 매출, 30 : 외주, 40 : 납품)
        string ItemType = "10,20,30,40,50,60,70";

        public Frm_CM_Item()
        {
            InitializeComponent();
        }

        public Frm_CM_Item(string formType)
        {
            InitializeComponent();
            FormType = formType;
        }


        private void Frm_CM_Item_Type_Load(object sender, EventArgs e)
        {
            SetDesign();

            Auth.ApplyButtonAuth(Convert.ToString(this.Tag), smtCommonBar1);//상단 패널 사용자별 버튼 권한 설정

            Display();
        }


        #region 디자인 아이템 생성

        private void SetDesign()
        {
            //
            //1. 품목구분 데이터 입력(순서 변경 X)
            Set_Item_Div_Rdo(Rdo_Item_Div);

            //GridView 데이터 입력
            Set_GridView();

            //품목분류 데이터 입력
            Set_Lup(Lup_ItemClass);

            cmdCopy.SetToolTip("행 복사", "선택한 행을 복사하여 추가합니다.\n복사항목: 거래처코드, 품목구분");

            //폼 닫을때 검사 작성된 내역 있는지 검사
            this.FormClosing += new FormClosingEventHandler((sender, e) => {
                e.Cancel = false;
                if (grid.Check_Gv_FormClosing(Gc_Item.MainView as GridView, "Status"))
                {
                    e.Cancel = true;
                }
            });

            //// 광주요 엑셀업로드 기능 추가
            //if (Database.DbName.Contains("GJY") && smtCommonBar1.smtImageTitleText == "제품정보")
            //{
            //    cmdExcelUp.Visible = true;
            //}
        }

        private void Set_Item_Div_Rdo(RadioGroup rdo)
        {
            string all = "전체";

            //품목구분 RadioGroup 셋팅
            if (FormType == "Product")
            {
                DataTable dtP = code.GetItemType(new string[] { "10", "20", "30" });
                ItemType = string.Join(",", dtP.AsEnumerable().Select(row => row.Field<string>("코드"))); //UseYn = 'Y' 항목만 가져오기
            }
            else if (FormType == "Material")
            {
                //ItemType = "40,50,60,70";
                DataTable dtM = code.GetItemType(new string[] { "40", "50", "60", "70", "80", "90" });
                ItemType = string.Join(",", dtM.AsEnumerable().Select(row => row.Field<string>("코드"))); //UseYn = 'Y' 항목만 가져오기

                Gc_Item.MainView = Gv_Material; //자재일 경우 기본 Grid 자재 Grid로 설정, 자재코드 초기화 옵션 적용을 위해
            }
            else if (FormType == "")
            {
                ItemType = code.GetCM_CateItemAGG(WrGlobal.CorpID, "A010");
            }
            else
            {
                //Menugroup ParamText가 해당 품목구분 으로 등록 되어있을 경우 ex) 40 ex) 20,30 해당 품목구분 항목만 조회

                ItemType = this.FormType;

                if(!ItemType.Contains("20")) Gc_Item.MainView = Gv_Material; //(20) 제품이 아닐 경우 기본 Grid 자재 Grid로 설정, 자재코드 초기화 옵션 적용을 위해

                all = "";
            }

            if (!ItemType.Contains(",")) all = "";

            DataTable dt = code.smtLookUp("품목구분", new string[] { ItemType }, all);

            //Item 생성
            clsRadioGroup clsRadio = new clsRadioGroup();
            clsRadio.Create_RadioGroupItem(rdo, dt, "코드명", "코드");

            rdo.SelectedIndex = 0; //초기값

            rdo.EditValueChanged += Rdo_ItemDIv_EditValueChanged;
            rdo.EditValueChanging += Rdo_EditValueChanging;
        }

        private void Rdo_EditValueChanging(object sender, ChangingEventArgs e)
        {
            //1. 조회 검사
            if (!Check_Search())
            {
                e.Cancel = true;
                return;
            }
        }

        private void Set_Lup(SearchLookUpEdit lup)
        {
            DataTable dt = code.GetCM_CateItem(WrGlobal.CorpID, "A01210", true);
            cls_Lookup clsLup = new cls_Lookup();

            clsLup.Create_RepositoryItemSearchLookUp(lup, dt, "코드", "명칭");

            lup.EditValue = "ALL";

            lup.EditValueChanged += ItemClass_EditValueChanged;

            //거래처
            //품목 단일 거래처가 아닌경우 조회조건 거래처 숨김
            if (WrGlobal.ProcOption["BS"]["Item_OneCust"] == "Y")
            {
                LblSearchCustomer.Visible = true;
                LblSearchCustomer.Visible = true;

                if (FormType == "Product")
                {
                    CustDiv = new string[] { "05", "20", "50" };
                }
                else if (FormType == "Material")
                {
                    CustDiv = new string[] { "05", "10", "50" };
                }

                CM_Cust Cust = new CM_Cust();
                DataTable dt_Cust = Cust.Select_Cust_List(CustDiv, true);
                clsLup.Create_RepositoryItemSearchLookUp(LupCustomer, dt_Cust, "코드", "명칭");
                LupCustomer.EditValue = "ALL";
            }
            else
            {
                LblSearchCustomer.Visible = false;
                LupCustomer.Visible = false;
            }
        }

        #region GridView Setting

        private void Set_GridView()
        {
            Set_Grid(Gv_Goods, "10"); //상품
            Set_Grid(Gv_Product, "20"); //제품
            Set_Grid(Gv_Asy, "30"); //반제품
            Set_Grid(Gv_Material, "40"); //원자재
            Set_Grid(Gv_Subsidiary, "50"); //부자재
            Set_Grid(Gv_Consume, "60"); //소모자재
            //Set_Grid(Gv_Jig, "70"); //지그

            Set_Gv_History(Gv_History);
            layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            Gc_Item.ProcessGridKey += Event_ProcessGridKey;    //행추가, 삽입, 삭제 단축키기능 부여
        }

        private void Set_Grid(GridView gv, string item_Div)
        {
            //Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.IndicatorWidth = 50;                     //순번 너비
            //헤더 멀티라인 설정
            gv.OptionsView.AllowHtmlDrawHeaders = true;
            gv.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            //검색창
            gv.OptionsFind.AlwaysVisible = true;
            gv.OptionsView.ShowGroupPanel = false;
            //멀티셀렉트
            gv.OptionsSelection.MultiSelect = true;
            //Column 생성
            Set_Col_ByItem_Div(gv, item_Div);

            //이벤트 설정
            grid.RowNumber_Indicator(gv);   //행번호 표기
            gv.RowStyle += grid.RowStyle_ChangeBg_ByStatus;           //삭제시 배경색 지정
            gv.CellValueChanged += Event_CellValueChanged;            //셀값 수정시 상태값 수정
            gv.ValidatingEditor += Event_ValidatingEditor;            //코드 입력시 중복 확인
        }

        private void Set_Col_ByItem_Div(GridView gv, string item_Div)
        {
            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "거래처", "CustCode");
            grid.Create_GvColumn(gv, "품목구분", "ItemType");
            grid.Create_GvColumn(gv, "약칭", "Abbreviation");

            // 업체별 컬럼 추가
            switch (item_Div)
            {
                case "10":  //상품
                case "20":  //제품
                case "30":  //반제품
                    grid.Create_GvColumn_ByOption(gv, "제품코드 초기화");
                    grid.Create_GvColumn(gv, "제품이미지", "btnImg");
                    break;

                case "40":  //원자재
                case "50":  //부자재
                case "60":  //부자재
                case "70":  //부자재
                    grid.Create_GvColumn_ByOption(gv, "자재코드 초기화");
                    break;

                default:
                    grid.Create_GvColumn_ByOption(gv, "자재코드 초기화");
                    break;
            }
            grid.Create_GvColumn(gv, "요거트(g)", "YogurtG");
            grid.Create_GvColumn(gv, "요거트(Kg)", "YogurtKg");

            grid.Create_GvColumn(gv, "영문약어", "EnglishAbbreviation");
            grid.Create_GvColumn(gv, "품목군", "ItemGroup");
            grid.Create_GvColumn(gv, "공급구분", "SupplyType");
            grid.Create_GvColumn(gv, "자재T", "MaterialT");
            grid.Create_GvColumn(gv, "가로", "Width");
            grid.Create_GvColumn(gv, "세로", "Height");
            grid.Create_GvColumn(gv, "순중량", "NetWeight");
            if (item_Div == "10" || item_Div == "20")
            {
                grid.Create_GvColumn(gv, "포장단위", "PackUnit");
                grid.Create_GvColumn(gv, "포장수량", "PackQty");
                //grid.Create_GvColumn(gv, "원가 이력", "History");
            }
            grid.Create_GvColumn(gv, "PT적입수", "PTLoadQty");
            grid.Create_GvColumn(gv, "바코드", "BarCode");
            grid.Create_GvColumn(gv, "(구)품목코드", "OldItemCode");


            //이미지 버튼
            EditorButton Btn_Img_Move = new EditorButton();
            Btn_Img_Move.Kind = ButtonPredefines.Right;

            RepositoryItemButtonEdit Btn_Img = new RepositoryItemButtonEdit();
            Btn_Img.TextEditStyle = TextEditStyles.HideTextEditor;  //Text입력 삭제
            Btn_Img.Buttons.RemoveAt(0);
            Btn_Img.Buttons.Add(Btn_Img_Move);
            Btn_Img.ButtonClick += Btn_Grid_Click;
            //gv.RowClick += Event_IMG_Click;    //버튼 클릭 이벤트로 하면 grid focused를 할수없어서 gv클릭이벤트로 변경

            RepositoryItemButtonEdit Btn_WorkSheet = new RepositoryItemButtonEdit();
            Btn_WorkSheet.TextEditStyle = TextEditStyles.HideTextEditor;  //Text입력 삭제
            Btn_WorkSheet.Buttons.RemoveAt(0);
            Btn_WorkSheet.Buttons.Add(Btn_Img_Move);
            Btn_WorkSheet.ButtonClick += Btn_Grid_Click;

            RepositoryItemButtonEdit Btn_WorkSheet_History = new RepositoryItemButtonEdit();
            Btn_WorkSheet_History.TextEditStyle = TextEditStyles.HideTextEditor;  //Text입력 삭제
            Btn_WorkSheet_History.Buttons.RemoveAt(0);
            Btn_WorkSheet_History.Buttons.Add(Btn_Img_Move);
            Btn_WorkSheet_History.ButtonClick += Btn_Grid_Click;

            //원가 이력관리 항목
            RepositoryItemButtonEdit Btn_History = new RepositoryItemButtonEdit();
            Btn_History.TextEditStyle = TextEditStyles.HideTextEditor;  //Text입력 삭제
            Btn_History.Click += Btn_History_Click;                //이벤트 지정

            //gv.RowClick += Event_IMG_Click;    //버튼 클릭 이벤트로 하면 grid focused를 할수없어서 gv클릭이벤트로 변경

            //창고
            RepositoryItemSearchLookUpEdit lupStock = rpo.Create_RepositoryItemSearchLookUp(code.GetWarehouseList_Dt(), "창고코드", "창고명");

            grid.Create_GvColumn(gv, "창고", "HouseID", lupStock);
			grid.Create_GvColumn(gv, "안전재고", "SaftyQty");

			grid.Create_GvColumn(gv, "검사여부", "ExamYn", rpo.Create_rpoChk());
            grid.Create_GvColumn(gv, "사용여부", "UseYn", rpo.Create_rpoChk());

            grid.Create_GvColumn(gv, "비고", "Remark");
            grid.Create_GvColumn(gv, "등록자", "CreId");
            grid.Create_GvColumn(gv, "등록IP", "CreIP");
            grid.Create_GvColumn(gv, "등록일시", "CreDt");
            grid.Create_GvColumn(gv, "수정자", "ModId");
            grid.Create_GvColumn(gv, "수정IP", "ModIP");
            grid.Create_GvColumn(gv, "수정일시", "ModDt");
            grid.Create_GvColumn(gv, "리비전", "Revision");
            grid.Create_GvColumn(gv, "라우팅코드", "RoutingCode");

            //Column 단일 설정
            gv.Columns["Revision"].Visible = false;
            gv.Columns["RoutingCode"].Visible = false;

            //Repository 설정
            //Repository 생성 or 이벤트 입력
            //품목구분
            RepositoryItemLookUpEdit lup_ItemType = rpo.Create_RepositoryItem(code.smtLookUp("품목구분", new string[] { ItemType }, ""), "코드", "코드명");
            //단위
            RepositoryItemSearchLookUpEdit lup_UnitCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목단위", "", ""), "코드", "코드명");
            if (CM_Item.UsePurchasingFields)
            {
                // 품목 1개 기준 공통 금액
                grid.Create_GvColumn(gv, "금액", "Amount", rpo.Create_rpoTxt_Numeric(), true);
                grid.Column_AllowEdit(gv.Columns["Amount"]);
                gv.Columns["Amount"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gv.Columns["Amount"].DisplayFormat.FormatString = "#,##0.####";
                gv.Columns["Amount"].ToolTip = "품목 1개 기준 공통 금액";
    
                grid.Create_GvColumn(gv, "구매단위", "PurchaseUnitCode", lup_UnitCode, true);
                grid.Create_GvColumn(gv, "환산수량", "PurchaseConversionQty", null, true);
                grid.Create_GvColumn(gv, "납품초과 허용률(%)", "DeliveryOverTolerancePct", null, true);
                gv.Columns["PurchaseConversionQty"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gv.Columns["PurchaseConversionQty"].DisplayFormat.FormatString = "#,##0.######";
                gv.Columns["DeliveryOverTolerancePct"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gv.Columns["DeliveryOverTolerancePct"].DisplayFormat.FormatString = "#,##0.####";
                foreach (var field in new[]
                {
                    new { Caption = "HS코드", Name = "HSCode", MaxLength = 30 },
                    new { Caption = "HS명", Name = "HSName", MaxLength = 200 },
                    new { Caption = "HS규격", Name = "HSSpec", MaxLength = 200 },
                    new { Caption = "HS단위", Name = "HSUnit", MaxLength = 30 }
                })
                {
                    var editor = new RepositoryItemTextEdit { MaxLength = field.MaxLength };
                    gv.GridControl.RepositoryItems.Add(editor);
                    grid.Create_GvColumn(gv, field.Caption, field.Name, editor, true);
                }
            }

            //거래처
            RepositoryItemSearchLookUpEdit lup_CustCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("거래처", "", "", ""), "거래처코드", "거래처명");
            //포장단위

            //품목분류1
            //RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01210", new string[] { ItemType }), "코드", "명칭");
            //RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01220", new string[] {ItemType}), "코드", "명칭");
            //RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01230", new string[] {ItemType}), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01210"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01220"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01230"), "코드", "명칭");

            //라우팅 공정코드
            RepositoryItemSearchLookUpEdit lup_RoutingCode = rpo.Create_RepositoryItemSearchLookUp(code.GetRoutingCode(WrGlobal.CorpID, "10"), "코드", "명칭");

            //컬럼별 설정


            //프로세스 옵션 : 코드 자동채번 YN에 따른 편집 허용 설정
            if (WrGlobal.ProcOption["BS"]["Item_AutoNo"] != "Y")
            {
                grid.Column_AllowEdit(gv.Columns["ItemCode"]);  //
            }

            //품목구분
            if (gv.Columns.ColumnByFieldName("ItemType") != null)
            {
                gv.Columns["ItemType"].ColumnEdit = lup_ItemType;
                grid.Column_AllowEdit(gv.Columns["ItemType"]);

                gv.Columns["ItemType"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            }

            //품목분류
            if (gv.Columns.ColumnByFieldName("ItemClass1") != null)
            {
                gv.Columns["ItemClass1"].ColumnEdit = lup_Class_Cd1;
                grid.Column_AllowEdit(gv.Columns["ItemClass1"]);
            }

            if (gv.Columns.ColumnByFieldName("ItemClass2") != null)
            {
                gv.Columns["ItemClass2"].ColumnEdit = lup_Class_Cd2;
                grid.Column_AllowEdit(gv.Columns["ItemClass2"]);
            }

            if (gv.Columns.ColumnByFieldName("ItemClass3") != null)
            {
                gv.Columns["ItemClass3"].ColumnEdit = lup_Class_Cd3;
                grid.Column_AllowEdit(gv.Columns["ItemClass3"]);
            }
            //포장단위
            if (gv.Columns.ColumnByFieldName("PackUnit") != null)
            {
                gv.Columns["PackUnit"].ColumnEdit = lup_UnitCode;
            }

            //단위중량
            if (gv.Columns.ColumnByFieldName("UnitWeight") != null)
            {
                grid.Column_AllowEdit(gv.Columns["UnitWeight"]);
                grid.SetCol_Type(gv.Columns["UnitWeight"], clsGrid.TypeList.TFloatN2);
            }

            if (gv.Columns.ColumnByFieldName("SaftyQty") != null)
            {
                grid.Column_AllowEdit(gv.Columns["SaftyQty"]);
                grid.SetCol_Type(gv.Columns["SaftyQty"], clsGrid.TypeList.TInt);
            }

            gv.Columns["ItemCode"].ColumnEdit = rpo.Create_rpoTxt_Length(200);
            gv.Columns["ItemName"].ColumnEdit = rpo.Create_rpoTxt_Length(200);

            if (gv.Columns.ColumnByFieldName("btnImg") != null)
			{
                gv.Columns["btnImg"].ColumnEdit = Btn_Img;
                grid.Column_AllowEdit(gv.Columns["btnImg"]);
			}

            if (gv.Columns.ColumnByFieldName("History") != null)
            {
                gv.Columns["History"].ColumnEdit = Btn_History;
                grid.Column_AllowEdit(gv.Columns["History"]);
            }

            if (gv.Columns.ColumnByFieldName("Alias") != null)
            {
                gv.Columns["Alias"].ColumnEdit = rpo.Create_rpoTxt_Length(50);
                grid.Column_AllowEdit(gv.Columns["Alias"]);
            }

            if (gv.Columns.ColumnByFieldName("Size") != null)
            {
                gv.Columns["Size"].ColumnEdit = rpo.Create_rpoTxt_Length(20);
                grid.Column_AllowEdit(gv.Columns["Size"]);

                //프로세스 옵션
                //품목 규격 멀티라인 사용여부 Y인 경우 규격에 MemoEdit 추가
                if (WrGlobal.ProcOption["BS"].ContainsKey("Item_Spec_MultiLineYN"))
                {
                    if (WrGlobal.ProcOption["BS"]["Item_Spec_MultiLineYN"] == "Y")
                    {
                        GridColumn colSpec = gv.Columns.ColumnByFieldName("Size");

                        if (colSpec != null)
                        {
                            RepositoryItemMemoEdit memoEdit = new RepositoryItemMemoEdit();
                            colSpec.ColumnEdit = memoEdit;
                            colSpec.MinWidth = 100;

                            gv.OptionsView.RowAutoHeight = true;
                        }
                    }
                }
            }

            if (gv.Columns.ColumnByFieldName("UnitCode") != null)
            {
                gv.Columns["UnitCode"].ColumnEdit = lup_UnitCode;
                grid.Column_AllowEdit(gv.Columns["UnitCode"]);
            }

            //사출규격
            if (gv.Columns.ColumnByFieldName("Type") != null)
            {
                grid.Column_AllowEdit(gv.Columns["Type"], true);
            }

            //단일 거래처 옵션
            if (WrGlobal.ProcOption["BS"]["Item_OneCust"] == "Y")
            {
                SetGv_CustLup(gv);
                gv.Columns["CustCode"].ColumnEdit = lup_CustCode;
                grid.Column_AllowEdit(gv.Columns["CustCode"]);  //
            }
            else
            {
                if (gv.Columns.ColumnByFieldName("CustCode") != null) gv.Columns["CustCode"].Visible = false;
            }

            ////납품처 : 초기화옵션으로인한 자동생성 삭제
            //if (gv.Columns.ColumnByFieldName("Direct_CustCode") != null) { gv.Columns.Remove(gv.Columns["Direct_CustCode"]); }
            //if (gv.Columns.ColumnByFieldName("Direct_CustName") != null) { gv.Columns.Remove(gv.Columns["Direct_CustName"]); }

            //등록,수정자
            RepositoryItemSearchLookUpEdit lup_Emp_No = rpo.Create_RepositoryItemSearchLookUp(code.GetDataTableUser(false), "코드", "명칭");

            gv.Columns["CreId"].ColumnEdit = lup_Emp_No;
            gv.Columns["ModId"].ColumnEdit = lup_Emp_No;

            //셀값 변경 허용
            grid.Column_AllowEdit(gv.Columns["ItemName"]);  //
            grid.Column_AllowEdit(gv.Columns["Texture"]);  //
            grid.Column_AllowEdit(gv.Columns["Remark"]);  //
            grid.Column_AllowEdit(gv.Columns["PackUnit"]);       //포장단위
            grid.Column_AllowEdit(gv.Columns["PackQty"]);        //포장수량
            grid.Column_AllowEdit(gv.Columns["ExamYn"]);
            grid.Column_AllowEdit(gv.Columns["UseYn"]);
            grid.Column_AllowEdit(gv.Columns["UnitCode"]);
            grid.Column_AllowEdit(gv.Columns["SearchName"]);
            grid.Column_AllowEdit(gv.Columns["HouseID"]);
            grid.Column_AllowEdit(gv.Columns["Abbreviation"]);
            grid.Column_AllowEdit(gv.Columns["EnglishAbbreviation"]);
            grid.Column_AllowEdit(gv.Columns["ItemGroup"]);
            grid.Column_AllowEdit(gv.Columns["SupplyType"]);
            grid.Column_AllowEdit(gv.Columns["MaterialT"]);
            grid.Column_AllowEdit(gv.Columns["Width"]);
            grid.Column_AllowEdit(gv.Columns["Height"]);
            grid.Column_AllowEdit(gv.Columns["NetWeight"]);
            grid.Column_AllowEdit(gv.Columns["YogurtG"]);
            grid.Column_AllowEdit(gv.Columns["YogurtKg"]);
            grid.Column_AllowEdit(gv.Columns["PTLoadQty"]);
            grid.Column_AllowEdit(gv.Columns["BarCode"]);
            grid.Column_AllowEdit(gv.Columns["OldItemCode"]);

            //
            grid.SetCol_Type(gv.Columns["PackQty"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["NetWeight"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["YogurtG"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["YogurtKg"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["PTLoadQty"], clsGrid.TypeList.TFloat);

            //개발자용 코드 숨기기
            if (WrGlobal.CdOption["제품코드 초기화"]["ItemCode"].DispYn == false) gv.Columns["ItemCode"].Visible = false;

            //셀값 정렬
            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;    //상태
        }

        private void Set_Gv_History(GridView gv)
        {
            //Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기

            //Repository 생성 or 이벤트 입력
            //원가
            RepositoryItemTextEdit txt_Price = new RepositoryItemTextEdit();
            txt_Price.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;

            RepositoryItemSearchLookUpEdit lup_Item = rpo.Create_RepositoryItemSearchLookUp(code.GetMaterialList(new string[] { "20", "40" }), "품목코드", "품명");

            //Column 생성
            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "순번", "Idx");
            grid.Create_GvColumn(gv, "품목", "ItemCode", lup_Item);
            grid.Create_GvColumn(gv, "원가", "Price", txt_Price);
            grid.Create_GvColumn(gv, "원가 적용일", "ApplyDate");
            grid.Create_GvColumn(gv, "변경사유", "Remark");
            grid.Create_GvColumn(gv, "등록자", "CreId");
            grid.Create_GvColumn(gv, "등록일시", "CreDt");
            grid.Create_GvColumn(gv, "수정자", "ModId");
            grid.Create_GvColumn(gv, "수정일시", "ModDt");

            //Column 단일 설정
            //셀값 변경 허용
            grid.Column_AllowEdit(gv.Columns["Price"]);
            grid.Column_AllowEdit(gv.Columns["ApplyDate"]);
            grid.Column_AllowEdit(gv.Columns["Remark"]);

            gv.Columns["Idx"].Visible = false;

            //셀값 정렬
            grid.SetCol_Type(gv.Columns["Price"], clsGrid.TypeList.TFloat);

            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gv.Columns["ApplyDate"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;


            gv.RowStyle += grid.RowStyle_ChangeBg_ByStatus;                 //삭제시 배경색 지정
            gv.CellValueChanged += Event_CellValueChanged;                  //셀값 수정시 상태값 수정
            //gv.GridControl.ProcessGridKey += Event_ProcessGridKey_History;  //행추가, 삽입, 삭제 단축키기능 부여
            //gv.ValidatingEditor += Event_ValidatingEdito_History;           //단가적용일 입력시 중복 확인
        }

        private void SetGv_CustLup(GridView gv)
        {
            //거래처
            string itemDiv = Convert.ToString(Rdo_Item_Div.EditValue);

            string[] custDiv = null;

            switch (itemDiv)
            {
                case "10": //상품
                case "20": //제품
                    custDiv = new string[] { "05", "10", "20", "30", "40", "50" }; break;
                case "30": //반제품
                    custDiv = new string[] { "05", "20", "50" }; break;
                case "40": //원자재
                case "50": //부자재
                case "60": //소모자재
                    custDiv = new string[] { "05", "10", "50" };
                    break;

                case "70": //지그
                    custDiv = new string[] { "05", "10", "20", "50" };    //DREAMCNC 사용중임
                    break;

                default:
                    custDiv = new string[] { "05", "10", "20", "30", "40", "50" };
                    break;
            }

            CM_Cust cust = new CM_Cust();
            RepositoryItemSearchLookUpEdit lup_Cust = rpo.Create_RepositoryItemSearchLookUp(cust.Select_Cust_List(custDiv), "코드", "명칭");

            gv.Columns["CustCode"].ColumnEdit = lup_Cust;
        }

        private void Btn_Grid_Click(object sender, ButtonPressedEventArgs e)
        {
			////제품이미지, 제품사양서 등록

			ButtonEdit btn = sender as ButtonEdit;
			if (btn == null) return;

			GridView gv = Gc_Item.MainView as GridView;
			if (gv == null) return;

			DataRow drChanged = (Gc_Item.MainView as GridView).GetFocusedDataRow();
			string fieldName = gv.FocusedColumn.FieldName;
			string status = drChanged["Status"].ConvertString_Null_To_Empty();

			if (status == "신규")
			{
				XtraMessageBox.Show("품목정보 선저장 필수");
				return;
			}

			string ItemCode = Convert.ToString(drChanged["ItemCode"]);

			///*
			// * 프로세스 옵션
			// * 제품 사양서 조회(I: 이미지, P: PDF)
			// */

			if (fieldName == "btnImg")
			{
                Frm_CM_ItemImage frm = new Frm_CM_ItemImage(ItemCode);
				frm.FormClosed += FrmPictureEdit_FormClosed;
				frm.ShowDialog();
			}
			//else if (fieldName == "Img")
			//{
			//    //품목별 이미지 등록
			//    FrmCM_Item_TypeImage frm = new FrmCM_Item_TypeImage(ItemCode);
			//    frm.ShowDialog();
			//}
			//else if(fieldName == "Work_Standard_Sheet")
			//{
			//    if(WrGlobal.ProcOption["BS"]["Work_Standard_Sheed_IP"] == "P")
			//    {
			//        FrmPDFViewer frm = new FrmPDFViewer(ItemCode);
			//        frm.ShowDialog();
			//    }

			//    else
			//    {
			//        FrmPictureEdit frmPictureEdit = new FrmPictureEdit(ItemCode, fieldName);
			//        frmPictureEdit.FormClosed += FrmPictureEdit_FormClosed;
			//        frmPictureEdit.ShowDialog();
			//    }
			//}
		}

        private void FrmPictureEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            //1. 선택한 row 저장
            int SaveFocusedRowInx = (Gc_Item.MainView as GridView).FocusedRowHandle;

            //2재조회
            Display();

            //3. 저장 후 선택한 row를 불러옴
            (Gc_Item.MainView as GridView).FocusedRowHandle = SaveFocusedRowInx;
        }

        //private void Event_IMG_Click(object sender, RowClickEventArgs e)
        //{
        //    // 더블 클릭된 행 가져오기
        //    GridView gv = sender as GridView;
        //    int rowHandle = e.RowHandle;

        //    string fieldName = gv.FocusedColumn.FieldName;

        //    if (fieldName != "ItemImg")
        //    {
        //        return;
        //    }
        //    else
        //    {
        //        //데이터 불러오기
        //        DataRow drChanged = (Gc_Item.MainView as GridView).GetDataRow(e.RowHandle);
        //        Selected_ItemCd = Convert.ToString(drChanged["ItemCode"]);

        //        FrmPictureEdit frmPictureEdit = new FrmPictureEdit(Selected_ItemCd);
        //        frmPictureEdit.ShowDialog();
        //    }
        //}

        private void Event_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;
            string fieldName = e.Column.FieldName;
            DataRow drChanged = gv.GetDataRow(e.RowHandle);
            string value = e.Value.ConvertString_Null_To_Empty();

            //셀값 수정 시 상태값 수정
            if (e.Column.FieldName == gv.Columns[0].FieldName) return;
            grid.Change_Status(gv, e.RowHandle);

            //if (fieldName == "CustName")         { drChanged["CustCode"]         = drChanged["CustName"]; }
            //if (fieldName == "Vendor_Nm")       { drChanged["Vendor_Cd"]         = drChanged["Vendor_Nm"]; }
            //if (fieldName == "PurchaseCustName") { drChanged["Vendor_Cd"] = drChanged["PurchaseCustName"]; }
            //if (fieldName == "ItemClass_Nm")    { drChanged["ItemClass_Cd"]    = drChanged["ItemClass_Nm"]; }
            //if (fieldName == "Stock_Nm")        { drChanged["Stock_Cd"]        = drChanged["Stock_Nm"]; }

            gv.BestFitColumns();
        }

        #region 행 삽입, 추가, 삭제

        private void Event_ProcessGridKey(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case (Keys.Alt | Keys.I): Btn_RowEdit(cmdInsert, null); break;  //행삽입
                //case (Keys.Alt | Keys.A): Btn_Row_Add.PerformClick(); break;  //행추가
                case (Keys.Alt | Keys.D): Btn_RowEdit(cmdDelete, null); break; //행삭제
                case (Keys.Alt | Keys.V): cmdCopy.PerformClick(); ; break; //행복사
            }
        }

        private void Btn_RowEdit(object sender, EventArgs e)
        {
            //행삽입, 추가, 삭제 버튼 클릭 이벤트
            //1. 이름의 마지막 3글자 가져오기

            smtSearchButton btn = sender as smtSearchButton;
            string txt_Act = btn.Tag.ToString();

            string item_div = Rdo_Item_Div.EditValue.ConvertString_Null_To_Empty();
            string stock_div_Cd = "";
            string bom_Div_Cd = "40";

            //품목구분
            switch (item_div)
            {
                case "10":
                    stock_div_Cd = "20"; //창고구분 : 제품
                    break;

                case "20":
                    stock_div_Cd = "20"; //창고구분 : 제품
                    bom_Div_Cd = "10";  //BOM구성 : 기준품목
                    break;

                case "30":
                    //프로세스 옵션 : 반제품 창고 구분값 디폴트값 설정
                    if (WrGlobal.ProcOption["BS"].ContainsKey("Item_Assembly_Default_Stock_Div"))
                    {
                        stock_div_Cd = WrGlobal.ProcOption["BS"]["Item_Assembly_Default_Stock_Div"];
                    }
                    else
                    {
                        stock_div_Cd = "20"; //창고구분 : 제품
                    }
                    bom_Div_Cd = "30";  //BOM구성 : 기준/구성품목
                    break;

                case "40":
                case "50":
                case "60":
                case "70":
                    stock_div_Cd = "30"; //창고구분 : 자재
                    bom_Div_Cd = "20";  //BOM구성 : 구성품목
                    break;

                default:
                    if (FormType == "Product")
                    {
                        stock_div_Cd = "20"; //창고구분 : 제품
                    }
                    else if (FormType == "Material")
                    {
                        stock_div_Cd = "30"; //창고구분 : 자재
                        //bom_Div_Cd = "20";  //BOM구성 : 구성품목
                    }
                    break;
            }

            ////창고코드
            //string stock_Cd = "";

            //BS_Stock stock = new BS_Stock();

            //DataTable dt = stock.Select_Stock_List(new string[] { stock_div_Cd });

            //if(dt != null)
            //{
            //    if(dt.Rows.Count > 0)
            //    {
            //        stock_Cd = dt.Rows[0]["코드"].ConvertString_Null_To_Empty();
            //    }
            //}

            //2. 입력 데이터 설정
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (txt_Act != "Del")
            {
                //삽입, 추가 진행인 경우
                dic.Add("Status", "신규");
                dic.Add("ItemType", Convert.ToString(Rdo_Item_Div.EditValue) == "ALL" ? "" : Convert.ToString(Rdo_Item_Div.EditValue));
                dic.Add("ItemClass1", Convert.ToString(Lup_ItemClass.EditValue) == "ALL" ? "" : Convert.ToString(Lup_ItemClass.EditValue));
                dic.Add("CustCode", Convert.ToString(LupCustomer.EditValue) == "ALL" ? "" : Convert.ToString(LupCustomer.EditValue));
				dic.Add("UseYn", "Y");
				dic.Add("ExamYn", "Y");
				dic.Add("DisplayYn", "Y");

				//dic.Add("CustName", Convert.ToString(LupCustomer.EditValue) == "ALL" ? "" : Convert.ToString(LupCustomer.EditValue));
				//dic.Add("BOM_Div_Cd", bom_Div_Cd);
				//dic.Add("Stock_Cd", stock_Cd);
				//dic.Add("Stock_Nm", stock_Cd);

			}
            else
            {
                //삭제 진행인경우
                dic.Add("Status", Convert.ToString((Gc_Item.MainView as GridView).GetFocusedRowCellValue("Status")) == "삭제" ? "" : "삭제");
            }

            //3. 행삽입, 추가, 삭제 프로세스 진행
            grid.Set_Row(Gc_Item, dic, txt_Act);
        }

        #endregion 행 삽입, 추가, 삭제 End

        private void Event_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            //셀값 입력시 중복 검사
            grid.Check_OverLap_CellValue(sender as GridView, e, "ItemCode");
            //마이너스 값 검사
            grid.Check_Minus_CellValue(sender as GridView, e, "Base_Weight");
        }

        #endregion GridView Setting End

        #endregion 디자인 아이템 생성 종료

        #region Event

        #endregion Event End

        #region 조회 기능

        private void Rdo_ItemDIv_EditValueChanged(object sender, EventArgs e)
        {
            Display();
        }

        private void ItemClass_EditValueChanged(object sender, EventArgs e)
        {
            Display();
        }

        private void LupCustomer_EditValueChanged(object sender, EventArgs e)
        {
            Display();
        }

        private void Display()
        {
            string itemDiv = Convert.ToString(Rdo_Item_Div.EditValue);

            switch (itemDiv)
            {
                case "10": Gc_Item.MainView = Gv_Goods; break;  //상품
                case "20": Gc_Item.MainView = Gv_Product; break;  //제품
                case "30": Gc_Item.MainView = Gv_Asy; break;  //반제품
                case "40": Gc_Item.MainView = Gv_Material; break;  //원자재
                case "50": Gc_Item.MainView = Gv_Subsidiary; break;  //부자재
                case "60": Gc_Item.MainView = Gv_Consume; break;  //소모자재
                case "70": Gc_Item.MainView = Gv_Jig; break;  //소모자재
                default:
                    if (FormType == "Material") Gc_Item.MainView = Gv_Material;
                    break;
            }

            //자재기본정보 품목구분 전체 추가로 인해 거래처 LOOKUP 데이터 변경부 추가
            if (WrGlobal.ProcOption["BS"]["Item_OneCust"] == "Y")
            {
                SetGv_CustLup(Gc_Item.MainView as GridView);
            }

			//1. 조회 검사
			if (!Check_Search()) return;

			//2.품목 조회
			CM_Item item = new CM_Item();
			item.ItemType = itemDiv;    //품목구분
			item.ItemClass1 = Convert.ToString(Lup_ItemClass.EditValue);  //품목분류
			item.CustCode = Convert.ToString(LupCustomer.EditValue);  //품목분류
			Gc_Item.DataSource = item.Select_Item_Std(itemDiv == "ALL" ? this.ItemType.Split(',') : new string[] { itemDiv });

			(Gc_Item.MainView as GridView).BestFitColumns();   //Column 너비 조정
		}

        private bool Check_Search()
        {
            if((Gc_Item.DataSource as DataTable)?.Select(@"Status = '신규'
                                                                    OR Status = '수정'
                                                                    OR Status = '삭제'").Length > 0)
            {
                if (MessageBox.Show("작성내역이 사라집니다.\n다른항목을 진행하시겠습니까?", "Yes or No", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    //다른항목 진행할 경우
                    return true;
                }
                else
                {
                    //다른항목 진행하지 않을경우
                    return false;
                }
            }

            return true;
        }

        #endregion 조회버튼 클릭 End

        #region 저장 기능

        private void Save()
        {
            GridView gv = (Gc_Item.MainView as GridView);
            gv.PostEditor();
            gv.UpdateCurrentRow();

            //작성내역 저장
            DataTable dtItem = Gc_Item.DataSource as DataTable;

            //1. 데이터 불러온 상태인지 검사
            if (!Check_SaveClick(dtItem)) return;

            //2. 데이터 저장
            for (int curHandle = 0; curHandle < dtItem.Rows.Count; curHandle++)
            {
                if (Convert.ToString(dtItem.Rows[curHandle]["Status"]) == "신규")
                {
                    if (!Save_ToDatabase(dtItem.Rows[curHandle], 'I')) return;
                }
                else if (Convert.ToString(dtItem.Rows[curHandle]["Status"]) == "수정")
                {
                    if (!Save_ToDatabase(dtItem.Rows[curHandle], 'U')) return;
                }
                else if (Convert.ToString(dtItem.Rows[curHandle]["Status"]) == "삭제")
                {
                    if (!Save_ToDatabase(dtItem.Rows[curHandle], 'D')) return;
                }
            }

            //3. 선택한 row 저장
            int SaveFocusedRowInx = gv.FocusedRowHandle;

            MessageBox.Show("저장이 정상적으로 완료되었습니다.");

            //4. 정상절차 진행
            Display();

            //5. 저장 후 선택한 row를 불러옴
            (Gc_Item.MainView as GridView).FocusedRowHandle = SaveFocusedRowInx;
        }

        private bool Check_SaveClick(DataTable dt)
        {
            //Item을 불러온 상태인지 검사
            if (dt == null) return false;

            int checkCnt = 0;
            bool isDelete = false;

            string item_Div = Convert.ToString(Rdo_Item_Div.EditValue);

            foreach (DataRow dr in dt.Rows)
            {
                string state = Convert.ToString(dr["Status"]);
                if (CM_Item.UsePurchasingFields && (state == "신규" || state == "수정"))
                {
                    decimal conversion = dr["PurchaseConversionQty"].ConvertDecimal_Null_To_Zero();
                    decimal tolerance = dr["DeliveryOverTolerancePct"].ConvertDecimal_Null_To_Zero();
                    if ((!dr.IsNull("PurchaseConversionQty") && (conversion <= 0m
                            || conversion > 9999999999999.999999m || decimal.Round(conversion, 6) != conversion))
                        || tolerance < 0m || tolerance > 99999.9999m || decimal.Round(tolerance, 4) != tolerance)
                    {
                        MessageBox.Show("환산수량은 0보다 큰 수(소수 6자리 이내), 납품초과 허용률은 0 이상의 수(소수 4자리 이내)로 입력해 주세요.");
                        return false;
                    }
                }
                if (Convert.ToString(dr["Status"]) == "신규")
                {
                    if (Convert.ToString(dr["CustCode"]) == "")
                    {
                        if (WrGlobal.ProcOption["BS"]["Item_OneCust"] == "Y" && (item_Div == "20"))
                        {
                            MessageBox.Show("거래처 항목이 비어있습니다.");
                            return false;
                        }
                    }

                    if (Convert.ToString(dr["ItemType"]) == "")
                    {
                        MessageBox.Show("품목구분 항목이 비어있습니다.");
                        return false;
                    }

                    if (Convert.ToString(dr["ItemName"]) == "")
                    {
                        MessageBox.Show("품목명 항목이 비어있습니다.");
                        return false;
                    }

                    if (WrGlobal.ProcOption["BS"]["Item_AutoNo"] == "N")
                    {
                        if (Convert.ToString(dr["ItemCode"]) == "")
						{
                            MessageBox.Show("품목코드 항목이 비어있습니다.");
                            return false;
						}

                        //품목코드 체크(중복)
                        string ItemCode = dr["ItemCode"].ConvertString_Null_To_Empty();

                        if (code.Chk_ItemCode(ItemCode).Rows.Count > 0)
                        {
                            MessageBox.Show($"품목코드 : {ItemCode}\n이미 존재하는 품목코드입니다.\n다른 코드로 작성하세요.");
                            return false;
                        }
                    }

                    checkCnt++;
                }
                else if (Convert.ToString(dr["Status"]) == "수정") { checkCnt++; }
                else if (Convert.ToString(dr["Status"]) == "삭제")
                {

                    checkCnt++;
                    isDelete = true;
                }
            }
            if (checkCnt == 0) return false;

            if (isDelete)
            {
                DialogResult dialogResult = MessageBox.Show("삭제내역이 있습니다. 삭제하시겠습니까?", "Delete OK Cancle", MessageBoxButtons.OKCancel);

                if (dialogResult == DialogResult.Cancel)
                {
                    return false;
                }
            }

            return true;
        }

        private bool Save_ToDatabase(DataRow dr, char div)
        {
            //데이터베이스에 반영
            //1. 기본 셋팅
            CM_Item material = new CM_Item();
            material.CorpId = WrGlobal.CorpID;
            material.ItemCode = dr["ItemCode"].ConvertString_Null_To_Empty();
            material.ItemName = dr["ItemName"].ConvertString_Null_To_Empty();
            material.ItemType = dr["ItemType"].ConvertString_Null_To_Empty();
            material.ItemClass1 = dr["ItemClass1"].ConvertString_Null_To_Empty();
            material.ItemClass2 = dr["ItemClass2"].ConvertString_Null_To_Empty();
            material.ItemClass3 = dr["ItemClass3"].ConvertString_Null_To_Empty();
            material.Alias = dr["Alias"].ConvertString_Null_To_Empty();
            material.Size = dr["Size"].ConvertString_Null_To_Empty();
            material.Texture = dr["Texture"].ConvertString_Null_To_Empty();
            material.UnitWeight = dr["UnitWeight"].ConvertFloat_Null_To_Zero();
            material.UnitCode = dr["UnitCode"].ConvertString_Null_To_Empty();
            material.ExamYn = dr["ExamYn"].ConvertString_Null_To_Empty();
            material.UseYn = dr["UseYn"].ConvertString_Null_To_Empty();
            material.DisplayYn = dr["DisplayYn"].ConvertString_Null_To_Empty();
            material.CustCode = dr["CustCode"].ConvertString_Null_To_Empty();
            material.SaftyQty = dr["SaftyQty"].ConvertFloat_Null_To_Zero();
            material.Remark = dr["Remark"].ConvertString_Null_To_Empty();
            material.PackUnit = dr["PackUnit"].ConvertString_Null_To_Empty();
            material.PackQty = dr["PackQty"].ConvertFloat_Null_To_Zero();
            material.Abbreviation = dr["Abbreviation"].ConvertString_Null_To_Empty();
            material.EnglishAbbreviation = dr["EnglishAbbreviation"].ConvertString_Null_To_Empty();
            material.ItemGroup = dr["ItemGroup"].ConvertString_Null_To_Empty();
            material.SupplyType = dr["SupplyType"].ConvertString_Null_To_Empty();
            material.MaterialT = dr["MaterialT"].ConvertString_Null_To_Empty();
            material.Width = dr["Width"].ConvertString_Null_To_Empty();
            material.Height = dr["Height"].ConvertString_Null_To_Empty();
            material.NetWeight = dr["NetWeight"].ConvertDecimal_Null_To_Zero();
            material.YogurtG = dr["YogurtG"].ConvertDecimal_Null_To_Zero();
            material.YogurtKg = dr["YogurtKg"].ConvertDecimal_Null_To_Zero();
            material.PTLoadQty = dr["PTLoadQty"].ConvertInt_Null_To_Zero();
            material.BarCode = dr["BarCode"].ConvertString_Null_To_Empty();
            material.OldItemCode = dr["OldItemCode"].ConvertString_Null_To_Empty();
            material.SetPurchasingFields(dr);

            //material.RoutingCode = dr["RoutingCode"].ConvertString_Null_To_Empty();
            material.HouseID = dr["HouseID"].ConvertString_Null_To_Empty();


            //품목단가구분
            string priceType = ""; //10:매입, 20:매출


            //품목구분에따른 단가구분 설정
            switch (material.ItemType)
            {
                //제품, 반제품인 경우 매출
                case "10":
                case "20":
                case "30":
                    priceType = "20";
                    break;

                //자재인 경우 매입
                case "40":
                case "50":
                case "60":
                case "70":
                    priceType = "10";
                    break;

                default:
                    priceType = "10";
                    break;
            }

            //2. DB 적용
            if (div == 'I')
            {
                //Insert
                if (!material.Insert_CM_Item())
                {
                    //저장 중 에러 발생시
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return false;
                }
                else
                {
                    //상태값 초기화
                    dr[0] = "";
                }

                /*
                if (material.ItemType == "20")
                {
                    //프로세스 옵션 - 제품등록시 라우팅 자동 등록
                    if (WrGlobal.ProcOption["BS"]["AutoRoutingYN"] == "Y")
                    {
                        // CM_BOMRev Insert
                        CM_BOMRev cbm = new CM_BOMRev();
                        cbm.ItemCode = material.ItemCode;
                        cbm.Revision = "001";
                        cbm.RoutingCode = "RT0001";
                        cbm.StartDate = DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss");
                        cbm.EndDate = new DateTime(2999, 12, 31).ToString("yyyy-MM-dd HH:mm:ss");
                        cbm.UseYn = "Y";

                        cbm.InsertBomMaster();

                        // CM_BOM Insert
                        CM_BOM cb = new CM_BOM();
                        cb.PItemCode = material.ItemCode;
                        cb.Revision = cbm.Revision;

                        cb.InsertBOMByRoutingCode(cbm.RoutingCode);


                        //CM_Item Update
                        material.Revision = cbm.Revision;
                        material.RoutingCode = cbm.RoutingCode;

                        material.UpdateRevisionRoutingCode();
                    }
                }
                */
                 

                //추가 변경사항 private 따로 만들기
                //거래처 등록하는 경우
                if (material.CustCode != "")
                {
                    //단가이력에 0원으로 등록
                    CM_ItemPrice ip = new CM_ItemPrice();
                    ip.PriceType = priceType;
                    ip.ItemCode = material.ItemCode;
                    ip.CustCode = material.CustCode;
                    ip.Price = 0;
                    ip.ApplyDate = DateTime.Now.ToString("d");

                    //기존 단가정보 있는지 확인
                    DataRow drPrice = ip.Get_MaterialPrice();

                    if(drPrice == null)
                    {
                        if (ip.ItemCode != "")
                        {
                            if (!ip.Ins_MaterialPrice())
                            {
                                MessageBox.Show("저장 중 에러가 발생하였습니다.");
                                return false;
                            }
                        }
                    }
                }
            }
            else if(div == 'U')
            {
                //거래처 수정하는 경우
                if (material.CustCode != "")
                {
                    //기존 등록된 거래처랑 다른경우 신규입력
                    //기존 거래처 입력된 거래처 검색
                    DataRow drBefore = material.Select_Item_RowInfo();
                    if(drBefore != null)
                    {
                        string BfCustCode = Convert.ToString(drBefore["CustCode"]);

                        //기존 입력된 거래처랑 수정된 거래처가 다르면 단가이력에 입력
                        if (BfCustCode != material.CustCode)
                        {
                            //단가이력에 0원으로 등록
                            CM_ItemPrice ip = new CM_ItemPrice();
                            ip.PriceType = priceType;
                            ip.ItemCode = material.ItemCode;
                            ip.CustCode = material.CustCode;
                            ip.Price = 0;
                            ip.ApplyDate = DateTime.Now.ToString("d");

                            //기존 단가정보 있는지 확인
                            DataRow drPrice = ip.Get_MaterialPrice();

                            if (drPrice == null)
                            {
                                if (ip.ItemCode != "")
                                {
                                    if (!ip.Ins_MaterialPrice())
                                    {
                                        MessageBox.Show("저장 중 에러가 발생하였습니다.");
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                }

                //Update
                if (!material.Update_CM_Item())
                {
                    //저장 중 에러 발생시
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return false;
                }
                else
                {
                    //상태값 초기화
                    dr[0] = "";
                }

                //추가 변경사항 private 따로 만들기

            }
            else if (div == 'D')
            {
                //단가 삭제
                CM_ItemPrice ip = new CM_ItemPrice();
                ip.ItemCode = material.ItemCode;
                ip.Del_MaterialPriceByCode();

                //BOM 삭제
                CM_BOM bom = new CM_BOM();
                bom.PItemCode = material.ItemCode;
                //bom.Me_ItemCode = item.ItemCode;
                bom.DeleteBOMByMItemCode();

                //이미지삭제
                CM_ItemImage im = new CM_ItemImage();
                im.ItemCode = material.ItemCode;
                im.Del_item_Image_ByCode();

                //Delete
                if (!material.Delete_CM_Item())
                {
                    //저장 중 에러 발생시
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return false;
                }
                else
                {

                    //상태값 초기화
                    dr[0] = "";
                }

                //추가 변경사항 private 따로 만들기
            }

            return true;
        }


        #endregion 저장 기능 End

        #region History

        private int? currentRowHandle = null;

        private void Btn_History_Click(object sender, EventArgs e)
        {
            // Search_ItemPrice_History();

            // 현재 포커스 된 행 가져오기
            var view = Gv_Product;
            int clickedRowHandle = view.FocusedRowHandle;

            // 같은 행의 버튼을 한 번 더 클릭한 경우 → 하단 그리드 숨김
            if (currentRowHandle == clickedRowHandle)
            {
                //Gv_History.GridControl.DataSource = null;
                layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                currentRowHandle = null;

                return;
            }

            // 다른 행의 버튼 클릭 → 하단 그리드 갱신
            currentRowHandle = clickedRowHandle;

            // 선택된 행의 데이터 가져오기 (예시)
            var selectedRow = view.GetRow(clickedRowHandle);
            if (selectedRow != null)
            {
                Search_ItemPrice_History();
            }
            else
            {
                layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }

        private void Search_ItemPrice_History()
        {
            //해당품목 거래처별 단가 조회
            //Check_Search_History();

            //GridView gv = Gc_Item.MainView as GridView;
            //DataRow dr = gv.GetFocusedDataRow();

            ////신규Row면 단가이력 안보여주기
            //if (Convert.ToString(dr["Status"]) != "신규")
            //{
            //    layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

            //    CM_ItemPriceHistory history = new CM_ItemPriceHistory();
            //    history.ItemCode = dr["ItemCode"].ConvertString_Null_To_Empty();
            //    history.PriceType = "30"; //단가

            //    Gc_History.DataSource = history.Select_Material_PriceHistory();
            //    Gv_History.BestFitColumns();
            //}
        }

        private bool Check_Search_History()
        {
            GridView gv = Gv_History;

            for (int idx = 0; idx < gv.RowCount; idx++)
            {
                DataRow dr = gv.GetDataRow(idx);
                string status = Convert.ToString(dr["Status"]);

                if (status == "신규"
                    || status == "수정"
                    || status == "삭제")
                {
                    return MessageBox.Show("작성내역이 사라집니다.\n다른항목을 진행하시겠습니까?", "Yes or No", MessageBoxButtons.YesNo) == DialogResult.Yes;
                }
            }
            return true;
        }

        private void Btn_RowEdit_History(object sender, EventArgs e)
        {
            GridView gv = Gc_Item.MainView as GridView;
            DataRow dr = gv.GetFocusedDataRow();

            //행삽입, 추가, 삭제 버튼 클릭 이벤트
            //1. 이름의 마지막 3글자 가져오기
            SimpleButton btn = sender as SimpleButton;
            string txt_Act = btn.Name.Substring(btn.Name.Length - 3);

            //2. 입력 데이터 설정
            Dictionary<string, object> dic = new Dictionary<string, object>();

            if (txt_Act != "Del")
            {
                //삽입, 추가 진행인 경우
                dic.Add("Status", "신규");
                dic.Add("ItemCode", dr["ItemCode"].ConvertString_Null_To_Empty());
                dic.Add("Price", 0);
            }
            else
            {
                //삭제 진행인경우
                dic.Add("Status", Convert.ToString(Gv_History.GetFocusedRowCellValue("Status")) == "삭제" ? "" : "삭제");
            }

            //3. 행삽입, 추가, 삭제 프로세스 진행
            grid.Set_Row(Gc_History, dic, txt_Act);
        }

        private void Btn_History_Save_Click(object sender, EventArgs e)
        {
            //Func_Save_History();
        }

        private void Func_Save_History()
        {
            ////작성내역 저장
            //GridView gv = Gv_History;

            ////1. 데이터 불러온 상태인지 검사
            //try
            //{
            //    if (!Check_History_SaveClick(gv)) return;

            //    bool Success = true;
            //    //2. 데이터 저장
            //    for (int idx = 0; idx < gv.RowCount; idx++)
            //    {
            //        DataRow dr = gv.GetDataRow(idx);
            //        string status = Convert.ToString(dr["Status"]);

            //        switch (status)
            //        {
            //            case "신규": if (!History_Save_ToDatabase(dr, 'I')) { Success = false; } break;
            //            case "수정": if (!History_Save_ToDatabase(dr, 'U')) { Success = false; } break;
            //            case "삭제": if (!History_Save_ToDatabase(dr, 'D')) { Success = false; } break;
            //        }

            //        //Row별 정상처리 절차
            //        if (Success)
            //        {
            //            //상태값 초기화
            //            dr[0] = "";
            //        }
            //        else
            //        {
            //            break;
            //        }
            //    }

            //    //3. 정상절차 진행
            //    if (Success)
            //    {
            //        MessageBox.Show("저장이 정상적으로 완료되었습니다.");
            //        Search_ItemPrice_History();
            //    }
            //    else
            //    {
            //        MessageBox.Show("저장 중 오류가 발생하였습니다.");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private bool Check_History_SaveClick(GridView gv)
        {
            //Item을 불러온 상태인지 검사
            if (gv.RowCount == 0) return false;


            for (int idx = 0; idx < gv.RowCount; idx++)
            {
                DataRow dr = gv.GetDataRow(idx);
                string status = Convert.ToString(dr["Status"]);

                if (status == "신규")
                {
                    //미입력된 칸 있는지 확인
                    if (Convert.ToString(dr["Price"]) == "")
                    {
                        MessageBox.Show("단가가 비어있습니다.");
                        return false;
                    }
                    else if (Convert.ToString(dr["ApplyDate"]) == "")
                    {
                        MessageBox.Show("단가적용일이 비어있습니다.");
                        return false;
                    }
                }
                else if (status == "수정")
                {
                    //등록이력 수정이 안돼서 주석처리 함

                    //BS_ItemPrice_History his = new BS_ItemPrice_History();
                    //his.Price_Div_Cd = Convert.ToString(dr["Price_Div_Cd"]);
                    //if (MainObject == "Item")
                    //{
                    //his.ItemCode = Selected_Main_Cd;
                    //his.Cust_Cd = Selected_Sub_Cd;
                    //}
                    //else if (MainObject == "Cust")
                    //{
                    //his.Cust_Cd = Selected_Main_Cd;
                    //his.ItemCode = Selected_Sub_Cd;
                    //}
                    //his.Apply_Date = dr["Apply_Date"].ConvertDate_To_Char8();

                    //if (his.Select_ItemPrice_History_ApplyDate() != null)
                    //{
                    //MessageBox.Show("해당날짜에 이미 등록된 내역이 있습니다.");
                    //return false;
                    //}
                }
                else if (status == "삭제")
                {
                    return MessageBox.Show("삭제내역이 있습니다. 삭제하시겠습니까?", "Delete OK Cancle", MessageBoxButtons.OKCancel) == DialogResult.OK;
                }
            }
            return true;
        }

        //private bool History_Save_ToDatabase(DataRow dr, char div)
        //{
        //    //데이터베이스에 반영
        //    //1. 기본 셋팅
        //    CM_ItemPriceHistory history = new CM_ItemPriceHistory();
        //    history.Idx = dr["Idx"].ConvertInt_Null_To_Zero();
        //    history.PriceType = "30"; // 품목정보에서 등록 시 단가로 고정
        //    history.CustCode = dr["CustCode"].ConvertString_Null_To_Empty();
        //    history.ItemCode = dr["ItemCode"].ConvertString_Null_To_Empty();
        //    history.Price = dr["Price"].ConvertDecimal_Null_To_Zero();
        //    history.ApplyDate = dr["ApplyDate"].ConvertDate_To_Char10();
        //    history.Remark = dr["Remark"].ConvertString_Null_To_Empty();

        //    //2. DB 적용
        //    if (div == 'I')
        //    {
        //        if (!history.Ins_MaterialPriceHistory())
        //        {
        //            return false;
        //        }
        //    }
        //    else if (div == 'U')
        //    {
        //        if (!history.Upd_MaterialPriceHistoryByIdx())
        //        {
        //            return false;
        //        }
        //    }
        //    else if (div == 'D')
        //    {
        //        if (!history.Del_MaterialPriceHistoryByIdx())
        //        {
        //            return false;
        //        }
        //    }

        //    return true;
        //}

        private void Btn_History_Close_Click(object sender, EventArgs e)
        {
            layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }

        #endregion History

        private void Btn_ptrintPIC_Click(object sender, EventArgs e)
        {
            //GridView gv = Gc_Item.MainView as GridView;

            ////dt row 확인
            //DataTable dt = gv.GridControl.DataSource as DataTable;

            ////선택된 row
            //int[] selrow = gv.GetSelectedRows();
            //if (selrow.Length == 0)
            //{
            //    XtraMessageBox.Show("선택한 제품이 없습니다.", "");
            //    return;
            //}

            //DataTable selDataTable = dt.Clone();

            //for (int cnt = 0; cnt < gv.SelectedRowsCount; cnt++)
            //{
            //    DataRow dr = selDataTable.NewRow();
            //    dr.ItemArray = gv.GetDataRow(selrow[cnt]).ItemArray;
            //    dr["Alias"] = gv.GetRowCellDisplayText(selrow[cnt], "Alias");
            //    dr["ItemClass_Nm"] = gv.GetRowCellDisplayText(selrow[cnt], "ItemClass_Nm");
            //    dr["ItemName"] = gv.GetRowCellDisplayText(selrow[cnt], "ItemName");
            //    dr["ItemCode"] = gv.GetRowCellDisplayText(selrow[cnt], "ItemCode");

            //    selDataTable.Rows.Add(dr);
            //}

            //Rpt_ProductBarcode report = new Rpt_ProductBarcode(selDataTable);
            //ReportPrintTool reportTool = new ReportPrintTool(report);
            //reportTool.PreviewForm.PrintControl.Zoom = 0.9f;
            //reportTool.ShowPreviewDialog();
        }

        private void btn_ExcelUp_Click(object sender, EventArgs e)
        {
            Frm_CM_ExcelUpLoad_Item frm = new Frm_CM_ExcelUpLoad_Item();
            frm.Gc = Gc_Item;
            frm.formName = smtCommonBar1.smtImageTitleText;

            frm.ShowDialog();
        }


		private void cmd_Click(object sender, EventArgs e)
		{
            smtSearchButton btn = sender as smtSearchButton;

            switch (btn.Name)
			{
                case "cmdDisplay":
                    Display();
                    break;

                case "cmdCopy":
                    CopyRow();
                    break;

                case "cmdSave":
                    Save();
                    break;

                case "cmdExcelUp":
                    Frm_CM_ExcelUpLoad_Item frm = new Frm_CM_ExcelUpLoad_Item();
                    frm.Gc = Gc_Item;
                    frm.formName = smtCommonBar1.smtImageTitleText;

                    frm.ShowDialog();
                    break;

                case "cmdExcelDown":
                    clsFunction func = new clsFunction();
                    func.ExportExcel(Gc_Item.MainView as GridView);
                    break;

                case "cmdClose":
                    this.Close();
                    break;
			}
		}

        private void CopyRow()
		{
            GridView gv = Gc_Item.MainView as GridView;
            if (gv == null) return;

            DataTable dt = Gc_Item.DataSource as DataTable;
            if (dt == null) return;

            DataRow dr = gv.GetFocusedDataRow();
            int idx = dt.Rows.IndexOf(dr);

            DataRow drNew = dt.NewRow();
            //drNew.ItemArray = dr.ItemArray;   //전체복사
            drNew["Status"] = "신규";
            drNew["CustCode"] = dr["CustCode"];
            drNew["ItemType"] = dr["ItemType"];

            dt.Rows.InsertAt(drNew, idx + 1);
        }

        private void cmdDisplay_Click(object sender, EventArgs e)
        {
            Display();
        }
    }
}
