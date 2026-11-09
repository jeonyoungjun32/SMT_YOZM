using SmartCommon.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMES.Models.DTL
{
	public class DTL_Log
	{
        #region Property
        public string CorpId { get; set; }

        public long LogSeq { get; set; }

        public string OccurDate { get; set; }

        public string LogType { get; set; }

        public string JobName { get; set; }

        public string LogMsg { get; set; }

        public int ProcessCnt { get; set; }

        public decimal TotalSec { get; set; }

        #endregion

        Database db = new Database();
        string query = "";

        public DataTable Select_DTL_Log(string startDate, string endDate)
        {
            LogType = LogType.ConvertString_Null_To_Empty();

            query = $@"
            SELECT '' AS Status
                 , CorpId
                 , LogSeq
                 , OccurDate
                 , LogType
                 , JobName
                 , LogMsg
                 , ProcessCnt
                 , TotalSec
              FROM DTL_Log
             WHERE CorpId = '{WrGlobal.CorpID}'
             AND OccurDate BETWEEN '{startDate}' AND '{endDate} 23:59:59'
            ";

            if(LogType != "" && LogType != "ALL")
			{
                query += $"\nAND LogType = '{LogType}'";
			}

            return db.GetDataTable(query);
        }

        public bool Ins_DTL_Log()
        {
            query = $@"
            INSERT INTO DTL_Log
            (
                  CorpId
                , OccurDate
                , LogType
                , JobName
                , LogMsg
                , ProcessCnt
                , TotalSec
            )
            VALUES
            (
                  '{WrGlobal.CorpID}'
                , '{OccurDate}'
                , {LogType.ConvertStr_Empty_To_Null()}
                , {JobName.ConvertStr_Empty_To_Null()}
                , {LogMsg.ConvertStr_Empty_To_Null()}
                , {ProcessCnt.ConvertNumeric_Empty_To_Null()}
                , {TotalSec.ConvertNumeric_Empty_To_Null()}
            )";

            return db.ExecuteQuery(query);
        }

        public bool Upd_DTL_Log()
        {
            query = $@"
            UPDATE DTL_Log
               SET OccurDate = '{OccurDate}'
                 , LogType = {LogType.ConvertStr_Empty_To_Null()}
                 , JobName = {JobName.ConvertStr_Empty_To_Null()}
                 , LogMsg = {LogMsg.ConvertStr_Empty_To_Null()}
                 , ProcessCnt = {ProcessCnt.ConvertNumeric_Empty_To_Null()}
                 , TotalSec = {TotalSec.ConvertNumeric_Empty_To_Null()}
             WHERE CorpId = '{WrGlobal.CorpID}'
               AND LogSeq = '{LogSeq}'
            ";

            return db.ExecuteQuery(query);
        }

        public bool Del_DTL_Log()
        {
            query = $@"
            DELETE DTL_Log
             WHERE CorpId = '{WrGlobal.CorpID}'
               AND LogSeq = '{LogSeq}'
            ";

            return db.ExecuteQuery(query);
        }
    }
}
