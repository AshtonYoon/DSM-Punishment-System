﻿using DormitoryGUI.ViewModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DormitoryGUI.View;

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

            Password.Focus();

            this.mainWindow = mainWindow;            
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var requestDict = new Dictionary<string, object>
            {
                 { "id", ID.Text },
                 { "password", Password.Password }
            };

            var responseDict = Info.GenerateRequest("POST", Info.Server.AUTH, "", requestDict);

            if ((HttpStatusCode)responseDict["status"] != HttpStatusCode.Created)
            {
                MessageBox.Show("로그인 실패");
                return;
            }

            JObject responseJSON = JObject.Parse(responseDict["body"].ToString());

            Info.mainPage.AccessToken = responseJSON["accessToken"].ToString();
            Info.mainPage.RefreshToken = responseJSON["refreshToken"].ToString();

            mainWindow.NavigatePage(new MainPage(mainWindow));
        }

        private void Password_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                LoginButton_Click(sender, null);
            }
        }
        
        private bool ValidateInput()
        {
            return string.IsNullOrWhiteSpace(ID.Text) && string.IsNullOrWhiteSpace(Password.Password);
        }
    }
}
