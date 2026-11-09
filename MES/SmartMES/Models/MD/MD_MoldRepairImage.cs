using SmartCommon.Common;
using SmartMES.Common;
using System.Data;

namespace SmartMES.Models.MD
{
    public class MD_MoldRepairImage
    {
        #region Property

        public string MoldCode { get; set; }
        public string RepairNo { get; set; }
        public int Seq { get; set; }
        public string Path { get; set; }
        public string FileNm { get; set; }
        public string FileExt { get; set; }
        public byte[] RepairImg { get; set; }
        public string Remark { get; set; }

        #endregion

        Database db = new Database();
        string query = "";

        public DataTable Select_MD_MoldRepairImage()
        {
            query = $@"
                        SELECT '' AS Status
                            , A.RepairNo
                            , A.MoldCode
                            , B.MoldSN
                            , A.Seq
                            , A.Path
                            , A.FileNm
                            , A.FileExt
                            , A.RepairImg
                            , A.Remark
                            , A.CreId
                            , A.CreDt
                            , A.ModId
                            , A.ModDt
                        
                        FROM MD_MoldRepairImage A
                        LEFT JOIN MD_Mold B ON A.MoldCode= B.MoldCode
                        WHERE A.CorpId = '{WrGlobal.CorpID}'
                        AND A.RepairNo = '{RepairNo}'
                        AND A.MoldCode = '{MoldCode}'
                        ORDER BY SEQ DESC
                    ";

            return db.GetDataTable(query);
        }

        public DataTable Select_RepairImg(string MoldCode, string RepairNo)
        {
            query = $@"SELECT TOP 1 
                            RepairImg
                            , RepairNo
                            , Seq
                         FROM MD_MoldRepairImage
                        WHERE CorpID = '{WrGlobal.CorpID}'
                          AND MoldCode = '{MoldCode}'
                          AND RepairNo = '{RepairNo}'
                        ORDER BY SEQ DESC";

            DataTable dt = db.GetDataTable(query);
            return dt;
        }

        public DataRow Select_LastImg()
        {
            query = $@"
                       SELECT TOP 1 CorpId, Img_Div_Cd, Seq, Item_Img, Path, FileNm, FileExt
                       FROM MD_MoldRepairImage img
                       WHERE img.CorpId = '{WrGlobal.CorpID}'
                       AND img.MoldCode = '{MoldCode}'
                       AND img.RepairNo = '{RepairNo}'
                       ORDER BY Seq DESC";

            return db.GetDataRow(query);
        }

        public int Select_NextSeq()
        {
            query = $@"
                        SELECT ISNULL(MAX(Seq), 0) + 1 AS Seq 
                        FROM MD_MoldRepairImage 
                        WHERE CorpId = '{WrGlobal.CorpID}' 
                        AND MoldCode = '{MoldCode}' 
                        AND RepairNo = '{RepairNo}'

                        ";
            DataRow dr = db.GetDataRow(query);

            int seq = dr["Seq"].ConvertInt_Null_To_Zero();

            return seq;
        }

        public bool Ins_MD_MoldRepairImage()
        {
            query = $@"
                        INSERT INTO MD_MoldRepairImage
                        (
                            CorpId
                            , RepairNo
                            , MoldCode
                            , Seq
                            , Path
                            , FileNm
                            , FileExt
                            , RepairImg
                            , Remark
                            , CreId
                            , CreIP
                            , CreDt
                        )
                        VALUES
                        (
                             '{WrGlobal.CorpID}'
                            ,'{RepairNo}'
                            ,'{MoldCode}'
                            , '{Seq}'
                            , '{Path}'
                            , '{FileNm}'
                            , '{FileExt}'
                            , @Pic
                            , '{Remark}'
                            , '{WrGlobal.LoginID}'
                            , '{WrGlobal.ClientHostName}'
                            , GETDATE()
                        
                        )
                    ";

            return db.ExecuteQueryImage(query, RepairImg);
        }

        public bool Upd_MD_MoldRepairImage()
        {
            query = $@" 
                        UPDATE MD_MoldRepairImage 
                        SET RepairImg = @Pic
                            , Path = '{Path}'
                            , FileNm = '{FileNm}'
                            , FileExt = '{FileExt}'
                            , Remark = '{Remark}'
                            , ModId = '{WrGlobal.LoginID}' 
                            , ModIP = '{WrGlobal.ClientHostName}'
                            , ModDt = GETDATE()
                        WHERE CorpId = '{WrGlobal.CorpID}' 
                        AND MoldCode = '{MoldCode}'
                        AND RepairNo = '{RepairNo}'
                        AND Seq = '{Seq}'
                        ";

            return db.ExecuteQueryImage(query, RepairImg);
        }

        public bool SaveMoldRepairImage()
        {
            query = $@"
                    IF EXISTS (
                        SELECT 1
                          FROM MD_MoldRepairImage
                         WHERE CorpID = '{WrGlobal.CorpID}'
                           AND RepairNo = '{RepairNo}'
                           AND Seq = {Seq}
                           AND MoldCode = '{MoldCode}'
                    )
                    BEGIN
                        UPDATE MD_MoldRepairImage
                           SET RepairImg =  @Pic
                             , ModId = '{WrGlobal.LoginID}'
                             , ModIP = '{WrGlobal.ClientHostName}'
                             , ModDt = GETDATE()
                         WHERE CorpID = '{WrGlobal.CorpID}'
                           AND RepairNo = '{RepairNo}'
                           AND Seq = {Seq}
                           AND MoldCode = '{MoldCode}'
                    END
                    ELSE
                    BEGIN
                        INSERT INTO MD_MoldRepairImage
                        (
                            CorpID
                          , RepairNo
                          , Seq
                          , MoldCode
                          , RepairImg
                          , CreId
                          , CreIP
                          , CreDt
                        )
                        VALUES
                        (
                            '{WrGlobal.CorpID}'
                          , '{RepairNo}'
                          , {Seq}
                          , '{MoldCode}'
                          ,  @Pic
                          , '{WrGlobal.LoginID}'
                          , '{WrGlobal.ClientHostName}'
                          , GETDATE()
                        )
                    END";

            return db.ExecuteQueryImage(query, RepairImg);
        }

        public bool Del_MD_MoldRepairImage()
        {
            query = $@" 
                        DELETE FROM MD_MoldRepairImage 
                        WHERE CorpId = '{WrGlobal.CorpID}' 
                        AND MoldCode = '{MoldCode}'
                        AND RepairNo = '{RepairNo}'
                        AND Seq = '{Seq}'
                        ";

            return db.ExecuteQuery(query);
        }

        public bool Del_item_Image_ByItemCd()
        {
            query = $@" 
                        DELETE FROM MD_MoldRepairImage 
                        WHERE CorpId = '{WrGlobal.CorpID}' 
                        AND MoldCode = '{MoldCode}'
                        AND RepairNo = '{RepairNo}'
                        ";

            return db.ExecuteQuery(query);
        }
    }
}
