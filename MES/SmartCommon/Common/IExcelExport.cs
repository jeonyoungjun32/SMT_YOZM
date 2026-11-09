using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartMES.Common
{
    public interface IExcelExport
    {
        void Export();
    }

    public abstract class ExcelExportBase : IExcelExport
    {
        protected readonly string _filter;
        protected readonly string _formName;

        protected ExcelExportBase(string filter = "Excel Worksheets|*.xls", string formName = null)
        {
            _filter = filter;
            _formName = formName;
        }

        public void Export()
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Title = "엑셀 내보내기",
                Filter = _filter,
                FileName = GetDefaultFileName()
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (IsFileInUse(sfd.FileName))
                    {
                        HandleFileInUse(sfd.FileName);
                        return;
                    }

                    bool success = ExportInternal(sfd.FileName);
                    if (success)  // 성공했을 때만 파일 열기
                    {
                        Process.Start(sfd.FileName);
                    }
                    //ExportInternal(sfd.FileName);
                    //Process.Start(sfd.FileName);
                }
                catch (IOException ex)
                {
                    MessageBox.Show($"파일 저장 중 입출력 오류가 발생했습니다:\n{ex.Message}",
                                  "저장 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (UnauthorizedAccessException ex)
                {
                    MessageBox.Show($"파일에 대한 접근 권한이 없습니다:\n{ex.Message}",
                                  "권한 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"예상치 못한 오류가 발생했습니다:\n{ex.Message}",
                                  "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // 하위 클래스에서 구현할 실제 Export 
        protected abstract bool ExportInternal(string fileName);

        // 기본 파일명 생성 (폼이름_날짜.xls)
        private string GetDefaultFileName()
        {
            string baseName = string.IsNullOrEmpty(_formName) ? "ExportedData" : _formName;
            // _formName 에 / 가 포함되어있을시 삭제 
            baseName = string.Concat(baseName.Split(Path.GetInvalidFileNameChars()));
            string dateStr = DateTime.Now.ToString("yyyyMMdd");
            return $"{baseName}_{dateStr}.xls";
        }

        // 파일 사용 중일 때 처리
        private void HandleFileInUse(string fileName)
        {
            DialogResult result = MessageBox.Show(
                "선택한 파일이 다른 프로그램에서 사용 중입니다.\n\n" +
                "예: 다른 이름으로 저장\n" +
                "아니오: 취소",
                "파일 사용 중",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                string newFileName = GetAvailableFileName(fileName);
                bool success = ExportInternal(newFileName);
                if (success)  // 성공했을 때만 파일 열기
                {
                    ExportInternal(newFileName);
                    Process.Start(newFileName);
                    MessageBox.Show($"파일이 저장되었습니다:\n{Path.GetFileName(newFileName)}",
                                  "저장 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        protected void ExportGridView(object gridView, string fileName)
        {
            var exportMethod = gridView.GetType().GetMethod("ExportToXls", new Type[] { typeof(string) });
            if (exportMethod == null)
                throw new InvalidOperationException($"{gridView.GetType().Name}에서 ExportToXls 메서드를 찾을 수 없습니다.");

            exportMethod.Invoke(gridView, new object[] { fileName });
        }

        protected bool IsFileInUse(string filePath)
        {
            if (!File.Exists(filePath))
                return false;

            try
            {
                using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    return false;
                }
            }
            catch (IOException)
            {
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected string GetAvailableFileName(string fileName)
        {
            string directory = Path.GetDirectoryName(fileName);
            string nameWithoutExt = Path.GetFileNameWithoutExtension(fileName);
            string extension = Path.GetExtension(fileName);
            int counter = 1;

            while (true)
            {
                string newFileName = Path.Combine(directory, $"{nameWithoutExt}({counter}){extension}");
                if (!File.Exists(newFileName) || !IsFileInUse(newFileName))
                    return newFileName;
                counter++;
            }
        }
    }


    // 1. 단일 GridView Export
    public class SingleGridViewExcelExport : ExcelExportBase
    {
        private readonly object _gridView;

        public SingleGridViewExcelExport(object gridView, string formName = null, string filter = "Excel Worksheets|*.xls")
            : base(filter, formName)
        {
            _gridView = gridView ?? throw new ArgumentNullException(nameof(gridView));
        }

        protected override bool ExportInternal(string fileName)
        {
            ExportGridView(_gridView, fileName);
            return true;
        }
    }

    // 2. 탭별 다중 GridView Export
    public class TabbedGridViewExcelExport : ExcelExportBase
    {
        private readonly object _tabbedControl;
        private readonly object[] _gridViews;

        public TabbedGridViewExcelExport(object tabbedControl, object[] gridViews, string formName = null, string filter = "Excel Worksheets|*.xls")
            : base(filter, formName)
        {
            _tabbedControl = tabbedControl ?? throw new ArgumentNullException(nameof(tabbedControl));
            _gridViews = gridViews ?? throw new ArgumentNullException(nameof(gridViews));
        }

        protected override bool ExportInternal(string fileName)
        {
            int selectedIndex = GetSelectedTabIndex();

            if (selectedIndex < 0 || selectedIndex >= _gridViews.Length)
                throw new IndexOutOfRangeException($"선택된 탭 인덱스({selectedIndex})가 유효하지 않습니다.");

            ExportGridView(_gridViews[selectedIndex], fileName);
            return true;
        }

        private int GetSelectedTabIndex()
        {
            var selectedIndexProperty = _tabbedControl.GetType().GetProperty("SelectedTabPageIndex");
            if (selectedIndexProperty == null)
                throw new InvalidOperationException($"{_tabbedControl.GetType().Name}에서 SelectedTabPageIndex 속성을 찾을 수 없습니다.");

            return (int)selectedIndexProperty.GetValue(_tabbedControl);
        }
    }


    // 3. 다중 GridView Export (선택 다이얼로그 포함) 
    public class MultipleGridViewExcelExport : ExcelExportBase
    {
        private readonly Dictionary<string, object> _gridViews;

        public MultipleGridViewExcelExport(Dictionary<string, object> gridViews, string formName = null, string filter = "Excel Worksheets|*.xls")
            : base(filter, formName)
        {
            _gridViews = gridViews ?? throw new ArgumentNullException(nameof(gridViews));
            if (!_gridViews.Any())
                throw new ArgumentException("GridView가 최소 하나는 필요합니다.", nameof(gridViews));
        }

        protected override bool ExportInternal(string fileName)
        {
            if (_gridViews.Count == 1)
            {
                // GridView가 하나뿐이면 바로 Export
                ExportGridView(_gridViews.First().Value, fileName);
                return true;
            }
            else
            {
                // 여러 개면 선택 다이얼로그 표시
                string selectedKey = ShowGridViewSelectionDialog();
                if (!string.IsNullOrEmpty(selectedKey))
                {
                    ExportGridView(_gridViews[selectedKey], fileName);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private string ShowGridViewSelectionDialog()
        {
            using (Form dialog = new Form())
            {
                dialog.Text = "내보낼 데이터 선택";
                dialog.Size = new System.Drawing.Size(350, 250);
                dialog.StartPosition = FormStartPosition.CenterParent;
                dialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                dialog.MaximizeBox = false;
                dialog.MinimizeBox = false;

                // 리스트박스
                ListBox listBox = new ListBox
                {
                    Location = new System.Drawing.Point(10, 10),
                    Size = new System.Drawing.Size(315, 150)
                };

                foreach (var kvp in _gridViews)
                {
                    listBox.Items.Add(kvp.Key);
                }
                listBox.SelectedIndex = 0;

                // 버튼들
                Button okButton = new Button
                {
                    Text = "확인",
                    DialogResult = DialogResult.OK,
                    Location = new System.Drawing.Point(170, 180),
                    Size = new System.Drawing.Size(75, 25)
                };

                Button cancelButton = new Button
                {
                    Text = "취소",
                    DialogResult = DialogResult.Cancel,
                    Location = new System.Drawing.Point(250, 180),
                    Size = new System.Drawing.Size(75, 25)
                };

                dialog.Controls.Add(listBox);
                dialog.Controls.Add(okButton);
                dialog.Controls.Add(cancelButton);

                if (dialog.ShowDialog() == DialogResult.OK && listBox.SelectedItem != null)
                {
                    return listBox.SelectedItem.ToString();
                }

                return null;
            }
        }
    }



    public static class ExcelExportHelper
    {
        // <summary>
        /// 단일 GridView를 Excel로 내보내기
        /// </summary>
        /// <param name="gridView">내보낼 GridView</param>
        /// <param name="formName">폼 이름 (파일명에 사용)</param>
        /// <param name="filter">파일 필터</param>
        public static IExcelExport CreateSingleExport(object gridView, string formName = null, string filter = "Excel Worksheets|*.xls")
        {
            return new SingleGridViewExcelExport(gridView, formName, filter);
        }

        /// <summary>
        /// 탭별 다중 GridView를 Excel로 내보내기
        /// </summary>
        /// <param name="tabbedControl">탭 컨트롤</param>
        /// <param name="gridViews">GridView 배열 (탭 순서대로)</param>
        /// <param name="formName">폼 이름 (파일명에 사용)</param>
        /// <param name="filter">파일 필터</param>
        public static IExcelExport CreateTabbedExport(object tabbedControl, object[] gridViews, string formName = null, string filter = "Excel Worksheets|*.xls")
        {
            return new TabbedGridViewExcelExport(tabbedControl, gridViews, formName, filter);
        }

        /// <summary>
        /// 여러 GridView 중 선택하여 Excel로 내보내기
        /// </summary>
        /// <param name="gridViews">GridView 딕셔너리 (표시명, GridView)</param>
        /// <param name="formName">폼 이름 (파일명에 사용)</param>
        /// <param name="filter">파일 필터</param>
        public static IExcelExport CreateMultipleExport(Dictionary<string, object> gridViews, string formName = null, string filter = "Excel Worksheets|*.xls")
        {
            return new MultipleGridViewExcelExport(gridViews, formName, filter);
        }

        /// <summary>
        /// 여러 GridView 중 선택하여 Excel로 내보내기 (간편 버전)
        /// </summary>
        /// <param name="gridViewsWithNames">GridView와 표시명 쌍들</param>
        /// <param name="formName">폼 이름 (파일명에 사용)</param>
        /// <param name="filter">파일 필터</param>
        public static IExcelExport CreateMultipleExport(string formName, string filter = "Excel Worksheets|*.xls", params (string name, object gridView)[] gridViewsWithNames)
        {
            var dict = gridViewsWithNames.ToDictionary(x => x.name, x => x.gridView);
            return new MultipleGridViewExcelExport(dict, formName, filter);
        }
    }
}
