using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Models;
using System;
using System.Data;
using System.Windows.Forms;

namespace SmartMES.View.IF
{
    public partial class Frm_IF_DGPView : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();
        clsCode code = new clsCode();
        Database db = new Database();

        Timer CurTimer = new Timer();
        Timer SearchTimer = new Timer();

        public Frm_IF_DGPView()
        {
            InitializeComponent();

            Set_Design();

            // 현재시간 셋팅
            Set_Cur();

            // 조회
            Display_List();
        }

        #region  디자인
        private void Set_Design()
        {
            // 타이머 설정
            Set_Timer();

            // 그리드
            Set_GvMaterial(GvDGP);
        }

        private void Set_Timer()
        {
            // 우측 상단 현재시간 타이머
            Set_CurTime(CurTimer);

            // 조회 타이머
            Set_Search(SearchTimer);
        }

        private void Set_CurTime(Timer timer)
        {
            timer.Interval = 1000;      // 1초마다
            timer.Tick += CurTime;

            timer.Start();
        }


        private void Set_Search(Timer timer)
        {
            timer.Interval = 5000;      // 5초마다
            timer.Tick += SearchTick;

            timer.Start();
        }

        private void CurTime(object sender, EventArgs e)
        {
            Set_Cur();
        }

        private void SearchTick(object sender, EventArgs e)
        {
            Display_Update();
        }


        private void Set_Cur()
        {
            LblCurTime.Text = DateTime.Now.ToString("yyyy-MM-dd dddd HH:mm:ss");
        }

        #region 그리드 디자인
        private void Set_GvMaterial(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.RowAutoHeight = true;
            gv.OptionsView.ColumnAutoWidth = true; //gv.BestFitColumns() 쓸 경우 false
            gv.OptionsView.AllowCellMerge = true;   //같은값인 셀 합치기 허용

            //gv.Appearance.HeaderPanel.Font = new Font("Pretendard SemiBold", 18, FontStyle.Bold);   //gridview 폰트 조정
            //gv.Appearance.Row.Font = new Font("Pretendard SemiBold", 18, FontStyle.Regular);        //gridview 폰트 조정

            RepositoryItemSearchLookUpEdit lup_Item = rpo.Create_RepositoryItemSearchLookUp(code.GetMaterialList(), "품목코드", "품명");

            grid.Create_GvColumn(gv, "항목코드", "DgpCode");
            //grid.Create_GvColumn(gv, "설비분류", "DgpSpec");
            //grid.Create_GvColumn(gv, "설비코드", "Equip_Name");
            grid.Create_GvColumn(gv, "항목명", "DgpName");
            grid.Create_GvColumn(gv, "항목타입", "Gauge_Type");
            grid.Create_GvColumn(gv, "현재값", "Counter");
            grid.Create_GvColumn(gv, "갱신시간", "LastTime");
            grid.Create_GvColumn(gv, "사용여부", "Value_Or");

            grid.SetCol_Type_POP(gv.Columns["Counter"], clsGrid.TypeList.TFloat);

            gv.Columns["DgpName"].Width = 120;
            //gv.Columns["DgpCode"].Visible = false;
            gv.Columns["Value_Or"].Visible = false;

            gv.Columns["Counter"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            gv.Columns["LastTime"].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

            gv.CellMerge += Gv_CellMerge;
        }
        #endregion 그리드 디자인

        #endregion

        #region 조회
        private void Display_List()
        {
            string query = $@"SELECT
                    M.DgpCode,
                    M.Gauge_Type,
                    M.DgpSpec,
                    M.Equip_Name,
                    M.DgpName,
                    D.Counter,
                    convert(varchar,D.LastTime,120) as LastTime,
                    M.Value_Or
                    FROM IF_DGPCode M
                    LEFT JOIN 
                    IF_DGPCounter D
                    ON M.DgpCode = D.DgpCode
                    ORDER BY DgpCode
                    ";

            GcDGP.DataSource = db.GetDataTable(query);
        }
        private void Display_Update()
        {
            string query = $@"SELECT
                    M.DgpCode,
                    M.Gauge_Type,
                    M.DgpSpec,
                    M.Equip_Name,
                    M.DgpName,
                    D.Counter,
                    convert(varchar,D.LastTime,120) as LastTime
                    FROM IF_DGPCode M
                    LEFT JOIN 
                    IF_DGPCounter D
                    ON M.DgpCode = D.DgpCode
                    ORDER BY DgpCode
                    ";

            DataTable dt = db.GetDataTable(query);

            for (int i = 0; i < GvDGP.DataRowCount; i++)
            {
                DataRow row = GvDGP.GetDataRow(i);
                string DgpCode = row["DgpCode"].ToString();

                DataRow[] rowQ = dt.Select($"DgpCode = '{DgpCode}'");
                if (rowQ.Length > 0)
                {
                    int Counter = 0;
                    int.TryParse(rowQ[0]["Counter"].ToString(), out Counter);
                    string LastTime = rowQ[0]["LastTime"] == DBNull.Value ? "" : rowQ[0]["LastTime"].ToString();

                    row["Counter"] = Counter;
                    row["LastTime"] = LastTime;
                }
            }
        }
        #endregion

        private void frmOrderStatusView_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 폼닫힐 때 타이머 종료
            CurTimer.Stop();
            SearchTimer.Stop();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Gv_CellMerge(object sender, CellMergeEventArgs e)
        {
            //셀 병합시 예외처리
            GridView gv = sender as GridView;
            string fieldNm = e.Column.FieldName;

            if (fieldNm == "Gauge_Type")
            {
                string[] compareList = { fieldNm };  //비교할 FieldName 목록
                bool mergeYn = true;

                //데이터리스트 비교
                for (int idx = 0; idx < compareList.Length; idx++)
                {
                    string upData = Convert.ToString(gv.GetRowCellValue(e.RowHandle1, compareList[idx]));   //상단데이터
                    string downData = Convert.ToString(gv.GetRowCellValue(e.RowHandle2, compareList[idx])); //하단데이터

                    //데이터 비교
                    if (upData != downData) { mergeYn = false; break; } //다르면 Merge 안함처리

                    if (Database.DbName == "DOOIL" || Database.DbName == "DOOIL_G")     // 두일 불량 수량 때문에
                    {
                        upData = Convert.ToString(gv.GetRowCellValue(e.RowHandle1, "Value_Or"));   //상단데이터
                        downData = Convert.ToString(gv.GetRowCellValue(e.RowHandle2, "Value_Or")); //하단데이터

                        if (upData != downData) { mergeYn = false; break; } //다르면 Merge 안함처리
                    }
                }

                e.Merge = mergeYn;  //최종 적용 여부
                e.Handled = true;   //뭔지는 모르겠는데 안해주면 적용 안됨
            }
        }
    }
}