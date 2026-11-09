using SmartMES.Common;
using SmartMES.View.Lookup;
using System;
using SmartCommon.Common;
using System.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using SmartMES.Models;

namespace SmartMES.View.SM
{
    public partial class Frm_SM_Backup : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();
        Database db = new Database();
        clsCode code = new clsCode();
        string query = "";

        public Frm_SM_Backup()
        {
            InitializeComponent();

            //GridView 생성
            Set_GridView(Gv_BackUp);
        }

        private void Set_GridView(GridView gv)
        {
            //GvLine Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.IndicatorWidth = 50;                     //순번 너비

            //Column 생성
            grid.Create_GvColumn(gv, "백업일자", "BackUp_Date");
            grid.Create_GvColumn(gv, "파일명", "File_Nm");
            grid.Create_GvColumn(gv, "데이터베이스명", "DB_Nm");
            grid.Create_GvColumn(gv, "파일 Path", "File_Path");
            grid.Create_GvColumn(gv, "등록자", "CreId");
            grid.Create_GvColumn(gv, "등록일시", "CreDt");

            //gv.Columns["UseYn"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;

            //gv.Columns["CreId"].Visible = false;
            //gv.Columns["CreDt"].Visible = false;

            //셀값 정렬
            //gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;     //상태

            //이벤트 설정
            //gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
            //gv.RowStyle += grid.RowStyle_ChangeBg_ByStatus;           //삭제시 배경색 지정
            //gv.CellValueChanged += Event_CellValueChanged;            //셀값 수정시 상태값 수정
            //gv.ValidatingEditor += Event_ValidatingEditor;            //코드 입력시 중복 확인
        }

        private void Frm_SM_Backup_Load(object sender, EventArgs e)
        {
            Func_Search();
        }
        private void Func_Search()
        {
            query = $@"SELECT CONVERT(VARCHAR, CONVERT(DATETIME, BackUp_Date), 23) AS BackUp_Date,
                              File_Nm,
                              File_Path, 
                              DB_Nm,
                              Idx,

                              CreId,
                              CreIP,
                              FORMAT(CreDt, 'yyyy-MM-dd HH:mm') AS CreDt,
                              ModId,
                              ModIP,
                              FORMAT(ModDt, 'yyyy-MM-dd HH:mm') AS ModDt

                         FROM SM_Backup
                         order by BackUp_Date DESC";

            Gc_BackUp.DataSource = db.GetDataView("리스트", query);

            Gv_BackUp.BestFitColumns();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            Frm_LookUpDBBackup form = new Frm_LookUpDBBackup();
            form.ShowDialog();
            Func_Search();
        }

		private void btnDelete_Click(object sender, EventArgs e)
		{
            DataRow dr = Gv_BackUp.GetFocusedDataRow();
            if (dr == null) return;

            query = $@"
            DELETE SM_Backup WHERE Idx = '{dr["Idx"]}'
            ";

            db.ExecuteQuery(query);

            Func_Search();
            XtraMessageBox.Show(cls_MessageBox.Del_Comp);
        }

		private void btnUpdate_Click(object sender, EventArgs e)
		{
            DataRow dr = Gv_BackUp.GetFocusedDataRow();
            if (dr == null) return;

            Frm_LookUpDBBackup form = new Frm_LookUpDBBackup(dr);
            form.ShowDialog();
            Func_Search();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}