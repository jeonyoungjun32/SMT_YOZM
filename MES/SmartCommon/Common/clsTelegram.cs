using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SmartCommon.Common
{
    public class clsTelegram
    {
		//private static readonly string _baseUrl = "https://api.telegram.org/bot";
        //public static string ChatID = "-900607360";
        //public static string Token = "6200553972:AAFYbVpXyYWXWIFwi1YBtEyBHYL7uthHIHA";
		private static readonly string _baseUrl = "";
		public static string ChatID = "";
        public static string Token = "";
        public static bool SendMessage(string sendMessage, out string errMessage)
        {
            return SendMessage(ChatID, Token, sendMessage, out errMessage);
        }

        public static bool SendMessage(string chatId, string token, string sendMessage, out string errMessage)
        {
            var sURL = "https://api.telegram.org/bot" + token +
                           "/sendMessage" +
                           "?chat_id=" + chatId +
                           "&text=" + sendMessage;

            HttpWebRequest webRequest = WebRequest.Create(sURL) as HttpWebRequest;
            webRequest.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
            webRequest.Timeout = 30 * 1000;
            webRequest.Method = "POST";
            webRequest.ContentType = "application/json";

            string json = string.Format("{{\"chat_id\":\"{0}\", \"text\":\"{1}\"}}", chatId, EncodeJsonChars(sendMessage));
            byte[] data = UTF8Encoding.UTF8.GetBytes(json);
            webRequest.ContentLength = data.Length;

            using (Stream stream = webRequest.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
                stream.Flush();
            }

            HttpWebResponse webResponse = null;

            try
            {
                webResponse = webRequest.GetResponse() as HttpWebResponse;

                if (webResponse.StatusCode == HttpStatusCode.OK)
                {
                    string responseData = null;
                    using (Stream responseStream = webResponse.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(responseStream, UTF8Encoding.UTF8))
                        {
                            responseData = reader.ReadToEnd();
                        }
                    }

                    if (responseData.IndexOf("\"ok\":true") > 0)
                    {
                        errMessage = "";
                        return true;
                    }
                    else
                    {
                        errMessage = string.Format("결과 파싱 오류 ({0})", responseData);
                        return false;
                    }
                }
                else
                {
                    errMessage = string.Format("Http status: {0}", webResponse.StatusCode);
                    return false;
                }
            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
                return false;
            }
            finally
            {
                if (webResponse != null) webResponse.Close();
            }
        }

        private static string EncodeJsonChars(string text)
        {
            return text.Replace("\b", "\\\b")
                    .Replace("\f", "\\\f")
                    .Replace("\n", "\\\n")
                    .Replace("\r", "\\\r")
                    .Replace("\t", "\\\t")
                    .Replace("\"", "\\\"")
                    .Replace("\\", "\\\\");
        }
    }
}
