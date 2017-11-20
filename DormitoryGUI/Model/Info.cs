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

        public class Server
        {
            private const string SERVER_URL = "http://dsm2015.cafe24.com:3141/";
            public const string LOGIN_URL = SERVER_URL + "login";
            public const string GET_PERMISSION_URL = SERVER_URL + "permission/get";
            public const string SET_PERMSSION_URL = SERVER_URL + "permission/set";
            public const string GET_STUDENT_DATA = SERVER_URL + "student/get";
            public const string STUDENT_LOG = SERVER_URL + "student/log";
            public const string GET_RULE_DATA = SERVER_URL + "rule/get";
            public const string ADD_RULE_DATA = SERVER_URL + "rule/add";
            public const string GIVE_SCORE = SERVER_URL + "score/give";
            public const string GET_EXCEL_DATA = SERVER_URL + "excel/get";
            public const string SET_STUDENT_DATA = SERVER_URL + "student/add";
        }

        public enum POINT_TYPE { BAD = 0, GOOD = 1};
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
    }
}
