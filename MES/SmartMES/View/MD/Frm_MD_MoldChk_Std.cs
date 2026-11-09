using baseSMT;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.CM;
using SmartMES.Models.MD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SmartMES.View.MD
{
    public partial class Frm_MD_MoldChk_Std : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();
        clsCode code = new clsCode();

        RepositoryItemTextEdit rpoTxt = new RepositoryItemTextEdit();   //검사항목 Repository 숫자입력
        RepositoryItemCheckEdit rpoChk = new RepositoryItemCheckEdit(); //검사항목 Repository CheckBox

        public Frm_MD_MoldChk_Std()
        {
            InitializeComponent();
            SetDesign();
        }

        #region 디자인 아이템 생성

        private void SetDesign()
        {
            //GridView 데이터 입력
            Set_GridView(Gv_List);

            //검사구분 데이터 입력
            Set_Chk_Std_Rdo(Rdo_Div);

            //폼 닫을때 검사 작성된 내역 있는지 검사
            this.FormClosing += new FormClosingEventHandler((sender, e) => {
                e.Cancel = false;
                if (grid.Check_Gv_FormClosing(Gv_List, "Status"))
                {
                    e.Cancel = true;
                }
            });
        }

        private void Set_Chk_Std_Rdo(RadioGroup rdo)
        {
            //Item 생성
            clsRadioGroup clsRadio = new clsRadioGroup();
            CM_CateItem cc = new CM_CateItem();
            clsRadio.Create_RadioGroupItem(rdo, cc.SelectClassItemLookUp(WrGlobal.CorpID, "MD40"), "분류명", "분류코드");

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

            CommonClass cl = new CommonClass();
            CM_CateItem item = new CM_CateItem();

            RepositoryItem lup_InspectDiv = rpo.Create_RepositoryItem(item.SelectClassItemLookUp(WrGlobal.CorpID, "MD40"), "분류코드", "분류명");
            RepositoryItem lup_CycleDiv = rpo.Create_RepositoryItem(item.SelectClassItemLookUp(WrGlobal.CorpID, "MD4050"), "분류코드", "분류명") ;
            RepositoryItem lup_WeekDiv = rpo.Create_RepositoryItem(item.SelectClassItemLookUp(WrGlobal.CorpID, "MD4060"), "분류코드", "분류명") ;
            RepositoryItem lup_ValueType = rpo.Create_RepositoryItem(item.SelectClassItemLookUp(WrGlobal.CorpID, "MD4070"), "분류코드", "분류명") ;

            //금형
            MD_Mold MD_Mold = new MD_Mold();
            RepositoryItemSearchLookUpEdit lup_Mold = rpo.Create_RepositoryItemSearchLookUp(MD_Mold.Select_Mold_Mgmt_No(), "코드", "금형명");

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
            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "점검구분", "InspectDivCd", lup_InspectDiv);
            grid.Create_GvColumn(gv, "금형코드", "MoldCode");
            grid.Create_GvColumn(gv, "금형명", "MoldName", lup_Mold);
            grid.Create_GvColumn(gv, "저장순번", "Seq");
            grid.Create_GvColumn(gv, "표시순번", "DispSeq");
            grid.Create_GvColumn(gv, "관리항목", "ManagementItem");
            grid.Create_GvColumn(gv, "확인방법", "CheckMethod");
            grid.Create_GvColumn(gv, "점검기준", "StandardSpec");
            grid.Create_GvColumn(gv, "관리주기", "CycleDivCd", lup_CycleDiv);

            grid.Create_GvColumn(gv, "관리요일", "WeekDivCd", lup_WeekDiv);
            grid.Create_GvColumn(gv, "값 관리 형태", "ValueTypeDivCd", lup_ValueType);
            //grid.Create_GvColumn(gv, "이미지", "Chk_Image", Btn_Img);
            grid.Create_GvColumn(gv, "기본값", "InspectOk");
            grid.Create_GvColumn(gv, "측정수치Min", "InspectMin");
            grid.Create_GvColumn(gv, "측정수치Max", "InspectMax");
            grid.Create_GvColumn(gv, "비고", "Remark");

            //Column 단일 설정
            //셀값 변경 허용
            grid.Column_AllowEdit(gv.Columns["InspectDivCd"]);      //점검구분
            grid.Column_AllowEdit(gv.Columns["MoldName"]);          //금형명
            grid.Column_AllowEdit(gv.Columns["ManagementItem"]);    //관리항목
            grid.Column_AllowEdit(gv.Columns["CheckMethod"]);       //확인방법
            grid.Column_AllowEdit(gv.Columns["StandardSpec"]);      //점검기준
            grid.Column_AllowEdit(gv.Columns["CycleDivCd"]);        //관리주기
            grid.Column_AllowEdit(gv.Columns["WeekDivCd"]);         //관리요일
            grid.Column_AllowEdit(gv.Columns["ValueTypeDivCd"]);    //등록 형태
            //grid.Column_AllowEdit(gv.Columns["Chk_Image"]);
            grid.Column_AllowEdit(gv.Columns["InspectOk"]);         //기본값
            grid.Column_AllowEdit(gv.Columns["InspectMin"]);        //측정수치Min
            grid.Column_AllowEdit(gv.Columns["InspectMax"]);        //측정수치Max
            grid.Column_AllowEdit(gv.Columns["Remark"]);            //비고

            //Repository 설정
            rpoChk = rpo.Create_rpoChk();
            rpoTxt = rpo.Create_rpoTxt_Numeric();

            //셀값 정렬
            grid.SetCol_Type(gv.Columns["InspectMin"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["InspectMax"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["InspectOk"], clsGrid.TypeList.TFloat);

            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;     //상태

            //이벤트 설정
            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
            gv.CellValueChanged += Event_CellValueChanged;            //셀값 수정시 상태값 수정
            gv.GridControl.ProcessGridKey += Event_ProcessGridKey;    //행추가, 삽입, 삭제 단축키기능 부여
            gv.CustomRowCellEdit += Event_GvCustomRowCellEditForEditing;    //등록형태 값에 따른 기본값 입력 실

            //검사구분 라디오 버튼 변경시 재조회
            Rdo_Div.SelectedIndexChanged += new EventHandler(Rdo_Machine_Div_SelectedIndexChanged);

            gv.Columns["Seq"].Visible = false;
            gv.Columns["DispSeq"].Visible = false;
        }

        private void Event_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;
            DataRow drChanged = gv.GetDataRow(e.RowHandle);

            //셀값 수정 시 상태값 수정
            if (e.Column.FieldName == gv.Columns[0].FieldName) return;
            grid.Change_Status(gv, e.RowHandle);

            if (e.Column.FieldName == "MoldName")
            {
                grid.SetInfo_Mold(drChanged, Convert.ToString(e.Value));
            }

            gv.BestFitColumns();
        }

        #region 행 삽입, 추가, 삭제

        private void Event_ProcessGridKey(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case (Keys.Alt | Keys.I): Btn_Row_Ins.PerformClick(); break;  //행삽입
                case (Keys.Alt | Keys.A): Btn_Row_Ins.PerformClick(); break;  //행추가
                case (Keys.Alt | Keys.D): Btn_Row_Del.PerformClick(); ; break; //행삭제
            }
        }

        #endregion 행 삽입, 추가, 삭제 End

        #endregion GridView Setting End

        #endregion 디자인 아이템 생성 종료

        #region Event

        private void Frm_MD_MoldChk_Std_Load(object sender, EventArgs e)
        {
            Func_Search();
        }

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

        private void cmd_Click(object sender, EventArgs e)
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
                dic.Add("InspectDivCd", Rdo_Div.EditValue);
            }
            else
            {
                //삭제 진행인경우
                dic.Add("Status", Convert.ToString(Gv_List.GetFocusedRowCellValue("Status")) == "삭제" ? "" : "삭제");
            }

            //3. 행삽입, 추가, 삭제 프로세스 진행
            grid.Set_Row(Gc_List, dic, txt_Act);
        }

        private void CmdAllAdd_Click(object sender, EventArgs e)
        {
            DataTable dt = Gv_List.GridControl.DataSource as DataTable;
            if (dt.Rows.Count <= 0) return;

            string basic_Cd = Gv_List.GetFocusedRowCellValue("MoldCode").ConvertString_Null_To_Empty();

            //Frm_EQ_Inspection frm = new Frm_EQ_Inspection(basic_Cd, Rdo_Machine_Div.EditValue.ToString(), "Machine");
            //frm.StartPosition = FormStartPosition.CenterScreen;
            //frm.ShowDialog();

            Func_Search();
        }

        #endregion Event End

        #region 조회 기능

        private void cmdDisplay_Click(object sender, EventArgs e)
        {
            Func_Search();
        }

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
            MD_MoldChk_Std md = new MD_MoldChk_Std();
            md.InspectDivCd = Rdo_Div.EditValue.ConvertString_Null_To_Empty();

            Gc_List.DataSource = md.Sel_Mold_Chk_Std() ;

            Gv_List.BestFitColumns();   //Column 너비 조정
        }

        private bool Check_Search()
        {
            if((Gv_List.GridControl.DataSource as DataTable)?.Select(@"Status = '신규'
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

        private void cmdSave_Click(object sender, EventArgs e)
        {
            Func_Save();
        }

        private void Func_Save()
        {
            //작성내역 저장
            Gv_List.FocusInvalidRow();

            DataTable dtItem = Gc_List.DataSource as DataTable;

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
                    if (MessageBox.Show("해당 항목의 점검이력이 함께 삭제됩니다.\n삭제하시겠습니까?", "Yes or No", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (!Save_ToDatabase(dtItem.Rows[curHandle], 'D')) return;
                    }
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
					if (Convert.ToString(dr["MoldName"]) == "")
					{
						MessageBox.Show("금형 항목이 비어있습니다.");
						return false;
					}
					if (Convert.ToString(dr["ValueTypeDivCd"]) == "")
					{
						MessageBox.Show("값 관리 형태 항목이 비어있습니다.");
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
                DialogResult dialogResult = MessageBox.Show("삭제내역이 있습니다. 삭제하시겠습니까?", "Delete OK Cancel", MessageBoxButtons.OKCancel);
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
            MD_MoldChk_Std md = new MD_MoldChk_Std();
            try
            {
                //미리 dr에 입력
                if (div != 'D')
                {
                  Create_DispSeq(dr);
                }

                md.MoldCode = Convert.ToString(dr["MoldCode"]);
                md.Seq = dr["Seq"].ConvertInt_Null_To_Zero();
                md.DispSeq = dr["DispSeq"].ConvertInt_Null_To_Zero();

                if (div != 'D')
                {
                    //Delete제외 공통
                    md.InspectDivCd = Convert.ToString(dr["InspectDivCd"]);
                    md.ManagementItem = Convert.ToString(dr["ManagementItem"]);
                    md.CheckMethod = Convert.ToString(dr["CheckMethod"]);
                    md.StandardSpec = Convert.ToString(dr["StandardSpec"]);
                    md.CycleDivCd = Convert.ToString(dr["CycleDivCd"]);
                    md.WeekDivCd = Convert.ToString(dr["WeekDivCd"]);
                    md.ValueTypeDivCd = Convert.ToString(dr["ValueTypeDivCd"]);
                    md.InspectOk = Convert.ToString(dr["InspectOk"]);
                    md.InspectMin = dr["InspectMin"].ConvertInt_Null_To_Zero();
                    md.InspectMax = dr["InspectMax"].ConvertInt_Null_To_Zero();
                    md.Remark = Convert.ToString(dr["Remark"]);
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
                if (!md.Ins_Mold_Chk_Std())
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
                if (!md.Upd_Mold_Chk_Std())
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
                if (!md.Del_Mold_Chk_Std())
                {
                    //저장 중 에러 발생시
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return false;
                }

                MD_MoldChk mc = new MD_MoldChk();
                mc.MoldCode = Convert.ToString(dr["MoldCode"]);
                mc.Seq = dr["Seq"].ConvertInt_Null_To_Zero();

                if (!mc.Del_InspectResult())
                {
                    //저장 중 에러 발생시
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return false;
                }
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
                string md = Convert.ToString(drComp["MoldCode"]);
                string Seq = Convert.ToString(drComp["Seq"]);
                string DispSeq = Convert.ToString(drComp["DispSeq"]);

                if (Convert.ToString(drComp["MoldCode"]) == Convert.ToString(dr["MoldCode"])){
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
	}
}