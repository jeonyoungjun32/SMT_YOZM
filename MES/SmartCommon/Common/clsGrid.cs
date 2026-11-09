using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using baseSMT;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using SmartMES.Models;

namespace SmartCommon.Common
{
    public class clsGrid
    {
        #region 디자인 관련

        /// <summary>
        /// GridView Column 생성
        /// </summary>
        /// <param name="gv">입력할 GridView</param>
        /// <param name="caption">표기명칭</param>
        /// <param name="fieldName">연동데이터 Column명칭</param>
        /// <param name="repository">Repository Item(ComboBox, Lookup 등)</param>
        public void Create_GvColumn(GridView gv, string caption, string fieldName, RepositoryItem repository = null, bool allowEidt = false)
        {
            GridColumn col = new GridColumn();
            col.Caption = caption;          //표기명
            col.Name = fieldName;           //Column명
            col.FieldName = fieldName;      //연동 데이터 Column명
            col.ColumnEdit = repository;    //Repository Item(ComboBox, Lookup 등)
            col.Visible = true;             //화면에 보이기

            col.OptionsColumn.AllowEdit = allowEidt;   //유저가 수정 허용여부

            if (allowEidt) Column_AllowEdit(col);

            gv.Columns.Add(col);            //GridView에 추가
        }

        /// <summary>
        /// GridView Unbound Column 생성
        /// </summary>
        /// <param name="gv">입력할 GridView</param>
        /// <param name="caption">표기명칭</param>
        /// <param name="fieldName">Unbound Column명칭</param>
        /// <param name="dataType">결과 데이터 타입</param>
        /// <param name="expression">계산식</param>
        /// <param name="formatType">표시 형식</param>
        /// <param name="formatString">표시 포맷</param>
        /// <param name="repository">Repository Item</param>
        /// 
        public GridColumn Create_GvUnboundColumn(GridView gv,string caption,string fieldName,Type dataType,string expression,FormatType formatType = FormatType.None,string formatString = "",RepositoryItem repository = null)
        {
            Create_GvColumn(
                gv,
                caption,
                fieldName,
                repository,
                false);

            GridColumn col = gv.Columns[fieldName];

            col.UnboundDataType = dataType;
            col.UnboundExpression = expression;

            col.OptionsColumn.AllowEdit = false;
            col.ShowUnboundExpressionMenu = false;

            if (formatType != FormatType.None)
            {
                col.DisplayFormat.FormatType = formatType;
            }

            if (string.IsNullOrWhiteSpace(formatString) == false)
            {
                col.DisplayFormat.FormatString = formatString;
            }

            return col;
        }

        //S-MES 에서 추가
        public void Create_GvColumn_ByOption(GridView gv, string div_Cd)
        {
            if (WrGlobal.CdOption.ContainsKey(div_Cd))
            {
                int colCnt = gv.Columns.Count;

                if (gv.OptionsSelection.MultiSelectMode == GridMultiSelectMode.CheckBoxRowSelect)
                {
                    colCnt += 1;
                }

                foreach (string fieldNm in WrGlobal.CdOption[div_Cd].Keys)
                {
                    WrGlobal.CodeOption option = WrGlobal.CdOption[div_Cd][fieldNm];

                    GridColumn col = new GridColumn();

                    //표기명
                    col.Caption = option.Nm;
                    //연동 데이터 Column명
                    col.FieldName = fieldNm;
                    //화면에 보이기
                    col.Visible = option.DispYn;
                    //유저가 수정 허용여부
                    col.OptionsColumn.AllowEdit = option.LockYn;
                    col.OptionsColumn.AllowFocus = option.LockYn;

                    gv.Columns.Add(col);            //GridView에 추가

                    //컬럼 순서 설정
                    gv.Columns[fieldNm].AbsoluteIndex = (colCnt - 1) + option.DispSeq;

                    //Add후에 넣어야 적용되는 작업들
                    //수정허용 시 작업
                    if (option.LockYn)
                    {
                        col.AppearanceCell.BackColor = WrGlobal.EditBgColor;    //Column 배경색
                    }
                }
            }
        }

        /// <summary>
        /// GridColumn 변경 허용
        /// </summary>
        /// <param name="col">적용할 Column</param>
        public void Column_AllowEdit(GridColumn col, bool edit = true)
        {
            if (col == null) return;
            col.OptionsColumn.AllowEdit = edit;
            col.OptionsColumn.AllowFocus = edit;
            if (edit)
            {
                col.AppearanceCell.BackColor = smtDesignSetting.MustColor;  //Column 배경색
            }
            else
            {
                //col.AppearanceCell.BackColor = smtDesignSetting.ReadOnlyColor;  //Column 배경색
                col.OptionsColumn.AllowFocus = false;
            }
        }

        public void BandedColumn_AllowEdit(BandedGridColumn col, bool edit = true)
        {
            if (col == null) return;
            col.OptionsColumn.AllowEdit = edit;
            col.OptionsColumn.AllowFocus = edit;
            if (edit)
            {
                //col.AppearanceCell.BackColor = WrGlobal.EditBgColor;    //Column 배경색
                col.AppearanceCell.BackColor = Color.Bisque;
            }
            else
            {
                col.AppearanceCell.BackColor = Color.White;    //Column 배경색
                col.OptionsColumn.AllowFocus = false;
            }
        }

        public void Set_Navigator(GridView gv)
        {
            GridControl gc = gv.GridControl;

            gv.OptionsView.ShowIndicator = true;
            gc.UseEmbeddedNavigator = true;
            gc.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;

            gc.EmbeddedNavigator.Buttons.Append.Visible = false;
            gc.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gc.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gc.EmbeddedNavigator.Buttons.Remove.Enabled = false;
        }

        public void SetCol_Summary(GridView gv, string fieldNm, SummaryItemType type = SummaryItemType.Sum, string format = "{0:N0}")
        {
            //Summary
            gv.OptionsView.ShowFooter = true;

            //해당 컬럼 없을 경우 RETURN
            if (gv.Columns.ColumnByFieldName(fieldNm) == null) return;

            gv.Columns[fieldNm].Summary.Add(type);
            gv.Columns[fieldNm].SummaryItem.DisplayFormat = format; //N0: 정수
        }

        public void SetGroup_Summary(GridView gv, string fieldNm, SummaryItemType type = SummaryItemType.Sum, string format = "{0:N0}")
        {
            gv.GroupFooterShowMode = GroupFooterShowMode.VisibleAlways;

            if (gv.Columns.ColumnByFieldName(fieldNm) == null)
                return;

            GridGroupSummaryItem item = new GridGroupSummaryItem
            {
                FieldName = fieldNm,
                SummaryType = type,
                DisplayFormat = format,
                ShowInGroupColumnFooter = gv.Columns[fieldNm]
            };

            gv.GroupSummary.Add(item);
        }

        public enum TypeList
        {
            TInt,
            TFloat,
            TFloat_Hide0,
            TFloatN1,
            TFloatN2,
            TFloatN3,
            TDate,
            TDateTime,
            TTime,
        }

        public void SetCol_Type(GridColumn col, TypeList type)
        {
            if (col == null) return;
            switch (type)
            {
                case TypeList.TInt:
                    col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;

                    //표기타입
                    col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    col.DisplayFormat.FormatString = "#,##0";
                    break;
                case TypeList.TFloat:
                    col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;

                    //표기타입
                    col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    col.DisplayFormat.FormatString = "#,#0.####";

                    break;

                case TypeList.TFloat_Hide0:
                    col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;

                    //표기타입
                    col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    col.DisplayFormat.FormatString = "#,#.##";
                    break;

                case TypeList.TFloatN1:
                    col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    //표기타입
                    col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    col.DisplayFormat.FormatString = "n1";
                    break;

                case TypeList.TFloatN2:
                    col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    //표기타입
                    col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    col.DisplayFormat.FormatString = "n2";
                    break;

                case TypeList.TFloatN3:
                    col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    //표기타입
                    col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    col.DisplayFormat.FormatString = "n3";
                    break;

                case TypeList.TDate:
                    col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    break;

                case TypeList.TDateTime:
                    col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                    //표기타입
                    col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    col.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
                    break;

                case TypeList.TTime:
                    col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                    //표기타입
                    col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    col.DisplayFormat.FormatString = "HH:mm";
                    break;
            }
        }

        public void SetCol_Type_POP(GridColumn col, TypeList type)
        {
            if (col == null) return;

            switch (type)
            {
                case TypeList.TInt:
                    col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;

                    //표기타입
                    col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    col.DisplayFormat.FormatString = "#,##0";
                    break;
                case TypeList.TFloat:
                    col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;

                    //표기타입
                    col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    col.DisplayFormat.FormatString = "#,##0.##";
                    break;
                case TypeList.TDate:
                    col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;

                    // 달력의 폰트 사이즈 변경
                    DevExpress.XtraEditors.Repository.RepositoryItemDateEdit CalItem = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();

                    CalItem.CellSize = new Size(60, 50);

                    //달력 날짜 크기 키우기
                    int C_FontSize = 8;
                    CalItem.AppearanceCalendar.DayCellHoliday.FontSizeDelta = C_FontSize;
                    CalItem.AppearanceCalendar.DayCell.FontSizeDelta = C_FontSize;
                    CalItem.AppearanceCalendar.DayCellHighlighted.FontSizeDelta = C_FontSize;
                    CalItem.AppearanceCalendar.DayCellSelected.FontSizeDelta = C_FontSize;
                    CalItem.AppearanceCalendar.DayCellPressed.FontSizeDelta = C_FontSize;
                    CalItem.AppearanceCalendar.DayCellToday.FontSizeDelta = C_FontSize;
                    CalItem.AppearanceCalendar.DayCellToday.BackColor = Color.LightGray;
                    CalItem.AppearanceCalendar.Header.FontSizeDelta = C_FontSize;
                    CalItem.AppearanceCalendar.Header.ForeColor = Color.Black;
                    CalItem.AppearanceCalendar.HeaderPressed.FontSizeDelta = C_FontSize;
                    CalItem.AppearanceCalendar.HeaderHighlighted.FontSizeDelta = C_FontSize;

                    //적용
                    col.ColumnEdit = CalItem;

                    break;
                case TypeList.TDateTime:
                    col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                    //표기타입
                    col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    col.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
                    break;
            }
        }


        public void Create_Band(BandedGridView gv, string name, string caption, string parentBand = null)
        {
            GridBand gridBand = new GridBand();
            gridBand.Name = name; //밴드명
            gv.Bands.Add(gridBand); //gv에 밴드 넣기
            gridBand.Caption = caption == "" ? null : caption; //화면에보이는 밴드 텍스트

            if (parentBand != null) // 부모밴드 매개변수가 있으면 해당 밴드 자식밴드로 추가
            {
                gv.Bands[parentBand].Children.Add(gridBand);
            }

            gridBand.MinWidth = 100;
            gridBand.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        }

        public void Create_BandedColumn(BandedGridView gv, string name, string caption, string fieldName, string parentBandNm = null, string childBandNm = null)
        {
            BandedGridColumn bandCol = new BandedGridColumn();
            bandCol.Name = name;
            bandCol.Caption = caption;
            bandCol.FieldName = fieldName;
            bandCol.Visible = true;
            bandCol.OptionsColumn.AllowEdit = false;
            gv.Columns.Add(bandCol);
            if (parentBandNm != null && childBandNm != null)
            { // 자식밴드 안에 넣을 때
                gv.Bands[parentBandNm].Children[childBandNm].Columns.Add(bandCol);
                gv.Columns[fieldName].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            }
            else if (parentBandNm != null)
            { // 단일 밴드에 넣을 때
                gv.Bands[parentBandNm].Columns.Add(bandCol);
                gv.Columns[fieldName].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            }
            else
            {
                return;
            }
        }

        public void SetBandedCol_Type(BandedGridColumn col, TypeList type)
        {
            if (col == null) return;
            switch (type)
            {
                case TypeList.TInt:
                    col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;

                    //표기타입
                    col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    col.DisplayFormat.FormatString = "#,##0";
                    break;
                case TypeList.TFloat:
                    col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;

                    //표기타입
                    col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    col.DisplayFormat.FormatString = "#,##0.##";
                    break;
                case TypeList.TDate:
                    col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    break;

                case TypeList.TDateTime:
                    col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                    //표기타입
                    col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    col.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
                    break;
            }
        }

        public void SetPOPGrid(GridView gv)
        {
            //Grid 크기 조정
            gv.Appearance.HeaderPanel.Font = new System.Drawing.Font("Pretendard SemiBold", 18F);
            gv.Appearance.Row.Font = new System.Drawing.Font("Pretendard SemiBold", 18F);
            gv.Appearance.HeaderPanel.Options.UseFont = true;
            gv.Appearance.Row.Options.UseFont = true;
            gv.DetailHeight = 150;
            gv.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gv.OptionsSelection.EnableAppearanceFocusedCell = false;
            gv.OptionsView.ShowGroupPanel = false;
        }

        #endregion 디자인 관련 End

        #region Event 관련

        /// <summary>
        /// 셀값 입력시 중복값 검사(ValidatingEditor Event용)
        /// </summary>
        /// <param name="gv">검사 할 GridView</param>
        /// <param name="e">ValidatingEditor의 EventArgs</param>
        /// <param name="fieldName">검사 할 Column FieldName</param>
        public void Check_OverLap_CellValue(GridView gv, BaseContainerValidateEditorEventArgs e, string fieldName)
        {
            if (gv.FocusedColumn.FieldName == fieldName)
            {
                //입력된 데이터값과 일치하는 Data수 검사
                if (Convert.ToString(gv.GetFocusedRowCellValue(gv.Columns[0].FieldName)) == "신규")
                {
                    int cnt = (gv.GridControl.DataSource as DataTable).Select($" {fieldName} = '{Convert.ToString(e.Value)}'").Count();

                    //1개 이상이면 입력 불가
                    if (cnt > 0)
                    {
                        e.Valid = false;    //입력 불가 처리
                        e.ErrorText = $"중복된 {gv.FocusedColumn.Caption}이(가) 있습니다.";  //경고 알림
                    }
                }
                else
                {
                    e.Valid = false;
                    e.ErrorText = $"저장된 {gv.FocusedColumn.Caption}은/는 수정할 수 없습니다.";
                }
            }
        }

        public void Check_Minus_CellValue(GridView gv, BaseContainerValidateEditorEventArgs e, string fieldName)
        {
            if (gv.FocusedColumn.FieldName == fieldName)
            {
                if (Convert.ToSingle(e.Value) < 0)
                {
                    e.Valid = false;    //입력 불가 처리
                    e.ErrorText = $"0보다 작을 수 없습니다.";  //경고 알림
                }
            }
        }

        /// <summary>
        /// 셀값 수정 시 상태값 변경(CellValueChanged Event용) / 가장 좌측 Column이 상태Column이여야함
        /// </summary>
        /// <param name="gv"></param>
        /// <param name="rowHandle"></param>
        public void Change_Status(GridView gv, int rowHandle)
        {
            //셀값 수정 시 상태값 수정
            DataTable dt = gv.GridControl.DataSource as DataTable;
            string str_Status = Convert.ToString(gv.GetFocusedRowCellValue(dt.Columns[0].Caption));    //상태 값

            if (str_Status != "신규" &&
                str_Status != "신규(일괄)" &&
                str_Status != "수정" &&
                str_Status != "삭제")
            {
                gv.SetRowCellValue(rowHandle, gv.Columns[0].FieldName, "수정");
            }
        }

        #region 행 삽입, 추가 삭제

        /// <summary>
        /// 행삽입, 추가, 삭제 프로세스
        /// </summary>
        /// <param name="gc">이벤트가 발생될 GridControl</param>
        /// <param name="keys">입력 키</param>
        public void Set_Row(GridControl gc, Dictionary<string, object> dicData, string keys)
        {
            GridView gv = gc.MainView as GridView;
            try
            {
                switch (keys)
                {
                    case "Ins": Add_Row(gv, dicData, 'U'); break;  //행삽입
                    case "Add": Add_Row(gv, dicData, 'D'); break;  //행추가
                    case "Del": Del_Row(gv, dicData); break;       //행삭제
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Gridview Row 삽입
        /// </summary>
        /// <param name="gv">입력될 GridView</param>
        /// <param name="dicData">Row 기본값으로 입력될 데이터</param>
        /// <param name="pos">입력 위치 선택행 기준 / U : 상단입력, D : 하단입력</param>
        private void Add_Row(GridView gv, Dictionary<string, object> dicData, char pos)
        {
            //Row 데이터 하단 추가
            //1. 데이터 입력 준비
            DataTable dt = gv.GridControl.DataSource as DataTable;
            if (dt == null) return;
            int selRowCount = gv.SelectedRowsCount == 0 ? 1 : gv.SelectedRowsCount; //선택된 행 수 = 입력할 행 수

            //2. 입력 위치 설정
            int Ins_Pos = gv.FocusedRowHandle < 0 ? 0 : gv.FocusedRowHandle;
            if (pos == 'D') Ins_Pos++;  //하단입력일 경우 + 1

            //2. 기본입력 데이터 설정
            for (int idx = 0; idx < selRowCount; idx++)
            {
                DataRow dr = dt.NewRow();

                foreach (string key in dicData.Keys)
                {
                    dr[key] = dicData[key];
                }

                dt.Rows.InsertAt(dr, Ins_Pos);
            }

            //최상단행에서 행삽입 시
            if (pos == 'U' && Ins_Pos == 0)
            {
                gv.TopRowIndex = 0;
            }

            gv.FocusedRowHandle = Ins_Pos;
        }

        private void Del_Row(GridView gv, Dictionary<string, object> dicData)
        {
            //선택행 삭제 표기
            //0. Dictionary 입력 검사(개발자용)
            if (dicData.Count <= 0)
            {
                MessageBox.Show("Dictionary[Del] 데이터를 추가하지 않았습니다.");
                return;
            }

            //1. 데이터 입력 준비
            DataTable dt = gv.GridControl.DataSource as DataTable;
            int selRowCount = gv.SelectedRowsCount; //선택된 행 수 = 입력할 행 수
            int[] selRow = gv.GetSelectedRows();

            //2. 입력 데이터 설정
            for (int idx = 0; idx < selRowCount; idx++)
            {
                foreach (string key in dicData.Keys)
                {
                    if ((key == "Status" || key == "DispSeq") &&
                         Convert.ToString(gv.GetRowCellValue(selRow[idx], key)) == "신규")
                    {
                        //상태행이 신규인경우 해당 행 삭제
                        gv.DeleteRow(selRow[idx]);

                        idx--;
                        selRowCount--;
                        break;
                    }
                    else if ((key == "Status" || key == "DispSeq") &&
                         Convert.ToString(gv.GetRowCellValue(selRow[idx], key)) == "삭제")
                    {
                        //상태행이 삭제인경우 삭제표기 취소
                        gv.SetRowCellValue(selRow[idx], key, "");
                        break;
                    }
                    else
                    {
                        gv.SetRowCellValue(selRow[idx], key, dicData[key]);
                    }
                }
            }

            gv.SelectRow(gv.FocusedRowHandle);
        }

        #endregion 행 삽입, 추가, 삭제 End

        public void RowStyle_ChangeBg_ByStatus(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;

            //삭제시 배경색 지정
            GridView gv = sender as GridView;
            if (gv == null) return;

            DataRow dr = gv.GetDataRow(e.RowHandle);
            if (dr == null) return;

            string str_Status = Convert.ToString(dr["Status"]);
            Color delBgColor = Color.LightGray; //배경색

            if (str_Status == "삭제")
            {
                e.Appearance.BackColor = delBgColor;
            }
        }

        public void RowStyle_DataExistsYn(GridView gv, RowStyleEventArgs e, string fieldName)
        {
            //검사항목 데이터가 있으면 'Y', 없으면 'N'으로하고
            //데이터가 없으면 회색 표기
            if (e.RowHandle > -1 && gv.GetRowCellValue(e.RowHandle, fieldName).ToString() == "N")
            {
                e.Appearance.BackColor = Color.Gray;
            }
        }

        /// <summary>
        /// Form닫을때 저장내역 검사
        /// </summary>
        /// <param name="gv">입력할 GridView</param>
        /// <param name="fieldName">상태값 Column FieldName</param>
        /// <returns></returns>
        public bool Check_Gv_FormClosing(GridView gv, string fieldName)
        {
            try
            {
                //true : 종료방지, false : 정상종료
                DataTable dt = gv.GridControl.DataSource as DataTable;
                if (dt == null) return false;
                bool writeFlag = false;

                //1. 저장하지 않은 내역있는지 확인
                foreach (DataRow dr in dt.Rows)
                {
                    if (Convert.ToString(dr[fieldName]) == "신규" ||
                        Convert.ToString(dr[fieldName]) == "수정" ||
                        Convert.ToString(dr[fieldName]) == "삭제")
                    {
                        writeFlag = true;
                        break;
                    }
                }

                //2. 사용자 선택
                if (writeFlag)
                {
                    if (MessageBox.Show("저장하지 않은 내역이 있습니다.\n종료 하시겠습니까?", "Check_Save", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return true;
                    }
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public void RowNumber_Indicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            //GridView Indicator(좌측 현재행 표기)에 순번 표기하기
            GridView gv = sender as GridView;
            //데이터 없음 방지
            if (sender == null || gv.RowCount <= 0) return;

            //상단 Column 표시명
            //상단 Column 표시명
            if (e.RowHandle == GridControl.InvalidRowHandle) e.Info.DisplayText = "순번";
            else if (e.RowHandle == GridControl.AutoFilterRowHandle) e.Info.DisplayText = "";
            else if (e.RowHandle == GridControl.NewItemRowHandle) e.Info.DisplayText = "";
            else if (e.Info.IsRowIndicator) e.Info.DisplayText = (e.RowHandle + 1).ToString();

            //디자인
            e.Info.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //정렬
        }

        public void RowNumber_Indicator(GridView gv)
        {
            if (gv == null) return;

            gv.IndicatorWidth = 50;

            gv.CustomDrawRowIndicator += new RowIndicatorCustomDrawEventHandler((sender, e) => {
                //데이터 없음 방지
                if (gv.RowCount <= 0) return;

                //상단 Column 표시명
                if (e.RowHandle == GridControl.InvalidRowHandle) e.Info.DisplayText = "순번";
                else if (e.RowHandle == GridControl.AutoFilterRowHandle) e.Info.DisplayText = "";
                else if (e.RowHandle == GridControl.NewItemRowHandle) e.Info.DisplayText = "";
                else if (e.Info.IsRowIndicator) e.Info.DisplayText = (e.RowHandle + 1).ToString();

                //디자인
                e.Info.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //정렬
            });
        }

        /// <summary>
        /// RepositoryLookup "품명" 선택 시 해당 품목정보 다른 Column에 뿌리기
        /// </summary>
        /// <param name="drChanged">적용할 DataRow</param>
        /// <param name="ItemCode">선택된 품목코드</param>
        public void SetInfo_Item(DataRow drChanged, string ItemCode)
        {
            try
            {
                if (drChanged == null) return;

                DataRow drItem = null;  //불러올 품목정보Row

                clsCode code = new clsCode();
                drItem = code.GetMaterialInfo(ItemCode).Row;

                if (drItem == null || ItemCode == "")
                {
                    drChanged["ItemCode"] = null;
                    drChanged["ItemName"] = null;
                    drChanged["Alias"] = null;
                    if (drChanged.Table.Columns.Contains("ItemClass1")) { drChanged["ItemClass1"] = null; }
                    if (drChanged.Table.Columns.Contains("Size")) { drChanged["Size"] = null; }
                    if (drChanged.Table.Columns.Contains("Standard")) { drChanged["Standard"] = null; }
                    if (drChanged.Table.Columns.Contains("UnitWeight")) { drChanged["UnitWeight"] = null; }
                    //if (drChanged.Table.Columns.Contains("UnitCode")) { drChanged["UnitCode"] = null; }
                    //if (drChanged.Table.Columns.Contains("Grade")) { drChanged["Grade"] = null; }
                    //if (drChanged.Table.Columns.Contains("Base_Weight")) { drChanged["Base_Weight"] = null; }
                    //if (drChanged.Table.Columns.Contains("Color")) { drChanged["Color"] = null; }
                    //if (drChanged.Table.Columns.Contains("Stock_Cd")) { drChanged["Stock_Cd"] = null; }
                    //if (drChanged.Table.Columns.Contains("Vendor_Cd")) { drChanged["Vendor_Cd"] = null; }
                    //if (drChanged.Table.Columns.Contains("Type")) { drChanged["Type"] = null; }
                    //if (drChanged.Table.Columns.Contains("ShortName")) { drChanged["ShortName"] = null; }
                }
                else
                {
                    drChanged["ItemCode"] = drItem["ItemCode"];
                    drChanged["Alias"] = drItem["Alias"];

                    //공통
                    if (drChanged.Table.Columns.Contains("ItemClass1")) { drChanged["ItemClass1"] = drItem["ItemClass1"]; }
                    if (drChanged.Table.Columns.Contains("Size")) { drChanged["Size"] = drItem["Size"]; }
                    if (drChanged.Table.Columns.Contains("UnitCode")) { drChanged["UnitCode"] = drItem["UnitCode"]; }
                    if (drChanged.Table.Columns.Contains("Standard")) { drChanged["Standard"] = drItem["Standard"]; }
                    if (drChanged.Table.Columns.Contains("UnitWeight")) { drChanged["UnitWeight"] = drItem["UnitWeight"]; }
                    if (drChanged.Table.Columns.Contains("Grade")) { drChanged["Grade"] = drItem["Grade"]; }
                    if (drChanged.Table.Columns.Contains("Base_Weight")) { drChanged["Base_Weight"] = drItem["Base_Weight"]; }
                    if (drChanged.Table.Columns.Contains("Color")) { drChanged["Color"] = drItem["Color"]; }
                    if (drChanged.Table.Columns.Contains("Stock_Cd")) { drChanged["Stock_Cd"] = drItem["Stock_Cd"]; }
                    if (drChanged.Table.Columns.Contains("Vendor_Cd")) { drChanged["Vendor_Cd"] = drItem["Vendor_Cd"]; }
                    if (drChanged.Table.Columns.Contains("Type")) { drChanged["Type"] = drItem["Type"]; }
                    if (drChanged.Table.Columns.Contains("ShortName")) { drChanged["ShortName"] = drItem["ShortName"]; }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        public void SetInfo_Mold(DataRow drChanged, string MoldCode)
        {
            try
            {
                if (drChanged == null) return;

                DataRow drItem = null;  //불러올 품목정보Row

                clsCode code = new clsCode();
                drItem = code.GetMoldnfo(MoldCode).Row;

                if (drItem == null || MoldCode == "")
                {
                    drChanged["MoldCode"] = null;
                    drChanged["MoldName"] = null;
                    if (drChanged.Table.Columns.Contains("MoldMgmtNo")) { drChanged["MoldMgmtNo"] = null; }
                    if (drChanged.Table.Columns.Contains("InitShot")) { drChanged["InitShot"] = null; }
                    if (drChanged.Table.Columns.Contains("MoldSN")) { drChanged["MoldSN"] = null; }
                    if (drChanged.Table.Columns.Contains("Chasu")) { drChanged["Chasu"] = null; }
                    if (drChanged.Table.Columns.Contains("MoldCavity")) { drChanged["MoldCavity"] = null; }
                    if (drChanged.Table.Columns.Contains("ProdtCust")) { drChanged["ProdtCust"] = null; }
                    if (drChanged.Table.Columns.Contains("SupplyCust")) { drChanged["SupplyCust"] = null; }
                    if (drChanged.Table.Columns.Contains("MoldPjtNo")) { drChanged["MoldPjtNo"] = null; }
                }
                else
                {
                    drChanged["MoldCode"] = drItem["MoldCode"];

                    //공통
                    if (drChanged.Table.Columns.Contains("MoldMgmtNo")) { drChanged["MoldMgmtNo"] = drItem["MoldMgmtNo"]; }
                    if (drChanged.Table.Columns.Contains("InitShot")) { drChanged["InitShot"] = drItem["InitShot"]; }
                    if (drChanged.Table.Columns.Contains("MoldSN")) { drChanged["MoldSN"] = drItem["MoldSN"]; }
                    if (drChanged.Table.Columns.Contains("Chasu")) { drChanged["Chasu"] = drItem["Chasu"]; }
                    if (drChanged.Table.Columns.Contains("MoldCavity")) { drChanged["MoldCavity"] = drItem["MoldCavity"]; }
                    if (drChanged.Table.Columns.Contains("ProdtCust")) { drChanged["ProdtCust"] = drItem["ProdtCust"]; }
                    if (drChanged.Table.Columns.Contains("CustCode")) { drChanged["CustCode"] = drItem["SupplyCust"]; }
                    if (drChanged.Table.Columns.Contains("SupplyCust")) { drChanged["SupplyCust"] = drItem["SupplyCust"]; }
                    if (drChanged.Table.Columns.Contains("MoldPjtNo")) { drChanged["MoldPjtNo"] = drItem["MoldPjtNo"]; }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        ///// <summary>
        ///// Repository LookupEdit의 PopUp에해당하는 목록이 품목일 때 초기화 옵션 적용
        ///// </summary>
        ///// <param name="lup"></param>
        ///// <param name="task_Div"></param>
        public void SetLupView_Item(RepositoryItemSearchLookUpEdit lup, string task_Div)
        {
            lup.PopulateViewColumns();
            lup.View.OptionsView.ColumnAutoWidth = false;   //Column Width 조절용
            lup.View.OptionsView.RowAutoHeight = true;      //MultiLine

            lup.BestFitMode = BestFitMode.BestFitResizePopup;          //Column Width 조절용

            //모든 PopupColumn 검색
            foreach (GridColumn col in lup.View.Columns)
            {
                bool colExists = false;

                //초기화옵션 검색
                foreach (string fieldNm in WrGlobal.CdOption[task_Div].Keys)
                {
                    if (col.FieldName != fieldNm) continue; //Popup의 FieldName과 CdOption의 FieldName이 다르면 다음 CdOption 검색

                    //FieldName과 초기화옵션의 FieldName이 같으면 PopUp Column의 Caption 조정
                    col.Caption = WrGlobal.CdOption[task_Div][fieldNm].Nm;
                    col.Visible = WrGlobal.CdOption[task_Div][fieldNm].DispYn;

                    colExists = true;
                }

                //Column이 초기화옵션에 없으면 숨기기
                if (!colExists)
                {
                    col.Visible = false;
                }
            }


            //규격 멀티라인 옵션 적용
            if (WrGlobal.ProcOption["BS"].ContainsKey("Item_Size_MultiLineYN"))
            {
                if (WrGlobal.ProcOption["BS"]["Item_Size_MultiLineYN"] == "Y")
                {
                    GridColumn colSize = lup.View.Columns.ColumnByFieldName("Size");

                    if (colSize != null)
                    {
                        RepositoryItemMemoEdit memoEdit = new RepositoryItemMemoEdit();
                        lup.RepositoryItems.AddRange(new RepositoryItem[] { memoEdit });

                        colSize.ColumnEdit = memoEdit;
                        colSize.MinWidth = 100;
                    }
                }
            }

            lup.View.BestFitColumns();
        }

        public void SetLupView_Item(SearchLookUpEdit lup, string task_Div)
        {
            lup.ForceInitialize();                   //Datasource 적용(안해주면 visible 안먹힘)
            lup.Properties.PopulateViewColumns();
            lup.Properties.View.OptionsView.ColumnAutoWidth = false;
            lup.Properties.View.OptionsView.RowAutoHeight = true;                 //MultiLine
            lup.Properties.BestFitMode = BestFitMode.BestFitResizePopup;          //Column Width 조절용

            //모든 PopupColumn 검색
            foreach (GridColumn col in lup.Properties.View.Columns)
            {
                bool colExists = false;

                //초기화옵션 검색
                foreach (string fieldNm in WrGlobal.CdOption[task_Div].Keys)
                {
                    if (col.FieldName != fieldNm) continue; //Popup의 FieldName과 CdOption의 FieldName이 다르면 다음 CdOption 검색

                    //FieldName과 초기화옵션의 FieldName이 같으면 PopUp Column의 Caption 조정
                    col.Caption = WrGlobal.CdOption[task_Div][fieldNm].Nm;
                    col.Visible = WrGlobal.CdOption[task_Div][fieldNm].DispYn;
                    colExists = true;
                }

                //Column이 초기화옵션에 없으면 숨기기
                if (!colExists)
                {
                    col.Visible = false;
                }
            }

            //규격 멀티라인 옵션
            if (WrGlobal.ProcOption["BS"].ContainsKey("Item_Size_MultiLineYN"))
            {
                if (WrGlobal.ProcOption["BS"]["Item_Size_MultiLineYN"] == "Y")
                {
                    GridColumn colSize = lup.Properties.View.Columns.ColumnByFieldName("Size");

                    if (colSize != null)
                    {
                        RepositoryItemMemoEdit memoEdit = new RepositoryItemMemoEdit();
                        colSize.ColumnEdit = memoEdit;
                        colSize.MinWidth = 100;
                    }
                }
            }

        }

        #endregion
    }
}
