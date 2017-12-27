using DormitoryGUI.ViewModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        private StudentList listviewCollection;

        // private RoutedEventHandler UnCheckedEventHandler;
        // private RoutedEventHandler CheckedEventHandler;

        private JArray studentList;

        private int UUID;

        public PunishmentLogPage(string name, string schoolNumber, int totalGoodPoint, int totalBadPoint,
            string currentStep, int uuid)
        {
            InitializeComponent();

            listviewCollection = Resources["StudentListKey"] as ViewModel.StudentList;

            BackButton.Click += (s, e) => { this.NavigationService.GoBack(); };

            object masterData = Info.MultiJson(Info.Server.GET_STUDENT_DATA, "");
            studentList = (JArray) masterData;

            foreach (JObject json in studentList)
            {
                listviewCollection.Add(new ViewModel.StudentListViewModel(
                    roomNumber: 0.ToString(),
                    classNumber: json["USER_SCHOOL_NUMBER"].ToString(),
                    name: json["USER_NAME"].ToString(),
                    isChecked: false,
                    goodPoint: int.Parse(json["TOTAL_GOOD_SCORE"].ToString()),
                    badPoint: int.Parse(json["TOTAL_BAD_SCORE"].ToString()),
                    currentStep: Info.ParseStatus(json["PUNISH_STATUS"].ToString()),
                    userUUID: int.Parse(json["USER_UUID"].ToString())));
            }

            UUID = uuid;

            SetLogData();

            StudentName.Content = name;
            ClassNumber.Content = schoolNumber;
            TotalGoodPoint.Content = totalGoodPoint.ToString();
            TotalBadPoint.Content = totalBadPoint.ToString();
            TotalPunishStep.Content = currentStep.ToString();

            foreach (StudentListViewModel item in StudentList.Items)
            {
                if (item.UserUUID == uuid)
                    StudentList.SelectedItems.Add(item);
            }
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
            AddSelectionByClick(StudentList, e);
        }

        private void AddSelectionByClick(ListView listview, SelectionChangedEventArgs e)
        {
            listview.Items.Refresh();           
            var targets = e.AddedItems;
            if (e.AddedItems.Count != 0)
            {
                var target = (StudentListViewModel) e.AddedItems[e.AddedItems.Count - 1];
                UUID = target.UserUUID;
                SetLogData();
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

        private void SetLogData()

        {
            JObject jobj = new JObject
            {
                {"USER_UUID", UUID}
            };
            object temp = Info.MultiJson(Info.Server.STUDENT_LOG, jobj);
            if (temp == null)
                return;

            JArray result = (JArray) temp;
            Timeline.Children.Clear();

            for (int i = result.Count - 1; i >= 0; i--)
            {
                JObject obj = (JObject) result[i];

                bool isGood = obj["POINT_TYPE"].ToString().Equals("0");

                Timeline.Children.Add(new TimelineBlock(
                    isGood: isGood,
                    createTime: DateTime.Parse(obj["CREATE_TIME"].ToString()).ToLongDateString()
                                + " " + DateTime.Parse(obj["CREATE_TIME"].ToString()).ToLongTimeString(),
                    pointValue: obj["POINT_VALUE"].ToString(),
                    pointCause: obj["POINT_MEMO"].ToString()));
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string command = SearchCommand.Text;
            listviewCollection.Clear();

            foreach (JObject student in studentList)
            {
                if (student["USER_SCHOOL_NUMBER"].ToString().Contains(command) ||
                    student["USER_NAME"].ToString().Contains(command) ||
                    student["TOTAL_GOOD_SCORE"].ToString().Contains(command) ||
                    student["TOTAL_BAD_SCORE"].ToString().Contains(command))
                {
                    listviewCollection.Add(new ViewModel.StudentListViewModel(
                        roomNumber: student["user_school_room_number"] != null
                            ? student["user_school_room_number"].ToString()
                            : "NULL",
                        classNumber: student["USER_SCHOOL_NUMBER"].ToString(),
                        name: student["USER_NAME"].ToString(),
                        isChecked: false,
                        goodPoint: int.Parse(student["TOTAL_GOOD_SCORE"].ToString()),
                        badPoint: int.Parse(student["TOTAL_BAD_SCORE"].ToString()),
                        currentStep: Info.ParseStatus(student["PUNISH_STATUS"].ToString()),
                        userUUID: int.Parse(student["USER_UUID"].ToString())));
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