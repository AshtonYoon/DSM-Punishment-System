using DormitoryGUI.ViewModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace DormitoryGUI
{
    /// <summary>
    /// LoginPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LoginPage : Page
    {
        private MainPageViewModel mainPage;
        private readonly MainWindow mainWindow;

        public LoginPage(MainWindow mainWindow)
        {
            InitializeComponent();

            mainPage = new MainPageViewModel();
            this.mainWindow = mainWindow;

            Password.Focus();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            JObject data = new JObject();
            data.Add("ID", ID.Text);
            data.Add("PW", sha1Encrypt(Password.Password));

            JObject response = getStringFromJson(Info.Server.LOGIN_URL, data);

            if (response["TEACHER_UUID"] != null)
            {
                mainPage.Name = response["TEACHER_NAME"].ToString();

                mainPage.PermissionData = new KeyValuePair<bool, bool>
                    (Int32.Parse(response["STUDENT_MANAGE"].ToString()) == 1
                    , Int32.Parse(response["SCORE_MANAGE"].ToString()) == 1);

                mainPage.TeacherUUID = Int32.Parse(response["TEACHER_UUID"].ToString());
                
                mainWindow.NavigatePage(new MainPage());
            }
            else
            {
                MessageBox.Show("로그인에 실패하였습니다.");
            }
        }

        private JObject getStringFromJson(string url, JObject json)
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
                        if (httpResponse.StatusCode != HttpStatusCode.OK)
                            return JObject.Parse("{}");

                        using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            string result = streamReader.ReadToEnd();
                            return JObject.Parse(result);
                        }
                    }
                }
            }
            catch (Exception)
            {
                return JObject.Parse("{}");
            }
        }

        private static string sha1Encrypt(string input)
        {
            var hash = (new SHA1Managed()).ComputeHash(Encoding.UTF8.GetBytes(input));
            return string.Join("", hash.Select(b => b.ToString("x2")).ToArray());
        }

        private void Password_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                LoginButton_Click(sender, null);
            }
        }
    }
}
