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

namespace SmartMES.View.Chatbot
{
    public partial class FrmKnowledgeBase : XtraForm
    {
        private const string ChromaCollectionName = "knowledge_base";

        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();
        ChatBot_Api chatbotApi = new ChatBot_Api();
        string knowledgeSeq = "";
        
        public FrmKnowledgeBase()
        {
            InitializeComponent();

            Set_Design();

            Load += FrmKnowledgeBase_Load;
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
            // 분류
            code.Class = "C020";
            DataTable dt_Catagory_All = code.Select_CM_CateItem(true);
            DataTable dt_Catagory = code.Select_CM_CateItem();

            SearchLupType.smtLookUpDataSource = dt_Catagory_All;
            SearchLupType.smtLookUpDataValueMember = "ItemCode";
            SearchLupType.smtLookUpDataDisplayMember = "Title";

            LupType.smtLookUpDataSource = dt_Catagory;
            LupType.smtLookUpDataValueMember = "ItemCode";
            LupType.smtLookUpDataDisplayMember = "Title";

            if (dt_Catagory != null
                && dt_Catagory.Rows.Count > 0)
            {
                SearchLupType.smtLookUpValue = "ALL";
            }

            // 사용유무
            DataTable dt = new DataTable();
            dt.Columns.Add("코드", typeof(string));
            dt.Columns.Add("명칭", typeof(string));

            dt.Rows.Add("ALL", "전체");
            dt.Rows.Add("Y", "Y");
            dt.Rows.Add("N", "N");

            SearchLupUseYn.smtLookUpDataSource = dt;
            SearchLupUseYn.smtLookUpDataValueMember = "코드";
            SearchLupUseYn.smtLookUpDataDisplayMember = "명칭";

            DataTable useTable = new DataTable();
            useTable.Columns.Add("코드", typeof(string));
            useTable.Columns.Add("명칭", typeof(string));
            useTable.Rows.Add("Y", "사용");
            useTable.Rows.Add("N", "미사용");
            RdoUseYn.smtRadioGroupDataSource = useTable;
            RdoUseYn.smtRadioGroupDataValueMember = "코드";
            RdoUseYn.smtRadioGroupDataDisplayMember = "명칭";
        }

        private void Set_Btn_Event()
        {
            BtnSearch.Click += SearchButton_Click;
            cmdAdd.Click += AddButton_Click;
            cmdClose.Click += CloseButton_Click;
            BtnSave.Click += Btn_Click;
            BtnEmbedding.Click += Btn_Click;
        }

        #region 그리드

        private void Set_GridView(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            //gv.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //그리드 기분 중앙 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false
            gv.IndicatorWidth = 50;                     //순번 너비
            gv.OptionsView.AllowHtmlDrawHeaders = true;
            gv.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

            RepositoryItemSearchLookUpEdit lup_Category = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "C020", null), "코드", "명칭");

            grid.Create_GvColumn(gv, "No", "No");
            grid.Create_GvColumn(gv, "분류", "Category", lup_Category);
            grid.Create_GvColumn(gv, "제목", "Title");
            grid.Create_GvColumn(gv, "최종수정일", "ModifiedDate");
            grid.Create_GvColumn(gv, "수정자", "ModifiedBy");
            grid.Create_GvColumn(gv, "임베딩상태", "EmbeddingStatus");
            grid.Create_GvColumn(gv, "사용여부", "UseYn");

            grid.Create_GvColumn(gv, "채번PK", "KnowledgeSeq");
            grid.Create_GvColumn(gv, "내용", "Content");
            grid.Create_GvColumn(gv, "컬렉션명", "ChromaCollection");
            grid.Create_GvColumn(gv, "벡터ID", "ChromaDocID");

            gv.Columns["KnowledgeSeq"].Visible = false;
            gv.Columns["Content"].Visible = false;
            gv.Columns["ChromaCollection"].Visible = false;
            gv.Columns["ChromaDocID"].Visible = false;

            // 이벤트
            gv.FocusedRowObjectChanged += Gv_Know_FocusedRowChanged;
        }

        private void Gv_Know_FocusedRowChanged(object sender, FocusedRowObjectChangedEventArgs e)
        {
            DataRow dr = Gv_Know.GetFocusedDataRow();
            if (dr == null)
            {
                return;
            }

            knowledgeSeq = dr["KnowledgeSeq"].ConvertString_Null_To_Empty();
            LupType.smtLookUpValue = dr["Category"].ConvertString_Null_To_Empty();
            TxtTitle.smtTextBoxValue = dr["Title"].ConvertString_Null_To_Empty();
            MemoContent.smtMemoValue = dr["Content"].ConvertString_Null_To_Empty();
            TxtEmbedding.smtTextBoxValue = dr["EmbeddingStatus"].ConvertString_Null_To_Empty();
            RdoUseYn.smtRadioGroupValue = dr["UseYn"].ConvertString_Null_To_Empty();
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
                case "BtnEmbedding": Func_Embedding(); break;
            }
        }

        #endregion

        #region 저장 

        private async void Func_Save()
        {
            if (LupType.smtLookUpValue.ConvertString_Null_To_Empty() == "" ||
                TxtTitle.smtTextBoxValue.ConvertString_Null_To_Empty() == "" ||
                MemoContent.smtMemoValue.ConvertString_Null_To_Empty() == ""
                )
            {
                XtraMessageBox.Show("분류, 제목, 내용을 입력해 주세요.", "입력 확인",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ChatBot_Knowledge_Base knowledgeBase = new ChatBot_Knowledge_Base();

            SetModel(knowledgeBase);
            knowledgeBase.EmbeddingStatus = "대기";

            bool result = string.IsNullOrEmpty(knowledgeSeq)
                ? knowledgeBase.Ins_Knowledege() : knowledgeBase.Upd_Knowledege();
            if (!result)
            {
                XtraMessageBox.Show("저장 중 오류가 발생했습니다.", "저장",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            knowledgeSeq = knowledgeBase.KnowledgeSeq;
            knowledgeBase.ChromaDocID = WrGlobal.DBName + "_" + WrGlobal.CorpID + "_" + knowledgeSeq;
            try
            {
                await UpsertEmbedding(knowledgeBase);
                knowledgeBase.Upd_Knowledege_EmbeddingStatus("완료");
                Func_Search();
                XtraMessageBox.Show("저장 및 임베딩이 완료되었습니다.", "저장",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                knowledgeBase.Upd_Knowledege_EmbeddingStatus("실패");
                Func_Search();
                XtraMessageBox.Show("원문은 저장되었으나 임베딩 중 오류가 발생했습니다.\r\n" + ex.Message,
                    "저장", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion

        #region 삭제 

        private async void Func_Del()
        {
            if (knowledgeSeq.ConvertInt_Null_To_Zero() == 0)
            {
                XtraMessageBox.Show("삭제할 내역을 선택해 주세요.", "삭제", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (XtraMessageBox.Show("선택한 내역을 정말 삭제하시겠습니까?", "삭제 확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            ChatBot_Knowledge_Base knowledgeBase = new ChatBot_Knowledge_Base();
            try
            {
                await DeleteEmbedding();

                SetModel(knowledgeBase);

                if (!knowledgeBase.Del_Knowledege())
                {
                    throw new InvalidOperationException("데이터 삭제에 실패했습니다.");
                }

                Func_Search();
                ClearDetail();
                XtraMessageBox.Show("삭제되었습니다.", "삭제", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("삭제 중 오류가 발생했습니다.\r\n" + ex.Message, "삭제", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region 임베딩 

        private async void Func_Embedding()
        {
            if (knowledgeSeq.ConvertInt_Null_To_Zero() == 0)
            {
                XtraMessageBox.Show("임베딩할 내역을 선택해 주세요.", "임베딩", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ChatBot_Knowledge_Base knowledgeBase = new ChatBot_Knowledge_Base();
            try
            {

                SetModel(knowledgeBase);

                knowledgeBase.Upd_Knowledege_EmbeddingStatus("대기");
                await UpsertEmbedding(knowledgeBase);
                knowledgeBase.Upd_Knowledege_EmbeddingStatus("완료");
                TxtEmbedding.smtTextBoxValue = "완료";
                
                Func_Search();
                
                XtraMessageBox.Show("임베딩이 완료되었습니다.", "임베딩", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                knowledgeBase.Upd_Knowledege_EmbeddingStatus("실패");
                TxtEmbedding.smtTextBoxValue = "실패";
                XtraMessageBox.Show("임베딩 중 오류가 발생했습니다.\r\n" + ex.Message, "임베딩", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            ChatBot_Knowledge_Base knowledgeBase = new ChatBot_Knowledge_Base();
            knowledgeBase.Category = SearchLupType.smtLookUpValue.ConvertString_Null_To_Empty();
            knowledgeBase.Title = SearchTxtTitle.smtTextBoxValue.ConvertString_Null_To_Empty();
            knowledgeBase.UseYn = SearchLupUseYn.smtLookUpValue.ConvertString_Null_To_Empty();

            Gc_Know.DataSource = knowledgeBase.Select_Knowledege();
            Gv_Know.BestFitColumns();
        }

        private void ClearDetail()
        {
            knowledgeSeq = "";
            LupType.smtLookUpValue = "";
            TxtTitle.smtTextBoxValue = "";
            MemoContent.smtMemoValue = "";
            TxtEmbedding.smtTextBoxValue = "대기";
            RdoUseYn.smtRadioGroupValue = "Y";
        }

        #endregion

        #region 모델 데이터 설정

        private ChatBot_Knowledge_Base SetModel(ChatBot_Knowledge_Base knowledgeBase)
        {
            knowledgeBase.CorpID = WrGlobal.CorpID;
            knowledgeBase.KnowledgeSeq = knowledgeSeq;
            knowledgeBase.Category = LupType.smtLookUpValue;
            knowledgeBase.Title = TxtTitle.smtTextBoxValue;
            knowledgeBase.Content = MemoContent.smtMemoValue;
            knowledgeBase.ChromaCollection = ChromaCollectionName;
            knowledgeBase.ChromaDocID = WrGlobal.DBName + "_" + WrGlobal.CorpID + "_" + knowledgeSeq;
            knowledgeBase.UseYn = string.IsNullOrEmpty(RdoUseYn.smtRadioGroupValue)
                ? "Y" : RdoUseYn.smtRadioGroupValue;
            knowledgeBase.ModifiedBy = WrGlobal.LoginID;

            return knowledgeBase;
        }

        #endregion

        #region 챗봇 API 데이터 처리

        private async Task UpsertEmbedding(ChatBot_Knowledge_Base knowledgeBase)
        {
            await chatbotApi.UpsertKnowledge(knowledgeBase);
        }

        private async Task DeleteEmbedding()
        {
            await chatbotApi.DeleteKnowledge(knowledgeSeq);
        }

        #endregion

        #region 챗봇 API 연결

        private async void FrmKnowledgeBase_Load(object sender, EventArgs e)
        {
            bool isConnected = await chatbotApi.CheckConnection();

            Func_Search();
            ClearDetail();

            if (!isConnected)
            {
                XtraMessageBox.Show("챗봇 API에 연결할 수 없습니다.\r\n챗봇 API 실행 상태를 확인해 주세요.",
                    "챗봇 API 연결 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

    }
}
