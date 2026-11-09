using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base.ViewInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartCommon.View.Documents
{
    public partial class frmDocumentEdit : DevExpress.XtraEditors.XtraForm
    {
        public delegate void SetData(string rftData);  //델리게이트 선언
        public event SetData onSetData;                 //계약

        string originalRTF;
        public frmDocumentEdit()
        {
            InitializeComponent();
        }

        public frmDocumentEdit(string rtf)
        {
            InitializeComponent();
            richEditControl1.RtfText = rtf;
            originalRTF = rtf;
        }

        private void frmDocumentEdit_Load(object sender, EventArgs e)
        {

            
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            onSetData(richEditControl1.RtfText);
            this.Close();
        }
    }
}