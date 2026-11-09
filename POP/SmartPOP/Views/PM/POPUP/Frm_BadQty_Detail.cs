using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartPOP.Models.PM;
using SmartPOP.Common;
using SmartPOP.Models.QM;
using SmartPOP.Models.ST;
using SMT_MES.Controls;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartPOP.Views.Work.Popup
{
    public partial class Frm_BadQty_Detail : DevExpress.XtraEditors.XtraForm
    {
        Database db = new Database();
        string procType = "";
        #region Property

        public string SerialNo { get; set; }
        public string OrderNo { get; set; }
        public int OrderSeq { get; set; }
        public int SubSeq { get; set; }
        public string MaterialCode { get; set; }
        public int Rst_Row_Seq { get; set; }        
        public string In_No { get; set; }
        public int In_Row_Seq { get; set; }
        public string Bad_Date { get; set; }
        public int MaxBadQty { get; set; }

        cls_Grid grid = new cls_Grid();
        cls_Repository rpo = new cls_Repository();

        DataRow dr = null;

        QM_WorkDefect df = new QM_WorkDefect();

        #endregion

        public Frm_BadQty_Detail(DataRow _dr, string _type)
        {
            InitializeComponent();

            dr = _dr;
            procType = _type;

            if(procType == "가공")
            {
                SerialNo = dr["SerialNo"].ConvertString_Null_To_Empty();
                SubSeq = Convert.ToInt16(dr["SubSeq"]);
                OrderNo = dr["OrderNo"].ConvertString_Null_To_Empty();
                OrderSeq = Convert.ToInt16(dr["OrderSeq"]);
                MaterialCode = dr["ProductCode"].ConvertString_Null_To_Empty();

            }
            else if(procType == "조립")
            {
                OrderNo = dr["OrderNo"].ConvertString_Null_To_Empty();
                OrderSeq = Convert.ToInt16(dr["OrderSeq"]);
                MaterialCode = dr["ProductCode"].ConvertString_Null_To_Empty();

                string query = $@"SELECT  OrderNo, SerialNo, SubSeq FROM PM_OrderResult WHERE OrderNo = '{OrderNo}' ANd OrderSeq = '{OrderSeq}' ";
                DataTable dtS = db.GetDataTable(query);

                if (dtS.Rows.Count == 0) return;
                SerialNo = dtS.Rows[0]["SerialNo"].ConvertString_Null_To_Empty();
                SubSeq = dtS.Rows[0]["SubSeq"].ConvertInt_Null_To_Zero();
            }

            /*
            InitializeComponent();
            dr = _dr;

            if (dr.Table.Columns.Contains("SerialNo"))
            {
                SerialNo = dr["SerialNo"].ConvertString_Null_To_Empty();
            }

            else if (dr.Table.Columns.Contains("In_No"))
            {
                In_No = dr["In_No"].ConvertString_Null_To_Empty();
                In_Row_Seq = dr["In_Row_Seq"].ConvertInt_Null_To_Zero();
                Lbl_Good_Qty.Visible = true;
                Lbl_In_Qty.Visible = true;
                Txt_Good_Qty.Visible = true;
                Txt_In_Qty.Visible = true;
                Txt_Good_Qty.Text = Convert.ToString(dr["Good_Qty"]);
                Txt_In_Qty.Text = Convert.ToString(dr["In_Qty"]);
            }*/
        }

        private void Frm_Load(object sender, EventArgs e)
        {
            Set_GridView(Gv_Bad_Detail);
            Select_QT_Bad_Chk();
        }

        private void Set_GridView(GridView gv)
		{
            //Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = true;

            gv.Appearance.HeaderPanel.Font = new Font("Pretendard SemiBold", 15, FontStyle.Bold);   //gridview 폰트 조정
            gv.Appearance.Row.Font = new Font("Pretendard SemiBold", 15, FontStyle.Regular);        //gridview 폰트 조정

            gv.OptionsView.ShowFooter = true;   //하단 합계창 보이기

            //Column 생성
            grid.Create_GvColumn(gv, "상태", "Status");
            grid.Create_GvColumn(gv, "불량구분", "ProcessName");
            grid.Create_GvColumn(gv, "불량유형", "Title");
            grid.Create_GvColumn(gv, "수량", "FaultQty");

            //수정가능여부
            grid.Column_AllowEdit(gv.Columns["FaultQty"]);

            //칼럼 Visible
            gv.Columns["Status"].Visible = false;

            //그리드 하단 불량 수량 합계 표시
            gv.Columns["FaultQty"].Summary.Add(DevExpress.Data.SummaryItemType.Sum);

            //표기타임
            grid.SetCol_Type(gv.Columns["FaultQty"], cls_Grid.TypeList.TInt);
            gv.Columns["FaultQty"].AppearanceCell.BackColor = Color.PapayaWhip;
            gv.Columns["FaultQty"].ColumnEdit = new cls_Repository().Create_rpoTxt_Numeric();

            gv.CellValueChanged += Gv_CellValueChanged;
        }

        private void Gv_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //상단 등록 가능한 불량수량과 그리드에 등록한 불량수량 비교
        }

		private void Select_QT_Bad_Chk()
        {
            df.ProcessCode = dr["ProcessCode"].ConvertString_Null_To_Empty();
            df.ExamType = "10"; //10:생산 20:공정
            df.SubSeq = SubSeq;

            if (procType == "가공") { df.SerialNo = dr["SerialNo"].ConvertString_Null_To_Empty(); }
            else if (procType == "조립") { df.SerialNo = SerialNo; }

            DataTable dt = null;
            dt = df.Select_QtBadChk();
            
            //불량유형 한 눈에 볼 수 있게 폼 높이 조절
            if (dt.Rows.Count >= 8)
            {
                this.Height += 250;
            }

            Gc_Bad_Detail.DataSource = dt;
            Gv_Bad_Detail.BestFitColumns();
        }

        #region 저장
        private void BtnSave_Click(object sender, EventArgs e)
        {
            //유형별 불량수량 수정
            if(!Update_QT_Bad_Chk())
            {
                cls_MessageBox.Show("불량 등록시 오류");
                return;
            }

            //유형별 불량수량 등록 완료시 실적에서 불량수량, 양품수량 조정
            Tuple<bool, float> result = Update_ResultQty();
            if (!result.Item1)
			{
                cls_MessageBox.Show("실적 수정 오류");
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private bool Update_QT_Bad_Chk()
		{
            //유형별 불량수량 모두 저장 후 현재 테이블과 비교 후 추가, 수정, 삭제 결정
            try
            {
                int goodQty = 0;
                int ngQty = 0;

                DataTable BadData = Gc_Bad_Detail.DataSource as DataTable;

                df.SerialNo = SerialNo;
                df.OrderNo = OrderNo;
                df.SubSeq = SubSeq;
                df.MaterialCode = MaterialCode;

                for (int i = 0; i < BadData.Rows.Count; i++)
                {
                    df.BadCode = (BadData.Rows[i]["FaultCode"]).ConvertString_Null_To_Empty();
                    df.BadQty = (BadData.Rows[i]["FaultQty"]).ConvertFloat_Null_To_Zero();
                    ngQty += (BadData.Rows[i]["FaultQty"]).ConvertInt_Null_To_Zero();

                    df.Del_QtBacChk();
                    string rID = df.Get_ReWorkID();

                    df.ReWorkID = rID;

                    df.Upd_QtBadChk();
                }

                PM_OrderResult or = new PM_OrderResult();
                or.OrderNo = OrderNo;
                or.OrderSeq = OrderSeq;
                or.SubSeq = SubSeq;
                or.NgQty = ngQty;

                or.Update_PM_OrderResult_NgQty();

                ngQty = 0;

                DataTable dtResult = or.Select_PM_OrderResult_ByOrderNo();

                foreach (DataRow dr in dtResult.Rows)
                {
                    goodQty += dr["GoodQty"].ConvertInt_Null_To_Zero();
                    ngQty += dr["NgQty"].ConvertInt_Null_To_Zero();
                }

                PM_OrderProcess op = new PM_OrderProcess();
                op.OrderNo = OrderNo;
                op.OrderSeq = OrderSeq;
                op.WorkQty = goodQty + ngQty;
                op.GoodQty = goodQty;
                op.NgQty = ngQty;

                op.Upd_Qty();

                ST_StockD sd = new ST_StockD();

                string productCode = dr["ProductCode"].ConvertString_Null_To_Empty();

                sd.JobNo = SerialNo;
                sd.Remark = "자동등록";

                sd.Upd_ST_StockD_WasteQty(OrderNo, productCode);

                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion 

        public Tuple<bool, float> Update_ResultQty()
		{
            try
            {
                DataTable BadData = Gc_Bad_Detail.DataSource as DataTable;
                
                float BadQty = 0;

                for(int i = 0; i < BadData.Rows.Count; i++)
                {
                    BadQty += Convert.ToSingle(BadData.Rows[i]["FaultQty"]);
                }

                if(Gv_Bad_Detail.DataRowCount == 0)
                {
                    BadQty = dr["FaultQty"].ConvertFloat_Null_To_Zero();
                }

                else if (dr.Table.Columns.Contains("SerialNo"))
                {
                    df.SerialNo = SerialNo;
                    df.Seq = Rst_Row_Seq;
                    df.BadQty = BadQty;

                    df.Upd_ResultQty();

                    //float work_Qty = dr["Work_Qty"].ConvertFloat_Null_To_Zero();
                    float work_Qty = 0;

                    //제품입고 테이블 UPDATE
                    df.CodeString = SerialNo;
                    df.CodeInt = Rst_Row_Seq;
                    df.CodeFloat = work_Qty - BadQty;
                    df.CodeFloat = work_Qty - BadQty;
                    df.CodeFloat = BadQty;
                    df.Upd_ProdIn_Qty_ByRst();
                }

                return Tuple.Create(true, BadQty);
            }
            catch (Exception ex)
            {
                return Tuple.Create<bool, float>(false, 0);
            }

        }
    }
}