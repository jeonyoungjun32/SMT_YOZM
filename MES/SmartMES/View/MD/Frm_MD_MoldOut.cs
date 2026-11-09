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
    public partial class Frm_MD_MoldOut : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        private string SelectType = "Out";

        public Frm_MD_MoldOut()
        {
            InitializeComponent();
            SetDesign();
        }

        #region 디자인 아이템 생성

        private void SetDesign()
        {
            Set_SearchLup();
            Set_GvOut(Gv_MoldOut);
        }

        private void Set_SearchLup()
        {
            DateTime startDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-01"));
            DateTime endDate = Convert.ToDateTime(startDate.AddDays(40).ToString("yyyy-MM-01"));
            endDate = endDate.AddDays(-1);

            Dt_Date.smtStartDateTimeValue = startDate;
            Dt_Date.smtEndDateTimeValue = endDate;

            Lup_Cust.smtLookUpDataSource = code.Get_CustomerDt(true);
            Lup_Cust.smtLookUpDataDisplayMember = "내용";
            Lup_Cust.smtLookUpDataValueMember = "코드";
            Lup_Cust.smtLookUpValue = "ALL";

            cls_Lookup clsLup = new cls_Lookup();
            MD_Mold Mold = new MD_Mold();

            DataTable dt_Mold = Mold.Select_Mold_Mgmt_No(true);
            clsLup.Create_RepositoryItemSearchLookUp(Lup_Mold, dt_Mold, "코드", "금형명");
            Lup_Mold.smtLookUpValue = "ALL";
        }

        #region GridView Setting

        private void Set_GvOut(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false; //gv.BestFitColumns() 쓸 경우 false

            //Repository 설정
            CM_Cust cust = new CM_Cust();
            RepositoryItemSearchLookUpEdit lup_Cust = rpo.Create_RepositoryItemSearchLookUp(cust.Select_Cust_List(new string[] { }), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Emp = rpo.Create_RepositoryItemSearchLookUp(code.GetDataTableUser(false), "코드", "명칭");

            MD_Mold MD_Mold = new MD_Mold();
            RepositoryItemSearchLookUpEdit lup_Mold = rpo.Create_RepositoryItemSearchLookUp(MD_Mold.Select_Mold_Mgmt_No(), "코드", "금형명");
            lup_Mold.BestFitMode = BestFitMode.BestFitResizePopup;

            CM_CateItem cm = new CM_CateItem();
            RepositoryItem lup_Div = rpo.Create_RepositoryItem(cm.SelectClassItemLookUp(WrGlobal.CorpID, "MD20"), "분류코드", "분류명");

            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "처리구분", "OutType", lup_Div);
            grid.Create_GvColumn(gv, "출고번호", "OutNo");         //Dev만 visible
            grid.Create_GvColumn(gv, "출고순번", "OutRowSeq");    //Dev만 visible
            grid.Create_GvColumn(gv, "조회순번", "DispSeq");       //Dev만 visible
            grid.Create_GvColumn(gv, "출고일자", "OutDate");

            //grid.Create_GvColumn(gv, "반납/폐기처", "CustCode");
            grid.Create_GvColumn(gv, "고객사명", "CustCode", lup_Cust);
            grid.Create_GvColumn(gv, "제작처명", "ProdtCust");

            grid.Create_GvColumn(gv, "프로젝트명", "MoldPjtNo");
            grid.Create_GvColumn(gv, "품명(금형명)", "MoldName", lup_Mold);
            grid.Create_GvColumn(gv, "금형코드", "MoldCode");
            grid.Create_GvColumn(gv, "금형코드", "MoldSN");
            grid.Create_GvColumn(gv, "차수", "Chasu");

            //grid.Create_GvColumn(gv, "반납처명", "CustName");

            grid.Create_GvColumn(gv, "출고량", "OutQty");
            grid.Create_GvColumn(gv, "비고", "Remark");

            grid.Create_GvColumn(gv, "등록자", "CreId", lup_Emp);
            grid.Create_GvColumn(gv, "등록일시", "CreDt");
            grid.Create_GvColumn(gv, "수정자", "ModId", lup_Emp);
            grid.Create_GvColumn(gv, "수정일시", "ModDt");

            //Repository
            //gv.Columns["OutType"].ColumnEdit = lup_OutType;

            //셀값 변경 허용
            //grid.Column_AllowEdit(gv.Columns["CustName"]);
            grid.Column_AllowEdit(gv.Columns["OutType"]);
            grid.Column_AllowEdit(gv.Columns["MoldName"]);
            grid.Column_AllowEdit(gv.Columns["OutDate"]);
            grid.Column_AllowEdit(gv.Columns["OutQty"]);
            grid.Column_AllowEdit(gv.Columns["Remark"]);
            //grid.Column_AllowEdit(gv.Columns["CustCode"]);
            //grid.Column_AllowEdit(gv.Columns["ProdtCust"]);

            //코드 Visible
            //개발자

            gv.Columns["OutNo"].Visible = false;
            gv.Columns["OutRowSeq"].Visible = false;
            gv.Columns["DispSeq"].Visible = false;
            gv.Columns["OutQty"].Visible = false;
            gv.Columns["MoldCode"].Visible = false;

            gv.Columns["Remark"].MinWidth = 200;

            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;     //상태값
            gv.Columns["OutType"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            grid.SetCol_Type(gv.Columns["OutDate"], clsGrid.TypeList.TDate);  //출고일자
            grid.SetCol_Type(gv.Columns["OutQty"], clsGrid.TypeList.TFloat);  //출고량

            ////Caption 수정
            //gv.Columns["CustCode"].Caption = "매입처코드";
            //gv.Columns["CustName"].Caption = "매입처명";

            //이벤트
            gv.CellValueChanged += Event_CellValueChagned;        //셀값 수정 이벤트
            gv.ValidatingEditor += Gv_MatOut_ValidatingEditor;    //출고 처리된 행 수정 불가 이벤트
        }

        private void Event_CellValueChagned(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //셀값 변경 시 다른 Column 셀값 수정
            //GridView gv = sender as GridView;
            //DataRow drChanged = gv.GetDataRow(e.RowHandle);
            //string fieldName = e.Column.FieldName;
            //
            //if (e.Column.FieldName == gv.Columns[0].FieldName) return;
            //
            ////1. 상태값 수정
            //if (SelectType == "In")
            //{
            //    string str_Status = Convert.ToString(drChanged["Status"]);
            //
            //    if (str_Status != "신규" &&
            //        str_Status != "수정" &&
            //        str_Status != "삭제")
            //    {
            //        drChanged["Status"] = "신규";
            //    }
            //}
            //else if (SelectType == "Out")
            //{
            //    grid.Change_Status(gv, e.RowHandle);
            //}

            GridView gv = sender as GridView;
            DataRow drChanged = gv.GetDataRow(e.RowHandle);

            //셀값 수정 시 상태값 수정
            if (e.Column.FieldName == gv.Columns[0].FieldName) return;
            grid.Change_Status(gv, e.RowHandle);

            //1. 거래처 수정시
            if (e.Column.FieldName == "CustName") { drChanged["CustCode"] = drChanged["CustName"]; }
            //2. 금형수정시
            if (e.Column.FieldName == "MoldName")
            {
                grid.SetInfo_Mold(drChanged, Convert.ToString(e.Value));
            }

            gv.BestFitColumns();
        }

        private void Gv_MatOut_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            GridView gv = sender as GridView;
            DataRow dr = gv.GetFocusedDataRow();
            string fieldNm = gv.FocusedColumn.FieldName;

            //1. 출하조회상태 & OutNo가 빈값이 아니면 -> 출하일자, 거래처, 품목 수정불가
            if ( //SelectType == "Out"&&
                Convert.ToString(dr["OutNo"]) != ""
                && (fieldNm == "OutDate" || fieldNm == "MoldName"))
            {
                e.Valid = false;
                e.ErrorText = $"출고정보가 있는 행은 {gv.FocusedColumn.Caption}을/를 수정할 수 없습니다. \n 'ESC' 키를 눌러 원래 값으로 복구 하십시오";
            }

            if (fieldNm == "MoldName")
            {
                // 동일 금형 출하 불가
                //string MoldCode = dr["MoldCode"].ConvertString_Null_To_Empty();
                string MoldCode = e.Value.ConvertString_Null_To_Empty();
                //셀값 입력시 중복 검사
                if (!Chk_MoldOut(MoldCode))
                {
                    e.Valid = false;    //입력 불가 처리
                    e.ErrorText = $"이미 등록된 금형이(가) 있습니다.";  //경고 알림
                    //grid.Check_OverLap_CellValue(sender as GridView, e, "MoldName");
                }
            }
        }

        private bool Chk_MoldOut(string MoldCode)
        {
            MD_MoldOut mold = new MD_MoldOut();
            mold.MoldCode = MoldCode;

            DataTable dt = mold.Select_MoldOut_ByMoldCode();

            if (dt != null && dt.Rows.Count > 0)
            {
                return false;
            }

            return true;
        }

        #endregion GridView Setting End

        #endregion 디자인 아이템 생성 종료

        #region Event

        private void Frm_MD_MoldOut_Load(object sender, EventArgs e)
        {
            Func_Search();
        }

        #endregion Event End

        #region 조회 기능

        private void BtnSearch_Mold_Click(object sender, EventArgs e)
        {
            Func_Search();
        }

        private void Func_Search()
        {
            //금형출고 조회
            if (!Check_Search()) return;
            Gc_MoldOut.MainView = Gv_MoldOut;

            string startDate = Dt_Date.smtStartDateTimeValue.ConvertDate_To_Char10();
            string endDate = Dt_Date.smtEndDateTimeValue.ConvertDate_To_Char10();

            MD_MoldOut moldOut = new MD_MoldOut();
            moldOut.MoldCode = Lup_Mold.smtLookUpValue.ConvertString_Null_To_Empty();
            moldOut.CustCode = Lup_Cust.smtLookUpValue.ConvertString_Null_To_Empty();

            Gc_MoldOut.DataSource = moldOut.Select_MD_MoldOut(startDate, endDate);

            (Gc_MoldOut.MainView as GridView).BestFitColumns();
        }

        private bool Check_Search()
        {
            //조회시 확인 사항
            //true : 정상진행, false : 진행 Error
            DataTable dt = Gc_MoldOut.DataSource as DataTable;

            //1. 신규, 수정, 삭제내역 있는지 확인
            if (dt?.Select(@"Status = '신규'
                        OR Status = '수정'
                        OR Status = '삭제'").Length > 0)
            {
                return MessageBox.Show("작성내역이 사라집니다.\n다른항목을 진행하시겠습니까?", "Yes or No", MessageBoxButtons.YesNo) == DialogResult.Yes;
            }

            return true;
        }

        #endregion 조회 기능 End

        #region 저장 기능

        private void Btn_RowEdit(object sender, EventArgs e)
        {
            //행삽입, 추가, 삭제 버튼 클릭 이벤트
            //1. 이름의 마지막 3글자 가져오기
            SimpleButton btn = sender as SimpleButton;
            string txt_Act;
            GridView gv = Gv_MoldOut;

            //마우스 우클릭 했을때 sender
            if (sender.ToString() == "행추가" || sender.ToString() == "행삽입")
            {
                txt_Act = ((ToolStripItem)sender).Name.Substring(((ToolStripItem)sender).Name.Length - 3);
            }
            //기본 버튼 클릭 sender
            else
            {
                txt_Act = btn.Name.Substring(btn.Name.Length - 3);
            }

            //비활성화
            //if (SelectType == "In")
            //{
            //    MessageBox.Show("입고정보 조회상태에서 행조작 작업을 할 수 없습니다.");
            //    return;
            //}
            //else if (gv.FocusedRowHandle == 0
            //    && txt_Act == "Ins")
            //{
            //    MessageBox.Show("빈공간이 아닌 최상단에는 행을 추가할 수 없습니다.");
            //    return;
            //}

            //1. 자동입력값 가져오기
            string CustCode = Lup_Cust.smtLookUpValue.ConvertString_Null_To_Empty() == "ALL" ? "" : Lup_Cust.smtLookUpValue.ConvertString_Null_To_Empty();
            string MoldCode = Lup_Mold.smtLookUpValue.ConvertString_Null_To_Empty() == "ALL" ? "" : Lup_Mold.smtLookUpValue.ConvertString_Null_To_Empty();
            float remainQty = 0;

            //1-1 금형

            //1-3. 행 삽입 시 선택행 위에 위치한 행의 정보
            //출고일, 거래처
            DateTime OutDate = DateTime.Now;
            if (CustCode != "")
            {
                //if (txt_Act == "Ins" && gv.FocusedRowHandle > 0)
                if (txt_Act == "Ins" && gv.FocusedRowHandle >= 0)
                {
                    //OutDate = Convert.ToDateTime(gv.GetDataRow(gv.FocusedRowHandle - 1)["OutDate"]);
                    //CustCode = Convert.ToString(gv.GetDataRow(gv.FocusedRowHandle - 1)["CustCode"]);
                    OutDate = Convert.ToDateTime(gv.GetDataRow(gv.FocusedRowHandle)["OutDate"]);
                    CustCode = Convert.ToString(gv.GetDataRow(gv.FocusedRowHandle)["CustCode"]);
                }
                //행 추가 시 선택행의 입고일, 거래처
                else if (txt_Act == "Add" && gv.FocusedRowHandle >= 0)
                {
                    OutDate = Convert.ToDateTime(gv.GetDataRow(gv.FocusedRowHandle)["OutDate"]);
                    CustCode = Convert.ToString(gv.GetDataRow(gv.FocusedRowHandle)["CustCode"]);
                }
            }

            //2. 입력 데이터 설정
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (txt_Act != "Del")
            {
                dic.Add("Status", "신규");
                dic.Add("OutDate", OutDate);
                dic.Add("CustCode", CustCode);
                //dic.Add("CustName", CustCode);
                dic.Add("MoldCode", MoldCode);
                dic.Add("MoldName", MoldCode);
                dic.Add("OutQty", 0);
            }
            else
            {
                //멀티셀렉트 기능이 있을때 초기 삭제 로직 사용
                dic.Add("Status", "삭제");
            }

            //3. 행삽입, 추가, 삭제 프로세스 진행
            grid.Set_Row(Gc_MoldOut, dic, txt_Act);

            Gv_MoldOut.BestFitColumns();
        }


        private void BtnSave_Mold_Click(object sender, EventArgs e)
        {
            Func_Save();
        }

        private void Func_Save()
        {
            GridView gv = Gv_MoldOut;
            gv.PostEditor();
            gv.UpdateCurrentRow();

            //작성내역 저장
            DataTable dt = Gc_MoldOut.DataSource as DataTable;
            bool Success = false;

            //1. 데이터 불러온 상태인지 검사
            if (!Check_SaveClick(dt)) return;

            //2. 데이터 저장
            foreach (DataRow dr in dt.Rows)
            {
                string status = Convert.ToString(dr["Status"]);
                switch (status)
                {
                    case "신규": Success = Save_ToDatabase(dr, 'I'); break;
                    case "수정": Success = Save_ToDatabase(dr, 'U'); break;
                    case "삭제": Success = Save_ToDatabase(dr, 'D'); break;
                    default: Success = true; break;
                }

                if (!Success) break;
            }

            //상태값 초기화
            foreach (DataRow dr in dt.Rows)
            {
                if (Success)
                {
                    //상태값 삭제
                    dr["Status"] = "";
                }
                else
                {
                    //입고번호 삭제
                    if (Convert.ToString(dr["Status"]) == "신규")
                    {
                        dr["OutNo"] = "";
                    }
                }
            }

            if (Success)
            {
                MessageBox.Show("저장이 정상적으로 완료되었습니다.");

                Func_Search();
            }
            else
            {
                MessageBox.Show("저장 중 에러가 발생하였습니다.");
            }
        }

        private bool Check_SaveClick(DataTable dt)
        {
            //데이터 불러온 상태인지 검사
            if (dt == null) return false;

            int checkCnt = 0;

            foreach (DataRow dr in dt.Rows)
            {
                string status = Convert.ToString(dr["Status"]);
                string outType = dr["OutType"].ConvertString_Null_To_Empty();

                if (status == "신규" || status == "수정")
                {
                    if (outType == "") { MessageBox.Show("미입력한 처리구분이 있습니다."); return false; }
                    if (outType == "20" && dr["CustCode"].ConvertString_Null_To_Empty() == "") { MessageBox.Show("거래처가 선택되지 않은 반납건이 있습니다."); return false; }
                    if (dr["MoldCode"].ConvertString_Null_To_Empty() == "") { MessageBox.Show("미입력한 금형이 있습니다."); return false; }
                }

                if (status == "신규")
                {
                    checkCnt++;
                    //출고조회 상태시에만 추가 확인 조건
                    //if (SelectType == "Out")
                    {
                        if (Convert.ToString(dr["MoldCode"]) == "ALL") { MessageBox.Show("미입력한 금형이 있습니다."); return false; }
                    }

                    //공통확인 조건
                    //if (Convert.ToSingle(dr["OutQty"]) == 0) { MessageBox.Show("미입력한 수량이 있습니다."); return false; }
                }
                else if (status == "삭제")
                {
                    checkCnt++;
                    //삭제내역 있으면 물어보기
                    DialogResult dialogResult = MessageBox.Show("삭제내역이 있습니다. 삭제하시겠습니까?", "Delete OK Cancle", MessageBoxButtons.OKCancel);

                    if (dialogResult == DialogResult.Cancel)
                    {
                        return false;
                    }
                }
                else if (status == "수정")
                {
                    checkCnt++;
                }
            }

            if (checkCnt == 0) return false;

            return true;
        }


        private bool Save_ToDatabase(DataRow dr, char div)
        {
            //데이터베이스에 반영
            try
            {
                //미리 dr에 입력
                if (div == 'I')
                {
                    Create_No(dr);
                    Create_DispSeq(dr);
                }

                //1. 기본 셋팅
                MD_MoldOut moldOut = new MD_MoldOut();
                moldOut.OutNo = Convert.ToString(dr["OutNo"]);           //출고번호
                moldOut.OutRowSeq = dr["OutRowSeq"].ConvertInt_Null_To_Zero();       //출고순번
                moldOut.DispSeq = dr["DispSeq"].ConvertInt_Null_To_Zero();          //조회 순번
                moldOut.OutDate = dr["OutDate"].ConvertDate_To_Char8();                 //출고일자
                moldOut.CustCode = dr["CustCode"].ConvertString_Null_To_Empty();          //거래처
                moldOut.MoldCode = dr["MoldCode"].ConvertString_Null_To_Empty();          //금형
                moldOut.OutQty = dr["OutQty"].ConvertDecimal_Null_To_Zero();          //수량
                moldOut.Remark = dr["Remark"].ConvertString_Null_To_Empty();              //비고

                moldOut.InNo = Convert.ToString(dr["InNo"]);                //입고번호
                moldOut.InRowSeq = dr["InRowSeq"].ConvertInt_Null_To_Zero();   //입고 순번
                moldOut.OutType = dr["OutType"].ConvertInt_Null_To_Zero();        //처리구분

                //2. DB 적용
                if (div == 'I')
                {
                    //Insert
                    if (!moldOut.Ins_MoldOut()) { return false; }
                }
                else if (div == 'U')
                {
                    //Update
                    if (!moldOut.Upd_MoldOut()) { return false; }
                }
                else if (div == 'D')
                {
                    //Delete
                    if (!moldOut.Del_MoldOut()) { return false; }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }


        private void Create_No(DataRow dr)
        {
            cls_Database cDB = new cls_Database();
            int No_Seq = cDB.Get_Last_NoSeq("MD_MoldOut", "OutNo", dr["OutDate"].ConvertDate_To_Char8(), "MO");

            /*
             * Date가 같은데이터가 있는가?
             *      Yes : Status = "신규"인 항목이 더 있는가?
             *          Yes : 그중 No가 입력된 Row이 있는가?
             *              Yes : 해당 Row 중 거래처가 dr["CustCode"]가 같은항목이 있는가?
             *                  Yes : dr[No] = drComp[No]
             *                  No : No_Seq = LastSeq
             *              No : No_Seq = LastSeq
             *          No : No_Seq = LastSeq
             *      No: No_Seq = LastSeq
            */

            dr["OutNo"] = "MO" + dr["OutDate"].ConvertDate_To_Char8() + "-" + No_Seq.ToString("D4");

            foreach (DataRow drComp in dr.Table.Rows)
            {
                string status = Convert.ToString(drComp["Status"]);
                string OutNo = Convert.ToString(drComp["OutNo"]);
                if (drComp["OutDate"].ConvertDate_To_Char8() == dr["OutDate"].ConvertDate_To_Char8()
                    && status == "신규"
                    && OutNo != ""
                    //&& Convert.ToString(drComp["OutCustCode"]) == Convert.ToString(dr["OutCustCode"])
                    )
                {
                    dr["OutNo"] = drComp["OutNo"];
                }

                //현재행까지 왔는지 검사
                if (drComp == dr) break;
            }
        }

        private void Create_DispSeq(DataRow dr)
        {
            int disp = 1;
            /*
             * Date가 같은데이터가 있는가?
             *      Yes : Status = "신규"인 항목이 더 있는가?
             *          Yes : 그중 No가 입력된 Row이 있는가?
             *              Yes : drComp[InNo] == dr[InNo]
             *                  Yes : disp++;
             *                  No : disp = 유지
             *              No : disp = 유지
             *          No : disp = 유지
             *      No: disp = 유지
            */

            foreach (DataRow drComp in dr.Table.Rows)
            {
                //현재행까지 왔는지 검사
                if (drComp == dr) break;

                string status = Convert.ToString(drComp["Status"]);
                string OutNo = Convert.ToString(drComp["OutNo"]);
                if (drComp["OutDate"].ConvertDate_To_Char8() == dr["OutDate"].ConvertDate_To_Char8()
                    && status == "신규"
                    && OutNo != ""
                    //&& Convert.ToString(drComp["OutCustCode"]) == Convert.ToString(dr["OutCustCode"])
                    )
                {
                    disp++;
                }
            }

            dr["DispSeq"] = disp;
            dr["OutRowSeq"] = disp;
        }

        #endregion 저장 기능 End

        private void cmdExcel_Click(object sender, EventArgs e)
        {
            clsFunction function = new clsFunction();
            function.ExportExcel(Gv_MoldOut);
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}