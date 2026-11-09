using DevExpress.XtraEditors;
using SmartPOP.Common;
using SmartPOP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartPOP
{
	public partial class Frm_POPMenu : DevExpress.XtraEditors.XtraForm
	{
        clsSystem sys = new clsSystem();

        Database db = new Database();
        string query = "";

        private string FontName = "맑은 고딕";//WindowsFormsSettings.DefaultFont.Name;
		private float Font_Size = 45;



		public Frm_POPMenu()
		{
			InitializeComponent();
		}

		private void frmPOPMenu_Load(object sender, EventArgs e)
		{
			Set_Menu();
		}

        private void Set_Menu()
        {
            query = $@"
            SELECT * 
            FROM SM_MenuGroup
            WHERE ProgramType = 3
            AND GroupLevel = 1
            ";

            DataTable dt = db.GetDataTable(query);

            Create_Btn_Menu(dt); //메뉴 버튼 생성
        }

        private void Create_Btn_Menu(DataTable dt)
        {
            Font font = new Font(FontName, Font_Size, FontStyle.Bold);

            int Col = 0;
            int Row = 0;

            //버튼 생성
            foreach (DataRow dr in dt.Rows)   //대메뉴 중간메뉴 제외 폼만처리 
            {
                Button btn = new Button();

                // 아코디언 메뉴 스타일과 동일하게 ProjectName | FolderName | FormName | ParamText 형태로 Tag 조립
                string projectName = dr.Table.Columns.Contains("ProjectName") ? Convert.ToString(dr["ProjectName"]) : "SmartPOP";
                string folderName = dr.Table.Columns.Contains("FolderName") ? Convert.ToString(dr["FolderName"]) : "";
                string formName = Convert.ToString(dr["FormName"]); // 기존의 FormName 혹은 테이블 컬럼명 확인 필요
                if (string.IsNullOrEmpty(formName))
                {
                    formName = Convert.ToString(dr["FormName"]); // 테이블에 해당 컬럼이 없다면 원래 사용하시던 dr["FormName"] 유지
                }
                string paramText = dr.Table.Columns.Contains("ParamText") ? Convert.ToString(dr["ParamText"]) : "";

                // 특정 예외 처리: 기존 switch문에서 Frm_QM_ExamResult_Print일 때 "OutQC_Out" 파라미터를 던지던 부분을 하드코딩 대응 혹은 DB화
                if (formName == "Frm_QM_ExamResult_Print" && string.IsNullOrEmpty(paramText))
                {
                    paramText = "OutQC_Out";
                }

                btn.Name = "Btn_" + Convert.ToString(dr["Id"]); //버튼명
                btn.Text = Convert.ToString(dr["title_ko"]);         //버튼 표기글자

                // -------------------------------------------------------------
                // [중요] 동적 생성을 위해 4가지 정보를 파이프라인(|) 기호로 묶어서 Tag에 저장합니다.
                // -------------------------------------------------------------
                btn.Tag = $"{projectName}|{folderName}|{formName}|{paramText}";

                btn.Dock = DockStyle.Fill;      //전체 정렬
                btn.Size = new Size(300, 300);   //너비 130, 높이 : 
                btn.Font = new Font(FontName, Font_Size, FontStyle.Bold);
                btn.ForeColor = Color.Black;
                btn.TextAlign = ContentAlignment.MiddleCenter;
                btn.BackgroundImage = Properties.Resources.btn_light01;
                btn.BackgroundImageLayout = ImageLayout.Stretch;

                //상단메뉴 이벤트 설정
                btn.Click += new EventHandler(CallForm);

                //상단 버튼 
                tableLayoutPanel1.Controls.Add(btn, Col, Row);

                Col++;
                if (Col > tableLayoutPanel1.ColumnCount)
                {
                    Col = 1;
                    Row++;
                }
            }
        }

        private void CallForm(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button.Tag == null) return;

            // Tag에서 4가지 정보 분리 (0:ProjectName, 1:FolderName, 2:FormName, 3:ParamText)
            string[] arrTag = button.Tag.ToString().Split(new char[] { '|' });
            if (arrTag.Length < 3) return;

            string projectName = arrTag[0];
            string folderName = arrTag[1];
            string formName = arrTag[2];
            string paramText = arrTag.Length > 3 ? arrTag[3] : "";

            // -------------------------------------------------------------
            // [핵심 변경] 부모 메인폼(Frm_Main) 캐스팅 후 동적 오픈 메서드 호출
            // -------------------------------------------------------------
            if (this.MdiParent is Frm_Main mainForm)
            {
                mainForm.OpenFormDynamic(projectName, folderName, formName, paramText, button.Name, button.Text);
            }
            else
            {
                // 만약 MdiParent가 지정되지 않은 일반 팝업 형태인 경우, Application.OpenForms에서 찾아 호출하는 방안
                Frm_Main mainFormFromOpen = Application.OpenForms["Frm_Main"] as Frm_Main;
                if (mainFormFromOpen != null)
                {
                    mainFormFromOpen.OpenFormDynamic(projectName, folderName, formName, paramText, button.Name, button.Text);
                }
                else
                {
                    MessageBox.Show("메인 폼(Frm_Main)을 찾을 수 없어 화면을 열 수 없습니다.");
                }
            }
        }
    }
}