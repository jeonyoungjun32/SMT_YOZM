using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SmartCommon.Common;

namespace SmartMES.Common
{
	class cls_Database
    {
        Database db = new Database();
        string query = "";

        /// <summary>
        /// 테이블의 마지막 No 채번(-0000) 번호 가져오기
        /// </summary>
        /// <param name="table_Nm">테이블 명</param>
        /// <param name="colNo_Nm">No Column 명</param>
        /// <param name="stdDate">기준일자</param>
        /// <returns></returns>
        public int Get_Last_NoSeq(string table_Nm, string colNo_Nm, string stdDate, string Initial = null)
        {
            //Table의 Table No 생성
            query = $@"SELECT TOP 1
                                     CONVERT(INT, RIGHT({colNo_Nm}, 4))
                                FROM {table_Nm}
                              WHERE {colNo_Nm} LIKE '%{Initial}{stdDate}%'
                            ORDER BY {colNo_Nm} DESC";

            DataRow dr = db.GetDataRow(query);
            return dr == null ? 1 : Convert.ToInt16(dr[0]) + 1;
        }

        public int Get_NoSeq(string table_Nm, string colNo_Nm, string no, string seq)
        {
            //Table의 Table No 생성
            query = $@"SELECT TOP 1
                                     {seq}
                                FROM {table_Nm}
                               WHERE {colNo_Nm} LIKE '%{no}%'
                            ORDER BY {seq} DESC";

            DataRow dr = db.GetDataRow(query);
            return dr == null ? 1 : Convert.ToInt16(dr[0]) + 1;
        }

        public int Get_Last_Cd(string Corp_Cd, string Initial, string table_Nm, string colNo_Nm, int numLength = 4)
        {
            //Table의 No 생성
            query = $@"
                        SELECT TOP 1 CONVERT(INT, RIGHT({colNo_Nm}, {numLength}))
                        FROM {table_Nm}
                        WHERE CorpId = '{Corp_Cd}'
                        AND {colNo_Nm} LIKE '%{Initial}%'
                        ORDER BY {colNo_Nm} DESC";

            DataRow dr = db.GetDataRow(query);
            return dr == null ? 1 : Convert.ToInt16(dr[0]) + 1;
        }

        public DataTable Get_FIFO_ByMatInOut(string LotNo, string Type)
        {
            //자재입,출고 내역에서 LotNo 선입선출 대상 검색
            // Type == "FIFO" 선입선출 대상 리스트 검색
            query = $@"DECLARE @ItemCd nvarchar(20)
                        SELECT TOP 1 @ItemCd = ItemCode 
                          FROM MT_MatIn 
                         WHERE CorpId = '{WrGlobal.CorpID}' 
                           AND MatLot_No = '{LotNo}'

                        SELECT FIFO.*, i.Item_Nm, i.Alias,  i.Grade, mi.In_Date, i.Spec, i.Unit, mi.In_No, mi.In_Row_Seq, mi.Stock_Cd
                        FROM
                        (
	                        SELECT ItemCode, MatLot_No, sum(In_Qty - Out_Qty) AS Remain_Qty
	                        FROM
	                        (
		                        SELECT CorpId, MatLot_No, ItemCode, sum(In_Qty) AS In_Qty, 0 AS Out_Qty
		                        FROM MT_MatIn
		                        GROUP BY CorpId, MatLot_No, ItemCode

		                        UNION

		                        SELECT CorpId, MatLot_No, ItemCode, 0 AS In_Qty, sum(Out_Qty) AS  Out_Qty
		                        FROM MT_MatOut
		                        GROUP BY CorpId, MatLot_No, ItemCode
	                        ) InOutList

	                        WHERE CorpId = '{WrGlobal.CorpID}' 
	                        AND InOutList.ItemCode = @ItemCd ";
            if (Type == "FIFO")
                query += $@"AND MatLot_No < '{LotNo}' ";
            else
                query += $@"AND MatLot_No = '{LotNo}' ";


	            query += $@"GROUP BY MatLot_No, ItemCode
                        ) FIFO
                        JOIN BS_Item i on  i.ItemCode = FIFO.ItemCode
                        JOIN MT_MatIn mi on mi.MatLot_No = FIFO.MatLot_No
                        WHERE Remain_Qty > 0
                        ORDER BY ItemCode, MatLot_No ";

            DataTable dt = db.GetDataTable(query);
            return dt;
        }

        /// <summary>
        /// 초성으로 검색하기위한 Query 일부 반환(한글전용) / WHERE절 검색할 Column 뒤에 붙여서 사용
        /// </summary>
        /// <param name="SearchIni">검색할 초성(ㄱ,ㄴ 등)</param>
        /// <returns></returns>
        public string GetQuery_SearchTxt_ByIniital(char SearchIni)
        {
            //초성검색 query
            List<char> codeList = new List<char>(new char[] { 'ㄱ', 'ㄴ', 'ㄷ', 'ㄹ', 'ㅁ', 'ㅂ', 'ㅅ', 'ㅇ', 'ㅈ', 'ㅊ', 'ㅋ', 'ㅍ', 'ㅌ', 'ㅎ', '힣' }); //초성리스트

            char endIni = codeList[codeList.IndexOf(SearchIni) + 1];    //종료? 이니셜 추출

            return $" BETWEEN '{SearchIni}' AND '{endIni}' ";   //반환할 Query
        }

        /// <summary>
        /// 검색할 초성에따라 DataTable 추출
        /// </summary>
        /// <param name="dt">추출할 DataTable</param>
        /// <param name="fieldName">검색할 Column FieldName</param>
        /// <param name="searchTxt">검색어</param>
        /// <returns></returns>
        public DataTable ExtractDataTable(DataTable dt, string fieldName, string searchTxt)
        {
            //초성검색
            if (dt == null || fieldName == "" || searchTxt == "") return dt;

            char[] chr = { 'ㄱ', 'ㄲ', 'ㄴ', 'ㄷ', 'ㄸ', 'ㄹ', 'ㅁ', 'ㅂ', 'ㅃ', 'ㅅ', 'ㅆ',
                           'ㅇ', 'ㅈ', 'ㅉ', 'ㅊ','ㅋ','ㅌ', 'ㅍ', 'ㅎ' };

            string[] str = { "가", "까", "나", "다", "따", "라", "마", "바", "빠", "사", "싸",
                           "아", "자", "짜", "차","카","타", "파", "하" };

            int[] chrint = {44032,44620,45208,45796,46384,46972,47560,48148,48736,49324,49912,
                               50500,51088,51676,52264,52852,53440,54028,54616,55204};  //다음초성까지 사이 글자 수
            string pattern = "";    //검색식

            //List 검색식 생성
            for (int fullIni_Idx = 0; fullIni_Idx < searchTxt.Length; fullIni_Idx++)
            {
                char searchIni = searchTxt[fullIni_Idx];

                //초성만 입력되었을때 : 해당 초성 검색구간 지정
                if (searchIni >= 'ㄱ' && searchIni <= 'ㅎ')
                {
                    for (int j = 0; j < chr.Length; j++)
                    {
                        if (searchIni == chr[j])
                        {
                            pattern += string.Format("[{0}-{1}]", str[j], (char)(chrint[j + 1] - 1));
                        }
                    }
                }
                //완성된 문자를 입력했을때 검색패턴 쓰기
                else if (searchIni >= '가')
                {
                    //받침이 있는지 검사
                    int magic = ((searchIni - '가') % 588);

                    //받침이 없을때.
                    if (magic == 0)
                    {
                        pattern += string.Format("[{0}-{1}]", searchIni, (char)(searchIni + 27));
                    }

                    //받침이 있을때
                    else
                    {
                        magic = 27 - (magic % 28);
                        pattern += string.Format("[{0}-{1}]", searchIni, (char)(searchIni + magic));
                    }
                }
                //영어를 입력했을때
                else if (searchIni >= 'A' && searchIni <= 'z')
                {
                    pattern += searchIni;
                }
                //숫자를 입력했을때
                else if (searchIni >= '0' && searchIni <= '9')
                {
                    pattern += searchIni;
                }
            }

            //해당검색조건에 맞는 리스트 추출
            List<string> list = new List<string>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(Convert.ToString(row["CustName"]));
            }
            var res = list.Where(colData => System.Text.RegularExpressions.Regex.IsMatch(colData.ToString(), pattern));


            //기존 DataTable이 손상가지 않는 선에서 해당 Row 삭제
            DataTable returnDt = dt.Copy();

            foreach (DataRow dr in returnDt.Rows)
            {
                foreach (var SearchData in res)
                {
                    if(Convert.ToString(dr[fieldName]) == SearchData)
                    {
                        returnDt.Rows.Remove(dr);
                        break;
                    }
                }
            }

            return returnDt;
        }

        public int Get_From_NoSeq(string table_Nm, string colNo_Nm, string stdDate, string Initial = null)
        {
            //Table의 Table No 생성
            query = $@"SELECT TOP 1 CONVERT(INT, RIGHT({colNo_Nm}, 4))
                                FROM {table_Nm}
                               WHERE left({colNo_Nm},8) = '{Initial}{stdDate}'
                            ORDER BY RIGHT({colNo_Nm}, 4) DESC";

            System.Diagnostics.Debug.WriteLine(query);

            DataRow dr = db.GetDataRow(query);
            return dr == null ? 1 : Convert.ToInt16(dr[0]) + 1;
        }

        public string Get_AutoNumber(string type, string date)
        {
            query = $@"Declare @AutoNumber varchar(20);
                                EXEC  [Code_AutoNumber] '{WrGlobal.CorpID}', '{type}', '{date}', @AutoNumber OUTPUT;
                                SELECT @AutoNumber AS num";

            return db.GetDataRow(query)["num"].ToString();
        }
        public string Get_AutoNumber_Seq(string type, string date)
        {
            query = $@"Declare @AutoNumber varchar(20);
                                EXEC  [Code_AutoNumber_Seq] '{WrGlobal.CorpID}', '{type}', '{date}', @AutoNumber OUTPUT;
                                SELECT @AutoNumber AS num";

            return db.GetDataRow(query)["num"].ToString();
        }
    }
}