using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using SmartMES.Models.ST;
using SmartMES.Models.QM;
using System;
using System.Data;
using System.Windows.Forms;
using SmartMES.Models.PM;
using DevExpress.XtraEditors;

namespace SmartMES.View.PM.Popup
{
    public partial class Frm_PM_BadQty : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        DataRow dr = null;

        public Frm_PM_BadQty(DataRow _dr)
        {
            InitializeComponent();
            dr = _dr;

            //정수표시
            Set_TextEdit(Txt_WorkQty);
            Set_TextEdit(Txt_GoodQty);

            Txt_GoodQty.Text = dr["GoodQty"].ConvertString_Null_To_Empty();
            Txt_WorkQty.Text = dr["WorkQty"].ConvertString_Null_To_Empty();
        }

        private void Frm_Load(object sender, EventArgs e)
        {
            Set_GridView(Gv_Bad_Detail);
            Set_RowInfo(dr);
            Select_QT_Bad_Chk();
        }

        private void Set_TextEdit(TextEdit textEdit)
        {
            textEdit.Properties.DisplayFormat.FormatType = FormatType.Numeric;
            textEdit.Properties.DisplayFormat.FormatString = "n0";
            textEdit.Properties.EditFormat.FormatType = FormatType.Numeric;
            textEdit.Properties.EditFormat.FormatString = "n0";
            textEdit.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            textEdit.Properties.MaskSettings.Set("mask", "n0");
            textEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            textEdit.Properties.Mask.EditMask = "n0";
            textEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
        }

        private void Set_GridView(GridView gv)
		{
            //Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = true;
            gv.OptionsView.RowAutoHeight = true;
            gv.OptionsView.ShowFooter = true;   //하단 합계창 보이기

            //Column 생성
            grid.Create_GvColumn(gv, "불량구분", "FaultCode");
            grid.Create_GvColumn(gv, "불량유형", "Bad_Nm");
            grid.Create_GvColumn(gv, "수량", "NgQty");

            gv.Columns["NgQty"].ColumnEdit = new clsRepository().Create_rpoTxt_Numeric();

            grid.Column_AllowEdit(gv.Columns["NgQty"]);

            //표시타입
            grid.SetCol_Type(gv.Columns["NgQty"], clsGrid.TypeList.TInt);

            //그리드 하단 불량 수량 합계 표시
            grid.SetCol_Summary(gv, "NgQty");
        }

        private void Set_RowInfo(DataRow dr)
        {
            Txt_MaxBadQty.Text = dr["NgQty"].ConvertInt_Null_To_Zero().ToString();
        }

		private void Select_QT_Bad_Chk()
        {
            QM_WorkDefect bc = new QM_WorkDefect();

            bc.SerialNo = dr["SerialNo"].ConvertString_Null_To_Empty();
            bc.SubSeq = dr["SubSeq"].ConvertInt_Null_To_Zero();
            bc.FromProcessCode = dr["ProcessCode"].ConvertString_Null_To_Empty();

            DataTable dt = null;
            dt = bc.Select_NgQTY();

            Gc_Bad_Detail.DataSource = dt;
            Gv_Bad_Detail.BestFitColumns();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            //유형별 불량수량 수정
            Gv_Bad_Detail.PostEditor();
            Gv_Bad_Detail.UpdateCurrentRow();

            if (!Update_QT_Bad_Chk())
            {
                MessageBox.Show("불량 등록시 오류");
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();

            MessageBox.Show("저장이 정상적으로 완료되었습니다.");
        }

        private bool Update_QT_Bad_Chk()
		{
            try
            {
                QM_WorkDefect bc = new QM_WorkDefect();
                bc.SerialNo = Convert.ToString(dr["SerialNo"]);
                bc.Seq = dr["OrderSeq"].ConvertInt_Null_To_Zero(); ; // 순번
                bc.ExamType = "10"; // 10:생산, 20:검사
                bc.SubSeq = dr["SubSeq"].ConvertInt_Null_To_Zero();
                bc.OrderNo = Convert.ToString(dr["OrderNo"]);
                bc.FromProcessCode = Convert.ToString(dr["ProcessCode"]);
                bc.ItemCode = Convert.ToString(dr["ItemCode"]);
                bc.WorkDate = DateTime.Now.ToString("d");

                DataTable BadData = Gc_Bad_Detail.DataSource as DataTable;

                int sumQty = 0;

                for (int i = 0; i < BadData.Rows.Count; i++)
                {
                    bc.ReworkID = BadData.Rows[i]["ReworkID"].ToString();
                    bc.DefectCode = BadData.Rows[i]["FaultCode"].ToString();
                    bc.Qty = BadData.Rows[i]["NgQty"].ConvertFloat_Null_To_Zero();
                    sumQty += BadData.Rows[i]["NgQty"].ConvertInt_Null_To_Zero();

                    DataTable chkTable = bc.chkQM_WorkDefect();
                    if (bc.Qty != 0)
                    {
                        if (chkTable.Rows.Count == 0)
                        {
                            string newReworkID = bc.GetNextReworkID();
                            bc.ReworkID = newReworkID;

                            bc.Ins_QM_WorkDefect();
                        }
                        else
                        {
                            bc.Upd_QM_WorkDefect();
                        }
                    }
                    else
                    {
                        // 불량 수량이 0일 경우, QM_WorkDefect 테이블에서 삭제
                        if (chkTable.Rows.Count > 0)
                        {
                            bc.DelQM_WorkDefect_BySerialNo();
                        }
                    }
                }

                // Update PM_OrderResult
                PM_OrderResult odr = new PM_OrderResult();
                odr.SerialNo = Convert.ToString(dr["SerialNo"]);
                odr.SubSeq = dr["SubSeq"].ConvertInt_Null_To_Zero();
                odr.NgQty = sumQty;

                if (!odr.Upd_NgQTY())
                {
                    return false;
                }

                //지시테이블 생산, 양품, 불량 수량 합계 업데이트
                PM_OrderProcess op = new PM_OrderProcess();
                op.OrderNo = Convert.ToString(dr["OrderNo"]);
                op.OrderSeq = dr["OrderSeq"].ConvertInt_Null_To_Zero();
                op.Upd_Qty_OrderNoSeq();

                //생산재고 입고 수량 수정
                ST_StockD std = new ST_StockD();
                std.CorpId = WrGlobal.CorpID;
                std.JobNo = Convert.ToString(dr["OrderNo"]);
                std.JobSeq = dr["OrderSeq"].ConvertInt_Null_To_Zero();
                std.JobSeq2 = dr["SubSeq"].ConvertInt_Null_To_Zero();
                std.Type = "생산";

                decimal workQty = Txt_WorkQty.EditValue.ConvertDecimal_Null_To_Zero();
                decimal ngQty = odr.NgQty;
                std.Qty = workQty - ngQty;

                std.Upd_Qty_ByDefect();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}