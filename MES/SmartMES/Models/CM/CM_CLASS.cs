using SmartMES.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartCommon.Common;

namespace SmartMES.Models.CM
{
    class CM_CLASS
    {
        #region Property
        public string CorpId { get; set; }
        public string CLASS { get; set; }
        public int CLASSLEVEL { get; set; }
        public string CLASSNAME { get; set; }
        public string HIGHCLASS { get; set; }
        public string CLASSFULLNAME { get; set; }
        public string STARTDATE { get; set; }
        public string ENTDATE { get; set; }
        public string ROOTCLASS { get; set; }
        public string DisplayYn { get; set; }
        public string CreId { get; set; }
        public string CreIP { get; set; }
        public string CreDt { get; set; }
        public string ModId { get; set; }
        public string ModIP { get; set; }
        public string ModDt { get; set; }
        #endregion Property End

        Database db = new Database();
        string query;

        public DataTable Select_Class()
        {
            query = $@"
                        SELECT
                            '' as Status
                            , CorpId
                            , CLASS
                            , CLASSLEVEL
                            , CLASSNAME
                            , HIGHCLASS
                            , CLASSFULLNAME
                            , STARTDATE
                            , ENTDATE
                            , ROOTCLASS
                            , DisplayYn
                            , CreId
                            , CreIP
                            , CreDt
                            , ModId
                            , ModIP
                            , ModDt
                        FROM CM_CLASS
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND CLASSLEVEL = '{CLASSLEVEL}' AND DisplayYN = 'Y'
                    ";
            if (HIGHCLASS != null)
            {
                query += $"AND HIGHCLASS = '{HIGHCLASS}'";
            }

            return db.GetDataTable(query);
        }

        public DataTable Select_Class_Like()
        {
            query = $@"
                        SELECT
                            '' as Status
                            , CorpId
                            , CLASS
                            , CLASSLEVEL
                            , CLASSNAME
                            , HIGHCLASS
                            , CLASSFULLNAME
                            , STARTDATE
                            , ENTDATE
                            , ROOTCLASS
                            , DisplayYn
                            , CreId
                            , CreIP
                            , CreDt
                            , ModId
                            , ModIP
                            , ModDt
                        FROM CM_CLASS
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND CLASS LIKE '{CLASS}%' 
                    ";

            return db.GetDataTable(query);
        }

        public DataTable Select_Class_Like_LookUp()
        {
            query = $@"
                        SELECT
                             CLASS 코드
                            , CLASSNAME 명칭
                        FROM CM_CLASS
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND CLASS LIKE '{CLASS}%' 
                    ";

            return db.GetDataTable(query);
        }

        public DataRow Select_Class_by_Key()
        {
            query = $@"
                        SELECT * FROM CM_CLASS WHERE CorpId = '{WrGlobal.CorpID}' AND CLASS = '{CLASS}'
                    ";

            return db.GetDataRow(query);
        }

        public bool Ins_Class()
        {
            query = $@"
                        INSERT INTO CM_CLASS(CorpId, CLASS, CLASSLEVEL, CLASSNAME, HIGHCLASS, CLASSFULLNAME, STARTDATE, ENTDATE, ROOTCLASS
                                        , DisplayYn, CreId, CreIP, CreDt)
                        VALUES(
                                '{WrGlobal.CorpID}'
                                , '{CLASS}'
                                , '{CLASSLEVEL}'
                                , '{CLASSNAME}'
                                , '{HIGHCLASS}'
                                , '{CLASSFULLNAME}'
                                , '{STARTDATE}'
                                , '{ENTDATE}'
                                , '{ROOTCLASS}'
                                , '{DisplayYn}'
                                , '{WrGlobal.LoginID}'
                                , '{WrGlobal.ClientHostName}'
                                , GETDATE()
                                )
                    ";

            return db.ExecuteQuery(query);
        }

        public bool Upd_Class()
        {
            query = $@"
                        UPDATE CM_CLASS SET
                            CLASSLEVEL = '{CLASSLEVEL}'
                            , CLASSNAME = '{CLASSNAME}'
                            , HIGHCLASS = '{HIGHCLASS}'
                            , ROOTCLASS = '{ROOTCLASS}'
                            , DisplayYn = '{DisplayYn}'
                            , ModId = '{WrGlobal.LoginID}'
                            , ModIP = '{WrGlobal.ClientHostName}'
                            , ModDt = GETDATE()
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND CLASS = '{CLASS}'
                    ";

            return db.ExecuteQuery(query);
        }

        public bool Upd_ClassName()
        {
            query = $@"
                        UPDATE CM_CLASS SET
                             CLASSNAME = '{CLASSNAME}'
                            , ModId = '{WrGlobal.LoginID}'
                            , ModIP = '{WrGlobal.ClientHostName}'
                            , ModDt = GETDATE()
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND CLASS = '{CLASS}'
                    ";

            return db.ExecuteQuery(query);
        }

        public bool Del_Class()
        {
            query = $@"
                        DELETE CM_CLASS
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND CLASS = '{CLASS}'
                        OR HIGHCLASS = '{CLASS}'
                    ";


            return db.ExecuteQuery(query);
        }

        public bool Del_Class_Root()
        {
            query = $@"
                        DELETE CM_CLASS
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND ROOTCLASS = '{ROOTCLASS}'
                    ";

            return db.ExecuteQuery(query);
        }
    }
}
