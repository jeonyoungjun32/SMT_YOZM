using SmartCommon.Common;
using System.Data;

namespace SmartMES.Models.MD
{
    public class MD_MoldImage
    {
        #region Property

        public string MoldCode { get; set; }
        public int Seq { get; set; }
        public string ItemCode { get; set; }
        public string Title { get; set; }
        public byte[] Pic { get; set; }
        public byte[] MoldImage1 { get; set; }
        public byte[] MoldImage2 { get; set; }

        #endregion

        Database db = new Database();
        string query = "";

        public DataRow Select_MD_MoldImageList(string mold_Cd)
        {
            query = $@"
                        SELECT
                            CorpId
                            , MoldCode
                            , Seq
                            , ImgCode
                            , Title
                            , MoldImage1
                            , MoldImage2
                            , CreId
                            , CreIP
                            , CreDt
                            , ModId
                            , ModIP
                            , ModDt
                        FROM MD_MoldImage
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND MoldCode = '{mold_Cd}'
                    ";

            return db.GetDataRow(query);
        }

        public DataRow Select_MD_MoldImageBySeq()
        {
            query = $@"
                        SELECT
                            CorpId
                            , MoldCode
                            , Seq
                            , ItemCode
                            , Title
                            , CreId
                            , CreIP
                            , CreDt
                            , ModId
                            , ModIP
                            , ModDt
                        FROM MD_MoldImage
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND MoldCode = '{MoldCode}'
                        AND Seq = {Seq}
                    ";

            return db.GetDataRow(query);
        }

        public byte[] Select_MD_MoldImage()
        {
            query = $@"
                        SELECT Pic FROM MD_MoldImage WHERE CorpId = '{WrGlobal.CorpID}' AND MoldCode = '{MoldCode}' AND Seq = {Seq}
                    ";

            return db.GetImageFromQuery(query);
        }

        public DataRow Get_MD_MoldImageSeq()
        {
            query = $@"
                        SELECT ISNULL(MAX(Seq), 0) + 1 Seq FROM MD_MoldImage WHERE CorpId = '{WrGlobal.CorpID}' AND MoldCode = '{MoldCode}'
                    ";

            return db.GetDataRow(query);
        }

        public bool Ins_MD_MoldImage()
        {
            query = $@"
                        INSERT INTO MD_MoldImage
                        (
                            CorpId
                            , MoldCode
                            , Seq
                            , ItemCode
                            , Title
                            , Pic
                            , CreId
                            , CreIP
                            , CreDt
                        )
                        VALUES
                        (
                            '{WrGlobal.CorpID}'
                            , '{MoldCode}'
                            , {Seq}
                            , '{ItemCode}'
                            , '{Title}'
                            , @Pic
                            , '{WrGlobal.LoginID}'
                            , '{WrGlobal.ClientHostName}'
                            , GETDATE()
                        )
                    ";

            return db.ExecuteQueryImage(query, Pic);
        }

        public bool SaveMoldImage()
        {
            query = $@"
                    IF EXISTS (
                        SELECT 1
                          FROM MD_MoldImage
                         WHERE CorpID = '{WrGlobal.CorpID}'
                           AND MoldCode = '{MoldCode}'
                    )
                    BEGIN
                        UPDATE MD_MoldImage
                           SET MoldImage1 = @Pic1
                             , MoldImage2 = @Pic2
                             , ModId = '{WrGlobal.LoginID}'
                             , ModIP = '{WrGlobal.ClientHostName}'
                             , ModDt = GETDATE()
                         WHERE CorpID = '{WrGlobal.CorpID}'
                           AND MoldCode = '{MoldCode}'
                    END
                    ELSE
                    BEGIN
                        INSERT INTO MD_MoldImage
                        (
                            CorpID
                          , MoldCode
                          , Seq
                          , MoldImage1
                          , MoldImage2
                          , CreId
                          , CreIP
                          , CreDt
                        )
                        VALUES
                        (
                            '{WrGlobal.CorpID}'
                          , '{MoldCode}'
                          , {Seq}
                          , @Pic1
                          , @Pic2
                          , '{WrGlobal.LoginID}'
                          , '{WrGlobal.ClientHostName}'
                          , GETDATE()
                        )
                    END";

            return db.ExecuteQueryImages(query, MoldImage1, MoldImage2);
        }

        public bool Del_MD_MoldImage()
        {
            query = $@"
                        DELETE MD_MoldImage WHERE CorpId = '{WrGlobal.CorpID}' AND MoldCode = '{MoldCode}'
                    ";

            return db.ExecuteQuery(query);
        }

        public bool Del_MD_MoldImageBySeq()
        {
            query = $@"
                        DELETE MD_MoldImage WHERE CorpId = '{WrGlobal.CorpID}' AND MoldCode = '{MoldCode}' AND Seq = {Seq}
                    ";

            return db.ExecuteQuery(query);
        }
    }
}
