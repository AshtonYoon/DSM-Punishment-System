using DormitoryGUI.Model;
using DormitoryGUI.View;
using DormitoryGUI.ViewModel;
using Microsoft.Win32;
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


            UnCheckedEventHandler += new RoutedEventHandler((s, e) =>
            {
                var target = GetAncestorOfType<ListView>(s as CheckBox);
                foreach (var element in target.Items)
                {
                    ((StudentListViewModel) element).IsChecked = false;
                }
                target.SelectedItems.Clear();

                target.Items.Refresh();
            });

            CheckedEventHandler += new RoutedEventHandler((s, e) =>
            {
                var target = GetAncestorOfType<ListView>(s as CheckBox);
                foreach (var element in target.Items)
                {
                    ((StudentListViewModel) element).IsChecked = true;
                    target.SelectedItems.Add(element);
                }

                target.Items.Refresh();
            });
            
            PunishmentList.Click += (s, e) => { mainWindow.NavigatePage(new PunishmentListPage(mainWindow)); };
            CheckTarget.Click += (s, e) => { mainWindow.NavigatePage(new CheckPunishmentTargetPage(mainWindow)); };

            this.mainWindow = mainWindow;

            Update();
        }

        public void Update()
        {
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
        }

        private enum Step
        {
            First,
            Second,
            Third,
        }

        private Step CurrentStep = Step.First;

        private void ApplyPointButton_Click(object sender, RoutedEventArgs e)
        {
            if (((bool) Good.IsChecked || (bool) Bad.IsChecked) && CurrentStep == Step.First)
            {
                HideAnimation(FirstGrid);
                ShowAnimation(SecondGrid);

                if ((bool) Good.IsChecked)
                    PunishmentComboBox.PunishmentType = (int) Info.POINT_TYPE.GOOD;

                else if ((bool) Bad.IsChecked)
                    PunishmentComboBox.PunishmentType = (int) Info.POINT_TYPE.BAD;

                CurrentStep = Step.Second;
            }

            if (PunishmentComboBox.SelectedItem != null && CurrentStep == Step.Second)
            {
                HideAnimation(SecondGrid);
                ShowAnimation(ThirdGrid);

                PunishmentSlider.SliderValue =
                    ((PunishmentListViewModel) PunishmentComboBox.SelectedItem).MinimumPoint;

                CurrentStep = Step.Third;
                return;
            }

            if (CurrentStep == Step.Third)
            {
                if (MessageBox.Show("점수를 부여하시겠습니까?", "알림", MessageBoxButton.YesNo, MessageBoxImage.Question) ==
                    MessageBoxResult.Yes)
                {
                    /** Response
                        json = {
	                        "DEST_UUID":123
	                        "TARGET":[12,32,43,STUDENT_UUID]
	                        "POINT_UUID":123
	                        "POINT_VALUE":1567
                        } 
                    */
                    JObject obj = new JObject
                    {
                        {"DEST_UUID", Info.mainPage.TeacherUUID}
                    };

                    PunishmentListViewModel item = PunishmentComboBox.SelectedItem as PunishmentListViewModel;
                    obj.Add("POINT_UUID", item.PointUUID);

                    if (PunishmentSlider.SliderValue >= item.MinimumPoint &&
                        PunishmentSlider.SliderValue <= item.MaximumPoint)
                    {
                        // 부여하고자 하는 벌점이 최댓값, 최솟값을 넘지 않는지에 대해 검사

                        obj.Add("POINT_VALUE", PunishmentSlider.SliderValue);

                        var targets = new JArray();

                        foreach (StudentListViewModel element in ResultList.Items)
                            targets.Add(element.UserUUID);

                        obj.Add("TARGET", targets);
                        Info.MultiJson(Info.Server.GIVE_SCORE, obj);

                        listviewCollection.Clear();
                        Update();
                        MessageBox.Show("처리가 완료되었습니다.");

                        CurrentStep = Step.First;

                        HideAnimation(ThirdGrid);
                        ShowAnimation(FirstGrid);
                    }
                    else
                    {
                        MessageBox.Show("벌점은 최댓값과 최솟값을 넘을 수 없습니다.");
                    }
                }
            }
        }

        private void ApplyPointBackButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentStep == Step.Second)
            {
                HideAnimation(SecondGrid);
                ShowAnimation(FirstGrid);

                CurrentStep = Step.First;
                return;
            }

            if (CurrentStep == Step.Third)
            {
                CurrentStep = Step.Second;

                HideAnimation(ThirdGrid);
                ShowAnimation(SecondGrid);
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
                0.14,
                0.14,
                0.14,
                0.14,
                0.14,
                0.25,
            };

            foreach (var element in gridView.Columns)
                element.Width = workingWidth * columnRatio[gridView.Columns.IndexOf(element)];
        }

        private void HideAnimation(Panel target)
        {
            var Duration = new Duration(new TimeSpan(0, 0, 0, 0, 600));

            Storyboard HideStoryBoard = new Storyboard();

            DoubleAnimation FadeOutAnimation = new DoubleAnimation(0, Duration)
            {
                EasingFunction = new QuadraticEase()
            };

            Storyboard.SetTargetProperty(FadeOutAnimation, new PropertyPath(OpacityProperty));

            Storyboard.SetTarget(FadeOutAnimation, target);

            ThicknessAnimation ShiftLeftAnimation =
                new ThicknessAnimation(new Thickness(0, 0, 200, target.Margin.Bottom), Duration)
                {
                    EasingFunction = new QuadraticEase()
                };

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

            DoubleAnimation FadeInAnimation = new DoubleAnimation(1, Duration)
            {
                EasingFunction = new QuadraticEase()
            };

            Storyboard.SetTargetProperty(FadeInAnimation, new PropertyPath(OpacityProperty));

            Storyboard.SetTarget(FadeInAnimation, target);

            ThicknessAnimation ShiftLeftAnimation = new ThicknessAnimation(new Thickness(0, 0, 0, target.Margin.Bottom), Duration)
            {
                EasingFunction = new QuadraticEase()
            };

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

            foreach (JObject student in studentList)
            {
                if (student["USER_SCHOOL_NUMBER"].ToString().Contains(command) ||
                    student["USER_NAME"].ToString().Contains(command) ||
                    student["TOTAL_GOOD_SCORE"].ToString().Contains(command) ||
                    student["TOTAL_BAD_SCORE"].ToString().Contains(command))
                {
                    listviewCollection.Add(new ViewModel.StudentListViewModel(
                        roomNumber: student["user_school_room_number"] != null ? student["user_school_room_number"].ToString() : "NULL",
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

        private void SearchCommand_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                SearchButton_Click(sender, null);
        }

        /// <summary>
        /// 명단 넣어줌
        /// </summary>
        private void SetStudentData()
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Filter = "Excel Files (*.xlsx)|*.xls"
            };
            bool? result = dialog.ShowDialog();
            if ((bool) result)
            {
                JArray list = new JArray();

                var studentExcel = ExcelProcessing.OpenExcelDB(dialog.FileName);
                var studentList = studentExcel.Tables[0];

                foreach (DataRow row in studentList.Rows)
                {
                    foreach (var item in row.ItemArray)
                    {
                        JObject obj = new JObject();
                        if (int.TryParse(item.ToString(), out int num))
                        {
                            obj.Add("USER_SCHOOL_NUMBER", num);

                            var name = row.ItemArray[Array.IndexOf(row.ItemArray, item) + 1].ToString();

                            if (name != "")
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

                Info.MultiJson(Info.Server.SET_STUDENT_DATA, list);
                MessageBox.Show("데이터 설정이 완료되었습니다.");
            }
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
            foreach (StudentListViewModel element in SearchList.Items)
            {
                if (element.IsChecked)
                {
                    resultListCollection.Add(
                        new StudentListViewModel(
                            isChecked: element.IsChecked,
                            roomNumber: element.RoomNumber,
                            name: element.Name,
                            classNumber: element.ClassNumber,
                            goodPoint: element.GoodPoint,
                            badPoint: element.BadPoint,
                            currentStep: element.CurrentStep,
                            userUUID: element.UserUUID
                        )
                    );

                    element.IsChecked = false;
                }
            }

            ResultList.ItemsSource = Deduplication(resultListCollection as IEnumerable<StudentListViewModel>);
            ResultList.Items.Refresh();

            SearchList.Items.Refresh();
        }

        private T GetAncestorOfType<T>(FrameworkElement child) where T : FrameworkElement
        {
            var parent = VisualTreeHelper.GetParent(child);
            if (parent != null && !(parent is T))
                return GetAncestorOfType<T>((FrameworkElement) parent);

            return (T) parent;
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
                ((StudentListViewModel) element).IsChecked = true;
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
            if (e.Key == Key.Delete)
                DeleteSelectedItem();
        }

        private void DeleteSelectedItem()
        {
            // resultListCollection 중 IsChecked가 true 인 아이템 제거

            resultListCollection
                .Where(x => x.IsChecked).ToList()
                .All(i => resultListCollection.Remove(i));

            // 이후 이를 ResultList의 ItemSource에 대입하고 Refresh

            ResultList.ItemsSource = resultListCollection;
            ResultList.Items.Refresh();
        }

        private void SelectAllCommand_Click(object sender, RoutedEventArgs e)
        {
            foreach (StudentListViewModel element in ResultList.Items)
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

        private void Permission_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.NavigatePage(new PermissionManagementPage(mainWindow));
        }

        private void DownloadExcel_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog()
            {
                Filter = "Excel Files (*.xlsx)|*.xlsx"
            };
            
            if (!(bool)saveFileDialog.ShowDialog())
            {
                return;
            }

            var dataSet = new DataSet();

            for (int i = 1; i <= 3; i++)
            {
                var items = listviewCollection.Where(s => s.ClassNumber.StartsWith(i.ToString()));
                var dataTable = new DataTable(string.Format("{0}학년", i));

                dataTable.Columns.Add("학번");
                dataTable.Columns.Add("이름");
                dataTable.Columns.Add("상점");
                dataTable.Columns.Add("벌점");
                dataTable.Columns.Add("상점 내역");
                dataTable.Columns.Add("벌점 내역");
                dataTable.Columns.Add("교육 단계");

                foreach (StudentListViewModel item in items)
                {
                    JObject jobj = new JObject
                    {
                        {"USER_UUID", item.UserUUID }
                    };

                    JArray logs = Info.MultiJson(Info.Server.STUDENT_LOG, jobj) as JArray;

                    StringBuilder goodLogsBuilder = new StringBuilder();
                    StringBuilder badLogsBuilder = new StringBuilder();

                    if (logs != null)
                    {
                        foreach (JObject log in logs)
                        {
                            switch ((int)log["POINT_TYPE"])
                            {
                                case 0:
                                    goodLogsBuilder.AppendFormat("{0} ({1}점)\n", log["POINT_MEMO"], log["POINT_VALUE"]);
                                    break;

                                case 1:
                                    badLogsBuilder.AppendFormat("{0} ({1}점)\n", log["POINT_MEMO"], log["POINT_VALUE"]);
                                    break;
                            }
                        }
                    }

                    int goodLogsCount = goodLogsBuilder.ToString().Length;
                    string goodLogsString = (goodLogsCount > 0) ? goodLogsBuilder.ToString().Substring(0, goodLogsCount - 1) : string.Empty;

                    int badLogsCount = badLogsBuilder.ToString().Length;
                    string badLogsString = (badLogsCount > 0) ? badLogsBuilder.ToString().Substring(0, badLogsCount - 1) : string.Empty;

                    dataTable.Rows.Add(
                        new object[]
                        {
                            item.ClassNumber,
                            item.Name,
                            item.GoodPoint,
                            item.BadPoint,
                            goodLogsString,
                            badLogsString,
                            item.CurrentStep
                        }
                    );
                }

                dataSet.Tables.Add(dataTable);
            }

            if (ExcelProcessing.SaveExcelDB(saveFileDialog.FileName, dataSet))
            {
                MessageBox.Show("엑셀 데이터 저장 성공");
            }

            else
            {
                MessageBox.Show("엑셀 데이터 저장 실패");
            }
        }

        private void Log_Click(object sender, RoutedEventArgs e)
        {
            var target = (StudentListViewModel) GetAncestorOfType<ListViewItem>(sender as Button).DataContext;

            mainWindow.NavigatePage(
                new PunishmentLogPage(
                    mainWindow,
                    target.Name,
                    target.ClassNumber,
                    target.GoodPoint,
                    target.BadPoint,
                    target.CurrentStep,
                    target.UserUUID
                )
            );
        }

        private void SearchCommand_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}