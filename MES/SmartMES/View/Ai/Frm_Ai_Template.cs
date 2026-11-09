using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using System.Drawing.Drawing2D;
using SmartMES.Models.ChatBot;
using System.Data;
using SmartMES.Common;
using System.Threading.Tasks;
using SmartCommon.Common;
using DevExpress.XtraGrid.Views.Grid;
using baseSMT;
using SmartMES.Models.AI;
using SmartMES.Common;
using DevExpress.XtraGrid.Views.Base;
using SmartMES.Models;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;

namespace SmartMES.View.Ai
{
    public partial class Frm_Ai_Template : XtraForm
    {
        private const string ChromaCollectionName = "knowledge_base";

        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();
        ChatBot_Api chatbotApi = new ChatBot_Api();
        string knowledgeSeq = "";
        
        public Frm_Ai_Template()
        {
            InitializeComponent();

            Set_Design();
            
            //Load += FrmKnowledgeBase_Load;
        }

        #region 디자인

        private void Set_Design()
        {
            // 룩업 설정 분류
            Set_Lup();

            // 버튼 이벤트지정
            Set_Btn_Event();
            
            // 그리드
            Set_GridView(Gv_Know);
        }

        private void Set_Lup()
        {
            // 조회
            SearchLupType.smtLookUpDataSource = code.GetCategory("D010", true).ToTable();
            SearchLupType.smtLookUpDataDisplayMember = "분류명";
            SearchLupType.smtLookUpDataValueMember = "분류코드";
            SearchLupType.smtLookUpValue = "ALL";

            SearchLupUseYn.smtLookUpDataSource = GetYn();
            SearchLupUseYn.smtLookUpDataDisplayMember = "분류명";
            SearchLupUseYn.smtLookUpDataValueMember = "분류코드";
            SearchLupUseYn.smtLookUpValue = "ALL";

            // 디테일
            LupType.smtLookUpDataSource = code.GetCategory("D010", false).ToTable();
            LupType.smtLookUpDataDisplayMember = "분류명";
            LupType.smtLookUpDataValueMember = "분류코드";
            LupType.smtLookUpValue = "";
        }

        private void Set_Btn_Event()
        {

        }

        #region 그리드

        private void Set_GridView(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;
            gv.OptionsView.ColumnAutoWidth = false;

            grid.Create_GvColumn(gv, "No", "");
            grid.Create_GvColumn(gv, "품목명", "ItemName");
            grid.Create_GvColumn(gv, "대상월", "RegDate");
            grid.Create_GvColumn(gv, "예상수요량", "Qty");
        }

        private void Gv_Know_FocusedRowChanged(object sender, FocusedRowObjectChangedEventArgs e)
        {

        }

        #endregion
        #endregion

        #region 버튼 분기

        private void Btn_Click(object sender, EventArgs e)
        {
            smtBodyButton Btn = (smtBodyButton)sender;

            string btnname = Btn.Name.ToString();

            switch (btnname)
            {
                case "BtnSave": Func_Save(); break;
                case "BtnDel": Func_Del(); break;
            }
        }

        #endregion

        #region 저장 

        private async void Func_Save()
        {

        }

        #endregion

        #region 삭제 

        private async void Func_Del()
        {

        }

        #endregion

        private void SearchButton_Click(object sender, EventArgs e)
        {
            Func_Search();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            ClearDetail();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        #region  조회 및 셋팅

        private void Func_Search()
        {

        }

        private void ClearDetail()
        {

        }

        #endregion

        #region 모델 데이터 설정

        private ChatBot_Knowledge_Base SetModel(ChatBot_Knowledge_Base knowledgeBase)
        {

            return knowledgeBase;
        }

        #endregion

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Word 문서 (*.doc; *.docx)|*.doc;*.docx";
            if (ofd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            txtFileName.Text = ofd.FileName;
        }

        private DataTable GetYn()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("분류코드", typeof(string));
            dt.Columns.Add("분류명", typeof(string));

            dt.Rows.Add("ALL", "전체");
            dt.Rows.Add("Y", "Y");
            dt.Rows.Add("N", "N");

            return dt;
        }
    }
}
