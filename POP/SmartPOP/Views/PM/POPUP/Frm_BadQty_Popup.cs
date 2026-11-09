using DevExpress.Dialogs.Core.Base.ComInterfaces;
using DevExpress.LookAndFeel;
using DevExpress.Pdf.Native.BouncyCastle.Asn1.X509;
using DevExpress.Skins;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using SmartPOP.Models.PM;
using SmartPOP.Common;
using SmartPOP.Models.QM;
using SmartPOP.Models.ST;
using SmartPOP.Views.POPUP;
using SMT_MES.Controls;
using System;
using System.Data;
using System.Drawing;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace SmartPOP.Views.Work.Popup
{
	public partial class Frm_BadQty_Popup : DevExpress.XtraEditors.XtraForm
    {
        cls_Grid grid = new cls_Grid();
        DataRow dr = null;

        string Target = ""; //터치패드 입력 대상

        public Frm_BadQty_Popup(DataRow _dr)
        {
            InitializeComponent();
            dr = _dr;  
        }

        private void Frm_Load(object sender, EventArgs e)
        {
            Txt_GoodQty.EditValue = dr["GoodQty"].ConvertFloat_Null_To_Zero();
            Txt_WorkQty.EditValue = dr["WorkQty"].ConvertFloat_Null_To_Zero();
            Txt_MaxBadQty.EditValue = dr["NgQty"].ConvertFloat_Null_To_Zero();

            Set_GridView(Gv_Bad_Detail);
            Select_QT_Bad_Chk();
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

            gv.Columns["NgQty"].ColumnEdit = new cls_Repository().Create_rpoTxt_Numeric();

            //표시타입
            grid.SetCol_Type(gv.Columns["NgQty"], cls_Grid.TypeList.TInt);

            //그리드 하단 불량 수량 합계 표시
            grid.SetPOPGrid(gv);
            grid.SetCol_Summary(gv, "NgQty");
            gv.RowCellClick += Gv_RowCellClick;                       //POP 용숫자패드
            //gv.CellValueChanged += Gv_CellValueChanged;
        }

        private void Gv_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
		{
			//시, 분 터치패드 입력
			GridView gv = sender as GridView;
			if (e.RowHandle < 0) return;

			DataRow drChanged = gv.GetDataRow(e.RowHandle);

			string fieldName = e.Column.FieldName;

			if (fieldName == "NgQty")
			{
				string BtnTag = "Qty";
				Frm_NumPad NumPad = new Frm_NumPad(BtnTag, 0);
				//터치패드로 입력한 값을 넣을 컨트롤명
				Target = fieldName;

				// 방법1: 화면의 오른쪽 중앙 고정
				int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
				int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
				int x = (screenWidth - NumPad.Width) / 4;
				int y = (screenHeight - NumPad.Height) / 2;

				NumPad.StartPosition = FormStartPosition.Manual;
				NumPad.Location = new Point(x, y);

				NumPad.FormSendEvent += new Frm_NumPad.FormSendDataHandler(AddNum_cell);
				NumPad.ShowDialog();
			}
		}

		//숫자패드
		private void AddNum_cell(string _num)
		{
			//수량/값 터치패드 입력
			try
			{

				GridView gv = Gv_Bad_Detail;

				int SaveFocusedRowInx = gv.FocusedRowHandle;

				//string searchString = "Qty";
				//bool isQty = Target.Contains(searchString);

				//if (isQty)
				//{
					gv.SetRowCellValue(SaveFocusedRowInx, Target, _num);
                //}
                //else
                //{
                //	string numTime = _num.PadLeft(4, '0');
                //	gv.SetRowCellValue(SaveFocusedRowInx, Target, numTime);
                //}

                gv.UpdateSummary();
			}
			catch { }
		}

        private void Gv_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView gv = sender as GridView;
            DataRow drChanged = (gv.GridControl.DataSource as DataTable).Rows[e.RowHandle];
            string colName = e.Column.FieldName;

            //0. 수정사항 있을시 상태값 수정 표기
            if (colName == gv.Columns[0].FieldName) return;
            grid.Change_Status(gv, e.RowHandle);
        }

		private void Select_QT_Bad_Chk()
        {
            QM_WorkDefect bc = new QM_WorkDefect();

            bc.SerialNo = dr["SerialNo"].ConvertString_Null_To_Empty();
            bc.SubSeq = dr["SubSeq"].ConvertInt_Null_To_Zero();

            DataTable dt = null;
            dt = bc.Select_NgQTY();

            Gc_Bad_Detail.DataSource = dt;
            Gv_Bad_Detail.BestFitColumns();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            //유형별 불량수량 수정
            if (!Update_QT_Bad_Chk())
            {
                cls_MessageBox.Show("불량 등록시 오류");
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();

            cls_MessageBox.Show("저장이 정상적으로 완료되었습니다.");
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
                bc.ProcessCode = Convert.ToString(dr["ProcessCode"]);
                bc.MaterialCode = Convert.ToString(dr["MaterialCode"]);

                DataTable BadData = Gc_Bad_Detail.DataSource as DataTable;

                int sumQty = 0;

                for (int i = 0; i < BadData.Rows.Count; i++)
                {
                    bc.ReworkID = BadData.Rows[i]["ReworkID"].ToString();
                    bc.DefectCode = BadData.Rows[i]["FaultCode"].ToString();
                    bc.Qty = BadData.Rows[i]["NgQty"].ConvertFloat_Null_To_Zero();
                    sumQty += BadData.Rows[i]["NgQty"].ConvertInt_Null_To_Zero();

                    //수량이 0이 아닐때만 저장, 0으로 변경 시 삭제 처리
                    if(bc.Qty == 0)
					{
                        bc.DelQM_WorkDefect();
					}
					else
					{
                        if (bc.ChkQM_WorkDefect())
                        {
                            bc.Upd_QM_WorkDefect();
                        }
                        else
                        {
                            bc.Ins_QM_WorkDefect();
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

                //재고
                //생산재고 입고 수량 수정
                ST_StockD std = new ST_StockD();
                std.CorpId = WrGlobal.CorpID;
                std.JobNo = odr.OrderNo;
                std.JobSeq = odr.OrderSeq;
                std.JobSeq2 = odr.SubSeq;
                std.Type = "생산";
                std.Qty = Txt_WorkQty.EditValue.ConvertInt_Null_To_Zero() - odr.NgQty;

                std.Upd_Qty_ByDefect();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}