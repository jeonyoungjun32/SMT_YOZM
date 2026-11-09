using DevExpress.Xpo.DB.Helpers;
using SmartCommon.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMES.Models.DTL
{
	class DTL_OrderResult
	{
        #region Property
        public string CorpId { get; set; }

        public long Idx { get; set; }

        public string SerialNo { get; set; }

        public string ItemCode { get; set; }

        public string LineCode { get; set; }

        public string ProcessCode { get; set; }

        public string MachineCode { get; set; }

        public string WorkDate { get; set; }

        public decimal WorkQty { get; set; }

        public decimal GoodQty { get; set; }

        public decimal NgQty { get; set; }

        public string CollectType { get; set; }

        public string CollectStatus { get; set; }

        public string CreId { get; set; }

        public string CreIP { get; set; }

        public DateTime CreDt { get; set; }

        #endregion

        Database db = new Database();
        string query = "";


        public DataTable Select_DTL_OrderResult(string startDate, string endDate)
        {
            query = $@"
                SELECT '' AS Status
                     , A.CorpId
                     , cr.CorpName

                     , A.Idx
                     , A.SerialNo

                     , A.ItemCode
                     , cm.ItemName

                     , A.LineCode
                     , cl.LineName

                     , A.ProcessCode
                     , cp.ProcessName

                     , A.MachineCode
                     , eq.MachineName

                     , A.WorkDate
                     , A.WorkQty
                     , A.GoodQty
                     , A.NgQty
                     , A.CollectType
                     , A.CollectStatus
                     , A.CreId
                     , A.CreIP
                     , A.CreDt
                     , A.UseYn

                  FROM DTL_OrderResult A
                LEFT JOIN SM_Corp cr ON A.CorpId = cr.CorpId
                LEFT JOIN CM_Item cm ON A.CorpId = cm.CorpId AND A.ItemCode = cm.ItemCode
                LEFT JOIN CM_Line cl ON A.CorpId = cl.CorpId AND A.LineCode = cl.LineCode
                LEFT JOIN CM_Process cp ON A.CorpId = cp.CorpId AND A.ProcessCode = cp.ProcessCode
                LEFT JOIN EQ_Machine eq ON A.CorpId = eq.CorpId AND A.MachineCode = eq.MachineCode

                 WHERE A.CorpId = '{WrGlobal.CorpID}'
                 AND A.WorkDate BETWEEN '{startDate}' AND '{endDate}'
                ";

            return db.GetDataTable(query);
        }

        public bool Ins_DTL_OrderResult()
        {
            query = $@"
            INSERT INTO DTL_OrderResult
            (
                  CorpId
                , SerialNo
                , ItemCode
                , LineCode
                , ProcessCode
                , MachineCode
                , WorkDate
                , WorkQty
                , GoodQty
                , NgQty
                , CollectType
                , CollectStatus
                , CreId
                , CreIP
                , CreDt
            )
            VALUES
            (
                  '{WrGlobal.CorpID}'
                , {SerialNo.ConvertStr_Empty_To_Null()}
                , {ItemCode.ConvertStr_Empty_To_Null()}
                , {LineCode.ConvertStr_Empty_To_Null()}
                , {ProcessCode.ConvertStr_Empty_To_Null()}
                , {MachineCode.ConvertStr_Empty_To_Null()}
                , {WorkDate.ConvertStr_Empty_To_Null()}
                , {WorkQty.ConvertNumeric_Empty_To_Null()}
                , {GoodQty.ConvertNumeric_Empty_To_Null()}
                , {NgQty.ConvertNumeric_Empty_To_Null()}
                , {CollectType.ConvertStr_Empty_To_Null()}
                , {CollectStatus.ConvertStr_Empty_To_Null()}
                , '{WrGlobal.LoginID}'
                , '{WrGlobal.ClientHostName}'
                , GETDATE()
            )";

            return db.ExecuteQuery(query);
        }

        public bool Ins_Select()
        {
            query = $@"
            INSERT INTO DTL_OrderResult
            (
                CorpId
                , SerialNo
                , ItemCode
                , LineCode
                , ProcessCode
                , MachineCode
                , WorkDate
                , WorkQty
                , GoodQty
                , NgQty
                , CollectType
                , CollectStatus
                , CreId
                , CreIP
                , CreDt
            )

                SELECT 
                  CorpId
                , SerialNo
                , ItemCode
                , LineCode
                , ProcessCode
                , MachineCode
                , WorkDate
                , WorkQty
                , GoodQty
                , NgQty
                , 'MES' --CollectType
                , '정상' --CollectStatus
                , '{WrGlobal.LoginID}'
                , '{WrGlobal.ClientHostName}'
                , GETDATE()
                FROM PM_OrderResult
                WHERE CorpId = '{WrGlobal.CorpID}'
                AND SerialNo NOT IN (SELECT SerialNo FROM DTL_OrderResult)
                ";

            return db.ExecuteQuery(query);
        }

        public bool Upd_DTL_OrderResult()
        {
            query = $@"
            UPDATE DTL_OrderResult
               SET SerialNo = {SerialNo.ConvertStr_Empty_To_Null()}
                 , ItemCode = {ItemCode.ConvertStr_Empty_To_Null()}
                 , LineCode = {LineCode.ConvertStr_Empty_To_Null()}
                 , ProcessCode = {ProcessCode.ConvertStr_Empty_To_Null()}
                 , MachineCode = {MachineCode.ConvertStr_Empty_To_Null()}
                 , WorkDate = {WorkDate.ConvertStr_Empty_To_Null()}
                 , WorkQty = {WorkQty.ConvertNumeric_Empty_To_Null()}
                 , GoodQty = {GoodQty.ConvertNumeric_Empty_To_Null()}
                 , NgQty = {NgQty.ConvertNumeric_Empty_To_Null()}
                 , CollectType = {CollectType.ConvertStr_Empty_To_Null()}
                 , CollectStatus = {CollectStatus.ConvertStr_Empty_To_Null()}
             WHERE CorpId = '{WrGlobal.CorpID}'
               AND Idx = '{Idx}'
            ";

            return db.ExecuteQuery(query);
        }

        public bool Del_DTL_OrderResult()
        {
            query = $@"
            DELETE DTL_OrderResult
             WHERE CorpId = '{WrGlobal.CorpID}'
               AND Idx = '{Idx}'
            ";

            return db.ExecuteQuery(query);
        }
    }
}
