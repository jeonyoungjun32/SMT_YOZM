using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartMES.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartMES.UserControls
{
    public partial class Btn_ExcelDown_ALL : DevExpress.XtraEditors.XtraUserControl
    {
        [DisplayName("DownGrid"),
         Description("DownLoad할 GridControl, 현재 조회되고있는 MainView를 DownLoad 합니다.")]
        public GridControl DownGridControl { get; set; }
        public string FormName {get; set;}
        public Btn_ExcelDown_ALL()
        {
            InitializeComponent();            
            Btn_Save_Xls.Click += new EventHandler(Btn_Click);
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            if (DownGridControl == null)
            {
                XtraMessageBox.Show("엑셀다운 실패");
                return;
            }

            if(DownGridControl != null)
            {
                GridView gv = DownGridControl.MainView as GridView;
                if (gv != null)
                {
                    SaveFileDialog saveFile = new SaveFileDialog();
                    saveFile.Filter = "Excel File|*.xlsx"; //파일형식
                    saveFile.Title = "Save an Excel File"; //파일 저장 시 좌측상단에 나타나는 제목

                    // FormName에 따라 기본 파일명 설정
                    if(FormName != null)
                    {
                        saveFile.FileName = FormName + "_" + DateTime.Now.ToString("yyyyMMddHHmm") + $"_{WrGlobal.LoginUserNM}";// 폼이름 + 당일 날짜 + 시간이 최초 파일명으로 설정
                    }
                    // 
                    else
                    {
                        //saveFile.FileName = DateTime.Now.ToString($"yyyyMMddHHmm_{WrGlobal.LoginUserNM}"); // 폼이름이 없을 경우 당일 날짜 + 시간이 최초 파일명으로 설정
                        saveFile.FileName = DateTime.Now.ToString("yyyyMMddHHmm")+$"_{WrGlobal.LoginUserNM}"; // 폼이름이 없을 경우 당일 날짜 + 시간이 최초 파일명으로 설정
                    }
                    
                    if (saveFile.ShowDialog() == DialogResult.Cancel)
                    {
                        return;
                    }


                    // 파일명을 설정하지 않으면(빈 값) 저장 취소
                    if (string.IsNullOrEmpty(saveFile.FileName))
                    {
                        MessageBox.Show("파일명을 입력하세요.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    try
                    {
                        gv.ExportToXlsx(saveFile.FileName);                        
                        MessageBox.Show("엑셀 파일 저장 완료", "파일 저장", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // 저장된 파일 열기
                        Process.Start(saveFile.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"파일 저장 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (gv.RowCount == 0)
                {
                    MessageBox.Show("조회된 자료가 없습니다.");
                    return;
                }
            }
        }
    }
}
