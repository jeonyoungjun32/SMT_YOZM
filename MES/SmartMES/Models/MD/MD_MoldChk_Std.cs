using System.Data;
using SmartCommon.Common;
using SmartMES.Common;

namespace SmartMES.Models.MD
{
    public class MD_MoldChk_Std
    {
        #region Property
        public string CorpId { get; set; }

        public string MoldCode { get; set; }

        public int Seq { get; set; }

        public decimal DispSeq { get; set; }

        public string InspectDivCd { get; set; }

        public string ManagementItem { get; set; }

        public string CheckMethod { get; set; }

        public string StandardSpec { get; set; }

        public string CycleDivCd { get; set; }

        public string WeekDivCd { get; set; }

        public string ValueTypeDivCd { get; set; }

        public byte[] ChkImage { get; set; }

        public string InspectOk { get; set; }

        public decimal InspectMin { get; set; }

        public decimal InspectMax { get; set; }

        public string Remark { get; set; }

        #endregion

        Database db = new Database();
        string query;

        public DataTable Sel_Mold_Chk_Std()
        {
            InspectDivCd = InspectDivCd.ConvertString_Null_To_Empty();

            query = $@"
                    SELECT
                        '' AS Status
                        , MS.MoldCode
                        , MD.MoldMgmtNo
                        , MD.MoldCode AS MoldName
                        , MS.Seq
                        , MS.DispSeq
                        , MS.InspectDivCd
                        , MS.ManagementItem
                        , MS.CheckMethod
                        , MS.StandardSpec
                        , MS.CycleDivCd
                        , MS.WeekDivCd
                        , MS.ValueTypeDivCd
                        , MS.ChkImage
                        , MS.InspectOk
                        , MS.InspectMin
                        , MS.InspectMax
                        , MS.Remark
                        , MS.CreId
                        , MS.CreDt
                        , MS.ModId
                        , MS.ModDt
                    
                    FROM MD_MoldChk_Std MS
                    INNER JOIN MD_Mold MD ON MS.CorpId = md.CorpId AND MS.MoldCode = MD.MoldCode
                    WHERE MS.CorpId = '{WrGlobal.CorpID}'
                    ";

            if(InspectDivCd != "")
            {
                query += $"\nAND MS.InspectDivCd = '{InspectDivCd}' ";
            }

            query += "\n ORDER BY MS.MoldCode, MS.DispSeq";

            return db.GetDataTable(query);
        }

        public DataTable Select_Mold_Chk_Std_byCd()
        {
            query = $@"
                        SELECT 
                            '' AS Status
                            , MS.MoldCode 
                            , MS.MoldCode AS MoldName
                            , MD.MoldMgmtNo
                            , MD.MoldName AS R_MoldName
                            , MS.Seq
                            , MS.DispSeq
                            , MS.InspectDivCd
                            , MS.ManagementItem
                            , MS.CheckMethod
                            , MS.StandardSpec
                            , MS.CycleDivCd
                            , MS.WeekDivCd
                            , MS.ValueTypeDivCd
                            , MS.ChkImage
                            , MS.InspectOk
                            , MS.InspectMin
                            , MS.InspectMax
                            , MS.Remark
                            , MS.CreId
                            , MS.CreDt
                            , MS.ModId
                            , MS.ModDt
                    
                        FROM MD_MoldChk_Std MS
                        INNER JOIN MD_Mold MD ON MS.CorpId = md.CorpId AND MS.MoldCode = MD.MoldCode
                        WHERE MS.CorpId = '{WrGlobal.CorpID}'
					    AND MS.InspectDivCd = '{InspectDivCd}'
					    AND MS.MoldCode = '{MoldCode}'
                    ";

            return db.GetDataTable(query);
        }

        public DataTable Select_MoldChk_Std()
        {
            query = $@"      SELECT  
                               chk.MoldCode
							 , chk.InspectDate
                             , chk.InspectDivCd
                             , std.ManagementItem
                             , std.StandardSpec
                             , std.ValueTypeDivCd
                            , CASE WHEN chk.InspectCheck = '10' THEN '○'
						         WHEN chk.InspectCheck = '20' THEN '△'
                                 WHEN chk.InspectCheck = '30' THEN '□'
						         ELSE chk.InspectCheck
				              END InspectCheck
                             , std.CycleDivCd
                             , std.WeekDivCd
                         FROM MD_MoldChk chk
						LEFT JOIN MD_MoldChk_Std std on std.MoldCode = chk.MoldCode   AND std.Seq = chk.Seq 
                        WHERE std.CorpId = '{WrGlobal.CorpID}'
                          AND std.MoldCode = '{MoldCode}' 
                            ";

            return db.GetDataTable(query);
        }

        public DataTable Select_Mold_Chk_List()
        {
            query = $@"SELECT  '신규' AS Status
                             , mcs.MoldCode
                             , Seq
                             , DispSeq
                             , NULL AS InspectDate
                             , ''AS ChkTimeDivCd
                             , InspectDivCd
                             , ManagementItem
                             , CheckMethod
                             , StandardSpec
                             , ValueTypeDivCd
                             --, InspectOk AS InspectCheck
							 , CASE WHEN InspectOk = 'Y' THEN '10'
							          WHEN InspectOk = 'N' THEN '20'
									  ELSE InspectOk
							   END InspectCheck
                             , CycleDivCd
                             , WeekDivCd

							 , '' AS ChargeEmpNo
							 , '' AS ActionResultDivCd
							 , '' AS ActionDivCd
							 , '' AS ActionMemo
							 , '' AS ActionEmpNo
                             
                             --, ChkImage

                             , InspectMin
	                         , InspectMax
                             , mcs.Remark
							 , md.MoldSN
							 , md.MoldName
							 , md.Chasu
							 , md.ProdtCust
							 , md.SupplyCust
                         FROM MD_MoldChk_Std mcs
                   INNER JOIN MD_Mold md on mcs.CorpId = md.CorpId AND mcs.MoldCode = md.MoldCode
                        WHERE mcs.CorpId = '{WrGlobal.CorpID}'
                          AND mcs.InspectDivCd = '{InspectDivCd}'
                            ";

            if (MoldCode != "ALL")
            {
                query += $"\n AND mcs.MoldCode = '{MoldCode}' ";
            }

            if (InspectDivCd == "20")
            {
                query += $"\n AND mcs.WeekDivCd = '{WeekDivCd}'";
            }

            query += "\n ORDER BY mcs.MoldCode, mcs.DispSeq";

            return db.GetDataTable(query);
        }


        public DataTable Select_MD_MoldChk_Std()
        {
            query = $@"SELECT * FROM MD_MoldChk_Std
                        WHERE CorpId = '{WrGlobal.CorpID}'
                          AND MoldCode = '{MoldCode}'
                          AND Seq = {Seq}
                            ";

            return db.GetDataTable(query);
        }


        public bool Ins_Mold_Chk_Std()
        {
            query = $@"
                        INSERT INTO MD_MoldChk_Std
                        (
                            CorpId
                            , MoldCode
                            , DispSeq
                            , InspectDivCd
                            , ManagementItem
                            , CheckMethod
                            , StandardSpec
                            , CycleDivCd
                            , WeekDivCd
                            , ValueTypeDivCd
                            , ChkImage
                            , InspectOk
                            , InspectMin
                            , InspectMax
                            , Remark
                            , CreId
                            , CreIP
                            , CreDt
                        )
                        VALUES 
                        (
                            '{WrGlobal.CorpID}'
                            , '{MoldCode}'
                            , {DispSeq.ConvertDecimal_Empty_To_Null()}
                            , {InspectDivCd.ConvertStr_Empty_To_Null()}
                            , {ManagementItem.ConvertStr_Empty_To_Null()}
                            , {CheckMethod.ConvertStr_Empty_To_Null()}
                            , {StandardSpec.ConvertStr_Empty_To_Null()}
                            , {CycleDivCd.ConvertStr_Empty_To_Null()}
                            , {WeekDivCd.ConvertStr_Empty_To_Null()}
                            , {ValueTypeDivCd.ConvertStr_Empty_To_Null()}
                            , @Pic
                            , {InspectOk.ConvertStr_Empty_To_Null()}
                            , {InspectMin.ConvertDecimal_Empty_To_Null()}
                            , {InspectMax.ConvertDecimal_Empty_To_Null()}
                            , {Remark.ConvertStr_Empty_To_Null()}
                            , '{WrGlobal.LoginID}'
                            , '{WrGlobal.ClientHostName}'
                            , GETDATE()
                        )
                        
                      ";

            return db.ExecuteQueryImage(query, ChkImage);
        }

        public bool Upd_Mold_Chk_Std()
        {
            query = $@"
                        UPDATE MD_MoldChk_Std
                        SET DispSeq = {DispSeq.ConvertInt_Null_To_Zero()}
                          , InspectDivCd = {InspectDivCd.ConvertStr_Empty_To_Null()}
                          , ManagementItem = {ManagementItem.ConvertStr_Empty_To_Null()}
                          , CheckMethod = {CheckMethod.ConvertStr_Empty_To_Null()}
                          , StandardSpec = {StandardSpec.ConvertStr_Empty_To_Null()}
                          , CycleDivCd = {CycleDivCd.ConvertStr_Empty_To_Null()}
                          , WeekDivCd = {WeekDivCd.ConvertStr_Empty_To_Null()}
                          , ValueTypeDivCd = {ValueTypeDivCd.ConvertStr_Empty_To_Null()}
                          , ChkImage= @Pic
                          , InspectOk= {InspectOk.ConvertStr_Empty_To_Null()}
                          , InspectMin= {InspectMin.ConvertDecimal_Empty_To_Null()}
                          , InspectMax= {InspectMax.ConvertDecimal_Empty_To_Null()}
                          , Remark= {Remark.ConvertStr_Empty_To_Null()}
                          , ModId = '{WrGlobal.LoginID}'
                          , ModIP = '{WrGlobal.ClientHostName}'
                          , ModDt = GETDATE()
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND MoldCode = '{MoldCode}'
                        AND Seq = '{Seq}'
                    ";

            return db.ExecuteQueryImage(query, ChkImage);
        }

        public bool Upd_MoldChk_Std()
        {
            query = $@"
                        UPDATE MD_MoldChk_Std
                        SET Remark= {Remark.ConvertStr_Empty_To_Null()}
                          , ModId = '{WrGlobal.LoginID}'
                          , ModIP = '{WrGlobal.ClientHostName}'
                          , ModDt = GETDATE()
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND MoldCode = '{MoldCode}'
                        AND Seq = '{Seq}'
                    ";

            return db.ExecuteQueryImage(query, ChkImage);
        }

        public bool Del_Mold_Chk_Std()
        {
            query = $@"
                        DELETE MD_MoldChk_Std
                         WHERE CorpId = '{WrGlobal.CorpID}'
                           AND MoldCode = '{MoldCode}'
                           AND Seq = '{Seq}'
                    ";

            return db.ExecuteQuery(query);
        }

        public DataRow Select_Mold_Chk_Std_DispSeq()
        {
            query = $@"
                        SELECT TOP 1 DispSeq
                        FROM MD_MoldChk_Std
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND MoldCode = '{MoldCode}'
                        AND InspectDivCd = '{InspectDivCd}'
                        ORDER BY DispSeq DESC
                    ";

            return db.GetDataRow(query);
        }
    }
}
