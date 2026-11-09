using DevExpress.Xpo.DB.Helpers;
using SmartCommon.Common;
using SmartMES.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMES.Models.CM
{
	public class CM_BOMRev
	{
        #region Property
        public string CorpId { get; set; }

        public string ItemCode { get; set; }

        public string Revision { get; set; }

        public string RevisionName { get; set; }

        public string RoutingCode { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string Remark { get; set; }

        public string UseYn { get; set; }

        public string CreId { get; set; }

        public string CreIP { get; set; }

        public string CreDt { get; set; }

        public string ModId { get; set; }

        public string ModIP { get; set; }

        public string ModDt { get; set; }


        #endregion

        Database db = new Database();
        string query = "";

        public DataTable SelectBomMaster()
        {
            Revision = Revision.ConvertString_Null_To_Empty();
            UseYn = UseYn.ConvertString_Null_To_Empty();

            query = $@"
            SELECT 
                  bm.CorpId
                , bm.ItemCode
                , bm.Revision
                , bm.RevisionName
                , bm.RoutingCode
                , bm.StartDate
                , bm.EndDate
                , bm.Remark
                , bm.UseYn
                , bm.CreId
                , bm.CreIP
                , bm.CreDt
                , bm.ModId
                , bm.ModIP
                , bm.ModDt
                , '' AS btnDelete
            FROM CM_BOMRev bm
            WHERE bm.CorpId = '{CorpId}'
            AND bm.ItemCode = '{ItemCode}'
          
            ";

            if (UseYn != "")
            {
                query += $"\nAND bm.UseYn = '{UseYn}'";
            }

            if (Revision!= "")
			{
                query += $"\nAND bm.Revision = '{Revision}'";
			}

            query += $"\nORDER BY TRY_CONVERT(int, Revision) DESC, Revision DESC";

            return db.GetDataTable(query);
        }

        public DataRow SelectBomMasterInfo()
        {
            Revision = Revision.ConvertString_Null_To_Empty();
            UseYn = UseYn.ConvertString_Null_To_Empty();

            query = $@"
            SELECT 
                  bm.CorpId
                , bm.ItemCode
                , bm.Revision
                , bm.RevisionName
                , bm.RoutingCode
                , bm.StartDate
                , bm.EndDate
                , bm.Remark
                , bm.UseYn
                , bm.CreId
                , bm.CreIP
                , bm.CreDt
                , bm.ModId
                , bm.ModIP
                , bm.ModDt
            FROM CM_BOMRev bm
            WHERE bm.CorpId = '{CorpId}'
            AND bm.ItemCode = '{ItemCode}'
            AND bm.Revision = '{Revision}'
            ";

            return db.GetDataRow(query);
        }

        public DataTable SelectBomMasterLup()
		{
            query = $@"
            SELECT bm.Revision, bm.RevisionName, bm.RoutingCode
            FROM CM_BOMRev bm
            WHERE CorpId = '{CorpId}'
            AND ItemCode = '{ItemCode}'
            AND UseYn = 'Y'
            ORDER BY Revision DESC
            ";

            return db.GetDataTable(query);
        }

        public string GetNextRevision()
        {
            int revision = 0;

            query = $@"
            SELECT MAX(Revision) AS MaxRevision 
            FROM CM_BOMRev
            WHERE CorpId = '{CorpId}'
            AND ItemCode = '{ItemCode}'
            ";

            object maxRevision = db.GetDataScalar(query);
            revision = maxRevision.ConvertInt_Null_To_Zero();

            return (revision + 1).ToString("D3");
        }

        public bool ExistsBOMMasterByRoutingCode()
        {
            query = $@"
                SELECT COUNT(*) AS CNT
                FROM CM_BOMRev
                WHERE CorpId = '{CorpId}'
                AND ItemCode = '{ItemCode}'
                AND RoutingCode = '{RoutingCode}'
            ";

            DataRow dr = db.GetDataRow(query);
            int cnt = 0;

            if (dr != null)
            {
                cnt = dr["CNT"].ConvertInt_Null_To_Zero();
            }

            if (cnt > 0)
            {
                query = $@"
                    UPDATE CM_BOMRev
                    SET UseYn = 'Y'
                    WHERE CorpId = '{CorpId}'
                    AND ItemCode = '{ItemCode}'
                    AND RoutingCode = '{RoutingCode}'
                ";

                db.ExecuteQuery(query);
                return true;
            }

            return false;
        }

        public bool ExistsBOMMaster()
		{
            query = $@"
                SELECT COUNT(*) AS CNT
                FROM CM_BOMRev
                WHERE CorpId = '{CorpId}'
                AND ItemCode = '{ItemCode}'
                AND UseYn = 'Y'
            ";

            DataRow dr = db.GetDataRow(query);

            int cnt = 0;

            if(dr != null)
			{
                cnt = dr["CNT"].ConvertInt_Null_To_Zero();
			}

            return cnt == 0 ? false : true;
		}

        public bool InsertBomMaster()
		{
            query = $@"
            INSERT INTO CM_BOMRev
            (
                CorpId
                , ItemCode
                , Revision
                , RevisionName
                , RoutingCode
                , StartDate
                , EndDate
                , Remark
                , UseYn
                , CreId
                , CreIP
                , CreDt
            )
            VALUES
            (
                '{WrGlobal.CorpID}'
                , {ItemCode.ConvertStr_Empty_To_Null()}
                , {Revision.ConvertStr_Empty_To_Null()}
                , {RevisionName.ConvertStr_Empty_To_Null()}
                , {RoutingCode.ConvertStr_Empty_To_Null()}
                , {StartDate.ConvertStr_Empty_To_Null()}
                , {EndDate.ConvertStr_Empty_To_Null()}
                , {Remark.ConvertStr_Empty_To_Null()}
                , {UseYn.ConvertStr_Empty_To_Null()}
                , '{WrGlobal.LoginID}'
                , '{WrGlobal.ClientHostName}'
                , GETDATE()
            );
            
            ";

            return db.ExecuteQuery(query);
		}

        public bool InsertBomMaster_SourceCode(string sourceCode, string sourceRevision)
        {
            query = $@"
            INSERT INTO CM_BOMRev
            (
                CorpId
                , ItemCode
                , Revision
                , RevisionName
                , RoutingCode
                , StartDate
                , EndDate
                , Remark
                , UseYn
                , CreId
                , CreIP
                , CreDt
            )
              SELECT '{WrGlobal.CorpID}', '{ItemCode}', '{Revision}', RevisionName, RoutingCode, StartDate, EndDate, Remark, UseYn
                , '{WrGlobal.LoginID}', '{WrGlobal.ClientHostName}', GETDATE()
                    FROM CM_BOMRev
                    WHERE CorpId = '{WrGlobal.CorpID}' AND ItemCode = '{sourceCode}' AND Revision= '{sourceRevision}'
            
            
            ";

            return db.ExecuteQuery(query);
        }

        public bool Upd_CM_BOMRev()
        {
            query = $@"
                UPDATE CM_BOMRev
                   SET RevisionName = {RevisionName.ConvertStr_Empty_To_Null()}
                     , RoutingCode = {RoutingCode.ConvertStr_Empty_To_Null()}
                     , StartDate = {StartDate.ConvertStr_Empty_To_Null()}
                     , EndDate = {EndDate.ConvertStr_Empty_To_Null()}
                     , Remark = {Remark.ConvertStr_Empty_To_Null()}
                     , UseYn = {UseYn.ConvertStr_Empty_To_Null()}
                     , ModId = '{WrGlobal.LoginID}'
                     , ModIP = '{WrGlobal.ClientHostName}'
                     , ModDt = GETDATE()
                 WHERE CorpId = '{WrGlobal.CorpID}'
                   AND ItemCode = '{ItemCode}'
                   AND Revision = '{Revision}'
                ";

            return db.ExecuteQuery(query);
        }


        public bool UpdateBomRev_UseYn_ByItemCode()
        {
            query = $@"
                UPDATE CM_BOMRev
                SET UseYn = 'N'
                WHERE CorpId = '{WrGlobal.CorpID}'
                AND ItemCode = '{ItemCode}'
            ";

            return db.ExecuteQuery(query);
        }

        public bool UpdateBomRev_UseYn_ByRoutingCode()
        {
            query = $@"
                UPDATE CM_BOMRev
                SET UseYn = 'N'
                WHERE CorpId = '{CorpId}'
                AND ItemCode = '{ItemCode}'
                AND RoutingCode <> '{RoutingCode}'
            ";

            return db.ExecuteQuery(query);
        }

        public bool DeleteBomRev()
        {
            query = $@"

                DECLARE  
                  @pCorpId nvarchar(10)      = '{WrGlobal.CorpID}'
                , @pProductCode nvarchar(30) = '{ItemCode}'
                , @pRevision nvarchar(10)    = '{Revision}'

                -- CmBom 데이터 삭제
	            DELETE FROM CM_BOM 
                WHERE CorpId = @pCorpId 
                AND PItemCode = @pProductCode 
                AND Revision = @pRevision

	            --CmMaterial Routing 정보 Null로 삭제
	            -- 삭제할 리비전이 기본 리비전이면 품목 테이블 revision = null
	            IF EXISTS 
                (
                    SELECT Revision 
                    FROM CM_BOMRev 
                    WHERE CorpId = @pCorpId 
                    AND ItemCode = @pProductCode 
                    AND Revision = @pRevision 
                    AND UseYn = 'Y'
                )
	            BEGIN

	            	UPDATE CM_Item 
                    SET Revision = null, RoutingCode = null 
                    WHERE CorpId = @pCorpId
                    AND ItemCode = @pProductCode 

	            END

                --BOM리비전 데이터 삭제
                DELETE FROM CM_BOMRev 
                WHERE CorpId = @pCorpId 
                AND ItemCode = @pProductCode 
                AND Revision = @pRevision 

            ";

            return db.ExecuteQuery(query);
        }
    }
}
