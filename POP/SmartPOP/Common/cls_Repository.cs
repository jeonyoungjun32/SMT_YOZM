
using baseSMT;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartPOP.Common
{
    class cls_Repository
    {
        #region LookupEdit

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

        internal RepositoryItemSearchLookUpEdit Create_RepositoryItemSearchLookUp(DataTable dt, string str_Value, string str_Disp, string div)
        {
            //Repository SearchLookUpEdit 생성
            RepositoryItemSearchLookUpEdit rpoLup = new RepositoryItemSearchLookUpEdit();
            rpoLup.DataSource = dt;             //공통코드 리스트
            rpoLup.ValueMember = str_Value;     //실제값
            rpoLup.DisplayMember = str_Disp;    //표시값

            rpoLup.NullText = "";   //빈값 그냥 빈칸으로 나오도록 수정

            return rpoLup;
        }


        /// <summary>
        /// POP SearchLook 크기 설정
        /// </summary>
        /// <returns></returns>
        internal void Set_POP_SearchLup(SearchLookUpEdit lup)
        {
            lup.Properties.PopupFormSize = new Size(700, 500);

            //서치룩업 크기 조정
            GridView gv = lup.Properties.View;
            if (gv == null) return;

            gv.Appearance.HeaderPanel.Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Bold);
            gv.Appearance.HeaderPanel.Options.UseFont = true;
            gv.Appearance.Row.Font = new System.Drawing.Font("맑은 고딕", 20F, System.Drawing.FontStyle.Bold);
            gv.Appearance.Row.Options.UseFont = true;
            gv.DetailHeight = 300;
            gv.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gv.OptionsSelection.EnableAppearanceFocusedCell = false;
            gv.OptionsView.ShowGroupPanel = false;
        }

        internal void Set_POP_SearchLup(RepositoryItemSearchLookUpEdit lup)
        {
            //셀 내부 서치룩업 크기 조정
            GridView gv = lup.View;
            if (gv == null) return;

            gv.Appearance.HeaderPanel.Font = new System.Drawing.Font("맑은 고딕", 20F);
            gv.Appearance.HeaderPanel.Options.UseFont = true;
            gv.Appearance.Row.Font = new System.Drawing.Font("맑은 고딕", 20F);
            gv.Appearance.Row.Options.UseFont = true;
            gv.DetailHeight = 300;
            gv.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gv.OptionsSelection.EnableAppearanceFocusedCell = false;
            gv.OptionsView.ShowGroupPanel = false;
            lup.PopupFormSize = new Size(700, 500);
        }

        #endregion LookupEdit End

        /// <summary>
        /// Repository CheckBox 생성, 체크값 : "Y", 미체크값 : "N"
        /// </summary>
        /// <returns></returns>
        internal RepositoryItemCheckEdit Create_rpoChk()
        {
            RepositoryItemCheckEdit rpoChk = new RepositoryItemCheckEdit();
            rpoChk.AllowGrayed = false;
            rpoChk.ValueChecked = "Y";
            rpoChk.ValueUnchecked = "N";
            rpoChk.NullStyle = StyleIndeterminate.Unchecked;

            return rpoChk;
        }

        /// <summary>
        /// Repository TextBox 숫자만 입력
        /// </summary>
        /// <returns></returns>
        internal RepositoryItemTextEdit Create_rpoTxt_Numeric()
        {
            RepositoryItemTextEdit rpoTxt = new RepositoryItemTextEdit();
            rpoTxt.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;

            return rpoTxt;
        }

        /// <summary>
        /// Repository TextBox 최대 글자수 지정
        /// </summary>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        internal RepositoryItemTextEdit Create_rpoTxt_Length(int maxLength)
        {
            RepositoryItemTextEdit rpoTxt = new RepositoryItemTextEdit();
            rpoTxt.MaxLength = maxLength;

            return rpoTxt;
        }

        /// <summary>
        /// Repository ToggleSwitch 기본 셋팅
        /// </summary>
        /// <returns></returns>
        internal RepositoryItemToggleSwitch Create_rpoToggle()
        {
            RepositoryItemToggleSwitch rpo = new RepositoryItemToggleSwitch();
            rpo.ValueOff = "N";
            rpo.ValueOn = "Y";

            rpo.ShowText = true;
            rpo.OffText = "N";
            rpo.OnText = "Y";

            return rpo;
        }

        /// <summary>
        /// Repository DateEdit 기본셋팅
        /// </summary>
        /// <returns></returns>
        internal RepositoryItemDateEdit Create_rpoDate()
        {
            //날짜데이터 입력 편의성
            RepositoryItemDateEdit rpo = new RepositoryItemDateEdit();
            rpo.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;

            return rpo;
        }

        internal void Set_Dtp(DateEdit dtp)
        {
            //날짜데이터 입력 편의성
            dtp.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
        }

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

            //Clear버튼 숨겨서 빈값 방지
            dtp.Properties.ShowClear = false;  
        }

        internal void Set_POP_Dtp(RepositoryItemDateEdit dtp)
        {
            //달력 디자인 변경
            //클릭이벤트 추가(화살표가 아닌 다른영역을 클릭해도 팝업활성화)
            dtp.MouseDown += (sender, e) => DateEdit_MouseDown(sender, e);
            dtp.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            //달력 팝업 크기 키우기
            dtp.Properties.CellSize = new Size(70, 60);
            //달력 날짜 크기 키우기
            int size = 8;
            dtp.AppearanceCalendar.DayCellHoliday.FontSizeDelta = size;
            dtp.AppearanceCalendar.DayCell.FontSizeDelta = size;
            dtp.AppearanceCalendar.DayCellHighlighted.FontSizeDelta = size;
            dtp.AppearanceCalendar.DayCellSelected.FontSizeDelta = size;
            dtp.AppearanceCalendar.DayCellPressed.FontSizeDelta = size;
            dtp.AppearanceCalendar.DayCellToday.FontSizeDelta = size;
            dtp.AppearanceCalendar.DayCellToday.BackColor = Color.LightGray;
            dtp.AppearanceCalendar.Header.FontSizeDelta = size;
            dtp.AppearanceCalendar.Header.ForeColor = Color.Black;
            dtp.AppearanceCalendar.HeaderPressed.FontSizeDelta = size;
            dtp.AppearanceCalendar.HeaderHighlighted.FontSizeDelta = size;

            //날짜데이터 입력 편의성
            dtp.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;

            //Clear버튼 숨겨서 빈값 방지
            dtp.ShowClear = false;
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

        internal RepositoryItemTimeSpanEdit Create_TimeSpan(string format)
        {
            //날짜데이터 입력 편의성
            RepositoryItemTimeSpanEdit rpo = new RepositoryItemTimeSpanEdit();
            rpo.AllowEditDays = false;  //날짜수정 불가능처리
            rpo.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;    //":"따로 입력 안해도 되도록 처리
            rpo.Mask.EditMask = format;
            rpo.Mask.UseMaskAsDisplayFormat = true;

            return rpo;
        }

        internal void Set_TimeSpan(TimeSpanEdit ts, string format)
        {
            ts.Properties.AllowEditDays = false;  //날짜수정 불가능처리
            ts.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;    //":"따로 입력 안해도 되도록 처리
            ts.Properties.Mask.EditMask = format;
            ts.Properties.Mask.UseMaskAsDisplayFormat = true;
        }

        internal RepositoryItemMemoEdit Create_rpoMemo()
        {
            RepositoryItemMemoEdit rpo = new RepositoryItemMemoEdit();

            return rpo;
        }

        internal RepositoryItemCalcEdit Create_rpoCalc()
        {
            RepositoryItemCalcEdit rpo = new RepositoryItemCalcEdit();
            rpo.ValidateOnEnterKey = true;

            return rpo;
        }
    }

}
