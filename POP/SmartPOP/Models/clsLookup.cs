using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartPOP.Models
{
    class cls_Lookup
    {

        #region Lookup -> ComboBox 형태

        /// <summary>
        /// LookUpEidt ComboBox 형태
        /// </summary>
        /// <param name="lup">ComboBox형태로 바꿀 LookUpEdit</param>
        /// <param name="dt">입력할 DataTable</param>
        /// <param name="str_Value">Value로 가질 Table Column명</param>
        /// <param name="str_Disp">화면에 표기할 Table Column명</param>
        internal void SetLup_Combo(LookUpEdit lup, DataTable dt, string str_Value, string str_Disp)
        {
            //Repository LookUpEdit 생성
            RepositoryItemLookUpEdit lupData = lup.Properties;
            lupData.DataSource = dt;     //공통코드 리스트
            lupData.ValueMember = str_Value;             //실제값
            lupData.DisplayMember = str_Disp;            //표시값

            lupData.NullText = "";                       //빈값 그냥 빈칸으로 나오도록 수정
            lupData.ForceInitialize();                   //Datasource 적용(안해주면 visible 안먹힘)
            lupData.PopulateColumns();                   //Datasource 적용(안해주면 visible 안먹힘)
            lupData.Columns[str_Value].Visible = false;  //코드 숨김
            lupData.ShowHeader = false;                  //Column Header 숨김
        }

        /// <summary>
        /// Repository LookupEdit ComboBox 형태로 생성
        /// </summary>
        /// <param name="dt">입력할 DataTable</param>
        /// <param name="str_Value">Value로 가질 Table Column명</param>
        /// <param name="str_Disp">화면에 표기할 Table Column명</param>
        internal RepositoryItemLookUpEdit Create_RepositoryItem(DataTable dt, string str_Value, string str_Disp)
        {
            //Repository LookUpEdit 생성
            RepositoryItemLookUpEdit rpoLup = new RepositoryItemLookUpEdit();
            rpoLup.DataSource = dt;     //공통코드 리스트
            rpoLup.ValueMember = str_Value;             //실제값
            rpoLup.DisplayMember = str_Disp;            //표시값

            rpoLup.NullText = "";                       //빈값 그냥 빈칸으로 나오도록 수정
            rpoLup.PopulateColumns();                   //Datasource 적용(안해주면 visible 안먹힘)
            rpoLup.Columns[str_Value].Visible = false;  //코드 숨김
            rpoLup.ShowHeader = false;                  //Column Header 숨김

            return rpoLup;
        }

        #endregion Lookup -> ComboBox 형태 End

        #region RepositorySearchLookUp 형태
        internal void Create_RepositoryItemSearchLookUp(SearchLookUpEdit lup, DataTable dt, string str_Value, string str_Disp)
        {
            //Repository LookUpEdit 생성
            RepositoryItemSearchLookUpEdit lupData = lup.Properties;
            lupData.DataSource = dt;     //공통코드 리스트
            lupData.ValueMember = str_Value;               //실제값
            lupData.DisplayMember = str_Disp;             //표시값

            lupData.NullText = "";                       //빈값 그냥 빈칸으로 나오도록 수정
        }

        internal void Create_ItemAdd_RepositoryItemSearchLookUp(SearchLookUpEdit lup, DataTable dt, string str_Value, string str_Disp)
        {
            //Repository LookUpEdit 생성
            RepositoryItemSearchLookUpEdit lupData = lup.Properties;
            lupData.DataSource = dt;     //공통코드 리스트
            lupData.ValueMember = str_Value;               //실제값
            lupData.DisplayMember = str_Disp;             //표시값

            lupData.NullText = "";                       //빈값 그냥 빈칸으로 나오도록 수정

            lupData.ShowAddNewButton = true;
            lupData.View.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            lupData.View.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditForm;
            lupData.View.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;

            lupData.PopulateViewColumns();

            lupData.View.OptionsEditForm.EditFormColumnCount = 1;
            lupData.View.OptionsEditForm.PopupEditFormWidth = 400;
        }

        internal RepositoryItemSearchLookUpEdit Create_RepositoryItemSearchLookUp(DataTable dt, string str_Value, string str_Disp)
        {
            //Repository SearchLookUpEdit 생성
            RepositoryItemSearchLookUpEdit rpoLup = new RepositoryItemSearchLookUpEdit();
            rpoLup.DataSource = dt;             //공통코드 리스트
            rpoLup.ValueMember = str_Value;     //실제값
            rpoLup.DisplayMember = str_Disp;    //표시값

            rpoLup.NullText = "";   //빈값 그냥 빈칸으로 나오도록 수정

            return rpoLup;
        }

        internal RepositoryItemSearchLookUpEdit Create_ItemAdd_RepositoryItemSearchLookUp(DataTable dt, string str_Value, string str_Disp)
        {
            //Repository SearchLookUpEdit 생성
            RepositoryItemSearchLookUpEdit rpoLup = new RepositoryItemSearchLookUpEdit();
            rpoLup.DataSource = dt;             //공통코드 리스트
            rpoLup.ValueMember = str_Value;     //실제값
            rpoLup.DisplayMember = str_Disp;    //표시값

            rpoLup.NullText = "";   //빈값 그냥 빈칸으로 나오도록 수정

            rpoLup.ShowAddNewButton = true;
            rpoLup.View.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            rpoLup.View.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditForm;
            rpoLup.View.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDown;

            rpoLup.PopulateViewColumns();

            rpoLup.View.OptionsEditForm.EditFormColumnCount = 1;
            rpoLup.View.OptionsEditForm.PopupEditFormWidth = 400;

            return rpoLup;
        }

        internal void Create_RepositoryItemSearchLookUpDefault(SearchLookUpEdit lup, DataTable dt, string str_Value, string str_Disp)
        {
            RepositoryItemSearchLookUpEdit lupData = lup.Properties;
            lupData.DataSource = dt;                        //공통코드 리스트
            lupData.ValueMember = str_Value;                //코드값
            lupData.DisplayMember = str_Disp;               //표시값

            lupData.NullText = "";                       //빈값 그냥 빈칸으로 나오도록 수정

            if (dt != null)
            {
                if (dt.Rows.Count == 1)
                {
                    lup.EditValue = dt.Rows[0][str_Value].ToString();
                }
            }
        }
        #endregion RepositorySearchLookUp 형태 END

        /// <summary>
        /// POP 달력 크기 설정
        /// </summary>
        /// <returns></returns>
        internal void Set_POP_Dtp(DateEdit dtp)
        {
            //달력 디자인 변경
            //클릭이벤트 추가(화살표가 아닌 다른영역을 클릭해도 팝업활성화)
            dtp.MouseDown += (sender, e) => DateEdit_MouseDown(sender, e);
            dtp.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            //달력 팝업 크기 키우기
            dtp.Properties.CellSize = new Size(70, 60);
            //달력 날짜 크기 키우기
            int size = 8;
            dtp.Properties.AppearanceCalendar.DayCellHoliday.FontSizeDelta = size;
            dtp.Properties.AppearanceCalendar.DayCell.FontSizeDelta = size;
            dtp.Properties.AppearanceCalendar.DayCellHighlighted.FontSizeDelta = size;
            dtp.Properties.AppearanceCalendar.DayCellSelected.FontSizeDelta = size;
            dtp.Properties.AppearanceCalendar.DayCellPressed.FontSizeDelta = size;
            dtp.Properties.AppearanceCalendar.DayCellToday.FontSizeDelta = size;
            dtp.Properties.AppearanceCalendar.DayCellToday.BackColor = Color.LightGray;
            dtp.Properties.AppearanceCalendar.Header.FontSizeDelta = size;
            dtp.Properties.AppearanceCalendar.Header.ForeColor = Color.Black;
            dtp.Properties.AppearanceCalendar.HeaderPressed.FontSizeDelta = size;
            dtp.Properties.AppearanceCalendar.HeaderHighlighted.FontSizeDelta = size;

            //날짜데이터 입력 편의성
            dtp.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
        }

        private void DateEdit_MouseDown(object sender, MouseEventArgs e)
        {
            DateEdit dtp = sender as DateEdit;
            if (dtp == null) return;

            // MouseDown 이벤트가 발생한 위치가 화살표 버튼이 아닌 다른 UI인 경우
            if (e.Button == MouseButtons.Left && e.X > dtp.Width - dtp.Properties.Buttons[0].Width)
            {
                // 팝업 창 띄우기
                dtp.ShowPopup();
            }
        }

        internal void Set_POPSize_Lup(SearchLookUpEdit lup)
        {
            lup.Properties.PopupFormSize = new Size(700, 500);

            //서치룩업 크기 조정
            GridView gv = lup.Properties.View as GridView;
            if (gv == null) return;

            gv.Appearance.HeaderPanel.Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Bold);
            gv.Appearance.HeaderPanel.Options.UseFont = true;
            gv.Appearance.Row.Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Bold);
            gv.Appearance.Row.Options.UseFont = true;
            gv.DetailHeight = 400;
            gv.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gv.OptionsSelection.EnableAppearanceFocusedCell = false;
            gv.OptionsView.ShowGroupPanel = false;
        }
    }
}
