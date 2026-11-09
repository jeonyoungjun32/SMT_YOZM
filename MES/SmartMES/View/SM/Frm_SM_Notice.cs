using SmartMES.Common;
using SmartMES.Models;
using System;
using System.Data;
using SmartCommon.Common;
using System.Windows.Forms;
using baseSMT;
using SmartMES.Models.SM;

namespace SmartMES.View.SM
{
	public partial class Frm_SM_Notice : DevExpress.XtraEditors.XtraForm
    {
        clsCode code = new clsCode();
        Database db = new Database();

        string m_BoardNo = "";

        public Frm_SM_Notice(string BoardNo = "", bool swEdit = true)
        {
            InitializeComponent();
            btn_Save.Visible = swEdit;
            m_BoardNo = BoardNo;

            if (m_BoardNo == "")
            {
                // 신규셋팅
                Set_NewInfo();
            }
            else
            {
                // 기존정보셋팅
                Set_UpdInfo();

                //공지번호 수정 불가처리
                txt_BoardNo.smtTextBoxReadOnly = true;
            }
        }

        #region 초기셋팅
        private void Set_NewInfo()
        {
            txt_BoardNo.smtTextBoxValue = code.GetBorderNo();       // 공지번호
            WriteDt.smtDateTimeValue = DateTime.Now;                // 작성일자
            StartDt.smtDateTimeValue = DateTime.Now;                // 시작일자
            EndDt.smtDateTimeValue = DateTime.Now;                  // 종료일자
        }

        private void Set_UpdInfo()
        {
            DataRow dr = code.Get_DataRow_Notice(m_BoardNo);
            if (dr == null)
                return;

            txt_BoardNo.smtTextBoxValue = m_BoardNo;                                        // 공지번호
            txt_Subject.smtTextBoxValue = dr["Subject"].ConvertString_Null_To_Empty();          // 제목
            WriteDt.smtDateTimeValue = Convert.ToDateTime(dr["WriteDate"]);                 // 작성일자
            StartDt.smtDateTimeValue = Convert.ToDateTime(dr["StartDate"]);                // 시작일자
            EndDt.smtDateTimeValue = Convert.ToDateTime(dr["EndDate"]);                    // 종료일자
            smtBodyRichEdit1.smtRichEditRtfText = dr["Body"].ConvertString_Null_To_Empty(); // 내용
        }

        #endregion

        #region 버튼이벤트
        private void Btn_Click(object sender, EventArgs e)
        {
            smtSearchButton btn = sender as smtSearchButton;

            string name = btn.smtButtonStyle.ConvertString_Null_To_Empty();

            switch (name)
            {
                case "저장":
                    if (m_BoardNo == "") Ins_Board();   // 신규
                    else Upd_Board();                   // 수정
                    break;

                case "닫기":
                    this.Close();
                    break;

                default:
                    break;
            }
        }

        private bool chk_Save()
        {
            if (txt_Subject.smtTextBoxValue == "")
            {
                MessageBox.Show("제목을 입력해 주십시오", "알림", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void Ins_Board()
        {
            if (!chk_Save()) return;

            SM_Notice nt = new SM_Notice();
            nt.BoardNo = txt_BoardNo.smtTextBoxValue.ConvertString_Null_To_Empty();
            nt.Class = "NON";
            nt.WriteDate = WriteDt.smtDateTimeValue.ToString("yyyy-MM-dd HH:mm:ss");
            nt.Subject = txt_Subject.smtTextBoxValue.ConvertString_Null_To_Empty();
            nt.Body = smtBodyRichEdit1.smtRichEditRtfText.Replace("'", "''");
            nt.StartDate = StartDt.smtDateTimeValue.ToString("yyyy-MM-dd HH:mm:ss");
            nt.EndDate = EndDt.smtDateTimeValue.ToString("yyyy-MM-dd HH:mm:ss");

            if (!nt.Ins_SM_Notice())
            {
                MessageBox.Show("저장 중 오류가 발생했습니다.");
            }

            MessageBox.Show("저장되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void Upd_Board()
        {
            if (!chk_Save()) return;

            SM_Notice nt = new SM_Notice();
            nt.BoardNo = m_BoardNo;
            nt.Subject = txt_Subject.smtTextBoxValue.ConvertString_Null_To_Empty();
            nt.Body = smtBodyRichEdit1.smtRichEditRtfText.Replace("'", "''");
            nt.StartDate = StartDt.smtDateTimeValue.ToString("yyyy-MM-dd HH:mm:ss");
            nt.EndDate = EndDt.smtDateTimeValue.ToString("yyyy-MM-dd HH:mm:ss");

            if (!nt.Upd_SM_Notice())
            {
                MessageBox.Show("저장 중 오류가 발생했습니다.");
            }

            MessageBox.Show("저장되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
        #endregion
    }
}
