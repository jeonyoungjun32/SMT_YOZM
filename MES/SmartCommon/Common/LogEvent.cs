using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Xml;
using System.Web;
using SmartCommon.Models.IF;

namespace SmartCommon.Common
{
    public class LogEvent
    {
        string APIuri = "https://log.smart-factory.kr/apisvc/sendLogDataXML.do";  // 웹서비스 주소

        string APIString = string.Empty;

        string rtn_code = string.Empty;     //수신 결과 코드
        string rtn_rslt = string.Empty;     //수신 결과 메시지
        string rtn_msge = string.Empty;     //수신 결과 상세 메시지
        string rtn_date = string.Empty;     //수신 일시

        public void CrudEvent(string UserUse, string Query)
        {

            string UserID = WrGlobal.LoginUserNM;

            string dateWork = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.sss");
            string ConectIp = GetExternalIPAddress();
            string DataUsg = Query.Length.ToString();

            //업체별 API
            //switch (Database.DbName.ToUpper())
            //{
            //    case "SEJONGRMC": APIString = "$5$API$AEPeMmmldkirmC3Y2NYxNDwHki55g1xpnAUVHhEXcd7"; break;  //세종레미콘
            //    case "ALLTHEN": APIString   = "$5$API$r/YhkkKVWJ7OmoUUriYsaMZsvWQB1Mdz1W9Y8hnTGl8"; break;  //올덴
                //case "TPG":
                APIString       = "$5$API$AEPeMmmldkirmC3Y2NYxNDwHki55g1xpnAUVHhEXcd7";
            //break;  //티피지
                //case "JSG": APIString       = "$5$API$p0i/yo/5pQ9okM034H.FwaYCaDxitErOCQlQ/ydfrg1"; break;  //제이에스글로벌
            //}

            if (APIString == "") return;

            string XMLdata = "<logData>" +
                             "<crtfcKey>" + APIString + "</crtfcKey>" +
                             "<logDt>" + dateWork + "</logDt>" +
                             "<useSe>" + UserUse + "</useSe>" +
                             "<sysUser>" + UserID + "</sysUser>" +
                             "<conectIp>" + ConectIp + "</conectIp>" +
                             "<dataUsgqty>" + DataUsg + "</dataUsgqty>" +
                             "</logData>";

            string sendData = "logData";

            System.Diagnostics.Debug.WriteLine(XMLdata);

            string[,] msg = new String[1, 2] { { sendData, XMLdata } }; // 전송할 Parameter
            string encodeStr = "EUC-KR";                                          // 인코딩 방식
            int errorcode = 0;                                                     // 에러 전달받을 값

            try
            {
                string returnVal = GetHttpPOST(sendData, XMLdata, APIuri, encodeStr, ref errorcode);

                System.Diagnostics.Debug.WriteLine(returnVal);

                string return_string = returnMsg(rtn_code);

                System.Diagnostics.Debug.WriteLine(rtn_code + ":" + return_string);

                IF_ApiHistory api = new IF_ApiHistory();

                api.crtfcKey = APIString;
                api.useSe = UserUse;
                api.sysUser = UserID;
                api.conectIp = ConectIp;
                api.dataUsgqty = DataUsg;
                api.SendResulrt = return_string;

                api.Ins_API_History();

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        private string returnMsg(string returnVal)
        {
            string retvalue = string.Empty;

            switch (returnVal)
            {
                case "AP1001": retvalue = "데이터 수신 완료"; break;
                case "AP1002": retvalue = "데이터 이관 완료"; break;
                case "AP1003": retvalue = "데이터 이관 완료 오류"; break;
                case "AP1004": retvalue = "데이터 수신 오류"; break;
                case "AP1010": retvalue = "기타 오류"; break;
                case "AP1011": retvalue = "기타 오류 (API 인증키 데이터 없음)"; break;
                case "AP1012": retvalue = "기타 오류 (로그 일시 데이터 없음)"; break;
                case "AP1013": retvalue = "기타 오류 (로그 일시 데이터 길이 오류)"; break;
                case "AP1014": retvalue = "기타 오류 (로그 일시 데이터가 지정된 날짜 형식이 아님)"; break;
                case "AP1015": retvalue = "기타 오류 (사용 구분 데이터 없음)"; break;
                case "AP1016": retvalue = "기타 오류 (사용 구분 데이터 길이가 200자 이상)"; break;
                case "AP1017": retvalue = "기타 오류 (시스템 사용자 데이터 없음)"; break;
                case "AP1018": retvalue = "기타 오류 (시스템 사용자 데이터 길이가 60자 이상)"; break;
                case "AP1019": retvalue = "기타 오류 (접속 IP 데이터 없음)"; break;
                case "AP1020": retvalue = "기타 오류 (접속 IP 데이터 길이가 30자 이상)"; break;
                case "AP1021": retvalue = "기타 오류 (데이터 사용량 데이터가 숫자가 아님)"; break;
                case "AP1022": retvalue = "기타 오류 (데이터 사용량 데이터가 공백)"; break;
                case "AP1023": retvalue = "기타 오류 (JSON 데이터 형식 오류)"; break;
                case "AP1024": retvalue = "기타 오류 (XML 데이터 형식 오류)"; break;
                case "AP1025": retvalue = "기타 오류 (데이터 전송 오류)"; break;
                case "AP1026": retvalue = "기타 오류 (데이터 사용량 데이터 길이가 10자 이상)"; break;
                case "AP1027": retvalue = "기타 오류 (로그 일시 데이터 오류)"; break;
                case "AP1028": retvalue = "TEST DATA 체크 완료"; break;
                case "AP1029": retvalue = "전송완료(API 전송주기)"; break;
                case "AP1030": retvalue = "기타오류(LOG수집 미대상 오류)"; break;

                default: retvalue = "기타 오류"; break;
            }

            return retvalue;

        }


        private string GetHttpPOST(string sendstr, string XMLdata, string url, string encode, ref int errcode)
        {
            string retValue = string.Empty;

            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.Method = "POST";
                Stream requestStream = null;
                byte[] sendData = null;
                if (XMLdata != null)
                {
                    string postMsg = sendstr + "=" + HttpUtility.UrlEncode(XMLdata, Encoding.GetEncoding(encode));

                    if (String.IsNullOrEmpty(encode) || encode == "UTF-8")
                    {
                        sendData = UTF8Encoding.UTF8.GetBytes(postMsg);
                        httpWebRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                    }
                    else if (encode == "EUC-KR")
                    {
                        sendData = Encoding.GetEncoding("EUC-KR").GetBytes(postMsg);
                        httpWebRequest.ContentType = "application/x-www-form-urlencoded; charset=EUC-KR";
                    }
                    else
                    {
                        sendData = Encoding.GetEncoding("ks_c_5601-1987").GetBytes(postMsg);
                        httpWebRequest.ContentType = "application/x-www-form-urlencoded; charset=ks_c_5601-1987";
                    }
                    httpWebRequest.ContentLength = sendData.Length;
                    requestStream = httpWebRequest.GetRequestStream();
                    requestStream.Write(sendData, 0, sendData.Length);
                }
                else
                {
                    httpWebRequest.ContentLength = 0;
                    requestStream = httpWebRequest.GetRequestStream();
                }
                requestStream.Flush();
                requestStream.Close();

                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.GetEncoding(encode));
                retValue = streamReader.ReadToEnd();
                streamReader.Close();
                httpWebResponse.Close();
            }
            catch (WebException ex)
            {
                if (ex.Response is HttpWebResponse)
                {
                    errcode = (int)((HttpWebResponse)ex.Response).StatusCode;
                    retValue = ex.Message;
                }
                else
                {
                    retValue = ex.Message;
                    errcode = -1;
                }
            }

            //리턴값을 컬럼별로 분리
            returnXML(retValue);

            return retValue;
        }

        private void returnXML(string rtn_string)
        {

            string retvalue = string.Empty;

            //< Result xmlns = "" >< recptnDt > 2023 - 01 - 04 13:26:23.762 </ recptnDt >< recptnRsltCd > AP1002 </ recptnRsltCd >< recptnRslt >? 곗댄?? 닿 ? ?猷</ recptnRslt >< recptnRsltDtl ></ recptnRsltDtl ></ Result >:

            // XML 형식의 결과값을 파싱하여 컬럼별로 분리
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(rtn_string);
            XmlNodeList xnList = xml.GetElementsByTagName("Result");

            foreach (XmlNode xn in xnList)
            {
                rtn_code = xn["recptnRsltCd"].InnerText;
                rtn_rslt = xn["recptnRslt"].InnerText;
                rtn_msge = xn["recptnRsltDtl"].InnerText;
                rtn_date = xn["recptnDt"].InnerText;
            }

        }

        private string GetExternalIPAddress()
        {
            try
            {
                string externalIp = new WebClient().DownloadString("http://ipinfo.io/ip").Trim();
                return externalIp;
            }
            catch (WebException ex)
            {
                return GetInternalIPAddress();//null경우 Get Internal IP를 가져오게 한다.
            }
        }

        private string GetInternalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                { return ip.ToString(); }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }


        public string SelectSM_UserLogin()
        {
            //Data 사용량 확인용으로 사용 됨
            string query = $@"  SELECT 
		                                e.PassWord
		                                , e.EmployeeName
		                                , e.AuthorGroup
		                                , d.DeptName
		                                , c.CorpName, c.CorpNo
		                                , '' AS CorpRegNo
		                                , c.PresiName 
		                                , '' AS PostNo
		                                , c.Address
		                                , Tel, Fax
                                FROM SM_User e
                                LEFT JOIN SM_Corp c on c.CorpId = e.CorpId
                                LEFT JOIN CodeDept d on d.DeptCode = e.AuthorGroup
                                WHERE e.CorpId = '{WrGlobal.CorpID}'
                                  AND e.EmployeeId = '{WrGlobal.LoginID}' ";
            return query;
        }

    }
}
