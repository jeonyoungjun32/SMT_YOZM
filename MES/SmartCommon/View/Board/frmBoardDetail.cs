using DevExpress.CodeParser;
using DevExpress.XtraEditors;
using SmartCommon.Common;
using SmartCommon.View.Documents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartCommon.View.Board
{
    public partial class frmBoardDetail : DevExpress.XtraEditors.XtraForm
    {
        Database db = new Database();
        string query = "";
        string crud = "";
        public frmBoardDetail()
        {
            InitializeComponent();
        }

        public frmBoardDetail(string crud, string boardNo)
        {
            InitializeComponent();

            //query = $"select ItemCode 코드, Title 내용 FROM CM_CateItem where CorpId = '{WrGlobal.CorpID}' and class = 'A70010'";
            //searchClass.Properties.DataSource = db.GetDataView("구분", query);
            //searchClass.Properties.DisplayMember = "내용";
            //searchClass.Properties.ValueMember = "코드";

            //query = $"select ItemCode 코드, Title 내용 FROM CM_CateItem where CorpId = '{WrGlobal.CorpID}' and class = 'A70020'";
            //searchDisplay.Properties.DataSource = db.GetDataView("구분", query);
            //searchDisplay.Properties.DisplayMember = "내용";
            //searchDisplay.Properties.ValueMember = "코드";

            //this.crud = crud;
            //if (crud == "Insert")
            //{
            //    boardBindingSource.AddNew();
            //    DataRowView drv = (DataRowView)boardBindingSource.Current;
            //    drv["CorpId"] = WrGlobal.CorpID;
            //    drv["Class"] = "";
            //    drv["BoardNo"] = "";
            //    drv["WriteDate"] = DateTime.Now;
            //    drv["WriteName"] = WrGlobal.LoginUserNM;
            //    boardBindingSource.EndEdit();

            //}
            //else
            //{
            //    //this.boardTableAdapter.FillByKey(this.documentDataSet.Board, WrGlobal.CorpID, boardNo);
            //}
        }


        private void frmBoardDetail_Load(object sender, EventArgs e)
        {
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {

            //DataRowView drv = (DataRowView)boardBindingSource.Current;
            //drv["WriteDate"] = DateTime.Now;
            ////문서번호 생성 
            //if (crud == "Insert" || crud == "Copy")
            //{
            //    string docNo = db.ExecAutoNumber("공지사항", DateTime.Now.Date);
            //    drv["BoardNo"] = "DB" + docNo;
            //}

            //boardBindingSource.EndEdit();
            //this.boardTableAdapter.Update(this.documentDataSet.Board);
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmDocumentEdit form = new frmDocumentEdit(richEditControl1.RtfText);
            form.onSetData += new frmDocumentEdit.SetData(DocumentSetData);
            form.ShowDialog();
        }

        private void DocumentSetData(string rtfData)
        {
            richEditControl1.RtfText = rtfData;
        }
    }
}