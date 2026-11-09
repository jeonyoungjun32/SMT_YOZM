using baseSMT;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Models;
using SmartMES.Models.QM;
using SMT_MES.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using static SmartCommon.Common.clsGrid;

namespace SmartMES.View.QM.Gauge
{
	public partial class Frm_QM_GaugeCalibrationCertificate : DevExpress.XtraEditors.XtraForm
    {
        clsGrid grid = new clsGrid();
        clsRepository rpo = new clsRepository();

        clsCode code = new clsCode();

        public Frm_QM_GaugeCalibrationCertificate()
        {
            InitializeComponent();
            SetDesign();
        }

        #region 디자인 아이템 생성

        private void SetDesign()
        {
            Dt_Date.smtStartDateTimeValue = DateTime.Now.AddYears(-2);
            Dt_Date.smtEndDateTimeValue = DateTime.Now;

            //GridView 데이터 입력
            Set_GvGauge(Gv_GaugeCal);
        }
   
        #region GridView Setting

        private void Set_GvGauge(GridView gv)
        {
            //GvMachine Column 생성 및 셋팅
            //Grid 공통 설정
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;   //Header 정렬
            gv.OptionsView.ColumnAutoWidth = false;     //Column 많으면 스크롤 생기도록 바꾸기
            gv.IndicatorWidth = 50;                     //순번 너비
            //검색창
            gv.OptionsFind.AlwaysVisible = false;
            gv.OptionsView.ShowGroupPanel = false;

            RepositoryItemSearchLookUpEdit lup_Result = rpo.Create_RepositoryItemSearchLookUp(code.GetCM_CateItem(WrGlobal.CorpID, "Q020", false), "코드", "명칭");
            RepositoryItemSearchLookUpEdit lup_Emp_No = rpo.Create_RepositoryItemSearchLookUp(code.GetDataTableUser(false), "코드", "명칭");
            RepositoryItemButtonEdit btnPreView = rpo.Create_rpoBtn();
			btnPreView.ButtonClick += BtnPreView_ButtonClick;

            //Column 생성
            grid.Create_GvColumn(gv, "계측기코드", "GaugeCode");
            grid.Create_GvColumn(gv, "계측기명", "GaugeName");
            grid.Create_GvColumn(gv, "관리번호", "SerialNo");

            grid.Create_GvColumn(gv, "교정일자", "CalibrationDate", rpo.Create_rpoDate());
            grid.Create_GvColumn(gv, "교정업체", "CalibrationCompany");
            grid.Create_GvColumn(gv, "교정결과", "Result", lup_Result);

            grid.Create_GvColumn(gv, "비고", "Remark", rpo.Create_rpoMemo());
            grid.Create_GvColumn(gv, "파일명", "OriFileName");
            grid.Create_GvColumn(gv, "보기", "Preview", btnPreView, true);

            //Column 단일 설정
            //VISIBLE

            //Repository 설정

            //셀값 정렬
            grid.SetCol_Type(gv.Columns["CalibrationDate"], TypeList.TDate);
            gv.Columns["Result"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            //칼럼 너비 설정

            //이벤트 설정
            gv.CustomDrawRowIndicator += grid.RowNumber_Indicator;    //행번호 표기
			//gv.RowCellStyle += Gv_RowCellStyle;
        }

        private void BtnPreView_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            //Grid 보기 버튼
            DataRow dr = Gv_GaugeCal.GetFocusedDataRow();

            if (dr == null)
                return;

            string fileName = dr["FileName"].ConvertString_Null_To_Empty();

            if (fileName == "")
            {
                XtraMessageBox.Show("등록된 성적서가 없습니다.");
                return;
            }

            //FTP 파일 다운로드
            string localFilePath = DownloadCertificateFile(dr);

            if (localFilePath == "")
                return;

            //파일 열기
            OpenExternalFile(localFilePath);
        }


        private string DownloadCertificateFile(DataRow dr)
        {
            if (dr == null)
                return "";

            string path = dr["Path"].ConvertString_Null_To_Empty();
            string oriFileName = dr["OriFileName"].ConvertString_Null_To_Empty();
            string fileName = dr["FileName"].ConvertString_Null_To_Empty();
            long capacity = (long) dr["Capacity"];

            if (fileName == "")
            {
                XtraMessageBox.Show("등록된 성적서가 없습니다.");
                return "";
            }

            try
            {
                //임시 다운로드 폴더
                string tempPath = Path.Combine(
                    Path.GetTempPath(),
                    "SmartMES",
                    "GaugeCertificate");

                if (!Directory.Exists(tempPath))
                {
                    Directory.CreateDirectory(tempPath);
                }

                List<string> selPaths = new List<string>();
                List<string> selOriFiles = new List<string>();
                List<string> selFiles = new List<string>();

                selPaths.Add(path);
                selOriFiles.Add(oriFileName);
                selFiles.Add(fileName);

                FtpFileDownloader downloader = new FtpFileDownloader(
                    tempPath,
                    selPaths.ToArray(),
                    selOriFiles.ToArray(),
                    selFiles.ToArray(),
                    capacity);

                downloader.ShowDialog();

                //FtpFileDownloader가 원본파일명으로 저장하는 경우
                string localFilePath = Path.Combine(tempPath, oriFileName);

                if (!File.Exists(localFilePath))
                {
                    XtraMessageBox.Show("다운로드한 파일을 찾을 수 없습니다.");
                    return "";
                }

                return localFilePath;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    "성적서를 불러오는 중 오류가 발생하였습니다.\n" +
                    ex.Message);

                return "";
            }
        }

        private void OpenExternalFile(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    XtraMessageBox.Show("파일을 찾을 수 없습니다.");
                    return;
                }

                System.Diagnostics.ProcessStartInfo psi =
                    new System.Diagnostics.ProcessStartInfo();

                psi.FileName = filePath;
                psi.UseShellExecute = true;

                System.Diagnostics.Process.Start(psi);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    "파일을 열 수 없습니다.\n" +
                    ex.Message);
            }
        }

		#endregion GridView Setting End

		#endregion 디자인 아이템 생성 종료

		#region Event

		private void Frm_QM_GaugeCalibration_Load(object sender, EventArgs e)
        {
            Search();
        }

        private void cmd_Click(object sender, EventArgs e)
        {
            smtSearchButton Btn = (smtSearchButton)sender;

            string btnname = Btn.Name.ToString();

            switch (btnname)
            {
                case "cmdSearch":
                    Search();
                    break;

                case "cmdExcel":
                    clsFunction func = new clsFunction();
                    func.ExportExcel(Gv_GaugeCal);
                    break;

                case "cmdClose":
                    this.Close();
                    break;
            }
        }

        #endregion Event End

        #region 조회 기능

        private void Search()
        {
            string startDate = Dt_Date.smtStartDateTimeValue.ToString("d");
            string endDate = Dt_Date.smtEndDateTimeValue.ToString("d");

            QM_GaugeCalibration cal = new QM_GaugeCalibration();
            Gc_GaugeCal.DataSource = cal.Select_QM_GaugeCal_Certificate(startDate, endDate);

			Gv_GaugeCal.BestFitColumns();   //Column 너비 조정
        }
 
        #endregion 조회 기능 Endz
    }
}