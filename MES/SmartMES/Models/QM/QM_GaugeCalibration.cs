using DevExpress.Xpo.DB.Helpers;
using DevExpress.XtraRichEdit.Model;
using SmartCommon.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using static DevExpress.Xpo.Helpers.AssociatedCollectionCriteriaHelper;
using static DevExpress.XtraEditors.Mask.MaskSettings;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SmartMES.Models.QM
{
	class QM_GaugeCalibration
	{
		#region Property
		public string CorpId { get; set; }

		public string CalibrationNo { get; set; }

		public string GaugeCode { get; set; }

		public string CalibrationDate { get; set; }

		public string CalibrationCompany { get; set; }

		public string Result { get; set; }

		public string NextDate { get; set; }

		public string CertificateFileID { get; set; }

		public decimal? Cost { get; set; }

		public string UseYn { get; set; }

		public string Remark { get; set; }

		public byte[] CalImage { get; set; }

		public string CreId { get; set; }

		public string CreIP { get; set; }

		public string CreDt { get; set; }

		public string ModId { get; set; }

		public string ModIP { get; set; }

		public string ModDt { get; set; }

		#endregion

		Database db = new Database();
		string query = "";

		public DataTable Select_QM_GaugeCalibration(string startDate, string endDate)
		{
            GaugeCode = GaugeCode.ConvertString_Null_To_Empty();

            query = $@"
            SELECT '' AS Status
                 , gc.CorpId
                 , gc.CalibrationNo

                 , gc.GaugeCode
                 , gg.GaugeName
                 , gg.SerialNo

                 , gc.CalibrationDate
                 , gc.CalibrationCompany
                 , gc.Result
                 , gc.NextDate
                 , gc.CertificateFileID
                 , IIF(ISNULL(gc.CertificateFileID, '') <> '', 'Y', '') AS CfYn

                 , gc.CalImage
                 , gc.Cost
                 , gc.UseYn
                 , gc.Remark
                 , gc.CreId
                 , gc.CreIP
                 , gc.CreDt
                 , gc.ModId
                 , gc.ModIP
                 , gc.ModDt

              FROM QM_GaugeCalibration gc
             LEFT JOIN QM_Gauge gg ON gc.CorpId = gg.CorpId AND gc.GaugeCode = gg.GaugeCode
             WHERE gc.CorpId = '{WrGlobal.CorpID}'
            ";

            if(startDate != "" && endDate != "")
			{
                query += $"\nAND gc.CalibrationDate BETWEEN '{startDate}' AND '{endDate}'";
			}

            if(GaugeCode != "" && GaugeCode != "ALL")
			{
                query += $"\nAND gc.GaugeCode = '{GaugeCode}'";
			}

			return db.GetDataTable(query);
		}

        public DataTable Select_QM_GaugeCal_Certificate(string startDate, string endDate)
        {
            GaugeCode = GaugeCode.ConvertString_Null_To_Empty();

            query = $@"
            SELECT '' AS Status
                 , gc.CorpId
                 , gc.CalibrationNo

                 , gc.GaugeCode
                 , gg.GaugeName
                 , gg.SerialNo

                 , gc.CalibrationDate
                 , gc.CalibrationCompany
                 , gc.Result
                 , gc.NextDate
                 , gc.CertificateFileID
                 , IIF(ISNULL(gc.CertificateFileID, '') <> '', 'Y', '') AS CfYn

                 , gc.CalImage
                 , gc.Cost
                 , gc.UseYn
                 , gc.Remark
                 , gc.CreId
                 , gc.CreIP
                 , gc.CreDt
                 , gc.ModId
                 , gc.ModIP
                 , gc.ModDt

                 , cf.OriFileName
                 , cf.FileName
                 , cf.Path
                 , cf.Title
                 , cf.Capacity

              FROM QM_GaugeCalibration gc
             LEFT JOIN QM_Gauge gg ON gc.CorpId = gg.CorpId AND gc.GaugeCode = gg.GaugeCode
             LEFT JOIN CM_CommonFile cf ON gc.CorpId = cf.CorpId AND gc.CalibrationNo = cf.RefNo
             WHERE gc.CorpId = '{WrGlobal.CorpID}'
            ";

            if (startDate != "" && endDate != "")
            {
                query += $"\nAND gc.CalibrationDate BETWEEN '{startDate}' AND '{endDate}'";
            }

            if (GaugeCode != "" && GaugeCode != "ALL")
            {
                query += $"\nAND gc.GaugeCode = '{GaugeCode}'";
            }

            return db.GetDataTable(query);
        }

        public DataRow Select_LatestCalibration()
        {
            query = $@"
                SELECT TOP 1
                       CalibrationDate
                     , NextDate
                FROM QM_GaugeCalibration
                WHERE CorpId = '{WrGlobal.CorpID}'
                  AND GaugeCode = '{GaugeCode}'
                  AND UseYn = 'Y'
                ORDER BY CalibrationDate DESC
                       , CalibrationNo DESC
            ";

            return db.GetDataRow(query);
        }

        public bool Ins_QM_GaugeCalibration()
		{
			query = $@"
            INSERT INTO QM_GaugeCalibration
            (
                  CorpId
                , CalibrationNo
                , GaugeCode
                , CalibrationDate
                , CalibrationCompany
                , Result
                , NextDate
                , CertificateFileID
                , Cost
                , UseYn
                , Remark
                , CreId
                , CreIP
                , CreDt
            )
            VALUES
            (
                  '{WrGlobal.CorpID}'
                , {CalibrationNo.ConvertStr_Empty_To_Null()}
                , {GaugeCode.ConvertStr_Empty_To_Null()}
                , {CalibrationDate.ConvertStr_Empty_To_Null()}
                , {CalibrationCompany.ConvertStr_Empty_To_Null()}
                , {Result.ConvertStr_Empty_To_Null()}
                , {NextDate.ConvertStr_Empty_To_Null()}
                , {CertificateFileID.ConvertStr_Empty_To_Null()}
                , {Cost.ConvertNumeric_Empty_To_Null()}
                , {UseYn.ConvertStr_Empty_To_Null()}
                , {Remark.ConvertStr_Empty_To_Null()}
                , '{WrGlobal.LoginID}'
                , '{WrGlobal.ClientHostName}'
                , GETDATE()
            )";

			return db.ExecuteQuery(query);
		}

		public bool Upd_QM_GaugeCalibration()
		{
			query = $@"
            UPDATE QM_GaugeCalibration
               SET GaugeCode = {GaugeCode.ConvertStr_Empty_To_Null()}
                 , CalibrationDate = {CalibrationDate.ConvertStr_Empty_To_Null()}
                 , CalibrationCompany = {CalibrationCompany.ConvertStr_Empty_To_Null()}
                 , Result = {Result.ConvertStr_Empty_To_Null()}
                 , NextDate = {NextDate.ConvertStr_Empty_To_Null()}
                 , Cost = {Cost.ConvertNumeric_Empty_To_Null()}
                 , UseYn = {UseYn.ConvertStr_Empty_To_Null()}
                 , Remark = {Remark.ConvertStr_Empty_To_Null()}
                 , ModId = '{WrGlobal.LoginID}'
                 , ModIP = '{WrGlobal.ClientHostName}'
                 , ModDt = GETDATE()
             WHERE CorpId = '{WrGlobal.CorpID}'
               AND CalibrationNo = '{CalibrationNo}'
            ";

			return db.ExecuteQuery(query);
		}

		public bool Del_QM_GaugeCalibration()
		{
			query = $@"
            DELETE QM_GaugeCalibration
             WHERE CorpId = '{WrGlobal.CorpID}'
               AND CalibrationNo = '{CalibrationNo}'
            ";

			return db.ExecuteQuery(query);
		}

        public bool Del_QM_GaugeCalibration_ByGaugeCode()
        {
            query = $@"
            DELETE QM_GaugeCalibration
             WHERE CorpId = '{WrGlobal.CorpID}'
               AND GaugeCode = '{GaugeCode}'
            ";

            return db.ExecuteQuery(query);
        }
    }
}
