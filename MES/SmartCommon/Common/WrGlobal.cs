using DevExpress.XtraReports.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace SmartCommon.Common
{
    public static class WrGlobal
    {
        public static ReportStorageExtension reportStorage;
        public static ReportStorageExtension ReportStorage
        {
            get
            {
                return reportStorage;
            }
        }
        //S-MES 에서 추가 
        //Process Option : SC_Switch Table 에서 가져옴 
        public static Dictionary<string, Dictionary<string, string>> ProcOption = new Dictionary<string, Dictionary<string, string>>();

        //Code Option : SM_CodeOption Table에서 가져옴
        public struct CodeOption
        {
            public string Nm;   //GridView Header 표기명
            public int DispSeq; //Column Edit 활성화화 여부(true:수정가능, false:수정불가)
            public bool DispYn; //Column Visible 여부(true:표기, false:숨김)
            public bool LockYn; //Column Edit 활성화화 여부(true:수정가능, false:수정불가)
        }
        public static Dictionary<string, Dictionary<string, CodeOption>> CdOption = new Dictionary<string, Dictionary<string, CodeOption>>();
        //기타
        public static Color EditBgColor = Color.PapayaWhip; //변경허용 Column 배경색

        public static bool GAM_SW = false;  //감리용

        public static string ClientIpAddress = System.Net.Dns.GetHostName();   //클라이언트 HostName
        public static string ClientHostName = System.Net.Dns.GetHostName();   //클라이언트 HostName

        public static string PublicCorpID = "Z000";
        public static string CorpID = "1000";
        public static string CorpName = "";
        public static string LoginID = "";
        public static string LoginUserNM = "";
        public static DateTime LoginTime;                   // 로그인한 시간(로그아웃 시간 입력용)
        public static string Login_Project = "MES";          // 로그인한 프로젝트
        public static string Sabun = "";

        public static string DeptCD = string.Empty;          // 부서코드
        public static string DeptName = string.Empty;        // 부서명칭
        public static string GroupCode = string.Empty;       // 부서명칭
        public static string OrderType = string.Empty;       // 납품오더의 종류

        public static string ItemCode = "";
        public static string ProductName = "";

        public static string Email = "";                     // 이메일
        public static string CustYn = string.Empty;          // 거래처 선택여부

        public static string StoreYn = "N";
        public static string StoreID = string.Empty;        // 소속매장코드

        public static string PassHouseID = "";    // 직출창고
        public static string PassZoneID = "";     // 직출창고존

        public static string Yn = string.Empty;              // 선택 및 취소
        public static DateTime SelectionDate;                // 선택일자
        public static string EmpNo;                          // 승인자
        public static string DeptCode;                       // 승인부서
        public static string CustType;                       // 거래처 구분
        public static string CustCode;                       // 거래처 코드
        public static string CustName;                       // 거래처 명
        public static string ShipToCode;                     // 납품처 코드
        public static string DeliLocation;                   // 남품처명 또는 주소
        public static string MachineCode;                    // 장비 코드
        public static string MachineName;                    // 장비 코드
        public static string PostNo;
        public static string Address;
        public static int PostSeq;
        public static string Message = "";
        public static string HostName = System.Net.Dns.GetHostName();


        public static string Port;                           //포트번호
        public static string IpAddress = System.Net.Dns.GetHostAddresses(HostName).FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToString(); //IP주소
        public static string Title;                          //센서명

        public static string ftpIp = "ftp://smtv.iptime.org:21/FTP";
        public static string ftpUser = "SMT";
        public static string ftpPwd = "@admin9150";

        public static string SessionNo = "";
        public static string ProcessGroup = "";

        public static string Language = "ko";

        public static Color SelectedRowBackColor = Color.LightBlue;
        public static Color SelectedRowForeColor = Color.Black;

        public static string ProgramType = "2";

        public static string ProJectName = "SmartMES";

        public static List<clsAuthor> AuthorList = new List<clsAuthor>();

        public static string[] Business_ItemType = new string[] { };
        public static string[] Product_ItemType = new string[] { };
        public static string[] Material_ItemType = new string[] { };

        public static string DBServer = "smtv.iptime.org,2433";
        public static string DBUserName = "smtv";
        public static string DBUserPassword = "#smtv@9150$";
        public static string DBName = "YOZM";

        public static string OpeningMenuId = "";

        //FTP
        public static string m_ftpServerip = "ftp://smtv.iptime.org:21/FTP";
        public static string m_ftpFolderPath = DBName;

        public static string m_ftpServerUser = "SMT";
        public static string m_ftpServerPwd = "@admin9150";

        public static string m_NetworkDrivePath = "\\\\192.168.0.190\\smtv\\SMT\\MESDATA";



        public static string Code = string.Empty;           // ID
        public static string CodeTitle = string.Empty;       // Password

        public static int RU5202PortNumber = -1;
        public static int RU5202BaudRate = -1;

        public static string GetUniqueComputerId()
        {
            string uniqueId = string.Empty;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT UUID FROM Win32_ComputerSystemProduct");

            foreach (ManagementObject managementObject in searcher.Get())
            {
                uniqueId = managementObject["UUID"].ToString();
                break;
            }

            return uniqueId;
        }
    }
}
