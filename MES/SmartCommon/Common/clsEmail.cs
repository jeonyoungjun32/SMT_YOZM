using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SmartCommon.Common
{

    public class clsEMail
    {
        private string _sendAddress = null;
        private string _sendNic = null;
        private string _sendPassword = "";//  // EMail Password  
        private string _sendUser = "";//          // EMail ID   

        public clsEMail()
        {

        }

        //public clsEMail(string sendMail, string sendNic)
        //{
        //    _sendAddress = sendMail;
        //    _sendNic = sendNic;
        //}

        public clsEMail(string sendUser, string sendPW, string sendMail, string sendNic)
        {
            _sendUser = sendUser;
            _sendPassword = sendPW;
            _sendAddress = sendMail;
            _sendNic = sendNic == "" ? _sendUser : sendNic;
        }

        public string SendEMail(string subject, string body, List<string> toSend, List<string> toReg, List<string> toHide)
        {
            SmtpClient smtp = null;
            MailMessage message = null;

            try
            {
                // Naver 계정 SMTP 설정 > 
                smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";  //"smtp.naver.com";
                smtp.UseDefaultCredentials = false;
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Timeout = 20000;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(_sendUser, _sendPassword);

                //message = new MailMessage();

                message = new MailMessage();  // from address를 따로 넣으려면 어떻게 해야 하나?

                message.From = new MailAddress(_sendAddress, _sendNic, System.Text.Encoding.UTF8);

                if (toSend.Count > 0)
                {
                    foreach (string strAddress in toSend)
                    {
                        message.To.Add(new MailAddress(strAddress)); // 앞에서 정규식 체크 하고 넘어 왔음으로 그냥 사용
                    }
                }

                if (toReg.Count > 0)
                {
                    foreach (string strAddress in toReg)
                    {
                        message.CC.Add(new MailAddress(strAddress)); // 앞에서 정규식 체크 하고 넘어 왔음으로 그냥 사용
                    }
                }

                if (toHide.Count > 0)
                {
                    foreach (string strAddress in toHide)
                    {
                        message.Bcc.Add(new MailAddress(strAddress)); // 앞에서 정규식 체크 하고 넘어 왔음으로 그냥 사용
                    }
                }

                /* 기타 설정들
                    message.To.Add("aaa@naver.com");   // 받는 사람
                    message.CC.Add("aaa@naver.com");   // 참조
                    message.Bcc.Add("aaa@naver.com");   // 비공개
                    message.IsBodyHtml = false;  // Body Format
                    message.Attachments.Add(new Attachment(new FileStream(@"D:\test.zip", FileMode.Open, FileAccess.Read), "test.zip"));  // File 첨부
                    */

                message.Subject = subject;
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;  // Body Format
                message.Body = body;
                message.BodyEncoding = System.Text.Encoding.UTF8;

                //message.Attachments.Add(new Attachment(new FileStream(@"C:\temp\견적서.pdf", FileMode.Open, FileAccess.Read), "견적서.pdf"));  // File 첨부


                smtp.Send(message);
                return "Mail 전송";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            finally
            {
                if (smtp != null) smtp.Dispose();
                if (message != null) message.Dispose();
            }
        }

        public string SendEMail(string subject, string body, List<string> toSend, List<string> toReg, List<string> toHide, List<string>filePath)
        {
            SmtpClient smtp = null;
            MailMessage message = null;

            try
            {
                // Naver 계정 SMTP 설정 > 
                smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";  //"smtp.naver.com";
                smtp.UseDefaultCredentials = false;
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Timeout = 20000;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(_sendUser, _sendPassword);

                //message = new MailMessage();

                message = new MailMessage();  // from address를 따로 넣으려면 어떻게 해야 하나?

                message.From = new MailAddress(_sendAddress, _sendNic, System.Text.Encoding.UTF8);

                if (toSend.Count > 0)
                {
                    foreach (string strAddress in toSend)
                    {
                        message.To.Add(new MailAddress(strAddress)); // 앞에서 정규식 체크 하고 넘어 왔음으로 그냥 사용
                    }
                }

                if (toReg.Count > 0)
                {
                    foreach (string strAddress in toReg)
                    {
                        message.CC.Add(new MailAddress(strAddress)); // 앞에서 정규식 체크 하고 넘어 왔음으로 그냥 사용
                    }
                }

                if (toHide.Count > 0)
                {
                    foreach (string strAddress in toHide)
                    {
                        message.Bcc.Add(new MailAddress(strAddress)); // 앞에서 정규식 체크 하고 넘어 왔음으로 그냥 사용
                    }
                }

                /* 기타 설정들
                    message.To.Add("aaa@naver.com");   // 받는 사람
                    message.CC.Add("aaa@naver.com");   // 참조
                    message.Bcc.Add("aaa@naver.com");   // 비공개
                    message.IsBodyHtml = false;  // Body Format
                    message.Attachments.Add(new Attachment(new FileStream(@"D:\test.zip", FileMode.Open, FileAccess.Read), "test.zip"));  // File 첨부
                    */

                message.Subject = subject;
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;  // Body Format
                message.Body = body;
                message.BodyEncoding = System.Text.Encoding.UTF8;

				foreach (string path in filePath)
				{
                    if (File.Exists(path))
					{
                        message.Attachments.Add(new Attachment(path));
                    }
				}

				smtp.Send(message);
                return "Mail 전송";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            finally
            {
                if (smtp != null) smtp.Dispose();
                if (message != null) message.Dispose();
            }
        }

    }
}
