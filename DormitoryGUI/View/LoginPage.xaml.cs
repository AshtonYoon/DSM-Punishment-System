using DormitoryGUI.ViewModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DormitoryGUI
{
    /// <summary>
    /// LoginPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LoginPage : Page
    {
        private readonly MainWindow mainWindow;

        public LoginPage(MainWindow mainWindow)
        {
            InitializeComponent();
            
            this.mainWindow = mainWindow;

            Password.Focus();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            JObject jobj = new JObject
            {
                { "id", ID.Text },
                { "pw", Password.Password }
            };

            HttpWebResponse webResponse = Info.JSONRequest("POST", Info.Server.AUTH, "", jobj);

            if (webResponse.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show("로그인 실패");
                return;
            }

            using (StreamReader streamReader = new StreamReader(webResponse.GetResponseStream()))
            {
                string responseString = streamReader.ReadToEnd();
                JObject responseJSON = JObject.Parse(responseString);

                Info.mainPage.AccessToken = responseJSON["access_token"].ToString();
                Info.mainPage.RefreshToken = responseJSON["refresh_token"].ToString();
            }
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
