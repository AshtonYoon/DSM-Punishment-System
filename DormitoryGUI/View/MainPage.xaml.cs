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
            var responseDict = Info.GenerateRequest("GET", Info.Server.MANAGING_STUDENT, Info.mainPage.AccessToken, "");

            if ((HttpStatusCode)responseDict["status"] != HttpStatusCode.OK)
            {
                return;
            }

            studentList = JArray.Parse(responseDict["body"].ToString());

            foreach (JObject student in studentList)
            {

                listviewCollection.Add(new ViewModel.StudentListViewModel(
                    id: student["id"].ToString(),
                    classNumber: student["number"].ToString(),
                    name: student["name"].ToString(),
                    goodPoint: student["good_point"].Type == JTokenType.Null ? 0 : int.Parse(student["good_point"].ToString()),
                    badPoint: student["bad_point"].Type == JTokenType.Null ? 0 : int.Parse(student["bad_point"].ToString()),
                    currentStep: student["penalty_training_status"].Type == JTokenType.Null ? 0 : int.Parse(student["penalty_training_status"].ToString()),
                    isChecked: false
                ));
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

                var target = ((PunishmentListViewModel)PunishmentComboBox.SelectedItem);

                int minPoint = target.MinPoint < 0 ? -1 * target.MinPoint : target.MinPoint;

                PunishmentSlider.SliderValue = minPoint;

                CurrentStep = Step.Third;

                return;
            }

            if (CurrentStep == Step.Third)
            {
                if (MessageBox.Show("점수를 부여하시겠습니까?", "알림", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    /** Request
                        json = {
	                        "id":city7320 (student ID)
	                        "rule_id":50045
	                        "point":12
                        } 
                    */

                    PunishmentListViewModel item = PunishmentComboBox.SelectedItem as PunishmentListViewModel;

                    int minPoint = item.MinPoint < 0 ? -1 * item.MinPoint : item.MinPoint;
                    int maxPoint = item.MaxPoint < 0 ? -1 * item.MaxPoint : item.MaxPoint;

                    if (PunishmentSlider.SliderValue < item.MaxPoint || PunishmentSlider.SliderValue > item.MaxPoint )
                    {
                        // 부여하고자 하는 벌점이 최댓값, 최솟값을 넘지 않는지에 대해 검사

                        MessageBox.Show("최댓값과 최솟값을 초과할 수 없음");
                        return;
                    }

                    bool operationComplete = true;

                    foreach (StudentListViewModel student in ResultList.Items)
                    {
                        int point = item.MinPoint < 0 ? -1 * PunishmentSlider.SliderValue : PunishmentSlider.SliderValue;

                        JObject jsonBody = new JObject
                        {
                            { "id", student.ID },
                            { "rule_id", item.ID },
                            { "point", point },
                        };
                        
                        var responseDict = Info.GenerateRequest("POST", Info.Server.MANAGING_POINT, Info.mainPage.AccessToken, jsonBody);

                        if ((HttpStatusCode)responseDict["status"] != HttpStatusCode.Created)
                        {
                            operationComplete = false;
                            MessageBox.Show("처리 실패");

                            break; 
                        }
                    }

                    if (operationComplete)
                    {
                        MessageBox.Show("처리 완료");
                    }

                    listviewCollection.Clear();
                    Update();

                    CurrentStep = Step.First;

                    HideAnimation(ThirdGrid);
                    ShowAnimation(FirstGrid);
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
                if (student["number"].ToString().Contains(command) ||
                    student["name"].ToString().Contains(command) ||
                    student["good_point"].ToString().Contains(command) ||
                    student["bad_point"].ToString().Contains(command))
                {
                    listviewCollection.Add(new ViewModel.StudentListViewModel(
                        id: student["id"].ToString(),
                        classNumber: student["number"].ToString(),
                        name: student["name"].ToString(),
                        goodPoint: student["good_point"] == null ? 0 : int.Parse(student["good_point"].ToString()),
                        badPoint: student["bad_point"] == null ? 0 : int.Parse(student["bad_point"].ToString()),
                        currentStep: student["penalty_training_status"] == null ? 0 : int.Parse(student["penalty_training_status"].ToString()),
                        isChecked: false));
                }
            }
        }

        private void SearchCommand_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                SearchButton_Click(sender, null);
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
                            id: element.ID,
                            isChecked: element.IsChecked,
                            name: element.Name,
                            classNumber: element.ClassNumber,
                            goodPoint: element.GoodPoint,
                            badPoint: element.BadPoint,
                            currentStep: element.CurrentStep
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
                    var responseDict = Info.GenerateRequest("GET", $"{Info.Server.MANAGING_POINT}/{item.ID}", "", "") ;

                    if ((HttpStatusCode)responseDict["status"] != HttpStatusCode.OK)
                    {
                        MessageBox.Show("상벌점 내역 조회 실패");
                        return;
                    }

                    JArray logs = JArray.Parse(responseDict["body"].ToString());

                    StringBuilder goodLogsBuilder = new StringBuilder();
                    StringBuilder badLogsBuilder = new StringBuilder();

                    if (logs != null)
                    {
                        foreach (JObject log in logs)
                        {
                            if ((int)log["point"] > 0)
                            {
                                goodLogsBuilder.AppendFormat("{0} ({1}점)\n", log["reason"], log["point"]);
                            }
                            else if ((int)log["point"] < 0)
                            {
                                badLogsBuilder.AppendFormat("{0} ({1}점)\n", log["reason"], log["point"]);
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
                    mainWindow: mainWindow,
                    id: target.ID,
                    name: target.Name,
                    classNumber: target.ClassNumber,
                    goodPoint: target.GoodPoint,
                    badPoint: target.BadPoint,
                    currentStep: target.CurrentStep
                )
            );
        }

        private void SearchCommand_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}