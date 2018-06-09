using DormitoryGUI.ViewModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DormitoryGUI
{
    class Info
    {
        public static MainPageViewModel mainPage = new MainPageViewModel();

        // 새로운 상벌점 서버를 위한 PATH 수정 
        public class Server
        {
            private const string SERVER_URL = "http://dsm2015.cafe24.com/v2/";
            public const string AUTH = SERVER_URL + "admin/auth";
            public const string REFRESH = SERVER_URL + "admin/refresh";
            public const string MANAGING_POINT = SERVER_URL + "admin/point/point";
            public const string MANAGING_STUDENT = SERVER_URL + "admin/point/student";
            public const string MANAGING_RULE = SERVER_URL + "admin/point/rule";
            public const string MANAGING_PENALTY = SERVER_URL + "admin/point/student/penalty";
        }

        // 상벌점 유형 { 0: 상점, 1: 벌점 }
        public enum POINT_TYPE
        {
            GOOD = 0,
            BAD
        };

        // 새로운 상벌점 서버 전용 request
        public static Dictionary<string, object> GenerateRequest(string method, string url, string token, object body)
        {
            var responseDict = new Dictionary<string, object>();

            try
            {
                HttpWebRequest webRequest = (HttpWebRequest) WebRequest.Create(url);
                webRequest.Method = method;

                if (!string.IsNullOrWhiteSpace(token))
                {
                    webRequest.Headers.Add("Authorization", $"JWT {token}");
                }

                string bodyString;

                if (body is JObject)
                {
                    webRequest.ContentType = "application/json";
                    bodyString = body.ToString();
                }
                else if (body is Dictionary<string, object>)
                {
                    webRequest.ContentType = "application/json";
                    bodyString = GenerateUrlEncodedBody(body as Dictionary<string, object>);
                }
                else
                {
                    bodyString = body.ToString();
                }

                byte[] bytesBody = Encoding.UTF8.GetBytes(bodyString);

                if (!method.Equals("GET"))
                {
                    using (Stream stream = webRequest.GetRequestStream())
                    {
                        stream.Write(bytesBody, 0, bytesBody.Length);
                    }
                }

                HttpWebResponse webResponse = (HttpWebResponse) webRequest.GetResponse();

                responseDict["status"] = webResponse.StatusCode;

                using (StreamReader streamReader = new StreamReader(webResponse.GetResponseStream()))
                {
                    responseDict["body"] = streamReader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

                if (e is WebException)
                {
                    responseDict["status"] = ((e as WebException).Response as HttpWebResponse)?.StatusCode;
                    return responseDict;
                }
            }

            return responseDict;
        }

        // urlencoded body 문자열 생성
        public static string GenerateUrlEncodedBody(Dictionary<string, object> data)
        {
            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append("{");

            foreach (var entry in data)
            {
                if (entry.Value is int || entry.Value is bool || entry.Value is Int16)
                {
                    Console.WriteLine(entry.Value);
                    queryBuilder.AppendFormat("\"{0}\":{1},", entry.Key, entry.Value.ToString().ToLower());
                }
                else
                {
                    queryBuilder.AppendFormat("\"{0}\": \"{1}\",", entry.Key, entry.Value);
                    Console.WriteLine(entry.Value);
                }
            }

            queryBuilder = queryBuilder.Remove(queryBuilder.Length - 1, 1);
            queryBuilder.Append("}");
            Console.WriteLine(queryBuilder.ToString());
            return queryBuilder.ToString();
        }

        public static string ParseStatus(int status)
        {
            switch (status)
            {
                case 0:
                    return "";
                case 1:
                    return "1단계";
                case 2:
                    return "2단계";
                case 3:
                    return "3단계";
                default:
                    return " ";
            }
        }
    }
}