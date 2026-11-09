using SmartMES.Common;
using System.Data;
using SmartCommon.Common;

namespace SmartMES.Models.IF
{
	public class IF_DGPCode
    {
        #region Property

        public string CTCode { get; set; }
        public string CTName { get; set; }
        public string DgpCode { get; set; }
        public string DgpName { get; set; }
        public string DgpSpec { get; set; }
        public string StatCD { get; set; }
        public string Equip_Name { get; set; }
        public string MemoryCode { get; set; }
        public string Gauge_Type { get; set; }
        public string Error_Or { get; set; }
        public string Write_Address { get; set; }
        public decimal Error_Max { get; set; }
        public decimal Error_Min { get; set; }
        public string Value_Or { get; set; }
        public decimal Value_Max { get; set; }
        public decimal Value_Min { get; set; }

        public string PlcModel { get; set; }

        #endregion

        Database db = new Database();
        string query = "";

        public DataTable Select_IF_DGPCode()
        {
            query = $@"
                        SELECT '' AS Status
                            , CTCode
                            , DgpCode
                            , DgpName
                            , DgpSpec
                            , StatCD
                            , Equip_Name
                            , MemoryCode
                            , Write_Address
                            , Gauge_Type
                            , Error_Or
                            , Error_Max
                            , Error_Min
                            , Value_Or
                            , Value_Max
                            , Value_Min
                        FROM IF_DGPCode
                        WHERE 1=1
                    ";

            if(Equip_Name != null)
			{
                query += $"\nAND Equip_Name = '{Equip_Name}'";
            }

            if (CTCode != null)
            {
                query += $"\nAND CTCode = '{CTCode}'";
            }

            return db.GetDataTable(query);
        }

        public DataTable Select_ActiveQty()
        {
            DgpCode = DgpCode.ConvertString_Null_To_Empty();
            Equip_Name = Equip_Name.ConvertString_Null_To_Empty();
            Gauge_Type = Gauge_Type.ConvertString_Null_To_Empty();

            query = $@" 
                        SELECT 
                            C.DgpCode
                            , C.DgpName
                            , C.Gauge_Type
                            , C.Value_Max
                            , C.Value_Min
                            , H.Active_Qty
                            , C.Equip_Name
                            , MC.MachineName
                            , T.Counter
                        FROM IF_DGPCode C
                        INNER JOIN IF_DgpHour H ON C.DgpCode = H.DgpCode
                        LEFT JOIN EQ_Machine MC ON C.Equip_Name = MC.MachineCode
                        LEFT JOIN IF_DgpCounter T ON C.DgpCode = T.DgpCode
                        WHERE 1 = 1
                      ";

            if (DgpCode != "")
            {
                query += $"\nAND C.DgpCode = '{DgpCode}' ";
            }

            if (Equip_Name != "")
            {
                query += $"\nAND C.Equip_Name = '{Equip_Name}' ";
            }

            if (Gauge_Type != "")
            {
                query += $"\nAND C.Gauge_Type = '{Gauge_Type}' ";
            }

            return db.GetDataTable(query);
        }

        public DataTable Select_DGPMachine()
        {
            //수정필요
            query = $@" SELECT DISTINCT Equip_Name 
                        FROM IF_DGPCode  
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND ISNULL(Equip_Name, '') <> ''
                        AND DgpCode LIKE '{DgpCode}%'
                        ";

            return db.GetDataTable(query);
        }

        public DataTable Select_DGPList()
        {
            query = $@" SELECT DISTINCT DgpCode, DgpName, Equip_Name
                        FROM IF_DGPCode
                        WHERE DgpCode LIKE '{DgpCode}%'
                        ";

            return db.GetDataTable(query);
        }

        public DataTable Select_DGPList_Counter()
        {
            query = $@" 
					    SELECT DgpCode, DgpName, Equip_Name
                            FROM IF_DGPCode
                            WHERE Gauge_Type = '카운터'
					    order by len(DgpName),DgpName

                        ";

            return db.GetDataTable(query);
        }

        public int SelectDgpCnt()
        {
            int cnt = 0;
            query = $@"

                SELECT COUNT(*) AS CNT
		        FROM IF_DGPCode
            ";

            DataRow dr = db.GetDataRow(query);

            if (dr != null)
            {
                cnt = dr["CNT"].ConvertInt_Null_To_Zero();
            }

            return cnt;
        }

        public bool UpdateDGPValue_Or()
        {
            Equip_Name = Equip_Name.ConvertString_Null_To_Empty();

            query = $@" UPDATE IF_DGPCode 
                        SET Value_Or = '{Value_Or}' 
                        WHERE 1 = 1 ";

            if (Equip_Name != "")
            {
                query += $"\nAND Equip_Name = '{Equip_Name}' ";
            }

            return db.ExecuteQuery(query);
        }

        public DataTable Select_DGPCounter()
        {
            query = $@"
                        WITH TableFilter AS 
		                        (
			                        SELECT 
				                        LineCode,
				                        OrderNo,
				                        OrderSeq,
				                        ItemCode,
				                        OrderQty,
				                        WorkQty,
				                        GoodQty,
				                        NgQty,
				                        ROW_NUMBER() OVER (
					                        PARTITION BY LineCode 
					                        ORDER BY OrderNo, OrderSeq
				                        ) AS RowNum
			                        FROM 
				                        PM_OrderProcess
			                        WHERE 
				                        Status = '작업중'
		                        )

                        SELECT
	                        mc.DgpCode
	                        , mc.DgpName
	                        , mc.Equip_Name
	                        , mc.Gauge_Type
	                        , mct.Counter
	                        , CL.LineCode
	                        , TF.ItemCode
	                        , CM.ItemName
                        FROM IF_DGPCode mc
                        LEFT JOIN IF_DGPCounter mct ON mc.DgpCode = mct.DgpCode
                        INNER JOIN CM_Line CL ON mc.Equip_Name = CL.MachineCode
                        LEFT JOIN TableFilter TF ON CL.LineCode = TF.LineCode AND TF.RowNum = 1
                        LEFT JOIN CM_Item CM ON TF.ItemCode = cm.ItemCode
                        WHERE mc.DgpCode = '{DgpCode}' AND Gauge_Type = '카운터'
                    ";

            return db.GetDataTable(query);
        }

        public DataTable Select_DGPTemp_SM()
        {
            query = $@"
                        SELECT
                            mc.DgpCode
                            , mc.DgpName
                            , mc.Equip_Name
                            , cm.MachineName
                            , mc.Gauge_Type
                            , mtc.Counter as SetValue
							, mtc2.Counter as NowValue
                        FROM IF_DGPCode mc
                        LEFT JOIN IF_DGPCounter mtc ON mc.DgpCode = mtc.DgpCode
						LEFT JOIN IF_DGPCode mc2 ON mc.DgpName = mc2.DgpName AND mc2.Gauge_Type = '온도'
						LEFT JOIN IF_DGPCounter mtc2 ON mc2.DgpCode = mtc2.DgpCode
                        LEFT JOIN EQ_Machine cm ON mc.Equip_Name = cm.MachineCode
                        WHERE mc.CorpId = '{WrGlobal.CorpID}'
                        AND mc.Equip_Name = '{Equip_Name}'
						AND mc.Gauge_Type = '설정'
                    ";

            return db.GetDataTable(query);
        }

        public DataRow Select_DGPRow_ByKey_SM()
        {
            query = $@"
                        SELECT
                            mc.DgpCode
                            , mc.DgpName
                            , mc.Equip_Name
                            , cm.MachineName
                        FROM IF_DGPCode mc
                        LEFT JOIN EQ_Machinecm ON mc.Equip_Name = cm.MachineCode
                        WHERE mc.CorpId = '{WrGlobal.CorpID}'
                        AND mc.DgpCode = '{DgpCode}'
                    ";

            return db.GetDataRow(query);
        }

        public bool Ins_IF_DGPCode()
        {
            query = $@"
                        DECLARE @DgpCode VARCHAR(10) = dbo.FN_Create_DgpCode('{CTCode}');

                        INSERT INTO IF_DGPCode
                        (
                            Corpid
                            , CTCode
                            , DgpCode
                            , DgpName
                            , DgpSpec
                            , Equip_Name
                            , MemoryCode
                            , Write_Address
                            , Gauge_Type
                            , Value_Or
                            , Value_Max
                            , Value_Min
                        )
                        VALUES
                        (
                            '{WrGlobal.CorpID}'
                            , '{CTCode}'
                            , @DgpCode
                            , '{DgpName}'
                            , '{DgpSpec}'
                            , '{Equip_Name}'
                            , '{MemoryCode}'
                            , '{Write_Address}'
                            , '{Gauge_Type}'
                            , '{Value_Or}'
                            , {Value_Max}
                            , {Value_Min}
                        )

                        INSERT INTO IF_DGPCounter
                        (
                            Corpid
                            , DgpCode
                            , DgpName
                            , GroupID
                            , Counter
                            , LastTime
                        )
                        VALUES
                        (
                            '{WrGlobal.CorpID}'
                            , @DgpCode
                            , '{DgpName}'
                            , ''
                            , 0
                            , NULL
                        )

                        INSERT INTO IF_DGPStatus
                        (
                            Corpid
                            , DgpCode
                            , DgpName
                            , PlcModel
                            , [Use]
                            , MesSendState
                            , CounterReset
                            , MachineState
                        )
                        VALUES
                        (
                            '{WrGlobal.CorpID}'
                            , @DgpCode
                            , '{DgpName}'
                            , '{PlcModel}'
                            , 'YES'
                            , ''
                            , 'OFF'
                            , ''
                        )
                    ";

            return db.ExecuteQuery(query);
        }

        public bool Upd_IF_DGPCode()
        {
            query = $@"
                        UPDATE IF_DGPCode
                        SET
                            DgpName = '{DgpName}'
                            , DgpSpec = '{DgpSpec}'
                            , Equip_Name = '{Equip_Name}'
                            , Gauge_Type = '{Gauge_Type}'
                            , MemoryCode = '{MemoryCode}'
                            , Write_Address = '{Write_Address}'
                            , Value_Or = '{Value_Or}'
                            , Value_Max = {Value_Max}
                            , Value_Min = {Value_Min}
                        WHERE Corpid = '{WrGlobal.CorpID}'
                        AND DgpCode = '{DgpCode}'

                        UPDATE IF_DGPCounter
                        SET
                            DgpName = '{DgpName}'
                        WHERE Corpid = '{WrGlobal.CorpID}'
                        AND DgpCode = '{DgpCode}'

                        UPDATE IF_DGPStatus
                        SET
                            DgpName = '{DgpName}'
                        WHERE Corpid = '{WrGlobal.CorpID}'
                        AND DgpCode = '{DgpCode}'
                    ";

            return db.ExecuteQuery(query);
        }

        public bool Del_IF_DGPCode()
        {
            query = $@"
                        DELETE IF_DGPStatus
                        WHERE DgpCode = '{DgpCode}'

                        DELETE IF_DGPCounter
                        WHERE DgpCode = '{DgpCode}'

                        DELETE IF_DGPCode
                        WHERE DgpCode = '{DgpCode}'
                    ";

            return db.ExecuteQuery(query);
        }
    }
}
