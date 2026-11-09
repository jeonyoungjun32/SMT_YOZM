using SmartCommon.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMES.Models.ChatBot
{
    class ChatBot
    {
        #region Property
        public string Material { get; set; }
        public string WorkDate { get; set; }

        #endregion

        Database db = new Database();
        string query = "";

        public DataTable Select_CmdStatus(string date)
        {
            query = $@"
                SELECT 
                      op.OrderNo
                    , op.ItemCode AS ItemCode
					, cm.ItemName
                    , SUM(op.OrderQty) AS Qty
                    , COUNT(*) OVER() AS Count
                FROM PM_OrderProcess op
				LEFT JOIN CM_Item cm ON cm.ItemCode = op.ItemCode
                WHERE SUBSTRING(op.OrderNo,3,6) = '{date}'
                GROUP BY OrderNo, ItemCode,ItemName
            ";
            return db.GetDataTable(query);
        }


        public DataTable Select_PM_OrderResult(string date)
        {
            query = $@"
                SELECT 
                      ore.SerialNo
                    , op.ItemCode
					, cm.ItemName
					, op.Status
                    , SUM(op.GoodQty) AS GoodQty
                    , SUM(op.NgQty) AS NgQty
                    , COUNT(*) OVER() AS CmdCount
                FROM PM_OrderResult ore
				LEFT JOIN PM_OrderProcess op ON op.OrderNo = ore.OrderNo AND op.OrderSeq = ore.OrderSeq
				LEFT JOIN CM_Item cm ON cm.ItemCode = op.ItemCode
                WHERE SUBSTRING(ore.SerialNo,3,6) = '{date}'
                GROUP BY ore.SerialNo, ItemCode,ItemName,Status
	            Order By op.Status
            ";
            return db.GetDataTable(query);
        }
        public DataTable Select_StockListView()
        {
            query = $@"
                SELECT ItemCode, ItemName, UnitCode, StockQty 
                FROM StockListView WHERE ItemCode = '{Material}' OR ItemName = '{Material}' 
            ";
            return db.GetDataTable(query);
        }

        public DataTable Select_StockListView_Material()
        {
            query = $@"
                SELECT ItemCode, ItemName, UnitCode, StockQty 
                FROM StockListView 
                WHERE 1=1
            ";

            if (Material != null && Material != "" && Material.ToUpper() != "ALL")
            {
                query += $"\nAND ItemCode LIKE '%{Material}%'  OR ItemName LIKE '%{Material}%'  ";
            }
            return db.GetDataTable(query);
        }

        public DataTable Select_PM_OrderProcess()
        {
            query = $@"
                SELECT op.ItemCode
						, ci.ItemName
						, ISNULL(OrderQty, 0) OrderQty
						, ISNULL(WorkQty, 0) WorkQty
						, Status
				FROM PM_OrderProcess op 
				LEFT JOIN CM_Item ci ON ci.CorpId = op.CorpId AND ci.ItemCode = op.ItemCode
                WHERE op.CorpId = '{WrGlobal.CorpID}'
            ";

            if (WorkDate != null && WorkDate != "" )
            {
                query += $"\nAND CONVERT(VARCHAR(8), op.OrderDate, 112) = '{WorkDate}'  ";
            }
            return db.GetDataTable(query);
        }

        public DataTable Select_PM_OrderResult()
        {
            query = $@"
                SELECT rst.ItemCode
						, ci.ItemName
						, ISNULL(WorkQty, 0) WorkQty
						, ISNULL(GoodQty, 0) GoodQty
						, ISNULL(NgQty, 0) NgQty
				FROM PM_OrderResult rst
				LEFT JOIN CM_Item ci ON ci.CorpId = rst.CorpId AND ci.ItemCode = rst.ItemCode
                WHERE rst.CorpId = '{WrGlobal.CorpID}'
            ";

            if (WorkDate != null && WorkDate != "")
            {
                query += $"\nAND CONVERT(VARCHAR(8), rst.WorkDate, 112) = '{WorkDate}'  ";
            }
            return db.GetDataTable(query);
        }
    }
}
