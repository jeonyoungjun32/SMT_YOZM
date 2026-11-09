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
    class ChatBot_Knowledge_Base
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

        public DataTable Select_Knowledege()
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

        public bool Ins_Knowledege()
        {
            query = $@"
                INSERT INTO ChatBot_Knowledge_Base
                       (CorpID, Category, Title, Content, ChromaCollection,
                        ChromaDocID, EmbeddingStatus, UseYn, ModifiedBy, ModifiedDate)
                VALUES ('{WrGlobal.CorpID}',
                        '{Category}', '{Title}', '{Content}',
                        '{ChromaCollection}', '{ChromaDocID}',
                        '{EmbeddingStatus}', '{UseYn}',
                        '{WrGlobal.LoginID}', GETDATE());

                SELECT CONVERT(bigint, SCOPE_IDENTITY()) AS KnowledgeSeq";

            DataTable result = db.GetDataTable(query);
            if (result == null || result.Rows.Count == 0)
            {
                return false;
            }

            KnowledgeSeq = result.Rows[0]["KnowledgeSeq"].ConvertString_Null_To_Empty();
            return KnowledgeSeq != "";
        }

        public bool Upd_Knowledege()
        {
            db.Edit_Property(this);

            query = $@"
                UPDATE ChatBot_Knowledge_Base
                   SET Category = '{Category}'
                     , Title = '{Title}'
                     , Content = '{Content}'
                     , ChromaCollection = '{ChromaCollection}'
                     , ChromaDocID = '{ChromaDocID}'
                     , EmbeddingStatus = '{EmbeddingStatus}'
                     , UseYn = '{UseYn}'
                     , ModifiedBy = '{WrGlobal.LoginID}'
                     , ModifiedDate = GETDATE()
                 WHERE CorpID = '{WrGlobal.CorpID}'
                   AND KnowledgeSeq = '{KnowledgeSeq}'";

            return db.ExecuteQuery(query);
        }

        public bool Del_Knowledege()
        {
            query = $@"
                DELETE FROM ChatBot_Knowledge_Base
                 WHERE CorpID = '{WrGlobal.CorpID}'
                   AND KnowledgeSeq = '{KnowledgeSeq}'";

            return db.ExecuteQuery(query);
        }

        public bool Upd_Knowledege_EmbeddingStatus(string embeddingStatus)
        {
            EmbeddingStatus = embeddingStatus.ConvertString_Null_To_Empty();
            db.Edit_Property(this);

            query = $@"
                UPDATE ChatBot_Knowledge_Base
                   SET EmbeddingStatus = '{EmbeddingStatus}'
                     , ChromaCollection = '{ChromaCollection}'
                     , ChromaDocID = '{ChromaDocID}'
                     , ModifiedBy = '{WrGlobal.LoginID}'
                     , ModifiedDate = GETDATE()
                 WHERE CorpID = '{WrGlobal.CorpID}'
                   AND KnowledgeSeq = '{KnowledgeSeq}'";

            return db.ExecuteQuery(query);
        }
    }
}
