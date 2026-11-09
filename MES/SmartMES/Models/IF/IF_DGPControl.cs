using System.Data;
using SmartCommon.Common;

namespace SmartMES.Models.IF
{
    public class IF_DGPControl
    {
        #region Property

        public string CTCode { get; set; }
        public string CTName { get; set; }
        public string CT_Company { get; set; }
        public string CT_IP { get; set; }
        public string CT_Port { get; set; }
        public string CT_UserYn { get; set; }
        public string EmployeeId { get; set; }

        #endregion

        Database db = new Database();
        string query = "";

        public DataTable Select_IF_DGPControl()
        {
            query = $@"
                        SELECT
                            '' as Status
                            , idc.CorpId
                            , idc.CTCode
                            , idc.CTName
                            , idc.CT_Company
                            , idc.CT_IP
                            , idc.CT_Port
                            , idc.CT_UserYn
                            , su.AutoLoginYn
                            , idc.EmployeeId
                            , idc.CreId
                            , idc.CreIP
                            , idc.CreDt
                            , idc.ModId
                            , idc.ModIP
                            , idc.ModDt
                        FROM IF_DGPControl idc
                        LEFT JOIN SM_User su ON idc.CorpId = su.CorpId AND idc.EmployeeId = su.EmployeeId
                        WHERE idc.CorpId = '{WrGlobal.CorpID}'
                    ";
            return db.GetDataTable(query);
        }

        public bool Insert_IF_DGPControl()
        {
            query = $@"
                        INSERT INTO IF_DGPControl
                        (
                            CorpId
                            , CTCode
                            , CTName
                            , CT_Company
                            , CT_IP
                            , CT_Port
                            , CT_UserYn
                            , EmployeeId
                            , CreId
                            , CreIP
                            , CreDt
                        )
                        VALUES
                        (
                            '{WrGlobal.CorpID}'
                            , dbo.[FN_Create_Cd_DgpControl]('{WrGlobal.CorpID}')
                            , '{CTName}'
                            , '{CT_Company}'
                            , '{CT_IP}'
                            , '{CT_Port}'
                            , '{CT_UserYn}'
                            , '{EmployeeId}'
                            , '{WrGlobal.LoginID}'
                            , '{WrGlobal.HostName}'
                            , GETDATE()
                        )
                    ";

            return db.ExecuteQuery(query);
        }

        public bool Update_IF_DGPControl()
        {
            query = $@"
                        UPDATE IF_DGPControl
                        SET
                            CTName = '{CTName}'
                            , CT_Company = '{CT_Company}'
                            , CT_IP = '{CT_IP}'
                            , CT_Port = '{CT_Port}'
                            , CT_UserYn = '{CT_UserYn}'
                            , EmployeeId = '{EmployeeId}'
                            , ModId = '{WrGlobal.LoginID}'
                            , ModIP = '{WrGlobal.HostName}'
                            , ModDt = GETDATE()
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND CTCode = '{CTCode}'
                    ";

            return db.ExecuteQuery(query);
        }

        public bool Delete_IF_DGPControl()
        {
            query = $@"
                        DELETE IF_DGPControl
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND CTCode = '{CTCode}'
                    ";

            return db.ExecuteQuery(query);
        }

    }
}
