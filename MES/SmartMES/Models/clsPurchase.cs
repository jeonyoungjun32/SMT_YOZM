using SmartMES.Common;
using System;
using System.Collections.Generic;
using SmartMES.Models.MM;
using System.Data;
using System.Linq;
using SmartCommon.Common;
using System.Text;
using System.Threading.Tasks;

namespace SmartMES.Models
{
    public class clsPurchase
    {
        Database db = new Database();
        string query = "";

        #region IDisposable Support
        private bool disposedValue = false; // 중복 호출을 검색하려면

        ~clsPurchase()
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

        public string GetPlacingState(string mmOrderNo)
        {
            query = string.Format("Select State From MM_PlacingM Where CorpId = '{0}' and MmOrderNo = '{1}'", WrGlobal.CorpID, mmOrderNo);
            DataRowView drv = db.GetDataRecord(query);
            return drv["State"].ToString();
        }
        // 발주 삭제
        public void DeleteMM_PlacingM(string mmOrderNo)
        {
            query = String.Format("Delete MM_PlacingD Where CorpId ='{0}' And MmOrderNo = '{1}'", WrGlobal.CorpID, mmOrderNo);
            db.ExecuteQuery(query);

            query = String.Format("Delete MM_PlacingM Where CorpId ='{0}' And MmOrderNo = '{1}'", WrGlobal.CorpID, mmOrderNo);
            db.ExecuteQuery(query);
        }
        // 발주 완료처리
        public void EndMM_PlacingM(string mmOrderNo)
        {
            if (MM_PlacingD.UseYozmOrderColumns)
            {
                query = $"UPDATE MM_PlacingM SET CloseDate = CAST(GETDATE() AS date), CloseUserID = '{WrGlobal.LoginID}' " +
                    $"WHERE CorpId = '{WrGlobal.CorpID}' AND MmOrderNo = '{mmOrderNo}'";
                db.ExecuteQuery(query);
                return;
            }

            query = String.Format("Update MM_PlacingM Set State = '완료', CloseDate = Cast(getdate() as date), CloseUserID = '{2}' Where CorpId ='{0}' And MmOrderNo = '{1}'", WrGlobal.CorpID, mmOrderNo, WrGlobal.LoginID);
            db.ExecuteQuery(query);

            query = String.Format("Update MM_PlacingD Set State = '완료' Where CorpId ='{0}' And MmOrderNo = '{1}'", WrGlobal.CorpID, mmOrderNo, WrGlobal.LoginID);
            db.ExecuteQuery(query);

            //query = String.Format("Delete MM_PlacingD Set State = '완료', BalanceQty = 0 Where CorpId ='{0}' And MmOrderNo = '{1}'", WrGlobal.CorpID, mmOrderNo);
            //db.ExecuteQuery(query);
        }

        public void UpdatePlacingInQtyByPurchase(string stoNo)
        {
            query = string.Format("exec Placing_UpdateInQtyByPurchase '{0}', '{1}'", WrGlobal.CorpID, stoNo);
            db.ExecuteQuery(query);
        }

        public void MakeStockByPurchase(string stoNo)
        {
            query = string.Format("exec Stock_MakeStockByPurchase '{0}', '{1}', '{2}'", WrGlobal.CorpID, stoNo, WrGlobal.LoginID);
            db.ExecuteQuery(query);
        }

        public void DeleteMM_PurchaseM(string coId, string stoNo)
        {
            if (MM_PlacingD.UseYozmOrderColumns)
            {
                db.ExecuteQuery($@"
                    SET XACT_ABORT ON;
                    BEGIN TRANSACTION;
                    EXEC dbo.Placing_UpdateInQtyByPurchase '{coId}', '{stoNo}', 1;
                    DELETE dbo.MM_PurchaseD WHERE CorpId = '{coId}' AND StoNo = '{stoNo}';
                    DELETE dbo.MM_PurchaseM WHERE CorpId = '{coId}' AND StoNo = '{stoNo}';
                    COMMIT;");
                return;
            }

            query = String.Format("Delete MM_PurchaseD Where CorpId ='{0}' And StoNo = '{1}'", coId, stoNo);
            db.ExecuteQuery(query);

            query = String.Format("Delete MM_PurchaseM Where CorpId ='{0}' And StoNo = '{1}'", coId, stoNo);
            db.ExecuteQuery(query);
        }

        public string GetReqeustPoState(string reqNo)
        {//구매의뢰 상태값 확인
            query = string.Format("Select State From RequestPoMaster Where CorpId = '{0}' and ReqNo = '{1}'", WrGlobal.CorpID, reqNo);
            DataRowView drv = db.GetDataRecord(query);
            return drv["State"].ToString();
        }

        public void DeleteReqeustPoMaster(string reqNo)
        {//구매의뢰(Master) 상태값 확인
            query = String.Format("Delete RequestPoDetail Where CorpId ='{0}' And ReqNo = '{1}'", WrGlobal.CorpID, reqNo);
            db.ExecuteQuery(query);

            query = String.Format("Delete RequestPoMaster Where CorpId ='{0}' And ReqNo = '{1}'", WrGlobal.CorpID, reqNo);
            db.ExecuteQuery(query);
        }

        public void EndRequestPoMaster(string reqNo)
        {//구매의뢰(Master) 발주완료
            query = String.Format("Update RequestPoMaster Set State = '확정', ModDt = Cast(getdate() as date), ModId = '{2}', ModIP = host_name() Where CorpId ='{0}' And ReqNo = '{1}'", WrGlobal.CorpID, reqNo, WrGlobal.LoginID);
            db.ExecuteQuery(query);

            query = String.Format("Update RequestPoDetail Set State = '확정',ModDt = Cast(getdate() as date), ModId = '{2}', ModIP = host_name() Where CorpId ='{0}' And ReqNo = '{1}'", WrGlobal.CorpID, reqNo, WrGlobal.LoginID);
            db.ExecuteQuery(query);
        }
    }
}
