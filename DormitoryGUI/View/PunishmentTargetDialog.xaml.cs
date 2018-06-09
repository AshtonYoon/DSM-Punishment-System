using System;
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
        private StudentList filteredCollection;
        private JArray studentList;

        private string filter = "전체";

        public PunishmentTargetDialog()
        {
            InitializeComponent();
            listviewCollection = Resources["StudentListKey"] as ViewModel.StudentList;
            filteredCollection = Resources["StudentListKey"] as ViewModel.StudentList;

            Update();
        }

        public void Update()
        {
            listviewCollection.Clear();
            var responseDict = Info.GenerateRequest("GET", Info.Server.MANAGING_STUDENT, Info.mainPage.AccessToken, "");

            if ((HttpStatusCode) responseDict["status"] != HttpStatusCode.OK)
            {
                return;
            }

            studentList = JArray.Parse(responseDict["body"].ToString());
            
            
            foreach (JObject student in studentList)
            {
                int currentStep = student["penaltyLevel"].Type == JTokenType.Null
                    ? 0
                    : (int) student["penaltyLevel"];

                if (filter != "전체" && Info.ParseStatus(currentStep).Equals(filter))
                {
                    continue;
                }

                if (currentStep >= 1 && (bool)student["penaltyTrainingStatus"] == true)
                {
                    listviewCollection.Add(new ViewModel.StudentListViewModel(
                        id: student["id"].ToString(),
                        classNumber: student["number"].ToString(),
                        name: student["name"].ToString(),
                        goodPoint: student["goodPoint"].Type == JTokenType.Null ? 0 : (int) student["goodPoint"],
                        badPoint: student["badPoint"].Type == JTokenType.Null ? 0 : (int) student["badPoint"],
                        penaltyTrainingStaus: bool.Parse(student["penaltyTrainingStatus"].ToString()),
                        penaltyLevel: bool.Parse(student["penaltyTrainingStatus"].ToString()) == true ? Info.ParseStatus((int)student["penaltyLevel"]) : " ",
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
            if (All.IsChecked == true)
            {
                Update();
            }
            else if (Grade1.IsChecked == true)
            {
                FilteringStudent(1);
            }
            else if (Grade2.IsChecked == true)
            {
                FilteringStudent(2);
            }
            else if (Grade3.IsChecked == true)
            {
                FilteringStudent(3);
            }
        }

        private void FilteringStudent(int step)
        {
            listviewCollection.Clear();
            foreach (JObject student in studentList)
            {
                int currentStep = student["penaltyLevel"].Type == JTokenType.Null
                    ? 0
                    : (int)student["penaltyLevel"];

                if (currentStep == step && (bool)student["penaltyTrainingStatus"] == true)
                {
                    listviewCollection.Add(new ViewModel.StudentListViewModel(
                        id: student["id"].ToString(),
                        classNumber: student["number"].ToString(),
                        name: student["name"].ToString(),
                        goodPoint: student["goodPoint"].Type == JTokenType.Null ? 0 : (int)student["goodPoint"],
                        badPoint: student["badPoint"].Type == JTokenType.Null ? 0 : (int)student["badPoint"],
                        penaltyTrainingStaus: bool.Parse(student["penaltySrainingStatus"].ToString()),
                        penaltyLevel: Info.ParseStatus(currentStep),
                        isSelected: false
                    ));
                }
            }
        }   
        private void Offset_Click(object sender, RoutedEventArgs e)
        {
            var target = (StudentListViewModel) GetAncestorOfType<ListViewItem>(sender as Button).DataContext;
            var requestDict = new Dictionary<string, object>
            {                
                {"status", false}
            };
            var responseDict = Info.GenerateRequest("PATCH", $"{Info.Server.MANAGING_PENALTY}/{target.ID}", Info.mainPage.AccessToken,
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