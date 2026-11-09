using SmartCommon.Common;
using SmartMES.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMES.Models.EQ
{
	class EQ_MachineImage
	{
        #region Property
        public string CorpId { get; set; }

        public string MachineCode { get; set; }

        public int Seq { get; set; }

        public string Title { get; set; }

        public byte[] Image { get; set; }

        public string CreId { get; set; }

        public string CreIP { get; set; }

        public DateTime CreDt { get; set; }

        public string ModId { get; set; }

        public string ModIP { get; set; }

        public DateTime ModDt { get; set; }
        #endregion

        Database db = new Database();
		string query = "";

        public DataTable SelectImage()
		{
            MachineCode = MachineCode.ConvertString_Null_To_Empty();

            query = $@"
                SELECT '' AS Status
                    , img.CorpId
                    , img.MachineCode
                    , img.Seq
                    , img.Title
                    , img.Image
                    , img.CreId
                    , img.CreIP
                    , img.CreDt
                    , img.ModId
                    , img.ModIP
                    , img.ModDt
                FROM EQ_Machineimage img
                WHERE img.CorpId = '{CorpId}'
            ";

            if(MachineCode != "")
			{
                query += $"\nAND img.MachineCode = '{MachineCode}'";
			}

            return db.GetDataTable(query);
		}

        public DataTable SelectImageByMachineCode()
        {
            MachineCode = MachineCode.ConvertString_Null_To_Empty();

            query = $@"
                SELECT '' AS Status
                    , img.CorpId
                    , img.MachineCode
                    , img.Seq
                    , img.Title
                    , img.Image
                    , img.CreId
                    , img.CreIP
                    , img.CreDt
                    , img.ModId
                    , img.ModIP
                    , img.ModDt
                FROM EQ_Machineimage img
                WHERE img.CorpId = '{CorpId}'
                AND img.MachineCode = '{MachineCode}'
            ";

            return db.GetDataTable(query);
        }

        public bool Ins_Image()
		{
            query = $@"

                INSERT INTO EQ_Machineimage
                (
                    CorpId
                    , MachineCode
                    , Seq
                    , Title
                    , Image
                    , CreId
                    , CreIP
                    , CreDt
                )
                VALUES
                (
                    '{CorpId}'
                    , {MachineCode.ConvertStr_Empty_To_Null()}
                    , {Seq.ConvertInt_Empty_To_Null()}
                    , {Title.ConvertStr_Empty_To_Null()}
                    , @Pic
                    , '{WrGlobal.LoginID}'
                    , '{WrGlobal.ClientHostName}'
                    , GETDATE()
                );
            ";

            return db.ExecuteQueryImage(query, Image);
		}

        public bool Upd_Image()
        {
            query = $@"

                UPDATE EQ_Machineimage SET
                    Title     = {Title.ConvertStr_Empty_To_Null()}
                    , Image   = @Pic
                    , ModId   = '{WrGlobal.LoginID}'
                    , ModIP   = '{WrGlobal.ClientIpAddress}'
                    , ModDt   = GETDATE()
                WHERE CorpId = '{CorpId}'
                  AND MachineCode = '{MachineCode}'
                  AND Seq = '{Seq}'
            ";

            return db.ExecuteQueryImage(query, Image);
        }

        public bool Del_Image()
        {
            query = $@"
              DELETE FROM EQ_Machineimage
              WHERE CorpId = '{CorpId}'
                AND MachineCode = '{MachineCode}'
                AND Seq = '{Seq}'

            ";

            return db.ExecuteQuery(query);
        }

        public bool Del_ImageBy_MachineCd()
        {
            query = $@"
              DELETE FROM EQ_Machineimage
              WHERE CorpId = '{CorpId}'
                AND MachineCode = '{MachineCode}'
            ";

            return db.ExecuteQuery(query);
        }
    }
}
