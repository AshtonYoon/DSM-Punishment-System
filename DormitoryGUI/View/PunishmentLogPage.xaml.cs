using DormitoryGUI.ViewModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DormitoryGUI.View
{
    /// <summary>
    /// PunishmentLogPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PunishmentLogPage : Page
    {
        private readonly MainWindow mainWindow;

        private StudentList listviewCollection;

        private JArray studentList;

        public PunishmentLogPage(MainWindow mainWindow, string id, string name, string classNumber, int goodPoint, int badPoint, int currentStep)
        {
            InitializeComponent();

            listviewCollection = Resources["StudentListKey"] as ViewModel.StudentList;

            BackButton.Click += (s, e) =>
            {
                NavigationService.GoBack();
            };

            HttpWebResponse webResponse = Info.GenerateRequest("GET", Info.Server.MANAGING_STUDENT, Info.mainPage.AccessToken, "");

            if (webResponse.StatusCode != HttpStatusCode.OK)
            {
                return;
            }

            using (StreamReader streamReader = new StreamReader(webResponse.GetResponseStream()))
            {
                string responseString = streamReader.ReadToEnd();
                JArray responseJSON = JArray.Parse(responseString);

                studentList = responseJSON;
            }

            foreach (JObject student in studentList)
            {
                string status = student["penalty_traning_status"].ToString();

                listviewCollection.Add(new ViewModel.StudentListViewModel(    
                    id: student["id"].ToString(),
                    classNumber: student["number"].ToString(),
                    name: student["name"].ToString(),
                    goodPoint: int.Parse(student["good_point"].ToString()),
                    badPoint: int.Parse(student["bad_point"].ToString()),
                    isChecked: false,
                    currentStep: status == "NULL" ? 0 : int.Parse(status)));
            }

            SetLogData(id);

            StudentName.Content = name;
            ClassNumber.Content = classNumber;
            TotalGoodPoint.Content = goodPoint.ToString();
            TotalBadPoint.Content = badPoint.ToString();
            TotalPunishStep.Content = currentStep.ToString();

            this.mainWindow = mainWindow;
        }

        private T GetAncestorOfType<T>(FrameworkElement child) where T : FrameworkElement
        {
            var parent = VisualTreeHelper.GetParent(child);
            if (parent != null && !(parent is T))
                return GetAncestorOfType<T>((FrameworkElement) parent);

            return (T) parent;
        }

        private void StudentList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                var target = (StudentListViewModel)e.AddedItems[0];
                SetLogData(target.ID);

                StudentName.Content = target.Name;
                ClassNumber.Content = target.ClassNumber;
                TotalGoodPoint.Content = target.GoodPoint.ToString();
                TotalBadPoint.Content = target.BadPoint.ToString();
                TotalPunishStep.Content = target.CurrentStep.ToString();
            }
        }
        
        private void StudentList_SizeChanged(object sender, SizeChangedEventArgs e)

        {
            var listView = sender as ListView;
            var gridView = listView.View as GridView;

            var workingWidth = listView.ActualWidth - 18;

            double[] columnRatio =
            {
                0.4,
                0.6
            };

            foreach (var element in gridView.Columns)
                element.Width = workingWidth * columnRatio[gridView.Columns.IndexOf(element)];
        }

        private void SetLogData(string id)
        {
            HttpWebResponse webResponse = Info.GenerateRequest("GET", $"{Info.Server.MANAGING_POINT}/{id}", Info.mainPage.AccessToken, "");

            if (webResponse.StatusCode != HttpStatusCode.OK)
            {
                return;
            }

            JArray logs;

            using (StreamReader streamReader = new StreamReader(webResponse.GetResponseStream()))
            {
                string responseString = streamReader.ReadToEnd();
                JArray responseJSON = JArray.Parse(responseString);

                logs = responseJSON;
            }

            Timeline.Children.Clear();

            for (int i = logs.Count - 1; i >= 0; i--)
            {
                JObject log = (JObject) logs[i];

                bool isGood = int.Parse(log["point"].ToString()) > 0;

                Timeline.Children.Add(new TimelineBlock(
                    isGood: isGood,
                    createTime: DateTime.Parse(log["time"].ToString()).ToLongDateString()
                                + " " + DateTime.Parse(log["time"].ToString()).ToLongTimeString(),
                    pointValue: log["point"].ToString(),
                    pointCause: log["reason"].ToString()));
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string command = SearchCommand.Text;
            listviewCollection.Clear();

            foreach (JObject student in studentList)
            {
                if (student["number"].ToString().Contains(command) ||
                    student["name"].ToString().Contains(command) ||
                    student["good_point"].ToString().Contains(command) ||
                    student["bad_point"].ToString().Contains(command))
                {
                    string status = student["penalty_training_status"].ToString();

                    listviewCollection.Add(new ViewModel.StudentListViewModel(
                        id: student["id"].ToString(),
                        classNumber: student["number"].ToString(),
                        name: student["name"].ToString(),
                        isChecked: false,
                        goodPoint: int.Parse(student["good_point"].ToString()),
                        badPoint: int.Parse(student["bad_point"].ToString()),
                        currentStep: status == "null" ? 0 : int.Parse(status)));
                }
            }
        }

        private void SearchCommand_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                SearchButton_Click(sender, null);
        }
    }
}