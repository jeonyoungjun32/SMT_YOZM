using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using System.Data;
using System.Drawing;
using DevExpress.XtraGrid.Views.Grid;
using System.Windows.Forms;
using DevExpress.Utils;

namespace SmartCommon.Common
{
    public class clsRepository
    {
        #region LookupEdit

        /// <summary>
        /// Repository LookupEdit ComboBox 형태로 생성
        /// </summary>
        /// <param name="dt">입력할 DataTable</param>
        /// <param name="str_Value">Value로 가질 Table Column명</param>
        /// <param name="str_Disp">화면에 표기할 Table Column명</param>
        public RepositoryItemLookUpEdit Create_RepositoryItem(DataTable dt, string str_Value, string str_Disp)
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

        public RepositoryItemSearchLookUpEdit Create_RepositoryItemSearchLookUp(DataTable dt, string str_Value, string str_Disp)
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
        public void Set_POP_SearchLup(SearchLookUpEdit lup)
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

		public void Set_POP_SearchLup(RepositoryItemSearchLookUpEdit lup)
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
        }

        #endregion LookupEdit End

        /// <summary>
        /// Repository CheckBox 생성, 체크값 : "Y", 미체크값 : "N"
        /// </summary>
        /// <returns></returns>
        public RepositoryItemCheckEdit Create_rpoChk()
        {
            RepositoryItemCheckEdit rpoChk = new RepositoryItemCheckEdit();
            rpoChk.AllowGrayed = false;
            rpoChk.ValueChecked = "Y";
            rpoChk.ValueUnchecked = "N";
            rpoChk.NullStyle = StyleIndeterminate.Unchecked;

            return rpoChk;
        }

        /// <summary>
        /// Repository CheckBox 생성, 체크값 :checkValue, 미체크값 : unCheckValue
        /// </summary>
        /// <returns></returns>
        public RepositoryItemCheckEdit Create_rpoChk(string checkValue, string unCheckValue)
        {
            RepositoryItemCheckEdit rpoChk = new RepositoryItemCheckEdit();
            rpoChk.AllowGrayed = false;
            rpoChk.ValueChecked = checkValue;
            rpoChk.ValueUnchecked = unCheckValue;
            rpoChk.NullStyle = StyleIndeterminate.Unchecked;

            return rpoChk;
        }

        /// <summary>
        /// Repository TextBox 숫자만 입력
        /// </summary>
        /// <returns></returns>
        public RepositoryItemTextEdit Create_rpoTxt_Numeric()
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
        public RepositoryItemTextEdit Create_rpoTxt_Length(int maxLength)
        {
            RepositoryItemTextEdit rpoTxt = new RepositoryItemTextEdit();
            rpoTxt.MaxLength = maxLength;

            return rpoTxt;
        }

        /// <summary>
        /// Repository ToggleSwitch 기본 셋팅
        /// </summary>
        /// <returns></returns>
        public RepositoryItemToggleSwitch Create_rpoToggle()
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
        /// Repository PictureEdit 기본셋팅
        /// </summary>
        /// <returns></returns>
        public RepositoryItemPictureEdit Create_rpoPic(PictureSizeMode sizeMode = PictureSizeMode.Zoom)
        {
            RepositoryItemPictureEdit rpo = new RepositoryItemPictureEdit();

            rpo.PictureStoreMode = PictureStoreMode.ByteArray;
            rpo.ShowCameraMenuItem = CameraMenuItemVisibility.Auto;
            rpo.SizeMode = sizeMode;
            rpo.PictureAlignment = System.Drawing.ContentAlignment.MiddleCenter;

            return rpo;
        }

        /// <summary>
        /// Repository DateEdit 기본셋팅
        /// </summary>
        /// <returns></returns>
        public RepositoryItemDateEdit Create_rpoDate(string formatString = "yyyy-MM-dd")
        {
            RepositoryItemDateEdit rpo = new RepositoryItemDateEdit();

            // 1. 마스크 설정 (입력 편의성 및 포맷 지정)
            rpo.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            rpo.Mask.EditMask = formatString;
            rpo.Mask.UseMaskAsDisplayFormat = true; // 입력 마스크를 화면 표시용으로도 동일하게 사용

            // 2. 디스플레이 및 편집 포맷 명시적 설정 (안전장치)
            rpo.DisplayFormat.FormatType = FormatType.DateTime;
            rpo.DisplayFormat.FormatString = formatString;

            rpo.EditFormat.FormatType = FormatType.DateTime;
            rpo.EditFormat.FormatString = formatString;

            return rpo;
        }

        public void Set_Dtp(DateEdit dtp)
        {
            //날짜데이터 입력 편의성
            dtp.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
        }

        /// <summary>
        /// POP 달력 크기 설정
        /// </summary>
        /// <returns></returns>
        public void Set_POP_Dtp(DateEdit dtp)
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

        public RepositoryItemTimeSpanEdit Create_TimeSpan(string format)
        {
            //날짜데이터 입력 편의성
            RepositoryItemTimeSpanEdit rpo = new RepositoryItemTimeSpanEdit();
            rpo.AllowEditDays = false;  //날짜수정 불가능처리
            rpo.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;    //":"따로 입력 안해도 되도록 처리
            rpo.Mask.EditMask = format;
            rpo.Mask.UseMaskAsDisplayFormat = true;

            return rpo;
        }

        public RepositoryItemTimeEdit Create_TimeEdit()
        {
            //날짜데이터 입력 편의성
            RepositoryItemTimeEdit timeEdit = new RepositoryItemTimeEdit();
            timeEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;    //":"따로 입력 안해도 되도록 처리
            timeEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            timeEdit.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm";
            timeEdit.Mask.EditMask = "yyyy-MM-dd HH:mm";
            timeEdit.TimeEditStyle = TimeEditStyle.TouchUI;

            return timeEdit;
        }

        public void Set_TimeSpan(TimeSpanEdit ts, string format)
        {
            ts.Properties.AllowEditDays = false;  //날짜수정 불가능처리
            ts.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;    //":"따로 입력 안해도 되도록 처리
            ts.Properties.Mask.EditMask = format;
            ts.Properties.Mask.UseMaskAsDisplayFormat = true;
        }

        public RepositoryItemMemoEdit Create_rpoMemo()
        {
            RepositoryItemMemoEdit rpo = new RepositoryItemMemoEdit();

            return rpo;
        }

        public RepositoryItemCalcEdit Create_rpoCalc()
        {
            RepositoryItemCalcEdit rpo = new RepositoryItemCalcEdit();
            rpo.ValidateOnEnterKey = true;

            return rpo;
        }

        public RepositoryItemButtonEdit Create_rpoBtn(ButtonPredefines kind = ButtonPredefines.Right)
        {
            EditorButton btn = new EditorButton();
            btn.Kind = kind;

            RepositoryItemButtonEdit rpoBtn = new RepositoryItemButtonEdit();
            rpoBtn.TextEditStyle = TextEditStyles.HideTextEditor;
            rpoBtn.Buttons.RemoveAt(0);
            rpoBtn.Buttons.Add(btn);

            return rpoBtn;
        }
    }
}
