using baseSMT;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Model.EQ;
using SmartMES.Models;
using SmartMES.Models.CM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SmartMES.View.EQ
{
    public partial class Frm_EQ_MachineChkStd : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();
        clsCode code = new clsCode();

        RepositoryItemTextEdit rpoTxt = new RepositoryItemTextEdit();   //검사항목 Repository 숫자입력
        RepositoryItemCheckEdit rpoChk = new RepositoryItemCheckEdit(); //검사항목 Repository CheckBox

        string ChkDiv = "";

        public Frm_EQ_MachineChkStd()
        {
            InitializeComponent();
        }

        public Frm_EQ_MachineChkStd(string div)
        {
            InitializeComponent();

            this.ChkDiv = div;
        }

        private void Frm_EQ_MachineChkStd_Load(object sender, EventArgs e)
        {
            SetDesign();

            Func_Search();
        }

        #region 디자인 아이템 생성

        private void SetDesign()
        {
            //GridView 데이터 입력
            Set_GridView(Gv_Machine_Chk_Std);

            //검사구분 데이터 입력
            Set_Machine_Chk_Std_Rdo(Rdo_Machine_Div);

            //폼 닫을때 검사 작성된 내역 있는지 검사
            this.FormClosing += new FormClosingEventHandler((sender, e) => {
                e.Cancel = false;
                if (grid.Check_Gv_FormClosing(Gv_Machine_Chk_Std, "Status"))
                {
                    e.Cancel = true;
                }
            });
        }

        private void Set_Machine_Chk_Std_Rdo(RadioGroup rdo)
        {
            //Item 생성
            clsRadioGroup clsRadio = new clsRadioGroup();
            CM_CateItem cc = new CM_CateItem();

            DataTable dt = cc.SelectClassItemLookUp(WrGlobal.CorpID, "2040", this.ChkDiv == "" ? null : this.ChkDiv);

            clsRadio.Create_RadioGroupItem(rdo, dt, "분류명", "분류코드");

            rdo.SelectedIndex = 0;  //초기값
        }

        #region GridView Setting

        private void Set_GridView(GridView gv)
        {
            //Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.IndicatorWidth = 50;                     //순번 너비

            //그리드 룩업
            CommonClass cl = new CommonClass();
            CM_CateItem item = new CM_CateItem();

            RepositoryItem lup_Inspect_Div = rpo.Create_RepositoryItem(item.SelectClassItemLookUp(WrGlobal.CorpID, "2040"), "분류코드", "분류명");
            RepositoryItem lup_Cycle_Div = rpo.Create_RepositoryItem(item.SelectClassItemLookUp(WrGlobal.CorpID, "204050"), "분류코드", "분류명") ;
            RepositoryItem lup_Week_Div = rpo.Create_RepositoryItem(item.SelectClassItemLookUp(WrGlobal.CorpID, "204060"), "분류코드", "분류명") ;
            RepositoryItem lup_ValueType = rpo.Create_RepositoryItem(item.SelectClassItemLookUp(WrGlobal.CorpID, "204070"), "분류코드", "분류명") ;
            //이미지 버튼
            EditorButton Btn_Img_Move = new EditorButton();
            Btn_Img_Move.Kind = ButtonPredefines.Right;
            RepositoryItemButtonEdit Btn_Img = new RepositoryItemButtonEdit();
            Btn_Img.TextEditStyle = TextEditStyles.HideTextEditor;  //Text입력 삭제
            Btn_Img.Buttons.RemoveAt(0);
            Btn_Img.Buttons.Add(Btn_Img_Move);
            groupControl3.Visible = false;
            layoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            //gv.RowClick += Event_IMG_Click;    //버튼 클릭 이벤트로 하면 grid focused를 할수없어서 gv클릭이벤트로 변경

            //Column 생성
            grid.Create_GvColumn(gv, "상태",          "Status");
            grid.Create_GvColumn(gv, "점검구분", "InspectDivCd", lup_Inspect_Div);
            grid.Create_GvColumn(gv, "설비코드", "MachineCode");
            grid.Create_GvColumn(gv, "설비명", "MachineName");
            grid.Create_GvColumn(gv, "저장순번", "Seq");
            grid.Create_GvColumn(gv, "표시순번", "DispSeq");
            grid.Create_GvColumn(gv, "관리항목", "ManagementItem");
            grid.Create_GvColumn(gv, "확인방법", "CheckMethod");
            grid.Create_GvColumn(gv, "점검기준", "StandardSpec");
            grid.Create_GvColumn(gv, "관리주기", "CycleDivCd", lup_Cycle_Div);

            grid.Create_GvColumn(gv, "관리요일", "WeekDivCd", lup_Week_Div);
            grid.Create_GvColumn(gv, "설비값 관리 형태", "ValueTypeDivCd", lup_ValueType);
            //grid.Create_GvColumn(gv, "이미지", "Chk_Image", Btn_Img);
            grid.Create_GvColumn(gv, "기본값", "InspectOk");
            grid.Create_GvColumn(gv, "측정수치Min", "InspectMin");
            grid.Create_GvColumn(gv, "측정수치Max", "InspectMax");
            grid.Create_GvColumn(gv, "비고", "Remark");
            grid.Create_GvColumn(gv, "등록자", "CreIP");
            grid.Create_GvColumn(gv, "등록일시", "CreDt");
            grid.Create_GvColumn(gv, "수정자", "ModId");
            grid.Create_GvColumn(gv, "수정일시", "ModDt");

            //Column 단일 설정
            //셀값 변경 허용
            grid.Column_AllowEdit(gv.Columns["InspectDivCd"]);     //점검구분
            grid.Column_AllowEdit(gv.Columns["MachineName"]);    //설비명
            grid.Column_AllowEdit(gv.Columns["ManagementItem"]);    //관리항목
            grid.Column_AllowEdit(gv.Columns["CheckMethod"]);       //확인방법
            grid.Column_AllowEdit(gv.Columns["StandardSpec"]);      //점검기준
            grid.Column_AllowEdit(gv.Columns["CycleDivCd"]);       //관리주기
            grid.Column_AllowEdit(gv.Columns["WeekDivCd"]);        //관리요일
            grid.Column_AllowEdit(gv.Columns["ValueTypeDivCd"]);   //등록 형태
            //grid.Column_AllowEdit(gv.Columns["Chk_Image"]);                //비고
            grid.Column_AllowEdit(gv.Columns["InspectOk"]);         //기본값
            grid.Column_AllowEdit(gv.Columns["InspectMin"]);        //측정수치Min
            grid.Column_AllowEdit(gv.Columns["InspectMax"]);        //측정수치Max
            grid.Column_AllowEdit(gv.Columns["Remark"]);                //비고


            //Repository 설정
            rpoChk = rpo.Create_rpoChk();
            rpoTxt = rpo.Create_rpoTxt_Numeric();
            //사용여부
            RepositoryItemCheckEdit chk_Use = rpo.Create_rpoChk();
            //Repository 생성 or 이벤트 입력
            //설비명
            RepositoryItemSearchLookUpEdit lup_MachineCode = rpo.Create_RepositoryItemSearchLookUp(code.GetMachineList(false), "설비코드", "설비명");
            gv.Columns["MachineName"].ColumnEdit = lup_MachineCode;

            //셀값 정렬
            grid.SetCol_Type(gv.Columns["InspectMin"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["InspectMax"], clsGrid.TypeList.TFloat);

            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;     //상태
            //gv.Columns["DecimalCnt"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;   //소수점 자리수

            //이벤트 설정
            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
            gv.CellValueChanged += Event_CellValueChanged;            //셀값 수정시 상태값 수정
            gv.GridControl.ProcessGridKey += Event_ProcessGridKey;    //행추가, 삽입, 삭제 단축키기능 부여
            gv.CustomRowCellEdit += Event_GvCustomRowCellEditForEditing;    //등록형태 값에 따른 기본값 입력 실

            //검사구분 라디오 버튼 변경시 재조회
            Rdo_Machine_Div.SelectedIndexChanged += new EventHandler(Rdo_Machine_Div_SelectedIndexChanged);

            gv.Columns["MachineCode"].Visible = false;
            gv.Columns["Seq"].Visible = false;
            gv.Columns["DispSeq"].Visible = false;
        }

        private void Event_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;

            //셀값 수정 시 상태값 수정
            if (e.Column.FieldName == gv.Columns[0].FieldName) return;
            grid.Change_Status(gv, e.RowHandle);

            //설비명 lookup 선택시 설비코드 자동 삽입
            if (e.Column.FieldName == "MachineName")
            {
                DataRow dr = gv.GetDataRow(e.RowHandle);

                dr["MachineCode"] = dr["MachineName"];
            }

            //등록형태 변경시 기본값 초기화
            //if (e.Column.FieldName == gv.Columns["ValueTypeDivCd"].FieldName)
            //{
            //    DataRow dr = gv.GetDataRow(e.RowHandle);
            //    dr["InspectOk"] = "";
            //}

            gv.BestFitColumns();
        }

        #region 행 삽입, 추가, 삭제

        private void Event_ProcessGridKey(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case (Keys.Alt | Keys.I): cmd_Click(cmdIns, null); break;  //행삽입
                case (Keys.Alt | Keys.D): cmd_Click(cmdDel, null); ; break; //행삭제
            }
        }

        #endregion 행 삽입, 추가, 삭제 End

        private void Event_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            //셀값 입력시 중복 검사
            GridView gv = sender as GridView;
            grid.Check_OverLap_CellValue(gv, e, "MachineCode");
            Check_SameMachineNm(gv, e);
        }

        private void Check_SameMachineNm(GridView gv, BaseContainerValidateEditorEventArgs e)
        {
            string ColName = gv.FocusedColumn.FieldName;

            if (ColName == "MachineName")
            {
                string InspectDivCd = Convert.ToString(gv.GetFocusedRowCellValue("InspectDivCd"));

                if (InspectDivCd == "ALL")
                {
                    e.Valid = false;    //입력 불가 처리
                    e.ErrorText = $"검사구분 먼저 선택이 필요합니다.";  //경고 알림

                    return;
                }

                for (int idx = 0; idx < gv.RowCount; idx++)
                {
                    DataRow dr = gv.GetDataRow(idx);

                    if(Convert.ToString(dr["InspectDivCd"]) == InspectDivCd
                        && Convert.ToString(dr["MachineName"]) == Convert.ToString(e.Value))
                    {
                        e.Valid = false;    //입력 불가 처리
                        e.ErrorText = $"중복된 {gv.FocusedColumn.Caption}이(가) 있습니다.";  //경고 알림
                        break;
                    }
                }
            }
        }
        private void Event_IMG_Click(object sender, RowClickEventArgs e)
        {
            //if (Gv_Machine_Chk_Std.RowCount == 0) return;


            //// 더블 클릭된 행 가져오기
            //GridView gv = sender as GridView;
            //int rowHandle = e.RowHandle;

            //string fieldName = gv.FocusedColumn.FieldName;

            //if (fieldName != "Chk_Image")
            //{
            //    return;
            //}
            //else
            //{
            //    EQ_MachineChkStd Mgnt = new EQ_MachineChkStd();
            //    Mgnt.MachineCode = Convert.ToString(gv.GetRowCellValue(e.RowHandle, "MachineCode"));
            //    Mgnt.Seq = gv.GetRowCellValue(e.RowHandle, "Seq").ConvertInt_Null_To_Zero();
            //    Mgnt.InspectDivCd = Convert.ToString(gv.GetRowCellValue(e.RowHandle, "InspectDivCd"));

            //    DataRow dataRow = Mgnt.Select_Machine_Chk_Exsits();

            //    if (dataRow == null)
            //    {
            //        MessageBox.Show("등록된 이미지가 없습니다.");
            //        return;
            //    }

            //    string InFilePath = dataRow["Chk_Image"].ConvertString_Null_To_Empty();

            //    if (InFilePath == "")
            //    {
            //        MessageBox.Show("등록된 이미지가 없습니다.");
            //        return;
            //    }

            //    //FTP 이미지 다운로드
            //    cls_FTP clsFtp = new cls_FTP();
            //    Stream stream = clsFtp.DownloadFile(InFilePath);
            //    if (stream == null) return;

            //    pictureEdit1.Image = Image.FromStream(stream);



                //PictureEdit picture = new PictureEdit();
                //picture.Image = Image.FromStream(stream);

                //FrmPictureEdit frmPictureEdit = new FrmPictureEdit(picture);
                //frmPictureEdit.ShowDialog();
            //}


            //버튼클릭 이벤트라 rowhandle 인식 불가


            // 행에 대한 작업 수행
            //groupControl3.Visible = true;
        }

        #endregion GridView Setting End

        #endregion 디자인 아이템 생성 종료

        #region Event

        private void Event_GvCustomRowCellEditForEditing(object sender, CustomRowCellEditEventArgs e)
        {
            //이미지창
            layoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            //검사항목 일정Row의 Repository다르게하기
            GridView gv = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string fieldName = e.Column.FieldName;

                if (fieldName == "InspectOk")
                {
                    string type = Convert.ToString(gv.GetRowCellValue(e.RowHandle, "ValueTypeDivCd"));

                    if (type == "10")
                    {
                        //숫자 외 체크박스
                        e.RepositoryItem = rpoChk;
                    }
                    else
                    {
                        //숫자
                        e.RepositoryItem = rpoTxt;
                    }
                }
            }
        }

        #endregion Event End

        #region 조회 기능

        private void Rdo_Machine_Div_SelectedIndexChanged(object sender, EventArgs e)
        {
            Func_Search();
        }

        private void Btn_Search_Click(object sender, EventArgs e)
        {
            Func_Search();
        }

        private void Func_Search()
        {
            //1. 조회 검사
            if (!Check_Search()) return;

            //2. 검사항목 조회
            EQ_MachineChkStd Mgnt = new EQ_MachineChkStd();

            Gc_Machine_Chk_Std.DataSource = Mgnt.Select_Machine_Chk_Std(Rdo_Machine_Div.EditValue.ToString(), true) ;

            Gv_Machine_Chk_Std.BestFitColumns();   //Column 너비 조정
        }

        private bool Check_Search()
        {
            if((Gv_Machine_Chk_Std.GridControl.DataSource as DataTable)?.Select(@"Status = '신규'
                                                                    OR Status = '수정'
                                                                    OR Status = '삭제'").Length > 0)
            {
                if (MessageBox.Show("작성내역이 사라집니다.\n다른항목을 진행하시겠습니까?", "Yes or No", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    //다른항목 진행할 경우
                    return true;
                }
                else
                {
                    //다른항목 진행하지 않을경우
                    return false;
                }
            }

            return true;
        }

        #endregion 조회버튼 클릭 End

        #region 저장 기능

        private void Func_Save()
        {
            //작성내역 저장
            Gv_Machine_Chk_Std.FocusInvalidRow();

            DataTable dtItem = Gc_Machine_Chk_Std.DataSource as DataTable;

            //1. 데이터 불러온 상태인지 검사
            if (!Check_SaveClick(dtItem)) return;

            //2. 데이터 저장
            for (int curHandle = 0; curHandle < dtItem.Rows.Count; curHandle++)
            {
                if (Convert.ToString(dtItem.Rows[curHandle]["Status"]) == "신규")
                {
                    if (!Save_ToDatabase(dtItem.Rows[curHandle], 'I')) return;
                }
                else if (Convert.ToString(dtItem.Rows[curHandle]["Status"]) == "수정")
                {
                    if (!Save_ToDatabase(dtItem.Rows[curHandle], 'U')) return;
				}
				else if (Convert.ToString(dtItem.Rows[curHandle]["Status"]) == "삭제")
				{
					if (!Save_ToDatabase(dtItem.Rows[curHandle], 'D')) return;
				}
				else
                {
                    if (!Save_ToDatabase(dtItem.Rows[curHandle], 'U')) return;
                }
            }

            //3. 정상절차 진행
            Func_Search();

            MessageBox.Show("저장이 정상적으로 완료되었습니다.");
        }

        private bool Check_SaveClick(DataTable dt)
        {
            //Item을 불러온 상태인지 검사
            if (dt == null) return false;

            int checkCnt = 0;
            bool isDelete = false;

            //미입력된 칸 있는지 확인
            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToString(dr["Status"]) == "신규")
                {
                    if (Convert.ToString(dr["InspectDivCd"]) == "")
                    {
                        MessageBox.Show("검사구분 항목이 비어있습니다.");
                        return false;
                    }
					if (Convert.ToString(dr["MachineName"]) == "")
					{
						MessageBox.Show("설비명 항목이 비어있습니다.");
						return false;
					}
					if (Convert.ToString(dr["ValueTypeDivCd"]) == "")
					{
						MessageBox.Show("설비값 관리 형태 항목이 비어있습니다.");
						return false;
					}
					checkCnt++;
                }
                else if (Convert.ToString(dr["Status"]) == "수정"){ checkCnt++; }
                else if (Convert.ToString(dr["Status"]) == "삭제")
                {
                    isDelete = true;
                    checkCnt++;
                }
            }
            if (checkCnt == 0) return false;

            if (isDelete)
            {
                DialogResult dialogResult = MessageBox.Show("삭제내역이 있습니다. 삭제하시겠습니까?\n*해당 항목의 점검이력이 함께 삭제됩니다.", "Delete OK Cancel", MessageBoxButtons.OKCancel);
                if (dialogResult == DialogResult.OK)
                {
                    return true;
                }
            }

            return true;
        }

        private bool Save_ToDatabase(DataRow dr, char div)
        {

            //데이터베이스에 반영
            //1. 기본 셋팅
            EQ_MachineChkStd Machine = new EQ_MachineChkStd();
            try
            {
                //미리 dr에 입력
                if (div != 'D')
                {
                  Create_DispSeq(dr);
                }

                Machine.MachineCode = Convert.ToString(dr["MachineCode"]);
                Machine.Seq = dr["Seq"].ConvertInt_Null_To_Zero();
                Machine.DispSeq = dr["DispSeq"].ConvertInt_Null_To_Zero();

                if (div != 'D')
                {
                    //Delete제외 공통

                    Machine.InspectDivCd = Convert.ToString(dr["InspectDivCd"]);
                    Machine.MachineCode = Convert.ToString(dr["MachineCode"]);
                    Machine.ManagementItem = Convert.ToString(dr["ManagementItem"]);
                    Machine.CheckMethod = Convert.ToString(dr["CheckMethod"]);
                    Machine.StandardSpec = Convert.ToString(dr["StandardSpec"]);
                    Machine.CycleDivCd = Convert.ToString(dr["CycleDivCd"]);
                    Machine.WeekDivCd = Convert.ToString(dr["WeekDivCd"]);
                    Machine.ValueTypeDivCd = Convert.ToString(dr["ValueTypeDivCd"]);
                    Machine.InspectOk = Convert.ToString(dr["InspectOk"]);
                    Machine.InspectMin = dr["InspectMin"].ConvertInt_Null_To_Zero();
                    Machine.InspectMax = dr["InspectMax"].ConvertInt_Null_To_Zero();
                    Machine.Remark = Convert.ToString(dr["Remark"]);
                }
            }
            catch
            {
                MessageBox.Show("값 형태변환 에러");
            }

            //2. DB 적용
            if(div == 'I')
            {
                //Insert
                Machine.StartDate = DateTime.Now.AddDays(1).ConvertDate_To_Char8();
                Machine.ExpectDate = Machine.StartDate;
                if (!Machine.Ins_Machine())
                {
                    //저장 중 에러 발생시
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return false;
                }

                //추가 변경사항 private 따로 만들기
            }
            else if(div == 'U')
            {
                //Update
                if (!Machine.Upd_Machine())
                {
                    //저장 중 에러 발생시
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return false;
                }

                //추가 변경사항 private 따로 만들기
            }
            else if (div == 'D')
            {
                //Delete
                if (!Machine.Del_Machine())
                {
                    //저장 중 에러 발생시
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return false;
                }

                if (!Machine.Del_InspectResult())
                {
                    //저장 중 에러 발생시
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return false;
                }
                //추가 변경사항 private 따로 만들기
            }

            //상태값 초기화
            dr[0] = "";

            return true;
        }

        private void Create_DispSeq(DataRow dr)
        {
            //DispSeq 표시순번 검출
            //Seq는 Mssql 테이블에서 처리하도록 설정(ID사양 1씩 증가하게 설정)
            int disp = 1;


            foreach (DataRow drComp in dr.Table.Rows)
            {
                //현재행까지 왔는지 검사
                if (drComp == dr) break;

                string status = Convert.ToString(drComp["Status"]);
                string MachineCode = Convert.ToString(drComp["MachineCode"]);
                string Seq = Convert.ToString(drComp["Seq"]);
                string DispSeq = Convert.ToString(drComp["DispSeq"]);

                if (Convert.ToString(drComp["MachineCode"]) == Convert.ToString(dr["MachineCode"])){
                    if (status != "삭제")
                    {
                        ++disp;
                    }
                }
            }
            dr["DispSeq"] = disp;
        }


        #endregion 저장 기능 End

        private void cmdClose_Click(object sender,EventArgs e)
        {
            this.Close();
        }

        private void cmdDisplay_Click(object sender,EventArgs e)
        {
            Func_Search();
        }

        private void cmdSave_Click(object sender,EventArgs e)
        {
            Func_Save();
        }

        private void cmd_Click(object sender,EventArgs e)
        {
            //행삽입, 추가, 삭제 버튼 클릭 이벤트
            //1. 이름의 마지막 3글자 가져오기
            smtSearchButton btn = sender as smtSearchButton;
            string txt_Act = btn.Name.Substring(btn.Name.Length - 3);

            //2. 입력 데이터 설정
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (txt_Act != "Del")
            {
                //삽입, 추가 진행인 경우
                dic.Add("Status", "신규");
                dic.Add("InspectDivCd", Rdo_Machine_Div.EditValue);
                dic.Add("Start_Date", DateTime.Now.AddDays(1));
            }
            else
            {
                //삭제 진행인경우
                dic.Add("Status", Convert.ToString(Gv_Machine_Chk_Std.GetFocusedRowCellValue("Status")) == "삭제" ? "" : "삭제");
            }

            //3. 행삽입, 추가, 삭제 프로세스 진행
            grid.Set_Row(Gc_Machine_Chk_Std, dic, txt_Act);
        }

        private void Gv_Machine_Chk_Std_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

        }

		private void CmdAllAdd_Click(object sender, EventArgs e)
		{
            DataTable dt = Gv_Machine_Chk_Std.GridControl.DataSource as DataTable;
            if (dt.Rows.Count <= 0) return;

            string basic_Cd = Gv_Machine_Chk_Std.GetFocusedRowCellValue("MachineCode").ConvertString_Null_To_Empty();

            Frm_EQ_Inspection frm = new Frm_EQ_Inspection(basic_Cd, Rdo_Machine_Div.EditValue.ToString(), "Machine");
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();

            Func_Search();
        }
	}
}