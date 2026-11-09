using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using SmartMES.Common;
using SmartCommon.Common;

namespace SmartMES.Report
{
    public partial class rptPM_ProductPlan : DevExpress.XtraReports.UI.XtraReport
    {
        Database db = new Database();
        string query = "";
        public rptPM_ProductPlan()
        {
            InitializeComponent();

            DisplayData();
        }

        private void DisplayData()
        {
            query = $@"SELECT A.WK_CD
		                    , A.WK_SUB_CD
		                    , CONCAT(A.PROCESS, ' - ', A.LINE) PROCESS
		                    , A.WORKTYPE
		                    , A.WORK_DATE
		                    , A.WORK_EDATE
		                    , A.REMARK
		                    , A.ItemCode
		                    , A.ITEM_NM
		                    , A.SIZE
		                    , A.UNIT_DC
		                    , A.WORK_QT

		                    , B.Seq
		                    , B.ItemCode ItemCode2
		                    , B.ITEM_NM ITEM_NM2
		                    , B.SIZE SIZE2
		                    , B.UNIT_DC UNIT_DC2
		                    , B.QT 
		                    , B.REMARK REMARK2
                    FROM TempWorkMaster A
                    LEFT JOIN TempWorkDetail B ON A.HOSTNAME = B.HOSTNAME
                    WHERE A.HOSTNAME = HOST_NAME()
                        ";
            
            this.DataSource = db.GetDataView("리스트", query);
        }
    }
}
