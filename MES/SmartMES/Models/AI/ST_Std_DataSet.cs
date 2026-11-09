using SmartCommon.Common;
using SmartMES.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMES.Models.AI
{
    class ST_Std_DataSet
    {
        #region Property
        public string Cond_Id { get; set; }
        public string Ex_Id { get; set; }
        public string Merge_Id { get; set; }
        public string GetDate_From { get; set; }
        public string GetDate_To { get; set; }
        public string Remark { get; set; }

        #endregion

        Database db = new Database();
        string query;

        public DataTable SelectST_Std_DataSet(string cond_Id)
        {
            query = $@"SELECT Cond_Id
                            , Ex_Id
                            , Merge_Id
                            , GetDate_From
                            , GetDate_To
                            , Remark
                         FROM ST_Std_DataSet
                        WHERE Cond_Id = '{cond_Id}'";

            return db.GetDataSet(query);
        }
        public DataTable Select_ST_Std_Dataset(string Date_From, string Date_To)
        {
            query = $@" SELECT '' AS Status
                             , Cond_Id
                             , Ex_Id
                             , Merge_Id
                             , GetDate_From
                             , GetDate_To
                             , Remark
                        FROM ST_Std_Dataset 
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND GetDate_From BETWEEN CASE WHEN '{Date_From}' = '' THEN '19000101'
											     ELSE '{Date_From}' END
										     AND CASE WHEN '{Date_To}' = '' THEN '99991231'
											     ELSE '{Date_To}' END 
                        ORDER BY Cond_Id ASC";

            return db.GetDataSet(query);
        }

        public void UpdateST_Std_DataSet_MergeID(string cond_Id, string merge_Id)
        {
            query = $@"UPDATE ST_Std_Dataset
                          SET Merge_Id = '{merge_Id}'
                        WHERE Cond_Id = '{cond_Id}'";

            db.ExecuteQuery(query);
        }

        public void Update_ST_Std_Dataset()
        {
            query = $@" UPDATE ST_Std_Dataset
                        SET Remark = '{Remark}'
                          , ModId = '{WrGlobal.LoginID}'
                          , ModDt = GETDATE()
                        WHERE  CorpId = '{WrGlobal.CorpID}'
                        AND Cond_Id = '{Cond_Id}'";

            db.ExecuteQuery(query);
        }


        public void Insert_ST_Std_Dataset()
        {
            query = $@" INSERT INTO ST_Std_Dataset
                        (
                               CorpId
                            ,  Cond_Id
                            ,  Ex_Id
                            ,  GetDate_From
                            ,  GetDate_To
                            ,  CreId
                            ,  CreDt
                        )

                        VALUES
                        (
                              '{WrGlobal.CorpID}'
                            , '{Cond_Id}'
                            , '{Ex_Id}'
                            , '{GetDate_From}'
                            , '{GetDate_To}'
                             --, '{WrGlobal.LoginID}'
                            , 'admin'
                            , GETDATE()
                        ) ";

            db.ExecuteQuery(query);
        }

    }
}
