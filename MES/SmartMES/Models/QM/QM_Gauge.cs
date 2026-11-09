using DevExpress.Xpo.DB.Helpers;
using SmartCommon.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMES.Models.QM
{
	class QM_Gauge
	{
        #region Property
        public string CorpId { get; set; }

        public string GaugeCode { get; set; }

        public string GaugeName { get; set; }

        public string GaugeType { get; set; }

        public string SerialNo { get; set; }

        public string Maker { get; set; }

        public string PurchaseDate { get; set; }

        public int? CalCycleDay { get; set; }

        public string LastCalDate { get; set; }

        public string NextCalDate { get; set; }

        public string Remark { get; set; }

        public byte[] GaugeImage { get; set; }
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

        public DataTable Select_QM_Gauge()
        {
            query = $@"
            SELECT '' AS Status
                 , CorpId
                 , GaugeCode
                 , GaugeName
                 , GaugeType
                 , SerialNo
                 , Maker
                 , PurchaseDate
                 , CalCycleDay
                 , LastCalDate
                 , NextCalDate
                 , Remark
                 , GaugeImage
                 , UseYn
                 , CreId
                 , CreIP
                 , CreDt
                 , ModId
                 , ModIP
                 , ModDt
              FROM QM_Gauge
             WHERE CorpId = '{WrGlobal.CorpID}'";

            return db.GetDataTable(query);
        }

        public DataRow Select_Gauge_Image()
        {
            query = $@"
                        SELECT
                            GaugeImage
                        FROM QM_Gauge
                        WHERE CorpId = '{WrGlobal.CorpID}'
                        AND GaugeCode = '{GaugeCode}'
                    ";

            return db.GetDataRow(query);
        }

        public DataTable Select_QM_Gauge_Schedule()
        {
            query = $@"
             SELECT
                  GaugeCode
                , GaugeName
                , SerialNo
                , CalCycleDay
                , LastCalDate
                , NextCalDate
            
                , DATEDIFF(DAY, GETDATE(), NextCalDate) AS RemainDay
            
                , CASE
                    WHEN NextCalDate IS NULL
                        THEN '미등록'
            
                    WHEN NextCalDate < CAST(GETDATE() AS DATE)
                        THEN '기한초과'
            
                    WHEN NextCalDate <= DATEADD(DAY, 30, CAST(GETDATE() AS DATE))
                        THEN '교정예정'
            
                    ELSE '정상'
                  END AS CalibrationStatus
            
                , UseYn
            FROM QM_Gauge
            WHERE CorpId = '{WrGlobal.CorpID}'
              AND UseYn = 'Y'
            ORDER BY NextCalDate
            ";

            return db.GetDataTable(query);
        }

        public bool Ins_QM_Gauge()
        {
            query = $@"
            INSERT INTO QM_Gauge
            (
                  CorpId
                , GaugeCode
                , GaugeName
                , GaugeType
                , SerialNo
                , Maker
                , PurchaseDate
                , CalCycleDay
                , LastCalDate
                , NextCalDate
                , Remark
                , CreId
                , CreIP
                , CreDt
            )
            VALUES
            (
                  '{WrGlobal.CorpID}'
                , {GaugeCode.ConvertStr_Empty_To_Null()}
                , {GaugeName.ConvertStr_Empty_To_Null()}
                , {GaugeType.ConvertStr_Empty_To_Null()}
                , {SerialNo.ConvertStr_Empty_To_Null()}
                , {Maker.ConvertStr_Empty_To_Null()}
                , {PurchaseDate.ConvertStr_Empty_To_Null()}
                , {CalCycleDay.ConvertNumeric_Empty_To_Null()}
                , {LastCalDate.ConvertStr_Empty_To_Null()}
                , {NextCalDate.ConvertStr_Empty_To_Null()}
                , {Remark.ConvertStr_Empty_To_Null()}
                , '{WrGlobal.LoginID}'
                , '{WrGlobal.ClientHostName}'
                , GETDATE()
            )";

            return db.ExecuteQuery(query);
        }

        public bool Upd_QM_Gauge()
        {
            query = $@"
            UPDATE QM_Gauge
               SET GaugeName = {GaugeName.ConvertStr_Empty_To_Null()}
                 , GaugeType = {GaugeType.ConvertStr_Empty_To_Null()}
                 , SerialNo = {SerialNo.ConvertStr_Empty_To_Null()}
                 , Maker = {Maker.ConvertStr_Empty_To_Null()}
                 , PurchaseDate = {PurchaseDate.ConvertStr_Empty_To_Null()}
                 , CalCycleDay = {CalCycleDay.ConvertNumeric_Empty_To_Null()}
                 , LastCalDate = {LastCalDate.ConvertStr_Empty_To_Null()}
                 , NextCalDate = {NextCalDate.ConvertStr_Empty_To_Null()}
                 , Remark = {Remark.ConvertStr_Empty_To_Null()}
                 , UseYn = '{UseYn}'
                 , ModId = '{WrGlobal.LoginID}'
                 , ModIP = '{WrGlobal.ClientHostName}'
                 , ModDt = GETDATE()
             WHERE CorpId = '{WrGlobal.CorpID}'
               AND GaugeCode = '{GaugeCode}'
            ";

            return db.ExecuteQuery(query);
        }

        public bool Upd_QM_Gauge_Image()
        {
            query = $@"UPDATE QM_Gauge
                          SET GaugeImage = @Pic

                             , ModId = '{WrGlobal.LoginID}'
                             , ModIP = '{WrGlobal.ClientHostName}'
                             , ModDt = GETDATE()

                        WHERE CorpId = '{WrGlobal.CorpID}'
                          AND GaugeCode = '{GaugeCode}'";

            return db.ExecuteQueryImage(query, GaugeImage);
        }

        public bool Upd_CalibrationDate()
        {
            query = $@"
                UPDATE QM_Gauge
                SET
                      LastCalDate = {LastCalDate.ConvertStr_Empty_To_Null()}
                    , NextCalDate = {NextCalDate.ConvertStr_Empty_To_Null()}
                    , ModId       = '{WrGlobal.LoginID}'
                    , ModIP       = '{WrGlobal.ClientIpAddress}'
                    , ModDt       = GETDATE()
                WHERE CorpId = '{WrGlobal.CorpID}'
                  AND GaugeCode = '{GaugeCode}'
            ";

            return db.ExecuteQuery(query);
        }

        public bool Del_QM_Gauge()
        {
            query = $@"
            DELETE QM_Gauge
             WHERE CorpId = '{WrGlobal.CorpID}'
               AND GaugeCode = '{GaugeCode}'
            ";

            return db.ExecuteQuery(query);
        }

    }
}
