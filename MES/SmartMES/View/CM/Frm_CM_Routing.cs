using DevExpress.Utils.DragDrop;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList.Nodes;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models;
using SmartMES.Models.CM;
using SmartMES.View.Lookup;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SmartMES.View.CM
{
    public partial class Frm_CM_Routing : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();
        clsCode code = new clsCode();
        string query = "";
        Database db = new Database();

        int maxChildID = 0;

        BindingSource CM_RoutingRevBindingSource = new BindingSource();

        public Frm_CM_Routing()
        {
            InitializeComponent();

            code.LookupCategory(ref workTypeColLookUpEdit, "401010");            //공정구분
            code.LookupCategory(ref workTypeColLookUpEdit1, "401010");            //공정구분
            code.LookupCategory(ref routingTypeColLookUpEdit, "401020");            //라우팅타입
        }

        private void Frm_CM_Routing_Load(object sender, EventArgs e)
        {
            //if (WrGlobal.AuthorList.Exists(s => s.MenuID == this.Tag.ToString() && s.AuthorCode == "02"))
            //{
            //    cmdInsert.Visible = true;
            //    cmdSave.Visible = true;
            //    cmdDelete.Visible = true;
            //}
            DisplayRoutingMaster();
            DisplayProcess();

            DragDropManager.Default.DragOver += OnDragOver;
            DragDropManager.Default.DragDrop += OnDragDrop;
        }

        //전체 라우팅 리스트
        private void DisplayRoutingMaster()
        {
            gvCM_RoutingRev.FocusInvalidRow();

            CM_RoutingRev rr = new CM_RoutingRev();
            gcCM_RoutingRev.DataSource = rr.Select_CM_RoutingRev();

            gvCM_RoutingRev.BestFitColumns();
        }

        private void DisplayBomList()
        {
            try
            {
                CM_Routing cr = new CM_Routing();
                cr.RoutingCode = gvCM_RoutingRev.GetFocusedRowCellValue("RoutingCode").ConvertString_Null_To_Empty();

                DataTable dt = cr.Code_GetRoutingDetail();
                RoutingTreeList.DataSource = dt;
                RoutingTreeList.BestFitColumns();

                maxChildID = 0;

                foreach (DataRowView drv in cmRoutingBindingSource.List)
                {
                    if ((int)drv["ChildID"] > maxChildID) maxChildID = (int)drv["ChildID"];
                }

                RoutingTreeList.ExpandAll();
                RoutingTreeList.Columns["ProcessCode"].BestFit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
        }

        private void DisplayProcess()
        {
            //시내작업만 보이게
            gcCM_Process.DataSource = code.Select_ProcessByBom();
            gvCM_Process.BestFitColumns();
        }

        private void DisplayProduct()
        {
            gcCM_Item.DataSource = code.GetRoutingProduct((gvCM_RoutingRev.GetFocusedRowCellValue("RoutingCode") ?? "").ToString());
            gvCM_Item.BestFitColumns();

        }
        private void gvCM_Process_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle)
            {
                e.Appearance.Assign(view.PaintAppearance.SelectedRow);
                e.Appearance.Options.UseForeColor = true;
            }//end if
        }

        private void gvCM_RoutingRev_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DisplayBomList();
            DisplayProduct();
        }

        private void OnDragOver(object sender, DragOverEventArgs e)
        {
            if (object.ReferenceEquals(e.Source, e.Target))
                return;
            e.Default();
            if (e.InsertType == InsertType.None)
                return;
            e.Action = IsCopy(e.KeyState) ? DragDropActions.Copy : DragDropActions.Move;
            Cursor current = Cursors.No;
            if (e.Action != DragDropActions.None)
                current = Cursors.Default;
            e.Cursor = current;
        }

        bool IsCopy(DragDropKeyState key)
        {
            return (key & DragDropKeyState.Control) != 0;
        }

        void OnDragDrop(object sender, DragDropEventArgs e)
        {
            if (object.ReferenceEquals(e.Source, e.Target))
                return;
            e.Handled = true;
            if (e.Action == DragDropActions.None || e.InsertType == InsertType.None)
                return;
            if (e.Target == RoutingTreeList)
                OnTreeListDrop(e);
            Cursor.Current = Cursors.Default;
        }

        private void OnTreeListDrop(DragDropEventArgs e)
        {
            if (((DevExpress.XtraGrid.Views.Grid.GridView)e.Source).Name == "gvCM_Process")
            {
                int gIndex = ((int[])e.Data)[0];
                if (gIndex < 0)
                    return;

                var destNode = GetDestNode(e.Location);
                int index = -1;
                TreeListNode addNode = null;

                index = CalcDestNodeIndex(e, destNode);
                RoutingTreeList.BeginUpdate();
                RoutingTreeList.Selection.UnselectAll();

                DataRow sourceRow = ((GridView)e.Source).GetDataRow(gIndex);

                object[] nodeData = new object[20];
                nodeData[0] = WrGlobal.CorpID;
                nodeData[1] = gvCM_RoutingRev.GetFocusedRowCellValue("RoutingCode");
                nodeData[2] = 0;    //ChildID
                nodeData[3] = 0;    //ParentID
                nodeData[4] = DBNull.Value;   //RoutingSeq
                nodeData[5] = DBNull.Value;   //TreeLevel
                nodeData[6] = DBNull.Value;   //RowNum
                nodeData[7] = sourceRow["ProcessCode"];   //ProcessCode
                nodeData[8] = sourceRow["WorkType"];   //WorkType
                nodeData[9] = DBNull.Value;   //WorkType
                nodeData[10] = DBNull.Value;   //WorkType
                nodeData[11] = DBNull.Value;   //WorkType
                nodeData[12] = DBNull.Value;   //WorkType
                nodeData[13] = DBNull.Value;   //WorkType
                nodeData[14] = DBNull.Value;   //WorkType
                nodeData[15] = DBNull.Value;   //WorkType
                nodeData[16] = DBNull.Value;   //WorkType
                nodeData[17] = DBNull.Value;   //WorkType
                nodeData[18] = DBNull.Value;   //WorkType
                nodeData[19] = sourceRow["ProcessName"];   //ProcessName

                addNode = RoutingTreeList.AppendNode(nodeData, index == -1000 ? destNode : null);

                //addNode.SetValue("ChildID", ++maxChildID);
                maxChildID = GetMaxChildID();
                addNode.SetValue("ChildID", maxChildID + 1);

                if (index > -1)
                {
                    RoutingTreeList.MoveNode(addNode, destNode.ParentNode, true, index);
                    index++;
                }

                RoutingTreeList.SelectNode(addNode);
                if (addNode.ParentNode != null)
                {
                    addNode.SetValue("ParentID", addNode.ParentNode["ChildID"]);
                    addNode.ParentNode.Expand();
                }

                RoutingTreeList.EndUpdate();
                RoutingTreeList.Columns["ProcessCode"].BestFit();

                RefreshParentID();
            }
            else
            {
                return;
            }
        }

        private TreeListNode GetDestNode(Point hitPoint)
        {
            Point pt = RoutingTreeList.PointToClient(hitPoint);
            DevExpress.XtraTreeList.TreeListHitInfo ht = RoutingTreeList.CalcHitInfo(pt);
            TreeListNode destNode = ht.Node;
            if (destNode is TreeListAutoFilterNode)
                return null;
            return destNode;
        }

        private int CalcDestNodeIndex(DragDropEventArgs e, TreeListNode destNode)
        {
            if (destNode == null)
                return -1;
            if (e.InsertType == InsertType.AsChild)
                return -1000;
            var nodes = destNode.ParentNode == null ? RoutingTreeList.Nodes : destNode.ParentNode.Nodes;
            int index = nodes.IndexOf(destNode);
            if (e.InsertType == InsertType.After)
                return ++index;
            return index;
        }

        private int GetMaxChildID()
        {
            int max = 0;

            List<TreeListNode> nodes = RoutingTreeList.GetNodeList();
            foreach (TreeListNode node in nodes)
            {
                object val = node.GetValue("ChildID");

                if (val != DBNull.Value)
                {
                    int id = Convert.ToInt32(val);
                    if (id > max)  max = id;
                }
            }
            return max;
        }

        private void RefreshParentID()
        {
            List<TreeListNode> nodes = RoutingTreeList.GetNodeList();

            foreach (TreeListNode node in nodes)
            {
                if (node.ParentNode == null)
                    node.SetValue("ParentID", 0);
                else
                    node.SetValue("ParentID", node.ParentNode.GetValue("ChildID"));
            }
        }

        // 신규 라우팅 등록
        private void cmdInsert_Click(object sender, EventArgs e)
        {
            if (gcCM_RoutingRev.DataSource == null) return;

            DataTable dt = gcCM_RoutingRev.DataSource as DataTable;
            if (dt == null) return;

            foreach (DataRow row in dt.Rows)
            {
                if (row["RoutingCode"].ConvertString_Null_To_Empty() == "")
                {
                    return;
                }
            }

            DataRow newRow = dt.NewRow();
            newRow["CorpId"] = WrGlobal.CorpID;
            dt.Rows.Add(newRow);

            gvCM_RoutingRev.RefreshData();
        }

        // 라우팅 저장
        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (RoutingTreeList.Nodes.Count > 1)
            {
                MessageBox.Show("최상위 계층에는 최종 제품 공정만 존재해야 합니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                this.Validate();

                string routingCode = (gvCM_RoutingRev.GetFocusedRowCellValue("RoutingCode") ?? "").ConvertString_Null_To_Empty();
                if (string.IsNullOrEmpty(routingCode))
                {
                    MessageBox.Show("라우팅코드가 없습니다.");
                    return;
                }

                RoutingTreeList.BeginUpdate();

                SetNodeLevel(RoutingTreeList.Nodes);

                int cnt = 0;

                List<TreeListNode> Listnodes = RoutingTreeList.GetNodeList();
                foreach (TreeListNode node in Listnodes)
                {
                    RoutingTreeList.SetRowCellValue(node, "RowNum", ++cnt);
                }

                RoutingTreeList.EndUpdate();

                CM_Routing cr = new CM_Routing();
                cr.RoutingCode = routingCode;

                if (!cr.Del_CM_Routing())
                {
                    //저장 중 에러 발생시
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return;
                }

                foreach (TreeListNode node in RoutingTreeList.Nodes)
                {
                    Save_NodeRouting(node, routingCode);
                }

                MessageBox.Show("저장되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Save_NodeRouting(TreeListNode node, string routingCode)
        {
            if (node == null) return;

            int childID = node.GetValue("ChildID") == DBNull.Value? 0: Convert.ToInt32(node.GetValue("ChildID"));
            int parentID = node.GetValue("ParentID") == DBNull.Value? 0: Convert.ToInt32(node.GetValue("ParentID"));
            int rowNum = Convert.ToInt32(node.GetValue("RowNum"));

            CM_Routing cr = new CM_Routing();
            cr.RoutingCode = routingCode;
            cr.ChildID = childID;
            cr.ParentID = parentID;
            cr.RoutingSeq = rowNum;
            cr.TreeLevel = node.Level;
            cr.RowNum = rowNum;
            cr.ProcessCode = node.GetValue("ProcessCode").ConvertString_Null_To_Empty();
            cr.WorkType = node.GetValue("WorkType").ConvertString_Null_To_Empty();

            if (!cr.Ins_CM_Routing())
            {
                //저장 중 에러 발생시
                MessageBox.Show("저장 중 에러가 발생하였습니다.");
                return;
            }

            foreach (TreeListNode child in node.Nodes)
            {
                Save_NodeRouting(child, routingCode);
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

        private void cmdMaterial_Click(object sender, EventArgs e)
        {
			DataRow dr = gvCM_RoutingRev.GetFocusedDataRow();
			if (dr != null)
			{
				string routingcode = dr["routingcode"].ConvertString_Null_To_Empty();
                Frm_LookUpItem form = new Frm_LookUpItem("P", new string [] {"20"});        //품목조회 (공정라우팅관리화면이름, 테이블, routingcode)
				form.onSetData += new Frm_LookUpItem.SetData(ProductSetData);
				form.ShowDialog();
			}

			DisplayProduct();
		}

        private void ProductSetData(DataRowView drv)
        {
            string pItemCode = drv["ItemCode"].ConvertString_Null_To_Empty();
            string routingCode = gvCM_RoutingRev.GetFocusedRowCellValue("RoutingCode").ConvertString_Null_To_Empty();
            
            // BOMRev 추가
            // CM_BOMRev Insert
            CM_BOMRev cbm = new CM_BOMRev();
            cbm.CorpId = WrGlobal.CorpID;
            cbm.ItemCode = pItemCode;
            cbm.RoutingCode = routingCode;
            
            // 기존 데이터 유무 확인
            if (cbm.ExistsBOMMasterByRoutingCode()) return;

            cbm.Revision = cbm.GetNextRevision();
            cbm.StartDate = DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss");
            cbm.EndDate = new DateTime(2999, 12, 31).ToString("yyyy-MM-dd HH:mm:ss");
            cbm.UseYn = "Y";

            if (!cbm.InsertBomMaster()) return;
            if (!cbm.UpdateBomRev_UseYn_ByRoutingCode()) return;

            // BOM 추가
            CM_BOM bom = new CM_BOM();
            bom.PItemCode = pItemCode;
            bom.Revision = cbm.Revision;

            bom.InsertBOMByRoutingCode(cbm.RoutingCode);

            // 품목 업데이트
            code.SetProductRouting(WrGlobal.CorpID, pItemCode, routingCode);
        }

        private void gvCM_RoutingRev_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            DataRowView newItem = (DataRowView)CM_RoutingRevBindingSource.Current;
            newItem["CorpId"] = WrGlobal.CorpID;
            newItem["RoutingCode"] = "";
            newItem["RoutingType"] = "10";
            newItem["UseYn"] = "Y";
            newItem["CreId"] = WrGlobal.LoginID;
            newItem["CreIP"] = WrGlobal.ClientHostName;
            newItem["CreDt"] = DateTime.Now;
        }

        private void gvCM_RoutingRev_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            try
            {
                this.Validate();

                DataRowView row = (DataRowView)e.Row;

                CM_RoutingRev cr = new CM_RoutingRev();
                cr.RoutingCode = row["RoutingCode"].ConvertString_Null_To_Empty();
                cr.RoutingName = row["RoutingName"].ConvertString_Null_To_Empty();
                cr.RoutingType = row["RoutingType"].ConvertString_Null_To_Empty();
                cr.Remark = row["Remark"].ConvertString_Null_To_Empty();
                cr.UseYn = row["UseYn"].ConvertString_Null_To_Empty();

                if (row.Row.RowState == DataRowState.Added)
                {
                    cr.RoutingCode = code.GetNewRoutingCode(WrGlobal.CorpID);

                    if (!cr.Ins_CM_RoutingRev())
                    {
                        //저장 중 에러 발생시
                        MessageBox.Show("저장 중 에러가 발생하였습니다.");
                        return;
                    }
                }
                else if (row.Row.RowState == DataRowState.Modified)
                {
                    if (!cr.Upd_CM_RoutingRev())
                    {
                        //저장 중 에러 발생시
                        MessageBox.Show("저장 중 에러가 발생하였습니다.");
                        return;
                    }
                }

                MessageBox.Show("저장되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DisplayRoutingMaster();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        // 공정 삭제
        private void btnNodeDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (MessageBox.Show("선택한 항목을 삭제 하시겠습니까?\r\n하위항목이 존재하는 경우 하위 항목도 같이 삭제됩니다.", "항목삭제", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                List<TreeListNode> nodes = new List<TreeListNode>();
                nodes.Add(RoutingTreeList.FocusedNode);
                DropNode(nodes);
            }
        }

        // 노드 삭제
        void DropNode(IEnumerable<TreeListNode> nodes)
        {
            List<TreeListNode> _nodes = new List<TreeListNode>(nodes);
            foreach (TreeListNode node in _nodes)
            {
                if (node.HasChildren)
                    DropNode(node.Nodes);
                object obj = RoutingTreeList.GetRow(node.Id);
                if (obj == null)
                    return;
                RoutingTreeList.Nodes.Remove(node);
            }
        }

        // 라우팅 삭제
        private void cmdDelete_Click(object sender, EventArgs e)
        {
            DeleteRoutingMaster();
        }

        private void btnRoutingMasterDelete_Click(object sender, EventArgs e)
        {
            DeleteRoutingMaster();
        }

        private void DeleteRoutingMaster()
        {
            try
            {
                string Process = gvCM_RoutingRev.GetFocusedRowCellValue("RoutingCode") == null ? "" : gvCM_RoutingRev.GetFocusedRowCellDisplayText("RoutingCode");
                if (MessageBox.Show(string.Format("선택한 '{0}' 라우팅을 삭제하시겠습니까? 공정 순서 정보도 같이 삭제됩니다.", Process), "삭제", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }

                // 라우팅 삭제
                CM_Routing cr = new CM_Routing();
                cr.RoutingCode = Process;

                if (!cr.Del_CM_Routing())
                {
                    //저장 중 에러 발생시
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return;
                }

                // 라우팅 리비전 삭제
                CM_RoutingRev rr = new CM_RoutingRev();
                rr.RoutingCode = Process;

                if (!rr.Del_CM_RoutingRev())
                {
                    //저장 중 에러 발생시
                    MessageBox.Show("저장 중 에러가 발생하였습니다.");
                    return;
                }

                MessageBox.Show("자료가 삭제 되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DisplayRoutingMaster();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
