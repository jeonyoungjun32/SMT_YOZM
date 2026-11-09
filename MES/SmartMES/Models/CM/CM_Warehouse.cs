using SmartMES.Common;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using SmartCommon.Common;

namespace SmartMES.Models.CM
{
    class CM_Warehouse
    {
        #region Property

        public string CorpId { get; set; }

        public string HouseID { get; set; }

        public string Title { get; set; }

        public string WareType { get; set; }

        public string WareKind { get; set; }

        public string WareState { get; set; }

        public string ProcessCode { get; set; }

        public string DeptCode { get; set; }

        public string Remark { get; set; }

        public string CloseDate { get; set; }

        public string Tel { get; set; }

        public string Fax { get; set; }

        public string Address { get; set; }

        public string UseYn { get; set; }

        public string ZoneYn { get; set; }

        public string CreId { get; set; }

        public string CreIP { get; set; }

        public DateTime? CreDt { get; set; }

        public string ModId { get; set; }

        public string ModIP { get; set; }

        public DateTime? ModDt { get; set; }

        #endregion

        Database db = new Database();
        string query = "";

        public DataTable Select_CM_Warehouse()
        {
            UseYn = UseYn.ConvertString_Null_To_Empty();

            query = $@"
                SELECT '' Status
                             , CorpId
                             , HouseID
                             , Title
                             , WareType
                             , WareKind
                             , WareState
                             , ProcessCode
                             , DeptCode
                             , Remark
                             , CloseDate
                             , Tel
                             , Fax
                             , Address
                             , UseYn
                             , ZoneYn
                             , CreId
                             , CreIP
                             , CreDt
                             , ModId
                             , ModIP
                             , ModDt
                        FROM CM_Warehouse
                        WHERE CorpId = '{WrGlobal.CorpID}'
                ";

            if(UseYn != "" && UseYn.ToUpper() != "ALL")
			{
                query += $"\nAND UseYn = '{UseYn}'";
            }

            return db.GetDataTable(query);
        }

        public DataTable Select_WareHouse_Lookup(string type)
        {
            query = $@"SELECT HouseID AS 분류코드, Title as 분류명 FROM CM_Warehouse WHERE CorpId = '{WrGlobal.CorpID}' AND UseYn = 'Y' ";

            if (type == "제품") query += $@"AND WareType = '20'";
            else if (type == "자재") query += $@"AND WareType = '10'";
            //else if (type == "공정") query += $@"AND WareType = '30'";
            else if (type == "공정") query += Database.DbName.Equals("YOZM", StringComparison.OrdinalIgnoreCase)
                ? "AND WareType = '30'" : "AND WareType IN ('10', '20', '30')"; //공정재고 창고 임의 지정 - TPG

            return db.GetDataTable(query);
        }

        public DataTable Select_LupWareHouse(bool AllYn = false, string[] type = null)
        {
            query = "";

            if (AllYn)
            {
                query += $@"SELECT'ALL' 창고코드, '전체' 창고명 
                            UNION ALL
                            ";
            }

            query += $@"SELECT HouseID 창고코드, Title 창고명
                        FROM CM_Warehouse
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND UseYn = 'Y'
                    ";

            if (type != null) query += $"\n AND WareType IN ('{string.Join("','", type)}')";

            query += $"\n ORDER BY HouseID ";

            return db.GetDataTable(query);
        }

        public bool Ins_CM_Warehouse()
        {
            query = $@"
                        INSERT INTO CM_Warehouse
                        (
                             CorpId
                            , HouseID
                            , Title
                            , WareType
                            , WareKind
                            , WareState
                            , ProcessCode
                            , DeptCode
                            , Remark
                            , CloseDate
                            , Tel
                            , Fax
                            , Address
                            , UseYn
                            , CreId
                            , CreIP
                            , CreDt
                            , ModId
                            , ModIP
                            , ModDt
                            , ZoneYn
                        ) VALUES
                        (
                             '{CorpId}'
                            ,'{HouseID}'
                            ,{Title.ConvertStr_Empty_To_Null()}
                            ,{WareType.ConvertStr_Empty_To_Null()}
                            ,{WareKind.ConvertStr_Empty_To_Null()}
                            ,{WareState.ConvertStr_Empty_To_Null()}
                            ,{ProcessCode.ConvertStr_Empty_To_Null()}
                            ,{DeptCode.ConvertStr_Empty_To_Null()}
                            ,{Remark.ConvertStr_Empty_To_Null()}
                            ,{CloseDate.ConvertStr_Empty_To_Null()}
                            ,{Tel.ConvertStr_Empty_To_Null()}
                            ,{Fax.ConvertStr_Empty_To_Null()}
                            ,{Address.ConvertStr_Empty_To_Null()}
                            ,{UseYn.ConvertStr_Empty_To_Null()}
                            ,'{WrGlobal.LoginID}'
                            ,'{WrGlobal.ClientHostName}'
                            ,GETDATE()
                            ,'{WrGlobal.LoginID}'
                            ,'{WrGlobal.ClientHostName}'
                            ,GETDATE()
                            ,{ZoneYn.ConvertStr_Empty_To_Null()}
                        )";

            return db.ExecuteQuery(query);
        }

        public bool Upd_CM_Warehouse()
        {
            query = $@"
                        UPDATE CM_Warehouse SET
                             Title        = {Title.ConvertStr_Empty_To_Null()}
                            ,WareType     = {WareType.ConvertStr_Empty_To_Null()}
                            ,WareKind     = {WareKind.ConvertStr_Empty_To_Null()}
                            ,WareState    = {WareState.ConvertStr_Empty_To_Null()}
                            ,ProcessCode  = {ProcessCode.ConvertStr_Empty_To_Null()}
                            ,DeptCode     = {DeptCode.ConvertStr_Empty_To_Null()}
                            ,Remark       = {Remark.ConvertStr_Empty_To_Null()}
                            ,CloseDate    = {CloseDate.ConvertStr_Empty_To_Null()}
                            ,Tel          = {Tel.ConvertStr_Empty_To_Null()}
                            ,Fax          = {Fax.ConvertStr_Empty_To_Null()}
                            ,Address      = {Address.ConvertStr_Empty_To_Null()}
                            ,UseYn        = {UseYn.ConvertStr_Empty_To_Null()}
                            ,ModId        = '{WrGlobal.LoginID}'
                            ,ModIP        = '{WrGlobal.ClientHostName}'
                            ,ModDt        = GETDATE()
                            ,ZoneYn       = {ZoneYn.ConvertStr_Empty_To_Null()}
                        WHERE CorpId  = '{CorpId}'
                          AND HouseID = '{HouseID}'";

            return db.ExecuteQuery(query);
        }

        public bool Del_CM_Warehouse()
        {
            DeleteUnused(Database.m_ConnectionString, CorpId, HouseID);
            return true;
        }

        /// <summary>기본 창고와 품목·라인·수불 등에 연결된 창고는 삭제하지 않습니다.</summary>
        internal static void DeleteUnused(string connectionString, string corpId, string houseId)
        {
            if (string.IsNullOrWhiteSpace(corpId) || string.IsNullOrWhiteSpace(houseId))
                throw new InvalidOperationException("사업장과 창고코드를 확인하세요.");
            if (Array.IndexOf(new[] { "S0001", "S0002", "S0008", "S0009", "S0010" }, houseId.ToUpperInvariant()) >= 0)
                throw new InvalidOperationException("기본 창고는 삭제할 수 없습니다. 필요한 경우 사용 여부를 변경하세요.");
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction(IsolationLevel.Serializable))
                {
                    using (var check = new SqlCommand("SELECT COUNT(*) FROM dbo.CM_Warehouse WITH(UPDLOCK,HOLDLOCK) WHERE CorpId=@corp AND HouseID=@house", connection, transaction))
                    {
                        check.Parameters.AddWithValue("@corp", corpId);
                        check.Parameters.AddWithValue("@house", houseId);
                        if (Convert.ToInt32(check.ExecuteScalar()) != 1)
                            throw new InvalidOperationException("창고가 변경되었거나 삭제되었습니다. 다시 조회하세요.");
                    }
                    var references = new List<string[]>();
                    using (var query = new SqlCommand(@"
                        SELECT SCHEMA_NAME(t.schema_id),t.name,c.name
                        FROM sys.tables t INNER JOIN sys.columns c ON c.object_id=t.object_id
                        WHERE t.object_id<>OBJECT_ID(N'dbo.CM_Warehouse')
                          AND (c.name LIKE '%House%' OR c.name LIKE '%Warehouse%' OR c.name LIKE '%Stock_Cd%')
                          AND TYPE_NAME(c.user_type_id) IN ('nvarchar','varchar','char')
                          AND EXISTS(SELECT 1 FROM sys.columns x WHERE x.object_id=t.object_id AND x.name='CorpId')", connection, transaction))
                    using (var reader = query.ExecuteReader())
                        while (reader.Read()) references.Add(new[] { reader.GetString(0), reader.GetString(1), reader.GetString(2) });
                    Func<string, string> quote = value => "[" + value.Replace("]", "]]") + "]";
                    foreach (var reference in references)
                    {
                        string sql = "SELECT TOP(1) 1 FROM " + quote(reference[0]) + "." + quote(reference[1])
                            + " WITH(HOLDLOCK) WHERE CorpId=@corp AND " + quote(reference[2]) + "=@house";
                        using (var check = new SqlCommand(sql, connection, transaction))
                        {
                            check.Parameters.AddWithValue("@corp", corpId);
                            check.Parameters.AddWithValue("@house", houseId);
                            if (check.ExecuteScalar() != null)
                                throw new InvalidOperationException("연결된 내역이 있어 삭제할 수 없습니다: " + reference[1] + "." + reference[2]
                                    + "\r\n과거 내역을 보존하려면 사용 여부를 해제하세요.");
                        }
                    }
                    using (var delete = new SqlCommand("DELETE dbo.CM_Warehouse WHERE CorpId=@corp AND HouseID=@house", connection, transaction))
                    {
                        delete.Parameters.AddWithValue("@corp", corpId);
                        delete.Parameters.AddWithValue("@house", houseId);
                        if (delete.ExecuteNonQuery() != 1) throw new InvalidOperationException("창고 삭제에 실패했습니다.");
                    }
                    transaction.Commit();
                }
            }
        }

    }
}
