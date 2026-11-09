using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors;
using SmartPOP.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMT_MES.Controls;

namespace SmartPOP.Models
{
    public class clsCode
    {
        Database db = new Database();
        string query = "";

        string asda = "";

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

        public string CustCode { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialName { get; set; }
        public string MaterialType { get; set; }
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
        public int PurchasePrice { get; set; }
        public int SalesPrice { get; set; }

        #endregion

        public DataRow GetCorpLogoImg(string corpId)
        {
            query = $@"select LogoImg from SM_Corp WHERE CorpId = '{corpId}'";

            return db.GetDataRow(query);
        }

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
        public string[] smtComboBox(string DIV, string Type, string All = "")
        {
            List<string> datas = new List<string>();

            Database db = new Database();
            string query = $"exec SMT_ComboBoxGet '{DIV}','{Type}','{All}'";
            DataTable dt = db.GetDataTable(query);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    datas.Add(row["항목"].ToString());
                }
                return datas.ToArray();
            }
            return null;
        }

        public DataTable GetMachineList(bool allYn)
        {
            query = "";

            if (allYn)
            {
                query = $@"SELECT 'ALL' 설비코드, '전체' 설비명
                            UNION ALL
                            ";
            }

            query += $@"Select MachineCode 설비코드, MachineName 설비명 From EQ_Machine Where CorpId = '{WrGlobal.CorpID}' and UseYn = 'Y'";
            return db.GetDataTable(query);
        }

        public DataView GetMaterialList(string[] Type, bool isAll)
        {
            query = "";

            if (isAll)
            {
                query += $@"
                        SELECT 'All' 품번, '전체' 품명

                        Union All";
            }
            query += $@"
                SELECT ItemCode 품번, ItemName 품명
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

        public DataTable GetMaterialList_Lup(string[] Type, bool isAll)
        {
            query = "";

            if (isAll)
            {
                query += $@"
                        SELECT 'All' 품번, '전체' 품명

                        Union All";
            }
            query += $@"
                SELECT MaterialCode 품번, MaterialName 품명
                From CM_Item 
                WHERE CorpId = '{WrGlobal.CorpID}'
                ";

            if (Type != null)
            {
                query += " AND MaterialType IN (";

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

            return db.GetDataTable(query);
        }

        public DataTable GetCustomerList(string[] type = null, bool allYn = true)
        {
            query = "";

            if (allYn)
            {   //조회항목에서 전체를 추가하고 싶을경우
                query += @"SELECT 'ALL' AS 거래처코드, '전체' AS 거래처명
                            UNION ALL ";
            }

            query += $@"select CustCode 거래처코드, CustName 거래처명 from CM_Cust Where CorpId = '{WrGlobal.CorpID}'";

            if (type != null) query += $"\n AND CustType IN ('{string.Join("','", type)}') ";
            return db.GetDataTable(query);
        }

        public DataTable GetProcess_List_ProcessType(bool allYn, string[] processType, string[] workType = null)
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


        public bool ExecuteQry(string _query)
        {
            return db.ExecuteQuery(_query);
        }

        public DataView GetCorp()
        {
            query = string.Format("select CorpId 코드, CorpName 내용 from CmCorp Order by CorpId");
            return db.GetDataView("사업장", query);
        }

        public DataView GetCorp(bool isAll)
        {
            if (isAll)
            {
                query = string.Format("select '전체' 코드, '전체' 내용 Union All select CorpId 코드, CorpName 내용 from CmCorp Order by 코드");
            }
            else
            {
                query = string.Format("select CorpId 코드, CorpName 내용 from CmCorp Order by 코드");
            }
            return db.GetDataView("사업장", query);
        }

        public void LookupCorp(ref LookUpEdit search)
        {
            search.Properties.DataSource = GetCorp();
            search.Properties.DisplayMember = "내용";
            search.Properties.ValueMember = "코드";
        }

        public void LookupCorp(ref RepositoryItemLookUpEdit search)
        {
            search.DataSource = GetCorp();
            search.DisplayMember = "내용";
            search.ValueMember = "코드";
        }

        public DataView GetCustomer(string corpId)
        {
            query = string.Format("select CustCode 코드, CustName 내용 from CM_Cust Where CorpId = '{0}' and UseYn = 'Y' and CustType = '20' Order by CustCode", corpId);
            return db.GetDataView("거래처", query);
        }

        public DataTable GetCustomer(string [] type = null)
        {
            query = $@"select CustCode 거래처코드, CustName 거래처명 from CM_Cust Where CorpId = '{WrGlobal.CorpID}'";

            if (type != null) query += $"\n AND CustType IN ('{string.Join("','", type)}') ";
            return db.GetDataTable(query);
        }

        public DataView GetCustomer(string corpId, string custType)
        {
            query = string.Format("select CustCode 코드, CustName 내용 from CM_Cust Where CorpId = '{0}' and CustType = '{1}' and UseYn = 'Y' Order by CustCode", corpId, custType);
            return db.GetDataView("거래처", query);
        }

        //매입업체 CustType 10,40
        public DataTable GetCustomerI_Lup(string corpId, bool isAll, string useYn = "Y")
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

        public void LookupCustomer(string corpId, ref RepositoryItemSearchLookUpEdit search)
        {
            search.DataSource = GetCustomer(corpId);
            search.DisplayMember = "내용";
            search.ValueMember = "코드";
        }

        public void LookupCustomer(string corpId, ref SearchLookUpEdit search)
        {
            search.Properties.DataSource = GetCustomer(corpId);
            search.Properties.DisplayMember = "내용";
            search.Properties.ValueMember = "코드";
        }

        public void LookupCustomer(string corpId, string custType, ref SearchLookUpEdit search)
        {
            search.Properties.DataSource = GetCustomer(corpId, custType);
            search.Properties.DisplayMember = "내용";
            search.Properties.ValueMember = "코드";
        }

        public void LookupWareHouse(string corpId, ref LookUpEdit search)
        {
            search.Properties.DataSource = GetWarehouseList();
            search.Properties.DisplayMember = "창고명";
            search.Properties.ValueMember = "창고코드";
        }

        public void LookupWareHouse(string corpId, ref RepositoryItemLookUpEdit search)
        {
            search.DataSource = GetWarehouseList();
            search.DisplayMember = "창고명";
            search.ValueMember = "창고코드";
        }

        public string GetAutoNumber(string docType, DateTime currentDate)
        {
            return db.ExecAutoNumber(docType, currentDate);
        }

        public DataView GetCategory()
        {
            query = string.Format("Select * From CLASS Where CorpId = '{0}'",WrGlobal.CorpID);
            return db.GetDataView("분류코드", query);
        }
        public DataView GetCategoryRest()
        {
            query = string.Format("Select * From CLASS Where (CorpId = '{0}' OR CorpId = '{1}') and Class = 'A010' ", WrGlobal.PublicCorpID, WrGlobal.CorpID);
            return db.GetDataView("분류코드", query);
        }

        public DataView GetCategoryDefective()
        {
            query = string.Format("Select * From CLASS Where (CorpId = '{0}' OR CorpId = '{1}') and CLASSLEVEL = 2 and HIGHCLASS = 'QC10'", WrGlobal.PublicCorpID, WrGlobal.CorpID);
            return db.GetDataView("분류코드", query);
        }
        public DataView GetCategoryOperation()
        {
            query = string.Format("Select * From CLASS Where (CorpId = '{0}' OR CorpId = '{1}') and CLASSLEVEL = 2 and HIGHCLASS = '2030'", WrGlobal.PublicCorpID, WrGlobal.CorpID);
            return db.GetDataView("분류코드", query);
        }

        public DataView GetClass(string highClass)
        {
            query = string.Format("Select * From CM_CLASS Where (CorpId = '{0}' OR CorpId = '{1}') and HIGHCLASS = '{2}'", WrGlobal.PublicCorpID, WrGlobal.CorpID, highClass);
            return db.GetDataView("분류코드", query);
        }


        public DataView GetRootClass(string rootClass)
        {
            query = string.Format("Select * From CLASS Where (CorpId = '{0}' OR CorpId = '{1}') and ROOTCLASS = '{2}'", WrGlobal.PublicCorpID, WrGlobal.CorpID, rootClass);
            return db.GetDataView("분류코드", query);
        }

        public DataView GetFaultClass()
        {
            query = string.Format("Select * From CLASS Where (CorpId = '{0}' OR CorpId = '{1}') and HIGHCLASS like 'H0%'", WrGlobal.PublicCorpID, WrGlobal.CorpID);
            return db.GetDataView("분류코드", query);
        }

        public DataView GetCategory(string pclass, bool isAll)
        {
            if (isAll)
            {
                query = string.Format("Select a.분류코드, a.분류명 From (select 1 Sorting, 'All' 분류코드, '전체' 분류명 Union All select 2 Sorting, ItemCode 분류코드, Title 분류명 from CM_CateItem Where (CorpId = '{0}' OR CorpId = '{1}') and Class = '{2}') a Order By a.Sorting, a.분류코드", WrGlobal.PublicCorpID, WrGlobal.CorpID, pclass);
            }
            else
            {
                query = string.Format("select ItemCode 분류코드, Title 분류명 from CM_CateItem Where (CorpId = '{0}' OR CorpId = '{1}') and Class = '{2}' order by ItemCode", WrGlobal.PublicCorpID, WrGlobal.CorpID, pclass);
            }

            return db.GetDataView("분류코드", query);
        }

        public DataView GetCategory(string pclass, string[] type = null)
        {
            if (type == null)
            {
                query = string.Format("select distinct ClassItemCode 분류코드, Title 분류명 from CM_CateItem Where Class = '{0}' AND useYn = 'Y' AND (CorpId = '1000' or CorpId = 'Z000') order by ClassItemCode", pclass);
            }
            else
            {
                query = string.Format("select distinct ClassItemCode 분류코드, Title 분류명 from CM_CateItem Where Class = '{0}' AND ClassItemCode IN ('{1}') AND (CorpId = '1000' or CorpId = 'Z000') order by ClassItemCode", pclass, string.Join("','", type));
            }

            return db.GetDataView("분류코드", query);
        }

        public DataTable GetCateItem(string corpId, string highClass, string[] extraValue = null)
        {
            string materialType = "";
            if (extraValue != null) materialType = string.Join(",", extraValue[0].Split(',').Select(v => $"'{v.Trim()}'"));

            query = $@"
                        Select ClassItemCode 코드 , Title 명칭 
                        From CM_CateItem 
                        Where CorpId = '{corpId}'
                        AND CLASS = '{highClass}'
                        AND UseYN = 'Y'
                        ";

            if (extraValue != null)
            {
                query += $"\n AND ExtraValue IN ({materialType})";
            }
            return db.GetDataTable(query);
        }

        public DataTable GetMaterialType()
        {
            query = $@"SELECT ItemCode 분류코드, Title 분류명
                        FROM CM_CateItem
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND CLASS = 'T010'
                    ";

            return db.GetDataTable(query);
        }

        public DataView GetCategory(string pclass, string extraValue)
        {
            query = string.Format("select ItemCode 분류코드, Title 분류명 from CM_CateItem Where (CorpId = '{0}' OR CorpId = '{1}') and Class = '{2}' and ExtraValue = '{3}' order by ItemCode", WrGlobal.PublicCorpID, WrGlobal.CorpID, pclass, extraValue);
            return db.GetDataView("분류코드", query);
        }

        public void LookupCategory(ref SearchLookUpEdit search, string pclass, string[] type = null)
        {
            search.Properties.DataSource = GetCategory(pclass, type);
            search.Properties.DisplayMember = "분류명";
            search.Properties.ValueMember = "분류코드";
        }

        public void LookUpSize(ref SearchLookUpEdit search)
        {
            search.Properties.DataSource = GetMaterialType();
            search.Properties.DisplayMember = "분류명";
            search.Properties.ValueMember = "분류코드";
        }

        public void LookupCategory(ref RepositoryItemSearchLookUpEdit search, string pclass)
        {
            search.DataSource = GetCategory(pclass);
            search.DisplayMember = "분류명";
            search.ValueMember = "분류코드";
        }

        public void SearchLookupQuery(ref RepositoryItemSearchLookUpEdit search, string query)
        {
            search.DataSource = db.GetDataView("리스트", query);
            search.DisplayMember = "Title";
            search.ValueMember = "Code";
        }

        public void LookupCategory(ref RepositoryItemLookUpEdit search, string pclass)
        {
            search.DataSource = GetCategory(pclass);
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

        public void LookupCategory(ref LookUpEdit search, string pclass, string extraValue)
        {
            search.Properties.DataSource = GetCategory(pclass, extraValue);
            search.Properties.DisplayMember = "분류명";
            search.Properties.ValueMember = "분류코드";
        }

        public DataTable GetMaterialList(string[] type = null)
        {
            // 품목구분 정리
            string materialType = null;

            if (type != null) materialType = "'" + string.Join("','", type) + "'";

            // 마지막 ,(쉼표) 제거

            query = $@"SELECT ItemCode 품목코드, ItemName 품명
                        FROM CM_Item
                        WHERE UseYN = 'Y'
                        ";

            if (materialType != null) query += $"\n AND ItemType IN ({materialType})";

            return db.GetDataTable(query);
        }

        public DataTable GetMaterialType(string[] type = null)
        {
            if (type == null)
            {
                query = $@"SELECT ItemCode 코드, Title 코드명 FROM CM_CateItem WHERE CLASS = 'A010' AND CorpId = 'Z000' ";
            }
            else
            {
                query = $@"SELECT ItemCode 코드, Title 코드명 FROM CM_CateItem WHERE CLASS = 'A010' AND CorpId = 'Z000' AND ItemCode IN ('{string.Join("','", type)}') ";
            }

            return db.GetDataTable(query);
        }
        public DataRowView GetMaterialInfo(string materialCode)
        {
            query = string.Format("Select * From CM_Item Where CorpId = '{0}' and ItemCode = '{1}'", WrGlobal.CorpID, materialCode);
            return db.GetDataRecord(query);
        }

        public DataView GetMaterialList(bool isAll)
        {
            if (isAll)
            {
                query = string.Format("select 'All' 품번, '전체' 품명  Union All select MaterialCode 품번, MaterialName 품명 From CM_Item Where CorpId = '{0}' and materialType = 20", WrGlobal.CorpID);
            }
            else
            {
                query = string.Format("select MaterialCode 품번, MaterialName 품명 From CM_Item Where CorpId = '{0}' and  materialType = 20", WrGlobal.CorpID);
            }
            return db.GetDataView("품명코드", query);
        }

        //구매 자재 리스트
        public DataView GetPurchaseMaterialList(bool isAll)
        {
            if (isAll)
            {
                query = string.Format("select 'All' 품번, '품명' 내용 Union All select MaterialCode 품번, MaterialName 품명 From CM_Item Where CorpId = '{0}' and MaterialType in('10', '40', '50', '60') and UseYn = 'Y'", WrGlobal.CorpID);

            }
            else
            {
                query = string.Format("select MaterialCode 품번, MaterialName 품명 From CM_Item Where CorpId = '{0}' and MaterialType in('20') and UseYn = 'Y'", WrGlobal.CorpID);
            }
            return db.GetDataView("품명코드", query);
        }

        public int GetNextFileSeq(string tableName, string filter)
        {
            query = string.Format("Select isnull(max(FileSeq), 0) + 1 seq From {1} Where CorpId = '{0}' and {2}", WrGlobal.CorpID, tableName, filter);
            DataRowView drv = db.GetDataRecord(query);

            return (int)drv[0];
        }

        public void LookupClass(ref RepositoryItemLookUpEdit search, string highClass)
        {
            search.DataSource = GetClassLookUp(highClass);
            search.DisplayMember = "분류명";
            search.ValueMember = "분류코드";
        }

        public void LookupClass(ref RepositoryItemSearchLookUpEdit search, string highClass)
        {
            search.DataSource = GetClassLookUp(highClass);
            search.DisplayMember = "분류명";
            search.ValueMember = "분류코드";
        }

        public DataView GetClassLookUp(string highClass)
        {
            query = string.Format("Select CLASS 분류코드, CLASSNAME 분류명 From CLASS Where (CorpId = '{0}' OR CorpId = '{1}') AND HIGHCLASS = '{2}'", WrGlobal.PublicCorpID, WrGlobal.CorpID, highClass);
            return db.GetDataView("분류코드", query);
        }


        public void DeleteCategory(string classCode)
        {
            query = string.Format("Delete Class Where (CorpID = '{0}' OR CorpId = '{1}') and Class like '{2}%'", WrGlobal.PublicCorpID, WrGlobal.CorpID, classCode);
            try
            {
                db.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                cls_MessageBox.Show(ex.Message);
            }
        }

        public DataView GetWarehouseList()
        {
            query = string.Format("Select HouseID 창고코드, Title 창고명 From CM_Warehouse Where CorpId = '{0}' and UseYn = 'Y'", WrGlobal.CorpID);
            return db.GetDataView("창고", query);
        }

        public DataTable GetWarehouseList_Dt()
        {
            query = $@"Select HouseID 창고코드, Title 창고명 From CM_Warehouse Where CorpId = '{WrGlobal.CorpID}' and UseYn = 'Y' ORDER BY WareType, HouseID";
            return db.GetDataTable(query);
        }


        public DataView GetMachineList()
        {
            query = string.Format("Select MachineCode 설비코드, MachineName 설비명 From EQ_Machine Where CorpId = '{0}' and UseYn = 'Y'", WrGlobal.CorpID);
            return db.GetDataView("설비", query);
        }

        public DataView GetUserList()
        {
            query = string.Format("Select EmpNo 사번, Name 사원명 From Employee Where CorpId = '{0}' and EmpType = '001'", WrGlobal.CorpID);
            return db.GetDataView("사원", query);
        }

        //공정
        public DataView GetProcessList()
        {
            query = string.Format("Select ProcessCode 공정코드, ProcessName 공정명 From CM_Process Where CorpId = '{0}' and UseYn = 'Y'", WrGlobal.CorpID);
            return db.GetDataView("공정", query);
        }
        public DataTable GetProcessListDt(bool Yn = false)
        {
            query = "";

            if (Yn)
            {
                query = $@"SELECT 'ALL' 코드, '전체' 명칭
                            UNION ALL
                            ";
            }


            query += $@"
                        Select ProcessCode 코드, ProcessName 명칭 
                        From CM_Process 
                        Where CorpId = '{WrGlobal.CorpID}' and UseYn = 'Y'
            ";

            return db.GetDataTable( query);
        }

        public DataTable GetProcess_List(bool Yn = false, string[] processType = null)
        {
            query = "";

            if (Yn)
            {
                query = $@"SELECT 'ALL' 공정코드, '전체' 공정명
                            UNION ALL
                            ";
            }

            query += $@"Select ProcessCode 공정코드, ProcessName 공정명 From CM_Process Where CorpId = '{WrGlobal.CorpID}' and UseYn = 'Y'";


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


        public DataTable GetLineLookUpList()
        {
            query = $"Select LineCode 코드, LineName 명칭, ProcessCode From CM_Line Where CorpId = '{WrGlobal.CorpID}' and UseYn = 'Y' Order By LineCode";
            return db.GetDataTable( query);
        }

        public DataTable GetLine_List(string proc, bool Yn = false)
        {
            query = "";

            if (Yn)
            {
                query = $@"SELECT 'ALL' 라인코드, '전체' 라인명
                            UNION ALL
                            ";
            }

            query += $@"Select LineCode 라인코드, LineName 라인명 From CM_Line Where CorpId = '{WrGlobal.CorpID}' and UseYn = 'Y'";

            if (proc != "") query += $"\n AND ProcessCode = '{proc}'";
            return db.GetDataTable(query);
        }

        public DataView GetMenu(string useYn, string popupYn)
        {
            if (useYn == "")
            {
                if (popupYn == "")
                {
                    query = string.Format("Select * From MenuGroup Where CorpId = '{0}'", WrGlobal.PublicCorpID);
                }
                else
                {
                    query = string.Format("Select * From MenuGroup Where CorpId = '{0}' and PopupYn = '{1}'", WrGlobal.PublicCorpID, popupYn);
                }

            }
            else
            {
                if (popupYn == "")
                {
                    query = string.Format("Select * From MenuGroup Where CorpId = '{0}' and UseYn = '{1}'", WrGlobal.PublicCorpID, useYn);
                }
                else
                {
                    query = string.Format("Select * From MenuGroup Where CorpId = '{0}' and UseYn = '{1}' and PopupYn = '{2}'", WrGlobal.PublicCorpID, useYn, popupYn);
                }

            }
            return db.GetDataView("메뉴", query);
        }

        //매출업체 CustType 20
        public DataView GetCustomer(string corpId, bool isAll)
        {
            if (isAll)
            {
                query = string.Format("select a.코드, a.내용 from (select 1 Sorting, 'All' 코드, '전체' 내용 Union All "
                                                            + "select 2 Sorting, CustCode 코드, CustName 내용 from CM_Cust Where CorpId = '{0}' and UseYn = 'Y' and CustType = '20') a Order By a.Sorting, a.코드", corpId);
            }
            else
            {
                query = string.Format("select CustCode 코드, CustName 내용 from CM_Cust Where CorpId = '{0}' and UseYn = 'Y' and CustType = '20' Order by CustCode", corpId);
            }
            return db.GetDataView("거래처", query);
        }

        public DataView GetCustomer(string corpId, string custType, bool isAll)
        {
            if (isAll)
            {
                query = string.Format("select a.코드, a.내용 from (select 1 Sorting, 'All' 코드, '전체' 내용 Union All "
                                                            + "select 2 Sorting, CustCode 코드, CustName 내용 from CM_Cust Where CorpId = '{0}' and CustType = '{1}' and UseYn = 'Y') a Order By a.Sorting, a.코드", corpId, custType);
            }
            else
            {
                query = string.Format("select CustCode 코드, CustName 내용 from CM_Cust Where CorpId = '{0}' and CustType = '{1}' and UseYn = 'Y' Order by CustCode", corpId, custType);
            }
            return db.GetDataView("거래처", query);
        }

        public DataView GetWarehouseList(string corpId, bool isAll)
        {
            if (isAll)
            {
                query = string.Format("select '전체' 코드, '전체' 내용 Union All Select HouseID 코드, Title 내용 From CM_Warehouse Where CorpId = '{0}' and UseYn = 'Y'", corpId);

            }
            else
            {
                query = string.Format("Select HouseID 코드, Title 내용 From CM_Warehouse Where CorpId = '{0}' and UseYn = 'Y'", corpId);
            }

            return db.GetDataView("창고", query);
        }

        public DataView GetWarehouseList(string corpId, string wareType, bool isAll)
        {
            if (isAll)
            {
                query = string.Format("select 'All' 코드, '전체' 내용 Union All Select HouseID 코드, Title 내용 From CM_Warehouse Where CorpId = '{0}' and WareType in ({1}) and UseYn = 'Y'", corpId, wareType);

            }
            else
            {
                query = string.Format("Select HouseID 코드, Title 내용 From CM_Warehouse Where CorpId = '{0}' and WareType in ({1}) and UseYn = 'Y'", corpId, wareType);
            }

            return db.GetDataView("창고", query);
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
                query += @"SELECT 'ALL' AS 코드
                                , '전체' AS 명칭
                            UNION ALL ";
            }

            query = $@"
                Select cu.EmployeeId 코드, cu.EmployeeName 명칭 
                From SM_User cu
                Where cu.CorpId = '{WrGlobal.CorpID}' 
                and cu.UseYn = 'Y'";

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

        public DataTable GetSM_UserList()
        {
            query = string.Format("Select EmployeeId ID, EmployeeName 사용자명 From SM_User Where CorpId = '{0}' and UseYn = 'Y'", WrGlobal.CorpID);

            return db.GetDataTable(query);
        }

        public DataView GetPIMDMachine(bool isAll)
        {
            if (isAll)
            {
                query = string.Format("select '전체' 장비코드, '전체' 장비명 Union All select InsMcCode 장비코드, InsMcName 장비명 from InsMcHeader Order by InsMcCode");
            }
            else
            {
                query = string.Format("select InsMcCode 장비코드, InsMcName 장비명 from InsMcHeader Order by 코드");
            }
            return db.GetDataView("PIMC", query);
        }

        public DataView GetUser(string corpId, bool isAll)
        {
            if (isAll)
            {
                query = string.Format("select '전체' 사번, '전체' 사원명 Union All Select EmployeeId 사번, EmployeeName 사원명 From SM_User Where CorpId = '{0}'", corpId);

            }
            else
            {
                query = string.Format("Select EmployeeId 사번, EmployeeName 사원명 From SM_User Where CorpId = '{0}'", corpId);
            }

            return db.GetDataView("사용자", query);
        }

        // 라우팅 마스터 삭제시 하위 테이블 항목 삭제
        public bool DeleteRoutingByMaster(string routingCode)
        {
            query = $"Delete From CmRouting Where CorpId = '{WrGlobal.CorpID}' and RoutingCode = '{routingCode}'";
            return db.ExecuteQuery(query);
        }

        public void SaveRoutingTable(string routingCode, DataTable saveTable)
        {
            query = string.Format("Delete From CmRouting Where CorpId = '{0}' And RoutingCode = '{1}'", WrGlobal.CorpID, routingCode);
            db.ExecuteQuery(query);

            if (saveTable != null)
            {
                db.WriteBulkInsert(saveTable);

                query = string.Format("Update CmRouting Set RoutingSeq = t.Seq "
                                + "From ( "
                                        + "select ROW_NUMBER() OVER(Order By TreeLevel desc, RowNum) Seq, * "
                                        + "From CmRouting "
                                        + "Where CorpId = '{0}' and RoutingCode = '{1}' "
                                    + ") t "
                                + "Where CmRouting.CorpId = t.CorpId and CmRouting.RoutingCode = t.RoutingCode and CmRouting.ChildID = t.ChildID"
                                , WrGlobal.CorpID, routingCode);
                db.ExecuteQuery(query);
            }

        }
        public DataView GetRoutingProduct(string routingCode)
        {
            query = string.Format("Select * From CM_Item Where CorpId = '{0}' and RoutingCode = '{1}' and UseYn = 'Y'", WrGlobal.CorpID, routingCode);
            return db.GetDataView("CM_Item", query);
        }

        public void SetProductRouting(string corpId, string materialCode, string routingCode)
        {
            query = string.Format("Update CM_Item Set RoutingCode = '{2}' Where CorpId = '{0}' and MaterialCode = '{1}'"
                                , corpId, materialCode, routingCode);
            db.ExecuteQuery(query);
        }

        // BOM 기본 사용리비전 품목 테이블 적용
        public bool SetProductBomRevision(string corpId, string productCode, string revisionNo, string newYn, string routingCode)
        {
            query = $"exec Code_SetProductBomRevision '{corpId}', '{productCode}', '{revisionNo}', '{WrGlobal.LoginID}', '{newYn}','{routingCode}'";
            return db.ExecuteQuery(query);
        }

        public DataView GetBomMaterial(string corpId)
        {
            query = $"exec Code_GetBomMaterial '{corpId}'";
            return db.GetDataView("CM_Item", query);
        }

        // BOM에 저장
        public void SaveBomTable(string productCode, string revision, DataTable saveTable)
        {
            query = string.Format("Delete From CmBom Where CorpId = '{0}' And ProductCode = '{1}' and Revision = '{2}'", WrGlobal.CorpID, productCode, revision);
            db.ExecuteQuery(query);

            if (saveTable != null)
            {
                db.WriteBulkInsert(saveTable);
            }
        }

        public bool SaveBomRevision(string producdtCode, string revision, string revisionName, string routingCode, string startDate, string endDate, string remark, string useYn, string userId)
        {
            query = $"Code_MakeBomRevision '{WrGlobal.CorpID}', '{producdtCode}', '{revision}', '{revisionName}', '{routingCode}', '{startDate}', '{endDate}', '{remark}','{useYn}', '{userId}'";
            return db.ExecuteQuery(query);
        }

        // BOM 리비전(CmBomMater) 삭제
        public bool DeleteBomByRevision(string corpId, string productCode, string revisionNo)
        {
            query = $"exec Code_DeleteBomRevision '{corpId}', '{productCode}', '{revisionNo}'";
            return db.ExecuteQuery(query);
        }

        // 신규 라우팅번호 발번
        public string GetNewRoutingCode(string corpId)
        {
            query = $"Select 'RT' + Format(isnull(Cast(RIGHT(max(RoutingCode), 4) as int), 0) + 1, '0000') from CmRoutingMaster Where CorpId = '{corpId}'";
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

        // 원자재
        public void Get_Material(ref SearchLookUpEdit lup, string[] type = null)
        {
            query = $@"SELECT ItemCode 품목코드, ItemName 품명
                        FROM CM_Item
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND UseYn = 'Y'
                        ";

            if (type != null) query += $"\n AND ItemType IN ('{string.Join("','", type)}')";

            lup.Properties.DataSource = db.GetDataTable(query);
            lup.Properties.ValueMember = "품목코드";
            lup.Properties.DisplayMember = "품명";
        }

        public DataTable Get_SizeType()
        {
            query = $@"SELECT ItemCode 코드, Title 코드명 
                        FROM CM_CateItem
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND CLASS = 'T010'
                        ";

            return db.GetDataTable(query);
        }

        public DataTable Chk_MaterialCode(string _materialCode)
        {
            query = $@"SELECT MaterialCode 
                        FROM CM_Item
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND MaterialCode = '{_materialCode}'
                    ";

            return db.GetDataTable(query);
        }

        public DataTable GetUnitCode()
        {
            query = $@"SELECT ItemCode 코드, Title 코드명
                        FROM CM_CateItem
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND CLASS = '401040'
                        AND UseYn = 'Y'
                        ";

            return db.GetDataTable(query);
        }

        public DataTable GetWorkType()
        {
            query = $@"SELECT ItemCode 분류코드, Title 분류명
                        FROM CM_CateItem
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND CLASS = 'G010'
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

        public DataTable GetMaterialUnitCode(string materialCode)
        {
            query = $@"SELECT UnitCode
                        FROM CM_Item
                        WHERE CorpID = '{WrGlobal.CorpID}'
                        AND MaterialCode = '{materialCode}'
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

        public DataTable GetDefectiveCode(string proc)
        {
            query = $@"SELECT FaultCode, Title
                        FROM CM_DefectCode
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND ProcessID = '{proc}'
                        AND UseYn = 'Y'
                        ";

            return db.GetDataTable(query);
        }

        public DataTable GetInspType()
        {
            query = $@"SELECT ItemCode 코드, Title 코드명
                        FROM CM_CateItem
                        WHERE CorpID = '{WrGlobal.CorpID}'
                        AND CLASS = 'Q010'
                        ";

            return db.GetDataTable(query);
        }

        public DataTable Get_MaterialType(string[] type = null, bool AllYn = false)
        {
            query = "";

            if (AllYn)
            {
                query += $@"SELECT 'ALL' 분류코드, '전체' 분류명
                            UNION ALL
                        ";
            }

            query += $@"SELECT ItemCode 분류코드, Title 분류명
                        FROM CM_CateItem
                        WHERE CorpId = '{WrGlobal.CorpID}' --OR CorpId = 'Z000'
                        AND CLASS = 'A010'
                ";

            if (type != null) query += $"\n AND ItemCode IN ('{string.Join("','", type)}')";

            return db.GetDataTable(query);
        }
    }
}
