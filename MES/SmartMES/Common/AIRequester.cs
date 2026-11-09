using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Net.Http;

namespace SmartMES.Common.AIBase
{
    public class AIRequester
    {
        /// <summary>
        /// AI 미들웨어에 리퀘스트 요청
        /// </summary>
        /// <param name="req"></param>
        /// <returns>최종 AI의 응답</returns>
        public static async Task<AIResponse> SendRequest(AIRequest req)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = "http://192.168.0.90:5000/api/TEST";
                string json = JsonConvert.SerializeObject(req);

                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                try
                {
                    HttpResponseMessage response = await client.PostAsync(url, content);

                    string result = await response.Content.ReadAsStringAsync();
                    // 성공
                    if ((int)response.StatusCode == 200)
                    {
                        AIResponse res = JsonConvert.DeserializeObject<AIResponse>(result);
                        return res;
                    }

                    // Request오류
                    if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        return null;
                    }

                    // 서버오류
                    if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                    {
                        return null;
                    }

                    return null;
                }

                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }

    #region Json Class
    /// <summary>
    /// AI 미들웨어 Request 양식
    /// </summary>
    [Serializable]
    public class AIRequest
    {
        /// <summary>
        /// 챗봇, 자동화문서 등
        /// </summary>
        public RequestType type;
        /// <summary>
        /// 선형구조, 분기구조 선택(기본값 : 선형)
        /// </summary>
        public StructureType structure = StructureType.Sequential;
        /// <summary>
        /// DB명
        /// </summary>
        public string dbName;
        /// <summary>
        /// 사용자명
        /// </summary>
        public string userId;
        /// <summary>
        /// AI옵션 리스트
        /// </summary>
        public List<AIOptions> ai_list = new List<AIOptions>();
    }

    /// <summary>
    /// AI 구조 명세
    /// </summary>
    [Serializable]
    public class AIOptions
    {
        /// <summary>
        /// AI 식별ID(분기구조시 필수)
        /// </summary>
        public string id;
        /// <summary>
        /// 사용 API
        /// </summary>
        public API api;
        /// <summary>
        /// 사용 모델
        /// </summary>
        public string model;
        /// <summary>
        /// 메시지 내역
        /// </summary>
        public List<AIMessage> messages = new List<AIMessage>();
        /// <summary>
        /// AI 응답 결과(예: 쿼리 생성)를 DB 처리 여부 <br/>
        /// 기본값 false
        /// </summary>
        public bool useDatabaseWithResponse = false;
        /// <summary>
        /// 이전 AI의 응답 결과 시스템 메시지에 반영 여부 <br/>
        /// true시 System 메시지에 {0} 필요 <br/>
        /// 기본값 false
        /// </summary>
        public bool useBeforeAnswer = false;
        /// <summary>
        /// Fail 처리할때의 errorMessage 내용 <br/>
        /// 설정시 해당 AI에 도달하면 Success = Fail처리
        /// </summary>
        public string reasonForFailConditon;
        /// <summary>
        /// 대답을 AI를 거치지 않고 고정된 메시지를 처리
        /// </summary>
        public string fixedMessage;
        /// <summary>
        /// 응답 포맷을 설정(예: {query : "SELECT 'HELLO, WORLD!'"}) <br/>
        /// 1개만 설정 시, Response에 자동으로 반영되고, 그 이상일 시 isResponse = true인 Format이 Response로 결정
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public AICustomFormat[] customFormat;
        /// <summary>
        /// 분기 설정. 분기구조에서 미설정 시 마지막 작업으로 인지.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public AIConditional conditional;

        private object _think;
        /// <summary>
        /// qwen3 등에서의 think 설정. bool, string, null 허용
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object think
        {
            get => _think;
            set
            {
                if (!(value is bool _)
                    && !(value is string _)
                    && !(value is null))
                {
                    throw new ArgumentException("think는 bool 혹은 string이어야합니다.");
                }

                _think = value;
            }
        }
    }

    /// <summary>
    /// 커스텀
    /// </summary>
    [Serializable]
    public class AICustomFormat
    {
        /// <summary>
        /// Format 명 (예: query)
        /// </summary>
        public string name;
        /// <summary>
        /// Format 타입 (예: string)
        /// </summary>
        public string type;
        /// <summary>
        /// Format 설명
        /// </summary>
        public string description;
        /// <summary>
        /// Response로 반환여부.
        /// </summary>
        public bool isResponse = false;
    }

    [Serializable]
    public class AIMessage
    {
        /// <summary>
        /// 작성자
        /// </summary>
        public Role role;
        /// <summary>
        /// 내용
        /// </summary>
        public string content;
    }

    [Serializable]
    public class AIResponse
    {
        /// <summary>
        /// 성공여부
        /// </summary>
        public bool success; //임의로 해당 AI에 접근이 되면 success = false이도록 처리가 가능한 로직도
        /// <summary>
        /// AI 최종 응답
        /// </summary>
        public string result;
        /// <summary>
        /// Request 받은 시간
        /// </summary>
        public string requestTime;
        /// <summary>
        /// Response 생성 시간
        /// </summary>
        public string responseTime;
        /// <summary>
        /// 에러 메시지
        /// </summary>
        public string errorMessage;
    }

    [Serializable]
    public class AIConditional
    {
        /// <summary>
        /// 분기에 사용할 Format. 미설정 시, AI의 응답 전문과 대조.
        /// </summary>
        public string conditionFormat;
        /// <summary>
        /// 분기가 되는 다음 AI 리스트
        /// </summary>
        public List<AINext> nextSequence = new List<AINext>();
    }

    [Serializable]
    public class AINext
    {
        /// <summary>
        /// 분기값. 미설정 시 검증 후 이쪽 분기로 빠짐.
        /// switch - case 구조와 유사.
        /// </summary>
        public string condition;
        /// <summary>
        /// 분기로 빠지게되는 AI의 식별ID
        /// </summary>
        public string id;
    }
    #endregion

    #region Enum
    [JsonConverter(typeof(StringEnumConverter))]
    public enum RequestType
    {
        /// <summary>
        /// 자동화문서
        /// </summary>
        [EnumMember(Value = "aidocs")]
        AIDocs,
        /// <summary>
        /// 챗봇
        /// </summary>
        [EnumMember(Value = "chatbot")]
        Chatbot
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum API
    {
        [EnumMember(Value = "ollama")]
        Ollama
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum Role
    {
        /// <summary>
        /// 시스템 메시지(Prompt 등)
        /// </summary>
        [EnumMember(Value = "system")]
        System,
        /// <summary>
        /// 사용자 메시지
        /// </summary>
        [EnumMember(Value = "user")]
        User,
        /// <summary>
        /// 
        /// </summary>
        [EnumMember(Value = "assistant")]
        Assistant
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum StructureType
    {
        /// <summary>
        /// 선형구조
        /// </summary>
        [EnumMember(Value = "sequential")]
        Sequential,
        /// <summary>
        /// 분기구조
        /// </summary>
        [EnumMember(Value = "conditional")]
        Conditional
    }
    #endregion
}
