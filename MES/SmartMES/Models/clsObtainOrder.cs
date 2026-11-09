using SmartMES.Common;
using System;
using System.Collections.Generic;
using System.Data;
using SmartCommon.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMES.Models
{
    public class clsObtainOrder
    {
        Database db = new Database();
        string query;

        public string GetObtainState(string corpId, string sdOrderNo)
        {
            query = string.Format("Select State From BM_SdOrderM Where CorpId = '{0}' and SdOrderNo = '{1}'", corpId, sdOrderNo);
            DataRowView drv = db.GetDataRecord(query);
            return drv["State"].ToString();
        }

        public void DeleteBM_SdOrderM(string coId, string sdOrderNo)
        {
            query = String.Format("Delete BM_SdOrderD Where CorpId ='{0}' And SdOrderNo = '{1}'", coId, sdOrderNo);
            db.ExecuteQuery(query);

            query = String.Format("Delete BM_SdOrderM Where CorpId ='{0}' And SdOrderNo = '{1}'", coId, sdOrderNo);
            db.ExecuteQuery(query);
        }

        public bool DeleteCheckBM_SalesD(string sdOrderNo)
        {

            query = String.Format("Select top 1 * From BM_SalesD Where CorpId = '{0}' and SdOrderNo = '{1}'", WrGlobal.CorpID, sdOrderNo);
            DataRowView drv = db.GetDataRecord(query);

            if (drv != null)
            {
                return true;
            }

            return false;
        }

        public bool DeleteCheckOrderProcess(string sdOrderNo)
        {
            query = $@"
                SELECT TOP 1 op.CorpId
                FROM PM_OrderProcess op
                LEFT JOIN PM_ProductPlan pp ON op.CorpId = pp.CorpId AND op.OrderNO = pp.OrderNo
                WHERE op.CorpId = '{WrGlobal.CorpID}'
                AND pp.OrderNo = '{sdOrderNo}'
                ";

            DataRowView drv = db.GetDataRecord(query);

            if (drv != null)
            {
                return true;
            }

            return false;
        }

        public void UpdateOrderProgressBySdOrderDelete(string sdOrderNo)
        {
            query = String.Format("exec Obtain_UpdateOrderProgressBySdOrderDelete '{0}', '{1}'", WrGlobal.CorpID, sdOrderNo);
            db.ExecuteQuery(query);
        }

        public void DeleteMM_PlacingM(string coId, string mmOrderNo)
        {
            query = String.Format("Delete MM_PlacingD Where CorpId ='{0}' And MmOrderNo = '{1}'", coId, mmOrderNo);
            db.ExecuteQuery(query);

            query = String.Format("Delete MM_PlacingM Where CorpId ='{0}' And MmOrderNo = '{1}'", coId, mmOrderNo);
            db.ExecuteQuery(query);
        }

        public int GetMM_PurchaseDCnt(string corpId, string stoNo)
        {
            query = string.Format("Select Count(*) CNT From MM_PurchaseD Where CorpId = '{0}' and StoNo = '{1}'", corpId, stoNo);
            DataRowView drv = db.GetDataRecord(query);
            return (int)drv["CNT"];
        }

        public void DeleteMM_PurchaseM(string coId, string stoNo)
        {
            query = String.Format("Delete MM_PurchaseM Where CorpId ='{0}' And StoNo = '{1}'", coId, stoNo);
            db.ExecuteQuery(query);
        }

        public string GetPlacingState(string corpId, string mmOrderNo)
        {
            query = string.Format("Select State From MM_PlacingM Where CorpId = '{0}' and MmOrderNo = '{1}'", corpId, mmOrderNo);
            DataRowView drv = db.GetDataRecord(query);
            return drv["State"].ToString();
        }

        public void UpdateObtainOutQtyBySales(string corpId, string salesNo)
        {
            query = string.Format("exec Sales_UpdateOutQtyToObtain '{0}', '{1}'", corpId, salesNo);
            db.ExecuteQuery(query);
        }

        public void UpdatePlacingInQtyByPurchase(string stoNo)
        {
            query = string.Format("exec Placing_UpdateInQtyToPurchase '{0}', '{1}'", WrGlobal.CorpID, stoNo);
            db.ExecuteQuery(query);
        }

        public void MakeStockByPurchase(string stoNo)
        {
            query = string.Format("exec Stock_MakeStockByPurchase '{0}', '{1}', '{2}'", WrGlobal.CorpID, stoNo, WrGlobal.LoginID);
            db.ExecuteQuery(query);
        }

        public void MakeStockBySales(string salesNo)
        {
            query = string.Format("exec Stock_MakeStockBySales '{0}', '{1}', '{2}'", WrGlobal.CorpID, salesNo, WrGlobal.LoginID);
            db.ExecuteQuery(query);
        }

        public DataView GetCustomerByObtainDate(DateTime startDate, DateTime endDate, string isAll)
        {
            query = string.Format("exec Obtain_GetCustomerByObtainDate '{0}', '{1:yyyy-MM-dd}', '{2:yyyy-MM-dd}', '{3}'",
                                WrGlobal.CorpID, startDate, endDate, isAll);
            return db.GetDataView("주문거래처", query);
        }

        public DataView GetProductByObtainDate(DateTime startDate, DateTime endDate, string isAll)
        {
            query = string.Format("exec Obtain_GetProductByObtainDate '{0}', '{1:yyyy-MM-dd}', '{2:yyyy-MM-dd}', '{3}'",
                                WrGlobal.CorpID, startDate, endDate, isAll);
            return db.GetDataView("주문품목", query);
        }

        public int GetSubSeqByRequestOutOrder(string sdOrderNo, int sdOrderSeq)
        {
            query = string.Format("Select isnull(max(SubSeq), 0) + 1 SubSeq From RequestOutOrder Where CorpId = '{0}' and SdOrderNo = '{1}' and SdOrderSeq = {2}", WrGlobal.CorpID, sdOrderNo, sdOrderSeq);
            DataRowView drv = db.GetDataRecord(query);
            return (int)drv[0];
        }

        public void UpdateSdOrderQtyByRequestOutOrder(string sdOrderNo, int sdOrderSeq)
        {
            query = string.Format("exec Sales_UpdateSdOrderQtyByRequestOutOrder '{0}', '{1}', {2}", WrGlobal.CorpID, sdOrderNo, sdOrderSeq);
            db.ExecuteQuery(query);
        }


        public int GetCollectDetailCnt(string collectNo)
        {
            query = string.Format("Select Count(*) CNT From CollectDetail Where CorpId = '{0}' and CollectNo = '{1}'", WrGlobal.CorpID, collectNo);
            DataRowView drv = db.GetDataRecord(query);
            return (int)drv["CNT"];
        }

        public void DeleteCollectMaster(string collectNo)
        {
            query = String.Format("Delete CollectMaster Where CorpId ='{0}' And CollectNo = '{1}'", WrGlobal.CorpID, collectNo);
            db.ExecuteQuery(query);
        }

        public DataView GetSalseDetailLotGroupList(string salseNo, int salseSeq)
        {
            query = string.Format("exec Sales_GetDetailLotGroupList '{0}', '{1}', {2}",
                                WrGlobal.CorpID, salseNo, salseSeq);
            return db.GetDataView("출고로트", query);
        }

        public DataRowView GetReturnSalesInfo(string salseNo, int salseSeq)
        {
            query = string.Format("exec Return_GetSalesInfo '{0}', '{1}', {2}", WrGlobal.CorpID, salseNo, salseSeq);
            return db.GetDataRecord(query);
        }

        public void MakeStockByReturn(string returnNo)
        {
            query = string.Format("exec Stock_MakeStockByReturn '{0}', '{1}'", WrGlobal.CorpID, returnNo);
            db.ExecuteQuery(query);
        }

        public void DeleteStockByReturn(string returnNo)
        {
            query = string.Format("exec Stock_DeleteStockByReturn '{0}', '{1}'", WrGlobal.CorpID, returnNo);
            db.ExecuteQuery(query);
        }
        public void Insert_TempSelectionKey(string _type, string salesNo)
        {
            query = string.Format("Insert Into TempSelectionKey(corpid , HostName , DataKey)  values ({0}, Host_Name(), '{1}')", WrGlobal.CorpID, salesNo);
            db.ExecuteQuery(query);
        }

        public void Excute_SalesInvocie(string _type)
        {
            query = string.Format("Exec Sales_Invocie '{0}', '{1}' ", WrGlobal.CorpID, _type);
            db.ExecuteQuery(query);
        }

        public void Delete_TempSelectionKey()
        {
            query = string.Format("Delete TempSelectionKey where CorpId = '{0}' and HostName = Host_Name()", WrGlobal.CorpID);
            db.ExecuteQuery(query);
        }

        public void ExecutePurchaseClose(DataTable table)
        {
            query = string.Format("Delete TempSelectionKey where CorpId = '{0}' and HostName = Host_Name()", WrGlobal.CorpID);
            db.ExecuteQuery(query);

            db.WriteBulkInsert(table);

            query = string.Format("exec Purchase_ExecuteClose '{0}', '{1}'", WrGlobal.CorpID, WrGlobal.ClientHostName);
            db.ExecuteQuery(query);
        }

        public void ExecuteSalesClose(DataTable table)
        {
            query = string.Format("Delete TempSelectionKey where CorpId = '{0}' and HostName = Host_Name()", WrGlobal.CorpID);
            db.ExecuteQuery(query);

            db.WriteBulkInsert(table);

            query = string.Format("exec Sales_ExecuteClose '{0}', '{1}'", WrGlobal.CorpID, WrGlobal.ClientHostName);
            db.ExecuteQuery(query);
        }

        public void CollectUpdateBalancePrice(string collectNo, string custCode, DateTime collectDate)
        {
            query = string.Format("exec Collect_UpdateBalancePrice '{0}', '{1}', '{2}', '{3:yyyy-MM-dd}'", WrGlobal.CorpID, collectNo, custCode, collectDate);
            db.ExecuteQuery(query);
        }

        public DataView GetBM_SalesDHistory(string gubun, string writeID, string custCode, DateTime startDate, DateTime endDate)
        {
            query = string.Format("exec Sales_GetDetailHistory '{0}', '{1}', '{2}', '{3}', '{4:yyyy-MM-dd}', '{5:yyyy-MM-dd}'",
                                WrGlobal.CorpID, gubun, writeID, custCode, startDate, endDate);
            return db.GetDataView("출고현황", query);
        }

        public void DeleteBM_SalesM(string coId, string salesNo)
        {
            query = String.Format("Delete BM_SalesD Where CorpId ='{0}' And SalesNo = '{1}'", coId, salesNo);
            db.ExecuteQuery(query);

            query = String.Format("Delete BM_SalesM Where CorpId ='{0}' And SalesNo = '{1}'", coId, salesNo);
            db.ExecuteQuery(query);
        }

        public DataView GetObtainDetailCopyList(string sdOrderNo, string custCode)
        {
            query = string.Format("exec Obtain_GetDetailCopyList '{0}', '{1}', '{2}'",
                                WrGlobal.CorpID, sdOrderNo, custCode);
            return db.GetDataView("주문품목", query);
        }

        public DataRowView GetObtainMasterCopyRow(string sdOrderNo)
        {
            query = string.Format("Select * From BM_SdOrderM Where CorpId = '{0}' and SdOrderNo = '{1}'", WrGlobal.CorpID, sdOrderNo);
            return db.GetDataRecord(query);
        }
        public string GetOrderMasterTotalPrice(string corpId, string sdOrderNo)
        {
            query = string.Format("Select TotalPrice From BM_SdOrderM Where CorpId = '{0}' and SdOrderNo = '{1}'", corpId, sdOrderNo);
            DataRowView drv = db.GetDataRecord(query);
            return drv["TotalPrice"].ToString();
        }

        public string GetOrderDetailQty(string corpId, string sdOrderNo, string ItemCode)
        {
            query = string.Format("Select Qty From BM_SdOrderD Where CorpId = '{0}' and SdOrderNo = '{1}' and ItemCode = '{2}'", corpId, sdOrderNo, ItemCode);
            DataRowView drv = db.GetDataRecord(query);
            return drv["Qty"].ToString();
        }
    }
}
