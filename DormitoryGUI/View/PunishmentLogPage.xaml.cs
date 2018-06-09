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

        private JArray students;

        public PunishmentLogPage(MainWindow mainWindow, string id, string name, string classNumber, int goodPoint, int badPoint, string currentStep)
        {
            InitializeComponent();

            listviewCollection = Resources["StudentListKey"] as ViewModel.StudentList;

            BackButton.Click += (s, e) =>
            {
                NavigationService.GoBack();
            };

            var responseDict = Info.GenerateRequest("GET", Info.Server.MANAGING_STUDENT, Info.mainPage.AccessToken, "");

            if ((HttpStatusCode)responseDict["status"] != HttpStatusCode.OK)
            {
                MessageBox.Show("학생 목록 조회 실패");
                return;
            }

            students = JArray.Parse(responseDict["body"].ToString());

            foreach (JObject student in students)
            {

                listviewCollection.Add(new ViewModel.StudentListViewModel(    
                    id: student["id"].ToString(),
                    classNumber: student["number"].ToString(),
                    name: student["name"].ToString(),
                    goodPoint: student["good_point"].Type == JTokenType.Null ? 0 : (int) student["good_point"],
                    badPoint: student["bad_point"].Type == JTokenType.Null ? 0 : (int) student["bad_point"],
                    penaltyLevel: Info.ParseStatus(student["bad_point_status"].Type == JTokenType.Null ? 0 : (int) student["bad_point_status"]),
                    penaltyTrainingStaus: bool.Parse(student["penalty_training_status"].ToString()),
                    isSelected: false));
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
                TotalPunishStep.Content = target.PenaltyTrainingStatus.ToString();
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
            var responseDict = Info.GenerateRequest("GET", $"{Info.Server.MANAGING_POINT}/{id}", Info.mainPage.AccessToken, "");

            if ((HttpStatusCode)responseDict["status"] != HttpStatusCode.OK)
            {
                MessageBox.Show("상벌점 내역 조회 실패");
                return;
            }

            JArray logs = JArray.Parse(responseDict["body"].ToString());

            Timeline.Children.Clear();

            for (int i = logs.Count - 1; i >= 0; i--)
            {
                JObject log = (JObject) logs[i];

                bool isGood = bool.Parse(log["point_type"].ToString());
                int point = (int) log["point"];
                Timeline.Children.Add(new TimelineBlock(
                    isGood: isGood,
                    createTime: DateTime.Parse(log["time"].ToString()).ToLongDateString()
                                + " " + DateTime.Parse(log["time"].ToString()).ToLongTimeString(),
                    pointValue: point > 0 ? point.ToString() : (-1 * point).ToString(),
                    pointCause: log["reason"].ToString()));
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string command = SearchCommand.Text;
            listviewCollection.Clear();

            foreach (JObject student in students)
            {
                if (student["number"].ToString().Contains(command) ||
                    student["name"].ToString().Contains(command) ||
                    student["good_point"].ToString().Contains(command) ||
                    student["bad_point"].ToString().Contains(command))
                {
                    listviewCollection.Add(new ViewModel.StudentListViewModel(
                        id: student["id"].ToString(),
                        classNumber: student["number"].ToString(),
                        name: student["name"].ToString(),
                        goodPoint: int.Parse(student["good_point"].ToString()),
                        badPoint: int.Parse(student["bad_point"].ToString()),
                        penaltyLevel: Info.ParseStatus(student["bad_point_status"].Type == JTokenType.Null ? 0 : int.Parse(student["bad_point_status"].ToString())),
                        penaltyTrainingStaus: bool.Parse(student["penalty_training_status"].ToString()),
                        isSelected: false));
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
