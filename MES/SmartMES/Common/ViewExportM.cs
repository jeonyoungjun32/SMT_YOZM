using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Diagram.Core.Native;
using DevExpress.Spreadsheet;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;


namespace SmartMES.Common
{
    public class ViewExportM
    {
        /// <summary>
        /// DevExpress컨트를Excel내보내기,여러컨트롤 한시트 혹은 다른 시트로 내보내기 가능
        /// eg:ExportToXlsx("test",true,"컨트롤",gridControl1,gridControl2);
        /// <param name="title">문서명</param>
        /// <param name="isPageForEachLink">여러 시트로 나눌지 선택</param>
        /// <param name="sheetName">시트명칭</param>
        /// <param name="printables">컨트롤들 eg:GridControl,PivotGridControl,TreeList,ChartControl...</param>
        public void ExportToExcel(string title, bool isPageForEachLink, params (DevExpress.XtraPrinting.IPrintable control, string sheetName)[] controlsWithSheetNames)
        {   //매개변수 수정 : string sheetName -> (DevExpress.XtraPrinting.IPrintable control, string sheetName)[] controlsWithSheetNames 데이터(control)과 sheetName을 함께 받음

            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                FileName = title,
                Title = "Excel내보내기",
                Filter = "Excel문서(*.xlsx)|*.xlsx|Excel문서(*.xls)|*.xls"
            };
            DialogResult dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult == DialogResult.Cancel)
                return;
            string fileName = saveFileDialog.FileName;

            DevExpress.XtraPrintingLinks.CompositeLink link = new DevExpress.XtraPrintingLinks.CompositeLink(new DevExpress.XtraPrinting.PrintingSystem()); //데이터 매핑을 위한 CompositeLink
            Dictionary<IBasePrintable, string> sheetNames = new Dictionary<IBasePrintable, string>();   //sheetName을 control에 매핑하기 위한 Dictionary

            //control과 sheetName을 매핑
            for (int i = 0; i < controlsWithSheetNames.Length; i++)
            {
                var (control, sheet) = controlsWithSheetNames[i];
                var plink = new PrintableComponentLink() { Component = control };
                link.Links.Add(plink);
                sheetNames[control] = sheet;
            }

            if (isPageForEachLink)             //15.1 의Xls는 현재기능 지원하지 않음，15.2 미지
                link.CreatePageForEachLink();  //여러 시트로 나눌지 선택
            //if (string.IsNullOrEmpty(sheetName)) sheetName = "Sheet";//시트 디폴트 명
            try
            {
                int count = 1;
                //중복명칭 뒤에 순번주기
                while (System.IO.File.Exists(fileName))
                {
                    if (fileName.Contains(")."))
                    {
                        int start = fileName.LastIndexOf("(");
                        int end = fileName.LastIndexOf(").") - fileName.LastIndexOf("(") + 2;
                        fileName = fileName.Replace(fileName.Substring(start, end), string.Format("({0}).", count));
                    }
                    else
                    {
                        fileName = fileName.Replace(".", string.Format("({0}).", count));
                    }
                    count++;
                }
                if (fileName.LastIndexOf(".xlsx") >= fileName.Length - 5)
                {
                    XlsxExportOptions options = new XlsxExportOptions();
                    if (isPageForEachLink)
                        options.ExportMode = XlsxExportMode.SingleFilePageByPage;
                    //DevExpress.XtraPrinting.XlsxExportOptions options = new DevExpress.XtraPrinting.XlsxExportOptions() { SheetName = sheetName };
                    //if (isPageForEachLink)
                    //    options.ExportMode = DevExpress.XtraPrinting.XlsxExportMode.SingleFilePageByPage;
                    link.ExportToXlsx(fileName, options);   //엑셀 파일 생성

                    // 시트 이름 변경 로직 추가
                    using (var workbook = new Workbook())
                    {
                        workbook.LoadDocument(fileName);
                        for (int i = 0; i < link.Links.Count && i < workbook.Worksheets.Count; i++)
                        {
                            var component = ((PrintableComponentLink)link.Links[i]).Component;
                            if (sheetNames.TryGetValue(component, out string currentSheetName))
                            {
                                workbook.Worksheets[i].Name = currentSheetName;
                            }
                        }
                        workbook.SaveDocument(fileName);
                    }
                }
                else
                {
                    XlsExportOptions options = new XlsExportOptions();
                    if (isPageForEachLink)
                        options.ExportMode = XlsExportMode.SingleFilePageByPage;
                    //DevExpress.XtraPrinting.XlsExportOptions options = new DevExpress.XtraPrinting.XlsExportOptions() { SheetName = sheetName };
                    //if (isPageForEachLink) //15.Xls는 현재 속성 없음，15.2미지
                    //    options.ExportMode = DevExpress.XtraPrinting.XlsExportMode.SingleFilePageByPage;
                    var firstComponent = ((PrintableComponentLink)link.Links[0]).Component;
                    if (sheetNames.TryGetValue(firstComponent, out string firstSheetName))
                    {
                        options.SheetName = firstSheetName;  // Set the first sheet name
                    }
                    link.ExportToXls(fileName, options);
                }
                if (DevExpress.XtraEditors.XtraMessageBox.Show("저장성공，실행하시겠습니까？", "알림", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    System.Diagnostics.Process.Start(fileName);//지정경로 문서 열기
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message);
            }
        }
    }
}
