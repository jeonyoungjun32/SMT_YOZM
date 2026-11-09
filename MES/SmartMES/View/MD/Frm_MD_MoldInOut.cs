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
    public partial class Frm_MD_MoldInOut : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        private string SelectType = "Out";

        public Frm_MD_MoldInOut()
        {
            InitializeComponent();
            SetDesign();
        }

        #region 디자인 아이템 생성

        private void SetDesign()
        {
            Set_SearchLup();
            Set_GvOut(Gv_MoldInOut);
        }

        private void Set_SearchLup()
        {
            DateTime startDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-01"));
            DateTime endDate = Convert.ToDateTime(startDate.AddDays(40).ToString("yyyy-MM-01"));
            endDate = endDate.AddDays(-1);

            Dt_Date.smtStartDateTimeValue = startDate;
            Dt_Date.smtEndDateTimeValue = endDate;

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

            MD_Mold mold = new MD_Mold();
            RepositoryItemSearchLookUpEdit lup_Mold = rpo.Create_RepositoryItemSearchLookUp(mold.Select_Mold_Mgmt_No(), "코드", "금형명");
            lup_Mold.BestFitMode = BestFitMode.BestFitResizePopup;

            //Column 셋팅
            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "입출고번호", "InOutNo");
            grid.Create_GvColumn(gv, "입출고순번", "InOutRowSeq");
            grid.Create_GvColumn(gv, "조회순번", "DispSeq");
            grid.Create_GvColumn(gv, "입고일자", "InDate");
            grid.Create_GvColumn(gv, "출고일자", "OutDate");

            //grid.Create_GvColumn(gv, "입고처", "CustCode");


            //CodeOption에 따른 Column 생성부
            grid.Create_GvColumn(gv, "프로젝트명", "MoldPjtNo");
            grid.Create_GvColumn(gv, "품명(금형명)", "MoldName", lup_Mold);
            grid.Create_GvColumn(gv, "금형코드", "MoldCode");
            grid.Create_GvColumn(gv, "금형코드", "MoldSN");
            grid.Create_GvColumn(gv, "차수", "Chasu");
            grid.Create_GvColumn(gv, "고객사", "CustCode", lup_Cust);
            //grid.Create_GvColumn(gv, "출고처", "Prodt_CustCode");
            grid.Create_GvColumn(gv, "출고처", "ShipCustCode");
            grid.Create_GvColumn(gv, "출고내용", "Remark");
            grid.Create_GvColumn(gv, "시작Shot", "InitShot");
            grid.Create_GvColumn(gv, "입고량", "InQty");
            grid.Create_GvColumn(gv, "단가", "InPrice");
            grid.Create_GvColumn(gv, "금액", "InAmt");

            grid.Create_GvColumn(gv, "등록자", "CreId", lup_Emp);
            grid.Create_GvColumn(gv, "등록일시", "CreDt");
            grid.Create_GvColumn(gv, "수정자", "ModId", lup_Emp);
            grid.Create_GvColumn(gv, "수정일시", "ModDt");

            //기타
            gv.Columns["InDate"].ColumnEdit = rpo.Create_rpoDate();

            //셀값 변경 허용
            grid.Column_AllowEdit(gv.Columns["InDate"]);
            grid.Column_AllowEdit(gv.Columns["OutDate"]);
            grid.Column_AllowEdit(gv.Columns["CustCode"]);
            grid.Column_AllowEdit(gv.Columns["MoldName"], true);
            grid.Column_AllowEdit(gv.Columns["InitShot"]);
            grid.Column_AllowEdit(gv.Columns["InQty"]);
            grid.Column_AllowEdit(gv.Columns["InPrice"]);
            grid.Column_AllowEdit(gv.Columns["InAmt"]);
            grid.Column_AllowEdit(gv.Columns["Remark"]);
            grid.Column_AllowEdit(gv.Columns["ShipCustCode"]);

            gv.Columns["Remark"].MinWidth = 200;
            gv.Columns["MoldCode"].Visible = false;
            gv.Columns["InitShot"].Visible = false;
            gv.Columns["InPrice"].Visible = false;
            gv.Columns["InOutNo"].Visible = false;
            gv.Columns["InOutRowSeq"].Visible = false;
            gv.Columns["DispSeq"].Visible = false;
            gv.Columns["InPrice"].Visible = false;
            gv.Columns["InAmt"].Visible = false;
            //gv.Columns["Prodt_CustCode"].Visible = false;

            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;         //상태값
            grid.SetCol_Type(gv.Columns["InDate"], clsGrid.TypeList.TDate);     //일고일자
            grid.SetCol_Type(gv.Columns["InQty"], clsGrid.TypeList.TInt);    //입고수량
            grid.SetCol_Type(gv.Columns["InitShot"], clsGrid.TypeList.TInt);      //Shot
            grid.SetCol_Type(gv.Columns["InPrice"], clsGrid.TypeList.TInt);      //단가
            grid.SetCol_Type(gv.Columns["InAmt"], clsGrid.TypeList.TInt);      //금액

            gv.GridControl.ProcessGridKey += Event_ProcessGridKey;
            gv.CellValueChanged += Event_CellValueChagned;    //셀값 수정 Event
            gv.ValidatingEditor += Event_ValidatingEditor;            //셀값 수정시 변경불가 Event

            gv.Columns.Remove(gv.Columns["In_No"]);
            gv.Columns.Remove(gv.Columns["In_Row_Seq"]);
        }

        #region 행 추가, 삽입, 삭제

        private void Event_ProcessGridKey(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case (Keys.Alt | Keys.I): Btn_MoldRow_Ins.PerformClick(); break;  //행삽입
                //case (Keys.Alt | Keys.A): Btn_Row_Add.PerformClick(); break;  //행추가
                case (Keys.Alt | Keys.D): Btn_MoldRow_Del.PerformClick(); break; //행삭제
            }
        }

        private void Btn_RowEdit(object sender, EventArgs e)
        {
            //행삽입, 추가, 삭제 버튼 클릭 이벤트
            //1. 이름의 마지막 3글자 가져오기
            SimpleButton btn = sender as SimpleButton;
            string txt_Act;
            GridView gv = Gv_MoldInOut;

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

            //1. 자동입력값 가져오기
            string MoldCode = Lup_Mold.smtLookUpValue.ConvertString_Null_To_Empty() == "ALL" ? "" : Lup_Mold.smtLookUpValue.ConvertString_Null_To_Empty();
            float price = 0;
            DataRow dr = gv.GetDataRow(gv.FocusedRowHandle);
            string CustCode = "";
            string OutDate = DateTime.Now.ToString("d");

            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (txt_Act != "Del")
            {
                dic.Add("Status", "신규");
                dic.Add("OutDate", OutDate);
                dic.Add("CustCode", CustCode);
                dic.Add("InPrice", price);
                dic.Add("InQty", 1);
            }
            else
            {
                //멀티셀렉트 기능이 있을때 초기 삭제 로직 사용
                dic.Add("Status", "삭제");
            }

            //3. 행삽입, 추가, 삭제 프로세스 진행
            grid.Set_Row(Gc_MoldInOut, dic, txt_Act);
        }

        #endregion 행 추가, 삽입, 삭제 End

        #region 셀 변경 Event

        private void Event_CellValueChagned(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //셀값 변경 시 다른 Column 셀값 수정
            GridView gv = sender as GridView;
            string fieldName = e.Column.FieldName;
            DataRow drChanged = gv.GetDataRow(e.RowHandle);

            //셀값 수정 시 상태값 수정
            if (e.Column.FieldName == gv.Columns[0].FieldName) return;
            grid.Change_Status(gv, e.RowHandle);

            //1. 거래처 수정시
            //if (fieldName == "CustCode")
            //{
            //drChanged["Cust_Nm"] = drChanged["Cust_Nm"];

            //}

            //2. 금형 수정시
            if (fieldName == "MoldName")
            {
                grid.SetInfo_Mold(drChanged, Convert.ToString(e.Value));
            }

            //3. 입고일자 수정시
            if (fieldName == "InDate")
            {
            }


            gv.BestFitColumns();
        }

        #endregion 셀 변경 Event End

        private void Event_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            //GridView gv = sender as GridView;
            //DataRow dr = gv.GetFocusedDataRow();

            ////1. 입고조회상태 & OrderNo가 빈값이 아니면 -> 구분, 입고일자, 거래처, 품명 수정불가
            //if (Convert.ToString(dr["In_No"]) != ""
            //    && (gv.FocusedColumn.FieldName == "Mat_Div_Cd" ||
            //        gv.FocusedColumn.FieldName == "CustCode" ||
            //        gv.FocusedColumn.FieldName == "MoldCode" ||
            //        gv.FocusedColumn.FieldName == "CustCode"))
            //{
            //        e.Valid = false;
            //        e.ErrorText = $"입고정보가 있는 행은 {gv.FocusedColumn.Caption}을/를 수정할 수 없습니다.";
            //}
        }

        #endregion GridView Setting End

        #endregion 디자인 아이템 생성 종료

        #region Event

        private void Frm_MD_MoldInOut_Load(object sender, EventArgs e)
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
            if (!Check_Search()) return;

            MD_MoldInOut Inout = new MD_MoldInOut();
            Inout.MoldCode = Lup_Mold.smtLookUpValue.ConvertString_Null_To_Empty() == "ALL" ? "" : Lup_Mold.smtLookUpValue.ConvertString_Null_To_Empty();

            string startDate = Dt_Date.smtStartDateTimeValue.ConvertDate_To_Char10();
            string endDate = Dt_Date.smtEndDateTimeValue.ConvertDate_To_Char10();

            Gc_MoldInOut.DataSource = Inout.Select_MoldInOut(startDate, endDate);

            Gv_MoldInOut.BestFitColumns();
        }

        private bool Check_Search()
        {
            //조회시 확인 사항
            //true : 정상진행, false : 진행 Error
            DataTable dt = Gc_MoldInOut.DataSource as DataTable;

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

        private void BtnSave_Mold_Click(object sender, EventArgs e)
        {
            Func_Save();
        }

        private void Func_Save()
        {
            GridView gv = Gv_MoldInOut;
            gv.PostEditor();
            gv.UpdateCurrentRow();

            //작성내역 저장
            DataTable dtMatIn = Gc_MoldInOut.DataSource as DataTable;
            bool Success = false;

            //1. 데이터 불러온 상태인지 검사
            if (!Check_SaveClick(dtMatIn)) return;

            //2. 데이터 저장
            foreach (DataRow dr in dtMatIn.Rows)
            {
                string status = Convert.ToString(dr["Status"]);
                switch (status)
                {
                    case "신규": Success = Save_ToDatabase(dr, 'I'); break;
                    case "수정": Success = Save_ToDatabase(dr, 'U'); break;
                    case "삭제": Success = Save_ToDatabase(dr, 'D'); break;
                    default: Success = true; break;
                }

                if (!Success)
                {
                    //입고번호 삭제
                    if (Convert.ToString(dr["Status"]) == "신규")
                    {
                        dr["InOutNo"] = "";
                    }
                    break;
                }
                else
                {
                    dr["Status"] = "";
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

            //삭제내역 있으면 물어보기
            foreach (DataRow dr in dt.Rows)
            {
                string status = Convert.ToString(dr["Status"]);
                if (status == "삭제")
                {
                    checkCnt++;
                    DialogResult dialogResult = MessageBox.Show("삭제내역이 있습니다. 삭제하시겠습니까?", "Delete OK Cancle", MessageBoxButtons.OKCancel);

                    if (dialogResult == DialogResult.Cancel)
                    {
                        return false;
                    }
                    else
                    {
                        break;
                    }
                }
                else if (status == "수정")
                {
                    checkCnt++;
                }
                else if (status == "신규")
                {
                    checkCnt++;
                }
            }

            //신규항목 미입력 검사
            foreach (DataRow dr in dt.Rows)
            {
                string status = Convert.ToString(dr["Status"]);
                if (status == "신규")
                {
                    if (Convert.ToString(dr["CustCode"]) == "ALL" || Convert.ToString(dr["CustCode"]) == "") { MessageBox.Show("미입력한 거래처가 있습니다."); return false; }
                    else if (Convert.ToString(dr["MoldName"]) == "ALL" || Convert.ToString(dr["MoldName"]) == "") { MessageBox.Show("미입력한 금형이 있습니다."); return false; }
                    else if (Convert.ToSingle(dr["InQty"]) == 0) { MessageBox.Show("미입력한 수량이 있습니다."); return false; }
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
                    Create_InNo(dr);
                    Create_DispSeq(dr);
                }

                //1. 기본 셋팅
                MD_MoldInOut Inout = new MD_MoldInOut();
                Inout.InOutNo = Convert.ToString(dr["InOutNo"]);                     //입출고번호
                Inout.InOutRowSeq = dr["InOutRowSeq"].ConvertInt_Null_To_Zero();   //입출고 순번
                Inout.DispSeq = dr["DispSeq"].ConvertInt_Null_To_Zero();             //조회 순번
                Inout.InDate = dr["InDate"].ConvertDate_To_Char8();                  //입고일자
                Inout.OutDate = dr["OutDate"].ConvertDate_To_Char8();                //출고일자
                Inout.CustCode = Convert.ToString(dr["CustCode"]);                       //거래처
                Inout.ShipCustCode = dr["ShipCustCode"].ConvertString_Null_To_Empty();  // 출고처
                Inout.MoldCode = Convert.ToString(dr["MoldCode"]);                       //품목
                Inout.InQty = dr["InQty"].ConvertInt_Null_To_Zero();                 //수량
                Inout.InShot = dr["InitShot"].ConvertInt_Null_To_Zero();             //Shot
                Inout.InPrice = dr["InPrice"].ConvertDecimal_Null_To_Zero();         //단가
                Inout.InAmt = Inout.InQty * Inout.InPrice;                          //금액
                Inout.Remark = Convert.ToString(dr["Remark"]);                               //비고

                MD_Mold mold = new MD_Mold();
                mold.MoldCode = dr["MoldCode"].ConvertString_Null_To_Empty();
                mold.InitShot = dr["InitShot"].ConvertInt_Null_To_Zero();

                //2. DB 적용
                if (div == 'I')
                {
                    //Insert
                    if (!Inout.Insert_MoldInOut())
                    {
                        //저장 중 에러 발생시
                        return false;
                    }

                    // 금형입고 시 초기shot 저장
                    mold.Update_InitShot();
                    //추가 변경사항 private 따로 만들기
                }
                else if (div == 'U')
                {
                    //Update
                    if (!Inout.Update_MoldInOut())
                    {
                        //저장 중 에러 발생시
                        return false;
                    }

                    // 금형입고 시 초기shot 저장
                    mold.Update_InitShot();
                }
                else if (div == 'D')
                {
                    //Delete
                    if (!Inout.Delete_MoldInOut())
                    {
                        //저장 중 에러 발생시
                        return false;
                    }

                    //추가 변경사항 private 따로 만들기
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        private void Create_InNo(DataRow dr)
        {
            cls_Database cDB = new cls_Database();
            int No_Seq = cDB.Get_Last_NoSeq("MD_MoldInOut", "InOutNo", dr["OutDate"].ConvertDate_To_Char8(), "DI");

            /*
             * InDate가 같은데이터가 있는가?
             *      Yes : Status = "신규"인 항목이 더 있는가?
             *          Yes : 그중 No가 입력된 Row이 있는가?
             *              Yes : 해당 Row 중 거래처가 dr["CustCode"]가 같은항목이 있는가?
             *                  Yes : dr[No] = drComp[No]
             *                  No : No_Seq = LastSeq
             *              No : No_Seq = LastSeq
             *          No : No_Seq = LastSeq
             *      No: No_Seq = LastSeq
            */

            dr["InOutNo"] = "DI" + dr["OutDate"].ConvertDate_To_Char8() + "-" + No_Seq.ToString("D4");

            foreach (DataRow drComp in dr.Table.Rows)
            {
                string status = Convert.ToString(drComp["Status"]);
                string in_No = Convert.ToString(drComp["InOutNo"]);
                if (drComp["OutDate"].ConvertDate_To_Char8() == dr["OutDate"].ConvertDate_To_Char8()
                    && status == "신규"
                    && in_No != ""
                    && Convert.ToString(drComp["CustCode"]) == Convert.ToString(dr["CustCode"]))
                {
                    dr["InOutNo"] = drComp["InOutNo"];
                }

                //현재행까지 왔는지 검사
                if (drComp == dr) break;
            }
        }

        private void Create_DispSeq(DataRow dr)
        {
            int disp = 1;
            /*
             * InDate가 같은데이터가 있는가?
             *      Yes : Status = "신규"인 항목이 더 있는가?
             *          Yes : 그중 No가 입력된 Row이 있는가?
             *              Yes : drComp[In_No] == dr[In_No]
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
                string in_No = Convert.ToString(drComp["InOutNo"]);
                string cust = Convert.ToString(drComp["CustCode"]);
                if (drComp["OutDate"].ConvertDate_To_Char8() == dr["OutDate"].ConvertDate_To_Char8()
                    && status == "신규"
                    && in_No != ""
                    && Convert.ToString(drComp["CustCode"]) == Convert.ToString(dr["CustCode"]))
                {
                    disp++;
                }
            }

            dr["DispSeq"] = disp;
            dr["InOutRowSeq"] = disp;
        }

        #endregion 저장 기능 End

        private void cmdExcel_Click(object sender, EventArgs e)
        {
            clsFunction function = new clsFunction();
            function.ExportExcel(Gv_MoldInOut);
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}