using DevExpress.XtraSpreadsheet.Import.Xls;
using SmartCommon.Common;
using SmartMES.Common;
using System;
using System.Data;

namespace SmartMES.Models.CM
{
	class CM_CommonFile
    {
        #region Property
        public string CorpId { get; set; }

        public string ClassItemCode { get; set; }

        public int FileSeq { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string Title { get; set; }

        public string OriFileName { get; set; }

        public string FileName { get; set; }

        public string Path { get; set; }

        public long? Capacity { get; set; }

        public string FileExt { get; set; }

        public string UserID { get; set; }

        public string CustCode { get; set; }
        public string RefNo { get; set; }
        public int RefSeq { get; set; }
        public byte[] Pic { get; set; }
        public string DocType { get; set; }

        #endregion

        Database db = new Database();
        string query = "";

        public DataTable Select_CM_CommonFile()
        {
            RefNo = RefNo.ConvertString_Null_To_Empty();
            ClassItemCode = ClassItemCode.ConvertString_Null_To_Empty();

            query = $@"
                SELECT '' AS Status
                     , cf.CorpId
                     , cf.ClassItemCode
                     , cf.FileSeq
                     , cf.StartDate
                     , cf.EndDate
                     , cf.Title
                     , cf.OriFileName
                     , cf.FileName
                     , cf.Path
                     , cf.Capacity
                     , cf.FileExt
                     , cf.UserID
                     , cf.CustCode
                     , cf.CreId
                     , cf.CreIP
                     , cf.CreDt
                     , cf.ModId
                     , cf.ModIP
                     , cf.ModDt
                     , cf.RefNo
                     , cf.RefSeq
                     , cf.DocType
                  FROM CM_CommonFile cf
                 WHERE CorpId = '{WrGlobal.CorpID}'
            ";

            if (ClassItemCode != "")
            {
                query += $"\nAND cf.ClassItemCode = '{ClassItemCode}'";
            }

            if (RefNo != "")
			{
                query += $"\nAND cf.RefNo = '{RefNo}'";
			}

            return db.GetDataTable(query);
        }

        public DataRow Select_CM_CommonFile_Img()
        {
            query = $@"
                SELECT TOP 1 Pic
                  FROM CM_CommonFile 
                 WHERE CorpId = '{WrGlobal.CorpID}'
                 AND ClassItemCode = '{ClassItemCode}'
                  Order by FileSeq Desc  ";


            return db.GetDataRow(query);
        }

        public int GetNextSeq()
        {
            int seq = 0;

            query = $@" SELECT MAX(FileSeq) AS MaxSeq
                        FROM CM_CommonFile 
                        WHERE CorpId = '{WrGlobal.CorpID}' 
                        AND ClassItemCode = '{ClassItemCode}' ";

            DataRow dr = db.GetDataRow(query);

            if (dr != null)
            {
                seq = dr["MaxSeq"].ConvertInt_Null_To_Zero();
            }

            return seq + 1;
        }

        public bool Ins_CM_CommonFile()
        {
            query = $@"
            INSERT INTO CM_CommonFile
            (
                  CorpId
                , ClassItemCode
                , FileSeq
                , StartDate
                , EndDate
                , Title
                , OriFileName
                , FileName
                , Path
                , Capacity
                , FileExt
                , UserID
                , CustCode
                , RefNo
                , RefSeq

                , CreId
                , CreIP
                , CreDt
            )
            VALUES
            (
                  '{CorpId}'
                , {ClassItemCode.ConvertStr_Empty_To_Null()}
                , {FileSeq.ConvertNumeric_Empty_To_Null()}
                , {StartDate.ConvertStr_Empty_To_Null()}
                , {EndDate.ConvertStr_Empty_To_Null()}
                , {Title.ConvertStr_Empty_To_Null()}
                , {OriFileName.ConvertStr_Empty_To_Null()}
                , {FileName.ConvertStr_Empty_To_Null()}
                , {Path.ConvertStr_Empty_To_Null()}
                , {Capacity.ConvertNumeric_Empty_To_Null()}
                , {FileExt.ConvertStr_Empty_To_Null()}
                , {UserID.ConvertStr_Empty_To_Null()}
                , {CustCode.ConvertStr_Empty_To_Null()}
                , {RefNo.ConvertStr_Empty_To_Null()}
                , {RefSeq.ConvertInt_Empty_To_Null()}
                , '{WrGlobal.LoginID}'
                , '{WrGlobal.ClientHostName}'
                , GETDATE()
            )";

            return db.ExecuteQuery(query);
        }

        public bool Ins_CM_CommonFile_Img()
        {
            query = $@"
            INSERT INTO CM_CommonFile
            (
                  CorpId
                , ClassItemCode
                , FileSeq
                , Pic
                , CreId
                , CreIP
                , CreDt
            )
            VALUES
            (
                  '{CorpId}'
                , {ClassItemCode.ConvertStr_Empty_To_Null()}
                , {FileSeq.ConvertNumeric_Empty_To_Null()}
                , @Pic
                , '{WrGlobal.LoginID}'
                , '{WrGlobal.ClientHostName}'
                , GETDATE()
            )";

            return db.ExecuteQueryImage(query, Pic);
        }

        public bool Upd_CM_CommonFile()
        {
            query = $@"
            UPDATE CM_CommonFile
               SET StartDate = {StartDate.ConvertStr_Empty_To_Null()}
                 , EndDate = {EndDate.ConvertStr_Empty_To_Null()}
                 , Title = {Title.ConvertStr_Empty_To_Null()}
                 , OriFileName = {OriFileName.ConvertStr_Empty_To_Null()}
                 , FileName = {FileName.ConvertStr_Empty_To_Null()}
                 , Path = {Path.ConvertStr_Empty_To_Null()}
                 , Capacity = {Capacity.ConvertNumeric_Empty_To_Null()}
                 , FileExt = {FileExt.ConvertStr_Empty_To_Null()}
                 , UserID = {UserID.ConvertStr_Empty_To_Null()}
                 , CustCode = {CustCode.ConvertStr_Empty_To_Null()}
                 , DocType = {DocType.ConvertStr_Empty_To_Null()}
                 , ModId = '{WrGlobal.LoginID}'
                 , ModIP = '{WrGlobal.ClientHostName}'
                 , ModDt = GETDATE()
             WHERE CorpId = '{CorpId}'
               AND ClassItemCode = '{ClassItemCode}'
               AND FileSeq = '{FileSeq}'
            ";

            return db.ExecuteQuery(query);
        }


        public bool Del_CM_CommonFile()
        {
            query = $@"
                DELETE CM_CommonFile
                 WHERE CorpId = '{CorpId}'
                   AND ClassItemCode = '{ClassItemCode}'
                   AND FileSeq = '{FileSeq}'
                ";

            return db.ExecuteQuery(query);
        }

    }
}
