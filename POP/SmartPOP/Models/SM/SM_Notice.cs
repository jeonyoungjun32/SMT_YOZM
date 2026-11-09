using SmartPOP.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartERP.Models.SM
{
    public class SM_Notice
    {
        #region Property

        public string Seq { get; set; }

        public string ManCode { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime EndDate { get; set; }

     //   public Database Db { get => db; set => db = value; }

        #endregion

        Database db = new Database();
        string query = "";

        public DataTable Select_Notice()
        {
            query = $@"SELECT '' as Status, nt.Seq, nt.Title, nt.Content, WriteDate, nt.EmployeeId, cu.EmployeeName, nt.EndDate, nt.NoticeImg
                        FROM SM_Notice nt
						LEFT JOIN SM_User cu ON nt.EmployeeId = cu.EmployeeId
						ORDER BY Seq DESC";

            return db.GetDataTable(query);
        }

        public DataTable Select_Notice_View()
        {
            query = $@"
                        SELECT CorpId, BoardNo, Class, WriteDate, Subject, WriteName, Body, DisplayType, StartDate, EndDate, CreId, CreIP, CreDt, ModId, ModIP, ModDt
                        FROM SM_Notice
                        where CorpId = {WrGlobal.CorpID} 
                        order by BoardNo desc
                    ";

            return db.GetDataTable(query);
        }


        public void InsertUpdateNotice()
        {
            db.PropertyConvert(this);

            string sql = $@" IF EXISTS (
                                        SELECT Seq
                                          FROM SM_Notice
                                         WHERE Seq = '{Seq}'
                                       )

                                        UPDATE SM_Notice
                                           SET WriteDate = '{DateTime.Now.ToString("yyyy-MM-dd")}',
                                               EmployeeId  = '{WrGlobal.LoginID}',
                                               Title    = '{Title}',
                                               Content  = '{Content}',
                                               EndDate  = '{EndDate.ToString("yyyy-MM-dd")}'
                                         WHERE Seq = '{Seq}'

                                  ELSE
                                   INSERT INTO SM_Notice
                                               (CorpId, WriteDate, EmployeeId, Seq, Title, Content, EndDate, CreId, CreIP, CreDt)
                                        VALUES ('{WrGlobal.CorpID}'
                                                ,  GETDATE()
                                                ,'{WrGlobal.LoginID}'
                                                , '{Seq}'
                                                , '{Title}'
                                                , '{Content}'
                                                , '{EndDate.ToString("yyyy-MM-dd")}'
                                                , '{WrGlobal.LoginID}'
                                                , HOST_NAME()
                                                , GETDATE()
                                               )";

            db.ExecuteQuery(sql);
        }

        public void DeleteNotice()
        {
            string sql = $@"DELETE FROM SM_Notice 
                                  WHERE Seq = '{Seq}'";

            db.ExecuteQuery(sql);
        }

        public DataView GetMaxSeq()
        {
            query = $@"SELECT ISNULL(MAX(Seq),0)+1 MaxSeq FROM SM_Notice WHERE CorpId = '{WrGlobal.CorpID}'";

            return db.GetDataView("Seq", query);
        }
    }
}
