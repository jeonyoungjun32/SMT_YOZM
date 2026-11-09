using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraReports.UI;
using SmartCommon.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace SmartMES.Models
{
	public class clsCode
    {
        Database db = new Database();
        string query = "";

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
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemType { get; set; }
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

        #endregion

        public DataRowView GetMaterialInfo(string ItemCode)
        {
            query = string.Format("Select * From CM_Item Where CorpId = '{0}' and ItemCode = '{1}'", WrGlobal.CorpID, ItemCode);
            return db.GetDataRecord(query);
        }

        public DataRowView GetMoldnfo(string MoldCode)
        {
            query = string.Format("Select * From MD_Mold Where CorpId = '{0}' and MoldCode = '{1}'", WrGlobal.CorpID, MoldCode);
            return db.GetDataRecord(query);
        }

        public XtraReport GetSelectedReport(string url)
        {
            // Return a report by a URL selected in the ListBox.
            if (url == string.Empty)
                return null;
            using (MemoryStream stream = new MemoryStream(WrGlobal.reportStorage.GetData(url)))
            {
                return XtraReport.FromStream(stream, true);
            }
        }

        public XtraReport GetSelectedReport(int reportId)
        {
            query = $"Select LayoutData From ReportItem Where ReportId = {reportId}";
            DataRowView drv = db.GetDataRecord(query);

            if (drv != null)
            {
                byte[] strByte = Encoding.UTF8.GetBytes(drv["LayoutData"].ToString());
                using (MemoryStream stream = new MemoryStream(strByte))
                {
                    return XtraReport.FromStream(stream, true);
                }
            }
            else
            {
                return null;
            }
        }

    }
}
