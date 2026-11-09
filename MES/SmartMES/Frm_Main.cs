using DevExpress.Skins;
using DevExpress.Skins.XtraForm;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Microsoft.Win32;
using SmartMES.View.Lookup;
using System;
using System.Data;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using DevExpress.XtraBars.Ribbon.Gallery;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraGrid;
using DevExpress.XtraTab;
using DevExpress.XtraGrid.Views.Grid;
using SmartCommon.Common;
using SmartCommon.View.Mail;
using SmartMES.View.Chatbot;
using SmartMES.View;
using SmartMES.View.SM;
using SmartMES.View.Monitoring;
using SmartMES.View.Management;

namespace SmartMES
{
    public partial class Frm_Main : DevExpress.XtraEditors.XtraForm
    {
        string displayNoticeYn;
        Models.clsSystem sys = new Models.clsSystem();
        Database db = new Database();
        string query = string.Empty;

        string comparsion = string.Empty;
        string workRate = string.Empty;
        string preWorkRate = string.Empty;

        string typeValue = string.Empty;
        string[] typeList = null;

        SecondaryAxisY stockAxisY = null;
        AccordionControlElement myMenuElement = null;
        Panel menuNavScrollPanel = null;
        int menuNavPaneContentWidth = 0;
        int menuNavPaneScrollOffset = 0;
        bool menuNavPaneDragging = false;
        bool menuNavPaneDragMoved = false;
        int menuNavPaneDragStartX = 0;
        int menuNavPaneDragStartOffset = 0;

        public Frm_Main()
        {
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Location = System.Windows.Forms.Screen.GetBounds(MousePosition).Location;

            InitializeComponent();
            InitializeScreenLayoutIcons();

            // 창 상단과 작업표시줄에 프로젝트 실행 파일의 YOZM Y 아이콘을 표시합니다.
            Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            //UserLookAndFeel.Default.SkinName = ReadRegKey("Skin");

            mainAccordion.Footer.Visible = false;

            string ver = string.Empty;

            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
            {
                ver = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString() + ", ";
            }

            this.Text += " (" + ver + WrGlobal.LoginUserNM +", " + WrGlobal.CorpName + ")";

            setUserAccordionMenu();         //메뉴
            setLoginID();                   //로그인 정보
            setMenuNavPaneHorizontalScroll();   // 대메뉴 가로스크롤
            // 탭 전환 시 왼쪽 메뉴에서도 현재 화면의 선택 상태를 표시합니다.
            mainAccordion.AllowItemSelection = true;
            xtraTabbedMdiManager1.SelectedPageChanged += TrackActiveMenu;

            //상단 팝업메뉴 - 개발 메뉴 표준 제외 VISIBLE FALSE 처리(이사님 요청사항)
            if(Database.DbName.ToUpper() != "YOZM")
			{
                barBtnMail.Visibility = BarItemVisibility.Never;
                //barBtnSkin.Visibility = BarItemVisibility.Never;
                barBtnFont.Visibility = BarItemVisibility.Never;
                //barBtnBot.Visibility = BarItemVisibility.Never;
			}
        }

        private void setLoginID()
        {
            nav_Login.Caption = WrGlobal.LoginUserNM;
        }

        private void setMenuNavPaneHorizontalScroll()
        {
            int controlIndex = this.Controls.GetChildIndex(menuNavPane);
            int buttonWidth = 0;

            foreach (NavButton button in menuNavPane.Buttons)
            {
                string caption = button.Caption == null ? string.Empty : button.Caption;
                Size textSize = TextRenderer.MeasureText(caption, menuNavPane.Font);
                int imageWidth = button.ImageOptions.HasSvgImage
                    ? (button.ImageOptions.SvgImageSize.Width > 0
                        ? button.ImageOptions.SvgImageSize.Width
                        : 24)
                    : 0;

                buttonWidth += textSize.Width
                    + imageWidth
                    + menuNavPane.ButtonPadding.Horizontal
                    + 8;
            }

            menuNavPaneContentWidth = buttonWidth;

            menuNavScrollPanel = new Panel();
            menuNavScrollPanel.AutoScroll = false;
            menuNavScrollPanel.BackColor = menuNavPane.Appearance.BackColor;
            menuNavScrollPanel.Dock = DockStyle.Top;
            menuNavScrollPanel.Resize += new EventHandler(menuNavScrollPanel_Resize);
            menuNavScrollPanel.MouseWheel += new MouseEventHandler(menuNavPane_MouseWheel);
            menuNavScrollPanel.MouseDown += new MouseEventHandler(menuNavPane_MouseDown);
            menuNavScrollPanel.MouseMove += new MouseEventHandler(menuNavPane_MouseMove);
            menuNavScrollPanel.MouseUp += new MouseEventHandler(menuNavPane_MouseUp);

            menuNavPane.Dock = DockStyle.None;
            menuNavPane.Location = Point.Empty;
            menuNavPane.MouseWheel += new MouseEventHandler(menuNavPane_MouseWheel);
            menuNavPane.MouseDown += new MouseEventHandler(menuNavPane_MouseDown);
            menuNavPane.MouseMove += new MouseEventHandler(menuNavPane_MouseMove);
            menuNavPane.MouseUp += new MouseEventHandler(menuNavPane_MouseUp);

            menuNavScrollPanel.Controls.Add(menuNavPane);
            this.Controls.Add(menuNavScrollPanel);
            this.Controls.SetChildIndex(menuNavScrollPanel, controlIndex);

            setMenuNavPaneScrollSize();
        }

        private void menuNavScrollPanel_Resize(object sender, EventArgs e)
        {
            setMenuNavPaneScrollSize();
        }

        private void menuNavPane_MouseWheel(object sender, MouseEventArgs e)
        {
            int maximumOffset = Math.Max(
                0,
                menuNavPaneContentWidth - menuNavScrollPanel.ClientSize.Width);

            if (e.Delta > 0)
            {
                menuNavPaneScrollOffset = Math.Max(0, menuNavPaneScrollOffset - 80);
            }
            else if (e.Delta < 0)
            {
                menuNavPaneScrollOffset = Math.Min(maximumOffset, menuNavPaneScrollOffset + 80);
            }

            menuNavPane.Location = new Point(-menuNavPaneScrollOffset, 0);
        }

        private void menuNavPane_MouseDown(object sender, MouseEventArgs e)
        {
            int maximumOffset = Math.Max(
                0,
                menuNavPaneContentWidth - menuNavScrollPanel.ClientSize.Width);

            if (e.Button != MouseButtons.Left || maximumOffset == 0)
            {
                return;
            }

            menuNavPaneDragging = true;
            menuNavPaneDragMoved = false;
            menuNavPaneDragStartX = Control.MousePosition.X;
            menuNavPaneDragStartOffset = menuNavPaneScrollOffset;
            menuNavPane.Cursor = Cursors.SizeWE;

            Control control = sender as Control;

            if (control != null)
            {
                control.Capture = true;
            }
        }

        private void menuNavPane_MouseMove(object sender, MouseEventArgs e)
        {
            if (!menuNavPaneDragging)
            {
                return;
            }

            int moveDistance = Control.MousePosition.X - menuNavPaneDragStartX;
            int maximumOffset = Math.Max(
                0,
                menuNavPaneContentWidth - menuNavScrollPanel.ClientSize.Width);

            if (Math.Abs(moveDistance) > 4)
            {
                menuNavPaneDragMoved = true;
            }

            menuNavPaneScrollOffset = Math.Max(
                0,
                Math.Min(maximumOffset, menuNavPaneDragStartOffset - moveDistance));
            menuNavPane.Location = new Point(-menuNavPaneScrollOffset, 0);
        }

        private void menuNavPane_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            menuNavPaneDragging = false;
            menuNavPane.Cursor = Cursors.Default;

            Control control = sender as Control;

            if (control != null)
            {
                control.Capture = false;
            }
        }

        private void setMenuNavPaneScrollSize()
        {
            if (menuNavScrollPanel == null)
            {
                return;
            }

            int maximumOffset = Math.Max(
                0,
                menuNavPaneContentWidth - menuNavScrollPanel.ClientSize.Width);

            menuNavPaneScrollOffset = Math.Min(menuNavPaneScrollOffset, maximumOffset);
            menuNavScrollPanel.Height = menuNavPane.Height;
            menuNavPane.Width = Math.Max(
                menuNavPaneContentWidth,
                menuNavScrollPanel.ClientSize.Width);
            menuNavPane.Location = new Point(-menuNavPaneScrollOffset, 0);
        }

        private void setUserAccordionMenu()
        {
            //DevExpress.XtraBars.Navigation.AccordionControlElement parentGroup = mainAccordion.Elements.add

            AccordionControlElement rootGroup = null;
            AccordionControlElement parentGroup1 = null;
            AccordionControlElement parentGroup2 = null;
            AccordionControlElement parentGroup3 = null;
            AccordionControlElement addGroup = null;
            AccordionControlElement eleItem = null;

            NavButton navButton = null;

            DataView dv = sys.GetEmployeeMenuList();
            DataView dvQuick = sys.GetEmployeeQuickMenuList("Y");
            dvQuick.Sort = "SortNum ASC";

            foreach (DataRowView drv in dv)
            {
                if (drv["Title_" + WrGlobal.Language].ToString() == "MyMenu") //즐겨찾기 메뉴명
                {
                    addGroup = new DevExpress.XtraBars.Navigation.AccordionControlElement(DevExpress.XtraBars.Navigation.ElementStyle.Group);
                    addGroup.Name = drv["ID"].ToString();
                    addGroup.Text = drv["Title_" + WrGlobal.Language].ToString();

                    if (drv["ImageKey"].ToString() != "")
                    {
                        addGroup.ImageOptions.SvgImage = svgImages[drv["ImageKey"].ToString()];
                        addGroup.ImageOptions.SvgImageSize = new Size(24, 24);
                        addGroup.Appearance.Normal.ForeColor = Color.LightCyan;
                    }
                    addGroup.Expanded = drv["ExpandYn"].ToString() == "0" ? false : true;

                    if (drv["GroupLevel"].ToString() == "1")
                    {
                        rootGroup.Elements.Add(addGroup);
                        parentGroup1 = null;
                        parentGroup1 = addGroup;
                    }
                    else if (drv["GroupLevel"].ToString() == "2")
                    {
                        parentGroup1.Elements.Add(addGroup);
                        parentGroup2 = null;
                        parentGroup2 = addGroup;
                    }
                    else if (drv["GroupLevel"].ToString() == "3")
                    {
                        parentGroup2.Elements.Add(addGroup);
                        parentGroup3 = null;
                        parentGroup3 = addGroup;
                    }

                    myMenuElement = parentGroup2;
                    addGroup = null;

                    foreach (DataRowView quickDrv in dvQuick)
                    {
                        eleItem = new DevExpress.XtraBars.Navigation.AccordionControlElement(DevExpress.XtraBars.Navigation.ElementStyle.Item);
                        eleItem.Name = "q" + quickDrv["ID"].ToString();
                        eleItem.Text = quickDrv["NewTitle_" + WrGlobal.Language].ToString() == "" ? quickDrv["Title_" + WrGlobal.Language].ToString() : quickDrv["NewTitle_" + WrGlobal.Language].ToString();
                        eleItem.Tag = quickDrv["ProjectName"].ToString() + "|" + quickDrv["FolderName"].ToString() + "|" + quickDrv["FormName"].ToString() + "|" + quickDrv["ParamText"].ToString();
                        if (quickDrv["ImageKey"].ToString() != "")
                        {
                            eleItem.ImageOptions.SvgImage = svgImages[drv["ImageKey"].ToString()];
                            eleItem.ImageOptions.SvgImageSize = new Size(24, 24);
                        }

                        parentGroup2.Elements.Add(eleItem);
                        eleItem = null;
                    }
                    continue;
                }

                if (drv["GroupLevel"].ToString() == "1")
                {
                    rootGroup = mainAccordion.Elements.Add();
                    rootGroup.Name = drv["ID"].ToString();
                    rootGroup.Text = drv["Title_" + WrGlobal.Language].ToString();
                    rootGroup.ImageOptions.SvgImage = svgImages[drv["ImageKey"].ToString()];
                    rootGroup.ImageOptions.SvgImageSize = new Size(24, 24);
                    rootGroup.Appearance.Normal.ForeColor = Color.LightCyan;

                    parentGroup1 = null;
                    parentGroup1 = addGroup;

                    navButton = new NavButton();
                    navButton.Name = "menu_" + drv["ID"].ToString();
                    navButton.Caption = drv["Title_" + WrGlobal.Language].ToString();
                    navButton.ImageOptions.SvgImage = svgImages[drv["ImageKey"].ToString()];
                    navButton.ImageOptions.SvgImageSize = new Size(24, 24);
                    navButton.ElementClick += new NavElementClickEventHandler(MenuNavClick);

                    navButton.AppearanceHovered.BackColor = Color.FromArgb(56, 138, 134);
                    navButton.AppearanceSelected.BackColor = Color.FromArgb(56, 138, 134);

                    menuNavPane.Buttons.Insert(menuNavPane.Buttons.Count-1, navButton);
                }

                if (drv["GroupYn"].ToString() == "1")
                {   // 그룹
                    addGroup = new DevExpress.XtraBars.Navigation.AccordionControlElement(DevExpress.XtraBars.Navigation.ElementStyle.Group);
                    addGroup.Name = drv["ID"].ToString();
                    addGroup.Text = drv["Title_" + WrGlobal.Language].ToString();

                    if (drv["ImageKey"].ToString() != "")
                    {
                        addGroup.ImageOptions.SvgImage = svgImages[drv["ImageKey"].ToString()];
                        addGroup.ImageOptions.SvgImageSize = new Size(24, 24);
                        addGroup.Appearance.Normal.ForeColor = Color.LightCyan;
                    }
                    addGroup.Expanded = drv["ExpandYn"].ToString() == "0" ? false : true;

                    if (drv["GroupLevel"].ToString() == "1")
                    {
                        rootGroup.Elements.Add(addGroup);
                        parentGroup1 = null;
                        parentGroup1 = addGroup;
                    }
                    else if (drv["GroupLevel"].ToString() == "2")
                    {
                        parentGroup1.Elements.Add(addGroup);
                        parentGroup2 = null;
                        parentGroup2 = addGroup;
                    }
                    else if (drv["GroupLevel"].ToString() == "3")
                    {
                        parentGroup2.Elements.Add(addGroup);
                        parentGroup3 = null;
                        parentGroup3 = addGroup;
                    }

                    addGroup = null;
                }
                else
                {   // 아이템
                    eleItem = new DevExpress.XtraBars.Navigation.AccordionControlElement(DevExpress.XtraBars.Navigation.ElementStyle.Item);
                    eleItem.Name = drv["ID"].ToString();
                    eleItem.Text = drv["Title_" + WrGlobal.Language].ToString();
                    eleItem.Tag = drv["ProjectName"].ToString() + "|" + drv["FolderName"].ToString() + "|" + drv["FormName"].ToString() + "|" + drv["ParamText"].ToString();
                    if (drv["ImageKey"].ToString() != "")
                    {
                        eleItem.ImageOptions.SvgImage = svgImages[drv["ImageKey"].ToString()];
                        eleItem.ImageOptions.SvgImageSize = new Size(24, 24);
                    }

                    if (drv["GroupLevel"].ToString() == "1")
                    {
                        rootGroup.Elements.Add(eleItem);
                    }
                    else if (drv["GroupLevel"].ToString() == "2")
                    {
                        parentGroup1.Elements.Add(eleItem);
                        eleItem.Text = "• " + eleItem.Text;
                    }
                    else if (drv["GroupLevel"].ToString() == "3")
                    {
                        parentGroup2.Elements.Add(eleItem);
                        eleItem.Text = "• " + eleItem.Text;
                    }
                    else if (drv["GroupLevel"].ToString() == "4")
                    {
                        parentGroup3.Elements.Add(eleItem);
                        eleItem.Text = "- " + eleItem.Text;
                    }

                    eleItem = null;
                }

            }
        }
        public void SetQuickMenuRefresh()
        {
            if (myMenuElement == null) return;

            myMenuElement.Elements.Clear();

            DataView dvQuick = sys.GetEmployeeQuickMenuList("Y");
            dvQuick.Sort = "SortNum ASC";
            AccordionControlElement eleItem = null;

            foreach (DataRowView quickDrv in dvQuick)
            {
                eleItem = new DevExpress.XtraBars.Navigation.AccordionControlElement(DevExpress.XtraBars.Navigation.ElementStyle.Item);
                eleItem.Name = "q" + quickDrv["ID"].ToString();
                eleItem.Text = quickDrv["NewTitle_" + WrGlobal.Language].ToString() == "" ? quickDrv["Title_" + WrGlobal.Language].ToString() : quickDrv["NewTitle_" + WrGlobal.Language].ToString();
                eleItem.Tag = quickDrv["ProjectName"].ToString() + "|" + quickDrv["FolderName"].ToString() + "|" + quickDrv["FormName"].ToString() + "|" + quickDrv["ParamText"].ToString();
                if (quickDrv["ImageKey"].ToString() != "")
                {
                    eleItem.ImageOptions.SvgImage = svgImages[quickDrv["ImageKey"].ToString()];
                    eleItem.ImageOptions.SvgImageSize = new Size(24, 24);
                }

                myMenuElement.Elements.Add(eleItem);
                eleItem = null;
            }
        }

        public void ReadRegValue()
        {
            RegistryKey reg = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\SNEXISMES", true);

            if (reg == null) return;

            displayNoticeYn = reg.GetValue("DisplayYn") as string;

            string checkDate = reg.GetValue("CheckDate") as string;
            DateTime chkDate = checkDate == null ? DateTime.MinValue : DateTime.Parse(checkDate);

            string query = $@"SELECT WriteDate 
                                 FROM Notice 
                                 WHERE CAST(WriteDate AS DATE) >= CAST(GETDATE() AS DATE)
                                 ORDER BY Seq DESC
                            ";
            DataTable dt = db.GetDataTable(query);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DateTime writeDate = Convert.ToDateTime(row["WriteDate"].ToString());

                    // 마지막 확인 날짜보다 공지사항 등록일이 더 최근인 경우 새로운 공지를 표시하도록 설정
                    if (chkDate < writeDate)
                    {
                        reg.SetValue("DisplayYn", "N"); // 새로운 공지가 있으면 "N"으로 설정하여 공지 표시
                        break;
                    }
                }
            }
            reg.Close();
        }

        #region 공지사항

        string lastDate = "";
        private void OpenNotice()
        {
            //현재시간
            DateTime dDatetime = DateTime.Now;
            string checkKey = "CheckDate_" + WrGlobal.DBName;

            RegistryKey reg = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\SNEXISMES", true);
            if (reg == null) return;
           string CheckDate = reg.GetValue(checkKey) == null ? "" : reg.GetValue(checkKey).ToString();
            if (CheckDate == dDatetime.ToString("yyyyMMdd")) return;

            query = $@"
                        select count(*) from SM_Notice
                        where CorpId = {WrGlobal.CorpID} and '{dDatetime.ToString("yyyy-MM-dd")}' between convert(date,StartDate) and convert(date,EndDate)
                    ";
            int count = db.ExecuteScalarQuery(query);
            if (count <= 0) return;

            Frm_LookUpNotice frmNotice = new Frm_LookUpNotice(dDatetime);
            if (frmNotice.ShowDialog() == DialogResult.OK)
            {
                reg.SetValue(checkKey, dDatetime.ToString("yyyyMMdd"));
            }

            lastDate = dDatetime.ToString("yyyyMMdd");
            if (!tmr_Notice.Enabled) tmr_Notice.Enabled = true;
        }

        #endregion

        private void MenuNavClick(object sender, DevExpress.XtraBars.Navigation.NavElementEventArgs e)
        {
            try
            {
                if (menuNavPaneDragMoved)
                {
                    menuNavPaneDragMoved = false;
                    return;
                }

                NavButton ctl = (NavButton)sender;
                string targetName = ctl.Name.Replace("menu_", "");

                SelectMenuModule(mainAccordion.Elements[targetName]);
            }
            catch
            { }
        }

        /// <summary>왼쪽 메뉴를 지정한 대메뉴로 전환하고 상단 대메뉴의 색을 맞춥니다.</summary>
        private void SelectMenuModule(AccordionControlElement root)
        {
            if (root == null)
                return;

            mainAccordion.Visible = true;
            mainAccordion.ActiveGroup = root;
            foreach (NavButton button in menuNavPane.Buttons)
            {
                button.Appearance.ForeColor = button.Name == "menu_" + root.Name
                    ? Color.PaleTurquoise
                    : Color.White;
            }
        }

        /// <summary>활성 탭을 연 메뉴의 상위 그룹을 펼치고 해당 항목으로 스크롤합니다.</summary>
        private void TrackActiveMenu(object sender, EventArgs e)
        {
            // 같은 화면 클래스가 다른 메뉴에서도 열릴 수 있으므로 탭에 기록된 메뉴 ID를 사용합니다.
            string menuId = xtraTabbedMdiManager1.SelectedPage?.MdiChild?.Tag as string;
            var path = new List<AccordionControlElement>();
            if (string.IsNullOrEmpty(menuId) || !FindMenuPath(mainAccordion.Elements, menuId, path))
            {
                mainAccordion.SelectedElement = null;
                return;
            }

            // 검색으로 숨겨진 항목도 활성 탭으로 돌아가면 메뉴에서 바로 찾을 수 있게 합니다.
            if (!string.IsNullOrEmpty(mainAccordion.FilterText))
                ucFilterControl1.ClearFilter();

            SelectMenuModule(path[0]);
            for (int index = 0; index < path.Count - 1; index++)
                path[index].Expanded = true;

            AccordionControlElement item = path[path.Count - 1];
            mainAccordion.SelectedElement = item;
            mainAccordion.MakeElementVisible(item);
        }

        /// <summary>메뉴 ID가 일치하는 항목까지의 경로를 대메뉴부터 순서대로 찾습니다.</summary>
        private static bool FindMenuPath(AccordionControlElementCollection elements, string menuId,
            List<AccordionControlElement> path)
        {
            foreach (AccordionControlElement element in elements)
            {
                path.Add(element);
                if (element.Style == ElementStyle.Item && element.Name == menuId)
                    return true;
                if (FindMenuPath(element.Elements, menuId, path))
                    return true;
                path.RemoveAt(path.Count - 1);
            }
            return false;
        }

        private void mainAccordion_ElementClick(object sender, ElementClickEventArgs e)
        {
            try
            {
                if (e.Element.Style == DevExpress.XtraBars.Navigation.ElementStyle.Group) return;
                if (e.Element.Tag == null) return;

                string[] arrTag = e.Element.Tag.ToString().Split(new char[] { '|' });

                if (!IsOpen(arrTag[2], e.Element.Name))
                {
                    //string assemblyName = arrTag[0] == "SmartMES" ? System.Reflection.Assembly.GetExecutingAssembly().GetName().Name : arrTag[0];
                    string assemblyName = arrTag[0] == "SmartMES" || arrTag[0] == "SMT_MES" ? System.Reflection.Assembly.GetExecutingAssembly().GetName().Name: arrTag[0];

                    string temp = arrTag[0] + "." + arrTag[1] + "." + arrTag[2] + "," + assemblyName;
                    WrGlobal.OpeningMenuId = e.Element.Name;

                    object[] constructorArgs = null;

                    // arrTag[3](ParamText)이 비어있지 않은 경우
                    if (arrTag.Length > 3 && !string.IsNullOrEmpty(arrTag[3]))
                    {
                        // 연속된 쉼표(,,) 사이의 빈칸은 빈 문자열("")로 보존하여 쪼갬 (예: ",20" -> ["", "20"])
                        string[] multiParams = arrTag[3].Split(new char[] { ',' });

                        constructorArgs = new object[multiParams.Length];
                        for (int i = 0; i < multiParams.Length; i++)
                        {
                            constructorArgs[i] = multiParams[i];
                        }
                    }
                    else
                    {
                        constructorArgs = null;
                    }

                    var frm = Activator.CreateInstance(Type.GetType(temp), constructorArgs) as Form;
                    frm.Name = arrTag[2];
                    frm.Text = e.Element.Text;
                    frm.Tag = e.Element.Name;
                    doOpenForm(frm);
                }
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void gv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                GridView gv = sender as GridView;
                if (gv.FocusedValue == null) return;

                e.Handled = true;
                gv.GetFocusedDisplayText();
                string selectedText = gv.GetFocusedDisplayText();
                Clipboard.SetText(selectedText);
            }

            //if (e.KeyCode == Keys.Enter)
            //{
            //    GridView view = sender as GridView;

            //    int rowHandle = view.FocusedRowHandle;
            //    if (view.FocusedColumn == null) return;

            //    int colIndex = view.FocusedColumn.VisibleIndex;

            //    view.FocusedRowHandle = rowHandle + 1;
            //    view.FocusedColumn = view.VisibleColumns[colIndex];

            //    e.Handled = true;
            //}
        }

        private void TreeList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                DevExpress.XtraTreeList.TreeList tr = sender as DevExpress.XtraTreeList.TreeList;
                if (tr.FocusedValue == null) return;

                e.Handled = true;
                tr.GetFocusedDisplayText();
                string selectedText = tr.GetFocusedDisplayText();
                Clipboard.SetText(selectedText);
            }
        }

        public void doOpenForm(Form frm)
        {
            // 아이콘
            frm.Icon = this.Icon;
            frm.ShowIcon = true;

            try
            {
                if (frm.GetType().Name == "frmPasswordChange")
                {
                    Rectangle r = this.RectangleToScreen(this.Bounds);
                    frm.Left = r.Left + (r.Width - this.Width) / 2;
                    frm.Top = r.Top + (r.Height - this.Height) / 4;

                    if (frm.ShowDialog() == DialogResult.OK)
                    {

                    }
                }
                else
                {
                    sys.UseSession((frm.Tag ?? "").ToString(), "Open");

                    RegistryKey reg = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\SNEXISMES", true);

                    if (reg != null)
                    {
                        string font = Convert.ToString(reg.GetValue("Font"));
                        float size = Convert.ToSingle(reg.GetValue("Size"));

                        if (font != "")
                        {
                            Font newFont = new Font(font, size, FontStyle.Bold);

                            foreach (Control ctrl in frm.Controls)
                            {
                                if (ctrl is TileNavPane tileNavPane)
                                {
                                    tileNavPane.Appearance.Font = newFont;
                                    tileNavPane.AppearanceHovered.Font = newFont;
                                    tileNavPane.AppearanceSelected.Font = newFont;
                                }
                            }
                        }
                    }

                    frm.MdiParent = this;
                    frm.Closed += new EventHandler(MDIChildrenCleanup);
                    frm.Dock = DockStyle.Fill;
                    frm.WindowState = FormWindowState.Maximized;

                    //탭색상설정
                    xtraTabbedMdiManager1.AppearancePage.Header.BackColor = Color.Transparent; // 비활성화된 탭의 색상
                    xtraTabbedMdiManager1.AppearancePage.HeaderActive.BackColor = Color.FromArgb(49, 63, 92); // 활성화된 탭의 색상

                    frm.Show();

                    System.Collections.Generic.List<Control> controls = getControls(frm);

                    if (frm is DevExpress.XtraEditors.XtraForm)
                    {
                        foreach (Control c in controls)
                        {
                            if (c is DevExpress.XtraGrid.GridControl)
                            {
                                DevExpress.XtraGrid.GridControl gc = c as DevExpress.XtraGrid.GridControl;

                                if (gc.MainView != null)
                                {
                                    gc.MainView.KeyDown += new KeyEventHandler(gv_KeyDown);
                                }
                            }
                            else if (c is DevExpress.XtraTreeList.TreeList)
                            {
                                DevExpress.XtraTreeList.TreeList tr = c as DevExpress.XtraTreeList.TreeList;

                                tr.KeyDown += new KeyEventHandler(TreeList_KeyDown);
                            }
                        }
                    }

                    try { ScreenLayouts.Register(frm); }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show("저장된 화면 배치를 불러오지 못했습니다. 기본 배치로 표시합니다.\r\n" + ex.Message,
                            "화면 배치", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    Application.DoEvents();
                    frm.BringToFront();

                }
            }
            catch { }

        }//end function

        private System.Collections.Generic.List<Control> getControls(Control c)
        {
            System.Collections.Generic.List<Control> controls = new System.Collections.Generic.List<Control>();

            foreach (Control k in c.Controls)
            {
                controls.Add(k);
                controls.AddRange(getControls(k));
            }
            return controls;
        }

        private bool IsOpen(string frmName, string tagName)
        {
            try
            {
                bool m_fReturn = false;
                foreach (var m_form in this.MdiChildren)
                {
                    if (m_form.Name.Equals(frmName) && (m_form.Tag ?? "").ToString() == tagName)
                    {
                        m_form.Activate();
                        m_fReturn = true;
                        break;
                    }//end if

                }//end foreach
                return m_fReturn;
            }
            catch { return false; }
        }//end function

        private void MDIChildrenCleanup(object sender, EventArgs e)
        {
            ((Form)sender).Dispose();
        }

        private void Frm_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            tmr_Notice.Enabled = false;

            if (WrGlobal.SessionNo != "")
            {
                sys.CloseSession();

                string startDate = WrGlobal.LoginTime.ToString("yyyy-MM-dd HH:mm:00");
                string endDate = WrGlobal.LoginTime.ToString("yyyy-MM-dd HH:mm:59");

                query = $@"
                        UPDATE SM_LoginHistory
                        SET Logout_Time =  GETDATE()
                        WHERE Login_Time >= '{startDate}' AND Login_Time <= '{endDate}'
                        AND Login_IP = '{WrGlobal.IpAddress}'
                    ";

                db.ExecuteQuery(query);
            }
        }

        private void btnChangePw_Click(object sender, EventArgs e)
        {
            var frm = Activator.CreateInstance(Type.GetType(string.Format("{0}.View.Systems.frmPasswordChange", WrGlobal.ProJectName)), WrGlobal.LoginID) as Form;
            doOpenForm(frm);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("사용자({0}) : 로그아웃 하겠습니까?", WrGlobal.LoginID), "삭제", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
            {
                Application.Restart();
            }//end if

        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Skin currentSkin = FormSkins.GetSkin(this.LookAndFeel);
            SkinElement element = currentSkin[FormSkins.SkinFormCaption];
            element.ContentMargins.Top = 6;
            element.ContentMargins.Bottom = 6;

            //element.Color.SolidImageCenterColor = Color.FromArgb(255, 33, 80, 126);
            this.LookAndFeel.UpdateStyleSettings();

            //OpenNotice();
        }

  //      protected override DevExpress.Skins.XtraForm.FormPainter CreateFormBorderPainter()
  //      {
		//	return new MyFormPainter(this, LookAndFeel, Resources.hosung_logo3_edited, new Size(140, 28));
		//	//return new MyFormPainter(this, LookAndFeel, Resources.SF로고원본ai파일_투명_, new Size(140, 28));
		//}

        public class MyFormPainter : FormPainter
        {
            private readonly Icon _icon;
            private readonly Size _size;

            public MyFormPainter(Control owner, ISkinProvider provider) : base(owner, provider) { }

            public MyFormPainter(Control owner, ISkinProvider provider, Icon icon, Size size) : base(owner, provider)
            {
                _icon = icon;
                _size = size;
            }

            protected override Size GetIconSize() { return _size; }
            protected override Icon GetIcon() { return _icon; }

            protected override void DrawText(DevExpress.Utils.Drawing.GraphicsCache cache)
            {
                string text = Text;
                if (text == null || text.Length == 0 || this.TextBounds.IsEmpty) return;
                AppearanceObject appearance = new AppearanceObject(GetDefaultAppearance());
                appearance.Font = new Font("Segoe UI", 18, FontStyle.Bold);
                appearance.TextOptions.Trimming = Trimming.EllipsisCharacter;
                appearance.ForeColor = Color.FromArgb(0, 100, 180);
                Rectangle r = RectangleHelper.GetCenterBounds(TextBounds, new Size(TextBounds.Width, appearance.CalcDefaultTextSize(cache.Graphics).Height));
                DrawTextShadow(cache, appearance, r);
                cache.DrawString(text, appearance.Font, appearance.GetForeBrush(cache), r, appearance.GetStringFormat());
            }
        }

        private void frmMain2_Load(object sender, EventArgs e)
        {
            //레지스트리 스킨 저장 불러오기
            UserLookAndFeel.Default.SetSkinStyle(ReadRegKey("Skin"));

            //스킨 변경
            GalleryControlGallery gallery = barBtnSkin.DropDownGallery.Gallery;
            gallery.ItemClick += Gallery_ItemClick;

            //디폴트 폰트
            RegistryKey reg = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\SNEXISMES", true);

            if (reg == null)
            {
                return;
            }

            Set_DefaultFont();
            //OpenNotice();

            doOpenForm(new Frm_MM_Dashboard_Total1()); //대시보드 화면
        }

        private void Set_DefaultFont()
        {
            RegistryKey reg = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\SNEXISMES", true);

            if (reg == null)
            {
                return;
            }

            string font = Convert.ToString(reg.GetValue("Font"));
            float size = Convert.ToSingle(reg.GetValue("Size"));

            if (font != "")
            {
                Font newFont = new Font(font, size, FontStyle.Bold);
                DevExpress.XtraEditors.WindowsFormsSettings.DefaultFont = newFont;
            }
        }

        private void Gallery_ItemClick(object sender, GalleryItemClickEventArgs e)
        {
            string skin = e.Item.Caption;
            WriteRegKey("Skin", skin);
        }
     
        private void navButton3_ElementClick(object sender, NavElementEventArgs e)
        {
            Point p = menuNavPane.Parent.PointToScreen(menuNavPane.Location);
            p.X += menuNavPane.Size.Width;
            p.Y += menuNavPane.Size.Height;
            p.X -= 200;

            if (WrGlobal.LoginID.ToUpper() != "DEV")
            {
                barBtnMail.Visibility = BarItemVisibility.Never; //메일
                barBtnSkin.Visibility = BarItemVisibility.Never; //스킨
                barBtnFont.Visibility = BarItemVisibility.Never; //폰트
                barBtnBot.Visibility = BarItemVisibility.Never;  //FAQ
                barButtonItem5.Visibility = BarItemVisibility.Never; //챗봇
            }

            popupMenu1.ShowPopup(barManager1,p);
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            Frm_SM_PasswordChange form = new Frm_SM_PasswordChange();
            form.ShowDialog();
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (MessageBox.Show(string.Format("사용자({0}) : 로그아웃 하겠습니까?", WrGlobal.LoginID), "삭제", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
            {
                Application.Restart();
            }//end if
        }

        private void barClose_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form A in this.MdiChildren)
            {
                if (A == xtraTabbedMdiManager1.SelectedPage.MdiChild)
                {
                    A.Close();
                }
            }
        }

        private void barCloseAll_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form A in this.MdiChildren)
            {
                A.Close();
            }
        }

        private void barCloseJe_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (Form A in this.MdiChildren)
            {
                if (A != xtraTabbedMdiManager1.SelectedPage.MdiChild)
                {
                    A.Close();
                }
            }
        }

		private void tmr_Notice_Tick(object sender, EventArgs e)
        {
            //if (lastDate != DateTime.Now.ToString("yyyyMMdd"))
            //{
            //    OpenNotice();
            //}
        }

        private void WriteRegKey(string name, string value)
        {
            RegistryKey reg = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\SNEXISMES", true);

            if (reg == null)
            {
                reg = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\SNEXISMES");
            }


            reg.SetValue(name, value);

            reg.Close();
        }


        private string ReadRegKey(string name)
        {
            RegistryKey reg = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\SNEXISMES", true);
            string defaultSkin = "WXI";

            if (reg == null)
            {
                reg = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\SNEXISMES");

                if(name == "Skin") reg.SetValue(name, defaultSkin);
            }

            string value = Convert.ToString(reg.GetValue(name));

            if(name == "Skin" && value == "")
			{
                reg.SetValue(name, defaultSkin);
                value = defaultSkin;
			}

            reg.Close();

            return value;
        }

		private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
		{
            this.Close();
		}

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            Frm_SM_FontChange frm = new Frm_SM_FontChange();
            frm.ShowDialog();
        }

        private void ApplyFontToAllControls(Control parent, Font newFont)
        {
            foreach (Control c in parent.Controls)
            {
                c.Font = newFont;

                // DevExpress GridControl 내부 GridView 처리
                if (c is GridControl gridControl)
                {
                    foreach (var view in gridControl.ViewCollection)
                    {
                        if (view is DevExpress.XtraGrid.Views.Grid.GridView gridView)
                        {
                            gridView.Appearance.Row.Font = newFont;
                            gridView.Appearance.HeaderPanel.Font = newFont;
                            gridView.Appearance.FocusedRow.Font = newFont;
                            gridView.Appearance.SelectedRow.Font = newFont;
                        }
                    }
                }

                // DevExpress XtraTabControl (탭 컨트롤) 처리
                if (c is XtraTabControl tabControl)
                {
                    tabControl.Appearance.Font = newFont;
                    tabControl.AppearancePage.Header.Font = newFont;
                    tabControl.AppearancePage.HeaderActive.Font = newFont;
                }

                // DevExpress RibbonControl (리본 메뉴) 처리
                if (c is RibbonControl ribbon)
                {
                    ribbon.Font = newFont;
                    foreach (BarItem item in ribbon.Items)
                    {
                        item.Appearance.Font = newFont;
                        item.Appearance.Options.UseFont = true;
                    }
                }

                // 하위 컨트롤도 재귀적으로 처리
                if (c.HasChildren)
                {
                    ApplyFontToAllControls(c, newFont);
                }
            }
        }

		private void lblMonthlySalesTrends_Click(object sender, EventArgs e)
		{
            Frm_SM_MenuGroup frm = new Frm_SM_MenuGroup();
            doOpenForm(frm);
		}

		private void barBtnMail_ItemClick(object sender, ItemClickEventArgs e)
		{
            frmSendmail frm = new frmSendmail();
            doOpenForm(frm);
        }

        private void barBtnBot_ItemClick(object sender, ItemClickEventArgs e)
        {
            //챗봇까지는 아니고 지금은 FAQ봇
            if (WrGlobal.LoginID.ToLower() == "dev")
            {
                // 열려있는 창 확인
                foreach (Form frm in Application.OpenForms)
                {
                    if (frm is ChatMain_V2)
                    {
                        frm.BringToFront();
                        frm.Activate();
                        return; // 새로 안 띄움
                    }
                }

                // 없으면 새로 생성
                ChatMain_V2 newFrm = new ChatMain_V2();
                newFrm.Show();
            }
        }

        private void barButtonItem4_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm.Name == "Frm_MM_Dashboard_Total1")
                {
                    frm.Activate();
                    return;
                }
            }

            doOpenForm(new Frm_MM_Dashboard_Total1());
        }

        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (WrGlobal.LoginID.ToLower() == "dev")
            {
                // 열려있는 창 확인
                foreach (Form frm in Application.OpenForms)
                {
                    if (frm is FrmChatAI)
                    {
                        frm.BringToFront();
                        frm.Activate();
                        return; // 새로 안 띄움
                    }
                }

                // 없으면 새로 생성
                FrmChatAI newFrm = new FrmChatAI();
                newFrm.Show();
            }
        }


        #region 화면 배치 저장
        private SmartMES.Common.ScreenLayoutManager screenLayouts;
        private SmartMES.Common.ScreenLayoutManager ScreenLayouts
        {
            get
            {
                if (screenLayouts == null)
                    screenLayouts = new SmartMES.Common.ScreenLayoutManager(
                        new SmartMES.Common.ScreenLayoutRepository(Database.m_ConnectionString, WrGlobal.CorpID, WrGlobal.LoginID));
                return screenLayouts;
            }
        }

        private void InitializeScreenLayoutIcons()
        {
            // 기존 메뉴와 같은 SVG 아이콘을 사용합니다.
            string save = "<svg xmlns='http://www.w3.org/2000/svg' width='24' height='24' viewBox='0 0 24 24'><path fill='#3185c5' d='M3 2h15l4 4v16H2V2z'/><path fill='#fff' d='M6 3h10v6H6zm0 10h12v8H6z'/><path fill='#3185c5' d='M13 4h2v4h-2z'/></svg>";
            string reset = "<svg xmlns='http://www.w3.org/2000/svg' width='24' height='24' viewBox='0 0 24 24'><path fill='none' stroke='#00a690' stroke-width='2.5' d='M5 7a8 8 0 1 1-1 8'/><path fill='#00a690' d='M2 2v8h8z'/></svg>";
            using (var stream = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(save)))
                barBtnScreenSave.ImageOptions.SvgImage = DevExpress.Utils.Svg.SvgImage.FromStream(stream);
            using (var stream = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(reset)))
                barBtnColumnLayoutReset.ImageOptions.SvgImage = DevExpress.Utils.Svg.SvgImage.FromStream(stream);
        }

        private void ScreenSaveButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                ScreenLayouts.Save(ActiveMdiChild);
                XtraMessageBox.Show("현재 화면 배치를 저장했습니다. 다음에 이 화면을 열면 적용됩니다.",
                    "화면 저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "화면 저장 실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ColumnLayoutResetButton_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (ActiveMdiChild == null)
            {
                XtraMessageBox.Show("초기화할 업무 화면을 먼저 여세요.", "화면 초기화");
                return;
            }
            if (XtraMessageBox.Show("현재 화면의 저장 배치를 지우고 기본 배치로 되돌릴까요?", "화면 초기화",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            try
            {
                ScreenLayouts.Reset(ActiveMdiChild);
                XtraMessageBox.Show("현재 화면을 소스의 기본 배치로 초기화했습니다.", "화면 초기화");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "화면 초기화 실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

    }
}
