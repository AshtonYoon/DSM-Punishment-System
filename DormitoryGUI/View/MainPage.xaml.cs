using DormitoryGUI.Model;
using DormitoryGUI.View;
using DormitoryGUI.ViewModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
        private StudentList listviewCollection;
        private StudentList resultListCollection;

        private JArray studentList;
        private readonly MainWindow mainWindow;

        private RoutedEventHandler UnCheckedEventHandler;
        private RoutedEventHandler CheckedEventHandler;

        public MainPage(MainWindow mainWindow)
        {
            InitializeComponent();

            listviewCollection = Resources["StudentListKey"] as ViewModel.StudentList;
            resultListCollection = Resources["ResultListKey"] as ViewModel.StudentList;

            UnCheckedEventHandler += new RoutedEventHandler((s, e)=> {
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

            UploadExcel.Click += (s, e) =>
            {
                setStudentData();
                MessageBox.Show("데이터 설정이 완료되었습니다.");
            };

            PunishmentList.Click += (s, e) =>
            {
                mainWindow.NavigatePage(new PunishmentListPage());
            };

            //((INotifyCollectionChanged)SearchList.Items).CollectionChanged += ListView_CollectionChanged;

            this.mainWindow = mainWindow;

            update();
        }

        //private void ListView_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        //{
        //    if(e.Action == NotifyCollectionChangedAction.Add)
        //    {

        //    }
        //}

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
                    isChecked: false,
                    goodPoint: int.Parse(json["TOTAL_GOOD_SCORE"].ToString()),
                    badPoint: int.Parse(json["TOTAL_BAD_SCORE"].ToString()),
                    userUUID: int.Parse(json["USER_UUID"].ToString())));
            }
        }

        private Step CurrentStep = Step.First;

        private enum Step { First, Second, Third };

        private void ApplyPointButton_Click(object sender, RoutedEventArgs e)
        {
            if (((bool)Good.IsChecked || (bool)Bad.IsChecked) && CurrentStep == Step.First)
            {
                HideAnimation(FirstGrid);
                ShowAnimation(SecondGrid);

                if(((bool)Good.IsChecked))
                    PunishmentComboBox.PunishmentType = 1;

                else if (((bool)Bad.IsChecked))
                    PunishmentComboBox.PunishmentType = 0;

                CurrentStep = Step.Second;
            }

            if(PunishmentComboBox.SelectedItem != null && CurrentStep == Step.Second)
            {
                //Do something
                HideAnimation(SecondGrid);
                ShowAnimation(ThirdGrid);

                PunishmentSlider.SliderValue = (PunishmentComboBox.SelectedItem as PunishmentListViewModel).MinimumPoint;

                CurrentStep = Step.Third;
            }

            if(CurrentStep == Step.Third)
            {
                //Do something
                /**
                    json = {
	                    "DEST_UUID":123
	                    "TARGET":[12,32,43,STUDENT_UUID]
	                    "POINT_UUID":123
	                    "POINT_VALUE":1567
                    } 
                */
                JObject obj = new JObject();
                obj.Add("DEST_UUID", Info.mainPage.TeacherUUID);

                PunishmentListViewModel item = PunishmentComboBox.SelectedItem as PunishmentListViewModel;
                obj.Add("POINT_UUID", item.PointUUID);

                obj.Add("POINT_VALUE", PunishmentSlider.SliderValue);

                var targets = new JArray();

                foreach (StudentListViewModel element in ResultList.Items)
                    targets.Add(element.UserUUID);

                obj.Add("TARGET", targets);

                Info.multiJson(Info.Server.GIVE_SCORE, obj);

                MessageBox.Show("처리가 완료되었습니다.");
                //CurrentStep = Step.First;
            }
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
            var Duration = new Duration(new TimeSpan(0, 0, 0, 0, 600));

            Storyboard HideStoryBoard = new Storyboard();

            DoubleAnimation FadeOutAnimation = new DoubleAnimation(0, Duration);
            FadeOutAnimation.EasingFunction = new QuadraticEase();
            Storyboard.SetTargetProperty(FadeOutAnimation, new PropertyPath(OpacityProperty));

            Storyboard.SetTarget(FadeOutAnimation, target);

            ThicknessAnimation ShiftLeftAnimation = new ThicknessAnimation(new Thickness(0, 0, 200, target.Margin.Bottom), Duration);
            ShiftLeftAnimation.EasingFunction = new QuadraticEase();
            Storyboard.SetTargetProperty(ShiftLeftAnimation, new PropertyPath(MarginProperty));

            Storyboard.SetTarget(ShiftLeftAnimation, target);

            HideStoryBoard.Children.Add(FadeOutAnimation);
            HideStoryBoard.Children.Add(ShiftLeftAnimation);

            target.Visibility = Visibility.Hidden;
            HideStoryBoard.Begin();
        }

        private void ShowAnimation(Panel target)
        {
            var Duration = new Duration(new TimeSpan(0, 0, 0, 0, 600));
            Storyboard HideStoryBoard = new Storyboard();

            DoubleAnimation FadeInAnimation = new DoubleAnimation(1, Duration);
            FadeInAnimation.EasingFunction = new QuadraticEase();
            Storyboard.SetTargetProperty(FadeInAnimation, new PropertyPath(OpacityProperty));

            Storyboard.SetTarget(FadeInAnimation, target);

            ThicknessAnimation ShiftLeftAnimation = new ThicknessAnimation(new Thickness(0, 0, 0, target.Margin.Bottom), Duration);
            ShiftLeftAnimation.EasingFunction = new QuadraticEase();
            Storyboard.SetTargetProperty(ShiftLeftAnimation, new PropertyPath(MarginProperty));

            Storyboard.SetTarget(ShiftLeftAnimation, target);

            HideStoryBoard.Children.Add(FadeInAnimation);
            HideStoryBoard.Children.Add(ShiftLeftAnimation);

            target.Visibility = Visibility.Visible;
            HideStoryBoard.Begin();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string command = SearchCommand.Text;
            listviewCollection.Clear();

            foreach(JObject student in studentList)
            {
                if (student["USER_SCHOOL_NUMBER"].ToString().Contains(command)||
                    student["USER_NAME"].ToString().Contains(command)||
                    student["TOTAL_GOOD_SCORE"].ToString().Contains(command)||
                    student["TOTAL_BAD_SCORE"].ToString().Contains(command))
                {
                    listviewCollection.Add(new ViewModel.StudentListViewModel(
                    roomNumber: student["user_school_room_number"] != null ? student["user_school_room_number"].ToString() : "NULL",
                    classNumber: student["USER_SCHOOL_NUMBER"].ToString(),
                    name: student["USER_NAME"].ToString(),
                    isChecked: false,
                    goodPoint: int.Parse(student["TOTAL_GOOD_SCORE"].ToString()),
                    badPoint: int.Parse(student["TOTAL_BAD_SCORE"].ToString()),
                    userUUID: int.Parse(student["USER_UUID"].ToString())));
                }
            }
           
            //UnCheckedEventHandler?.Invoke(sender, e);
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
            CheckedEventHandler?.Invoke(sender, e);
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            UnCheckedEventHandler?.Invoke(sender, e);
        }

        private void ResultList_SizeChanged(object sender, SizeChangedEventArgs e)
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

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            foreach(StudentListViewModel element in SearchList.Items)
            {
                if (element.IsChecked)
                {
                    resultListCollection.Add(new StudentListViewModel(
                        isChecked: element.IsChecked,
                        roomNumber: element.RoomNumber,
                        name: element.Name,
                        classNumber: element.ClassNumber,
                        goodPoint: element.GoodPoint,
                        badPoint: element.BadPoint,
                        userUUID: element.UserUUID));
                }
            }

            ResultList.ItemsSource = Deduplication(resultListCollection as IEnumerable<StudentListViewModel>);
            ResultList.Items.Refresh();
        }

        private T GetAncestorOfType<T>(FrameworkElement child)  where T : FrameworkElement
        {
            var parent = VisualTreeHelper.GetParent(child);
            if (parent != null && !(parent is T))
                return GetAncestorOfType<T>((FrameworkElement)parent);

            return (T)parent;
        }

        private void ResultList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AddSelectionByClick(ResultList, e);
        }

        private void SearchList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AddSelectionByClick(SearchList, e);
        }

        private void AddSelectionByClick(ListView listview, SelectionChangedEventArgs e)
        {
            var targets = e.AddedItems;
            foreach (var element in targets)
            {
                ((StudentListViewModel)element).IsChecked = true;
            }
            listview.Items.Refresh();
        }

        private IEnumerable<StudentListViewModel> Deduplication(IEnumerable<StudentListViewModel> source)
        {
            return source.GroupBy(x => x.ClassNumber)
                                 .Select(y => y.First());
        }

        private void ResultList_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            ContextMenu menu = FindResource("ListviewItemControlMenu") as ContextMenu;

            menu.PlacementTarget = sender as ListView;
            menu.IsOpen = true;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            DeleteSelectedItem();
        }

        private void ResultList_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Delete)
                DeleteSelectedItem();
        }

        private void DeleteSelectedItem()
        {
            ResultList.ItemsSource = resultListCollection.Where(x => !x.IsChecked);
            ResultList.Items.Refresh();
        }

        private void SelectAllCommand_Click(object sender, RoutedEventArgs e)
        {
            foreach(StudentListViewModel element in ResultList.Items)
            {
                element.IsChecked = true;
                ResultList.SelectedItems.Add(element);
            }
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

        private void DeselectAllCommand_Click(object sender, RoutedEventArgs e)
        {
            foreach (StudentListViewModel element in ResultList.Items)
            {
                element.IsChecked = false;
                ResultList.SelectedItems.Clear();
            }
        }
    }
}