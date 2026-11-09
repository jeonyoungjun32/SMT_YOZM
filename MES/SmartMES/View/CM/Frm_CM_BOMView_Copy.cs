using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList.Nodes;
using SmartMES.Common;
using SmartMES.Models;
using System;
using System.Collections.Generic;
using System.Data;
using SmartCommon.Common;
using System.Windows.Forms;
using SmartMES.Models.CM;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;

namespace SmartMES.View.CM
{
    public partial class Frm_CM_BOMView_Copy : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        clsCode code = new clsCode();
        clsBom bom = new clsBom();
        int maxChildID = 0;

        Database db = new Database();
        string query = "";

        string[] ItemDiv = new string[] { "20", "30" };

        public Frm_CM_BOMView_Copy()
        {
            InitializeComponent();
        }

        private void Frm_CM_BOMView_Copy_Load(object sender, EventArgs e)
        {
            LupMaterial.Properties.DataSource = code.GetMaterialList(ItemDiv, false);
            LupMaterial.Properties.DisplayMember = "품명";
            LupMaterial.Properties.ValueMember = "품목코드";

            BomTreeList.Columns.Remove(colNodeDel); //일단은 기존 있는 bom리스트만 복제하도록 삭제 버튼 숨김

            SetGvMaterial(gvMaterial);

            DisplayBom();
            DisplayMaterialList();
        }

        private void SetGvMaterial(GridView gv)
		{
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.OptionsView.ShowGroupPanel = false;

            gv.IndicatorWidth = 50;                     //순번 너비

            //단위
            RepositoryItemSearchLookUpEdit lup_UnitCode = rpo.Create_RepositoryItemSearchLookUp(code.smtLookUp("품목단위", "", ""), "코드", "코드명");
            //품목분류1,2,3
            RepositoryItemSearchLookUpEdit lup_Class_Cd1 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01210"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd2 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01220"), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Class_Cd3 = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "A01230"), "코드", "명칭");                          //Column 생성

            grid.Create_GvColumn(gv, "등록여부", "BomYn");
            grid.Create_GvColumn_ByOption(gv, "제품코드 초기화");

            //Repository
            if (gv.Columns.ColumnByFieldName("UnitCode") != null) gv.Columns["UnitCode"].ColumnEdit = lup_UnitCode;
            if (gv.Columns.ColumnByFieldName("ItemClass1") != null) gv.Columns["ItemClass1"].ColumnEdit = lup_Class_Cd1;
            if (gv.Columns.ColumnByFieldName("ItemClass2") != null) gv.Columns["ItemClass2"].ColumnEdit = lup_Class_Cd2;
            if (gv.Columns.ColumnByFieldName("ItemClass3") != null) gv.Columns["ItemClass3"].ColumnEdit = lup_Class_Cd3;


            //이벤트 설정
            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
            grid.Set_Navigator(gv);
        }


        private void LupMaterial_EditValueChanged(object sender, EventArgs e)
        {
            string ItemCode = LupMaterial.EditValue.ConvertString_Null_To_Empty();

            CM_BOMRev cbm = new CM_BOMRev();
            cbm.CorpId = WrGlobal.CorpID;
            cbm.ItemCode = ItemCode;

            LupRevision.Properties.DataSource = cbm.SelectBomMasterLup();
            LupRevision.Properties.DisplayMember = "Revision";
            LupRevision.Properties.ValueMember = "Revision";

            CM_Item cm = new CM_Item();
            cm.ItemCode = ItemCode;

            DataRow dr = cm.Select_Item_RowInfo();

            if (dr != null)
            {
                LupRevision.EditValue = dr["Revision"];
            }

            DisplayBom();
        }

        private void LupRevision_EditValueChanged(object sender, EventArgs e)
        {
            DisplayBom();
        }

        private void DisplayBom()
        {
            //BOM리스트 조회
            CM_BOM bom = new CM_BOM();
            bom.PItemCode = LupMaterial.EditValue.ConvertString_Null_To_Empty();
            bom.Revision = LupRevision.EditValue.ConvertString_Null_To_Empty();

            DataTable dtBom = bom.Code_GetBomRevision();

            maxChildID = 0;

            foreach (DataRow dr in dtBom.Rows)
            {
                if ((int)dr["ChildID"] > maxChildID) maxChildID = (int)dr["ChildID"];
            }

            BomTreeList.DataSource = dtBom;

            BomTreeList.ExpandAll();
            BomTreeList.BestFitColumns();
        }

        private void DisplayMaterialList()
        {
            CM_Item material = new CM_Item();
            gcMaterial.DataSource = material.SelectList_BOM(ItemDiv);

            gvMaterial.BestFitColumns();
        }

        private void CancelButtonClick(object sender, EventArgs e)
        {
            (sender as SimpleButton).Click -= CancelButtonClick;
        }

        void FindChildrenByType<T>(Control parent, List<T> list) where T : class
        {
            foreach (Control child in parent.Controls)
            {
                if (child is T)
                    list.Add(child as T);
                if (child.HasChildren)
                    FindChildrenByType<T>(child, list);
            }
        }

        private void BomTreeList_CustomNodeCellEdit(object sender, DevExpress.XtraTreeList.GetCustomNodeCellEditEventArgs e)
        {
            try
            {
                if (e.Column.Name == "colNodeDel")
                {
                    if (e.Node["ProcessType"].ToString() == "P")
                    {
                        e.RepositoryItem = btnNoDelete;
                    }
                    else
                    {
                        e.RepositoryItem = btnNodeDelete;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNodeDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (MessageBox.Show("선택한 항목을 삭제 하시겠습니까?", "항목삭제", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                BomTreeList.Nodes.Remove(BomTreeList.FocusedNode);
            }
        }

        private void SetNodeLevel(TreeListNodes nodes)
        {
            if (nodes.Count == 0) return;

            foreach (TreeListNode node in nodes)
            {
                node.SetValue("TreeLevel", node.Level);
                if (node.Nodes.Count > 0)
                {
                    SetNodeLevel(node.Nodes);
                }
            }
        }

        //조회
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            DisplayBom();
            DisplayMaterialList();
        }

		//저장
		private void cmdSave_Click(object sender, EventArgs e)
		{
			// BOM 저장
			try
			{
				this.Validate();

				BomTreeList.BeginUpdate();

				SetNodeLevel(BomTreeList.Nodes);

				string sourceCode = LupMaterial.EditValue.ConvertString_Null_To_Empty();
				string sourceRevision = LupRevision.EditValue.ConvertString_Null_To_Empty();

                CM_BOMRev cbm = new CM_BOMRev();
                cbm.CorpId = WrGlobal.CorpID;
                cbm.ItemCode = sourceCode;
                cbm.Revision = sourceRevision;

                DataRow drSource = cbm.SelectBomMasterInfo();

                int[] selectedIdx = gvMaterial.GetSelectedRows();

                //선택된 품목 루프
				foreach (int idx in selectedIdx)
				{
					DataRow drMaterial = gvMaterial.GetDataRow(idx);

					string targetCode = drMaterial["ItemCode"].ConvertString_Null_To_Empty();
					if (targetCode == "") continue;

                    cbm.ItemCode = targetCode;
                    string targetRevision = cbm.GetNextRevision(); //선택된 품목 리비전번호 채번

                    //CM_BOMRev INSERT
                    cbm.Revision = targetRevision;

                    cbm.InsertBomMaster_SourceCode(sourceCode, sourceRevision);

                    //품목 Revision, RoutingCode UPDATE
                    CM_Item cm = new CM_Item();
                    cm.ItemCode = targetCode;
                    cm.Revision = targetRevision;
                    cm.RoutingCode = drSource["RoutingCode"].ConvertString_Null_To_Empty();
                    cm.UpdateRevisionRoutingCode();

					int cnt = 0;
					DataTable TreeDt = BomTreeList.DataSource as DataTable; // TreeList의 데이터 원본 가져오기

                    DataTable dtCopy = TreeDt.Copy();

					foreach (DataRow row in dtCopy.Rows)
					{
                        if(row["ItemCode"].ConvertString_Null_To_Empty() == sourceCode)
						{
                            row["ItemCode"] = targetCode;
                            row["ItemName"] = drMaterial["ItemName"];
						}

                        row["ItemCode"] = targetCode;
						row["RowNum"] = cnt++;
						row["Revision"] = targetRevision;
                        row["CreId"] = WrGlobal.LoginID;
                        row["CreIP"] = WrGlobal.ClientHostName;
                        row["CreDt"] = DateTime.Now;
                    }

                    dtCopy.TableName = "CM_BOM";
                    if(dtCopy.Columns.Contains("DisplayName")) dtCopy.Columns.Remove("DisplayName");
                    if(dtCopy.Columns.Contains("ItemTypeName")) dtCopy.Columns.Remove("ItemTypeName");

                    code.SaveBomTable(targetCode, targetRevision, dtCopy);
				}

				MessageBox.Show(cls_MessageBox.Save_Comp, cls_MessageBox.SaveCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}


			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void cmdClose_Click(object sender, EventArgs e)
		{
            this.Close();
		}
	}
}