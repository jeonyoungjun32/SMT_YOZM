using baseSMT;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.CM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SmartMES.View.CM
{
    /// <summary>
    /// 거래처
    /// </summary>
    public partial class Frm_CM_Customer : DevExpress.XtraEditors.XtraForm
    {
        clsSystem sys = new clsSystem();
        string query = "";

        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();
        clsCode code = new clsCode();
        Database db = new Database();

        bool isSettingValue = false;

        public Frm_CM_Customer()
        {
            InitializeComponent();

         
        }

        private void Frm_CM_Customer_Load(object sender, EventArgs e)
        {
            Auth.ApplyButtonAuth(Convert.ToString(this.Tag), smtCommonBar1);//상단 패널 사용자별 버튼 권한 설정

            Init();
            Set_GridView(Gv_List);

            Func_Search();

            RegisterEditEvents(this);
        }

        private void Init()
		{
            code.LookupCorp(ref corpIdTextEdit);
            code.LookupCategory(ref custTypeTextEdit, "A030");            //거래처구분

            salesManSearchLookUpEdit.Properties.DataSource = code.GetDataTableUser(false, null, "N");    //영업담당
            salesManSearchLookUpEdit.Properties.DisplayMember = "명칭";
            salesManSearchLookUpEdit.Properties.ValueMember = "코드";

            corpIdTextEdit.EditValue = WrGlobal.CorpID;

            //BaseEdit 태그에 필드명 설정 - Grid에 값 세팅하기 위해 필요
            custCodeTextEdit.Tag            = "CustCode";
            custNameTextEdit.Tag            = "CustName";
            custTypeTextEdit.Tag            = "CustType";
            useYnComboboxEdit.Tag           = "UseYn";
            salesManSearchLookUpEdit.Tag    = "SalesMan";
            presiNameTextEdit.Tag           = "PresiName";
            custNoTextEdit.Tag              = "CustNo";
            citizeNoTextEdit.Tag            = "CustRegNo";
            bussinessCategoryTextEdit.Tag   = "BussinessCategory";
            bussinessFieldTextEdit.Tag      = "BussinessField";
            telTextEdit.Tag         = "Tel";
            faxTextEdit.Tag         = "Fax";
            emailTextEdit.Tag       = "Email";
            homePageTextEdit.Tag    = "HomePage";
            postNoTextEdit.Tag      = "PostNo";
            textEdit1.Tag           = "Address";
            remarksTextEdit.Tag     = "Remark";
            ediMatchNamesTextEdit.Tag = "EdiMatchNames";
        }

        #region GridView Setting

        private void Set_GridView(GridView gv)
        {
            //GvLine Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.IndicatorWidth = 50;                     //순번 너비

            CM_CateItem cm = new CM_CateItem();
            RepositoryItem lup_CustDiv = rpo.Create_RepositoryItem(cm.SelectClassItemLookUp(WrGlobal.CorpID, "A030"), "분류코드", "분류명");  //거래처구분

            //Column 생성
            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "거래처코드", "CustCode");
            grid.Create_GvColumn(gv, "거래처명", "CustName");
            grid.Create_GvColumn(gv, "거래처구분", "CustType", lup_CustDiv);

            grid.Create_GvColumn(gv, "대표자명", "PresiName");
            grid.Create_GvColumn(gv, "주소", "Address");
            grid.Create_GvColumn(gv, "영업담당", "SalesMan");
            grid.Create_GvColumn(gv, "전화", "Tel");
            grid.Create_GvColumn(gv, "사업자번호", "CustNo");


            grid.Create_GvColumn(gv, "법인번호", "CustRegNo");
            grid.Create_GvColumn(gv, "업태", "BussinessCategory");
            grid.Create_GvColumn(gv, "업종", "BussinessField");
            grid.Create_GvColumn(gv, "팩스번호", "Fax");
            grid.Create_GvColumn(gv, "이메일", "Email");
            grid.Create_GvColumn(gv, "홈페이지", "HomePage");
            grid.Create_GvColumn(gv, "우편번호", "PostNo");
            grid.Create_GvColumn(gv, "비고", "Remark");
            grid.Create_GvColumn(gv, "EDI 연결명", "EdiMatchNames");
            gv.Columns["EdiMatchNames"].Width = 240;

            grid.Create_GvColumn(gv, "사용여부", "UseYn");

            gv.Columns["CustRegNo"].Visible = false;
            gv.Columns["BussinessCategory"].Visible = false;
            gv.Columns["BussinessField"].Visible = false;
            gv.Columns["Fax"].Visible = false;
            gv.Columns["Email"].Visible = false;
            gv.Columns["HomePage"].Visible = false;
            gv.Columns["PostNo"].Visible = false;
            gv.Columns["UseYn"].Visible = false;

            //셀값 정렬
            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;     //상태

            //이벤트 설정
            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
            gv.RowStyle += grid.RowStyle_ChangeBg_ByStatus;           //삭제시 배경색 지정
            gv.CellValueChanged += Event_CellValueChanged;            //셀값 수정시 상태값 수정
            gv.GridControl.ProcessGridKey += Event_ProcessGridKey;    //행추가, 삽입, 삭제 단축키기능 부여
            gv.FocusedRowChanged += Gv_FocusedRowChanged;
        }

        private void Gv_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            this.Validate();

            DisplayDetail();

            CM_Cust customer = new CM_Cust();
            customer.CustCode = custCodeTextEdit.Text;

            if (customer.Check_Existence() == null) custCodeTextEdit.Enabled = true;
            else custCodeTextEdit.Enabled = false;
        }

        private void Event_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;

            //셀값 수정 시 상태값 수정
            if (e.Column.FieldName == gv.Columns[0].FieldName) return;
            grid.Change_Status(gv, e.RowHandle);
        }


        #endregion GridView Setting End

        #region 행 삽입, 추가, 삭제

        private void Event_ProcessGridKey(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case (Keys.Alt | Keys.I): BtnInsert_Click(cmdInsert, null); break;  //행삽입
                case (Keys.Alt | Keys.D): Delete(); break; //행삭제
            }
        }

        private void BtnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                //행삽입, 추가, 삭제 버튼 클릭 이벤트
                //1. 이름의 마지막 3글자 가져오기
                smtSearchButton btn = sender as smtSearchButton;
                string txt_Act = btn.Tag.ToString();

                //2. 입력 데이터 설정
                Dictionary<string, object> dic = new Dictionary<string, object>();

                if (txt_Act != "Del")
                {
                    dic.Add("Status", "신규");
                }

                //3. 행삽입, 추가, 삭제 프로세스 진행
                grid.Set_Row(Gc_List, dic, txt_Act);
              
                ClearText();

                useYnComboboxEdit.EditValue = "Y";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetText(DataRow dr)
        {
            if (dr == null) return;

            isSettingValue = true;

            custCodeTextEdit.EditValue = dr["CustCode"];
            custNameTextEdit.EditValue = dr["CustName"];
            custTypeTextEdit.EditValue = dr["CustType"];
            useYnComboboxEdit.EditValue = dr["UseYn"];
            salesManSearchLookUpEdit.EditValue = dr["SalesMan"];

            presiNameTextEdit.EditValue = dr["PresiName"];
            custNoTextEdit.EditValue = dr["CustNo"];
            citizeNoTextEdit.EditValue = dr["CustRegNo"];
            bussinessCategoryTextEdit.EditValue = dr["BussinessCategory"];
            bussinessFieldTextEdit.EditValue = dr["BussinessField"];

            telTextEdit.EditValue = dr["Tel"];
            faxTextEdit.EditValue = dr["Fax"];
            emailTextEdit.EditValue = dr["Email"];
            homePageTextEdit.EditValue = dr["HomePage"];
            postNoTextEdit.EditValue = dr["PostNo"];
            textEdit1.EditValue = dr["Address"];
            remarksTextEdit.EditValue = dr["Remark"];
            ediMatchNamesTextEdit.EditValue = dr["EdiMatchNames"];

            isSettingValue = false;
        }

        private void ClearText()
        {
            isSettingValue = true;

            custCodeTextEdit.Enabled = true;

            custCodeTextEdit.EditValue = null;
            custNameTextEdit.EditValue = null;
            custTypeTextEdit.EditValue = null;
            useYnComboboxEdit.EditValue = null;
            salesManSearchLookUpEdit.EditValue = null;

            presiNameTextEdit.EditValue = null;
            custNoTextEdit.EditValue = null;

            citizeNoTextEdit.EditValue = null;
            bussinessCategoryTextEdit.EditValue = null;
            bussinessFieldTextEdit.EditValue = null;

            telTextEdit.EditValue = null;
            faxTextEdit.EditValue = null;
            emailTextEdit.EditValue = null;
            homePageTextEdit.EditValue = null;
            postNoTextEdit.EditValue = null;
            textEdit1.EditValue = null;
            remarksTextEdit.EditValue = null;

            isSettingValue = false;
        }

        #endregion 행 삽입, 추가, 삭제 End

        #region 조회 기능

        private void cmdDisplay_Click(object sender, EventArgs e)
        {
            Func_Search();
        }

        private void Func_Search()
        {
            // 데이터 조회
            CM_Cust cc = new CM_Cust();

            Gc_List.DataSource = cc.Select_CM_CustAll();

            Gv_List.BestFitColumns();   //Column 너비 조정
            Gv_List.Columns["Address"].Width = 500;
        }

        private void DisplayDetail()
        {
            DataRow dr = Gv_List.GetFocusedDataRow();

            if (dr == null)
            {
                Gc_List.DataSource = null;
                ClearText();
                return;
            }

            SetText(dr);
        }

        #endregion 조회 기능 End

        #region 저장 기능

        private void cmdSave_Click(object sender, EventArgs e)
        {
            Func_Save();
        }

        private void Func_Save()
        {
            this.ValidateChildren();
            cmdSave.Focus();

            //작성내역 저장
            DataTable dt = Gc_List.DataSource as DataTable;

            //1. 데이터 불러온 상태인지 검사
            if (!Check_SaveClick(dt)) return;

            //2. 데이터 저장
            for (int curHandle = 0; curHandle < dt.Rows.Count; curHandle++)
            {
                if (Convert.ToString(dt.Rows[curHandle]["Status"]) == "신규")
                {
                    if (!Save_ToDatabase(dt.Rows[curHandle], 'I')) return;
                }
                else if (Convert.ToString(dt.Rows[curHandle]["Status"]) == "수정")
                {
                    if (!Save_ToDatabase(dt.Rows[curHandle], 'U')) return;
                }
            }

            //3. 정상절차 진행
            Func_Search();

            MessageBox.Show("저장이 정상적으로 완료되었습니다.");
        }

        private bool Check_SaveClick(DataTable dt)
        {
            int checkCnt = 0;
            bool isDelete = false;

            //Data를 불러온 상태인지 검사
            if (dt == null)
            {
                return false;
            }

            //변경사항이 없을 경우 RETURN
            foreach (DataRow dr in dt.Rows)
            {
                string status = dr["Status"].ConvertString_Null_To_Empty();

                if (status == "신규")
                {
                    if (WrGlobal.ProcOption["BS"]["Cust_AutoNo"] == "N" && dr["CustCode"].ConvertString_Null_To_Empty() == "")
                    {
                        MessageBox.Show("거래처코드가 비어있습니다.");
                        return false;
                    }

                    if (dr["CustName"].ConvertString_Null_To_Empty() == "")
                    {
                        MessageBox.Show("거래처명이 비어있습니다.");
                        return false;
                    }

                    if (dr["CustType"].ConvertString_Null_To_Empty() == "")
                    {
                        MessageBox.Show("거래처 구분이 비어있습니다.");
                        return false;
                    }

                    checkCnt++;
                }
                else if (status == "수정")
                {
                    checkCnt++;
                }
                else if (status == "삭제")
                {
                    checkCnt++;
                    isDelete = true;
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
            //CM_Cust cm = new CM_Cust();
            //cm.CustCode = custCodeTextEdit.EditValue.ConvertString_Null_To_Empty();
            //cm.CustName = custNameTextEdit.EditValue.ConvertString_Null_To_Empty();
            //cm.CustType = custTypeTextEdit.EditValue.ConvertString_Null_To_Empty();
            //cm.UseYn = useYnComboboxEdit.EditValue.ConvertString_Null_To_Empty();

            //cm.SalesMan = salesManSearchLookUpEdit.EditValue.ConvertString_Null_To_Empty();
            //cm.PresiName = presiNameTextEdit.EditValue.ConvertString_Null_To_Empty();
            //cm.CustNo = custNoTextEdit.EditValue.ConvertString_Null_To_Empty();
            //cm.CustRegNo = citizeNoTextEdit.EditValue.ConvertString_Null_To_Empty();
            //cm.BussinessCategory = bussinessCategoryTextEdit.EditValue.ConvertString_Null_To_Empty();
            //cm.BussinessField = bussinessFieldTextEdit.EditValue.ConvertString_Null_To_Empty();

            //cm.Tel = telTextEdit.EditValue.ConvertString_Null_To_Empty();
            //cm.Fax = faxTextEdit.EditValue.ConvertString_Null_To_Empty();
            //cm.Email = emailTextEdit.EditValue.ConvertString_Null_To_Empty();
            //cm.HomePage = homePageTextEdit.EditValue.ConvertString_Null_To_Empty();
            //cm.PostNo = postNoTextEdit.EditValue.ConvertString_Null_To_Empty();
            //cm.Address = textEdit1.EditValue.ConvertString_Null_To_Empty();
            //cm.Remark = remarksTextEdit.EditValue.ConvertString_Null_To_Empty();

            CM_Cust cm = new CM_Cust();
            cm.CorpId = WrGlobal.CorpID;
            cm.CustCode = dr["CustCode"].ConvertString_Null_To_Empty();
            cm.CustName = dr["CustName"].ConvertString_Null_To_Empty();
            cm.CustType = dr["CustType"].ConvertString_Null_To_Empty();
            cm.UseYn = dr["UseYn"].ConvertString_Null_To_Empty();

            cm.SalesMan     = dr["SalesMan"].ConvertString_Null_To_Empty();
            cm.PresiName    = dr["PresiName"].ConvertString_Null_To_Empty();
            cm.CustNo       = dr["CustNo"].ConvertString_Null_To_Empty();
            cm.CustRegNo    = dr["CustRegNo"].ConvertString_Null_To_Empty();
            cm.BussinessCategory    = dr["BussinessCategory"].ConvertString_Null_To_Empty();
            cm.BussinessField       = dr["BussinessField"].ConvertString_Null_To_Empty();

            cm.Tel   = dr["Tel"].ConvertString_Null_To_Empty();
            cm.Fax      = dr["Fax"].ConvertString_Null_To_Empty();
            cm.Email    = dr["Email"].ConvertString_Null_To_Empty();
            cm.HomePage = dr["HomePage"].ConvertString_Null_To_Empty();
            cm.PostNo   = dr["PostNo"].ConvertString_Null_To_Empty();
            cm.Address  = dr["Address"].ConvertString_Null_To_Empty();
            cm.Remark   = dr["Remark"].ConvertString_Null_To_Empty();
            cm.EdiMatchNames = dr["EdiMatchNames"].ConvertString_Null_To_Empty().Trim();
            cm.UseYn = useYnComboboxEdit.EditValue.ConvertString_Null_To_Empty();

            //2. DB 적용
            try
            {
                if (div == 'I')
                {
                    if(cm.CustCode.IsNullOrEmpty())
					{
                        cm.CustCode = WrGlobal.ProcOption["BS"]["Cust_AutoNo"] == "Y" ? "C" + sys.GetNextSequence("Cust_Seq").ToString("D6") : "";
					}

                    //Insert
                    if (!cm.Insert_CM_Cust())
                    {
                        //저장 중 에러 발생시
                        MessageBox.Show("저장 중 에러가 발생하였습니다.");
                        return false;
                    }
                }
                else if (div == 'U')
                {
                    //Update
                    if (!cm.Update_CM_Cust())
                    {
                        //저장 중 에러 발생시
                        MessageBox.Show("저장 중 에러가 발생하였습니다.");
                        return false;
                    }
                }
                else if (div == 'D')
                {
                    //Delete
                    if (!cm.Delete_CM_Cust())
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        private void RegisterEditEvents(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is DevExpress.XtraEditors.BaseEdit edit)
                {
                    edit.EditValueChanged += Detail_EditValueChanged;
                    edit.Leave += Detail_Leave;
                }

                if (ctrl.Controls.Count > 0)
                {
                    RegisterEditEvents(ctrl);
                }
            }
        }

        private void Detail_EditValueChanged(object sender, EventArgs e)
        {
			if (isSettingValue) return;

			if (Gv_List.FocusedRowHandle < 0) return;

			string status = Convert.ToString(
				Gv_List.GetFocusedRowCellValue("Status")
			);

			if (status != "신규" && status != "삭제")
			{
				Gv_List.SetFocusedRowCellValue("Status", "수정");
			}
		}

        private void Detail_Leave(object sender, EventArgs e)
        {
            // 1. 코드로 값을 수정 중이거나 포커스된 행이 없으면 중단
            if (isSettingValue || Gv_List.FocusedRowHandle < 0) return;

            BaseEdit editor = sender as BaseEdit;
            if (editor == null || editor.Tag == null) return;

            string fieldName = editor.Tag.ToString(); // Tag에 저장된 필드명 가져오기
            object newValue = editor.EditValue;
            object oldValue = Gv_List.GetFocusedRowCellValue(fieldName);

            // 2. 기존 값과 다를 경우에만 그리드에 반영
            if (Equals(oldValue, newValue) == false)
            {
                if(fieldName == "CustCode")
				{
                    //거래처코드 중복 체크
					for (int i = 0; i < Gv_List.RowCount; i++)
					{
                        if (newValue.ConvertString_Null_To_Empty() == Gv_List.GetRowCellValue(i, "CustCode").ConvertString_Null_To_Empty())
						{
                            XtraMessageBox.Show($"중복된 거래처 코드가 있습니다.\n코드 : '{newValue}'");
                            editor.EditValue = null;
                            return;
						}
					}
				}

                // 그리드 셀 값 변경
                Gv_List.SetFocusedRowCellValue(fieldName, newValue);
            }
        }

        #endregion 저장 기능 End

        //삭제 버튼 클릭
        private void cmdDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void Delete()
		{
            string custCode = Gv_List.GetFocusedRowCellDisplayText("CustCode") == null ? "" : Gv_List.GetFocusedRowCellDisplayText("CustCode");

            if (MessageBox.Show(string.Format("선택한 '{0}' 거래처를 삭제하시겠습니까?", custCode), "삭제", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            if (Check_DeleteClick(custCode))
            {
                MessageBox.Show("사용하고 있는 거래처로 삭제할 수 없습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                this.Validate();

                CM_Cust cm = new CM_Cust();
                cm.CustCode = custCode;

                if (!cm.Delete_CM_Cust())
                {
                    //저장 중 에러 발생시
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return;
                }

                Func_Search();

                MessageBox.Show("저장이 정상적으로 완료되었습니다.");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //사용하고 있는 거래처인지 체크
        private bool Check_DeleteClick(string custCode)
        {
            query = $"select count(1) from BM_SdOrderM where SoldToCode = '{custCode}'";
            DataRowView drv = db.GetDataRecord(query);
            int result = (int)drv[0];

            // 단일 값 반환 ExecuteScalar 추가
            //int result = db.ExecuteScalarQuery(query);
            //return result > 0;

            if (result == 0) { return false; }
            return true;
        }

        //엑셀 버튼 클릭
        private void cmdExcel_Click(object sender, EventArgs e)
        {
            Frm_CM_ExcelUpLoad_Customer frm = new Frm_CM_ExcelUpLoad_Customer();
            frm.Gc = Gc_List;
            frm.formName = smtCommonBar1.smtImageTitleText;

            frm.ShowDialog();
            Func_Search();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
	}
}
