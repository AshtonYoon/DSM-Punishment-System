using DormitoryGUI.ViewModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
            private const string SERVER_URL = "http://dsm2015.cafe24.com:3001/";
            public const string AUTH = SERVER_URL + "admin/auth";
            public const string REFRESH = SERVER_URL + "admin/refresh";
            public const string MANAGING_POINT = SERVER_URL + "admin/managing/point";
            public const string MANAGING_STUDENT = SERVER_URL + "admin/managing/student";
            public const string MANAGING_RULE = SERVER_URL + "admin/managing/rule";
        }

        
        // 상벌점 유형 { 0: 상점, 1: 벌점 }
        public enum POINT_TYPE { GOOD = 0, BAD };

        // 이전 상벌점 서버 전용 request
        public static object MultiJson(string url, object json)
        { 
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                byte[] postBody = Encoding.UTF8.GetBytes(json.ToString());

                using (Stream stream = httpWebRequest.GetRequestStream())
                {
                    stream.Write(postBody, 0, postBody.Length);

                    using (HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse())
                    {
                        using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            string result = streamReader.ReadToEnd();
                            if (result.StartsWith("["))
                                return JArray.Parse(result);
                            if (result.StartsWith("{"))
                                return JObject.Parse(result);
                            return null;
                        }
                    }
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
            return null;
        }

        // 새로운 상벌점 서버 전용 request
        public static HttpWebResponse JSONRequest(string method, string url, string token, object json)
        {
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

                httpWebRequest.ContentType = "application/json";

                httpWebRequest.Method = method;
                httpWebRequest.Headers["Autorization"] = token;

                byte[] jsonBody = Encoding.UTF8.GetBytes(json.ToString());

                if (method != "GET")
                {
                    using (Stream stream = httpWebRequest.GetRequestStream())
                    {
                        stream.Write(jsonBody, 0, jsonBody.Length);
                    }
                }
                
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                return httpWebResponse;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return null;
        }
    }
}
