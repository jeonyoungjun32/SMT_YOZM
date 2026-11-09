using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors;
using SmartMES.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartCommon.Common;
using baseSMT;
using DevExpress.Pdf.Native.BouncyCastle.Asn1.Ocsp;

namespace SmartMES.Models
{
    public class clsCode
    {
        Database db = new Database();
        string query = "";

        #region IDisposable Support
        private bool disposedValue = false; // 중복 호출을 검색하려면

        ~clsCode()
        {
            this.Dispose(false);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 관리되는 상태(관리되는 개체)를 삭제합니다.
                }

                // TODO: 관리되지 않는 리소스(관리되지 않는 개체)를 해제하고 아래의 종료자를 재정의합니다.
                // TODO: 큰 필드를 null로 설정합니다.
                db.Dispose();
                db = null;

                disposedValue = true;
            }
        }

        // TODO: 위의 Dispose(bool disposing)에 관리되지 않는 리소스를 해제하는 코드가 포함되어 있는 경우에만 종료자를 재정의합니다.
        // ~WrCategories()
        // {
        //   // 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
        //   Dispose(false);
        // }

        // 삭제 가능한 패턴을 올바르게 구현하기 위해 추가된 코드입니다.
        public void Dispose()
        {
            // 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
            Dispose(true);
            // TODO: 위의 종료자가 재정의된 경우 다음 코드 줄의 주석 처리를 제거합니다.
            // GC.SuppressFinalize(this);
        }
        #endregion

        #region Property
        public string Class { get; set; }
        public string CustCode { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemType { get; set; }
        public string SizeType { get; set; }
        public int UnitStandard { get; set; }
        public int RollWidth { get; set; }
        public int RollHeight { get; set; }
        public int PouchWidth { get; set; }
        public int PouchHeight { get; set; }
        public int PouchBase { get; set; }
        public int UnitHeight { get; set; }
        public int FabricWidth1 { get; set; }
        public int FabricWidth2 { get; set; }
        public int FabricWidth3 { get; set; }
        public int FabricWidth4 { get; set; }
        public int FabricWidth5 { get; set; }
        public int ColorCnt { get; set; }
        public string Color1 { get; set; }
        public string Color2 { get; set; }
        public string Color3 { get; set; }
        public string Color4 { get; set; }
        public string Color5 { get; set; }
        public string Color6 { get; set; }
        public string Color7 { get; set; }
        public string Color8 { get; set; }
        public string Color9 { get; set; }
        public string Color10 { get; set; }
        public int UnitPrice { get; set; }

        #endregion


        public DataTable smtLookUp(string DIV, string Type, string All, string UseYn = "Y")
        {
            Database db = new Database();
            string query = $"exec SMT_LookUpEditGet '{DIV}','{Type}','','','{All}','{UseYn}'";
            return db.GetDataTable(query);
        }

        public DataTable smtLookUp(string DIV, string Type, string Type2, string All, string UseYn = "Y")
        {
            Database db = new Database();
            string query = $"exec SMT_LookUpEditGet '{DIV}','{Type}','{Type2}','','{All}','{UseYn}'";
            return db.GetDataTable(query);
        }

        public DataTable smtLookUp(string DIV, string[] Types, string All, string UseYn = "Y")
        {
            string Type1 = "";
            string Type2 = "";
            string Type3 = "";
            if (Types != null)
            {
                Type1 = Types.Length > 0 ? Types[0] : "";
                Type2 = Types.Length > 1 ? Types[1] : "";
                Type3 = Types.Length > 2 ? Types[2] : "";
            }

            Database db = new Database();
            string query = $"exec SMT_LookUpEditGet '{DIV}','{Type1}','{Type2}','{Type3}','{All}','{UseYn}'";
            return db.GetDataTable(query);
        }

        public bool ExecuteQry(string _query)
        {
            return db.ExecuteQuery(_query);
        }

        public DataView GetCorp()
        {
            query = string.Format("select CorpId 코드, CorpName 내용 from SM_Corp Order by CorpId");
            return db.GetDataView("사업장", query);
        }

        public DataRow GetCorpLogoImg(string corpId)
        {
            query = $@"select LogoImg from SM_Corp WHERE CorpId = '{corpId}'";

            return db.GetDataRow(query);
        }

        public void LookupCorp(ref LookUpEdit search)
        {
            search.Properties.DataSource = GetCorp();
            search.Properties.DisplayMember = "내용";
            search.Properties.ValueMember = "코드";
        }

        public DataTable GetCustomer(string [] type = null)
        {
            query = $@"select CustCode 거래처코드, CustName 거래처명 from CM_Cust Where CorpId = '{WrGlobal.CorpID}'";

            if (type != null) query += $"\n AND CustType IN ('{string.Join("','", type)}') ";
            return db.GetDataTable(query);
        }

        public DataTable Select_CM_CateItem(bool all_Yn = false, string[] selected = null)
        {
            //Class에 해당하는 공통코드 Table 불러오는 query
            query = "";

            if (all_Yn)
            {   //조회항목에서 전체를 추가하고 싶을경우
                query += @"SELECT'ALL' AS ItemCode, '전체' AS Title --, '' AS Attr
                        UNION ALL ";
            }

            query += $@"SELECT ClassItemCode AS ItemCode, Title --, Attr
                         FROM CM_CateItem
                        WHERE Class = '{Class}'
                          AND UseYn = 'Y'
                          AND CorpId = '{WrGlobal.CorpID}'";

            if (selected != null &&
                selected.Length > 0)
            {
                query += " AND (";

                for (int idx = 0; idx < selected.Length; idx++)
                {
                    if (idx != 0)
                    {
                        query += " OR ";
                    }

                    query += $@"ClassItemCode = '{selected[idx]}'";
                }

                query += ")";
            }

            return db.GetDataTable(query);
        }

        public DataTable GetCustomerList(string[] type = null, bool allYn = true)
        {
            query = "";

            if (allYn)
            {   //조회항목에서 전체를 추가하고 싶을경우
                query += @"SELECT'ALL' AS 거래처코드, '전체' AS 거래처명
                            UNION ALL ";
            }

            query += $@"select CustCode 거래처코드, CustName 거래처명 from CM_Cust Where CorpId = '{WrGlobal.CorpID}'
                        AND UseYn = 'Y'
                    ";

            if (type != null)
            {
                if(type.Length == 1 && type[0].ToUpper() == "ALL")
				{

				}
				else
				{
                    query += $"\n AND CustType IN ('{string.Join("','", type)}') ";
				}
            }
            return db.GetDataTable(query);
        }

        public void LookupCustomer(string corpId, string custType, bool allYn, string useYn, ref SearchLookUpEdit search)
        {
            //custType = '' or '매입' or '매출' or '외주'
            search.Properties.DataSource = smtLookUp("거래처", custType, allYn ? "전체" : "", useYn);
            search.Properties.DisplayMember = "거래처명";
            search.Properties.ValueMember = "거래처코드";
        }

        public void LookupCustomer(string corpId, string custType, bool allYn, string useYn, ref smtSearchLookUp search)
        {
            //custType = '' or '매입' or '매출' or '외주'

            search.smtLookUpDataSource = smtLookUp("거래처", custType, allYn ? "전체" : "", useYn);
            search.smtLookUpDataDisplayMember = "거래처명";
            search.smtLookUpDataValueMember = "거래처코드";
        }

        public void LookupWareHouse(string corpId, ref LookUpEdit search)
        {
            search.Properties.DataSource = GetWarehouseList();
            search.Properties.DisplayMember = "창고명";
            search.Properties.ValueMember = "창고코드";
        }

        public string GetAutoNumber(string docType, DateTime currentDate)
        {
            return db.ExecAutoNumber(docType, currentDate);
        }

        public DataView GetCategory()
        {
            query = string.Format("Select * FROM CM_CLASS Where CorpId = '{0}' AND DisplayYn = 'Y' ",WrGlobal.CorpID);
            return db.GetDataView("분류코드", query);
        }

        public DataTable GetCategory_HighClass(string highClass, string[] classCode = null)
        {
            query = $@"
                Select CLASS 코드, CLASSNAME 명칭 
                FROM CM_CLASS 
                Where CorpId = '{WrGlobal.CorpID}' 
                AND DisplayYn = 'Y' 
                AND HIGHCLASS = '{highClass}' ";


            if (classCode != null)
            {
                query += " AND CLASS IN (";

                for (int idx = 0; idx < classCode.Length; idx++)
                {
                    if (idx != 0)
                    {
                        query += ", ";
                    }

                    query += $@"'{classCode[idx]}'";
                }
                query += $@")";
            }

            query += $"\nORDER BY CLASS ";

            return db.GetDataTable(query);
        }

        public DataTable GetCategory_HighClass_Like(string highClass)
        {
            query = $@"
                Select CLASS 코드, CLASSNAME 명칭 
                FROM CM_CLASS 
                Where CorpId = '{WrGlobal.CorpID}' 
                AND DisplayYn = 'Y' 
                AND HIGHCLASS = '{highClass}' 
                ORDER BY CLASS 
            ";

            return db.GetDataTable(query);
        }

        public DataView GetCategory(string pclass, bool isAll)
        {
            if (isAll)
            {
                query = string.Format("Select a.분류코드, a.분류명 From (select 1 Sorting,'ALL' 분류코드, '전체' 분류명 Union All select 2 Sorting, ClassItemCode 분류코드, Title 분류명 FROM CM_CateItem Where (CorpId = '{0}' OR CorpId = '{1}') and Class = '{2}') a Order By a.Sorting, a.분류코드", WrGlobal.PublicCorpID, WrGlobal.CorpID, pclass);
            }
            else
            {
                query = string.Format("select ClassItemCode 분류코드, Title 분류명 FROM CM_CateItem Where (CorpId = '{0}' OR CorpId = '{1}') and Class = '{2}' order by ClassItemCode", WrGlobal.PublicCorpID, WrGlobal.CorpID, pclass);
            }

            return db.GetDataView("분류코드", query);
        }

        public DataView GetCategory(string pclass, string[] type = null)
        {
            if (type == null)
            {
                query = string.Format("select distinct ClassItemCode 분류코드, Title 분류명 FROM CM_CateItem Where Class = '{0}' AND useYn = 'Y' AND (CorpId = '1000' or CorpId = 'Z000') order by ClassItemCode", pclass);
            }
            else
            {
                query = string.Format("select distinct ClassItemCode 분류코드, Title 분류명 FROM CM_CateItem Where Class = '{0}' AND ClassItemCode IN ('{1}') AND (CorpId = '1000' or CorpId = 'Z000') order by ItemClassItemCodeCode", pclass, string.Join("','", type));
            }

            return db.GetDataView("분류코드", query);
        }

        public DataTable GetItemType()
        {
            query = $@"SELECT ClassItemCode 분류코드, Title 분류명
                        FROM CM_CateItem
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND CLASS = 'T010'
                    ";

            return db.GetDataTable(query);
        }

        public DataTable GetCM_CateItem(string corpId, string highClass, string[] extraValue = null)
        {
            string ItemType = "";
            if (extraValue != null) ItemType = string.Join(",", extraValue[0].Split(',').Select(v => $"'{v.Trim()}'"));

            query = $@"
                        Select ClassItemCode 코드 , Title 명칭 
                        FROM CM_CateItem 
                        Where CorpId = '{corpId}'
                        AND CLASS = '{highClass}'
                        AND UseYN = 'Y'
                        ";

            if(extraValue != null)
            {
                query += $"\n AND ExtraValue IN ({ItemType})";
            }
           return db.GetDataTable(query);
        }

        public DataTable GetCM_CateItemALL(string corpId, string highClass, string[] extraValue = null, bool allYn = true)
        {
            string ItemType = "";
            if (extraValue != null) ItemType = string.Join(",", extraValue[0].Split(',').Select(v => $"'{v.Trim()}'"));

            query = "";

            if (allYn)
            {
                query += @"SELECT'ALL' AS 코드, '전체' AS 명칭
                            UNION ALL ";
            }

            query += $@"
                        Select ClassItemCode 코드 , Title 명칭 
                        FROM CM_CateItem 
                        Where CorpId = '{corpId}'
                        AND CLASS = '{highClass}'
                        AND UseYN = 'Y'
                        ";

            if (extraValue != null)
            {
                query += $"\n AND ExtraValue IN ({ItemType})";
            }
            return db.GetDataTable(query);
        }

        public DataRow GetCM_CateItemInfo_ByTitle(string corpId, string classCode, string title)
        {
            query = $@"
                    Select *
                    FROM CM_CateItem
                    WHERE CorpId = '{corpId}'
                    AND CLASS = '{classCode}'
                    AND Title = '{title}'
                    ";

            return db.GetDataRow(query);
        }

        public string GetCM_CateItemAGG(string corpId, string highClass)
		{
            query = $@"
                SELECT STRING_AGG(CAST(ClassItemCode AS NVARCHAR(30)), ',') AS ValuesList
                FROM CM_CateItem
                WHERE CorpId = '{corpId}'
                AND CLASS = '{highClass}'
                AND UseYn = 'Y'
                ";

            DataRow dr = db.GetDataRow(query);

            string agg = "";

            if(dr != null)
			{
                agg = dr["ValuesList"].ConvertString_Null_To_Empty();
			}

            return agg;
		}

        public string[] GetCM_CateItemCode(string corpId, string highClass)
        {
            query = $@"
                SELECT  ClassItemCode
                FROM CM_CateItem
                WHERE CorpId = '{corpId}'
                AND CLASS = '{highClass}'
                AND UseYn = 'Y'
                ";

            DataTable dt = db.GetDataTable(query);

            List<string> itemCodes = new List<string>();

            int i = 0;

			foreach (DataRow dr in dt.Rows)
			{
                itemCodes.Add(dr["ClassItemCode"].ConvertString_Null_To_Empty());
			}

            return itemCodes.ToArray();
        }

        public DataTable GetCM_CateItem(string corpId, string highClass, bool allYn)
        {
            query = "";

            if(allYn)
			{
                query = $@"
                            SELECT'ALL' 코드, '전체' 명칭
                            UNION ALL
                        ";
			}

            query += $@"
                        Select ClassItemCode 코드 , Title 명칭 
                        FROM CM_CateItem 
                        Where CorpId = '{corpId}'
                        AND CLASS = '{highClass}'
                        AND UseYn = 'Y'
                        ";
            return db.GetDataTable(query);
        }

        public void LookupCategory(ref SearchLookUpEdit search, string pclass, string[] type = null)
        {
            search.Properties.DataSource = GetCategory(pclass, type);
            search.Properties.DisplayMember = "분류명";
            search.Properties.ValueMember = "분류코드";
        }

        public void LookupCategory(ref RepositoryItemSearchLookUpEdit search, string pclass, string[] type = null)
        {
            search.DataSource = GetCategory(pclass, type);
            search.DisplayMember = "분류명";
            search.ValueMember = "분류코드";
        }

        public void LookupCategory(ref RepositoryItemLookUpEdit search, string pclass, string[] type = null)
        {
            search.DataSource = GetCategory(pclass, type);
            search.DisplayMember = "분류명";
            search.ValueMember = "분류코드";
        }
        public void LookupCategory(ref LookUpEdit search, string pclass)
        {
            search.Properties.DataSource = GetCategory(pclass);
            search.Properties.DisplayMember = "분류명";
            search.Properties.ValueMember = "분류코드";
        }

        public void LookupCategory(ref LookUpEdit search, string pclass, bool isAll)
        {
            search.Properties.DataSource = GetCategory(pclass, isAll);
            search.Properties.DisplayMember = "분류명";
            search.Properties.ValueMember = "분류코드";
        }

        public DataTable GetMaterialList(string[] type = null, string displayYn = "N")
        {
            // 품목구분 정리
            string ItemType = null;

            if (type != null) ItemType = "'" + string.Join("','", type) + "'";

            // 마지막 ,(쉼표) 제거

            query = $@"SELECT ItemCode 품목코드, ItemName 품명
                        FROM CM_Item
                        WHERE UseYN = 'Y'
                        ";
            
            if (displayYn == "N") query += $"\n AND ISNULL(DisplayYn, 'N') <> '{displayYn}'";


            if (ItemType != null) query += $"\n AND ItemType IN ({ItemType})";

            return db.GetDataTable(query);
        }

        public DataTable GetMaterialList(bool AllYn, string[] type)
        {
            // 품목구분 정리
            string ItemType = null;

            if (type != null) ItemType = "'" + string.Join("','", type) + "'";

            query = "";
            if (AllYn)
			{
                query += $@"SELECT 'ALL' 품목코드, '전체' 품명
                            UNION ALL ";
			}

           // 마지막 ,(쉼표) 제거

           query += $@"SELECT ItemCode 품목코드, ItemName 품명
                        FROM CM_Item
                        WHERE UseYN = 'Y'
                        ";

            if (ItemType != null) query += $"\n AND ItemType IN ({ItemType})";

            return db.GetDataTable(query);
        }


        public DataTable GetMaterialList_Class(string ItemClass)
        {
            query = $@"
                     SELECT ItemCode 품목코드, ItemName 품명
                     FROM CM_Item
                     WHERE ItemClass1 = '{ItemClass}'
                        OR ItemClass2 = '{ItemClass}'";

            return db.GetDataTable(query);
        }

        public DataTable GetFaultList()
        {
            query = $@"SELECT FaultCode 불량코드, Title 불량명
                        FROM CM_DefectCode
                        ";

            return db.GetDataTable(query);
        }

        public DataTable GetItemType(string[] type)
        {
            if (type == null)
            {
                query = $@"SELECT ClassItemCode 코드, Title 코드명 FROM CM_CateItem WHERE CorpId = '{WrGlobal.CorpID}' AND CLASS = 'A010' AND UseYn = 'Y'";
            }
            else
            {
                query = $@"SELECT ClassItemCode 코드, Title 코드명 FROM CM_CateItem WHERE CorpId = '{WrGlobal.CorpID}' AND CLASS = 'A010' AND UseYn = 'Y' AND ClassItemCode IN ('{string.Join("','", type)}') ";
            }

            return db.GetDataTable(query);
        }

        public DataRowView GetMaterialInfo(string ItemCode)
        {
            query = string.Format("Select * From CM_Item Where CorpId = '{0}' and ItemCode = '{1}'", WrGlobal.CorpID, ItemCode);
            return db.GetDataRecord(query);
        }

        public DataView GetMaterialList(string [] Type, bool isAll)
        {
            query = "";

            if (isAll)
            {
                 query += $@"
                        SELECT'ALL' 품목코드, '전체' 품명

                        Union All";
            }
            query += $@"
                SELECT ItemCode 품목코드, ItemName 품명
                From CM_Item 
                WHERE CorpId = '{WrGlobal.CorpID}'
                ";

            if (Type != null)
            {
                query += " AND ItemType IN (";

                for (int idx = 0; idx < Type.Length; idx++)
                {
                    if (idx != 0)
                    {
                        query += ", ";
                    }

                    query += $@"'{Type[idx]}'";
                }
            query += $@")";
            }

            return db.GetDataView("품명코드", query);
        }

        public DataView GetWarehouseList()
        {
            query = string.Format("Select HouseID 창고코드, Title 창고명, WareType 창고타입 From CM_Warehouse Where CorpId = '{0}' and UseYn = 'Y'", WrGlobal.CorpID);
            return db.GetDataView("창고", query);
        }

        public DataTable GetWarehouseList_Dt()
        {
            query = $@"Select HouseID 창고코드, Title 창고명 From CM_Warehouse Where CorpId = '{WrGlobal.CorpID}' and UseYn = 'Y' ORDER BY WareType, HouseID";
            return db.GetDataTable(query);
        }

        public DataTable GetWarehouseList_Dt(bool allYn, string[] wareType)
        {
            query = "";

			if (allYn)
			{
                query = $@"SELECT 'ALL' 창고코드, '전체' 창고명 UNION ALL 
                            ";
			}

            query += $@"Select HouseID 창고코드, Title 창고명 
                        From CM_Warehouse
                        Where CorpId = '{WrGlobal.CorpID}' 
                        and UseYn = 'Y'
                        ";

			//특정 구분만 보고 싶은 경우
			if (wareType != null && wareType.Length > 0)
			{
				query += " AND (";
				for (int idx = 0; idx < wareType.Length; idx++)
				{
					if (idx != 0)
					{
						query += " OR";
					}

					query += $@" WareType = '{wareType[idx]}'";
				}

				query += ")";

			}

			return db.GetDataTable(query);
		}

        public DataView GetMachineList()
        {
            query = string.Format("Select MachineCode 설비코드, MachineName 설비명 From EQ_Machine Where CorpId = '{0}' and UseYn = 'Y'", WrGlobal.CorpID);
            return db.GetDataView("설비", query);
        }

        public DataTable GetMachineList(bool allYn)
        {
            query = "";

            if (allYn)
            {
                query = $@"SELECT'ALL' 설비코드, '전체' 설비명
                            UNION ALL
                            ";
            }

            query += $@"Select MachineCode 설비코드, MachineName 설비명 From EQ_Machine Where CorpId = '{WrGlobal.CorpID}' and UseYn = 'Y'";
            return db.GetDataTable(query);
        }

        public DataTable GetCorpList(bool isAll)
        {
            if (isAll)
            {
                query = string.Format("select '전체' 코드, '전체' 명칭 Union All select CorpId 코드, CorpName 명칭 from SM_Corp Order by 코드");
            }
            else
            {
                query = string.Format("select CorpId 코드, CorpName 명칭 from SM_Corp Order by 코드");
            }
            return db.GetDataTable(query);
        }

        public DataTable GetAuthorGroupList(bool allYn)
        {
            query = "";

            if (allYn)
            {
                query = $@"SELECT'ALL' 코드, '전체' 명칭
                            UNION ALL
                            ";
            }

            query += $@"SELECT GroupCode 코드, GroupName 명칭 FROM SM_AuthorGroup";
            return db.GetDataTable(query);
        }

        public DataTable GetDeptList(bool allYn)
        {
            query = "";

            if (allYn)
            {
                query = $@"SELECT'ALL' 코드, '전체' 명칭
                            UNION ALL
                            ";
            }

            query += $@"Select DeptCode 코드, DeptName 명칭 From CM_Dept Where CorpId = '{WrGlobal.CorpID}' and UseYn = 'Y'";
            return db.GetDataTable(query);
        }


        public DataView GetUserList()
        {
            query = string.Format("Select EmpNo 사번, Name 사원명 From Employee Where CorpId = '{0}' and EmpType = '001'", WrGlobal.CorpID);
            return db.GetDataView("사원", query);
        }

        //공정
        public DataView GetProcessList(bool allYn = false)
        {
            if (allYn)
            {
                query = $@"SELECT'ALL' 공정코드, '전체' 공정명
                            UNION ALL
                            ";
            }

            query += $"Select ProcessCode 공정코드, ProcessName 공정명 From CM_Process Where CorpId = '{WrGlobal.CorpID}' and UseYn = 'Y'";
            return db.GetDataView("공정", query);
        }

        public DataTable GetProcess_List(bool Yn = false)
        {
            query = "";

            if (Yn)
            {
                query = $@"SELECT'ALL' 공정코드, '전체' 공정명
                            UNION ALL
                            ";
            }

            query += $@"Select ProcessCode 공정코드, ProcessName 공정명 From Cm_Process Where CorpId = '{WrGlobal.CorpID}' and UseYn = 'Y'";
            return db.GetDataTable(query);
        }

        public DataTable GetProcess_List(List<string> processCds, bool allYn)
        {
            query = "";

            if (allYn)
            {
                query = $@"SELECT'ALL' 공정코드, '전체' 공정명
                            UNION ALL
                            ";

            }

            query += $@"
                        Select ProcessCode 공정코드, ProcessName 공정명
                        From CM_Process 
                        Where CorpId = '{WrGlobal.CorpID}' 
                        and UseYn = 'Y'
                        ";

            if (processCds != null)
            {
                if (processCds.Count > 0)
                {
                    query += "\nAND ProcessCode IN (";

                    int idx = 0;

                    foreach (string item in processCds)
                    {
                        if (idx != 0)
                        {
                            query += ", ";
                        }

                        query += $@"'{item}'";
                        idx++;
                    }
                    query += ")";

                }
            }
            return db.GetDataTable(query);
        }

        public DataTable GetProcess_List_ProcessType(bool allYn, string[] processType)
        {
            query = "";

            if (allYn)
            {
                query = $@"SELECT 'ALL' 공정코드, '전체' 공정명
                            UNION ALL
                            ";
            }

            query += $@"
                        Select ProcessCode 공정코드, ProcessName 공정명
                        From CM_Process 
                        Where CorpId = '{WrGlobal.CorpID}' 
                        and UseYn = 'Y'
                        ";

            if (processType != null)
            {
                if (processType.Length > 0)
                {
                    query += "\nAND ProcessType IN (";

                    int idx = 0;

                    foreach (string item in processType)
                    {
                        if (idx != 0)
                        {
                            query += ", ";
                        }

                        query += $@"'{item}'";
                        idx++;
                    }
                    query += ")";

                }
            }

            return db.GetDataTable(query);
        }

        public DataTable SelectCM_Process_ByCode(bool allYn = false, string[] searchProcessCode = null)
        {
            query = "";

            //조회항목에서 전체를 추가하고 싶을경우
            if (allYn)
            {
                query += @"SELECT'ALL' AS 공정코드, '전체' AS 공정명
                        UNION ALL ";
            }

            query += $@" SELECT ProcessCode AS 공정코드, ProcessName AS 공정명
                           FROM CM_Process
                          WHERE CorpId = '{WrGlobal.CorpID}'
                            AND UseYn = 'Y'
                            ";

            //특정 Process만 보고 싶은 경우
            if (searchProcessCode != null &&
                searchProcessCode.Length > 0)
            {
                query += " AND (";

                for (int idx = 0; idx < searchProcessCode.Length; idx++)
                {
                    if (idx != 0)
                    {
                        query += " OR";
                    }

                    query += $@" ProcessCode = '{searchProcessCode[idx]}'";
                }

                query += ")";
            }

            return db.GetDataTable(query);
        }

        public DataTable GetPart_List(bool Yn = false)
        {
            query = "";

            if (Yn)
            {
                query = $@"SELECT'ALL' 부품코드, '전체' 부품명
                            UNION ALL
                            ";
            }

            query += $@"Select PartID 부품코드, PartName 부품명 From CodePart Where CorpId = '{WrGlobal.CorpID}'";
            return db.GetDataTable(query);
        }

        //라인
        public DataTable GetLine_List(string proc, bool Yn = false)
        {
            query = "";

            if (Yn)
            {
                query = $@"SELECT'ALL' 라인코드, '전체' 라인명
                            UNION ALL
                            ";
            }

            query += $@"Select LineCode 라인코드, LineName 라인명 From Cm_Line Where CorpId = '{WrGlobal.CorpID}' and UseYn = 'Y'";

            if (proc != "") query += $"\n AND ProcessCode = '{proc}'";
            return db.GetDataTable(query);
        }

        //매출업체 CustType 20,40
        public DataView GetCustomer(string corpId, bool isAll)
        {
            if (isAll)
            {
                query = string.Format("select a.코드, a.내용 from (select 1 Sorting,'ALL' 코드, '전체' 내용 Union All "
                                                            + "select 2 Sorting, CustCode 코드, CustName 내용 from CM_Cust Where CorpId = '{0}' and UseYn = 'Y' and CustType in ('20','40')) a Order By a.Sorting, a.코드", corpId);
            }
            else
            {
                query = string.Format("select CustCode 코드, CustName 내용 from CM_Cust Where CorpId = '{0}' and UseYn = 'Y' and CustType in ('20','40') Order by CustCode", corpId);
            }
            return db.GetDataView("거래처", query);
        }

        public DataTable GetCustomer_Lup(string corpId, bool isAll)
        {
            if (isAll)
            {
                query = string.Format("select a.코드, a.내용 from (select 1 Sorting,'ALL' 코드, '전체' 내용 Union All "
                                                            + "select 2 Sorting, CustCode 코드, CustName 내용 from CM_Cust Where CorpId = '{0}' and UseYn = 'Y' and CustType in ('20','40')) a Order By a.Sorting, a.코드", corpId);
            }
            else
            {
                query = string.Format("select CustCode 코드, CustName 내용 from CM_Cust Where CorpId = '{0}' and UseYn = 'Y' and CustType in ('20','40') Order by CustCode", corpId);
            }
            return db.GetDataTable(query);
        }

        //매입업체 CustType 10,40
        public DataTable GetCustomerI_Lup(string corpId, bool isAll, string useYn ="Y")
        {
            query = "";

            if (isAll)
            {
                query = @"
                        SELECT'ALL' AS 거래처코드, '전체' AS 거래처명 
                        UNION ALL 
                        ";
            }

            query += $@"
                    SELECT CustCode 거래처코드 , CustName 거래처명
                    FROM CM_Cust
                    WHERE CorpId = '{corpId}'
                    AND UseYn = '{useYn}'
                    AND CustType IN ('10', '40')
                    ";

            return db.GetDataTable(query);
        }

        public DataView GetCustomer(string corpId, string custType, bool isAll)
        {
            if (isAll)
            {
                query = string.Format("select a.코드, a.내용 from (select 1 Sorting,'ALL' 코드, '전체' 내용 Union All "
                                                            + "select 2 Sorting, CustCode 코드, CustName 내용 from CM_Cust Where CorpId = '{0}' and CustType = '{1}' and UseYn = 'Y') a Order By a.Sorting, a.코드", corpId, custType);
            }
            else
            {
                query = string.Format("select CustCode 코드, CustName 내용 from CM_Cust Where CorpId = '{0}' and CustType = '{1}' and UseYn = 'Y' Order by CustCode", corpId, custType);
            }
            return db.GetDataView("거래처", query);
        }

        public DataView GetUser()
        {
            query = string.Format("Select EmployeeId ID, EmployeeName 사용자명 From SM_User Where CorpId = '{0}' and UseYn = 'Y'", WrGlobal.CorpID);
            return db.GetDataView("사용자", query);
        }

        public DataTable GetDataTableUser(bool all, string[] deptId = null, string workerYn = "")
        {
            query = "";

            if (all)
            {   //조회항목에서 전체를 추가하고 싶을경우
                query += @"SELECT'ALL' AS 코드
                                , '전체' AS 명칭
                            UNION ALL ";
            }

            query = $@"
                Select cu.EmployeeId 코드, cu.EmployeeName 명칭 
                From SM_User cu
                Where cu.CorpId = '{WrGlobal.CorpID}' 
                and cu.UseYn = 'Y'
                and cu.EmployeeId <> 'Dev'
                ";

            //작업자여부
            if(workerYn != "")
			{
                query += $"\nAND cu.WorkerYn = '{workerYn}'";
			}

            //특정 부서만 보고 싶은 경우
            if (deptId != null &&
                deptId.Length > 0)
            {
                query += "\nAND cu.DeptId IN (";

                for (int idx = 0; idx < deptId.Length; idx++)
                {
                    if (idx != 0)
                    {
                        query += ", ";
                    }

                    query += $@"'{deptId[idx]}'";
                }

                query += ")";
            }

            return db.GetDataTable(query);
        }

        public DataTable GetWorker()
        {
            query = $@"
                Select EmployeeId ID, EmployeeName 사용자명 
                From SM_User 
                Where CorpId = '{WrGlobal.CorpID}' 
                and WorkerYn = 'Y'";

            return db.GetDataTable(query);
        }

        public DataTable GetSalesUser_Lup()
        {
            query = string.Format("SELECT'ALL' 코드, '전체' 내용 Union All SELECT EmployeeId 사번, EmployeeName 성명 FROM SM_User WHERE CorpID = '{0}' AND DeptId = '10015'", WrGlobal.CorpID);
            return db.GetDataTable(query);
        }

        public void SaveRoutingTable(string routingCode, DataTable saveTable)
        {
            query = string.Format("Delete From CM_Routing Where CorpId = '{0}' And RoutingCode = '{1}'", WrGlobal.CorpID, routingCode);
            db.ExecuteQuery(query);

            if (saveTable != null)
            {
                db.WriteBulkInsert(saveTable);

                query = string.Format("Update CM_Routing Set RoutingSeq = t.Seq "
                                + "From ( "
                                        + "select ROW_NUMBER() OVER(Order By TreeLevel desc, RowNum) Seq, * "
                                        + "From CM_Routing "
                                        + "Where CorpId = '{0}' and RoutingCode = '{1}' "
                                    + ") t "
                                + "Where CM_Routing.CorpId = t.CorpId and CmRouting.RoutingCode = t.RoutingCode and CmRouting.ChildID = t.ChildID"
                                , WrGlobal.CorpID, routingCode);
                db.ExecuteQuery(query);
            }

        }
        public DataView GetRoutingProduct(string routingCode)
        {
            query = string.Format("Select * From CM_Item Where CorpId = '{0}' and RoutingCode = '{1}' and UseYn = 'Y'", WrGlobal.CorpID, routingCode);
            return db.GetDataView("CM_Item", query);
        }

        public void SetProductRouting(string corpId, string ItemCode, string routingCode)
        {
            query = string.Format("Update CM_Item Set RoutingCode = '{2}' Where CorpId = '{0}' and ItemCode = '{1}'"
                                , corpId, ItemCode, routingCode);
            db.ExecuteQuery(query);
        }

        public DataTable GetRoutingCode(string corpId, string routingType)
        {
            query = $@"
                       SELECT  RoutingCode 코드, RoutingName 명칭 
                         FROM  CM_RoutingRev 
                        WHERE CorpId = '{corpId}'
                           AND UseYN = 'Y'
						   AND RoutingType = '10'
                        ";
            return db.GetDataTable(query);
        }


        // BOM 기본 사용리비전 품목 테이블 적용
        public bool SetProductBomRevision(string corpId, string ItemCode, string revisionNo, string newYn, string routingCode)
        {
            query = $"exec Code_SetProductBomRevision '{corpId}', '{ItemCode}', '{revisionNo}', '{WrGlobal.LoginID}', '{newYn}','{routingCode}'";
            return db.ExecuteQuery(query);
        }

        // BOM에 저장
        public void SaveBomTable(string ItemCode, string revision, DataTable saveTable)
        {
            query = string.Format("Delete From CM_BOM Where CorpId = '{0}' And PItemCode = '{1}' and Revision = '{2}'", WrGlobal.CorpID, ItemCode, revision);
            db.ExecuteQuery(query);

            if (saveTable != null)
            {
                db.WriteBulkInsert(saveTable);
            }
        }

        // 신규 라우팅번호 발번
        public string GetNewRoutingCode(string corpId)
        {
            query = $"Select 'RT' + Format(isnull(Cast(RIGHT(max(RoutingCode), 4) as int), 0) + 1, '0000') from CM_RoutingRev Where CorpId = '{corpId}'";
            DataRowView drv = db.GetDataRecord(query);

            if (drv != null)
            {
                return drv[0].ToString();
            }
            else
            {
                return "";
            }
        }


        public DataTable Select_Process_Lup()
        {
            query = $"select ProcessName 공정이름, ProcessCode 공정코드 from Cm_Process Where CorpId = '{WrGlobal.CorpID}'";
            return db.GetDataTable(query);
        }

		public DataTable Select_Machine_Lup(bool useYn, bool isAll = false)
		{
			query = "";

			if (isAll)
			{
				query = $@"SELECT'ALL' 설비코드, '전체' 설비명
                            UNION ALL
                            ";
			}

			query += $"select MachineCode 설비코드, MachineName 설비명 from EQ_Machine Where CorpId = '{WrGlobal.CorpID}'";

			if (useYn)
			{
				query += "AND UseYn = 'Y'";
			}

			return db.GetDataTable(query);
		}

        public DataTable Select_CM_CateItem_ByKey()
        {
            query = $"Select ClassItemCode 그룹코드, Title 그룹명 FROM CM_CateItem Where CorpID = '{WrGlobal.CorpID}' and Class = 'S210' AND ClassItemCode NOT IN ('30')";
            return db.GetDataTable(query);
        }

        public DataTable Select_Customer_ByKey()
        {
            query = $"Select CustCode 거래처코드, CustName 거래처명 From CM_Cust Where CorpID = '{WrGlobal.CorpID}'";
            return db.GetDataTable(query);
        }

        public DataView Select_ProcessByBom()
        {
            //query = string.Format("SELECT ProcessCode, ProcessName, WorkType FROM CM_Process Where CorpId = '{0}' AND WorkType = 10", WrGlobal.CorpID);
            query = string.Format("SELECT ProcessCode, ProcessName, WorkType FROM CM_Process Where CorpId = '{0}'", WrGlobal.CorpID);
            return db.GetDataView("공정", query);
        }

        // 원자재
        public void Get_Material(ref SearchLookUpEdit lup, string[] type = null)
        {
            query = $@"SELECT ItemCode 품목코드, ItemName 품명
                        FROM CM_Item
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        ";

            if (type != null) query += $"\n AND ItemType IN ('{string.Join("','", type)}')";

            lup.Properties.DataSource = db.GetDataTable(query);
            lup.Properties.ValueMember = "품목코드";
            lup.Properties.DisplayMember = "품명";
        }

        public DataTable Chk_ItemCode(string _ItemCode)
        {
            query = $@"SELECT ItemCode 
                        FROM CM_Item
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND ItemCode = '{_ItemCode}'
                    ";

            return db.GetDataTable(query);
        }

        public DataTable GetUnitCode()
        {
            query = $@"SELECT ClassItemCode 코드, Title 코드명
                        FROM CM_CateItem
                        WHERE CLASS = 'A011'
                        AND UseYn = 'Y'
                        ";

            return db.GetDataTable(query);
        }

        public DataTable GetClassList(string _class)
        {
            query = $@"SELECT ClassItemCode 분류코드, Title 분류명
                        FROM CM_CateItem
                        WHERE CorpID = '{WrGlobal.CorpID}'
                        AND CLASS = '{_class}'
                    ";

            return db.GetDataTable(query);
        }

        public DataRow GetClassInfo(string _class)
        {
            query = $@"SELECT *
                        FROM CM_CLASS
                        WHERE CorpID = '{WrGlobal.CorpID}'
                        AND CLASS = '{_class}'
                    ";

            return db.GetDataRow(query);
        }

        public DataTable GetMaterialUnitCode(string ItemCode)
        {
            query = $@"SELECT UnitCode
                        FROM CM_Item
                        WHERE CorpID = '{WrGlobal.CorpID}'
                        AND ItemCode = '{ItemCode}'
                        ";

            return db.GetDataTable(query);
        }

        public DataTable GetOperationCode()
        {
            query = $@"SELECT CL.CLASS 분류코드, CL.CLASSNAME 분류명, CCI.ClassItemCode 코드, CCI.Title 코드명
                        FROM CM_CLASS CL 
                        INNER JOIN CM_CateItem CCI ON CL.CLASS = CCI.CLASS
                        WHERE CL.CorpID = '1000'
                        AND CL.ROOTCLASS = '2030'
                        ";
            /*
            query = $@"SELECT ItemCode 코드, Title 코드명
                        FROM CM_CateItem
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND CLASS LIKE '2030%'
                    ";
            */
            return db.GetDataTable(query);
        }

        public DataTable SelectEQ_MachineLookup()
        {
            string sql = "SELECT'ALL' 코드, '전체' 설비명 UNION ALL ";
            sql += " SELECT MachineCode 코드, MachineName 설비명 " +
                         "   FROM EQ_Machine " +
                         "  WHERE CorpId = '" + $"{WrGlobal.CorpID}" + "'" +
                         "  AND UseYn = 'Y'";

            return db.GetDataTable(sql);
        }

        public DataTable Get_UserList(bool allYn = false, string[] dept = null)
        {
            query = "";

            if (allYn)
            {
                query = $@"SELECT'ALL' 코드, '전체' 코드명
                            UNION ALL
                        ";
            }

            query += $@"SELECT EmployeeId 코드, EmployeeName 코드명
                        FROM SM_User
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        ";

            if (dept != null) query += $"\n AND DeptId IN ('{string.Join("','", dept)}')";

            return db.GetDataTable(query);
        }

        public DataTable Get_CustomerDt(bool allYn = false, string[] type = null, string bussinessCategory = null)
        {
            query = "";

            if (allYn)
            {
                query = $@" SELECT 'ALL' 코드, '전체' 내용
                            UNION ALL
                        ";
            }

            query += $@"SELECT CustCode 코드, CustName 내용
                        FROM CM_Cust
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND UseYn = 'Y'
                        ";

			if (type != null && type.Length > 0)
			{
				query += "\nAND CustType IN (";

				for (int idx = 0; idx < type.Length; idx++)
				{
					if (idx != 0)
					{
						query += ", ";
					}

					query += $@"'{type[idx]}'";
				}

				query += ")";
			}


			if (bussinessCategory != null)
            {
                query += $"\nAND BussinessCategory = '{bussinessCategory}'";
            }

            return db.GetDataTable(query);
        }

        #region 금형목형

        public DataTable Select_Mold_Lup(bool useYn, bool isAll = false, bool dispose = true, string moldType = "")
        {
            query = "";

            if (isAll)
            {
                query = $@"SELECT'ALL' 목형코드, '전체' 목형명
                            UNION ALL
                            ";
            }

            query += $"select MoldCode 목형코드, MoldName 목형명 from CodeMold Where CorpId = '{WrGlobal.CorpID}'";

            if (useYn)
            {
                query += "AND UseYn = 'Y'";
            }

            if (!dispose)
            {
                query += "AND Status <> '폐기'";
            }

            if (moldType != "")
            {
                query += $"\n AND MoldType = '{moldType}'";
            }

            return db.GetDataTable(query);
        }

        public DataTable Select_MoldLocation_Lup(bool isAll = false)
        {
            query = "";

            if (isAll)
            {
                query = $@"SELECT'ALL' 위치코드, '전체' 위치명
                            UNION ALL
                            ";
            }

            query += $"select ClassItemCode 위치코드, Title 위치명 FROM CM_CateItem Where CorpId = '{WrGlobal.CorpID}' AND CLASS = 'A110'";

            return db.GetDataTable(query);
        }

        public DataTable Select_MoldIn_Wait_list(string moldType, string moldTypeName = null)
        {
            query = $@"
                        SELECT
                              CM.MoldCode {moldTypeName}코드
                            , CM.MoldName {moldTypeName}명
                        FROM CodeMold CM
                        LEFT JOIN MoldProcessZoneList MPZL ON CM.CorpId = MPZL.CorpId AND CM.MoldCode = MPZL.MoldCode
                        LEFT JOIN MoldHouseZoneList MHZL ON CM.CorpId = MHZL.CorpId AND CM.MoldCode = MHZL.MoldCode
                        WHERE CM.CorpId = '{WrGlobal.CorpID}'
                        AND MHZL.MoldCode IS NULL
                        AND CM.UseYn = 'Y'
                        AND CM.MoldType = '{moldType}'
                    ";

            return db.GetDataTable(query);
        }

        public DataTable Select_MoldOut_Wait_list(string moldType)
        {
            query = $@"
                        SELECT
                            CM.MoldCode 목형코드
                            , CM.MoldName 목형명
                        FROM CodeMold CM
                        INNER JOIN MoldHouseZoneList MHZL ON CM.CorpId = MHZL.CorpId AND CM.MoldCode = MHZL.MoldCode
                        WHERE CM.CorpId = '{WrGlobal.CorpID}'
                        AND CM.UseYn = 'Y'
                        AND CM.MoldType = '{moldType}'
                        ORDER BY CM.MoldCode
                    ";

            return db.GetDataTable(query);
        }

        public DataTable Select_MoldHouseZone()
        {
            query = $@"
                        SELECT
                            ZoneID 위치번호
                            , ZoneName 위치명
                        FROM CM_WarehouseZone
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND HouseID = 'S0004'   -- 목형창고
                    ";

            return db.GetDataTable(query);
        }
        public DataTable Select_MoldHouseZone(string moldType)
        {
            string houseID;
            switch (moldType)
            {
                case "10": houseID = "S0031"; break;
                case "20": houseID = "S0032"; break;
                case "30": houseID = "S0033"; break;
                default: houseID = "S0031"; break;
            }

            string query = $@"
                                SELECT ZoneID 위치번호
                                     , ZoneName 위치명 
                                FROM CM_WarehouseZone 
                                WHERE HouseID = '{houseID}'
                                ORDER BY 위치번호
                            ";

            return db.GetDataTable(query);
        }

        public DataTable Select_HouseZone(string HouseID = "")
        {
            query = $@"
                        SELECT
                            ZoneID 위치번호
                            , ZoneName 위치명
                        FROM CM_WarehouseZone
                        WHERE CorpId = '{WrGlobal.CorpID}'
                    ";

            query += $"\n AND HouseID = '{HouseID}'";

            return db.GetDataTable(query);
        }

        #endregion

        public string GetBorderNo()
        {
            query = $@"SELECT dbo.FN_Create_BoardNo('{WrGlobal.CorpID}')";

            return db.ExecuteScalarStringQuery(query);
        }

        public DataRow Get_DataRow_Notice(string BoarderNo)
        {
            query = $@"
                            SELECT CorpId
                                    , BoardNo
                                    , Class
                                    , WriteDate
                                    , Subject
                                    , WriteName
                                    , Body
                                    , DisplayType
                                    , StartDate
                                    , EndDate 
                                    , CreId
                                    , CreIP
                                    , CreDt
                                    , ModId
                                    , ModIP
                                    , ModDt
                            FROM SM_Notice
                            WHERE CorpId = '{WrGlobal.CorpID}'
                            AND BoardNo = '{BoarderNo}'
                            ";

            return db.GetDataRow(query);
        }
    }
}
