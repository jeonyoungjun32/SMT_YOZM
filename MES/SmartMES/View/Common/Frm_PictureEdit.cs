using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using SmartCommon.Common;
using SmartMES.Models.QM;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SmartMES.View.Common
{
	public partial class Frm_PictureEdit : DevExpress.XtraEditors.XtraForm
    {
        string ItemCd = "";
        string FieldName = "ItemImg";

        string FA_No = "";
        int FA_Row_Seq = 0;

        string ProcessCode = "";
        string ExamDivCd = "";
        string Img_Div_Cd = "";

        bool Pnl1_Visible = true;    //저장, 삭제 패널 Visible 여부
        string Pnl2_Visible = "";

        public Frm_PictureEdit(Image image)
        {
            //각 폼 이미지 클릭 시 크게 보는 용도
            InitializeComponent();
            SetDesign();

            pictureEdit1.Image = image;

            this.panelControl1.Visible = false;
            this.panelControl2.Visible = true;

        }

        public Frm_PictureEdit(Image image, string size)
        {
            //각 폼 이미지 클릭 시 크게 보는 용도
            InitializeComponent();
            SetDesign();

            //팝업화면 사이즈
            if(size.ToUpper() == "MAX") { this.WindowState = FormWindowState.Maximized; }
            else if(size.ToUpper() == "MIN") { this.WindowState = FormWindowState.Minimized; }
            else { this.WindowState = FormWindowState.Normal; }


            pictureEdit1.Image = image;

            this.panelControl1.Visible = false;
            this.panelControl2.Visible = true;

        }

        public Frm_PictureEdit(string Selected_ItemCd, string fieldName, bool pnlVisible = true)
        {
            InitializeComponent();
            ItemCd = Selected_ItemCd;
            FieldName = fieldName;
            Pnl1_Visible = pnlVisible;
            SetDesign();

            Search();
        }

        public Frm_PictureEdit(string fa_no, int fa_row_seq, string fieldName, bool pnlVisible = true)
        {
            //IwAuto 초품불량등록

            InitializeComponent();

            FA_No = fa_no;
            FA_Row_Seq = fa_row_seq;
            FieldName = fieldName;

            Pnl1_Visible = pnlVisible;
            SetDesign();

            Search();
        }

        public Frm_PictureEdit(string ItemCode, string img_Div_Cd, string ProcessCode, string ExamDivCd, bool pnl1Visible, string pnl2Visible)
        {
            //명진 검사기준서
            InitializeComponent();

            this.ItemCd = ItemCode;
            this.Img_Div_Cd = img_Div_Cd;
            this.ProcessCode = ProcessCode;
            this.ExamDivCd = ExamDivCd;
            this.Pnl1_Visible = pnl1Visible;
            this.Pnl2_Visible = pnl2Visible;

            SetDesign();

            Search();
        }

        private void SetDesign()
        {
            pictureEdit1.Properties.ExportMode = ExportMode.Value;                   //Output값 EditValue로설정
            pictureEdit1.Properties.PictureStoreMode = PictureStoreMode.ByteArray;   //데이터 타입 ByteArray로 설정
            pictureEdit1.Properties.SizeMode = PictureSizeMode.Zoom;                 //이미지 사이즈: (Stretch: 화면에 맞춰 늘어남, Zoom: 이미지 비율 맞춤)

            //패널 숨김 기능 수정
            panelControl1.Visible = Pnl1_Visible;
            panelControl2.Visible = !Pnl1_Visible;

            //패널2 숨김처리
            if(Pnl2_Visible != "")
			{
                panelControl2.Visible = Pnl2_Visible == "Y" ? true : false;
			}

            //240521 드림씨앤씨 요청사항 제품사양서, 제품이력 조회 가능하도록 요청
            if (Database.DbName.Contains("DREAMCNC"))
            {
                if(!Pnl1_Visible)
                {
                    panelControl1.Visible = true;
                    BtnUpload.Visible = false;
                    BtnSave.Visible = false;
                    BtnDelete.Visible = false;
                }

                BtnWorkSheet_History.Visible = true;
            }
        }

        private void Search()
        {
            object pic = null;
            DataRow drImg;

            if (FieldName == "CorpImg")
            {
                //사업장로고
                //SC_Corp sc = new SC_Corp();
                //sc.CorpId = ItemCd;
                //drImg = sc.Select_CorpImage();
                //pic = drImg[FieldName];
            }
            else if (FieldName == "CorpImg2")
            {
                //사업장로고2
                //SC_Corp sc = new SC_Corp();
                //sc.CorpId = ItemCd;
                //drImg = sc.Select_CorpImage2();
                //pic = drImg[FieldName];
            }
            else if (FieldName == "FA_Image")
			{
                //QT_FirstArticleInspection fa = new QT_FirstArticleInspection();
                //fa.FA_No = FA_No;
                //fa.FA_Row_Seq = FA_Row_Seq;
                //drImg = fa.Select_FA_Image();
                //pic = drImg[FieldName];
            }

            else if (FieldName == "Monitoring_Img")
            {
                //BS_Monitoring_ChkSheet mc = new BS_Monitoring_ChkSheet();
                //mc.Manage_Cd = ItemCd;
                //drImg = mc.Select_ChkSheet_Img();
                //if (drImg != null)
                //{
                //    pic = drImg[FieldName];
                //}
            }

            else if (FieldName == "GaugeImage")
            {
				QM_Gauge mc = new QM_Gauge();
				mc.GaugeCode = ItemCd;

				drImg = mc.Select_Gauge_Image();

				if (drImg != null)
				{
					pic = drImg[FieldName];
				}
			}
            else
            {
                //품목별 이미지 사용여부
                if (WrGlobal.ProcOption["BS"]["ItemImage_UseYN"] == "Y")
                {
                   //BS_ItemImage bi = new BS_ItemImage();
                   //bi.ItemCode = ItemCd;
                   //bi.Img_Div_Cd = Img_Div_Cd;
                   //bi.ProcessCode = ProcessCode;
                   //bi.ExamDivCd = ExamDivCd;
                   //
                   //drImg = bi.Select_LastImg();
                   //
                   //if (drImg != null)
                   //{
                   //    pic = drImg["Item_Img"];
                   //}
                }
				else
				{
                    ////제품이미지 , 제품사양서
                    //BS_Item bi = new BS_Item();
                    //bi.ItemCode = ItemCd;
                    //drImg = bi.Select_ItemImage();
                    //pic = drImg[FieldName];
				}
            }

            PictureEdit pictureEdit = new PictureEdit();
            pictureEdit1.EditValue = pic;
            //사진이 늘어나지 않음
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            SimpleButton Btn = (SimpleButton)sender;

            string btnname = Btn.Name.ToString();

            switch (btnname)
            {
                case "BtnUpload":
                    UploadImg();
                    break;

                case "BtnSave":
                    SaveImg();
                    break;

                case "BtnDelete":
                    DeleteImg();
                    break;

                case "BtnClose":
                    this.Close();
                    break;
            }
        }

        private void UploadImg()
        {
            // 이미지 로드 실행 코드 작성
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.bmp; *.jpg; *.jpeg; *.png;)| *.BMP; *.JPG; *.JPEG; *.PNG;";
            //"이미지 파일 (*.jpg)|*.jpg|이미지 파일 (*.PNG)|*.PNG "; //모든 파일 (*.*)|*.*

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // 선택된 파일을 PictureEdit 컨트롤에 로드
                    byte[] fileBytes = File.ReadAllBytes(openFileDialog.FileName);
                    pictureEdit1.EditValue = fileBytes;
                    //pictureEdit1.EditValue = Image.FromFile(openFileDialog.FileName); //이미지로 읽어와서 저장X
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void SaveImg()
        {
            //이미지 저장
            bool success = true;

            //BS_Item bi = new BS_Item();
            //SC_Corp sc = new SC_Corp();
            //BS_Monitoring_ChkSheet mc = new BS_Monitoring_ChkSheet();
            //QT_FirstArticleInspection fa = new QT_FirstArticleInspection();
            //QC_Moist moist = new QC_Moist();
            QM_Gauge gg = new QM_Gauge();

            //bi.ItemCode = ItemCd;
            //sc.CorpId = ItemCd;
            //mc.Manage_Cd = ItemCd;

            switch (FieldName)
            {
				/*
                case "ItemImg":
                    bi.ItemImg = pictureEdit1.EditValue as byte[];
                    success = bi.Upd_ItemImg();
                    break;

                case "Work_Standard_Sheet":
                    bi.Work_Standard_Sheet = pictureEdit1.EditValue as byte[];
                    success = bi.Upd_Item_Work_Standard_Sheet();
                    break;

                case "CorpImg":
                    sc.CorpImg = pictureEdit1.EditValue as byte[];
                    success = sc.Upd_CorpImg();
                    break;

                case "CorpImg2":
                    sc.CorpImg = pictureEdit1.EditValue as byte[];
                    success = sc.Upd_CorpImg2();
                    break;

                case "FA_Image":
                    fa.FA_No = FA_No;
                    fa.FA_Row_Seq = FA_Row_Seq;
                    fa.FA_Image = pictureEdit1.EditValue as byte[];
                    success = fa.Upd_FA_Image();
                    break;

                case "Monitoring_Img":
                    mc.ItemImg = pictureEdit1.EditValue as byte[];
                    success = mc.Upd_ChkSheet_Img();
                    break;
                    */
				case "GaugeImage":
					gg.GaugeCode = ItemCd;
					gg.GaugeImage = pictureEdit1.EditValue as byte[];
					success = gg.Upd_QM_Gauge_Image();
					break;

			}

            if (success)
            {
                MessageBox.Show("저장이 정상적으로 완료되었습니다.");
                Search();
                Close();
            }
            else
            {
                MessageBox.Show("삭제 중 에러가 발생하였습니다.");
            }
        }

        private void DeleteImg()
        {
            bool success = true;

            //BS_Item bi = new BS_Item();
            //SC_Corp sc = new SC_Corp();
            //BS_Monitoring_ChkSheet mc = new BS_Monitoring_ChkSheet();
            //QT_FirstArticleInspection fa = new QT_FirstArticleInspection();
            //QC_Moist moist = new QC_Moist();
            QM_Gauge gg = new QM_Gauge();

            //bi.ItemCode = ItemCd;
            //sc.CorpId = ItemCd;
            //mc.Manage_Cd = ItemCd;
            pictureEdit1.EditValue = new byte[0];

            switch (FieldName)
            {
                /*
                case "ItemImg":
                    bi.ItemImg = new byte[0];
                    success = bi.Upd_ItemImg();
                    break;

                case "Work_Standard_Sheet":
                    bi.Work_Standard_Sheet = new byte[0];
                    success = bi.Upd_Item_Work_Standard_Sheet();
                    break;

                case "CorpImg":
                    sc.CorpImg = pictureEdit1.EditValue as byte[];
                    success = sc.Upd_CorpImg();
                    break;

                case "CorpImg2":
                    sc.CorpImg = pictureEdit1.EditValue as byte[];
                    success = sc.Upd_CorpImg2();
                    break;

                case "FA_Image":
                    fa.FA_No = FA_No;
                    fa.FA_Row_Seq = FA_Row_Seq;
                    fa.FA_Image = pictureEdit1.EditValue as byte[];
                    success = fa.Upd_FA_Image();
                    break;
                case "Monitoring_Img":
                    mc.ItemImg = new byte[0];
                    success = mc.Upd_ChkSheet_Img();
                    break;
                */

                case "GaugeImage":
                    gg.GaugeCode = ItemCd;
                    gg.GaugeImage = null;
                    success = gg.Upd_QM_Gauge_Image();
                    break;
            }

            if (success)
            {
                MessageBox.Show("삭제가 정상적으로 완료되었습니다.");
                Search();
            }
            else
            {
                MessageBox.Show("삭제 중 에러가 발생하였습니다.");
            }
        }

        private void BtnWorkSheet_History_Click(object sender, EventArgs e)
        {
            //Frm_Item_Specification_POP frm = new Frm_Item_Specification_POP(this.ItemCd);
            //frm.ShowDialog();
        }
    }
}