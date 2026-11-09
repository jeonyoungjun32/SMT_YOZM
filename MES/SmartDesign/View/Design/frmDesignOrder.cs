using DevExpress.XtraEditors;
using SmartCommon;
using SmartCommon.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartDesign.View.Design
{
    public partial class frmDesignOrder : DevExpress.XtraEditors.XtraForm
    {
        Database db = new Database();
        string query;


        public frmDesignOrder()
        {
            InitializeComponent();
        }

        private void frmDesignOrder_Load(object sender, EventArgs e)
        {
            query = string.Format("Select CLASS, CLASSNAME, HIGHCLASS From Class Where Class like 'Z0%'");
            treeListDocument.DataSource = db.GetDataView("공통문서분류", query);
            treeListDocument.ExpandAll();
        }

        private void treeListDocument_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            string itemCode = treeListDocument.GetFocusedRowCellDisplayText(treeListDocument.Columns["CLASS"]);

            query = $"select * from documentCommonFile Where CorpID = '{WrGlobal.CorpID}' and ItemCode = '{itemCode}'";
            gcCmDevelopmentStdFile.DataSource = db.GetDataView("서식", query);
        }

        private void gvCmDevelopmentStdFile_DoubleClick(object sender, EventArgs e)
        {

            spreadsheetControl1.LoadDocument("c:\\temp\\작업지시서.xlsx");

            //if (gvCmDevelopmentStdFile.FocusedRowHandle < 0) return;
            //List<string> selPaths = new List<string>();
            //List<string> selOriFiles = new List<string>();
            //List<string> selFiles = new List<string>();

            //selPaths.Add(gvCmDevelopmentStdFile.GetFocusedRowCellValue("Path").ToString());
            //selOriFiles.Add(gvCmDevelopmentStdFile.GetFocusedRowCellValue("OriFileName").ToString());
            //selFiles.Add(gvCmDevelopmentStdFile.GetFocusedRowCellValue("FileName").ToString());

            //long totalSize = Convert.ToInt64(gvCmDevelopmentStdFile.GetFocusedRowCellValue("Capacity"));
            //string fileName = @"c:\temp\" + gvCmDevelopmentStdFile.GetFocusedRowCellValue("FileName").ToString();

            //FtpFileDownloader downloader = new FtpFileDownloader(fileName, selPaths.ToArray(), selOriFiles.ToArray(), selFiles.ToArray(), totalSize);
            //downloader.ShowDialog();
        }
    }
}