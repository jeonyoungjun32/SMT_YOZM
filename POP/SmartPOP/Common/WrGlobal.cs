using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DevExpress.XtraReports.Extensions;

namespace SmartPOP.Common
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
            public int Disp_Seq; //Column Edit 활성화화 여부(true:수정가능, false:수정불가)
            public bool DispYn; //Column Visible 여부(true:표기, false:숨김)
            public bool LockYn; //Column Edit 활성화화 여부(true:수정가능, false:수정불가)
        }
        public static Dictionary<string, Dictionary<string, CodeOption>> CdOption = new Dictionary<string, Dictionary<string, CodeOption>>();
        //기타
        public static Color EditBgColor = Color.PapayaWhip; //변경허용 Column 배경색

        public static string ClientIpAddress = GetHostNameSafely();   //클라이언트 HostName
        public static string ClientHostName = GetHostNameSafely();   //클라이언트 HostName

        public static bool GAM_SW = false;  //감리용

        public static string PublicCorpID = "Z000";
        public static string CorpID = "1000";               // 로그인한 사업장
        public static string CorpName = "SNexis-MES";       // 로그인한 사업장명
        public static string LoginID = "";                  // 로그인한 사용자 ID
        public static string LoginUserNM = "";              // 로그인한 사용자 이름
        public static DateTime LoginTime;                   // 로그인한 시간(로그아웃 시간 입력용)
        public static string Login_Project = "POP";          // 로그인한 프로젝트

        public static string DeptCD = string.Empty;          // 부서코드
        public static string DeptName = string.Empty;        // 부서명칭
        public static string GroupCode = string.Empty;       // 부서명칭
        public static string OrderType = string.Empty;       // 납품오더의 종류

        public static string SelectionCode = string.Empty;           // 코드
        public static string SelectionTitle = string.Empty;           // 코드
        public static string PlantID = string.Empty;          // 사업부
        public static string ProcGroup = string.Empty;        // 공정그룹
        public static string ProcGroupName = string.Empty;        // 공정그룹
        public static string LineID = string.Empty;           // 라인ID
        public static string LineName = string.Empty;           // 라인ID
        public static string ProcID = string.Empty;           // 공정코드
        public static string ProcName = string.Empty;           // 공정코드
        public static string Language = string.Empty;         // 언어
        public static string BasicPrinter = string.Empty;     // 기본프린터
        public static string BarcodePrinter = string.Empty;   // 바코드 프린터
        public static string WarehouseID = string.Empty;      // 창고코드
        public static string LocationID = string.Empty;       // 위치정보
        public static int PositionID = 0;                     // 좌석위치

        public static string ProductCode = "";
        public static string ProductName = "";


        public static string Email = "";                     // 이메일
        public static string CustYn = string.Empty;          // 거래처 선택여부

        public static string Yn = string.Empty;              // 선택 및 취소
        public static DateTime SelectionDate;                // 선택일자
        public static string EmpNo;                          // 승인자
        public static string DeptCode;                       // 승인부서
        public static string CustCode = "";                    // 거래처 코드
        public static string CustName;                       // 거래처 명
        public static string CustType;                       // 거래처 타입, 외주처 확인
        public static string ShipToCode;                     // 납품처 코드
        public static string DeliLocation;                   // 남품처명 또는 주소
        public static string MachineCode;                    // 장비 코드
        public static string MachineName;                    // 장비 코드
        public static string PostNo;
        public static string Address;
        public static int PostSeq;
        public static string Message = "";
        public static string OpenYn = "N";
        public static string OrderCount = "";
        public static string HostName = GetHostNameSafely();
        public static bool YesNo = false;
        public static string Remark = string.Empty;
        public static string PortName = string.Empty;        // 공정그룹
        public static string BaudRate = string.Empty;           // 라인ID
        public static string Parity = string.Empty;           // 공정코드
        public static string Parity1 = string.Empty;           // 공정코드
        public static string Parity2 = string.Empty;           // 공정코드
        public static string DataBits = string.Empty;           // 공정코드
        public static string DataBits1 = string.Empty;           // 공정코드
        public static string DataBits2 = string.Empty;           // 공정코드
        public static string StopBits = string.Empty;         // 언어
        public static string StopBits1 = string.Empty;         // 언어
        public static string StopBits2 = string.Empty;         // 언어

        // 데이터의 CRUD를 구분
        public enum ENU_CRUD
        {
            CREATE = 0,     // 문서생성
            READ = 1,       // 문서일기
            UPDATE = 2,     // 문서수정
            DELETE = 3,     // 문서삭제
            ORIGINAL = 4,   // RECOVERY
            APPROVALL = 5,   // 문서승인
            APPROVALCANCLE = 6,   // 문서승인취소
            PRINT1 = 7,
            PRINT2 = 8,
            PRINT3 = 9,
            PRINT4 = 10
        }//END 


        public static string Port;                           //포트번호
        public static string IpAddress = GetIpAddressSafely(); //IP주소
        private static string GetHostNameSafely()
        {
            try
            {
                return System.Net.Dns.GetHostName();
            }
            catch (System.Net.Sockets.SocketException)
            {
                return Environment.MachineName;
            }
        }

        private static string GetIpAddressSafely()
        {
            try
            {
                var address = System.Net.Dns.GetHostAddresses(HostName)
                    .FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
                return address == null ? System.Net.IPAddress.Loopback.ToString() : address.ToString();
            }
            catch (System.Net.Sockets.SocketException)
            {
                return System.Net.IPAddress.Loopback.ToString();
            }
        }
        public static string Title;                          //센서명



        public static string ftpIp = "ftp://smtv.iptime.org:21/FTP";
        public static string ftpUser = "SMT";
        public static string ftpPwd = "@admin9150";

        public static string SessionNo = "";
        public static string ProcessGroup = "";

        public static Color SelectedRowBackColor = Color.LightBlue;
        public static Color SelectedRowForeColor = Color.Black;

        public static string ProgramType = "3";

        public static string ProJectName = "SmartERP";

        public static List<clsAuthor> AuthorList = new List<clsAuthor>();

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
    }
}

