using DevExpress.PivotGrid.OLAP;
using DevExpress.Schedule;
using SmartPOP.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPOP.Models.EQ
{
	public class EQ_Operation
    {
        #region Property
        public string CorpId { get; set; }

        public string WorkDay { get; set; }

        public string ProcessCode { get; set; }

        public string LineCode { get; set; }

        public int Seq { get; set; }

        public string MachineCode { get; set; }

        public string OperationYn { get; set; }

        public string CLASS { get; set; }

        public string ItemCode { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public int StopTime { get; set; }

        public string OrderNo { get; set; }

        public int OrderSeq { get; set; }

        public string Remark { get; set; }

        public string OperationMeasure { get; set; }
        #endregion

        Database db = new Database();
        string query = "";

        public DataTable SelectMachineOP()
		{
            query = $@"SELECT '' Status
		                    , OrderNo
		                    , OrderSeq
		                    , ProcessCode
		                    , LineCode
                            , Seq
		                    , CONVERT(DATETIME, WorkDay, 112) WorkDay
		                    , ClassItemCode
		                    , StartTime
		                    , EndTime
		                    , MachineCode
                            , Remark
		                    , OperationMeasure
                    FROM EQ_Operation
                    WHERE CorpId = '{WrGlobal.CorpID}'
                    AND OrderNo = '{OrderNo}'
                    AND OrderSeq = {OrderSeq}
                    ORDER BY WorkDay DESC, Seq DESC
                    ";

            return db.GetDataTable(query);
        }

        public DataTable SelectOP_EndTimeNULL()
		{
            query = $@"

                SELECT WorkDay, ProcessCode, LineCode, MachineCode, Seq, StartTime, ClassItemCode, OrderNo, OrderSeq
                FROM EQ_Operation
                WHERE CorpId = '{WrGlobal.CorpID}'
                AND OrderNo = '{OrderNo}'
                AND OrderSeq = '{OrderSeq}'
                AND EndTime IS NULL
                ORDER BY WorkDay DESC, Seq DESC
                ";

            return db.GetDataTable(query);
		}


        public int GetNextSeq()
		{
            query = $@"

                    SELECT ISNULL(MAX(Seq), 0) MaxSeq
                    FROM EQ_Operation
                    WHERE CorpId = '{WrGlobal.CorpID}'
                    AND WorkDay     = '{WorkDay}'
                    AND ProcessCode = '{ProcessCode}'
                    AND LineCode    = '{LineCode}'
            ";

            DataRow dr = db.GetDataRow(query);

            int seq = 0;

            if(dr != null)
			{
                seq = dr["MaxSeq"].ConvertInt_Null_To_Zero();
			}

            seq += 1;

            return seq;
		}

        public bool Ins_MachineOP()
		{
            query = $@"
            INSERT INTO EQ_Operation
            (
                CorpId
                , WorkDay
                , ProcessCode
                , LineCode
                , Seq
                , MachineCode
                , OperationYn
                , CLASS
                , ClassItemCode
                , StartTime
                , EndTime
                , StopTime
                , OrderNo
                , OrderSeq
                , Remark
                , OperationMeasure
                , CreId
                , CreIP
                , CreDt
            )
            VALUES
            (
                '{WrGlobal.CorpID}'
                , {WorkDay.ConvertStr_Empty_To_Null()}
                , '{ProcessCode}'
                , '{LineCode}'
                , {Seq.ConvertInt_Empty_To_Null()}
                , {MachineCode.ConvertStr_Empty_To_Null()}
                , {OperationYn.ConvertStr_Empty_To_Null()}
                , {CLASS.ConvertStr_Empty_To_Null()}
                , {ItemCode.ConvertStr_Empty_To_Null()}
                , {StartTime.ConvertStr_Empty_To_Null()}
                , {EndTime.ConvertStr_Empty_To_Null()}
                , {StopTime.ConvertInt_Empty_To_Null()}
                , {OrderNo.ConvertStr_Empty_To_Null()}
                , {OrderSeq.ConvertInt_Empty_To_Null()}
                , {Remark.ConvertStr_Empty_To_Null()}
                , {OperationMeasure.ConvertStr_Empty_To_Null()}
                , '{WrGlobal.LoginID}'
                , '{WrGlobal.ClientHostName}'
                , GETDATE()
            );
            
            
            
            ";

            return db.ExecuteQuery(query);
		}

        public bool Upd_MachineOP()
		{
                            query = $@"
                UPDATE EQ_Operation SET
                    MachineCode        = {MachineCode.ConvertStr_Empty_To_Null()}
                    , ClassItemCode      = {ItemCode.ConvertStr_Empty_To_Null()}
                    , StartTime          = {StartTime.ConvertStr_Empty_To_Null()}
                    , EndTime            = {EndTime.ConvertStr_Empty_To_Null()}
                    , StopTime           = {StopTime.ConvertInt_Empty_To_Null()}
                    , OrderNo            = {OrderNo.ConvertStr_Empty_To_Null()}
                    , OrderSeq           = {OrderSeq.ConvertInt_Empty_To_Null()}
                    , Remark             = {Remark.ConvertStr_Empty_To_Null()}
                    , OperationMeasure   = {OperationMeasure.ConvertStr_Empty_To_Null()}
                    , ModId              = '{WrGlobal.LoginID}'
                    , ModIP              = '{WrGlobal.ClientIpAddress}'
                    , ModDt              = GETDATE()
                WHERE CorpId = '{CorpId}'
                  AND LineCode = '{LineCode}'
                  AND ProcessCode = '{ProcessCode}'
                  AND Seq = '{Seq}'
                  AND WorkDay = '{WorkDay}'
                ";

            return db.ExecuteQuery(query);
		}

        public bool Upd_MachineOP_EndTime()
        {
            query = $@"
                UPDATE EQ_Operation 
                SET
                      EndTime            = {EndTime.ConvertStr_Empty_To_Null()}
                    , StopTime           = {StopTime.ConvertInt_Empty_To_Null()}
                    , ClassItemCode           = '{ItemCode}'
                    , ModId              = '{WrGlobal.LoginID}'
                    , ModIP              = '{WrGlobal.ClientIpAddress}'
                    , ModDt              = GETDATE()
                WHERE CorpId = '{CorpId}'
                  AND LineCode = '{LineCode}'
                  AND ProcessCode = '{ProcessCode}'
                  AND Seq = '{Seq}'
                  AND WorkDay = '{WorkDay}'
                ";

            return db.ExecuteQuery(query);
        }

        public bool Del_MachineOP()
		{
            query = $@"
            DELETE FROM EQ_Operation
            WHERE CorpId = '{CorpId}'
              AND LineCode = '{LineCode}'
              AND ProcessCode = '{ProcessCode}'
              AND Seq = '{Seq}'
              AND WorkDay = '{WorkDay}'
            ";

            return db.ExecuteQuery(query);
        }
    }
}
