using DormitoryGUI.Model;
using DormitoryGUI.View;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DormitoryGUI
{
    /// <summary>
    /// MainPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainPage : Page
    {
        ViewModel.StudentList listviewCollection;
        private JArray studentList;
        private readonly MainWindow mainWindow;

        public MainPage(MainWindow mainWindow)
        {
            InitializeComponent();

            listviewCollection = Resources["StudentListKey"] as ViewModel.StudentList;

            UploadExcel.Click += (s, e) =>
            {
                setStudentData();
                MessageBox.Show("데이터 설정이 완료되었습니다.");
            };

            this.mainWindow = mainWindow;
            PunishmentList.Click += (s, e) =>
            {
                mainWindow.NavigatePage(new PunishmentListPage());
            };

            update();
        }

        public void update()
        {
            object masterData = Info.multiJson(Info.Server.GET_STUDENT_DATA, "");
            studentList = (JArray)masterData;
            
            foreach (JObject json in studentList)
            {
                listviewCollection.Add(new ViewModel.StudentListViewModel(
                    roomNumber: 0.ToString(),
                    classNumber: json["USER_SCHOOL_NUMBER"].ToString(),
                    name: json["USER_NAME"].ToString(),
                    IsChecked: false,
                    goodPoint: int.Parse(json["TOTAL_GOOD_SCORE"].ToString()),
                    badPoint: int.Parse(json["TOTAL_BAD_SCORE"].ToString())));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ApplyPointButton_Click(object sender, RoutedEventArgs e)
        {
            HideAnimation(PointProcedure.Children[PointProcedure.Children.Count - 1] as Panel);
        }

        private void SearchList_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var listView = sender as ListView;
            var gridView = listView.View as GridView;

            var workingWidth = listView.ActualWidth - 18;

            double[] columnRatio =
            {
                0.05,
                0.175,
                0.175,
                0.175,
                0.175,
                0.25,
            };

            foreach (var element in gridView.Columns)
                element.Width = workingWidth * columnRatio[gridView.Columns.IndexOf(element)];
        }

        private void HideAnimation(Panel target)
        {
            Storyboard HideStoryBoard = new Storyboard();

            DoubleAnimation FadeOutAnimation = new DoubleAnimation(0, new Duration(new TimeSpan(0, 0, 0, 0, 300)));
            FadeOutAnimation.EasingFunction = new QuadraticEase();
            Storyboard.SetTargetProperty(FadeOutAnimation, new PropertyPath(OpacityProperty));

            Storyboard.SetTarget(FadeOutAnimation, target);

            ThicknessAnimation ShiftLeftAnimation = new ThicknessAnimation(new Thickness(0, 0, 200, 5), new Duration(new TimeSpan(0, 0, 0, 0, 300)));
            ShiftLeftAnimation.EasingFunction = new QuadraticEase();
            Storyboard.SetTargetProperty(ShiftLeftAnimation, new PropertyPath(MarginProperty));

            Storyboard.SetTarget(ShiftLeftAnimation, target);

            HideStoryBoard.Children.Add(FadeOutAnimation);
            HideStoryBoard.Children.Add(ShiftLeftAnimation);
            HideStoryBoard.Begin();
        }

        private void ShowAnimation(Panel target)
        {
            Storyboard HideStoryBoard = new Storyboard();

            DoubleAnimation FadeOutAnimation = new DoubleAnimation(1, new Duration(new TimeSpan(0, 0, 0, 0, 300)));
            FadeOutAnimation.EasingFunction = new QuadraticEase();
            Storyboard.SetTargetProperty(FadeOutAnimation, new PropertyPath(OpacityProperty));

            Storyboard.SetTarget(FadeOutAnimation, target);

            ThicknessAnimation ShiftLeftAnimation = new ThicknessAnimation(new Thickness(10, 0, 40, 5), new Duration(new TimeSpan(0, 0, 0, 0, 300)));
            ShiftLeftAnimation.EasingFunction = new QuadraticEase();
            Storyboard.SetTargetProperty(ShiftLeftAnimation, new PropertyPath(MarginProperty));

            Storyboard.SetTarget(ShiftLeftAnimation, target);

            HideStoryBoard.Children.Add(FadeOutAnimation);
            HideStoryBoard.Children.Add(ShiftLeftAnimation);
            HideStoryBoard.Begin();
        }

        private void SecondProcedure(Panel target)
        {
            foreach(var element in target.Children)
            {
                
            }
            HideAnimation(target);
            PointProcedure.Children.Remove(target);

            Grid PointContainer = new Grid
            {
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(100, 0, 40, 5),
                Opacity = 0,
                Children =
                {
                    new View.CustomSlider()
                }
            };

            PointProcedure.Children.Add(PointContainer);
            ShowAnimation(PointContainer);
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string command = SearchCommand.Text;
            listviewCollection.Clear();

            foreach(JObject student in studentList)
            {
                if (student["USER_SCHOOL_NUMBER"].ToString().Contains(command)||
                    student["USER_NAME"].ToString().Contains(command))
                {
                    listviewCollection.Add(new ViewModel.StudentListViewModel(
                    roomNumber: student["user_school_room_number"] != null ? student["user_school_room_number"].ToString() : "NULL",
                    classNumber: student["USER_SCHOOL_NUMBER"].ToString(),
                    name: student["USER_NAME"].ToString(),
                    IsChecked: false,
                    goodPoint: 0,
                    badPoint: 0));
                }
            }
        }

        private void SearchCommand_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                SearchButton_Click(sender, null);
        }

        /// <summary>
        /// 명단 넣어줌
        /// </summary>
        private void setStudentData()
        {
            JArray list = new JArray();

            var studentExcel = ExcelProcessing.OpenExcelDB(@"C:\Users\thakd\Downloads\2017 대덕SW마이스터고 우정관 명렬표.xlsx");
            var studentList = studentExcel.Tables[0];

            foreach(DataRow row in studentList.Rows)
            {
                foreach(var item in row.ItemArray)
                {
                    JObject obj = new JObject();
                    int num;
                    if(int.TryParse(item.ToString(), out num))
                    {
                        obj.Add("USER_SCHOOL_NUMBER", num);

                        var name = row.ItemArray[Array.IndexOf(row.ItemArray, item) + 1].ToString();
                        if(name != "")
                            obj.Add("USER_NAME", name);
                        else
                            obj.Add("USER_NAME", "이름 없음");

                        obj.Add("TOTAL_GOOD_SCORE", 0);

                        obj.Add("TOTAL_BAD_SCORE", 0);

                        obj.Add("PUNISH_STATUS", 0);
                        list.Add(obj);
                    }
                }
            }

            Info.multiJson(Info.Server.SET_STUDENT_DATA, list);
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var element in SearchList.Items)
                SearchList.SelectedItems.Add(element);
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            SearchList.SelectedItems.Clear();
        }
    }
}
