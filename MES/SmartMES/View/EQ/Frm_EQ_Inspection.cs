using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Model.EQ;
using SmartMES.Models.CM;
using SmartMES.Models.EQ;
using System;
using System.Data;
using System.Windows.Forms;

namespace SmartMES.View.EQ
{
	public partial class Frm_EQ_Inspection : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        string Basic_cd = "";
        string InspectDivCd = "";
        string Class_Nm = "";

        public Frm_EQ_Inspection(string basic_Cd, string type, string class_Nm)
        {
            InitializeComponent();

            Basic_cd = basic_Cd;
            InspectDivCd = type;
            Class_Nm = class_Nm;

            SetDesign();
        }

        #region 디자인 아이템 생성

        private void SetDesign()
        {
            Set_GridView();
        }

        #region GridView Setting

        private void Set_GridView()
        {
            Set_GvInspection_Copy(Gv_Inspection_Copy);
            Set_GvInspection_Copy_Target(Gv_Inspection_Copy_Target);
        }

        private void Set_GvInspection_Copy(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            //검색창
            gv.OptionsFind.AlwaysVisible = false;
            gv.OptionsView.ShowGroupPanel = false;
            //그리드 멀티셀렉트 설정
            gv.OptionsSelection.MultiSelect = true;
            gv.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;


            if (Class_Nm == "Machine")
            {

                CM_CateItem item = new CM_CateItem();
                RepositoryItem lup_Inspect_Div = rpo.Create_RepositoryItem(item.SelectClassItemLookUp(WrGlobal.CorpID, "2040"), "분류코드", "분류명");
                RepositoryItem lup_Cycle_Div = rpo.Create_RepositoryItem(item.SelectClassItemLookUp(WrGlobal.CorpID, "204050"), "분류코드", "분류명");
                RepositoryItem lup_Week_Div = rpo.Create_RepositoryItem(item.SelectClassItemLookUp(WrGlobal.CorpID, "204060"), "분류코드", "분류명");
                RepositoryItem lup_ValueType = rpo.Create_RepositoryItem(item.SelectClassItemLookUp(WrGlobal.CorpID, "204070"), "분류코드", "분류명");

                //Column 생성
                grid.Create_GvColumn(gv, "점검구분", "InspectDivCd", lup_Inspect_Div);
                grid.Create_GvColumn(gv, "설비코드", "MachineCode");
                grid.Create_GvColumn(gv, "설비명", "Machine_Nm");
                grid.Create_GvColumn(gv, "저장순번", "Seq");
                grid.Create_GvColumn(gv, "표시순번", "DispSeq");
                grid.Create_GvColumn(gv, "관리항목", "ManagementItem");
                grid.Create_GvColumn(gv, "확인방법", "CheckMethod");
                grid.Create_GvColumn(gv, "점검기준", "StandardSpec");
                grid.Create_GvColumn(gv, "관리주기", "CycleDivCd", lup_Cycle_Div);
                grid.Create_GvColumn(gv, "관리요일", "WeekDivCd", lup_Week_Div);
                grid.Create_GvColumn(gv, "설비값 관리 형태", "ValueTypeDivCd", lup_ValueType);
                //grid.Create_GvColumn(gv, "이미지", "Chk_Image");
                grid.Create_GvColumn(gv, "기본값", "InspectOk");
                grid.Create_GvColumn(gv, "측정수치Min", "InspectMin");
                grid.Create_GvColumn(gv, "측정수치Max", "InspectMax");
                grid.Create_GvColumn(gv, "비고", "Remark");

                gv.Columns["Machine_Nm"].Visible = false;
                gv.Columns["Seq"].Visible = false;
                gv.Columns["DispSeq"].Visible = false;
            }
            else if (Class_Nm == "Mold")
            {
                ////점검기준
                //cmCd.Cm_Class = "024";
                //RepositoryItem lup_Inspect_Div = rpo.Create_RepositoryItem(cmCd.Select_CmCd(false, new string[] { "10", "20" }), "Cm_Cd", "Cm_Nm");
                ////관리주기
                //cmCd.Cm_Class = "026";
                //RepositoryItem lup_Cycle_Div = rpo.Create_RepositoryItem(cmCd.Select_CmCd(false), "Cm_Cd", "Cm_Nm");
                ////관리요일
                //cmCd.Cm_Class = "027";
                //RepositoryItem lup_Week_Div = rpo.Create_RepositoryItem(cmCd.Select_CmCd(false), "Cm_Cd", "Cm_Nm");
                ////값 형태
                //cmCd.Cm_Class = "028";
                //RepositoryItem lup_ValueType = rpo.Create_RepositoryItem(cmCd.Select_CmCd(false), "Cm_Cd", "Cm_Nm");

                //grid.Create_GvColumn(gv, "저장순번", "Seq");
                //grid.Create_GvColumn(gv, "표시순번", "DispSeq");
                //grid.Create_GvColumn(gv, "점검구분", "InspectDivCd", lup_Inspect_Div);
                //grid.Create_GvColumn(gv, "금형", "Mold_Cd");
                //grid.Create_GvColumn(gv, "금형명", "Mold_Nm");
                //grid.Create_GvColumn(gv, "금형관리번호", "Mold_Mgmt_No");
                //grid.Create_GvColumn(gv, "관리항목", "ManagementItem");
                //grid.Create_GvColumn(gv, "확인방법", "CheckMethod");
                //grid.Create_GvColumn(gv, "점검기준", "StandardSpec");
                //grid.Create_GvColumn(gv, "관리주기", "CycleDivCd", lup_Cycle_Div);
                //grid.Create_GvColumn(gv, "관리요일", "WeekDivCd", lup_Week_Div);
                //grid.Create_GvColumn(gv, "값 관리 형태", "ValueTypeDivCd", lup_ValueType);
                ////grid.Create_GvColumn(gv, "이미지", "Chk_Image", Btn_Img);
                //grid.Create_GvColumn(gv, "기본값", "InspectOk");
                //grid.Create_GvColumn(gv, "측정수치Min", "InspectMin");
                //grid.Create_GvColumn(gv, "측정수치Max", "InspectMax");
                //grid.Create_GvColumn(gv, "비고", "Remark");

                //gv.Columns["Seq"].Visible = false;
                //gv.Columns["DispSeq"].Visible = false;
                //gv.Columns["Mold_Nm"].Visible = false;
            }
        }

        private void Set_GvInspection_Copy_Target(GridView gv)
        {
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            //검색창
            gv.OptionsFind.AlwaysVisible = false;
            gv.OptionsView.ShowGroupPanel = false;
            //그리드 멀티셀렉트 설정
            gv.OptionsSelection.MultiSelect = true;
            gv.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;

            //Column 생성
            if (Class_Nm == "Machine")
            {
                grid.Create_GvColumn(gv, "설비코드", "MachineCode");
                grid.Create_GvColumn(gv, "설비명", "MachineName");
            }
            else if (Class_Nm == "Mold")
            {
                grid.Create_GvColumn(gv, "금형코드", "Mold_Cd");
                grid.Create_GvColumn(gv, "금형명", "Mold_Nm");
            }
        }

        #endregion GridView Setting

        #endregion 디자인 아이템 생성

        #region Event

        private void Frm_EQ_Inspection_Load(object sender, EventArgs e)
        {
            Search();
        }

        #endregion Event

        #region 조회 기능

        private void Search()
        {
            Func_Search_Inspection_Std();
            Func_Search_Target_Info();
        }

        private void Func_Search_Inspection_Std()
        {
            if (Class_Nm == "Machine")
            {
                EQ_MachineChkStd std = new EQ_MachineChkStd();
                std.InspectDivCd = InspectDivCd;
                std.MachineCode = Basic_cd;

                Gc_Inspection_Copy.DataSource = std.Select_Machine_Chk_Std_byCd();
            }
            else if (Class_Nm == "Mold")
            {
                //BS_Mold_Chk_Std std = new BS_Mold_Chk_Std();
                //std.InspectDivCd = InspectDivCd;
                //std.Mold_Cd = Basic_cd;

                //Gc_Inspection_Copy.DataSource = std.Select_Mold_Chk_Std_byCd();
            }

            Gv_Inspection_Copy.BestFitColumns();   //Column 너비 조정
        }

        private void Func_Search_Target_Info()
        {
            if (Class_Nm == "Machine")
{
                EQ_Machine machine = new EQ_Machine();
                machine.MachineCode = Basic_cd;

                Gc_Inspection_Copy_Target.DataSource = machine.Select_MachineCd_NotMe();
            }
            else if (Class_Nm == "Mold")
            {
                //BS_Mold mold = new BS_Mold();
                //mold.Mold_Cd = Basic_cd;

                //Gc_Inspection_Copy_Target.DataSource = mold.Select_Mold_byCd();
            }

            Gv_Inspection_Copy_Target.BestFitColumns();
        }

        #endregion 조회 기능

        #region 저장 기능

        private void BtnSave_Click(object sender, EventArgs e)
        {
            GridView gv_Std = Gv_Inspection_Copy;
            GridView gv_Target = Gv_Inspection_Copy_Target;

            if (!Check_SaveClick(gv_Std, gv_Target)) return;

            if (Class_Nm == "Machine")
            {
                if (!Save_Machine(gv_Std, gv_Target))
                {
                    return;
                }
            }
            else if (Class_Nm == "Mold")
            {
                //if (!Save_Mold(gv_Std, gv_Target))
                //{
                //    return;
                //}
            }

            MessageBox.Show("저장이 정상적으로 완료되었습니다.");

        }

        private bool Save_Machine(GridView gv_Std, GridView gv_Target)
        {
            foreach (int row in gv_Target.GetSelectedRows())
            {
                EQ_MachineChkStd machine = new EQ_MachineChkStd();
                DataRow dr_Target = gv_Target.GetDataRow(row);
                machine.MachineCode = Convert.ToString(dr_Target["MachineCode"]);
                machine.InspectDivCd = InspectDivCd;

                foreach (int idx in gv_Std.GetSelectedRows())
                {
                    DataRow dr = gv_Std.GetDataRow(idx);

                    if (machine.Select_Machine_Chk_Std_byCd().Rows.Count < 1)
                    {
                        machine.DispSeq = 1;
                    }
                    else
                    {
                        DataRow std_Disp = machine.Select_Machine_Chk_Std_DispSeq();
                        machine.DispSeq = std_Disp["DispSeq"].ConvertInt_Null_To_Zero() + 1;
                    }

                    //machine.DispSeq
                    machine.ManagementItem = Convert.ToString(dr["ManagementItem"]);
                    machine.CheckMethod = Convert.ToString(dr["CheckMethod"]);
                    machine.StandardSpec = Convert.ToString(dr["StandardSpec"]);
                    machine.CycleDivCd = Convert.ToString(dr["CycleDivCd"]);
                    machine.WeekDivCd = Convert.ToString(dr["WeekDivCd"]);
                    machine.ValueTypeDivCd = Convert.ToString(dr["ValueTypeDivCd"]);
                    machine.InspectOk = Convert.ToString(dr["InspectOk"]);
                    machine.InspectMin = dr["InspectMin"].ConvertInt_Null_To_Zero();
                    machine.InspectMax = dr["InspectMax"].ConvertInt_Null_To_Zero();
                    machine.Remark = Convert.ToString(dr["Remark"]);

                    //Insert
                    if (!machine.Ins_Machine())
                    {
                        //저장 중 에러 발생시
                        MessageBox.Show("저장 중 에러가 발생하였습니다.");
                        return false;
                    }
                }
            }
            return true;
        }

		/*private bool Save_Mold(GridView gv_Std, GridView gv_Target)
		{
			foreach (int row in gv_Target.GetSelectedRows())
			{
				BS_Mold_Chk_Std mold = new BS_Mold_Chk_Std();
				DataRow dr_Target = gv_Target.GetDataRow(row);
				mold.Mold_Cd = Convert.ToString(dr_Target["Mold_Cd"]);
				mold.InspectDivCd = InspectDivCd;

				foreach (int idx in gv_Std.GetSelectedRows())
				{
					DataRow dr = gv_Std.GetDataRow(idx);

					if (mold.Select_Mold_Chk_Std_byCd().Rows.Count < 1)
					{
						mold.DispSeq = 1;
					}
					else
					{
						DataRow std_Disp = mold.Select_Mold_Chk_Std_DispSeq();
						mold.DispSeq = std_Disp["DispSeq"].ConvertInt_Null_To_Zero() + 1;
					}

					//machine.DispSeq
					mold.ManagementItem = Convert.ToString(dr["ManagementItem"]);
					mold.CheckMethod = Convert.ToString(dr["CheckMethod"]);
					mold.StandardSpec = Convert.ToString(dr["StandardSpec"]);
					mold.CycleDivCd = Convert.ToString(dr["CycleDivCd"]);
					mold.WeekDivCd = Convert.ToString(dr["WeekDivCd"]);
					mold.ValueTypeDivCd = Convert.ToString(dr["ValueTypeDivCd"]);
					mold.InspectOk = Convert.ToString(dr["InspectOk"]);
					mold.InspectMin = dr["InspectMin"].ConvertInt_Null_To_Zero();
					mold.InspectMax = dr["InspectMax"].ConvertInt_Null_To_Zero();
					mold.Remark = Convert.ToString(dr["Remark"]);

					//Insert
					if (!mold.Ins_Mold_Chk_Std())
					{
						//저장 중 에러 발생시
						MessageBox.Show("저장 중 에러가 발생하였습니다.");
						return false;
					}
				}
			}

			return true;
		}*/

		private bool Check_SaveClick(GridView gv_Std, GridView gv_Target)
        {
            if (gv_Std.SelectedRowsCount < 1)
            {
                MessageBox.Show("선택된 기준이 없습니다.");
                return false;
            }
            if (gv_Target.SelectedRowsCount < 1)
            {
                MessageBox.Show("선택된 대상이 없습니다.");
                return false;
            }

            return true;
        }

		#endregion 저장 기능

		private void cmdClose_Click(object sender, EventArgs e)
		{
            this.Close();
		}
	}
}