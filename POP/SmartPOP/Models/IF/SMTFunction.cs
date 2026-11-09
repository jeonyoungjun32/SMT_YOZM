using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Net.NetworkInformation;

namespace SmartPOP.Models.IF
{
    class SMTFunction
    {
        /// <summary>
        /// SHA-2 256 암호화
        /// </summary>
        /// <param name="argData"></param>
        public static string sha256(string argData)
        {
            SHA256Managed crypt = new SHA256Managed();
            string hash = String.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(argData), 0, Encoding.ASCII.GetByteCount(argData));
            foreach (byte bit in crypto)
            {
                hash += bit.ToString("x2");
            }
            return hash;
        }

        /// <summary>
        /// Success or Fail Result
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetResult(string type)
        {
            string ret = String.Empty;

            switch (type.ToLower())
            {
                case "success":
                    ret = "0";
                    break;

                case "fail":
                    ret = "1";
                    break;
            }

            return ret;
        }

        /// <summary>
        /// 딜레이 함수
        /// </summary>
        /// <param name="MS"></param>
        /// <returns></returns>
        public static DateTime Delay(int MS)
        {
            DateTime ThisMoment = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);
            DateTime AfterWards = ThisMoment.Add(duration);

            while (AfterWards >= ThisMoment)
            {
                System.Windows.Forms.Application.DoEvents();
                ThisMoment = DateTime.Now;
            }

            return DateTime.Now;
        }

        /// <summary>
        /// User Start Time
        /// </summary>
        /// <returns></returns>
        public static string SetUserStart()
        {
            string strReturn = DateTime.Now.ToString("yyyy-MM-dd 00:00:00.000");

            return strReturn;
        }

        /// <summary>
        /// User End Time
        /// </summary>
        /// <returns></returns>
        public static string SetUserEnd()
        {
            string strReturn = DateTime.Now.ToString("yyyy-MM-dd 23:59:59.000");

            return strReturn;
        }

        /// <summary>
        /// SHA-2 512 암호화
        /// </summary>
        /// <param name="argData"></param>
        private static string sha512(string argData)
        {
            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] hashValue;
            byte[] message = UE.GetBytes(argData);
            SHA512Managed hashString = new SHA512Managed();
            string encodedData = Convert.ToBase64String(message);
            string hex = "";
            hashValue = hashString.ComputeHash(UE.GetBytes(encodedData));

            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;
        }

        /// <summary>
        /// 문자 -> byte[] 변환 (ex : L, P J, 등)
        /// </summary>
        /// <param name="getText"></param>
        /// <returns></returns>
        public static byte[] TextToByteArray(String getText)
        {
            byte[] StrByte = Encoding.UTF8.GetBytes(getText); 

            return StrByte;
        }

        /// <summary>
        /// 16진수 -> byte[] 변환 (ex : 0x00)
        /// </summary>
        /// <param name="hex"></param>
        public static byte[] StringToByteArray(String getHex)
        {
            int NumberChars = getHex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(getHex.Substring(i, 2), 16);
            return bytes;
        }

        /// <summary>
        /// byte[] -> 16진수 변환
        /// </summary>
        /// <param name="ba"></param>
        public static string ByteArrayToString(byte[] getByte)
        {
            StringBuilder hex = new StringBuilder(getByte.Length * 2);
            foreach (byte b in getByte)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        /// <summary>
        /// 문자열 16진수 -> 문자로 변환하여 문자열로 반환
        /// </summary>
        /// <param name="strCompany"></param>
        /// <returns></returns>
        public static string CharToString(string getString)
        {
            string result = "";
            int count = 0;

            if (getString != "")
            {
                for (int i = 0; i < getString.Length; i++)
                {
                    if (i % 2 == 0)
                    {
                        result += (char)Convert.ToInt32(getString.Substring(i, 2), 16);
                        count++;
                    }
                }

                return result;
            }
            else
                return "";
        }

        /// <summary>
        /// Hex데이터 1바이트씩 역순으로 스왑
        /// </summary>
        /// <param name="swap"></param>
        /// <returns></returns>
        public static string ReverseToSwap(string swap)
        {
            string result = "";

            if (swap != "")
            {
                // 전체 길이에서 역순으로 데이터 저장
                for (int i = swap.Length; i > 0; i--)
                {
                    if (i % 2 != 0)
                        result += swap.Substring(i - 1, 2);
                }
            }
            return result;
        }

        /// <summary>
        /// ms까지 시간을 구하는 함수
        /// </summary>
        /// <returns></returns>
        public static string GetDateTime()
        {
            DateTime NowDate = DateTime.Now;
            return NowDate.ToString("yyyy-MM-dd HH:mm:ss") + ":" + NowDate.Millisecond.ToString("000");
        }

        /// <summary>
        /// 로그 기록
        /// </summary>
        /// <param name="str">로그내용
        public static void Log(string str)
        {
            string FilePath = Application.StartupPath + @"\Logs\Log" + DateTime.Today.ToString("yyyyMMdd") + ".log";
            string DirPath = Application.StartupPath + @"\Logs";
            string temp;

            DirectoryInfo di = new DirectoryInfo(DirPath);
            FileInfo fi = new FileInfo(FilePath);

            try
            {
                if (di.Exists != true) Directory.CreateDirectory(DirPath);

                if (fi.Exists != true)
                {
                    using (StreamWriter sw = new StreamWriter(FilePath))
                    {
                        temp = string.Format("[{0}] : {1}", GetDateTime(), str);
                        sw.WriteLine(temp);
                        sw.Close();
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(FilePath))
                    {
                        temp = string.Format("[{0}] : {1}", GetDateTime(), str);
                        sw.WriteLine(temp);
                        sw.Close();
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.WriteLine("Log Error - " + e.Message);
            }
        }

        public static void Log(string type, string str)
        {
            string FilePath = Application.StartupPath + @"\Logs\" + type +"Log" + DateTime.Today.ToString("yyyyMMdd") + ".log";
            string DirPath = Application.StartupPath + @"\Logs";
            string temp;

            DirectoryInfo di = new DirectoryInfo(DirPath);
            FileInfo fi = new FileInfo(FilePath);

            try
            {
                if (di.Exists != true) Directory.CreateDirectory(DirPath);

                if (fi.Exists != true)
                {
                    using (StreamWriter sw = new StreamWriter(FilePath))
                    {
                        temp = string.Format("[{0}] : {1}", GetDateTime(), str);
                        sw.WriteLine(temp);
                        sw.Close();
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(FilePath))
                    {
                        temp = string.Format("[{0}] : {1}", GetDateTime(), str);
                        sw.WriteLine(temp);
                        sw.Close();
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.WriteLine("Log Error - " + e.Message);
            }
        }

        /// <summary>
        /// 문자열이 Null, Empty 인지를 확인하여 True(Null), False를 반환한다.
        /// </summary>
        /// <param name="sString">문자열</param>
        public static bool IsNull(string sString)
        {
            if (String.IsNullOrEmpty(sString))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Registry 생성 및 값 가져오기
        /// 0. DBConnect IP, 1. WOConnect IP, 2. PLC IP (SocketInit)
        /// </summary>
        public static string[] CreateRegistry()
        {
            string m_Text;
            string strSubkey = "SMT Sofrware\\IP Information";
            string[] strValue = new string[] { "SMTDB_IP", "BIZMES_IP", "PLC_IP" };
            string[] strData = new string[] { "211.193.41.58,2433", "jinsung-ed.iptime.org,2433", "192.168.0.99" };
            string[] result = new string[strValue.Length];

            try
            {
                // 상위키와 하위키 존재 검색
                RegistryKey rkey = Registry.CurrentUser.OpenSubKey(strSubkey, true);

                for (int i = 0; i < strValue.Length; i++)
                {
                    if (rkey != null)
                    {
                        m_Text = Convert.ToString(rkey.GetValue(strValue[i], ""));

                        // value의 데이터 값 있는지 확인
                        if (m_Text == "")
                        {
                            // value 값 생성
                            rkey.SetValue(strValue[i], strData[i], RegistryValueKind.String);
                        }
                    }
                    else
                    {
                        // 상위키와 하위키 생성
                        rkey = Registry.CurrentUser.CreateSubKey(strSubkey);
                        // value 값 생성
                        rkey.SetValue(strValue[i], strData[i], RegistryValueKind.String);
                    }

                    result[i] = rkey.GetValue(strValue[i]).ToString();
                }
                    
                rkey.Close(); // 레지스트리 닫기
            }
            catch(Exception ex)
            {
                Log("CreateRegistry - " + ex.Message);
            }

            return result;
        }

        /// <summary>
        /// 윈도우 시작 시 프로그램 시작 - 생성
        /// </summary>
        /// <param name="programName">프로그램 명</param>
        /// <returns></returns>
        public static bool CreateStartFromWindows(string programName)
        {
            bool Result = false;

            using (RegistryKey rk = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true))
            {
                try
                { 
                    //레지스트리 등록... 
                    if (rk.GetValue(programName) == null) 
                    { 
                        rk.SetValue(programName, Application.ExecutablePath.ToString());
                        Result = true;
                    }
                    else
                    {
                        Log("CreateStartFromWindows - 이미 존재하는 프로그램 입니다.");
                        Result = false;
                    }

                    //레지스트리 닫기... 
                    rk.Close(); 
                } 
                catch (Exception ex) 
                {
                    Log("CreateStartFromWindows - " + ex.Message);
                } 
            }
            return Result;
        }

        /// <summary>
        /// 윈도우 시작 시 프로그램 시작 - 삭제
        /// </summary>
        /// <param name="programName">프로그램 명</param>
        /// <returns></returns>
        public static bool DeleteStartFromWindows(string programName)
        {
            bool result = false;

            using (RegistryKey rk = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true))
            {
                try
                {
                    //레지스트리 삭제 
                    if (rk.GetValue(programName) != null)
                    {
                        rk.DeleteValue(programName, false);
                        result = true;
                    }
                    else
                    {
                        Log("DeleteStartFromWindows - 이미 삭제 하였습니다");
                        result = false;
                    }

                    //레지스트리 닫기... 
                    rk.Close();
                }
                catch (Exception ex)
                {
                    Log("DeleteStartFromWindows - " + ex.Message);
                }
            }

            return result;
        }

        /// <summary>
        /// STX에서 ETX까지의 XOR합산로 BCC코드 생성
        /// </summary>
        /// <param name="inputStream"></param>
        /// <returns></returns>
        public static byte GetBCC(byte[] inputStream)
        {
            byte bcc = 0;

            if (inputStream != null && inputStream.Length > 0)
            {
                // Exclude SOH during BCC calculation
                for (int i = 0; i < inputStream.Length; i++)
                {
                    bcc ^= inputStream[i];
                }
            }

            return bcc;
        }

        private readonly static ushort[] wCRCTable =
        {
            0X0000, 0XC0C1, 0XC181, 0X0140, 0XC301, 0X03C0, 0X0280, 0XC241, 0XC601, 0X06C0,
            0X0780, 0XC741, 0X0500, 0XC5C1, 0XC481, 0X0440, 0XCC01, 0X0CC0, 0X0D80, 0XCD41,
            0X0F00, 0XCFC1, 0XCE81, 0X0E40, 0X0A00, 0XCAC1, 0XCB81, 0X0B40, 0XC901, 0X09C0,
            0X0880, 0XC841, 0XD801, 0X18C0, 0X1980, 0XD941, 0X1B00, 0XDBC1, 0XDA81, 0X1A40,
            0X1E00, 0XDEC1, 0XDF81, 0X1F40, 0XDD01, 0X1DC0, 0X1C80, 0XDC41, 0X1400, 0XD4C1,
            0XD581, 0X1540, 0XD701, 0X17C0, 0X1680, 0XD641, 0XD201, 0X12C0, 0X1380, 0XD341,
            0X1100, 0XD1C1, 0XD081, 0X1040, 0XF001, 0X30C0, 0X3180, 0XF141, 0X3300, 0XF3C1,
            0XF281, 0X3240, 0X3600, 0XF6C1, 0XF781, 0X3740, 0XF501, 0X35C0, 0X3480, 0XF441,
            0X3C00, 0XFCC1, 0XFD81, 0X3D40, 0XFF01, 0X3FC0, 0X3E80, 0XFE41, 0XFA01, 0X3AC0,
            0X3B80, 0XFB41, 0X3900, 0XF9C1, 0XF881, 0X3840, 0X2800, 0XE8C1, 0XE981, 0X2940,
            0XEB01, 0X2BC0, 0X2A80, 0XEA41, 0XEE01, 0X2EC0, 0X2F80, 0XEF41, 0X2D00, 0XEDC1,
            0XEC81, 0X2C40, 0XE401, 0X24C0, 0X2580, 0XE541, 0X2700, 0XE7C1, 0XE681, 0X2640,
            0X2200, 0XE2C1, 0XE381, 0X2340, 0XE101, 0X21C0, 0X2080, 0XE041, 0XA001, 0X60C0,
            0X6180, 0XA141, 0X6300, 0XA3C1, 0XA281, 0X6240, 0X6600, 0XA6C1, 0XA781, 0X6740,
            0XA501, 0X65C0, 0X6480, 0XA441, 0X6C00, 0XACC1, 0XAD81, 0X6D40, 0XAF01, 0X6FC0,
            0X6E80, 0XAE41, 0XAA01, 0X6AC0, 0X6B80, 0XAB41, 0X6900, 0XA9C1, 0XA881, 0X6840,
            0X7800, 0XB8C1, 0XB981, 0X7940, 0XBB01, 0X7BC0, 0X7A80, 0XBA41, 0XBE01, 0X7EC0,
            0X7F80, 0XBF41, 0X7D00, 0XBDC1, 0XBC81, 0X7C40, 0XB401, 0X74C0, 0X7580, 0XB541,
            0X7700, 0XB7C1, 0XB681, 0X7640, 0X7200, 0XB2C1, 0XB381, 0X7340, 0XB101, 0X71C0,
            0X7080, 0XB041, 0X5000, 0X90C1, 0X9181, 0X5140, 0X9301, 0X53C0, 0X5280, 0X9241,
            0X9601, 0X56C0, 0X5780, 0X9741, 0X5500, 0X95C1, 0X9481, 0X5440, 0X9C01, 0X5CC0,
            0X5D80, 0X9D41, 0X5F00, 0X9FC1, 0X9E81, 0X5E40, 0X5A00, 0X9AC1, 0X9B81, 0X5B40,
            0X9901, 0X59C0, 0X5880, 0X9841, 0X8801, 0X48C0, 0X4980, 0X8941, 0X4B00, 0X8BC1,
            0X8A81, 0X4A40, 0X4E00, 0X8EC1, 0X8F81, 0X4F40, 0X8D01, 0X4DC0, 0X4C80, 0X8C41,
            0X4400, 0X84C1, 0X8581, 0X4540, 0X8701, 0X47C0, 0X4680, 0X8641, 0X8201, 0X42C0,
            0X4380, 0X8341, 0X4100, 0X81C1, 0X8081, 0X4040
        };

        public static string MakeCRC16(string data)
        {
            string result;
            ushort icrc = 0xFFFF;
            byte[] bytes = StringToByteArray(data);

            for (int i = 0; i < bytes.Length; i++)
            {
                icrc = Convert.ToUInt16((icrc >> 8) ^ wCRCTable[(icrc ^ bytes[i]) & 0xff]);
            }
            result = data + ByteArrayToString(BitConverter.GetBytes(icrc));

            return result;
        }

        public static bool IsEstablishedNetwork()
        {
            bool networkUp = NetworkInterface.GetIsNetworkAvailable();
            bool pingResult = false;

            try
            {
                if (networkUp)
                {
                    string addr = "smtv.iptime.org";

                    if (string.IsNullOrEmpty(addr))
                    {
                        pingResult = true;
                    }
                    else
                    {
                        Ping pingSender = new Ping();
                        PingReply reply = pingSender.Send(addr, 300);
                        pingResult = reply.Status == IPStatus.Success;
                    }
                }
            }
            catch(Exception ex)
            {
                Log("IsEstablishedNetwork - " + ex.Message);
            }
            
            return networkUp & pingResult;
        }
    }

    class WinAPI
    {
        public const int ACTIVATE = 0X00020000;
        public const int HOR_Positive = 0X00000001;
        public const int HOR_NEGATIVE = 0X00000002;
        public const int VER_POSITIVE = 0X00000004;
        public const int VER_NEGATIVE = 0X00000008;
        public const int CENTER = 0X00000010;
        public const int BLEND = 0X00080000;
        public const int HIDE = 0X00010000;
        public const int SLIDE = 0X00040000;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int AnimateWindow(IntPtr hwand, int dwTime, int dwFlag);
    }
}
