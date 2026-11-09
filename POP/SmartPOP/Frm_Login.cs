using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;

using SmartPOP.Models;
using SmartPOP.Common;
using Microsoft.Win32;
using SMT_MES.Controls;
using SmartPOP.Models.SM;
using DevExpress.XtraEditors;

namespace SmartPOP
{
    public partial class Frm_Login : DevExpress.XtraEditors.XtraForm
    {

        Database db = new Database();
        string query = "";
        clsSystem sys = new clsSystem();

        public Frm_Login()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            Change_Logo();

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
                txtID.Text = drv["EmployeeID"].ToString();
                txtPW.Text = "";
                chkRemember.Checked = true;
            }
			txtPW.Focus();
		}

		private void Change_Logo()
		{
			try
			{
				if (Convert.ToString(cboCompany.EditValue) == "") return;

				clsCode code = new clsCode();

				DataRow dr = code.GetCorpLogoImg(WrGlobal.CorpID);

				if (dr != null)
				{
					pictureEdit1.EditValue = dr["LogoImg"] == DBNull.Value ? Properties.Resources.SF로고원본ai파일_투명_ : dr["LogoImg"];   //Properties.Resources.SFLogo
					pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;             // 크기에 맞춰서 확대
				}
				else
				{
					pictureEdit1.EditValue = Properties.Resources.SF로고원본ai파일_투명_;
				}
			}
			catch { }
		}

		// 로그인
		private void btnLogin_Click(object sender, EventArgs e)
        {
            SetLogin();
        }

        private void SetLogin()
        {
            if (txtID.Text == "")
            {
                cls_MessageBox.Show("ID를 입력해주세요");
            }
            else if (txtPW.Text == "")
            {
                cls_MessageBox.Show("PASSWORD를 입력해주세요");
            }

            bool success = LoginProcess(cboCompany.EditValue.ToString(), txtID.Text, txtPW.Text);

            if (success)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                cls_MessageBox.Show("ID 또는 비밀번호가 틀렸습니다.");
            }
        }

        public bool LoginProcess(string corpId, string id, string pw, bool isAutoLogin = false)
        {
            if (isAutoLogin)
            {
                query = $@"exec Employee_GetUserInfo_AutoLogin '{corpId}', '{id}'";
            }
            else
            {
                query = $@"exec Employee_GetUserInfo '{corpId}', '{id}', '{pw}'";
            }

            DataRowView drv = db.GetDataRecord(query);

            if (drv == null)
            {
                return false;
            }

            // 로그인 성공
            WrGlobal.CorpID = corpId;
            WrGlobal.CorpName = cboCompany.Text.ToString();
            WrGlobal.LoginID = id;
            WrGlobal.LoginUserNM = drv["EmployeeName"].ToString();
            WrGlobal.DeptCD = drv["DeptCode"].ToString();
            WrGlobal.DeptName = drv["DeptName"].ToString();
            WrGlobal.Title = drv["JobTitle"].ToString();

            WrGlobal.Email = drv["Email"].ToString();
            WrGlobal.CustYn = drv["CustYn"].ToString();
            WrGlobal.CustCode = drv["CustCode"].ToString();
            WrGlobal.CustType = Convert.ToString(drv["CustType"]);

            // 권한 세팅
            sys.SetUserAuthor();

            // 세션 설정
            sys.CreateSession();

            CreateWrGlobal(drv.Row);

            SetAutoLogin();

            // 접속 이력
            LogEvent LogEvent = new LogEvent();
            LogEvent.CrudEvent("접속", LogEvent.SelectCodeUserLogin());

            return true;
        }

        //private void InsertLoGInOutHistory()
        //{
        //    //로그인이력 입력

        //    SC_LoginOut_History loginOut = new SC_LoginOut_History();

        //    loginOut.ID = WrGlobal.LoginID;
        //    loginOut.Login_Time = DateTime.Now;
        //    loginOut.Login_IP = WrGlobal.IpAddress;

        //    loginOut.Ins_LoginOut_History();

        //    WrGlobal.LoginTime = loginOut.Login_Time;
        //}

        private void CreateWrGlobal(DataRow drUser)
        {
            //Global데이터 입력
            //WrGlobal.CorpID = drUser["CorpId"].ToString();         //사업장번호
            //WrGlobal.CorpName = drUser["CorpName"].ToString();     //사업장명
            //WrGlobal.LoginID = drUser["EmployeeID"].ToString();    //ID
            //WrGlobal.LoginUserNM = drUser["EmployeeName"].ToString();    //사용자명
            //WrGlobal.m_ftpFolderPath = Database.DbName;             //FTP폴더 -> WrGlobal에 public static string m_ftpFolderPath = Database.DbName; 

            //Process Option  호출
            try
            {
                SM_SystemOption procOption = new SM_SystemOption();
                DataTable dtProcOption = procOption.Select_Switch();

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
                //CodeOption 호출
                SM_CodeOption cdOption = new SM_CodeOption();
                DataTable dtCdOption = cdOption.Select_CdOption();
                foreach (DataRow dr in dtCdOption.Rows)
                {
                    string div = Convert.ToString(dr["DivCd"]);
                    string fieldNm = Convert.ToString(dr["FieldID"]);
                    string caption = Convert.ToString(dr["CaptionNm"]);
                    int disp_Seq = dr["DispSeq"].ConvertString_Null_To_Empty() == "" ? 99 : dr["DispSeq"].ConvertInt_Null_To_Zero();
                    bool disp = Convert.ToString(dr["DispYn"]) == "Y" ? true : false;
                    bool lockYn = Convert.ToString(dr["LockYn"]) == "Y" ? true : false;

                    //Option 목록
                    WrGlobal.CodeOption optionList = new WrGlobal.CodeOption { Nm = caption, Disp_Seq = disp_Seq, DispYn = disp, LockYn = lockYn };

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
            catch (Exception ex) { cls_MessageBox.Show(ex.Message); }
        }

        private void SetAutoLogin()
        {
            //if (chkRemember.Checked)
            //{
            WrGlobal.LoginTime = DateTime.Now;

            query = $@"Insert Into SM_LoginHistory(HostName, CorpID, ID, Login_Time, Login_IP, Login_Project) 
                                               values ('{System.Net.Dns.GetHostName()}', '{cboCompany.EditValue}','{WrGlobal.LoginID}', GETDATE(), '{WrGlobal.IpAddress}', '{WrGlobal.Login_Project}')";
            db.ExecuteQuery(query);
            //}
        }

        private void txtID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtPW.Focus();
        }

        private void txtPW_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SetLogin();
        }

        public void WriteRegValue()
        {
            try
            {
                RegistryKey reg = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\SNEXISPOP", true);

                if (reg == null)
                {
                    reg = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\SNEXISPOP");
                }

                reg.Close();
            }
            catch (Exception ex)
            {

            }
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            WriteRegValue();
        }
    }
}