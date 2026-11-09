using SmartMES.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System;
using System.Data.SqlClient;
using SmartCommon.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMES.Models.EQ
{
	public class EQ_Machine
	{
        #region Property
        public string CorpId { get; set; }

        public string MachineCode { get; set; }

        public string MachineName { get; set; }

        public string Size { get; set; }

        public string AliasName { get; set; }

        public string NFCUid { get; set; }

        public string UnitCode { get; set; }

        public string UnitName { get; set; }

        public string Abbr { get; set; }

        public int Qty { get; set; }

        public string EquType { get; set; }

        public string ChargeNm { get; set; }

        public string SubChargeNm { get; set; }

        public string ObtainDate { get; set; }

        public float ObtainAmount { get; set; }

        public string SerialNo { get; set; }

        public string Maker { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string RentalDate { get; set; }

        public string ReturnDate { get; set; }

        public string RentalCustCode { get; set; }

        public string MType { get; set; }

        public string MGroup { get; set; }

        public string DisposalDate { get; set; }

        public string CapitalCode { get; set; }

        public string MachineType { get; set; }

        public string Context { get; set; }

        public string Score { get; set; }

        public string CheckDate { get; set; }

        public string ModelNo { get; set; }

        public string Prodution { get; set; }

        public string MachineUse { get; set; }

        public string CalDefect { get; set; }

        public string IdleMeasuring { get; set; }

        public string Correction { get; set; }

        public string NonCorrection { get; set; }

        public string CommID { get; set; }

        public string Middleware { get; set; }

        public string IpAddress { get; set; }

        public string Status { get; set; }

        public string CreateYYMM { get; set; }

        public int ArrayValue { get; set; }

        public int RPM { get; set; }

        public float ProductionYield { get; set; }

        public string UseYn { get; set; }

        public string Remark { get; set; }

        #endregion

        Database db = new Database();
		string query = "";

        // YOZM 설비 상세 컬럼을 조회와 저장에서 같은 목록으로 사용합니다.
        internal static readonly string[] DetailFields =
        {
            "ProductionPlantName",
            "ProductionLineName",
            "InstallDate",
            "ManufactureDate",
            "UsePlantName",
            "MaintenanceContactName",
            "MaintenanceContactTel",
            "InspectionDepartmentName",
            "InspectionStartDate",
            "InspectionEndDate",
            "IsMeasuringEquipment",
            "MeasuringInstrumentNo",
            "PowerSupplyType",
            "PowerConsumptionKw",
            "AgingGrade",
            "LegacyMachineCode",
            "SourceRegisteredAt",
            "EquipmentNumber",
            "RegisteredMachineName",
            "OriginalMachineName",
        };

        /// <summary>
        /// 화면에 연결된 항목만 매개변수로 저장하여 표시하지 않는 기존 설비정보를 보존합니다.
        /// </summary>
        internal void SaveDetails(DataRow row, bool insert)
        {
            var fields = new[]
            {
                "MachineName", "Remark", "UseYn", "AliasName", "Prodution"
            }.Concat(DetailFields.Where(field => field != "SourceRegisteredAt"
                && field != "RegisteredMachineName" && field != "OriginalMachineName")).ToArray();
            if (string.IsNullOrWhiteSpace(Convert.ToString(row["MachineName"])))
                throw new InvalidOperationException("설비명을 입력하세요.");
            if (string.IsNullOrWhiteSpace(MachineCode) || MachineCode.Length > 10)
                throw new InvalidOperationException("설비코드는 10자 이내로 입력하세요.");

            using (var connection = new SqlConnection(Database.m_ConnectionString))
            using (var command = connection.CreateCommand())
            {
                Func<string, string> column = field => "[" + field + "]";
                command.CommandText = insert
                    ? "INSERT dbo.EQ_Machine (CorpId, MachineCode, " + string.Join(", ", fields.Select(column))
                        + ", CreId, CreIP, CreDt) VALUES (@corp, @code, "
                        + string.Join(", ", fields.Select(field => "@" + field)) + ", @actor, @host, GETDATE())"
                    : "UPDATE dbo.EQ_Machine SET " + string.Join(", ", fields.Select(field => column(field) + "=@" + field))
                        + ", ModId=@actor, ModIP=@host, ModDt=GETDATE() WHERE CorpId=@corp AND MachineCode=@code";
                command.Parameters.AddWithValue("@corp", CorpId);
                command.Parameters.AddWithValue("@code", MachineCode);
                command.Parameters.AddWithValue("@actor", WrGlobal.LoginID);
                command.Parameters.AddWithValue("@host", WrGlobal.ClientHostName);
                foreach (string field in fields)
                {
                    object value = row[field];
                    if (value is string && string.IsNullOrWhiteSpace((string)value))
                        value = DBNull.Value;
                    command.Parameters.AddWithValue("@" + field, value ?? DBNull.Value);
                }
                connection.Open();
                if (command.ExecuteNonQuery() != 1)
                    throw new InvalidOperationException("설비가 변경되었거나 삭제되었습니다. 다시 조회하세요.");
            }
        }

        public DataTable SelectMachine()
		{
            MGroup = MGroup.ConvertString_Null_To_Empty();
            UseYn = UseYn.ConvertString_Null_To_Empty();

            query = $@"
                SELECT '' AS Status
                , ma.CorpId
                , ma.MachineCode
                , ma.MachineName
                , ma.Size
                , ma.AliasName
                , ma.NFCUid
                , ma.UnitCode
                , ma.UnitName
                , ma.Abbr
                , ma.Qty
                , ma.EquType
                , ma.ChargeNm
                , ma.SubChargeNm
                , ma.ObtainDate
                , ma.ObtainAmount

                , ma.SerialNo
                , ma.Maker
                , ma.StartDate
                , ma.EndDate
                , ma.RentalDate
                , ma.ReturnDate
                , ma.RentalCustCode
                , ma.MType
                , ma.MGroup
                , ma.DisposalDate
                , ma.CapitalCode
                , ma.MachineType
                , ma.Context
                , ma.Score
                , ma.CheckDate
                , ma.ModelNo
                , ma.Prodution
                , ma.MachineUse
                , ma.CalDefect
                , ma.IdleMeasuring
                , ma.Correction
                , ma.NonCorrection
                , ma.CommID
                , ma.Middleware
                , ma.IpAddress
                , ma.Remark
                , ma.Status AS MachineStatus
                , ma.UseYn
                , ma.CreId
                , ma.CreIP
                , ma.CreDt
                , ma.ModId
                , ma.ModIP
                , ma.ModDt
                , ma.CreateYYMM
                {(Database.DbName.Equals("YOZM", StringComparison.OrdinalIgnoreCase)
                    ? ", " + string.Join(", ", DetailFields.Select(field => "ma.[" + field + "]")) : "")}
                FROM EQ_Machine ma
                WHERE CorpId = '{WrGlobal.CorpID}'
            ";

            if(UseYn != "")
			{
                query += $"\nAND ma.UseYN = '{UseYn}'";
			}

            if(MGroup != "")
			{
                query += $"\nAND ma.MGroup = '{MGroup}'";
			}

             DataTable dt = db.GetDataTable(query);

            return dt;
		}

        public DataTable Select_Machine()
        {
            MGroup = MGroup.ConvertString_Null_To_Empty();

            query = $@"
                SELECT '' AS Status
                , ma.CorpId
                , ma.MachineCode
                , ma.MachineName
                , ma.Size
                , ma.AliasName
                , ma.NFCUid
                , ma.UnitCode
                , ma.UnitName
                , ma.Abbr
                , ma.Qty
                , ma.EquType
                , ma.ChargeNm
                , ma.SubChargeNm
                , ma.ObtainDate
                , ma.ObtainAmount

                , ma.SerialNo
                , ma.Maker
                , ma.StartDate
                , ma.EndDate
                , ma.RentalDate
                , ma.ReturnDate
                , ma.RentalCustCode
                , ma.MType
                , ma.MGroup
                , ma.DisposalDate
                , ma.CapitalCode
                , ma.MachineType
                , ma.Context
                , ma.Score
                , ma.CheckDate
                , ma.ModelNo
                , ma.Prodution
                , ma.MachineUse
                , ma.CalDefect
                , ma.IdleMeasuring
                , ma.Correction
                , ma.NonCorrection
                , ma.CommID
                , ma.Middleware
                , ma.IpAddress
                , ma.Remark
                , ma.Status
                , ma.UseYn
                , ma.CreId
                , ma.CreIP
                , ma.CreDt
                , ma.ModId
                , ma.ModIP
                , ma.ModDt
                , ma.CreateYYMM
                , ma.ArrayValue
                , ma.RPM
                , ma.ProductionYield
                FROM EQ_Machine ma
                WHERE CorpId = '{CorpId}'
            ";

            if (UseYn != "")
            {
                query += $"\nAND ma.UseYN = '{UseYn}'";
            }

            if (MGroup != "")
            {
                query += $"\nAND ma.MGroup = '{MGroup}'";
            }

            DataTable dt = db.GetDataTable(query);

            return dt;
        }

        public DataTable Select_Machine_Exam(string examDivCd)
        {
            MGroup = MGroup.ConvertString_Null_To_Empty();

            query = $@"
                SELECT '' AS Status
                , ma.CorpId
                , ma.MachineCode
                , ma.MachineName
                , ma.Size
                , ma.AliasName
                , ma.NFCUid
                , ma.UnitCode
                , ma.UnitName
                , ma.Abbr
                , ma.RPM
                , CASE WHEN EXISTS(SELECT TOP 1
                        		                  iem.MachineCode
                                             FROM QM_ItemExamMgnt iem
                                            WHERE iem.CorpId = ma.CorpId AND iem.MachineCode = ma.MachineCode
                                            AND ExamDivCd = '{examDivCd}'
                                    ) THEN 'Y'
                               ELSE 'N'
                          END AS [ExistsExam]
                FROM EQ_Machine ma
                WHERE CorpId = '{CorpId}'
            ";

            if (UseYn != "")
            {
                query += $"\nAND ma.UseYN = '{UseYn}'";
            }

            if (MGroup != "")
            {
                query += $"\nAND ma.MGroup = '{MGroup}'";
            }

            DataTable dt = db.GetDataTable(query);

            return dt;
        }

        public DataTable Select_LupMachine(bool allYn = false)
        {
            query = "";

            if (allYn) query += $@"SELECT'ALL' 코드, '전체' 명칭
                                    UNION ALL
                                    ";

            query += $@"
                        SELECT MachineCode AS 코드
                                , MachineName AS 명칭
                        FROM EQ_Machine
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND UseYn = 'Y'
                        ORDER BY 코드
                        ";

            return db.GetDataTable(query);
        }

        public DataTable Select_MachineCd_NotMe()
        {
            query = $@"
                        SELECT MachineCode, MachineName 
                        FROM EQ_Machine
                        WHERE CorpId = '{WrGlobal.CorpID}' AND MachineCode <> '{MachineCode}'
                    ";

            return db.GetDataTable(query);
        }

        public DataRow GetMachineData()
        {
            query = $@"
                        SELECT ArrayValue, RPM, ProductionYield
                        FROM EQ_Machine
                        WHERE CorpId = '{WrGlobal.CorpID}' AND MachineCode = '{MachineCode}'
                    ";

            return db.GetDataRow(query);
        }

        public string GetNewMachineCode()
		{
            query = $@"SELECT dbo.FN_Create_Cd_Machine('{CorpId}')";

            return db.GetDataScalar(query).ConvertString_Null_To_Empty();
        }

        public bool Ins_Machine()
		{
            query = $@"
                    INSERT INTO EQ_Machine
                    (
                        CorpId
                        , MachineCode
                        , MachineName
                        , Size
                        , AliasName
                        , NFCUid
                        , UnitCode
                        , UnitName
                        , Abbr
                        , Qty
                        , EquType
                        , ChargeNm
                        , SubChargeNm
                        , ObtainDate
                        , ObtainAmount
                        , SerialNo
                        , Maker
                        , StartDate
                        , EndDate
                        , RentalDate
                        , ReturnDate
                        , RentalCustCode
                        , MType
                        , MGroup
                        , DisposalDate
                        , CapitalCode
                        , MachineType
                        , Context
                        , Score
                        , CheckDate
                        , ModelNo
                        , Prodution
                        , MachineUse
                        , CalDefect
                        , IdleMeasuring
                        , Correction
                        , NonCorrection
                        , CommID
                        , Middleware
                        , IpAddress
                        , Remark
                        , Status
                        , UseYn
                        , CreId
                        , CreIP
                        , CreDt
                        , CreateYYMM
                    )
                    VALUES
                    (
                        '{WrGlobal.CorpID}'
                        , {MachineCode.ConvertStr_Empty_To_Null()}
                        , {MachineName.ConvertStr_Empty_To_Null()}
                        , {Size.ConvertStr_Empty_To_Null()}
                        , {AliasName.ConvertStr_Empty_To_Null()}
                        , {NFCUid.ConvertStr_Empty_To_Null()}
                        , {UnitCode.ConvertStr_Empty_To_Null()}
                        , {UnitName.ConvertStr_Empty_To_Null()}
                        , {Abbr.ConvertStr_Empty_To_Null()}
                        , {Qty.ConvertInt_Empty_To_Null()}
                        , {EquType.ConvertStr_Empty_To_Null()}
                        , {ChargeNm.ConvertStr_Empty_To_Null()}
                        , {SubChargeNm.ConvertStr_Empty_To_Null()}
                        , {ObtainDate.ConvertStr_Empty_To_Null()}
                        , {ObtainAmount.ConvertFloat_Null_To_Zero()}
                        , {SerialNo.ConvertStr_Empty_To_Null()}
                        , {Maker.ConvertStr_Empty_To_Null()}
                        , {StartDate.ConvertStr_Empty_To_Null()}
                        , {EndDate.ConvertStr_Empty_To_Null()}
                        , {RentalDate.ConvertStr_Empty_To_Null()}
                        , {ReturnDate.ConvertStr_Empty_To_Null()}
                        , {RentalCustCode.ConvertStr_Empty_To_Null()}
                        , {MType.ConvertStr_Empty_To_Null()}
                        , {MGroup.ConvertStr_Empty_To_Null()}
                        , {DisposalDate.ConvertStr_Empty_To_Null()}
                        , {CapitalCode.ConvertStr_Empty_To_Null()}
                        , {MachineType.ConvertStr_Empty_To_Null()}
                        , {Context.ConvertStr_Empty_To_Null()}
                        , {Score.ConvertStr_Empty_To_Null()}
                        , {CheckDate.ConvertStr_Empty_To_Null()}
                        , {ModelNo.ConvertStr_Empty_To_Null()}
                        , {Prodution.ConvertStr_Empty_To_Null()}
                        , {MachineUse.ConvertStr_Empty_To_Null()}
                        , {CalDefect.ConvertStr_Empty_To_Null()}
                        , {IdleMeasuring.ConvertStr_Empty_To_Null()}
                        , {Correction.ConvertStr_Empty_To_Null()}
                        , {NonCorrection.ConvertStr_Empty_To_Null()}
                        , {CommID.ConvertStr_Empty_To_Null()}
                        , {Middleware.ConvertStr_Empty_To_Null()}
                        , {IpAddress.ConvertStr_Empty_To_Null()}
                        , {Remark.ConvertStr_Empty_To_Null()}
                        , {Status.ConvertStr_Empty_To_Null()}
                        , {UseYn.ConvertStr_Empty_To_Null()}
                        , '{WrGlobal.LoginID}'
                        , '{WrGlobal.ClientHostName}'
                        , GETDATE()
                        , {CreateYYMM.ConvertStr_Empty_To_Null()}
                    );


            ";

            return db.ExecuteQuery(query);
		}

        public bool InsertMachine()
        {
            query = $@"
                    INSERT INTO EQ_Machine
                    (
                        CorpId
                        , MachineCode
                        , MachineName
                        , Size
                        , AliasName
                        , NFCUid
                        , UnitCode
                        , UnitName
                        , Abbr
                        , Qty
                        , EquType
                        , ChargeNm
                        , SubChargeNm
                        , ObtainDate
                        , ObtainAmount
                        , SerialNo
                        , Maker
                        , StartDate
                        , EndDate
                        , RentalDate
                        , ReturnDate
                        , RentalCustCode
                        , MType
                        , MGroup
                        , DisposalDate
                        , CapitalCode
                        , MachineType
                        , Context
                        , Score
                        , CheckDate
                        , ModelNo
                        , Prodution
                        , MachineUse
                        , CalDefect
                        , IdleMeasuring
                        , Correction
                        , NonCorrection
                        , CommID
                        , Middleware
                        , IpAddress
                        , Remark
                        , Status
                        , UseYn
                        , CreId
                        , CreIP
                        , CreDt
                        , CreateYYMM
                        , ArrayValue
                        , RPM
                        , ProductionYield
                    )
                    VALUES
                    (
                        '{WrGlobal.CorpID}'
                        , {MachineCode.ConvertStr_Empty_To_Null()}
                        , {MachineName.ConvertStr_Empty_To_Null()}
                        , {Size.ConvertStr_Empty_To_Null()}
                        , {AliasName.ConvertStr_Empty_To_Null()}
                        , {NFCUid.ConvertStr_Empty_To_Null()}
                        , {UnitCode.ConvertStr_Empty_To_Null()}
                        , {UnitName.ConvertStr_Empty_To_Null()}
                        , {Abbr.ConvertStr_Empty_To_Null()}
                        , {Qty.ConvertInt_Empty_To_Null()}
                        , {EquType.ConvertStr_Empty_To_Null()}
                        , {ChargeNm.ConvertStr_Empty_To_Null()}
                        , {SubChargeNm.ConvertStr_Empty_To_Null()}
                        , {ObtainDate.ConvertStr_Empty_To_Null()}
                        , {ObtainAmount.ConvertFloat_Null_To_Zero()}
                        , {SerialNo.ConvertStr_Empty_To_Null()}
                        , {Maker.ConvertStr_Empty_To_Null()}
                        , {StartDate.ConvertStr_Empty_To_Null()}
                        , {EndDate.ConvertStr_Empty_To_Null()}
                        , {RentalDate.ConvertStr_Empty_To_Null()}
                        , {ReturnDate.ConvertStr_Empty_To_Null()}
                        , {RentalCustCode.ConvertStr_Empty_To_Null()}
                        , {MType.ConvertStr_Empty_To_Null()}
                        , {MGroup.ConvertStr_Empty_To_Null()}
                        , {DisposalDate.ConvertStr_Empty_To_Null()}
                        , {CapitalCode.ConvertStr_Empty_To_Null()}
                        , {MachineType.ConvertStr_Empty_To_Null()}
                        , {Context.ConvertStr_Empty_To_Null()}
                        , {Score.ConvertStr_Empty_To_Null()}
                        , {CheckDate.ConvertStr_Empty_To_Null()}
                        , {ModelNo.ConvertStr_Empty_To_Null()}
                        , {Prodution.ConvertStr_Empty_To_Null()}
                        , {MachineUse.ConvertStr_Empty_To_Null()}
                        , {CalDefect.ConvertStr_Empty_To_Null()}
                        , {IdleMeasuring.ConvertStr_Empty_To_Null()}
                        , {Correction.ConvertStr_Empty_To_Null()}
                        , {NonCorrection.ConvertStr_Empty_To_Null()}
                        , {CommID.ConvertStr_Empty_To_Null()}
                        , {Middleware.ConvertStr_Empty_To_Null()}
                        , {IpAddress.ConvertStr_Empty_To_Null()}
                        , {Remark.ConvertStr_Empty_To_Null()}
                        , {Status.ConvertStr_Empty_To_Null()}
                        , {UseYn.ConvertStr_Empty_To_Null()}
                        , '{WrGlobal.LoginID}'
                        , '{WrGlobal.ClientHostName}'
                        , GETDATE()
                        , {CreateYYMM.ConvertStr_Empty_To_Null()}
                        , {ArrayValue.ConvertInt_Empty_To_Null()}
                        , {RPM.ConvertInt_Empty_To_Null()}
                        , {ProductionYield.ConvertFloat_Empty_To_Null()}
                    );


            ";

            return db.ExecuteQuery(query);
        }

        public bool Upd_Machine()
		{
            query = $@"
                UPDATE EQ_Machine SET
                    MachineName        = {MachineName.ConvertStr_Empty_To_Null()}
                    , Size               = {Size.ConvertStr_Empty_To_Null()}
                    , AliasName          = {AliasName.ConvertStr_Empty_To_Null()}
                    , NFCUid             = {NFCUid.ConvertStr_Empty_To_Null()}
                    , UnitCode           = {UnitCode.ConvertStr_Empty_To_Null()}
                    , UnitName           = {UnitName.ConvertStr_Empty_To_Null()}
                    , Abbr               = {Abbr.ConvertStr_Empty_To_Null()}
                    , Qty                = {Qty.ConvertInt_Empty_To_Null()}
                    , EquType            = {EquType.ConvertStr_Empty_To_Null()}
                    , ChargeNm           = {ChargeNm.ConvertStr_Empty_To_Null()}
                    , SubChargeNm        = {SubChargeNm.ConvertStr_Empty_To_Null()}
                    , ObtainDate         = {ObtainDate.ConvertStr_Empty_To_Null()}
                    , ObtainAmount       = {ObtainAmount.ConvertFloat_Null_To_Zero()}
                    , SerialNo           = {SerialNo.ConvertStr_Empty_To_Null()}
                    , Maker              = {Maker.ConvertStr_Empty_To_Null()}
                    , StartDate          = {StartDate.ConvertStr_Empty_To_Null()}
                    , EndDate            = {EndDate.ConvertStr_Empty_To_Null()}
                    , RentalDate         = {RentalDate.ConvertStr_Empty_To_Null()}
                    , ReturnDate         = {ReturnDate.ConvertStr_Empty_To_Null()}
                    , RentalCustCode     = {RentalCustCode.ConvertStr_Empty_To_Null()}
                    , MType              = {MType.ConvertStr_Empty_To_Null()}
                    , MGroup             = {MGroup.ConvertStr_Empty_To_Null()}
                    , DisposalDate       = {DisposalDate.ConvertStr_Empty_To_Null()}
                    , CapitalCode        = {CapitalCode.ConvertStr_Empty_To_Null()}
                    , MachineType        = {MachineType.ConvertStr_Empty_To_Null()}
                    , Context            = {Context.ConvertStr_Empty_To_Null()}
                    , Score              = {Score.ConvertStr_Empty_To_Null()}
                    , CheckDate          = {CheckDate.ConvertStr_Empty_To_Null()}
                    , ModelNo            = {ModelNo.ConvertStr_Empty_To_Null()}
                    , Prodution          = {Prodution.ConvertStr_Empty_To_Null()}
                    , MachineUse         = {MachineUse.ConvertStr_Empty_To_Null()}
                    , CalDefect          = {CalDefect.ConvertStr_Empty_To_Null()}
                    , IdleMeasuring      = {IdleMeasuring.ConvertStr_Empty_To_Null()}
                    , Correction         = {Correction.ConvertStr_Empty_To_Null()}
                    , NonCorrection      = {NonCorrection.ConvertStr_Empty_To_Null()}
                    , CommID             = {CommID.ConvertStr_Empty_To_Null()}
                    , Middleware         = {Middleware.ConvertStr_Empty_To_Null()}
                    , IpAddress          = {IpAddress.ConvertStr_Empty_To_Null()}
                    , Remark             = {Remark.ConvertStr_Empty_To_Null()}
                    , Status             = {Status.ConvertStr_Empty_To_Null()}
                    , UseYn              = {UseYn.ConvertStr_Empty_To_Null()}
                    , ModId              = '{WrGlobal.LoginID}'
                    , ModIP              = '{WrGlobal.ClientIpAddress}'
                    , ModDt              = GETDATE()
                    , CreateYYMM         = {CreateYYMM.ConvertStr_Empty_To_Null()}
                WHERE CorpId = '{CorpId}'
                AND MachineCode = '{MachineCode}'


            ";

            return db.ExecuteQuery(query);
        }

        public bool UpdateMachine()
        {
            query = $@"
                UPDATE EQ_Machine SET
                    MachineName        = {MachineName.ConvertStr_Empty_To_Null()}
                    , Size               = {Size.ConvertStr_Empty_To_Null()}
                    , AliasName          = {AliasName.ConvertStr_Empty_To_Null()}
                    , NFCUid             = {NFCUid.ConvertStr_Empty_To_Null()}
                    , UnitCode           = {UnitCode.ConvertStr_Empty_To_Null()}
                    , UnitName           = {UnitName.ConvertStr_Empty_To_Null()}
                    , Abbr               = {Abbr.ConvertStr_Empty_To_Null()}
                    , Qty                = {Qty.ConvertInt_Empty_To_Null()}
                    , EquType            = {EquType.ConvertStr_Empty_To_Null()}
                    , ChargeNm           = {ChargeNm.ConvertStr_Empty_To_Null()}
                    , SubChargeNm        = {SubChargeNm.ConvertStr_Empty_To_Null()}
                    , ObtainDate         = {ObtainDate.ConvertStr_Empty_To_Null()}
                    , ObtainAmount       = {ObtainAmount.ConvertFloat_Null_To_Zero()}
                    , SerialNo           = {SerialNo.ConvertStr_Empty_To_Null()}
                    , Maker              = {Maker.ConvertStr_Empty_To_Null()}
                    , StartDate          = {StartDate.ConvertStr_Empty_To_Null()}
                    , EndDate            = {EndDate.ConvertStr_Empty_To_Null()}
                    , RentalDate         = {RentalDate.ConvertStr_Empty_To_Null()}
                    , ReturnDate         = {ReturnDate.ConvertStr_Empty_To_Null()}
                    , RentalCustCode     = {RentalCustCode.ConvertStr_Empty_To_Null()}
                    , MType              = {MType.ConvertStr_Empty_To_Null()}
                    , MGroup             = {MGroup.ConvertStr_Empty_To_Null()}
                    , DisposalDate       = {DisposalDate.ConvertStr_Empty_To_Null()}
                    , CapitalCode        = {CapitalCode.ConvertStr_Empty_To_Null()}
                    , MachineType        = {MachineType.ConvertStr_Empty_To_Null()}
                    , Context            = {Context.ConvertStr_Empty_To_Null()}
                    , Score              = {Score.ConvertStr_Empty_To_Null()}
                    , CheckDate          = {CheckDate.ConvertStr_Empty_To_Null()}
                    , ModelNo            = {ModelNo.ConvertStr_Empty_To_Null()}
                    , Prodution          = {Prodution.ConvertStr_Empty_To_Null()}
                    , MachineUse         = {MachineUse.ConvertStr_Empty_To_Null()}
                    , CalDefect          = {CalDefect.ConvertStr_Empty_To_Null()}
                    , IdleMeasuring      = {IdleMeasuring.ConvertStr_Empty_To_Null()}
                    , Correction         = {Correction.ConvertStr_Empty_To_Null()}
                    , NonCorrection      = {NonCorrection.ConvertStr_Empty_To_Null()}
                    , CommID             = {CommID.ConvertStr_Empty_To_Null()}
                    , Middleware         = {Middleware.ConvertStr_Empty_To_Null()}
                    , IpAddress          = {IpAddress.ConvertStr_Empty_To_Null()}
                    , Remark             = {Remark.ConvertStr_Empty_To_Null()}
                    , Status             = {Status.ConvertStr_Empty_To_Null()}
                    , UseYn              = {UseYn.ConvertStr_Empty_To_Null()}
                    , ModId              = '{WrGlobal.LoginID}'
                    , ModIP              = '{WrGlobal.ClientIpAddress}'
                    , ModDt              = GETDATE()
                    , CreateYYMM         = {CreateYYMM.ConvertStr_Empty_To_Null()}
                    , ArrayValue         = {ArrayValue.ConvertInt_Empty_To_Null()}
                    , RPM                = {RPM.ConvertInt_Empty_To_Null()}
                    , ProductionYield    = {ProductionYield.ConvertFloat_Empty_To_Null()}
                WHERE CorpId = '{CorpId}'
                AND MachineCode = '{MachineCode}'


            ";

            return db.ExecuteQuery(query);
        }

        public bool Del_Machine()
		{
            query = $@"
                DELETE FROM EQ_Machine
                WHERE CorpId = '{CorpId}'
                AND MachineCode = '{MachineCode}'
            ";

            return db.ExecuteQuery(query);
		}
    }
}
