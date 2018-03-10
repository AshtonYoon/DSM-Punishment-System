﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DormitoryGUI.ViewModel;
using Newtonsoft.Json.Linq;

namespace DormitoryGUI.View
{
    /// <summary>
    /// PunishmentTargetDialog.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PunishmentTargetDialog : Window
    {
        private StudentList listviewCollection;

        private JArray studentList;

        private string filter = "전체";

        public PunishmentTargetDialog()
        {
            InitializeComponent();
            listviewCollection = Resources["StudentListKey"] as ViewModel.StudentList;

            Update();
        }

        public void Update()
        {
            var responseDict = Info.GenerateRequest("GET", Info.Server.MANAGING_STUDENT, Info.mainPage.AccessToken, "");

            if ((HttpStatusCode) responseDict["status"] != HttpStatusCode.OK)
            {
                return;
            }

            studentList = JArray.Parse(responseDict["body"].ToString());

            foreach (JObject student in studentList)
            {
                int currentStep = student["bad_point_status"].Type == JTokenType.Null
                    ? 0
                    : (int) student["bad_point_status"];

                if (filter != "전체" && Info.ParseStatus(currentStep).Equals(filter))
                {
                    continue;
                }

                if (currentStep >= 1)
                {
                    listviewCollection.Add(new ViewModel.StudentListViewModel(
                        id: student["id"].ToString(),
                        classNumber: student["number"].ToString(),
                        name: student["name"].ToString(),
                        goodPoint: student["good_point"].Type == JTokenType.Null ? 0 : (int) student["good_point"],
                        badPoint: student["bad_point"].Type == JTokenType.Null ? 0 : (int) student["bad_point"],
                        currentStep: Info.ParseStatus(currentStep),
                        isSelected: false
                    ));
                }
            }
        }

        private T GetAncestorOfType<T>(FrameworkElement child) where T : FrameworkElement
        {
            var parent = VisualTreeHelper.GetParent(child);
            if (parent != null && !(parent is T))
                return GetAncestorOfType<T>((FrameworkElement) parent);

            return (T) parent;
        }

        private void StudentList_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            var listView = sender as ListView;
            var gridView = listView.View as GridView;

            var workingWidth = listView.ActualWidth - 18;

            double[] columnRatio =
            {
                0.25,
                0.25,
                0.25,
                0.25
            };

            foreach (var element in gridView.Columns)
                element.Width = workingWidth * columnRatio[gridView.Columns.IndexOf(element)];
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
        }

        private void Offset_Click(object sender, RoutedEventArgs e)
        {
            var target = (StudentListViewModel) GetAncestorOfType<ListViewItem>(sender as Button).DataContext;
            var requestDict = new Dictionary<string, object>
            {
                {"id", target.ID},
                {"status", false}
            };
            var responseDict = Info.GenerateRequest("PATCH", Info.Server.MANAGING_PENALTY, Info.mainPage.AccessToken,
                requestDict);

            MessageBox.Show((HttpStatusCode) responseDict["status"] == HttpStatusCode.OK
                ? "상쇄했습니다."
                : responseDict["status"].ToString());
//            MessageBox.Show(target.ID);
            listviewCollection.Clear();
            Update();
        }
    }
}