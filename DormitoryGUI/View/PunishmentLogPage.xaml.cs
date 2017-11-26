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

        private RoutedEventHandler UnCheckedEventHandler;
        private RoutedEventHandler CheckedEventHandler;

        private JArray studentList;

        private int UUID;

        public PunishmentLogPage(string name, string schoolNumber, int totalGoodPoint, int totalBadPoint, int uuid)
        {
            InitializeComponent();

            listviewCollection = Resources["StudentListKey"] as ViewModel.StudentList;

            BackButton.Click += (s, e) =>
            {
                this.NavigationService.GoBack();
            };

            UnCheckedEventHandler += new RoutedEventHandler((s, e) => {
                var target = GetAncestorOfType<ListView>(s as CheckBox);
                foreach (var element in target.Items)
                {
                    ((StudentListViewModel)element).IsChecked = false;
                }
                target.SelectedItems.Clear();

                target.Items.Refresh();
            });

            CheckedEventHandler += new RoutedEventHandler((s, e) =>
            {
                var target = GetAncestorOfType<ListView>(s as CheckBox);
                foreach (var element in target.Items)
                {
                    ((StudentListViewModel)element).IsChecked = true;
                    target.SelectedItems.Add(element);
                }

                target.Items.Refresh();
            });

            object masterData = Info.MultiJson(Info.Server.GET_STUDENT_DATA, "");
            studentList = (JArray)masterData;

            foreach (JObject json in studentList)
            {
                listviewCollection.Add(new ViewModel.StudentListViewModel(
                    roomNumber: 0.ToString(),
                    classNumber: json["USER_SCHOOL_NUMBER"].ToString(),
                    name: json["USER_NAME"].ToString(),
                    isChecked: false,
                    goodPoint: int.Parse(json["TOTAL_GOOD_SCORE"].ToString()),
                    badPoint: int.Parse(json["TOTAL_BAD_SCORE"].ToString()),
                    currentStep: int.Parse(json["PUNISH_STATUS"].ToString()),
                    userUUID: int.Parse(json["USER_UUID"].ToString())));
            }

            UUID = uuid;

            SetLogData();

            StudentName.Content = name;
            ClassNumber.Content = schoolNumber;
            TotalGoodPoint.Content = totalGoodPoint.ToString();
            TotalBadPoint.Content = totalBadPoint.ToString();

            foreach(StudentListViewModel item in StudentList.Items)
            {
                if (item.UserUUID == uuid)
                    StudentList.SelectedItems.Add(item);
            }
        }
        private T GetAncestorOfType<T>(FrameworkElement child) where T : FrameworkElement
        {
            var parent = VisualTreeHelper.GetParent(child);
            if (parent != null && !(parent is T))
                return GetAncestorOfType<T>((FrameworkElement)parent);

            return (T)parent;
        }
        private void StudentList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var target = (StudentListViewModel)e.AddedItems[e.AddedItems.Count - 1];
            UUID = target.UserUUID;

            SetLogData();

            StudentName.Content = target.Name;
            ClassNumber.Content = target.ClassNumber;
            TotalGoodPoint.Content = target.GoodPoint.ToString();
            TotalBadPoint.Content = target.BadPoint.ToString();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckedEventHandler?.Invoke(sender, e);
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            UnCheckedEventHandler?.Invoke(sender, e);
        }

        private void StudentList_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var listView = sender as ListView;
            var gridView = listView.View as GridView;

            var workingWidth = listView.ActualWidth - 18;

            double[] columnRatio =
            {
                0.15,
                0.425,
                0.425
            };

            foreach (var element in gridView.Columns)
                element.Width = workingWidth * columnRatio[gridView.Columns.IndexOf(element)];
        }

        private void ItemCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var target = GetAncestorOfType<ListView>(sender as CheckBox);
            var targetItem = GetAncestorOfType<ListViewItem>(sender as CheckBox);

            target.SelectedItems.Add(targetItem);
            target.Items.Refresh();
        }
            
        private void ItemCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void SetLogData()
        {
            JObject jobj = new JObject
            {
                { "USER_UUID", UUID }
            };
            object temp = Info.MultiJson(Info.Server.STUDENT_LOG, jobj);
            if (temp == null)
                return;

            JArray result = (JArray)temp;
            Timeline.Children.Clear();

            for (int i = result.Count - 1; i >= 0; i--)
            {
                JObject obj = (JObject)result[i];
                bool isGood = obj["POINT_TYPE"].ToString().Equals("1");

                Timeline.Children.Add(new TimelineBlock(
                    isGood: isGood,
                    createTime: DateTime.Parse(obj["CREATE_TIME"].ToString()).ToLongDateString()
                    + " " + DateTime.Parse(obj["CREATE_TIME"].ToString()).ToLongTimeString(),
                    pointValue: obj["POINT_VALUE"].ToString(),
                    pointCause: obj["POINT_MEMO"].ToString()));
            }
        }
    }
}
