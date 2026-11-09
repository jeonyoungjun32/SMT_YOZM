using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.CM;
using SmartMES.Models.MD;
using SmartMES.View.MD.Rpt;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SmartMES.View.MD
{
    public partial class Frm_MD_Mold : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();
        private string[] ItemDiv = WrGlobal.Business_ItemType;

        public Frm_MD_Mold()
        {
            InitializeComponent();
            SetDesign();

            //바코드스캔 이벤트
            Text_Barcode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Text_Barcode_KeyPress);
        }

        #region 디자인 아이템 생성

        private void SetDesign()
        {
            Set_SearchLookUp();
            Set_GridView();

            //이미지
            pictureEdit1.Properties.ExportMode = ExportMode.Value;
            pictureEdit1.Properties.PictureStoreMode = PictureStoreMode.ByteArray;
            pictureEdit2.Properties.ExportMode = ExportMode.Value;
            pictureEdit2.Properties.PictureStoreMode = PictureStoreMode.ByteArray;

            //폼 닫을때 검사 작성된 내역 있는지 검사
            this.FormClosing += new FormClosingEventHandler((sender, e) => {
                e.Cancel = false;
                if (grid.Check_Gv_FormClosing(Gv_Mold, "Status"))
                {
                    e.Cancel = true;
                }
            });
        }

        private void Set_SearchLookUp()
        {
            cls_Lookup clsLup = new cls_Lookup();
            MD_Mold mold = new MD_Mold();

            DataTable dt_Mold = mold.Select_Mold_Mgmt_No(true);
            clsLup.Create_RepositoryItemSearchLookUp(lupMold, dt_Mold, "코드", "금형명");
            lupMold.smtLookUpValue = "ALL";
        }

        #region GridView Setting

        private void Set_GridView()
        {
            SetGrid_Mold(Gv_Mold);
            SetGrid_MoldItem(Gv_MoldItem);
        }

        private void SetGrid_Mold(GridView gv)
        {
            //GvMachine Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.IndicatorWidth = 50;                     //순번 너비
            //헤더 멀티라인 설정
            gv.OptionsView.AllowHtmlDrawHeaders = true;
            gv.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            //행높이(멀티라인)
            gv.OptionsView.RowAutoHeight = true;
            //검색창
            //gv.OptionsFind.AlwaysVisible = true;

            //Repository 설정
            CM_Cust cust = new CM_Cust();
            RepositoryItemSearchLookUpEdit lup_Cust = rpo.Create_RepositoryItemSearchLookUp(cust.Select_Cust_List(new string[] { }), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Emp = rpo.Create_RepositoryItemSearchLookUp(code.GetDataTableUser(false), "코드", "명칭");

            CM_CateItem cm = new CM_CateItem();
            RepositoryItem lup_Zone = rpo.Create_RepositoryItem(cm.SelectClassItemLookUp(WrGlobal.CorpID, "MD50"), "분류코드", "분류명");

            // 금형등급
            //sc.Cm_Class = "060";
            //RepositoryItemSearchLookUpEdit lup_Grade = rpo.Create_RepositoryItemSearchLookUp(sc.Select_CmCd(false), "Cm_Cd", "Cm_Nm");

            RepositoryItemCheckEdit chk = new RepositoryItemCheckEdit();
            chk.ValueChecked = "Y";
            chk.ValueUnchecked = "N";
            chk.NullStyle = StyleIndeterminate.Unchecked;
            chk.AllowGrayed = false;

            //Column 생성
            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "금형관리번호", "MoldMgmtNo");
            grid.Create_GvColumn(gv, "프로젝트명", "MoldPjtNo");
            grid.Create_GvColumn(gv, "품명(금형명)", "MoldName");
            grid.Create_GvColumn(gv, "금형코드", "MoldSN");
            grid.Create_GvColumn(gv, "금형코드", "MoldCode");
            grid.Create_GvColumn(gv, "차수", "Chasu");
            //grid.Create_GvColumn(gv, "금형등급", "Mold_Grade", lup_Grade);

            grid.Create_GvColumn(gv, "실보유처", "HoldCust", lup_Cust);           // 거래처정보에 외주로 등록된 항목
            grid.Create_GvColumn(gv, "동", "Zone", lup_Zone);
            grid.Create_GvColumn(gv, "구역No", "LocNo");
            grid.Create_GvColumn(gv, "고객사명", "SupplyCust", lup_Cust);
            grid.Create_GvColumn(gv, "제작처명", "ProdtCust");
            grid.Create_GvColumn(gv, "금형규격", "MoldStd");
            grid.Create_GvColumn(gv, "금형중량", "MoldWeight");
            grid.Create_GvColumn(gv, "FAMILY", "MoldCavity");
            grid.Create_GvColumn(gv, "CAVITY", "MoldCavityText");

            grid.Create_GvColumn(gv, "최종점검Shot", "RepairShot");

            grid.Create_GvColumn(gv, "시작Shot", "InitShot");
            grid.Create_GvColumn(gv, "누적Shot", "AccumulateShot");
            grid.Create_GvColumn(gv, "정기점검 비율(%)", "ChkRate");
            grid.Create_GvColumn(gv, "특수부품", "SpecialParts");

            grid.Create_GvColumn(gv, "최종생산일자", "Last_Rst_Date");
            //grid.Create_GvColumn(gv, "최종점검일자", "Last_Rst_Date");
            grid.Create_GvColumn(gv, "사용여부", "UseYn", chk);
            grid.Create_GvColumn(gv, "등록자", "CreId", lup_Emp);
            grid.Create_GvColumn(gv, "등록일시", "CreDt");
            grid.Create_GvColumn(gv, "수정자", "ModId", lup_Emp);
            grid.Create_GvColumn(gv, "수정일시", "ModDt");

            //grid.Create_GvColumn(gv, "적용TON", "ApplyEquip");
            //grid.Create_GvColumn(gv, "금형형식", "MoldForm");
            //grid.Create_GvColumn(gv, "성형품중량", "Weight");
            //grid.Create_GvColumn(gv, "투입자재", "FigurationGrade");
            //grid.Create_GvColumn(gv, "금형품목수", "MoldItemCount");

            //grid.Create_GvColumn(gv, "제작일", "ProdtDate");
            //grid.Create_GvColumn(gv, "폐기일", "DisposalDate");
            //grid.Create_GvColumn(gv, "제작가", "ProdtAmount");
            //grid.Create_GvColumn(gv, "GATE방식", "GateMethod");
            //grid.Create_GvColumn(gv, "수축율", "ContractRate");
            //grid.Create_GvColumn(gv, "SPRUE", "Sprue");
            //grid.Create_GvColumn(gv, "금형보관장소", "Keep_Place");
            //grid.Create_GvColumn(gv, "최근세척/점검Shot", "RepairShot");
            //grid.Create_GvColumn(gv, "진행타수", "Shot");
            //grid.Create_GvColumn(gv, "최근세척/점검일", "RepairDate");
            //grid.Create_GvColumn(gv, "대상유무", "Repair_Target");
            //grid.Create_GvColumn(gv, "대상구분", "Repair_Devide");
            //grid.Create_GvColumn(gv, "관리등급", "Manage_Rating");

            //Column 단일 설정

            gv.Columns["MoldCode"].ColumnEdit = rpo.Create_rpoTxt_Length(10);
            gv.Columns["MoldMgmtNo"].ColumnEdit = rpo.Create_rpoTxt_Length(30);
            gv.Columns["MoldSN"].ColumnEdit = rpo.Create_rpoTxt_Length(30);
            //gv.Columns["ApplyEquip"].ColumnEdit = rpo.Create_rpoTxt_Length(50);
            gv.Columns["MoldStd"].ColumnEdit = rpo.Create_rpoTxt_Length(50);
            gv.Columns["MoldCavityText"].ColumnEdit = rpo.Create_rpoTxt_Length(10);
            //gv.Columns["MoldCavity"].ColumnEdit = rpo.Create_rpoTxt_Numeric();
            //gv.Columns["Weight"].ColumnEdit = rpo.Create_rpoTxt_Numeric();
            gv.Columns["ProdtCust"].ColumnEdit = rpo.Create_rpoTxt_Length(20);
            //gv.Columns["MoldItemCount"].ColumnEdit = rpo.Create_rpoTxt_Numeric();
            //gv.Columns["FigurationGrade"].ColumnEdit = rpo.Create_rpoTxt_Length(30);   //투입 자재/소재
            //gv.Columns["MoldForm"].ColumnEdit = rpo.Create_rpoTxt_Length(50);
            //gv.Columns["ProdtDate"].ColumnEdit = rpo.Create_rpoDate();
            //gv.Columns["DisposalDate"].ColumnEdit = rpo.Create_rpoDate();
            //gv.Columns["ProdtAmount"].ColumnEdit = rpo.Create_rpoTxt_Numeric();
            //gv.Columns["GateMethod"].ColumnEdit = rpo.Create_rpoTxt_Length(30);
            //gv.Columns["ContractRate"].ColumnEdit = rpo.Create_rpoTxt_Numeric();
            //gv.Columns["Sprue"].ColumnEdit = rpo.Create_rpoTxt_Numeric();
            //gv.Columns["InitShot"].ColumnEdit = rpo.Create_rpoTxt_Numeric();
            //gv.Columns["LifeShot"].ColumnEdit = rpo.Create_rpoTxt_Numeric();

            //셀값 변경 허용
            //자동채번이 아닐 경우 코드값 편집 허용
            if (WrGlobal.ProcOption["BS"]["Mold_AutoNo"] != "Y")
            {
                grid.Column_AllowEdit(gv.Columns["MoldCode"]);
            }
            grid.Column_AllowEdit(gv.Columns["MoldMgmtNo"]);
            grid.Column_AllowEdit(gv.Columns["MoldPjtNo"]);
            grid.Column_AllowEdit(gv.Columns["Chasu"]);
            grid.Column_AllowEdit(gv.Columns["MoldSN"]);
            grid.Column_AllowEdit(gv.Columns["MoldName"]);
            grid.Column_AllowEdit(gv.Columns["HoldCust"]);
            grid.Column_AllowEdit(gv.Columns["SupplyCust"]);
            grid.Column_AllowEdit(gv.Columns["ProdtCust"]);
            grid.Column_AllowEdit(gv.Columns["ApplyEquip"]);
            grid.Column_AllowEdit(gv.Columns["MoldStd"]);
            grid.Column_AllowEdit(gv.Columns["MoldItemCount"]);
            grid.Column_AllowEdit(gv.Columns["MoldCavity"]);
            grid.Column_AllowEdit(gv.Columns["Weight"]);
            grid.Column_AllowEdit(gv.Columns["MoldWeight"]);
            grid.Column_AllowEdit(gv.Columns["UseYn"]);
            grid.Column_AllowEdit(gv.Columns["InitShot"]);
            grid.Column_AllowEdit(gv.Columns["Zone"]);
            grid.Column_AllowEdit(gv.Columns["LocNo"]);
            grid.Column_AllowEdit(gv.Columns["MoldCavityText"]);
            grid.Column_AllowEdit(gv.Columns["MoldGrade"]);
            grid.Column_AllowEdit(gv.Columns["SpecialParts"]);

            //셀값 정렬
            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;     //상태
            //gv.Columns["Manage_Rating"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;     //관리등급
            grid.SetCol_Type(gv.Columns["MoldItemCount"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["MoldCavity"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["ProdtAmount"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["LocNo"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["Weight"], clsGrid.TypeList.TFloat);
            grid.SetCol_Type(gv.Columns["MoldWeight"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["InitShot"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["LifeShot"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["AccumulateShot"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["StdShot"], clsGrid.TypeList.TInt);
            grid.SetCol_Type(gv.Columns["ChkRate"], clsGrid.TypeList.TInt);
            //grid.SetCol_Type(gv.Columns["ProdtDate"], grid.TypeList.TDate);
            //grid.SetCol_Type(gv.Columns["DisposalDate"], grid.TypeList.TDate);
            //grid.SetCol_Type(gv.Columns["Sprue"], grid.TypeList.TFloat);
            //grid.SetCol_Type(gv.Columns["ContractRate"], grid.TypeList.TFloat);
            //grid.SetCol_Type(gv.Columns["RepairShot"], grid.TypeList.TInt);
            //grid.SetCol_Type(gv.Columns["RepairDate"], grid.TypeList.TDate);

            gv.Columns["MoldCode"].Visible = false;

            // 최종생산일자 우측 고정
            //gv.Columns["Last_Rst_Date"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;

            //이벤트 설정
            grid.RowNumber_Indicator(gv);    //행번호 표기
            gv.RowStyle += grid.RowStyle_ChangeBg_ByStatus;           //삭제시 배경색 지정
            gv.CellValueChanged += Event_CellValueChanged;            //셀값 수정시 상태값 수정
            gv.GridControl.ProcessGridKey += Event_ProcessGridKey;    //행추가, 삽입, 삭제 단축키기능 부여
            gv.ValidatingEditor += Event_ValidatingEditor;            //코드 입력시 중복 확인
            //grid.SetInfo_ByObject(gv);
            gv.FocusedRowObjectChanged += Gv_FocusedRowObjectChanged;   //Row 변경시 하위항목 조회
            gv.RowCellStyle += Gv_Mold_RowCellStyle;    // 최종생산일자 현재일자보다 1년 이상 시 붉은색 표시
        }

        private void Gv_Mold_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView gv = sender as GridView;
            if (gv == null) return;

            DataRow dr = gv.GetDataRow(e.RowHandle);
            if (dr == null) return;

            // 점검 비율
            int rate = gv.GetRowCellValue(e.RowHandle, "Chk_Rate").ConvertInt_Null_To_Zero();

            if (e.Column != null && e.Column.FieldName == "Chk_Rate" && rate >= 80)
            {
                e.Appearance.BackColor = Color.DarkRed;   // 배경색
                e.Appearance.ForeColor = Color.White;       // 글자색
            }

            //string Last_Rst_Date = dr["Last_Rst_Date"].ConvertDate_To_Char10();
            //
            //if (Last_Rst_Date == "") return;
            //
            //TimeSpan dateDiif = DateTime.Now.Date - Convert.ToDateTime(Last_Rst_Date);
            //
            //if (e.Column != null && e.Column.FieldName == "Last_Rst_Date" && dateDiif.Days >= 365)  // 1년이상이면 빨간색으로 변경
            //{
            //    e.Appearance.ForeColor = Color.Red;
            //}

        }

        private void Gv_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            GridView gv = sender as GridView;
            DataRow dr = gv.GetDataRow(e.RowHandle);
            if (dr == null) return;
            string mold_Cd = dr["MoldCode"].ConvertString_Null_To_Empty();

            //금형품목 조회
            Func_Search_Item(mold_Cd);
            //금형 이미지 조회
            Fun_Search_Image(mold_Cd);

        }

        private void Event_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;

            //셀값 수정 시 상태값 수정
            if (e.Column.FieldName == gv.Columns[0].FieldName) return;
            grid.Change_Status(gv, e.RowHandle);

            DataRow drCur = gv.GetDataRow(e.RowHandle);
            string fieldNm = e.Column.FieldName;

            if (fieldNm == "Mold_Grade")
            {
                //cmcd.Cm_Class = "060";
                //cmcd.Cm_Cd = e.Value.ConvertString_Null_To_Empty();
                //
                //drCur["Std_Shot"] = cmcd.Select_CmCd_ByCd()["Rmk"].ConvertInt_Null_To_Zero();
            }

            /*
            else if (fieldNm == "Prodt_Cust_Nm")
            {
                BS_Customer cust = new BS_Customer();
                DataRow drCust = cust.Select_Cust_Info(Convert.ToString(e.Value));

                if(drCust != null )
                {
                    drCur["Prodt_Cust_Cd"] = drCust["Cust_Cd"];
                    drCur["Prodt_Cust_Nm"] = drCust["Cust_Cd"];
                }
                else
                {
                    drCur["Prodt_Cust_Cd"] = "";
                    drCur["Prodt_Cust_Nm"] = "";
                }
            }
            */
            string zone = Convert.ToString(gv.GetRowCellValue(e.RowHandle, "Zone")).Trim();
            string locNo = Convert.ToString(gv.GetRowCellValue(e.RowHandle, "LocNo")).Trim();

            if (string.IsNullOrWhiteSpace(zone) || string.IsNullOrWhiteSpace(locNo))
                return;

            for (int i = 0; i < gv.RowCount; i++)
            {
                if (i == e.RowHandle) continue;

                string existZone = Convert.ToString(gv.GetRowCellValue(i, "Zone")).Trim();
                string existLocNo = Convert.ToString(gv.GetRowCellValue(i, "LocNo")).Trim();

                if (zone == existZone && locNo == existLocNo)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                        "이미 동일한 Zone / Loc No 조합이 존재합니다."
                    );

                    // 값 원복
                    gv.SetRowCellValue(e.RowHandle, e.Column, e.OldValue);
                    return;
                }
            }
        }

        #region 행 삽입, 추가, 삭제

        private void Event_ProcessGridKey(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case (Keys.Alt | Keys.I): Btn_MoldRow_Ins.PerformClick(); break;   //행삽입
                //case (Keys.Alt | Keys.A): Btn_MoldRow_Add.PerformClick(); break;   //행추가
                case (Keys.Alt | Keys.D): Btn_MoldRow_Del.PerformClick(); ; break; //행삭제
            }
        }

        private void Btn_RowEdit(object sender, EventArgs e)
        {
            //행삽입, 추가, 삭제 버튼 클릭 이벤트
            //1. 이름의 마지막 3글자 가져오기
            SimpleButton btn = sender as SimpleButton;
            string txt_Act = btn.Name.Substring(btn.Name.Length - 3);

            //2. 입력 데이터 설정
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (txt_Act != "Del")
            {
                //int newSeq = GetNextSeq(Gv_Mold, "MoldMgmtNo");
                //삽입, 추가 진행인 경우
                dic.Add("Status", "신규");
                //dic.Add("MoldMgmtNo", newSeq);
                dic.Add("UseYn", "Y");
            }
            else
            {
                //삭제 진행인경우
                dic.Add("Status", Convert.ToString(Gv_Mold.GetFocusedRowCellValue("Status")) == "삭제" ? "" : "삭제");
            }

            //3. 행삽입, 추가, 삭제 프로세스 진행
            grid.Set_Row(Gc_Mold, dic, txt_Act);
        }

        private int GetNextSeq(GridView view, string columnName)
        {
            int max = 0;

            for (int i = 0; i < view.DataRowCount; i++)
            {
                object val = view.GetRowCellValue(i, columnName);

                if (val != null && val != DBNull.Value)
                {
                    int num;
                    if (int.TryParse(val.ToString(), out num))
                        max = Math.Max(max, num);
                }
            }

            return max + 1;
        }

        #endregion 행 삽입, 추가, 삭제 End

        private void Event_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            clsGrid grid = new clsGrid();
            grid.Check_OverLap_CellValue(sender as GridView, e, "MoldCode");
        }

        private void SetGrid_MoldItem(GridView gv)
        {
            //GvMachine Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.IndicatorWidth = 50;                     //순번 너비

            //Repository 설정
            DataTable dt = code.GetMaterialList(ItemDiv, false).ToTable();
            RepositoryItemSearchLookUpEdit lup_Item = rpo.Create_RepositoryItemSearchLookUp(dt, "품목코드", "품명");
            RepositoryItemSearchLookUpEdit lup_Emp_No = rpo.Create_RepositoryItemSearchLookUp(code.GetDataTableUser(false), "코드", "명칭");

            //Column 생성
            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "금형관리번호", "MoldMgmtNo");
            //grid.Create_GvColumn_ByOption(gv, "제품코드 초기화");
            grid.Create_GvColumn(gv, "품목코드", "ItemCode");
            grid.Create_GvColumn(gv, "품명", "ItemName", lup_Item);
            grid.Create_GvColumn(gv, "사용여부", "UseYn", rpo.Create_rpoChk());
            grid.Create_GvColumn(gv, "등록자", "CreId", lup_Emp_No);
            grid.Create_GvColumn(gv, "등록일시", "CreDt");
            grid.Create_GvColumn(gv, "수정자", "ModId", lup_Emp_No);
            grid.Create_GvColumn(gv, "수정일시", "ModDt");

            //셀값 변경 허용
            //grid.Column_AllowEdit(gv.Columns["MoldMgmtNo"], false);
            //grid.Column_AllowEdit(gv.Columns["MoldName"], true);
            grid.Column_AllowEdit(gv.Columns["ItemCode"], false);
            grid.Column_AllowEdit(gv.Columns["Alias"], false);
            grid.Column_AllowEdit(gv.Columns["Size"], false);
            grid.Column_AllowEdit(gv.Columns["UnitCode"], false);
            grid.Column_AllowEdit(gv.Columns["ItemName"]);
            grid.Column_AllowEdit(gv.Columns["UseYn"]);

            //셀값 정렬
            gv.Columns["Status"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;     //상태

            //이벤트 설정
            grid.RowNumber_Indicator(gv);    //행번호 표기
            gv.RowStyle += grid.RowStyle_ChangeBg_ByStatus;           //삭제시 배경색 지정
            gv.CellValueChanged += Event_CellValueChanged;            //셀값 수정시 상태값 수정
            gv.GridControl.ProcessGridKey += Event_ProcessGridKey_MoldItem;    //행추가, 삽입, 삭제 단축키기능 부여
            gv.CellValueChanged += Event_CellValueChanged_AddItem;            //셀값 수정시 상태값 수정
            gv.ValidatingEditor += Gv_ValidatingEditor;
        }

        #region 행 삽입, 추가, 삭제

        private void Event_ProcessGridKey_MoldItem(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case (Keys.Alt | Keys.I): Btn_ItemRow_Ins.PerformClick(); break;   //행삽입
                //case (Keys.Alt | Keys.A): Btn_ItemRow_Add.PerformClick(); break;   //행추가
                case (Keys.Alt | Keys.D): Btn_ItemRow_Del.PerformClick(); ; break; //행삭제
            }
        }

        private void Btn_RowEdit_MoldItem(object sender, EventArgs e)
        {
            //행삽입, 추가, 삭제 버튼 클릭 이벤트
            //1. 이름의 마지막 3글자 가져오기
            SimpleButton btn = sender as SimpleButton;
            string txt_Act = btn.Name.Substring(btn.Name.Length - 3);

            //2. 입력 데이터 설정
            Dictionary<string, object> dic = new Dictionary<string, object>();

            DataRow drMold = Gv_Mold.GetFocusedDataRow();
            if (drMold == null) return;

            string mold_Cd = Convert.ToString(drMold["MoldCode"]);
            string mold_No = Convert.ToString(drMold["MoldMgmtNo"]);

            if (mold_Cd == "") return;

            if (txt_Act != "Del")
            {
                //삽입, 추가 진행인 경우
                dic.Add("Status", "신규");
                dic.Add("MoldCode", mold_Cd);
                dic.Add("MoldMgmtNo", mold_No);
                dic.Add("UseYn", "Y");
            }
            else
            {
                //삭제 진행인경우
                dic.Add("Status", Convert.ToString(Gv_MoldItem.GetFocusedRowCellValue("Status")) == "삭제" ? "" : "삭제");
            }

            //3. 행삽입, 추가, 삭제 프로세스 진행
            grid.Set_Row(Gc_MoldItem, dic, txt_Act);
        }

        #endregion 행 삽입, 추가, 삭제 End

        private void Event_CellValueChanged_AddItem(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;
            DataRow drChanged = gv.GetDataRow(e.RowHandle);

            if (e.Column.FieldName == "ItemName")
            {
                grid.SetInfo_Item(drChanged, Convert.ToString(e.Value));
            }
        }

        private void Gv_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            //품목 수정 방지(Update, Delete 정상화 위함)
            GridView gv = sender as GridView;
            DataRow drChanged = gv.GetFocusedDataRow();

            if(gv.FocusedColumn.FieldName == "ItemName")
            {
                if(Convert.ToString(drChanged["Status"]) != "신규")
                {
                    //품목 수정 불가처리
                    e.Valid = false;
                    e.ErrorText = "등록된 품목은 변경될 수 없습니다. 삭제 후 재등록 하세요";
                }
                else
                {
                    //신규등록 시 중복 확인
                    for (int idx = 0; idx < gv.RowCount; idx++)
                    {
                        DataRow drComp = gv.GetDataRow(idx);
                        if (drChanged == drComp) continue;

                        if(Convert.ToString(drComp["ItemCode"]) == Convert.ToString(e.Value))
                        {
                            e.Valid = false;
                            e.ErrorText = "중복된 품목이 있습니다.";

                            break;
                        }
                    }
                }
            }
        }

        #endregion GridView Setting End

        #endregion 디자인 아이템 생성 종료

        #region Event

        private void Frm_MD_Mold_Load(object sender, EventArgs e)
        {
            Func_Search_Mold();
        }

        #endregion Event End

        #region 조회 기능

        private void BtnSearch_Mold_Click(object sender, EventArgs e)
        {
            Func_Search_Mold();
        }

        private void Func_Search_Mold()
        {
            //1. 조회 검사
            if (!Check_Search()) return;

            //2. 금형조회
            MD_Mold mold = new MD_Mold();
            mold.MoldCode = lupMold.smtLookUpValue.ConvertString_Null_To_Empty() == "전체" ? "" : lupMold.smtLookUpValue.ConvertString_Null_To_Empty();

            Gc_Mold.DataSource = mold.Select_Mold();

            //SC_CmCd sc = new SC_CmCd();
            //sc.Cm_Class = "060";
            //DataTable dt = sc.Select_CmCd();

            GridView gv = Gv_Mold;
            int AccumulateShot = 0;
            int RepairShot = 0;

            for (int i = 0; i < gv.RowCount; i++)
            {
                DataRow dr = gv.GetDataRow(i);

                AccumulateShot = dr["AccumulateShot"].ConvertInt_Null_To_Zero();
                RepairShot = dr["RepairShot"].ConvertInt_Null_To_Zero();
                dr["Shot"] = AccumulateShot - RepairShot;

               // for (int j = 0; j < dt.Rows.Count - 1; j++)
               // {
               //     if (AccumulateShot < dt.Rows[j]["Attr"].ConvertInt_Null_To_Zero())
               //     {
               //         dr["Manage_Rating"] = dt.Rows[j]["Cm_Nm"];
               //         break;
               //     }
               //     dr["Manage_Rating"] = dt.Rows[dt.Rows.Count - 1]["Cm_Nm"];
               // }
            }

            Gv_Mold.BestFitColumns();   //Column 너비 조정
        }

        private bool Check_Search()
        {
            if ((Gv_Mold.GridControl.DataSource as DataTable)?.Select(@"Status = '신규'
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

        private void Func_Search_Item(string mold_Cd)
        {
            //1. 조회 검사
            if (!Check_Search_Item()) return;

            //2. 금형조회
            MD_MoldItem mi = new MD_MoldItem();
            mi.MoldCode = mold_Cd;

            Gc_MoldItem.DataSource = mi.Select_MD_MoldItem();

            Gv_MoldItem.BestFitColumns();   //Column 너비 조정
        }

        private bool Check_Search_Item()
        {
            if ((Gv_MoldItem.GridControl.DataSource as DataTable)?.Select(@"Status = '신규'
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

        private void Fun_Search_Image(string mold_Cd)
        {
            //금형이미지 조회
            MD_MoldImage mold = new MD_MoldImage();
            DataRow drMold = mold.Select_MD_MoldImageList(mold_Cd);

            pictureEdit1.EditValue = drMold?["MoldImage1"];
            pictureEdit2.EditValue = drMold?["MoldImage2"];
        }

        #endregion 조회 기능 End

        #region 저장 기능

        #region 금형저장

        private void BtnSave_Mold_Click(object sender, EventArgs e)
        {
            Func_Save_Mold();
        }

        private void Func_Save_Mold()
        {
            //작성내역 저장
            GridView gv = Gv_Mold;
            gv.PostEditor();
            gv.UpdateCurrentRow();

            //1. 데이터 불러온 상태인지 검사
            if (!Check_SaveClick(gv)) return;

            //2. 데이터 저장
            bool success = true;

            for (int idx = 0; idx < gv.RowCount; idx++)
            {
                DataRow dr = gv.GetDataRow(idx);
                string status = Convert.ToString(dr["Status"]);

                switch (status)
                {
                    case "신규": success = SaveMold_ToDatabase(dr, 'I'); break;
                    case "수정": success = SaveMold_ToDatabase(dr, 'U'); break;
                    case "삭제": success = SaveMold_ToDatabase(dr, 'D'); break;
                }

                if (!success) break;

                dr["Status"] = "";
            }

            //이미지저장
            PictureSave();

            // 선택한 row 저장
            int SaveFocusedRowInx = (Gc_Mold.MainView as GridView).FocusedRowHandle;


            //3. 정상절차 진행
            if (success)
            {
                MessageBox.Show("저장이 정상적으로 완료되었습니다.");
                Func_Search_Mold();
            }
            else
            {
                MessageBox.Show("저장 중 에러가 발생하였습니다.");
            }

            // 저장 후 선택한 row를 불러옴
            (Gc_Mold.MainView as GridView).FocusedRowHandle = SaveFocusedRowInx;
        }

        private bool Check_SaveClick(GridView gv)
        {
            //Item을 불러온 상태인지 검사
            if (gv == null) return false;

            for (int idx = 0; idx < gv.RowCount; idx++)
            {
                DataRow dr = gv.GetDataRow(idx);
                string status = Convert.ToString(dr["Status"]);

                if (status == "신규")
                {
                    if (Convert.ToString(dr["MoldName"]) == "") { MessageBox.Show("금형명이 입력되지 않았습니다."); return false; }
                    if (Convert.ToString(dr["MoldSN"]) == "") { MessageBox.Show("금형코드가 입력되지 않았습니다."); return false; }
                    //if (Convert.ToString(dr["MoldPjtNo"]) == "") { MessageBox.Show("프로젝트명이 입력되지 않았습니다."); return false; }
                    //if (Convert.ToString(dr["MoldItemCount"]) == "") { MessageBox.Show("품목수가 입력되지 않았습니다."); return false; }
                    //if (Convert.ToString(dr["MoldCavity"]) == "") { MessageBox.Show("Cavity가 입력되지 않았습니다."); return false; }
                }
            }
            bool isRemoved = false;

            //삭제내역 있으면 물어보기
            for (int idx = 0; idx < gv.RowCount; idx++)
            {
                DataRow dr = gv.GetDataRow(idx);
                string status = Convert.ToString(dr["Status"]);

                if (status == "삭제")
                {
                    isRemoved = true;
                }
            }
            if (isRemoved == true)
            {
                DialogResult dialogResult = MessageBox.Show("삭제내역이 있습니다. 삭제하시겠습니까?", "Delete OK Cancle", MessageBoxButtons.OKCancel);
                if (dialogResult == DialogResult.Cancel)
                {
                    return false;
                }
            }
            return true;
        }

        private bool SaveMold_ToDatabase(DataRow dr, char div)
        {
            //데이터베이스에 반영
            MD_Mold mold = new MD_Mold();
            //1. 기본 셋팅
            string mold_Cd = dr["MoldCode"].ConvertString_Null_To_Empty();
            if (div == 'I' && WrGlobal.ProcOption["BS"]["Mold_AutoNo"] == "Y")
            {
                //Mold_Cd 입력 자동채번 셋팅
                DataRow drLast = mold.Select_MoldCode_Last();

                //금형데이터가 없는경우
                if (drLast == null)
                {
                    mold_Cd = "MD00001";
                }
                else
                {
                    //번호 떼오기
                    int seq = Convert.ToInt16(Convert.ToString(drLast["MoldCode"]).Substring(2, 5)) + 1;

                    mold_Cd = "MD" + seq.ToString("D5");
                }
            }

            mold.MoldCode = mold_Cd;                                            //금형코드
            mold.MoldMgmtNo = dr["MoldMgmtNo"].ConvertString_Null_To_Empty();                //금형관리번호
            mold.MoldSN = dr["MoldSN"].ConvertString_Null_To_Empty();                     //금형제작번호
            mold.Chasu            = dr["Chasu"].ConvertString_Null_To_Empty();          //차수
            mold.MoldName          = dr["MoldName"].ConvertString_Null_To_Empty();           //금형명
            mold.ApplyEquip      = dr["ApplyEquip"].ConvertString_Null_To_Empty();               //적용설비 스펙
            mold.MoldStd         = dr["MoldStd"].ConvertString_Null_To_Empty();                  //금형규격
            mold.MoldForm        = dr["MoldForm"].ConvertString_Null_To_Empty();                  //금형형식
            mold.MoldItemCount  = dr["MoldItemCount"].ConvertInt_Null_To_Zero();    //금형품목수
            mold.MoldCavity      = dr["MoldCavity"].ConvertInt_Null_To_Zero();        //Cavity
            mold.FigurationGrade = dr["FigurationGrade"].ConvertString_Null_To_Empty();           //성형재질
            mold.SupplyCust   = dr["SupplyCust"].ConvertString_Null_To_Empty();             //지급처
            mold.ProdtCust = dr["ProdtCust"].ConvertString_Null_To_Empty();              //제작처코드 (유림 사용 x)
            mold.ProdtDate       = dr["ProdtDate"].ConvertDate_To_Char8();            //제작일
            mold.DisposalDate = dr["DisposalDate"].ConvertDate_To_Char8();            //폐기일
            mold.ProdtAmount     = dr["ProdtAmount"].ConvertDecimal_Null_To_Zero();   //제작가
            mold.GateMethod = dr["GateMethod"].ConvertString_Null_To_Empty();                //Gate방식
            mold.ContractRate    = dr["ContractRate"].ConvertDecimal_Null_To_Zero();  //수축율
            mold.Weight           = dr["Weight"].ConvertDecimal_Null_To_Zero();         //성형품 중량
            mold.MoldWeight      = dr["MoldWeight"].ConvertDecimal_Null_To_Zero();    //금형중량
            mold.Sprue            = dr["Sprue"].ConvertDecimal_Null_To_Zero();          //Sprue
            mold.InitShot        = dr["InitShot"].ConvertInt_Null_To_Zero();          //초기Shot량
            mold.LifeShot        = dr["LifeShot"].ConvertInt_Null_To_Zero();          //수명Shot
            mold.UseYn           = dr["UseYn"].ConvertString_Null_To_Empty();         //사용여부
            mold.Zone             = dr["Zone"].ConvertString_Null_To_Empty();                       //동
            mold.LocNo           = dr["LocNo"].ConvertString_Null_To_Empty();                   //구역번호
            mold.MoldPjtNo      = dr["MoldPjtNo"].ConvertString_Null_To_Empty();         //프로젝트명
            mold.HoldCust      = dr["HoldCust"].ConvertString_Null_To_Empty();         //실보유처
            mold.MoldCavityText = dr["MoldCavityText"].ConvertString_Null_To_Empty();         //Cavity수 직접입력 (1*2 형식)
            mold.SpecialParts = dr["SpecialParts"].ConvertString_Null_To_Empty();  //특수부품

            //2. DB 적용
            if (div == 'I')
            {
                //Insert
                if (!mold.Ins_MD_Mold()) { return false; }

                //추가 변경사항 private 따로 만들기
            }
            else if (div == 'U')
            {
                //Update
                if (!mold.Upd_MD_Mold()) { return false; }

                //추가 변경사항 private 따로 만들기
            }
            else if (div == 'D')
            {
                //Delete
                if (!mold.Del_MD_Mold()) { return false; }

                //금형이미지 삭제
                MD_MoldImage ml = new MD_MoldImage();
                ml.MoldCode = mold_Cd;

                if (!ml.Del_MD_MoldImage()) { return false; }

                //추가 변경사항 private 따로 만들기
            }

            return true;
        }

        #endregion 금형저장 End

        #region 금형품목 저장

        private void BtnSave_Item_Click(object sender, EventArgs e)
        {
            Save_Item();
        }

        private void Save_Item()
        {
            //작성내역 저장
            GridView gv = Gv_MoldItem;
            gv.PostEditor();
            gv.UpdateCurrentRow();

            //1. 데이터 불러온 상태인지 검사
            if (!CheckItem_SaveClick(gv)) return;

            //2. 데이터 저장
            bool success = true;

            for (int idx = 0; idx < gv.RowCount; idx++)
            {
                DataRow dr = gv.GetDataRow(idx);
                string status = Convert.ToString(dr["Status"]);

                switch (status)
                {
                    case "신규": success = SaveItem_ToDatabase(dr, 'I'); break;
                    case "수정": success = SaveItem_ToDatabase(dr, 'U'); break;
                    case "삭제": success = SaveItem_ToDatabase(dr, 'D'); break;
                }

                if (!success) break;

                dr["Status"] = "";
            }

            //3. 정상절차 진행
            if (success)
            {
                MessageBox.Show("금형품목 저장이 정상적으로 완료되었습니다.");
                Func_Search_Item(Convert.ToString(Gv_Mold.GetFocusedDataRow()["MoldCode"]));
            }
            else
            {
                MessageBox.Show("금형품목 저장 중 에러가 발생하였습니다.");
            }

        }

        private bool CheckItem_SaveClick(GridView gv)
        {
            //Item을 불러온 상태인지 검사
            if (gv == null) return false;

            int checkCnt = 0;

            if (Gv_Mold.FocusedRowHandle < 0) return false;

            //삭제내역 있으면 물어보기
            for (int idx = 0; idx < gv.RowCount; idx++)
            {
                DataRow dr = gv.GetDataRow(idx);
                string status = Convert.ToString(dr["Status"]);

                if (status == "삭제")
                {
                    checkCnt++;
                    if (MessageBox.Show("삭제내역이 있습니다. 삭제하시겠습니까?", "Delete OK Cancle", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        break;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if(status == "수정")
                {
                    checkCnt++;
                }
                else if(status == "신규")
                {
                    checkCnt++;
                }
            }

            if (checkCnt == 0) return false;

            return true;
        }

        private bool SaveItem_ToDatabase(DataRow dr, char div)
        {
            //데이터베이스에 반영
            //1. 기본 셋팅
            MD_MoldItem mi = new MD_MoldItem();
            mi.MoldCode = dr["MoldCode"].ConvertString_Null_To_Empty();
            mi.ItemCode = dr["ItemCode"].ConvertString_Null_To_Empty();
            mi.UseYn = dr["UseYn"].ConvertString_Null_To_Empty();

            //2. DB 적용
            if (div == 'I')
            {
                //Insert
                if (!mi.Ins_MD_MoldItem()) { return false; }

                //추가 변경사항 private 따로 만들기
            }
            else if (div == 'U')
            {
                //Update
                if (!mi.Upd_MD_MoldItem()) { return false; }

                //추가 변경사항 private 따로 만들기
            }
            else if (div == 'D')
            {
                //Delete
                if (!mi.Del_MD_MoldItem()) { return false; }

                //추가 변경사항 private 따로 만들기
            }


            return true;
        }

        #endregion 금형품목 저장 End

        #region 금형이미지 저장

        private void PictureSave()
        {
            DataRow dr = Gv_Mold.GetFocusedDataRow();
            byte[] imageL = pictureEdit1.EditValue as byte[];
            byte[] imageR = pictureEdit2.EditValue as byte[];

            MD_MoldImage mold = new MD_MoldImage();
            mold.MoldCode = Convert.ToString(dr["MoldCode"]);
            mold.Seq = 1;
            mold.MoldImage1 = imageL;
            mold.MoldImage2 = imageR;

            if (!mold.SaveMoldImage())
            {
                MessageBox.Show("이미지 저장 중 에러가 발생하였습니다.");
            }
        }

        #endregion 금형이미지 저장End

        #endregion 저장 기능 End

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            DataRow dr = Gv_Mold.GetFocusedDataRow();
            if (dr == null) return;

            MD_Mold Mold = new MD_Mold();

            string FocusMold = Convert.ToString(dr["MoldCode"]);

            DataTable MoldHistory = Mold.Select_EquipmentManagement(FocusMold);

            Rpt_Mold_Management report = new Rpt_Mold_Management(MoldHistory);
            ReportPrintTool reportTool = new ReportPrintTool(report);

            reportTool.PrinterSettings.Collate = false;
            reportTool.PreviewForm.PrintControl.Zoom = 0.9f;
            reportTool.PreviewForm.PrintControl.ResumeLayout();

            reportTool.ShowPreviewDialog();
        }

        #region 바코드스캔

        private void Text_Barcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 바코드 조회
            if (e.KeyChar != (char)Keys.Enter) return;

            GridView gv = Gc_Mold.DefaultView as GridView;
            if (gv == null) return;

            string moldCode = Text_Barcode.Text.Trim();  // 금형코드 바코드

            try
            {
                // 금형코드 존재유무 확인
                MD_Mold mold = new MD_Mold();
                mold.MoldCode = moldCode;

                //if (!mold.Select_Mold_Cd())
                {
                    MessageBox.Show("'" + moldCode + "'는 등록되지 않은 금형 코드입니다.");
                    Text_Barcode.Text = "";
                    return;
                }

                //Gc_Mold.DataSource = mold.Select_Mold();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

         private void cmdExcel_Click(object sender, EventArgs e)
        {
            clsFunction function = new clsFunction();
            function.ExportExcel(Gv_Mold);
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}