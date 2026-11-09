using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using System.Drawing.Drawing2D;
using SmartMES.Models.ChatBot;
using System.Data;
using SmartMES.Common;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.IO;
using System.Text.RegularExpressions;
using SmartCommon.Common;
//using SmartMES.Models.AI;
using SmartMES.Common.AIBase;

namespace SmartMES.View.Chatbot
{
    public partial class FrmChatbot_AI_TEST : XtraForm
    {
        #region 상태값
        private string _currentIntent = null;   // 대화 흐름
        private int _sessionNo = 0;
        private string _workPromptText;
        private string _schemaScript;
        private Dictionary<string, string> _tableDescriptions;
        private Dictionary<string, TableSchema> _schemas;
        #endregion

        AIRequest request = new AIRequest();
        SmartCommon.Common.Database db = new SmartCommon.Common.Database();

        class ChatItem
        {
            public bool IsUser { get; set; }
            public string Text { get; set; }
            public bool IsLink { get; set; }
            public string Time { get; set; } = DateTime.Now.ToString("HH:mm");

            public long? ChatSeq { get; set; }      // DB 저장용 시퀀스
            public bool CanFeedback { get; set; } = true;   // 👍👎 표시 여부
            public string FeedBackYn { get; set; }      // "Y" or "N" or null

            public override string ToString() => Text;
        }

        class TableSchema
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public List<ColumnSchema> Columns { get; set; } = new List<ColumnSchema>();
            public List<string> PrimaryKeys { get; set; } = new List<string>();
            public List<string> ForeignKeys { get; set; } = new List<string>();
        }

        class ColumnSchema
        {
            public string Name { get; set; }
            public string DataType { get; set; }
            public string Nullable { get; set; }
        }

        class ChatSessionItem
        {
            public long ChatSeq { get; set; }
            public int SessionNo { get; set; }
            public string Text { get; set; }

            public override string ToString() => Text;
        }


        public FrmChatbot_AI_TEST()
        {
            InitializeComponent();

            InitOllama();
            InitChatUI();
            InitChatEvents();
            InitChatContextMenu();
        }

        private void InitOllama()
        {
            string qwen2_5 = "qwen2.5:7b";
            string qwen3 = "qwen3:latest";
            string date = DateTime.Today.ToString("yyyy-MM-dd");

            request.type = RequestType.AIDocs;
            request.dbName = Database.DbName;
            request.structure = StructureType.Conditional;
            AIOptions firstStep = new AIOptions();
            firstStep.id = "FIRST_STEP";
            firstStep.api = API.Ollama;
            firstStep.model = qwen2_5;

            AIConditional firstConditional = new AIConditional();
            firstConditional.conditionFormat = "findYn";
            firstStep.conditional = firstConditional;

            AICustomFormat firstStep_ConditionFormat = new AICustomFormat();
            firstStep_ConditionFormat.name = "findYn";
            firstStep_ConditionFormat.type = "boolean";
            firstStep_ConditionFormat.description = "true If Find Table Info";

            AICustomFormat firstStep_ResponseFormat = new AICustomFormat();
            firstStep_ResponseFormat.name = "tableInfoList";
            firstStep_ResponseFormat.type = "string";
            firstStep_ResponseFormat.description = "Table Info List";
            firstStep_ResponseFormat.isResponse = true;
            firstStep.customFormat = new AICustomFormat[] { firstStep_ResponseFormat, firstStep_ConditionFormat };

            AIMessage firstPrompt = new AIMessage();
            firstPrompt.role = Role.System;
            firstPrompt.content =
$@"
[Role]
1.Check user's message and write table info that user need.
2.Write table info each line for each table.(column name is on same line with table name.)
3.Table info consists of english table name and korean column name.
4.Must write table name and whole column name.
[Example]
tableInfoList : [VW_SdOrder] 수주테이블 [사업장번호, 수주번호, 순번, 수주일자, 납기일자, 수주처코드, 수주처명, 품목코드, 품목명, 수주수량, 단가, 금액]
[Now Date]
{date}
[Table Info List]
[VW_SdOrder] 수주테이블 [사업장번호, 수주번호, 순번, 수주일자, 납기일자, 수주처코드, 수주처명, 품목코드, 품목명, 수주수량, 단가, 금액]
[VW_Sales] 출하테이블 [사업장번호, 출하번호, 순번, 출하일자, 매출처코드, 매출처명, 납품처코드, 납품처명, 품목코드, 품목명, 출하수량, 단가, 금액]
[VW_ProductPlan] 생산테이블 [사업장번호, 계획번호, 계획일자, 품목코드, 품목명, 계획수량, 생산수량, 양품수량, 불량수량]
";
            firstStep.messages.Add(firstPrompt);

            AIOptions secondStep = new AIOptions();
            secondStep.id = "SECOND_STEP";
            secondStep.api = API.Ollama;
            secondStep.model = qwen3;
            secondStep.useDatabaseWithResponse = true;
            secondStep.useBeforeAnswer = true;
            secondStep.think = "low";

            AICustomFormat secondFormat = new AICustomFormat();
            secondFormat.name = "query";
            secondFormat.type = "string";
            secondFormat.description = "created query";
            secondStep.customFormat = new AICustomFormat[] { secondFormat };

            AIConditional secondConditional = new AIConditional();
            secondStep.conditional = secondConditional;

            AIMessage secondPrompt = new AIMessage();
            secondPrompt.role = Role.System;
            secondPrompt.content =
$@"
[Role]
1.Check user's message and write SQL query with table info for MSSQL.
2.All of column's name are korean.
[Now Date]
{date}
[Table Info List]
{{0}}
";
            secondStep.messages.Add(secondPrompt);

            AINext next1 = new AINext();
            next1.condition = "true";
            next1.id = "SECOND_STEP";

            firstConditional.nextSequence.Add(next1);

            AIOptions notFoundTable = new AIOptions();
            notFoundTable.id = "NOT_FOUND_STEP";
            notFoundTable.api = API.Ollama;
            notFoundTable.model = qwen2_5;
            notFoundTable.useDatabaseWithResponse = false;
            notFoundTable.useBeforeAnswer = false;
            notFoundTable.reasonForFailConditon = "사용자지정 실패";
            notFoundTable.fixedMessage = "Just Say Hello, World!";

            AIMessage notFoundTablePrompt = new AIMessage();
            notFoundTablePrompt.role = Role.System;
            notFoundTablePrompt.content =
$@"
[Role]
1.Check user's message.
2.Say that cannot find data about user's request.
";
            notFoundTable.messages.Add(notFoundTablePrompt);

            AINext next2 = new AINext();
            next2.condition = "false";
            next2.id = "NOT_FOUND_STEP";
            firstConditional.nextSequence.Add(next2);

            AIOptions finalStep = new AIOptions();
            finalStep.id = "FINAL_STEP";
            finalStep.api = API.Ollama;
            finalStep.model = qwen2_5;
            finalStep.useBeforeAnswer = true;

            AIMessage finalPrompt = new AIMessage();
            finalPrompt.role = Role.System;
            finalPrompt.content =
$@"
[Role]
1.Check user's message and report with data.
2.data was ultimately analizyed result.
3.Report in korean.
4.Never use Markdown formatting like **bold**. NEVER.
5.Use summary.
6.Never show data list directly.
7.If '사업장코드' is in data, do not report about that.
[Now Date]
{date}
[Data]
{{0}}
";
            finalStep.messages.Add(finalPrompt);

            AINext finalNext = new AINext();
            finalNext.id = "FINAL_STEP";

            secondConditional.nextSequence.Add(finalNext);

            request.ai_list.Add(firstStep);
            request.ai_list.Add(secondStep);
            request.ai_list.Add(notFoundTable);
            request.ai_list.Add(finalStep);
        }

        private void InitChatContextMenu()
        {
            var contextMenu = new ContextMenuStrip();

            var copyItem = new ToolStripMenuItem("📋 복사");

            contextMenu.Items.Add(copyItem);

            listBoxControlChat.ContextMenuStrip = contextMenu;

            // 우클릭한 채팅 항목 선택
            listBoxControlChat.MouseDown += (s, e) =>
            {
                if (e.Button != MouseButtons.Right)
                    return;

                Point pt =
                    listBoxControlChat.PointToClient(
                        Control.MousePosition
                    );

                int index =
                    listBoxControlChat.IndexFromPoint(pt);

                if (index < 0)
                    return;

                listBoxControlChat.SelectedIndex = index;
                listBoxControlChat.Focus();
            };

            // 복사
            copyItem.Click += (s, e) =>
            {
                CopySelectedChatItem();
            };
        }

        private void InitChatEvents()
        {
            listBoxControlChat.MeasureItem += ListBoxControlChat_MeasureItem;
            listBoxControlChat.DrawItem += ListBoxControlChat_DrawItem;

            listBoxControlChat.Resize += (s, e) =>
            {
                listBoxControlChat.Invalidate();
            };

            memoEditInput.KeyDown += MemoEditInput_KeyDown;

            btnSend.Click += async (s, e) =>
            {
                await Send();
            };
        }

        private void InitChatUI()
        {
            this.FormBorderStyle = FormBorderStyle.Sizable;

            listBoxControlChat.ItemAutoHeight = true;
            listBoxControlChat.Appearance.TextOptions.WordWrap = WordWrap.Wrap;

            AddBot(
                "안녕하세요! 👋\n" +
                "저는 SmartMES AI 도우미입니다.\n" +
                "편하게 물어봐 주세요!\n\n" +
                "예시 질문:\n" +
                "• A품목 재고 얼마나 남았어?\n" +
                "• 오늘 생산량 알려줘\n" +
                "• 설비 현황 어때?\n" +
                "• 오늘 작업지시 뭐야?"
            );
        }

        private void CopySelectedChatItem()
        {
            Point pt = listBoxControlChat.PointToClient(Control.MousePosition);
            int index =listBoxControlChat.IndexFromPoint(pt);

            if (index < 0)
                return;

            var item = listBoxControlChat.Items[index];

            string text = item is ChatItem chat ? chat.Text : item?.ToString();

            if (string.IsNullOrWhiteSpace(text))
                return;

            Clipboard.SetText(text);
        }

        #region Ollama API   

        private string ExtractText(string json)
        {
            try
            {
                var obj = JObject.Parse(json);

                string response = obj["response"]?.ToString();
                if (!string.IsNullOrEmpty(response))
                    return response.Replace("**", "").Replace("* ", "• ").Replace("# ", "").Trim();

                string chatResponse = obj["message"]?["content"]?.ToString();
                if (!string.IsNullOrEmpty(chatResponse))
                    return chatResponse.Replace("**", "").Replace("* ", "• ").Replace("# ", "").Trim();

                string error = obj["error"]?.ToString();
                if (!string.IsNullOrEmpty(error))
                    return $"Ollama API 오류: {error}";

                return "응답 없음";
            }
            catch (Exception ex)
            {
                return "파싱 실패: " + ex.Message;
            }
        }
        #endregion


        #region 핵심 Send
        private async Task Send()
        {
            string question = memoEditInput.Text.Trim();
            if (string.IsNullOrEmpty(question))
                return;

            // 사용자 질문 출력
            AddUser(question);
            memoEditInput.Text = "";

            // 로딩 표시
            int loadingIndex = listBoxControlChat.Items.Count;
            AddBot("답변 생성중...");

            try
            {
                // Python 챗봇 API 호출
                string answer = await CallChatbotApi(question);

                // 로딩 제거
                if (loadingIndex < listBoxControlChat.Items.Count)
                    listBoxControlChat.Items.RemoveAt(loadingIndex);

                // DB 저장 (SaveChatHistory 내부에서 _sessionNo 사용)
                //long? chatSeq = SaveChatHistory(question, answer);

                // 화면 출력
                AddBot(answer, null);
                //if (chatSeq.HasValue)
                //{
                //    AddLatestChat(question, 0);
                //}
            }
            catch (Exception ex)
            {
                if (loadingIndex < listBoxControlChat.Items.Count)
                    listBoxControlChat.Items.RemoveAt(loadingIndex);

                AddBot("오류가 발생했습니다.\r\n" + ex.Message);
            }
        }

        // 추가: 대화 이력 DB 저장, 성공 시 ChatSeq 반환
        private long? SaveChatHistory(string question, string answer)
        {
            try
            {
                string sql = $@"
                                INSERT INTO ChatBot_History
                                (
                                    CorpId,
                                    SessionNo,
                                    EmployeeId,
                                    Question,
                                    Answer,
                                    CreId,
                                    CreIP,
                                    CreDt
                                )
                                VALUES
                                (
                                    '{WrGlobal.CorpID}',
                                    {_sessionNo},
                                    '{WrGlobal.LoginID}',
                                    N'{question.Replace("'", "''")}',
                                    N'{answer.Replace("'", "''")}',
                                    '{WrGlobal.LoginID}',
                                    '{WrGlobal.HostName}',
                                    GETDATE()
                                );
                                
                                SELECT ISNULL(SCOPE_IDENTITY(), 0) AS NewSeq;";

                DataTable dt = db.GetDataTable(sql);

                if (dt != null && dt.Rows.Count > 0)
                {
                    long seq = Convert.ToInt64(dt.Rows[0]["NewSeq"]);
                    return seq > 0 ? (long?)seq : null;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        private async Task<string> CallChatbotApi(string userText)
        {
            for (int i = 0; i < request.ai_list.Count; i++)
            {
                if (request.ai_list[i].messages.Last().role != Role.System)
                {
                    request.ai_list[i].messages.RemoveAt(request.ai_list[i].messages.Count - 1);
                }

                AIMessage msg = new AIMessage();
                msg.role = Role.User;
                msg.content = userText;
                request.ai_list[i].messages.Add(msg);
            }

            AIResponse response = await AIRequester.SendRequest(request);
            if (response.success)
            {
                return response.result;
            }

            return "처리 중 오류가 발생했습니다.";
        }

        private Dictionary<string, string> ParseTableDescriptions(string promptText)
        {
            var descriptions = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            if (string.IsNullOrWhiteSpace(promptText))
                return descriptions;

            var matches = Regex.Matches(promptText, @"^\s*-\s*(?<table>[A-Za-z0-9_]+)\s+(?<desc>.+?)\s*$", RegexOptions.Multiline);
            foreach (Match match in matches)
            {
                string tableName = match.Groups["table"].Value.Trim();
                string description = match.Groups["desc"].Value.Trim();

                if (!string.IsNullOrWhiteSpace(tableName) && !descriptions.ContainsKey(tableName))
                    descriptions.Add(tableName, description);
            }

            return descriptions;
        }

        private Dictionary<string, TableSchema> ParseCreateTableSchemas(string schemaScript, Dictionary<string, string> descriptions)
        {
            var schemas = new Dictionary<string, TableSchema>(StringComparer.OrdinalIgnoreCase);

            if (string.IsNullOrWhiteSpace(schemaScript))
                return schemas;

            var tableMatches = Regex.Matches(
                schemaScript,
                @"CREATE\s+TABLE\s+\[dbo\]\.\[(?<table>[^\]]+)\]\s*\((?<body>.*?)\r?\n\)\s+ON\s+\[",
                RegexOptions.IgnoreCase | RegexOptions.Singleline);

            foreach (Match tableMatch in tableMatches)
            {
                string tableName = tableMatch.Groups["table"].Value.Trim();
                string body = tableMatch.Groups["body"].Value;

                var tableSchema = new TableSchema
                {
                    Name = tableName,
                    Description = descriptions.ContainsKey(tableName) ? descriptions[tableName] : ""
                };

                foreach (string rawLine in body.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None))
                {
                    string line = rawLine.Trim().TrimEnd(',');
                    if (!line.StartsWith("["))
                        continue;

                    Match columnMatch = Regex.Match(
                        line,
                        @"^\[(?<column>[^\]]+)\]\s+(?<type>\[[^\]]+\](?:\s*\([^)]+\))?)\s*(?<nullable>NOT\s+NULL|NULL)?",
                        RegexOptions.IgnoreCase);

                    if (!columnMatch.Success)
                        continue;

                    tableSchema.Columns.Add(new ColumnSchema
                    {
                        Name = columnMatch.Groups["column"].Value.Trim(),
                        DataType = columnMatch.Groups["type"].Value.Replace("[", "").Replace("]", "").Trim(),
                        Nullable = Regex.Replace(columnMatch.Groups["nullable"].Value.Trim(), @"\s+", " ").ToUpper()
                    });
                }

                Match pkMatch = Regex.Match(body, @"PRIMARY\s+KEY.*?\((?<columns>.*?)\)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                if (pkMatch.Success)
                {
                    foreach (Match columnMatch in Regex.Matches(pkMatch.Groups["columns"].Value, @"\[(?<column>[^\]]+)\]"))
                    {
                        string columnName = columnMatch.Groups["column"].Value.Trim();
                        if (!tableSchema.PrimaryKeys.Contains(columnName))
                            tableSchema.PrimaryKeys.Add(columnName);
                    }
                }

                schemas[tableName] = tableSchema;
            }

            return schemas;
        }

        private void ApplyForeignKeys(string schemaScript, Dictionary<string, TableSchema> schemas)
        {
            if (string.IsNullOrWhiteSpace(schemaScript) || schemas == null || schemas.Count == 0)
                return;

            var fkMatches = Regex.Matches(
                schemaScript,
                @"ALTER\s+TABLE\s+\[dbo\]\.\[(?<fromTable>[^\]]+)\].*?FOREIGN\s+KEY\s*\((?<fromColumns>.*?)\)\s*\r?\nREFERENCES\s+\[dbo\]\.\[(?<toTable>[^\]]+)\]\s*\((?<toColumns>.*?)\)",
                RegexOptions.IgnoreCase | RegexOptions.Singleline);

            foreach (Match fkMatch in fkMatches)
            {
                string fromTable = fkMatch.Groups["fromTable"].Value.Trim();
                string toTable = fkMatch.Groups["toTable"].Value.Trim();

                if (!schemas.ContainsKey(fromTable))
                    continue;

                string fromColumns = string.Join(", ", Regex.Matches(fkMatch.Groups["fromColumns"].Value, @"\[(?<column>[^\]]+)\]")
                    .Cast<Match>()
                    .Select(m => m.Groups["column"].Value.Trim()));
                string toColumns = string.Join(", ", Regex.Matches(fkMatch.Groups["toColumns"].Value, @"\[(?<column>[^\]]+)\]")
                    .Cast<Match>()
                    .Select(m => m.Groups["column"].Value.Trim()));

                string relation = $"{fromTable}({fromColumns}) -> {toTable}({toColumns})";
                if (!schemas[fromTable].ForeignKeys.Contains(relation))
                    schemas[fromTable].ForeignKeys.Add(relation);
            }
        }

        private string BuildRelevantSchemaText(Dictionary<string, TableSchema> schemas, Dictionary<string, string> descriptions, string userText)
        {
            var selectedTables = SelectRelevantTables(schemas, descriptions, userText);
            ExpandTablesByCommonRelations(selectedTables, schemas);

            var sb = new StringBuilder();
            sb.AppendLine("아래 스키마에 포함된 테이블과 컬럼만 사용하세요.");
            sb.AppendLine("질문에 필요한 테이블 또는 컬럼이 없으면 canQuery=false로 반환하세요.");

            if (selectedTables.Count == 0)
            {
                sb.AppendLine();
                sb.AppendLine("[테이블 설명 목록]");
                foreach (var item in descriptions.OrderBy(x => x.Key))
                    sb.AppendLine($"- {item.Key}: {item.Value}");
                return sb.ToString();
            }

            sb.AppendLine();
            sb.AppendLine("[선별 테이블 스키마]");
            foreach (string tableName in selectedTables.OrderBy(x => x))
            {
                if (!schemas.ContainsKey(tableName))
                    continue;

                TableSchema table = schemas[tableName];
                string description = string.IsNullOrWhiteSpace(table.Description) ? "" : $" - {table.Description}";

                sb.AppendLine($"- {table.Name}{description}");
                if (table.PrimaryKeys.Count > 0)
                    sb.AppendLine($"  PK: {string.Join(", ", table.PrimaryKeys)}");

                sb.AppendLine("  Columns:");
                foreach (ColumnSchema column in table.Columns)
                {
                    string nullable = string.IsNullOrWhiteSpace(column.Nullable) ? "" : $" {column.Nullable}";
                    sb.AppendLine($"    - {column.Name}: {column.DataType}{nullable}");
                }

                if (table.ForeignKeys.Count > 0)
                {
                    sb.AppendLine("  FK:");
                    foreach (string foreignKey in table.ForeignKeys)
                        sb.AppendLine($"    - {foreignKey}");
                }
            }

            return sb.ToString();
        }

        private HashSet<string> SelectRelevantTables(Dictionary<string, TableSchema> schemas, Dictionary<string, string> descriptions, string userText)
        {
            var selectedTables = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            if (schemas == null || schemas.Count == 0 || string.IsNullOrWhiteSpace(userText))
                return selectedTables;

            string normalizedText = userText.Trim();
            string lowerText = normalizedText.ToLower();
            var terms = Regex.Split(normalizedText, @"[\s,./\\|:;()\[\]{}'""!?]+")
                .Where(x => !string.IsNullOrWhiteSpace(x) && x.Trim().Length >= 2)
                .Select(x => x.Trim())
                .Distinct()
                .ToList();

            foreach (var schema in schemas)
            {
                string tableName = schema.Key;
                string description = descriptions.ContainsKey(tableName) ? descriptions[tableName] : "";

                if (lowerText.Contains(tableName.ToLower()))
                {
                    selectedTables.Add(tableName);
                    continue;
                }

                if (!string.IsNullOrWhiteSpace(description) && terms.Any(t => description.Contains(t)))
                    selectedTables.Add(tableName);
            }

            AddTablesByKeyword(selectedTables, normalizedText);

            return selectedTables;
        }

        private void AddTablesByKeyword(HashSet<string> selectedTables, string userText)
        {
            AddIfContains(selectedTables, userText, new[] { "생산실적", "실적", "생산량", "작업수량", "양품", "불량", "ng" },
                new[] { "PM_OrderResult", "PM_OrderResultWorker", "PM_OrderProcess", "PM_ProductPlan", "CM_Item", "CM_Line", "CM_Process" });

            AddIfContains(selectedTables, userText, new[] { "작업지시", "지시", "오더", "작업오더" },
                new[] { "PM_OrderProcess", "PM_ProductPlan", "CM_Item", "CM_Line", "CM_Process" });

            AddIfContains(selectedTables, userText, new[] { "재고", "현재고", "수불", "남은 수량" },
                new[] { "ST_StockM", "ST_StockD", "CM_Item", "CM_Warehouse" });

            AddIfContains(selectedTables, userText, new[] { "품목", "제품", "자재", "item" },
                new[] { "CM_Item" });

            AddIfContains(selectedTables, userText, new[] { "거래처", "고객", "고객사", "업체" },
                new[] { "CM_Cust" });

            AddIfContains(selectedTables, userText, new[] { "설비", "장비", "라인", "가동", "비가동" },
                new[] { "EQ_Machine", "EQ_Operation", "CM_Line" });

            AddIfContains(selectedTables, userText, new[] { "공정" },
                new[] { "CM_Process", "CM_Routing", "CM_RoutingRev" });

            AddIfContains(selectedTables, userText, new[] { "수주", "주문" },
                new[] { "BM_SdOrderM", "BM_SdOrderD", "CM_Item", "CM_Cust" });

            AddIfContains(selectedTables, userText, new[] { "출고", "매출" },
                new[] { "BM_SalesM", "BM_SalesD", "CM_Item", "CM_Cust" });

            AddIfContains(selectedTables, userText, new[] { "발주" },
                new[] { "MM_PlacingM", "MM_PlacingD", "CM_Item", "CM_Cust" });

            AddIfContains(selectedTables, userText, new[] { "입고", "구매" },
                new[] { "MM_PurchaseM", "MM_PurchaseD", "CM_Item", "CM_Cust", "CM_Warehouse" });

            AddIfContains(selectedTables, userText, new[] { "검사", "품질" },
                new[] { "QM_ExamResult", "QM_ExamMgnt", "QM_ItemExamMgnt", "QM_WorkDefect", "CM_Item" });

            AddIfContains(selectedTables, userText, new[] { "금형" },
                new[] { "MD_Mold", "MD_MoldItem", "MD_MoldIn", "MD_MoldOut", "MD_MoldInOut", "MD_MoldRepair" });

            AddIfContains(selectedTables, userText, new[] { "작업자", "담당자", "사용자" },
                new[] { "SM_User", "PM_OrderResultWorker", "CM_LineProcessWorker" });
        }

        private void AddIfContains(HashSet<string> selectedTables, string userText, string[] keywords, string[] tableNames)
        {
            if (selectedTables == null || string.IsNullOrWhiteSpace(userText))
                return;

            if (!keywords.Any(keyword => userText.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0))
                return;

            foreach (string tableName in tableNames)
                selectedTables.Add(tableName);
        }

        private void ExpandTablesByCommonRelations(HashSet<string> selectedTables, Dictionary<string, TableSchema> schemas)
        {
            if (selectedTables == null || schemas == null || selectedTables.Count == 0)
                return;

            var additionalTables = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (string tableName in selectedTables.ToList())
            {
                if (!schemas.ContainsKey(tableName))
                    continue;

                var columnNames = new HashSet<string>(schemas[tableName].Columns.Select(c => c.Name), StringComparer.OrdinalIgnoreCase);

                if (columnNames.Contains("ItemCode"))
                    additionalTables.Add("CM_Item");
                if (columnNames.Contains("CustCode"))
                    additionalTables.Add("CM_Cust");
                if (columnNames.Contains("WarehouseCode"))
                    additionalTables.Add("CM_Warehouse");
                if (columnNames.Contains("LineCode"))
                    additionalTables.Add("CM_Line");
                if (columnNames.Contains("ProcessCode"))
                    additionalTables.Add("CM_Process");
                if (columnNames.Contains("MachineCode"))
                    additionalTables.Add("EQ_Machine");
                if (columnNames.Contains("UserId") || columnNames.Contains("WorkerCode") || columnNames.Contains("WorkerId"))
                    additionalTables.Add("SM_User");

                foreach (string foreignKey in schemas[tableName].ForeignKeys)
                {
                    Match match = Regex.Match(foreignKey, @"->\s*(?<table>[A-Za-z0-9_]+)\(");
                    if (match.Success)
                        additionalTables.Add(match.Groups["table"].Value);
                }
            }

            foreach (string tableName in additionalTables)
            {
                if (schemas.ContainsKey(tableName))
                    selectedTables.Add(tableName);
            }
        }

        private string GetChatbotFilePath(string fileName)
        {
            var candidates = new List<string>();

            candidates.Add(Path.Combine(Application.StartupPath, "View", "Chatbot", "Chatbot_File", fileName));
            candidates.Add(Path.Combine(Application.StartupPath, "Chatbot_File", fileName));

            DirectoryInfo dir = new DirectoryInfo(Application.StartupPath);
            for (int i = 0; i < 6 && dir != null; i++)
            {
                candidates.Add(Path.Combine(dir.FullName, "View", "Chatbot", "Chatbot_File", fileName));
                candidates.Add(Path.Combine(dir.FullName, "MES", "SmartMES", "View", "Chatbot", "Chatbot_File", fileName));
                dir = dir.Parent;
            }

            foreach (string path in candidates.Distinct())
            {
                if (File.Exists(path))
                    return path;
            }

            throw new FileNotFoundException($"챗봇 파일을 찾을 수 없습니다. 파일명: {fileName}");
        }

        private string ExtractJsonText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return "";

            string cleaned = text.Replace("```json", "").Replace("```", "").Trim();
            int start = cleaned.IndexOf('{');
            if (start < 0)
                return cleaned;

            int depth = 0;
            bool inString = false;
            bool escape = false;

            for (int i = start; i < cleaned.Length; i++)
            {
                char c = cleaned[i];

                if (escape)
                {
                    escape = false;
                    continue;
                }

                if (c == '\\' && inString)
                {
                    escape = true;
                    continue;
                }

                if (c == '"')
                {
                    inString = !inString;
                    continue;
                }

                if (inString)
                    continue;

                if (c == '{')
                    depth++;
                else if (c == '}')
                {
                    depth--;
                    if (depth == 0)
                        return cleaned.Substring(start, i - start + 1);
                }
            }

            return cleaned;
        }

        private string GetAiQueryResult(string sql)
        {
            string checkedSql = CleanSqlText(sql);

            if (!IsSafeSelectSql(checkedSql, out string message))
                return message;

            DataTable dt = db.GetDataTable(checkedSql);
            return ConvertDataTableToText(dt);
        }

        private string CleanSqlText(string sql)
        {
            if (string.IsNullOrWhiteSpace(sql))
                return "";

            string cleaned = sql.Replace("```sql", "")
                                .Replace("```", "")
                                .Trim();

            if (cleaned.EndsWith(";"))
                cleaned = cleaned.Substring(0, cleaned.Length - 1).Trim();

            return cleaned;
        }

        private bool IsSafeSelectSql(string sql, out string message)
        {
            message = "";

            if (string.IsNullOrWhiteSpace(sql))
            {
                message = "SQL 검증 실패: 실행할 쿼리가 없습니다.";
                return false;
            }

            if (!Regex.IsMatch(sql, @"^\s*SELECT\b", RegexOptions.IgnoreCase))
            {
                message = "SQL 검증 실패: SELECT 문만 실행할 수 있습니다.";
                return false;
            }

            if (sql.Contains(";") || sql.Contains("--") || sql.Contains("/*") || sql.Contains("*/"))
            {
                message = "SQL 검증 실패: 여러 문장 또는 주석이 포함된 쿼리는 실행할 수 없습니다.";
                return false;
            }

            string forbiddenPattern = @"\b(INSERT|UPDATE|DELETE|MERGE|DROP|ALTER|TRUNCATE|EXEC|EXECUTE|CREATE|GRANT|REVOKE|BACKUP|RESTORE|DBCC|USE|DECLARE|WAITFOR|OPENROWSET|OPENDATASOURCE)\b";
            if (Regex.IsMatch(sql, forbiddenPattern, RegexOptions.IgnoreCase))
            {
                message = "SQL 검증 실패: 조회 외 명령어가 포함되어 실행할 수 없습니다.";
                return false;
            }

            return true;
        }

        private string ConvertDataTableToText(DataTable dt)
        {
            if (dt == null)
                return "DB 조회 결과를 가져오지 못했습니다.";

            if (dt.Rows.Count == 0)
                return "조회된 데이터가 없습니다.";

            var sb = new StringBuilder();
            int maxRows = Math.Min(dt.Rows.Count, 20);

            sb.AppendLine($"조회 결과: {dt.Rows.Count:N0}건");
            sb.AppendLine("컬럼: " + string.Join(", ", dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName)));

            for (int i = 0; i < maxRows; i++)
            {
                DataRow row = dt.Rows[i];
                var values = new List<string>();

                foreach (DataColumn col in dt.Columns)
                {
                    string value = row[col] == DBNull.Value ? "" : Convert.ToString(row[col]);
                    values.Add($"{col.ColumnName}={value}");
                }

                sb.AppendLine($"{i + 1}. {string.Join(", ", values)}");
            }

            if (dt.Rows.Count > maxRows)
                sb.AppendLine($"외 {dt.Rows.Count - maxRows:N0}건");

            return sb.ToString();
        }

        private string GetDbResult(string intent, string keyword)
        {
            /*
            string workDate = ParseWorkDate(keyword);

            switch (intent)
            {
                case "Stock":   //재고
                    cb.Material = keyword;

                    var dt = cb.Select_StockListView_Material();

                    if (dt == null || dt.Rows.Count == 0)
                        return string.IsNullOrEmpty(keyword) ? "재고 데이터 없음" : $"'{keyword}' 재고 없음";

                    var sb = new StringBuilder();

                    sb.AppendLine($"📦 재고 현황 ({dt.Rows.Count:N0}건)");

                    foreach (DataRow row in dt.Rows)
                    {
                        sb.AppendLine(
                            $"• [{row["ItemCode"]}] {row["ItemName"]} : " +
                            $"{row["StockQty"].ConvertInt_Null_To_Zero():N0} {row["UnitCode"]}"
                        );
                    }

                    return sb.ToString();

                case "OrderResult": //실적
                    cb.WorkDate = workDate;

                    dt = cb.Select_PM_OrderResult();

                    if (dt == null || dt.Rows.Count == 0)
                        return string.IsNullOrEmpty(keyword) ? "생산실적 데이터 없음" : $"{keyword} 생산실적 데이터 없음";

                    sb = new StringBuilder();
                    sb.AppendLine($"📋 {(string.IsNullOrEmpty(keyword) ? "전체" : keyword)} 생산실적 ({dt.Rows.Count:N0}건)");

                    foreach (DataRow row in dt.Rows)
                    {
                        sb.AppendLine(
                            $"• {row["ItemName"]} | " +
                            $"생산:{row["WorkQty"].ConvertInt_Null_To_Zero():N0} / " +
                            $"양품:{row["GoodQty"].ConvertInt_Null_To_Zero():N0}" +
                            $"불량:{row["NgQty"].ConvertInt_Null_To_Zero():N0}"
                        );
                    }

                    return sb.ToString();

                case "WorkOrder": //지시
                    cb.WorkDate = workDate;

                    dt = cb.Select_PM_OrderProcess();

                    if (dt == null || dt.Rows.Count == 0)
                        return string.IsNullOrEmpty(keyword)
                            ? "작업지시 데이터 없음"
                            : $"{keyword} 작업지시 데이터 없음";

                    int total = dt.Rows.Count;

                    int working = dt.AsEnumerable()
                                    .Count(r => r["Status"]?.ToString() != "완료");

                    int complete = dt.AsEnumerable()
                                     .Count(r => r["Status"]?.ToString() == "완료");

                    sb = new StringBuilder();

                    sb.AppendLine($"📋 {(string.IsNullOrEmpty(keyword) ? "전체" : keyword)} 작업지시");
                    sb.AppendLine($"총 {total:N0}건 | 작업중 {working:N0}건 | 완료 {complete:N0}건");

                    foreach (DataRow row in dt.Rows)
                    {
                        sb.AppendLine(
                            $"• {row["ItemName"]} | " +
                            $"지시:{row["OrderQty"].ConvertInt_Null_To_Zero():N0} / " +
                            $"작업:{row["WorkQty"].ConvertInt_Null_To_Zero():N0} / " +
                            $"상태:{row["Status"]}"
                        );
                    }

                    return sb.ToString();

                case "Machine":
                    return "설비 현황 조회 (구현 필요)";

                default:
                    return "";
            }
            */
            return "";
        }

        private string ParseWorkDate(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return "";

            keyword = keyword.Trim();

            if (keyword.Contains("오늘") || keyword.Contains("금일") || keyword.Contains("당일") || keyword.Contains("현재") || keyword.Contains("today"))
            {
                return DateTime.Now.ToString("yyyyMMdd");
            }

            if (keyword.Contains("어제") || keyword.Contains("전일"))
            {
                return DateTime.Now.AddDays(-1).ToString("yyyyMMdd");
            }

            if (keyword.Contains("내일"))
            {
                return DateTime.Now.AddDays(1).ToString("yyyyMMdd");
            }

            return keyword.Replace("-", "").Replace(".", "").Replace("/", "");
        }

        #endregion

        #region UI
        private void AddUser(string text)
        {
            listBoxControlChat.Items.Add(new ChatItem { IsUser = true, Text = text });
            listBoxControlChat.TopIndex = listBoxControlChat.ItemCount - 1;
        }
        private void AddLatestChat(string text, long chatSeq)
        {
            lboxLatestChatList.Items.Add(new ChatSessionItem
            {
                ChatSeq = chatSeq,
                SessionNo = _sessionNo,
                Text = text
            });
            lboxLatestChatList.TopIndex = lboxLatestChatList.ItemCount - 1;
        }

        private void AddBot(string text)
        {
            listBoxControlChat.Items.Add(new ChatItem { IsUser = false, Text = text });
            listBoxControlChat.TopIndex = listBoxControlChat.ItemCount - 1;
        }

        private void AddBot(string text, long? chatSeq)
        {
            var item = new ChatItem
            {
                IsUser = false,
                Text = text,
                ChatSeq = chatSeq,
                CanFeedback = chatSeq.HasValue  // ChatSeq 있으면 피드백 가능
            };

            listBoxControlChat.Items.Add(item);
            listBoxControlChat.TopIndex = listBoxControlChat.ItemCount - 1;
        }

        private void MemoEditInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !e.Shift)
            {
                e.SuppressKeyPress = true;
                _ = Send();
            }
        }

        private GraphicsPath GetRoundedRect(Rectangle bounds, int radius)
        {
            var path = new GraphicsPath();
            int d = radius * 2;

            var arc = new Rectangle(bounds.Location, new Size(d, d));
            path.AddArc(arc, 180, 90);

            arc.X = bounds.Right - d;
            path.AddArc(arc, 270, 90);

            arc.Y = bounds.Bottom - d;
            path.AddArc(arc, 0, 90);

            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }

        private void ListBoxControlChat_DrawItem(object sender, ListBoxDrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            var item = listBoxControlChat.Items[e.Index] as ChatItem;
            if (item == null) return;

            bool isUser = item.IsUser;
            string text = item.Text ?? "";

            e.Appearance.DrawBackground(e.Cache, e.Bounds);

            int padding = 10;
            int avatarSize = 36;
            int avatarMargin = 8;
            int timeWidth = 35; // 시간 표시 공간

            int leftMargin = !isUser ? avatarSize + avatarMargin * 2 : timeWidth + 10;
            int rightMargin = isUser ? timeWidth + 10 : 10;
            int maxWidth = e.Bounds.Width - leftMargin - rightMargin;
            int textMaxWidth = maxWidth - (padding * 2);

            var textSize = e.Appearance.CalcTextSize(e.Cache, text, textMaxWidth).ToSize();
            int bubbleWidth = Math.Max(60, Math.Min(textSize.Width + padding * 2, maxWidth));
            int bubbleHeight = textSize.Height + padding * 2;

            int avatarY = e.Bounds.Top + 4;

            if (!isUser)
            {
                var avatarRect = new Rectangle(e.Bounds.Left + avatarMargin, avatarY, avatarSize, avatarSize);
                var old2 = e.Graphics.SmoothingMode;
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var avatarBrush = new SolidBrush(Color.FromArgb(99, 99, 102)))
                    e.Graphics.FillEllipse(avatarBrush, avatarRect);
                using (var font = new Font(e.Appearance.Font.FontFamily, 8, FontStyle.Bold))
                {
                    var initSize = TextRenderer.MeasureText("SMT", font);
                    var initRect = new Rectangle(
                        avatarRect.Left + (avatarSize - initSize.Width) / 2,
                        avatarRect.Top + (avatarSize - initSize.Height) / 2,
                        initSize.Width, initSize.Height);
                    TextRenderer.DrawText(e.Graphics, "SMT", font, initRect, Color.White);
                }
                e.Graphics.SmoothingMode = old2;
            }

            int x = isUser
                ? e.Bounds.Right - bubbleWidth - timeWidth - 10
                : e.Bounds.Left + avatarSize + avatarMargin * 2;

            var bubbleRect = new Rectangle(x, e.Bounds.Top + 4, bubbleWidth, bubbleHeight);
            var textRect = new Rectangle(
                bubbleRect.Left + padding,
                bubbleRect.Top + padding,
                bubbleRect.Width - padding * 2,
                bubbleRect.Height - padding * 2);

            var old = e.Graphics.SmoothingMode;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using (var path = GetRoundedRect(bubbleRect, 12))
            using (var brush = new SolidBrush(isUser ? Color.FromArgb(0, 122, 255) : Color.FromArgb(229, 229, 234)))
                e.Graphics.FillPath(brush, path);

            var flags = TextFormatFlags.WordBreak | TextFormatFlags.NoPadding;
            TextRenderer.DrawText(e.Graphics, text, e.Appearance.Font, textRect,
                isUser ? Color.White : Color.Black, flags);

            // 시간
            string timeText = item.Time ?? DateTime.Now.ToString("HH:mm");
            using (var timeFont = new Font(e.Appearance.Font.FontFamily, 7, FontStyle.Regular))
            {
                var timeSize = TextRenderer.MeasureText(timeText, timeFont);
                int timeX = isUser
                    ? bubbleRect.Left - timeSize.Width - 4
                    : bubbleRect.Right + 4;
                int timeY = bubbleRect.Bottom - timeSize.Height - 2;
                TextRenderer.DrawText(e.Graphics, timeText, timeFont,
                    new Rectangle(timeX, timeY, timeSize.Width, timeSize.Height), Color.Gray);
            }

            e.Graphics.SmoothingMode = old;
            e.Handled = true;
        }

        private void ListBoxControlChat_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= listBoxControlChat.Items.Count)
                return;

            ChatItem item = listBoxControlChat.Items[e.Index] as ChatItem;
            if (item == null)
                return;

            string text = item.Text ?? "";

            int padding = 10;
            int avatarSize = 36;
            int avatarMargin = 8;
            bool isUser = item.IsUser;

            int leftMargin = !isUser ? avatarSize + avatarMargin * 2 : 10;
            int rightMargin = isUser ? 10 : 20;

            int maxWidth = Math.Max(100, e.ItemWidth - leftMargin - rightMargin - 40);
            int textWidth = maxWidth - (padding * 2) - 20;

            var textSize = TextRenderer.MeasureText(
                text,
                listBoxControlChat.Appearance.Font,
                new Size(textWidth, int.MaxValue),
                TextFormatFlags.WordBreak | TextFormatFlags.NoPadding
            );

            e.ItemHeight = textSize.Height + padding * 2 + 30;
        }
        #endregion

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void smtSearchButton1_Click(object sender, EventArgs e)
        {
            _sessionNo = GetNewSessionNo();

            listBoxControlChat.Items.Clear();
            lboxLatestChatList.Items.Clear();
            AddBot("안녕하세요! 👋\n저는 SmartMES AI 도우미입니다.\n편하게 물어봐 주세요!\n\n예시 질문:\n• A품목 재고 얼마나 남았어?\n• 오늘 생산량 알려줘\n• 설비 현황 어때?\n• 오늘 작업지시 뭐야?");
        }

        private int GetNewSessionNo()
        {
            string sql = @"
                           SELECT ISNULL(MAX(SessionNo), 0) + 1 AS SessionNo
                             FROM ChatBot_History
                           WHERE CorpId = '" + WrGlobal.CorpID + "'";

            DataTable dt = db.GetDataTable(sql);

            if (dt != null && dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0]["SessionNo"]);
            }

            return 1;
        }

        private void FrmChatbot_AI_Load(object sender, EventArgs e)
        {
            _sessionNo = GetNewSessionNo();
        }

        private void LoadChatHistory(int sessionNo, long targetChatSeq)
        {
            listBoxControlChat.Items.Clear();


            string sql = $@"
                             SELECT
                                   ChatSeq
                                 , Question
                                 , Answer
                             FROM ChatBot_History
                             WHERE EmployeeId = '{WrGlobal.LoginID}'
                               AND SessionNo = {sessionNo}
                             ORDER BY ChatSeq ASC";

            DataTable dt = db.GetDataTable(sql);

            if (dt == null || dt.Rows.Count == 0)
                return;
            int targetIndex = -1;

            foreach (DataRow dr in dt.Rows)
            {
                long chatSeq = Convert.ToInt64(dr["ChatSeq"]);

                // 현재 ChatSeq가 왼쪽에서 선택한 질문이면
                if (chatSeq == targetChatSeq)
                {
                    // 질문과 답변 중 질문 위치를 기억
                    targetIndex = listBoxControlChat.ItemCount;
                }

                AddUser(dr["Question"].ToString());

                AddBot(dr["Answer"].ToString());
            }

            // 선택한 질문 위치로 이동
            if (targetIndex >= 0 &&
                targetIndex < listBoxControlChat.ItemCount)
            {
                listBoxControlChat.TopIndex = targetIndex;
            }
        }

        private void lboxLatestChatList_MouseClick(object sender, MouseEventArgs e)
        {
            int index = lboxLatestChatList.IndexFromPoint(e.Location);

            if (index < 0)
                return;

            ChatSessionItem session =
                lboxLatestChatList.Items[index] as ChatSessionItem;

            if (session == null)
                return;

            _sessionNo = session.SessionNo;

            LoadChatHistory(
                session.SessionNo,
                session.ChatSeq
            );
        }
    }
}
