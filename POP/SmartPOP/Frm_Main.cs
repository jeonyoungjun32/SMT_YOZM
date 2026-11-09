using DevExpress.XtraBars.Navigation;
using Microsoft.Win32;
using SmartPOP.Common;
using SmartPOP.Models.IF;
using SmartPOP.Views.CM;
using SmartPOP.Views.Common;
using SmartPOP.Views.Lookup;
using SMT_MES.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SmartPOP
{
	public partial class Frm_Main : DevExpress.XtraEditors.XtraForm
	{
		Database db = new Database();
		Models.clsSystem sys = new Models.clsSystem();
		string displayNoticeYn;
		string query = "";
		bool AutoHide = false;
		AccordionControlElement myMenuElement = null;

		private readonly List<PLCCollector> plcCollectors = new List<PLCCollector>();
		private readonly object plcLock = new object();

		public Frm_Main()
		{
			bool autoLogin = false;

			try
			{
				Database db = new Database();

				string query = $@"
                                    SELECT TOP 1
	                                    slh.CorpId
	                                    , slh.ID
	                                    , su.AutoLoginYn
                                    FROM SM_LoginHistory slh
                                    INNER JOIN SM_User su ON slh.CorpId = su.CorpId AND slh.ID = su.EmployeeId
                                    WHERE slh.HostName = '{System.Net.Dns.GetHostName()}'
                                    AND slh.Login_Project = '{WrGlobal.Login_Project}'
                                    ORDER BY slh.Login_Time DESC
                                ";

				DataRowView drv = db.GetDataRecord(query);

				if (drv != null)
				{
					string autoYn = drv["AutoLoginYn"].ConvertString_Null_To_Empty();

					if (autoYn == "Y")
					{
						Frm_Login login = new Frm_Login();

						autoLogin = login.LoginProcess(drv["CorpId"].ToString(), drv["ID"].ToString(), "", true);
					}
				}
			}
			catch
			{
			}

			// 자동로그인 실패 시 일반 로그인창
			if (!autoLogin)
			{
				Frm_Login login = new Frm_Login();

				login.ShowDialog();
			}

			// 기존 유지
			if (WrGlobal.LoginID == "")
			{
				Application.ExitThread();
				Environment.Exit(0);
			}
			else
			{
				InitializeComponent();
			}

			mainAccordion.Footer.Visible = false;

			lbl_Process.Text = "현공정:" + WrGlobal.ProcName;
		}

		private void frmMain_Load(object sender, EventArgs e)
		{
			// 메뉴 세팅
			setUserAccordionMenu();

			// 로그인 정보
			LoadSetting();

			// 작업지시/공지사항 화면
			doOpenForm(new Frm_MainInfo());

			StartPLC();
		}

		private void setUserAccordionMenu()
		{
			mainAccordion.Elements.Clear();
			menuNavPane.Buttons.Clear();

			mainAccordion.GroupHeight = 50; // Group(Level 0)의 높이 설정 (원하는 크기로 조절 가능)
			mainAccordion.ItemHeight = 50;  // Item(Level 1)의 높이 설정 (원하는 크기로 조절 가능)

			DataView dv = sys.GetEmployeeMenuList();
			DataView dvQuick = sys.GetEmployeeQuickMenuList("Y");
			dvQuick.Sort = "SortNum ASC";

			AccordionControlElement rootGroup = null;
			AccordionControlElement myMenuGroup = null;

			NavButton navButton = null;
			Font groupFont = new Font("Pretendard SemiBold", 14F, FontStyle.Bold); // Level 0 그룹용 큰 폰트
			Font itemFont = new Font("Pretendard SemiBold", 12F, FontStyle.Regular); // Level 1 아이템용 폰트

			foreach (DataRowView drv in dv)
			{
				string level = drv["GroupLevel"].ToString();
				string title = drv["Title_ko" + WrGlobal.Language].ToString();

				if (level == "0")
				{
					rootGroup = new DevExpress.XtraBars.Navigation.AccordionControlElement(DevExpress.XtraBars.Navigation.ElementStyle.Group);
					rootGroup.Name = drv["ID"].ToString();
					rootGroup.Text = title;
					rootGroup.Expanded = true;

					rootGroup.Appearance.Normal.Font = groupFont;
					rootGroup.Appearance.Normal.Options.UseFont = true;
					rootGroup.Appearance.Normal.ForeColor = Color.LightCyan;
					rootGroup.Appearance.Normal.Options.UseForeColor = true;

					rootGroup.Appearance.Hovered.Font = groupFont;
					rootGroup.Appearance.Hovered.Options.UseFont = true;

					rootGroup.Appearance.Pressed.Font = groupFont;
					rootGroup.Appearance.Pressed.Options.UseFont = true;

					if (!string.IsNullOrEmpty(drv["ImageKey"].ToString()))
					{
						rootGroup.ImageOptions.SvgImage = svgImages[drv["ImageKey"].ToString()];
						rootGroup.ImageOptions.SvgImageSize = new Size(24, 24);
					}

					rootGroup.Appearance.Normal.ForeColor = Color.LightCyan;

					mainAccordion.Elements.Add(rootGroup);

					// Nav 버튼
					navButton = new NavButton();
					navButton.Name = "menu_" + drv["ID"].ToString();
					navButton.Caption = title;

					if (!string.IsNullOrEmpty(drv["ImageKey"].ToString()))
					{
						navButton.ImageOptions.SvgImage = svgImages[drv["ImageKey"].ToString()];
						navButton.ImageOptions.SvgImageSize = new Size(24, 24);
					}

					navButton.ElementClick += new NavElementClickEventHandler(MenuNavClick);
					navButton.AppearanceHovered.BackColor = Color.FromArgb(56, 138, 134);
					navButton.AppearanceSelected.BackColor = Color.FromArgb(56, 138, 134);

					menuNavPane.Buttons.Add(navButton);

					continue;
				}

				// Level 1 메뉴
				if (level == "1")
				{
					// rootGroup이 없으면 기본 그룹 자동 생성
					if (rootGroup == null)
					{
						rootGroup = new DevExpress.XtraBars.Navigation.AccordionControlElement(DevExpress.XtraBars.Navigation.ElementStyle.Group);
						rootGroup.Name = "defaultGroup";
						rootGroup.Text = "";
						rootGroup.Expanded = true;
						rootGroup.Appearance.Normal.ForeColor = Color.LightCyan;

						rootGroup.Appearance.Normal.Font = groupFont;
						rootGroup.Appearance.Normal.Options.UseFont = true;
						rootGroup.Appearance.Normal.ForeColor = Color.LightCyan;
						rootGroup.Appearance.Normal.Options.UseForeColor = true;

						mainAccordion.Elements.Add(rootGroup);
					}

					// 즐겨찾기
					if (title == "MyMenu")
					{
						continue;
					}

					var eleItem = new DevExpress.XtraBars.Navigation.AccordionControlElement(DevExpress.XtraBars.Navigation.ElementStyle.Item);
					eleItem.Name = drv["ID"].ToString();
					eleItem.Text = "" + title;

					eleItem.Tag = drv["ProjectName"] + "|" +
								  drv["FolderName"] + "|" +
								  drv["FormName"] + "|" +
								  drv["ParamText"];

					if (!string.IsNullOrEmpty(drv["ImageKey"].ToString()))
					{
						eleItem.ImageOptions.SvgImage = svgImages[drv["ImageKey"].ToString()];
						eleItem.ImageOptions.SvgImageSize = new Size(24, 24);
					}

					eleItem.Appearance.Normal.Font = itemFont;
					eleItem.Appearance.Normal.Options.UseFont = true;

					eleItem.Appearance.Hovered.Font = itemFont;
					eleItem.Appearance.Hovered.Options.UseFont = true;

					eleItem.Appearance.Pressed.Font = itemFont;
					eleItem.Appearance.Pressed.Options.UseFont = true;

					rootGroup.Elements.Add(eleItem);
				}
			}
		}

		private void MenuNavClick(object sender, DevExpress.XtraBars.Navigation.NavElementEventArgs e)
		{
			try
			{
				NavButton ctl = (NavButton)sender;
				string targetName = ctl.Name.Replace("menu_", "");

				mainAccordion.Visible = true;
				mainAccordion.ActiveGroup = mainAccordion.Elements[targetName];

				foreach (NavButton btn in menuNavPane.Buttons)
				{
					btn.Appearance.ForeColor = Color.FromArgb(255, 255, 255);
				}

				//상단 모듈 선택헀을 때 색
				//ctl.Appearance.ForeColor = Color.FromArgb(145, 241, 126);
				ctl.Appearance.ForeColor = Color.PaleTurquoise;
			}
			catch
			{ }
		}

		private void LoadSetting()
		{
			lblLogin.Text = $"{WrGlobal.Title} {WrGlobal.LoginUserNM}";
		}

		private void doOpenForm(Form frm)
		{
			if (frm == null)
			{
				MessageBox.Show("해당화면이 등록되어 있지 않습니다.", "화면연동", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			else
			{
				BeginInvoke(new Action(() =>
				{

					frm.MdiParent = this;
					frm.Closed += new EventHandler(MDIChildrenCleanup);
					frm.Dock = DockStyle.Fill;
					frm.WindowState = FormWindowState.Maximized;
					frm.Show();
					//Application.DoEvents();
					//frm.BringToFront();
				}));

				mainAccordion.OptionsMinimizing.State = DevExpress.XtraBars.Navigation.AccordionControlState.Minimized; //아코디언 접기
			}
		}//end function

		/*
         * 선택한 메뉴에 대한 폼이 활성화 되었는지 확인한다.
         * frmName : 폼의 이름 form's Name
         * menuName : 리본 이름
         */
		private bool IsOpen(string frmName, string tagName)
		{
			try
			{
				bool m_fReturn = false;
				foreach (var m_form in this.MdiChildren)
				{
					if (m_form.Name.Equals(frmName) && (m_form.Tag ?? "").ToString() == tagName)
					{
						m_form.Focus();
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

		public void OpenFormDynamic(string projectName, string folderName, string formName, string paramText, string elementId, string formTitle)
		{
			// 이미 열려있는지 확인 (기존 IsOpen 메서드 활용)
			if (IsOpen(formName, null)) return;

			try
			{
				// 1. 어셈블리 이름 결정 (SmartPOP이면 현재 실행 중인 어셈블리, 아니면 해당 프로젝트명)
				string assemblyName = projectName == "SmartPOP" ? System.Reflection.Assembly.GetExecutingAssembly().GetName().Name : projectName;

				// 2. 폼의 전체 네임스페이스 경로 생성 (중간 폴더명이 없는 경우 처리)
				string fullTypeName = "";
				if (string.IsNullOrEmpty(folderName))
				{
					fullTypeName = projectName + "." + formName + "," + assemblyName;
				}
				else
				{
					fullTypeName = projectName + "." + folderName + "." + formName + "," + assemblyName;
				}

				// 글로벌 변수에 현재 열리는 메뉴 ID 저장
				WrGlobal.OpeningMenuId = elementId;

				// 3. Type 객체 확보 및 인스턴스 생성
				Type formType = Type.GetType(fullTypeName);

				if (formType == null)
				{
					MessageBox.Show($"폼 타입을 찾을 수 없습니다.\n경로: {fullTypeName}");
					return;
				}

				Form frm = null;
				// 파라미터(생성자 매개변수) 유무에 따라 인스턴스 생성 방식 분기
				if (string.IsNullOrEmpty(paramText))
				{
					frm = Activator.CreateInstance(formType) as Form;
				}
				else
				{
					object[] constructorArgs = null;

					// arrTag[3](ParamText)이 비어있지 않은 경우
					if (!string.IsNullOrEmpty(paramText))
					{
						// 연속된 쉼표(,,) 사이의 빈칸은 빈 문자열("")로 보존하여 쪼갬 (예: ",20" -> ["", "20"])
						string[] multiParams = paramText.Split(new char[] { ',' });

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

					frm = Activator.CreateInstance(formType, constructorArgs) as Form;
				}

				if (frm == null)
				{
					MessageBox.Show($"폼 생성 실패: {formName}");
					return;
				}

				// 4. 폼 기본 속성 지정 후 메인폼의 기존 오픈 메서드 호출
				frm.Name = formName;
				frm.Text = formTitle;
				frm.Tag = elementId;

				doOpenForm(frm);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"동적 폼 오픈 중 오류 발생:\n{ex.Message}");
			}
		}

		private void PictureBox1_Click(object sender, EventArgs e)
		{
			string formName = "Main";

			if (!IsOpen(formName, ""))
			{
				doOpenForm(new Frm_MainInfo()); 
			}
		}

		// 공정 라인 설정 버튼
		private void cmd_Excel_Click(object sender, EventArgs e)
		{
			Frm_POPMenu f = new Frm_POPMenu();

			if (!IsOpen(f.Name, ""))
			{
				doOpenForm(f);
			}
			else
			{
				f.Focus();
			}

			//frmSetting form = new frmSetting();
			//form.ShowDialog();

			//lbl_Process.Text = "현공정:" + WrGlobal.ProcName;
		}

		private void timer1_Tick(object sender, EventArgs e)
		{

			lblTime.Text = string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now);

			query = $@"SELECT ISNULL(SUM(GoodQty), 0) GoodQty, ISNULL(SUM(NgQty), 0) NgQty, ISNULL(SUM(WorkQty), 0) AS WorkQty, (SELECT ISNULL(SUM(OrderQty), 0) OrderQty
            FROM PM_OrderProcess
            WHERE CorpId = '{WrGlobal.CorpID}'
            AND OrderDate = CONVERT(DATE, GETDATE())
            ) AS OrderQty
            FROM PM_OrderResult
            WHERE CorpId = '{WrGlobal.CorpID}'
            AND WorkDate = CONVERT(DATE, GETDATE())
            ";

			DataRow dr = db.GetDataRow(query);

			if (dr != null)
			{
				lblTotalWork.Text = dr["WorkQty"].ConvertFloat_Null_To_Zero().ToString("N0");
				lblGood.Text = dr["GoodQty"].ConvertFloat_Null_To_Zero().ToString("N0");
				lblNg.Text = dr["NgQty"].ConvertFloat_Null_To_Zero().ToString("N0");
				lblOrder.Text = dr["OrderQty"].ConvertFloat_Null_To_Zero().ToString("N0");
			}
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

		private void tmr_Notice_Tick(object sender, EventArgs e)
		{
			if (lastDate != DateTime.Now.ToString("yyyyMMdd"))
			{
				OpenNotice();
			}
		}

		private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
		{
			timer1.Enabled = false;
			tmr_Notice.Enabled = false;

			StopPLC();
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
					string assemblyName = arrTag[0] == "SmartPOP" ? System.Reflection.Assembly.GetExecutingAssembly().GetName().Name : arrTag[0];

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
			catch (Exception ex)
			{
				cls_MessageBox.Show(ex.Message);
			}
		}

		private void StartPLC()
		{
			try
			{
				StopPLC();

				string userId = WrGlobal.LoginID;

				IF_DgpControl control = new IF_DgpControl();
				control.EmployeeId = userId;

				DataTable dt = control.Select_DgpControl_by_EmployeeId();

				if (dt == null || dt.Rows.Count == 0)
				{
					return;
				}

				foreach (DataRow dr in dt.Rows)
				{
					try
					{
						string useYn = dr["CT_UserYn"].ConvertString_Null_To_Empty();

						if (useYn != "Y")
						{
							continue;
						}

						string ctCode = dr["CTCode"].ConvertString_Null_To_Empty();

						string ip = dr["CT_IP"].ConvertString_Null_To_Empty();

						if (string.IsNullOrEmpty(ip))
						{
							continue;
						}

						int port = dr["CT_PORT"].ConvertInt_Null_To_Zero();

						string maker = dr["CT_Company"].ConvertString_Null_To_Empty();

						PLCType type;

						switch (maker)
						{
							case "LS":
								type = PLCType.LS;
								break;

							case "Mitsubishi":
								type = PLCType.Mitsubishi;
								break;

							default:
								continue;
						}

						bool exists = plcCollectors.Any(x =>
							x.IP == ip &&
							x.Port == port);

						if (exists)
						{
							continue;
						}

						PLCCollector collector = new PLCCollector
						{
							CTCode = ctCode,
							IP = ip,
							Port = port,
							Type = type
						};

						collector.Start(ip, port, type, ctCode);

						lock (plcLock)
						{
							plcCollectors.Add(collector);
						}
					}
					catch (Exception ex)
					{
						Debug.WriteLine(ex.Message);
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}

		private void StopPLC()
		{
			lock (plcLock)
			{
				foreach (PLCCollector collector in plcCollectors)
				{
					try
					{
						collector.Stop();
					}
					catch (Exception ex)
					{
						Debug.WriteLine(ex.Message);
					}
				}

				plcCollectors.Clear();
			}
		}
	}
}
