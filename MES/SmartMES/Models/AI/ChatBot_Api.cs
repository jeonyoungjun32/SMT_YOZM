using System;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using SmartCommon.Common;
using SmartMES.Common;

namespace SmartMES.Models.AI
{
    class ChatBot_Api 
    {
        private static readonly HttpClient ApiClient = CreateApiClient();

        private static HttpClient CreateApiClient()
        {
            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromMinutes(5);
            return client;
        }

        public async Task<string> CallChatbot(string question)
        {
            JObject requestBody = new JObject
            {
                ["question"] = question,
                ["corp_id"] = WrGlobal.CorpID,
                ["database_name"] = WrGlobal.DBName
            };

            JObject responseBody = await PostAsync("/chat", requestBody);
            string answer = responseBody["answer"]?.ToString();
            if (string.IsNullOrWhiteSpace(answer))
            {
                throw new InvalidOperationException("챗봇 API 응답에 답변이 없습니다.");
            }

            return answer;
        }

        public async Task UpsertKnowledge(ChatBot_Knowledge_Base knowledgeBase)
        {
            JObject requestBody = new JObject
            {
                ["database_name"] = WrGlobal.DBName,
                ["corp_id"] = WrGlobal.CorpID,
                ["knowledge_seq"] = knowledgeBase.KnowledgeSeq,
                ["category"] = knowledgeBase.Category,
                ["title"] = knowledgeBase.Title,
                ["content"] = knowledgeBase.Content,
                ["use_yn"] = knowledgeBase.UseYn
            };

            await PostAsync("/knowledge/upsert", requestBody);
        }

        public async Task DeleteKnowledge(string knowledgeSeq)
        {
            JObject requestBody = new JObject
            {
                ["database_name"] = WrGlobal.DBName,
                ["knowledge_seq"] = knowledgeSeq
            };

            await PostAsync("/knowledge/delete", requestBody);
        }

        public async Task<bool> CheckConnection()
        {
            string apiUrl = GetApiUrl();
            HttpResponseMessage response = null;
            CancellationTokenSource cancellationTokenSource =
                new CancellationTokenSource(TimeSpan.FromSeconds(5));

            try
            {
                response = await ApiClient.GetAsync(
                    apiUrl + "/health", cancellationTokenSource.Token);
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException)
            {
                return false;
            }
            catch (TaskCanceledException)
            {
                return false;
            }
            finally
            {
                if (response != null)
                {
                    response.Dispose();
                }

                cancellationTokenSource.Dispose();
            }
        }

        private async Task<JObject> PostAsync(string requestPath, JObject requestBody)
        {
            string apiUrl = GetApiUrl();
            StringContent requestContent = null;
            HttpResponseMessage response = null;

            try
            {
                requestContent = new StringContent(
                    requestBody.ToString(), Encoding.UTF8, "application/json");
                response = await ApiClient.PostAsync(apiUrl + requestPath, requestContent);

                string responseText = await response.Content.ReadAsStringAsync();
                JObject responseBody = ParseResponse(responseText);
                if (!response.IsSuccessStatusCode)
                {
                    string detail = responseBody["detail"]?.ToString();
                    if (string.IsNullOrWhiteSpace(detail))
                    {
                        detail = "응답 내용을 확인할 수 없습니다.";
                    }

                    throw new InvalidOperationException(
                        $"챗봇 API 오류({(int)response.StatusCode}): {detail}");
                }

                return responseBody;
            }
            finally
            {
                if (response != null)
                {
                    response.Dispose();
                }

                if (requestContent != null)
                {
                    requestContent.Dispose();
                }
            }
        }

        private static JObject ParseResponse(string responseText)
        {
            if (string.IsNullOrWhiteSpace(responseText))
            {
                return new JObject();
            }

            try
            {
                return JObject.Parse(responseText);
            }
            catch
            {
                return new JObject { ["detail"] = responseText };
            }
        }

        private static string GetApiUrl()
        {
            string apiUrl = ConfigurationManager.AppSettings["ChatbotApiUrl"];
            if (string.IsNullOrWhiteSpace(apiUrl))
            {
                throw new InvalidOperationException("챗봇 API 주소가 설정되지 않았습니다.");
            }

            if (string.IsNullOrWhiteSpace(WrGlobal.DBName))
            {
                throw new InvalidOperationException("접속 데이터베이스 정보가 없습니다.");
            }

            if (string.IsNullOrWhiteSpace(WrGlobal.CorpID))
            {
                throw new InvalidOperationException("회사 정보가 없습니다.");
            }

            return apiUrl.TrimEnd('/');
        }
    }
}
