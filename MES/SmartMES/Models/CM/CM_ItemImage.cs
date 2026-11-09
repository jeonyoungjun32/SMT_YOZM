using SmartCommon.Common;
using SmartMES.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMES.Models.CM
{
	public class CM_ItemImage
	{
        #region Property
        public string CorpId { get; set; }

        public string ItemCode { get; set; }

        public int Seq { get; set; }

        public string ImgCode { get; set; }

        public string Date { get; set; }

        public string Title { get; set; }

        public string Path { get; set; }

        public string FileExt { get; set; }

        public byte[] Pic { get; set; }

        public string ExamDivCd { get; set; }
        public string ProcessCode { get; set; }

        #endregion

        Database db = new Database();
		string query = "";

		public DataTable SelectImage()
		{
			query = $@"
                SELECT '' AS Status
                    , cmi.CorpId
                    , cmi.ItemCode
                    , cmi.Seq
                    , cmi.ImgCode
                    , cmi.Date
                    , cmi.Title
                    , cmi.Path
                    , cmi.FileExt
                    , cmi.Pic
                    , cmi.ProcessCode
                    , cmi.CreId
                    , cmi.CreIP
                    , cmi.CreDt
                    , cmi.ModId
                    , cmi.ModIP
                    , cmi.ModDt
                    , cmi.ExamDivCd
                
                FROM CM_ItemImage cmi
                WHERE cmi.CorpId = '{CorpId}'
                AND cmi.ItemCode = '{ItemCode}'
                
                ";

			return db.GetDataTable(query);
		}

        public DataRow Select_LastImg()
        {
            query = $@"
                       SELECT TOP 1 CorpId, ImgCode, ItemCode, Seq, Pic, Path, FileExt, Title, Date
                       FROM CM_ItemImage img
                       WHERE img.CorpId = '{WrGlobal.CorpID}'
                       AND img.ImgCode = '{ItemCode}'
                       ";

            if (ProcessCode != "" && ProcessCode != null)
            {
                query += $"\nAND img.ProcessCode = '{ProcessCode}'";
            }

            if (ExamDivCd != "" && ExamDivCd != null)
            {
                query += $"\nAND img.ExamDivCd = '{ExamDivCd}'";
            }

            if (ItemCode != "" && ItemCode != null)
            {
                query += $"\nAND img.ItemCode = '{ItemCode}'";
            }

            query += $"\nORDER BY Seq DESC";

            return db.GetDataRow(query);
        }

        public int Sel_NextSeq()
        {
            query = $@"
                        SELECT ISNULL(MAX(Seq), 0) + 1 AS Seq 
                        FROM CM_ItemImage 
                        WHERE CorpId = '{WrGlobal.CorpID}' 
                        AND ItemCode = '{ItemCode}' 

                        ";
            DataRow dr = db.GetDataRow(query);

            int seq = dr["Seq"].ConvertInt_Null_To_Zero();

            return seq;
        }

        public bool Ins_item_Image()
        {
            query = $@"
                        INSERT INTO CM_ItemImage
                        (
                            CorpId
                            , ItemCode
                            , ImgCode
                            , ExamDivCd
                            , Date
                            , Title
                            , Seq
                            , ProcessCode
                            , Path
                            , FileExt
                            , Pic
                            , CreId
                            , CreIP
                            , CreDt
                        )
                        VALUES
                        (
                             '{WrGlobal.CorpID}'
                            ,'{ItemCode}'
                            , '{ImgCode}'
                            , '{ExamDivCd}'
                            , {Date.ConvertStr_Empty_To_Null()}
                            , {Title.ConvertStr_Empty_To_Null()}
                            , '{Seq}'
                            , {ProcessCode.ConvertStr_Empty_To_Null()}
                            , '{Path}'
                            , '{FileExt}'
                            , @Pic
                            , '{WrGlobal.LoginID}'
                            , '{WrGlobal.ClientHostName}'
                            , GETDATE()
                        
                        )
                    ";

            return db.ExecuteQueryImage(query, Pic);
        }

        public bool Upd_Item_Image()
        {
            query = $@" 
                        UPDATE CM_ItemImage 
                        SET ImgCode = '{ImgCode}'
                            , Pic = @Pic
                            , Path = '{Path}'
                            , ProcessCode = {ProcessCode.ConvertStr_Empty_To_Null()}
                            , Title = '{Title}'
                            , FileExt = '{FileExt}'
                            , ItemCode = '{ItemCode}'
                            , ExamDivCd = '{ExamDivCd}'
                            , ModId = '{WrGlobal.LoginID}'
                            , ModIP = '{WrGlobal.ClientHostName}'
                            , ModDt = GETDATE()
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND ItemCode = '{ItemCode}'
                        AND Seq = '{Seq}'
                        ";

            return db.ExecuteQueryImage(query, Pic);
        }

        public bool Del_Item_Image()
        {
            query = $@" 
                        DELETE FROM CM_ItemImage 
                        WHERE CorpId = '{WrGlobal.CorpID}' 
                        AND ItemCode = '{ItemCode}'
                        AND ImgCode = '{ImgCode}'
                        AND Seq = '{Seq}'
                        ";

            return db.ExecuteQuery(query);
        }

        public bool Del_item_Image_ByCode()
        {
            query = $@" 
                        DELETE FROM CM_ItemImage 
                        WHERE CorpId = '{WrGlobal.CorpID}' 
                        AND ItemCode = '{ItemCode}'
                        ";

            return db.ExecuteQuery(query);
        }
    }
}
