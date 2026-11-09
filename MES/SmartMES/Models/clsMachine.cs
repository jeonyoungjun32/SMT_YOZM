using DevExpress.XtraEditors.Repository;
using SmartMES.Common;
using System;
using System.Collections.Generic;
using System.Data;
using SmartCommon.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMES.Models
{
    public class clsMachine
    {
        Database db = new Database();
        string query = "";

        #region IDisposable Support
        private bool disposedValue = false; // 중복 호출을 검색하려면

        ~clsMachine()
        {
            this.Dispose(false);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 관리되는 상태(관리되는 개체)를 삭제합니다.
                }

                // TODO: 관리되지 않는 리소스(관리되지 않는 개체)를 해제하고 아래의 종료자를 재정의합니다.
                // TODO: 큰 필드를 null로 설정합니다.
                db.Dispose();
                db = null;

                disposedValue = true;
            }
        }

        // TODO: 위의 Dispose(bool disposing)에 관리되지 않는 리소스를 해제하는 코드가 포함되어 있는 경우에만 종료자를 재정의합니다.
        // ~WrCategories()
        // {
        //   // 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
        //   Dispose(false);
        // }

        // 삭제 가능한 패턴을 올바르게 구현하기 위해 추가된 코드입니다.
        public void Dispose()
        {
            // 이 코드를 변경하지 마세요. 위의 Dispose(bool disposing)에 정리 코드를 입력하세요.
            Dispose(true);
            // TODO: 위의 종료자가 재정의된 경우 다음 코드 줄의 주석 처리를 제거합니다.
            // GC.SuppressFinalize(this);
        }
        #endregion
                              
        public DataView GetClassView(string HighClass, string ClassLevel)
        {
            query = string.Format("Select CLASS 코드, CLASSNAME 명칭 FROM CM_CLASS Where  CorpId = '{0}'  AND HIGHCLASS = '{1}' AND CLASSLEVEL = '{2}' And DisplayYn ='Y' ", WrGlobal.CorpID, HighClass, ClassLevel);
            return db.GetDataView("그룹2", query);
        }

        public DataView GetClassView(string Code, string HighClass, string ClassLevel)
        {
            query = string.Format("select CLASS, CLASSNAME FROM CM_CLASS Where CorpId = '{0}' and CLASS like '{1}%' AND HIGHCLASS like '{2}%' AND CLASSLEVEL = '{3}'", WrGlobal.CorpID, Code, HighClass, ClassLevel);
            return db.GetDataView("그룹2", query);
        }

        public DataView GetClassView(string Code, string HighClass, string ClassLevel, bool allYn)
        {
            if (allYn)
            {
                query = $"Select'ALL' 코드, '전체' 명칭\nUNION ALL\n";
            }

            query += string.Format("select CLASS 코드, CLASSNAME 명칭 FROM CM_CLASS Where CorpId = '{0}' and CLASS like '{1}%' AND HIGHCLASS like '{2}%' AND CLASSLEVEL = '{3}'", WrGlobal.CorpID, Code, HighClass, ClassLevel);
            return db.GetDataView("그룹2", query);
        }

        public DataView GetCategoryForMachineTreeList(string classID)
        {
            query = string.Format("Select CLASS, CLASSNAME, HIGHCLASS FROM CM_CLASS Where CorpId = '{0}' AND (CLASSLEVEL > 1) AND (CLASS LIKE '{1}%') And DisplayYn ='Y' ", WrGlobal.CorpID, classID);

            return db.GetDataView("분류코드", query);
        }

        // 설비 이미지 삭제
        public void DeleteMachineImage(string machineCode)
        {
            query = String.Format("Delete EQ_MachineImage Where CorpId ='{0}' And MachineCode = '{1}'", WrGlobal.CorpID, machineCode);
            db.ExecuteQuery(query);
        }

        // 설비 수리 내역 삭제
        public void DeleteMachineRepair(string machineCode)
        {
            query = String.Format("Delete EQ_MachineRepair Where CorpId ='{0}' And MachineCode = '{1}'", WrGlobal.CorpID, machineCode);
            db.ExecuteQuery(query);
        }

        public DataView GetTotalMachineList(bool isAll)
        {
            if (isAll)
            {
                query = string.Format("select '전체' 코드, '전체' 명칭 Union All Select MachineCode 코드, MachineName 명칭 From EQ_Machine Where UseYn = 'Y'", WrGlobal.CorpID);
            }
            else
            {
                query = string.Format("Select MachineCode 코드, MachineName 명칭 From EQ_Machine Where UseYn = 'Y' Order by MachineCode", WrGlobal.CorpID);
            }

            return db.GetDataView("설비", query);
        }

        public DataRowView GetReportDataByDailyWork(string workNo)
        {
            query = string.Format("exec Work_GetReportDataByDialyWork '{0}', '{1}'", WrGlobal.CorpID, workNo);
            return db.GetDataRecord(query);
        }

        public DataView GetReportDataByDailyWorkProduct(string workNo)
        {
            query = string.Format("exec Work_GetReportDataByDialyWorkProduct '{0}', '{1}'", WrGlobal.CorpID, workNo);
            return db.GetDataView("생산", query);
        }

        public DataView GetReportDataByDailyWorkExam(string workNo)
        {
            query = string.Format("exec Work_GetReportDataByDialyWorkExam '{0}', '{1}'", WrGlobal.CorpID, workNo);
            return db.GetDataView("검수", query);
        }

        public DataView GetReportDataByDailyWorkFault(string workNo)
        {
            query = string.Format("exec Work_GetReportDataByDialyWorkFault '{0}', '{1}'", WrGlobal.CorpID, workNo);
            return db.GetDataView("불량", query);
        }

        public DataView GetPlcMachineList()
        {
            query = string.Format("Select * From EQ_Machine Where CorpId = '{0}' and UseYn = 'Y' and FacilityNo is not null order by MachineCode", WrGlobal.CorpID);
            return db.GetDataView("PLC", query);
        }

        public DataView GetWorkDayList(DateTime startTime, DateTime endTime)
        {
            query = string.Format("exec Work_GetDailyWorkDayList '{0}', '{1:yyyy-MM-dd}', '{2:yyyy-MM-dd}'", WrGlobal.CorpID, startTime, endTime);
            return db.GetDataView("WorkDay", query);
        }

        public DataView GetWorkDayTotalList(DateTime workTime)
        {
            query = string.Format("exec Work_GetDailyWorkTotalList '{0}', '{1:yyyy-MM-dd}'", WrGlobal.CorpID, workTime);
            return db.GetDataView("WorkDay", query);
        }

        public DataView GetCheckSheet(string MachineCode, string MasterCode, DateTime Startdate, string checkYear, string checkMonth)
        {
            query = string.Format("Exec Machine_CheckSheet '{0}', '{1}', '{2}', '{3:yyyy-MM-dd}', '{4}', '{5}'", WrGlobal.CorpID, MachineCode, MasterCode, Startdate, checkYear, checkMonth);
            return db.GetDataView("점검항목내역조회", query);
        }

        public DataView GetSearchCustomerView()
        {
            query = string.Format("select'ALL' 거래처코드 ,'전체' 거래처명 union all select CustCode 거래처코드, CustName 거래처명 from CM_Cust Where CorpId = '{0}'", WrGlobal.CorpID);
            return db.GetDataView("거래처뷰", query);
        }

        public string GetNextMachineCode()
        {
            int seq = 0;

            query = string.Format("Select cast(isnull(max(right(MachineCode, 3)),0) as int) + 1 Code From EQ_Machine Where CorpId = '{0}'", WrGlobal.CorpID);
            DataRowView drvSeq = db.GetDataRecord(query);
            if (drvSeq != null)
            {
                seq = (int)drvSeq["Code"];
            }

            return string.Format("MC{0:000}", seq);
        }

        public bool IsDuplicatedSerialNo(string serialNo, string machineCode)
        {
            bool bRet = false;
            //자기 자신을 제외한 관리번호 중복 체크 
            query = string.Format("Select Top 1 * From EQ_Machine Where CorpId = '{0}' and SerialNo = '{1}' and MachineCode <> '{2}' ", WrGlobal.CorpID, serialNo, machineCode);
            DataRowView dvEQ_Machine = db.GetDataRecord(query);
            if (dvEQ_Machine != null)
            {
                bRet = true;
            }

            return bRet;
        }

        public bool IsDuplicatedBySpec(string machineName, string serialNo, string size, string maker, string machineCode)
        {
            bool bRet = false;

            query = string.Format("Select Top 1 * From EQ_Machine Where CorpId = '{0}' and MachineName = '{1}' and SerialNo = '{2}' and Size = '{3}' and Maker = '{4}' and MachineCode <> '{5}'",
                                        WrGlobal.CorpID, machineName, serialNo, size, maker, machineCode);
            DataRowView dvEQ_Machine1 = db.GetDataRecord(query);
            if (dvEQ_Machine1 != null)
            {
                bRet = true;
            }

            return bRet;

        }

        public bool IsRentedHistory(string machineCode)
        {
            bool bRet = false;

            query = string.Format("Select Top 1 * From Rental Where CorpId = '{0}' and MachineCode = '{1}'", WrGlobal.CorpID, machineCode);
            DataRowView dvMachineCode = db.GetDataRecord(query);
            if (dvMachineCode != null)
            {
                bRet = true;
            }

            return bRet;
        }

        public int GetNextMachineDocumentSeq(string machineCode)
        {
            query = string.Format("Select isnull(max(Seq), 0) + 1 Seq From MachineDocument Where CorpId = '{0}' and MachineCode = '{1}'", WrGlobal.CorpID, machineCode);
            DataRowView drv = db.GetDataRecord(query);

            return (int)drv[0];
        }


        public void LookupProcessItem(ref RepositoryItemLookUpEdit search)
        {
            query = string.Format("Select ProcessCode 코드, ProcessName 명칭 From CM_Process Where CorpId = '{0}'", WrGlobal.CorpID);

            search.DataSource = db.GetDataView("공정", query);
            search.DisplayMember = "명칭";
            search.ValueMember = "코드";
        }


        public DataView GetLineItem(string processCode)
        {
            query = string.Format("Select LineCode 코드, LineName 명칭 From CM_Line Where CorpId = '{0}' and ProcessCode = '{1}'", WrGlobal.CorpID, processCode);
            return db.GetDataView("공정", query);
        }

        public string GetNextPartID()
        {
            int seq = 0;

            query = string.Format("Select cast(isnull(max(right(PartID, 4)),0) as int) + 1 Code From CodePart Where CorpId = '{0}'", WrGlobal.CorpID);
            DataRowView drvSeq = db.GetDataRecord(query);
            if (drvSeq != null)
            {
                seq = (int)drvSeq["Code"];
            }

            return string.Format("P{0:0000}", seq);
        }

        public DataView Select_EQ_Machine()
        {
            query = string.Format("Select MachineCode 설비코드, MachineName 설비명, Size 규격 From EQ_Machine Where CorpId = '{0}'", WrGlobal.CorpID);
            return db.GetDataView("설비", query);
        }

        public DataView Select_EQ_MachineByPart(string partId)
        {
            query = string.Format("Select cm.MachineCode 설비코드, cm.MachineName 설비명, cm.Size 규격 "
                            + "From EQ_Machine cm INNER JOIN "
                                + "CodePartMachine cpm On cm.CorpId = cpm.CorpId and cm.MachineCode = cpm.MachineCode "
                            + "Where cm.CorpId = '{0}' and cpm.PartID = '{1}'", WrGlobal.CorpID, partId);
            return db.GetDataView("설비", query);
        }

        public DataView GetMasterCodeView()
        {
            query = string.Format("select ItemCode 코드, Title 내용 FROM CM_CateItem Where CorpId = '{0}' and Class = '{1}'", WrGlobal.CorpID, "M020");
            return db.GetDataView("보전항목", query);
        }

        public DataView GetCustomerView()
        {
            query = string.Format("select CustCode 거래처코드, CustName 거래처명, CustNo 사업자번호, PresiName 대표자명 from CM_Cust Where CorpId = '{0}'", WrGlobal.CorpID);
            return db.GetDataView("거래처뷰", query);
        }
    }
}
