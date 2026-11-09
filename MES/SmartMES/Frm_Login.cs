using SmartMES.Model.SM;
using SmartMES.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Microsoft.Win32;
using SmartMES.Models.SM;
using SmartCommon.Common;
using SmartMES.View;
using static DevExpress.XtraEditors.Mask.MaskSettings;
using DevExpress.CodeParser.Diagnostics;
using System.Configuration;
using DevExpress.XtraEditors;

namespace SmartMES
{
	public partial class Frm_Login : DevExpress.XtraEditors.XtraForm
    {

        string query = "";

        private string userId = string.Empty;
        private string saveYn = string.Empty;
        private string dbName = string.Empty;

        public Frm_Login()
        {
            InitializeComponent();
            // 아이콘
            Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            AcceptButton = button2;
        }

        private void Frm_Login_Load(object sender, EventArgs e)
        {
            panelControl1.Visible = false;

            ReadRegValue();
            ReadRegConInfo();

            Change_Logo();

            if(Database.Url.ConvertString_Null_To_Empty() == "")
			{
                Btn_Setting.PerformClick();
			}
			else
			{
				SetInit();

                if (saveYn == "Y")
                {
                    txtID.Text = userId;
                    chkRemember.Checked = true;
                }
            }
		}

        private void SetInit()
		{
            Database db = new Database();
            query = $"select CorpID 코드, CorpName 사업장 from SM_Corp Where CorpId <> '{WrGlobal.PublicCorpID}'";
            cboCompany.Properties.DisplayMember = "사업장";
            cboCompany.Properties.ValueMember = "코드";
            cboCompany.Properties.DataSource = db.GetDataView("사업장", query);

            query = $@"select top 1 al.CorpId, cu.EmployeeID, cu.Password, al.Login_Time
                        from SM_LoginHistory al
                        Inner Join SM_User cu on al.CorpId = cu.CorpId and cu.EmployeeID = al.ID
                        where HostName = '{System.Net.Dns.GetHostName()}'
                        and Login_Project = '{WrGlobal.Login_Project}'
                        order  by al.Login_Time desc";

            DataRowView drv = db.GetDataRecord(query);

            if (drv != null)
            {
                cboCompany.EditValue = drv["CorpId"];
                txtPW.Text = "";
            }
            txtPW.Focus();
        }

        // 로그인
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(SetReinstall())
                SetLogin();
        }

        private void SetLogin()
        {
            Database db = new Database();
            clsSystem sys = new clsSystem();

            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                txtID.Focus();
                MessageBox.Show("ID를 입력해주세요");
                return;
            }
            if (string.IsNullOrEmpty(txtPW.Text))
            {
                txtPW.Focus();
                MessageBox.Show("PASSWORD를 입력해주세요");
                return;
            }

            query = $"exec Employee_GetUserInfo '{cboCompany.EditValue}', '{txtID.Text}', '{txtPW.Text}'";
            DataRowView drv = db.GetDataRecord(query);
            if (drv != null)
            {
                //로그인 성공
                WrGlobal.CorpID = cboCompany.EditValue.ToString();
                WrGlobal.CorpName = cboCompany.Text.ToString();
                WrGlobal.LoginID = txtID.Text;
                WrGlobal.LoginUserNM = drv["EmployeeName"].ToString();
                WrGlobal.DeptCD = drv["DeptCode"].ToString();
                WrGlobal.DeptName = drv["DeptName"].ToString();
                WrGlobal.Title = drv["JobTitle"].ToString();
                WrGlobal.DBName = Database.DbName;

                //호성테크닉스 전역변수 추가
                WrGlobal.Email = drv["Email"].ToString();
                WrGlobal.CustYn = drv["CustYn"].ToString();
                WrGlobal.CustCode = drv["CustCode"].ToString();
                WrGlobal.CustType = Convert.ToString(drv["CustType"]);

                //권한 세팅
                sys.SetUserAuthor();
                //세션 설정
                sys.CreateSession();

                CreateWrGlobal(drv.Row);        //Global데이터 입력

                SetAutoLogin();               //로그인이력 입력

                //접속 이력 API
                LogEvent LogEvent = new LogEvent();
                LogEvent.CrudEvent("접속", LogEvent.SelectSM_UserLogin());

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                MessageBox.Show("ID 또는 비밀번호가 틀렸습니다.");
            }
        }

        private bool SetReinstall()
        {
			//if (Database.DbName.Contains("UAGHITEC"))
			//{
			//    string url = "http://smtv.iptime.org/YOZM_UAG/MES/index.html";

			//    XtraMessageBox.Show($"기존 MES 삭제 후 아래 링크에서 재설치 후 사용하십시오. \n{url}", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
			//    System.Diagnostics.Process.Start(url);

			//    this.Close();
			//    return false;
			//}
			//else
			return true;
		}

        private void CreateWrGlobal(DataRow drUser)
        {
        
            try
            {
                SM_SystemOption procOption = new SmartMES.Model.SM.SM_SystemOption();
                DataTable dtProcOption = procOption.Select_SystemOption();

                foreach (DataRow dr in dtProcOption.Rows)
                {
                    string div = Convert.ToString(dr["TaskDiv"]);
                    string option = Convert.ToString(dr["OptionNm"]);
                    string value = Convert.ToString(dr["Value"]);

                    //업무구분
                    if (WrGlobal.ProcOption.ContainsKey(div))
                    {
                        WrGlobal.ProcOption[div].Add(option, value);
                    }
                    else
                    {
                        WrGlobal.ProcOption.Add(div, new Dictionary<string, string>() { { option, value } });
                    }
                }

                //모듈별 공통 품목구분
                WrGlobal.Business_ItemType = WrGlobal.ProcOption["SC"]["Business_ItemType"].Split(',');
                WrGlobal.Product_ItemType = WrGlobal.ProcOption["SC"]["Product_ItemType"].Split(',');
                WrGlobal.Material_ItemType = WrGlobal.ProcOption["SC"]["Material_ItemType"].Split(',');

                //CodeOption 호출
                SM_CodeOption cdOption = new SM_CodeOption();
				DataTable dtCdOption = cdOption.Select_CdOption();
				foreach (DataRow dr in dtCdOption.Rows)
				{
					string div = Convert.ToString(dr["DivCd"]);
					string fieldNm = Convert.ToString(dr["FieldID"]);
					string caption = Convert.ToString(dr["CaptionNm"]);
					int DispSeq = dr["DispSeq"].ConvertString_Null_To_Empty() == "" ? 99 : dr["DispSeq"].ConvertInt_Null_To_Zero();
					bool disp = Convert.ToString(dr["DispYn"]) == "Y" ? true : false;
					bool lockYn = Convert.ToString(dr["LockYn"]) == "Y" ? true : false;

					//Option 목록
					WrGlobal.CodeOption optionList = new WrGlobal.CodeOption { Nm = caption, DispSeq = DispSeq, DispYn = disp, LockYn = lockYn };

					//옵션구분 존재 확인
					if (WrGlobal.CdOption.ContainsKey(div))
					{
						//옵션구분이 존재시
						WrGlobal.CdOption[div].Add(fieldNm, optionList);
					}
					else
					{
						//옵션구분이 없을 시
						WrGlobal.CdOption.Add(div, new Dictionary<string, WrGlobal.CodeOption>() { { fieldNm, optionList } });
					}
				}
			}
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void SetAutoLogin()
        {
            WrGlobal.LoginTime = DateTime.Now;

            SM_LoginHistory lo = new SM_LoginHistory();
            lo.CorpID = cboCompany.EditValue.ConvertString_Null_To_Empty();
            lo.ID = WrGlobal.LoginID;
            lo.Login_Time = WrGlobal.LoginTime;
            lo.Login_IP = WrGlobal.IpAddress;
            lo.Login_Project = WrGlobal.Login_Project;

            lo.Ins_LoginOut_History();
        }

        private void txtID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtPW.Focus();
        }

        private void Frm_Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            WriteRegValue();

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.ConnectionStrings.ConnectionStrings["SmartMES.Properties.Settings.MESDBConnectionString"].ConnectionString = Database.m_ConnectionString;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("connectionStrings");
        }

        private void ReadRegValue()
        {
            try
            {
                RegistryKey reg = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\SNEXISMES", true);

                if (reg == null)
                {
                    return;
                }

                userId = (string)reg.GetValue("UserId");
                saveYn = (string)reg.GetValue("SaveYn");
                dbName = (string)reg.GetValue("DbName");

                reg.Close();
            }
            catch (Exception ex)
            {
            }
        }

        public void ReadRegConInfo()
        {
            //Registry에서 DB 연결주소, DB명 불러오기
            try
            {
                RegistryKey reg = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\SNEXISMES", true);

                if (reg == null)
                {
                    Database.Url = "smtv.iptime.org,2433";// (string)reg.GetValue("Url");
                    Database.DbName = "YOZM"; //(string)reg.GetValue("DbName");
                    return;
                }

                Database.Url = "smtv.iptime.org,2433";// (string)reg.GetValue("Url");
                Database.DbName = "YOZM"; //(string)reg.GetValue("DbName");

                reg.Close();
            }
            catch { }
        }

        public void WriteRegValue()
        {
            try
            {
                RegistryKey reg = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\SNEXISMES", true);

                if (reg == null)
                {
                    reg = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\SNEXISMES");
                }

				reg.SetValue("UserId", txtID.Text.Trim());
				reg.SetValue("SaveYn", chkRemember.Checked.ToString() == "True" ? "Y" : "N");

                string font = Convert.ToString(reg.GetValue("Font"));

                if (font == "")
                {
                    reg.SetValue("Font", "Pretendard SemiBold");
                    reg.SetValue("Size", 10.2);
                }

                reg.Close();
            }
            catch (Exception ex)
            {

            }
        }

		private void Btn_Setting_Click(object sender, EventArgs e)
		{
            Frm_SettingConntionDB setting = new Frm_SettingConntionDB();
			setting.FormClosed += Setting_FormClosed;
            setting.StartPosition = FormStartPosition.CenterParent;
            setting.ShowDialog(this);   // 부모 앞에 표시

            ReadRegValue();
            ReadRegConInfo();
            Change_Logo();

            SetInit();
        }

		private void Setting_FormClosed(object sender, FormClosedEventArgs e)
		{
		}

		private void Change_Logo()
        {

            try
            {
                if (Database.DbName != "")
                {
                    if (Convert.ToString(cboCompany.EditValue) == "") return;

                    clsCode code = new clsCode();

                    DataRow dr = code.GetCorpLogoImg(WrGlobal.CorpID);

                    if(dr != null)
					{
						pictureEdit1.EditValue = dr["LogoImg"] == DBNull.Value ? Properties.Resources.SFLOGO : dr["LogoImg"];   //Properties.Resources.SFLogo
                        pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;             // 크기에 맞춰서 확대
                    }
                    else
					{
                        pictureEdit1.EditValue = Properties.Resources.SFLOGO;
                    }
				}
				else
				{
                    pictureEdit1.EditValue = Properties.Resources.SFLOGO;
				}
            }
            catch { }
        }

    }
}
