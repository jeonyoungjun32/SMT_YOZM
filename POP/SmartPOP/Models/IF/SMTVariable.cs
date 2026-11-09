using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SmartPOP.Models.IF
{
    class SMTVariable
    {
        #region -UserBase Variable-
        // TCP IP Variable
        public static string gStrSvrIp = "";
        public static string gStrPLCSvrIp = "";
        public static string gStrFXSvrIp = "";
        public static string gStrRS485SvrIp = "";
        public static int gStrSvrPort = 0;
        public static int gStrPLCSvrPort = 0;
        public static int gStrFXSvrPort = 0;
        public static int gStrRS485SvrPort = 0;

        public static SMTSocket sockAct;

        // 삼화당
        public static string[] Corpid = null;
        public static string[] DgpCode = null;
        public static string[] DgpName = null;
        public static string[] PLCCountDevice = null;
        public static string[] NonOperationTime = null;
        public static string[] LastResetTime = null;
        public static string[] ResetCountDevice = null;
        public static string[] JSCountDevice = null;
        public static string[] JSCount = null;
        public static string[] JSTimeDevice = null;
        public static string[] JSTime = null;
        public static string gStrLevel = "";
        #endregion
    }

    class SMTVariableDatabase
    {
        public static string strDBServer = "";
        public static string strDBSource = "";
        public static string strDBUserId = "";
        public static string strDBUserPw = "";
    }

    public class LSPLCVariable
    {
        public int[] OldCount { get; set; }
        public bool[] NonOperEnd { get; set; }
        public bool[] DBCheck { get; set; }
        public bool IsRead { get; set; }
        public bool IsWrite { get; set; }

        public Stopwatch SWRead = new Stopwatch();
        public Stopwatch SWWrite = new Stopwatch();

        public LSReadSocket socketRead = null;
        public LSWriteSocket socketWrite = null;

        public LS_XGT LS_Read = new LS_XGT();
        public LS_XGT LS_Write = new LS_XGT();
        public List<string> LstCounter = new List<string>();
        public Dictionary<object, Stopwatch> DicWait = new Dictionary<object, Stopwatch>();
        public List<string> listMTDGPHour = new List<string>();

        public void SetVariable(int count)
        {
            OldCount = new int[count];
            NonOperEnd = new bool[count];
            DBCheck = new bool[count];
        }
    }
}
