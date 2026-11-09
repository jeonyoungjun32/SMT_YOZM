using SmartCommon.Common;
using SmartMES.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DevExpress.XtraEditors.Mask.MaskSettings;

namespace SmartMES.Models.SM
{
	class SM_MenuGroup
	{
        #region Property
        public string CorpId { get; set; }

        public string Id { get; set; }

        public string ParentId { get; set; }

        public string Title_ko { get; set; }

        public int GroupLevel { get; set; }

        public string GroupYn { get; set; }

        public string ExpandYn { get; set; }

        public int RowNum { get; set; }

        public string ProjectName { get; set; }

        public string FolderName { get; set; }

        public string FormName { get; set; }

        public string ParamText { get; set; }

        public string ImageKey { get; set; }

        public string WebImageKey { get; set; }

        public string ProgramType { get; set; }

        public string PopupYn { get; set; }

        public string Bigo { get; set; }

        public string UseYn { get; set; }
        #endregion

        Database db = new Database();
        string query = "";

        public DataTable Select_SM_MenuGroup()
        {
            UseYn = UseYn.ConvertString_Null_To_Empty();

            query = $@"
                SELECT '' AS Status
                     , CorpId
                     , Id
                     , ParentId
                     , Title_ko
                     , GroupLevel
                     , GroupYn
                     , ExpandYn
                     , RowNum
                     , ProjectName
                     , FolderName
                     , FormName
                     , ParamText
                     , ImageKey
                     , WebImageKey
                     , ProgramType
                     , PopupYn
                     , Bigo
                     , UseYn
                     , CreId
                     , CreIP
                     , CreDt
                     , ModId
                     , ModIP
                     , ModDt

                    , '' AS NodeDel --삭제버튼용

                  FROM SM_MenuGroup
                 WHERE CorpId = '{CorpId}'
            ";

            if(UseYn != "" && UseYn.ToUpper() != "ALL")
			{
                query += $"\nAND UseYn = '{UseYn}'";
			}

            query += $"ORDER BY RowNum";

            return db.GetDataTable(query);
        }

        public bool Ins_SM_MenuGroup()
        {
            query = $@"
            INSERT INTO SM_MenuGroup
            (
                  CorpId
                , Id
                , ParentId
                , Title_ko
                , GroupLevel
                , GroupYn
                , ExpandYn
                , RowNum
                , ProjectName
                , FolderName
                , FormName
                , ParamText
                , ImageKey
                , WebImageKey
                , ProgramType
                , PopupYn
                , Bigo
                , UseYn
                , CreId
                , CreIP
                , CreDt
            )
            VALUES
            (
                  '{CorpId}'
                , {Id.ConvertStr_Empty_To_Null()}
                , {ParentId.ConvertStr_Empty_To_Null()}
                , {Title_ko.ConvertStr_Empty_To_Null()}
                , {GroupLevel.ConvertNumeric_Empty_To_Null()}
                , {GroupYn.ConvertStr_Empty_To_Null()}
                , {ExpandYn.ConvertStr_Empty_To_Null()}
                , {RowNum.ConvertNumeric_Empty_To_Null()}
                , {ProjectName.ConvertStr_Empty_To_Null()}
                , {FolderName.ConvertStr_Empty_To_Null()}
                , {FormName.ConvertStr_Empty_To_Null()}
                , {ParamText.ConvertStr_Empty_To_Null()}
                , {ImageKey.ConvertStr_Empty_To_Null()}
                , {WebImageKey.ConvertStr_Empty_To_Null()}
                , {ProgramType.ConvertStr_Empty_To_Null()}
                , {PopupYn.ConvertStr_Empty_To_Null()}
                , {Bigo.ConvertStr_Empty_To_Null()}
                , {UseYn.ConvertStr_Empty_To_Null()}
                , '{WrGlobal.LoginID}'
                , '{WrGlobal.ClientHostName}'
                , GETDATE()
            )";

            return db.ExecuteQuery(query);
        }

        public bool Upd_SM_MenuGroup()
        {
            db.Edit_Property(this);

            query = $@"
            UPDATE SM_MenuGroup
               SET ParentId = {ParentId.ConvertStr_Empty_To_Null()}
                 , Title_ko = {Title_ko.ConvertStr_Empty_To_Null()}
                 , GroupLevel = {GroupLevel.ConvertNumeric_Empty_To_Null()}
                 , GroupYn = {GroupYn.ConvertStr_Empty_To_Null()}
                 , ExpandYn = {ExpandYn.ConvertStr_Empty_To_Null()}
                 , RowNum = {RowNum.ConvertNumeric_Empty_To_Null()}
                 , ProjectName = {ProjectName.ConvertStr_Empty_To_Null()}
                 , FolderName = {FolderName.ConvertStr_Empty_To_Null()}
                 , FormName = {FormName.ConvertStr_Empty_To_Null()}
                 , ParamText = {ParamText.ConvertStr_Empty_To_Null()}
                 , ImageKey = {ImageKey.ConvertStr_Empty_To_Null()}
                 , WebImageKey = {WebImageKey.ConvertStr_Empty_To_Null()}
                 , ProgramType = {ProgramType.ConvertStr_Empty_To_Null()}
                 , PopupYn = {PopupYn.ConvertStr_Empty_To_Null()}
                 , Bigo = {Bigo.ConvertStr_Empty_To_Null()}
                 , UseYn = {UseYn.ConvertStr_Empty_To_Null()}
                 , ModId = '{WrGlobal.LoginID}'
                 , ModIP = '{WrGlobal.ClientHostName}'
                 , ModDt = GETDATE()
             WHERE CorpId = '{CorpId}'
               AND Id = '{Id}'
            ";

            return db.ExecuteQuery(query);
        }

        public bool Del_SM_MenuGroup()
        {
            query = $@"
            DELETE SM_MenuGroup
            WHERE CorpId = '{CorpId}'
            AND Id = '{Id}'

            DELETE SM_MenuGroup
            WHERE CorpId = '{CorpId}'
            AND ParentId = '{Id}'
            ";

            return db.ExecuteQuery(query);
        }


    }
}
