using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartCommon.Common;
using SmartMES.Common;
using System.Data;

namespace SmartMES.Models.AI
{
    class ChatBot_History
    {
        #region Properties
        public string CorpID { get; set;}
        public string KnowledgeSeq      { get; set;}
        public string Category          { get; set;}
        public string Title             { get; set;}
        public string Content         { get; set;}
        public string ChromaCollection  { get; set;}
        public string ChromaDocID       { get; set;}
        public string EmbeddingStatus   { get; set;}
        public string UseYn             { get; set;}
        public string ModifiedBy        { get; set;}
        public string ModifiedDate { get; set;}

        #endregion 

        SmartCommon.Common.Database db = new SmartCommon.Common.Database();
        string query = "";

        public DataTable Select_History()
        {
            Category = Category.ConvertString_Null_To_Empty();
            Title = Title.ConvertString_Null_To_Empty();
            UseYn = UseYn.ConvertString_Null_To_Empty();

            query = $@"
                SELECT ROW_NUMBER() OVER(ORDER BY ModifiedDate DESC, KnowledgeSeq DESC) AS [No]
                     , Category
                     , Title
                     , ModifiedDate
                     , ModifiedBy
                     , EmbeddingStatus
                     , UseYn
                     , KnowledgeSeq
                     , Content
                     , ChromaCollection
                     , ChromaDocID
                  FROM ChatBot_Knowledge_Base
                 WHERE CorpID = '{WrGlobal.CorpID}'";

            if (Category != "" && Category != "ALL")
            {
                query += $" AND Category = '{Category}'";
            }

            if (Title != "")
            {
                query += $" AND Title LIKE '%{Title}%'";
            }

            if (UseYn != "" && UseYn != "ALL")
            {
                query += $" AND UseYn = '{UseYn}'";
            }

            query += " ORDER BY ModifiedDate DESC, KnowledgeSeq DESC";

            return db.GetDataTable(query);
        }

    }
}
