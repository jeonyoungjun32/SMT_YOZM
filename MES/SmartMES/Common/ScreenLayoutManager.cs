using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using DevExpress.XtraTreeList;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace SmartMES.Common
{
    /// <summary>화면의 배치만 캡처합니다. 조회 결과·입력값·편집 권한은 변경하지 않습니다.</summary>
    internal static class ScreenLayoutSnapshot
    {
        internal static string Capture(Form form)
        {
            return new XElement("ScreenLayout", new XAttribute("version", 1),
                Targets(form).Select(target => CaptureTarget(target.Key, target.Value))).ToString(SaveOptions.DisableFormatting);
        }

        private static XElement CaptureTarget(string key, object target)
        {
            var element = new XElement("Target", new XAttribute("key", key));
            var grid = target as GridView;
            var tree = target as TreeList;
            var layout = target as LayoutControl;
            var split = target as SplitContainerControl;
            if (grid != null)
            {
                element.SetAttributeValue("kind", "grid");
                foreach (DevExpress.XtraGrid.Columns.GridColumn column in grid.Columns)
                    element.Add(Column(column.FieldName, column.Name, column.Visible, column.VisibleIndex, column.Width, column.Fixed.ToString()));
            }
            else if (tree != null)
            {
                element.SetAttributeValue("kind", "tree");
                foreach (DevExpress.XtraTreeList.Columns.TreeListColumn column in tree.Columns)
                    element.Add(Column(column.FieldName, column.Name, column.Visible, column.VisibleIndex, column.Width, column.Fixed.ToString()));
            }
            else if (layout != null)
            {
                element.SetAttributeValue("kind", "layout");
                using (var stream = new MemoryStream())
                {
                    layout.SaveLayoutToStream(stream);
                    element.Value = Convert.ToBase64String(stream.ToArray());
                }
            }
            else if (split != null)
            {
                element.SetAttributeValue("kind", "split");
                element.SetAttributeValue("position", split.SplitterPosition);
            }
            return element;
        }

        private static XElement Column(string field, string name, bool visible, int index, int width, string fixedStyle)
        {
            return new XElement("Column", new XAttribute("field", field ?? ""), new XAttribute("name", name ?? ""),
                new XAttribute("visible", visible), new XAttribute("index", index),
                new XAttribute("width", width), new XAttribute("fixed", fixedStyle));
        }

        internal static void Apply(Form form, string xml)
        {
            if (string.IsNullOrEmpty(xml)) return;
            XDocument document;
            using (var reader = XmlReader.Create(new StringReader(xml), new XmlReaderSettings
            {
                DtdProcessing = DtdProcessing.Prohibit, XmlResolver = null, MaxCharactersInDocument = 8000000
            })) document = XDocument.Load(reader);
            if (document.Root == null || document.Root.Name != "ScreenLayout" || (int?)document.Root.Attribute("version") != 1)
                throw new InvalidOperationException("지원하지 않는 화면 배치 형식입니다.");
            var targets = Targets(form);
            foreach (var saved in document.Root.Elements("Target"))
            {
                object target;
                if (!targets.TryGetValue((string)saved.Attribute("key") ?? "", out target)) continue;
                var layout = target as LayoutControl;
                var split = target as SplitContainerControl;
                var grid = target as GridView;
                var tree = target as TreeList;
                string kind = (string)saved.Attribute("kind");
                if (layout != null && kind == "layout")
                {
                    using (var stream = new MemoryStream(Convert.FromBase64String(saved.Value)))
                        layout.RestoreLayoutFromStream(stream);
                }
                else if (split != null && kind == "split")
                    split.SplitterPosition = Math.Max(0, (int?)saved.Attribute("position") ?? 0);
                else if (grid != null && kind == "grid")
                {
                    grid.BeginUpdate();
                    try
                    {
                        foreach (var savedColumn in saved.Elements("Column").OrderBy(c => (int?)c.Attribute("index") ?? -1))
                        {
                            var column = grid.Columns.Cast<DevExpress.XtraGrid.Columns.GridColumn>().FirstOrDefault(c => Matches(savedColumn, c.FieldName, c.Name));
                            if (column == null) continue; // 소스에 추가된 컬럼은 유지하고 삭제된 컬럼은 건너뜁니다.
                            column.Visible = (bool?)savedColumn.Attribute("visible") ?? true;
                            if (column.Visible) column.VisibleIndex = Math.Max(0, (int?)savedColumn.Attribute("index") ?? 0);
                            column.Width = Math.Max(20, (int?)savedColumn.Attribute("width") ?? column.Width);
                            DevExpress.XtraGrid.Columns.FixedStyle fixedStyle;
                            if (Enum.TryParse((string)savedColumn.Attribute("fixed"), out fixedStyle)) column.Fixed = fixedStyle;
                        }
                    }
                    finally { grid.EndUpdate(); }
                }
                else if (tree != null && kind == "tree")
                {
                    tree.BeginUpdate();
                    try
                    {
                        foreach (var savedColumn in saved.Elements("Column").OrderBy(c => (int?)c.Attribute("index") ?? -1))
                        {
                            var column = tree.Columns.Cast<DevExpress.XtraTreeList.Columns.TreeListColumn>().FirstOrDefault(c => Matches(savedColumn, c.FieldName, c.Name));
                            if (column == null) continue;
                            column.Visible = (bool?)savedColumn.Attribute("visible") ?? true;
                            if (column.Visible) column.VisibleIndex = Math.Max(0, (int?)savedColumn.Attribute("index") ?? 0);
                            column.Width = Math.Max(20, (int?)savedColumn.Attribute("width") ?? column.Width);
                            DevExpress.XtraTreeList.Columns.FixedStyle fixedStyle;
                            if (Enum.TryParse((string)savedColumn.Attribute("fixed"), out fixedStyle)) column.Fixed = fixedStyle;
                        }
                    }
                    finally { tree.EndUpdate(); }
                }
            }
        }

        private static bool Matches(XElement saved, string field, string name)
        {
            string savedField = (string)saved.Attribute("field");
            return !string.IsNullOrEmpty(savedField) ? savedField == field : (string)saved.Attribute("name") == name;
        }

        private static Dictionary<string, object> Targets(Control root, string path = "")
        {
            var result = new Dictionary<string, object>();
            for (int i = 0; i < root.Controls.Count; i++)
            {
                var child = root.Controls[i];
                string key = path + "/" + (string.IsNullOrEmpty(child.Name) ? child.GetType().Name + i : child.Name);
                var control = child as GridControl;
                if (control != null)
                {
                    int viewIndex = 0;
                    foreach (var view in control.ViewCollection.OfType<GridView>())
                        result.Add(key + "/" + (string.IsNullOrEmpty(view.Name) ? "View" + viewIndex++ : view.Name), view);
                    continue;
                }
                if (child is TreeList) { result.Add(key, child); continue; }
                if (child is LayoutControl || child is SplitContainerControl) result.Add(key, child);
                foreach (var entry in Targets(child, key)) result.Add(entry.Key, entry.Value);
            }
            return result;
        }

        internal static string Key(Form form)
        {
            // 같은 클래스라도 메뉴 인자가 다른 일상/정기 화면은 별도 보관합니다.
            string identity = form.GetType().FullName + "\n" + form.Name + "\n" + Convert.ToString(form.Tag);
            using (var sha = SHA256.Create())
                return BitConverter.ToString(sha.ComputeHash(Encoding.UTF8.GetBytes(identity))).Replace("-", "");
        }
    }

    internal sealed class ScreenLayoutManager
    {
        private readonly ScreenLayoutRepository repository;
        private readonly Dictionary<Form, string> defaults = new Dictionary<Form, string>();
        internal ScreenLayoutManager(ScreenLayoutRepository repository) { this.repository = repository; }

        internal void Register(Form form)
        {
            if (defaults.ContainsKey(form)) return;
            // Load에서 소스가 구성한 배치를 먼저 보관한 후 DB의 사용자 배치를 복원합니다.
            defaults.Add(form, ScreenLayoutSnapshot.Capture(form));
            form.FormClosed += (sender, args) => defaults.Remove(form);
            // 조회 후 BestFitColumns가 실행되어도 사용자가 보고 있던 배치를 유지합니다.
            foreach (var grid in Grids(form))
            {
                grid.DataSourceChanged += (sender, args) =>
                {
                    if (form.IsDisposed || !form.IsHandleCreated) return;
                    string current = ScreenLayoutSnapshot.Capture(form);
                    form.BeginInvoke((Action)(() =>
                    {
                        if (!form.IsDisposed && defaults.ContainsKey(form))
                            ScreenLayoutSnapshot.Apply(form, current);
                    }));
                };
            }
            try
            {
                string xml = repository.Load(ScreenLayoutSnapshot.Key(form));
                if (xml != null) ScreenLayoutSnapshot.Apply(form, xml);
            }
            catch
            {
                ScreenLayoutSnapshot.Apply(form, defaults[form]);
                throw;
            }
        }

        private static IEnumerable<GridControl> Grids(Control parent)
        {
            foreach (Control child in parent.Controls)
            {
                var grid = child as GridControl;
                if (grid != null) yield return grid;
                else foreach (var nested in Grids(child)) yield return nested;
            }
        }

        internal void Save(Form form)
        {
            RequireRegistered(form);
            repository.Save(ScreenLayoutSnapshot.Key(form), form.GetType().FullName,
                Convert.ToString(form.Tag), ScreenLayoutSnapshot.Capture(form));
        }

        internal void Reset(Form form)
        {
            RequireRegistered(form);
            repository.Delete(ScreenLayoutSnapshot.Key(form));
            ScreenLayoutSnapshot.Apply(form, defaults[form]);
        }

        private void RequireRegistered(Form form)
        {
            if (form == null || form.IsDisposed || !defaults.ContainsKey(form))
                throw new InvalidOperationException("저장하거나 초기화할 업무 화면을 먼저 여세요.");
        }
    }
}
