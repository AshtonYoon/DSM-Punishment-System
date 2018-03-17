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

        private string filter = "전체";

        public MainPage(MainWindow mainWindow)
        {
            InitializeComponent();

            listviewCollection = Resources["StudentListKey"] as ViewModel.StudentList;
            resultListCollection = Resources["ResultListKey"] as ViewModel.StudentList;

            PunishmentList.Click += (s, e) => {
                PointManageDialog pointManageDialog = new PointManageDialog();
                ShowModal(pointManageDialog);               
            };

            CheckTarget.Click += (s, e) => {
                PunishmentTargetDialog punishmentTargetDialog = new PunishmentTargetDialog();
                ShowModal(punishmentTargetDialog);

                resultListCollection.Clear();

                ResultList.ItemsSource = resultListCollection;
                ResultList.Items.Refresh();

                Update();
            };

            Update();

            this.mainWindow = mainWindow;
        }

        public void Update()
        {
            listviewCollection.Clear();

            var responseDict = Info.GenerateRequest("GET", Info.Server.MANAGING_STUDENT, Info.mainPage.AccessToken, "");

            if ((HttpStatusCode) responseDict["status"] != HttpStatusCode.OK)
            {
                MessageBox.Show("학생 목록 조회 실패");
                return;
            }

            studentList = JArray.Parse(responseDict["body"].ToString());

            //
            foreach (JObject student in studentList)
            {
              /*  if ((filter != "전체") && (student["number"].ToString()[0] != filter[0]))
                {
                    continue;
                }*/

                StudentListViewModel item = new ViewModel.StudentListViewModel(
                    id: student["id"].ToString(),
                    classNumber: student["number"].ToString(),
                    name: student["name"].ToString(),
                    goodPoint: student["good_point"].Type == JTokenType.Null ? 0 : int.Parse(student["good_point"].ToString()),
                    badPoint: student["bad_point"].Type == JTokenType.Null ? 0 : int.Parse(student["bad_point"].ToString()),
                    //penaltyTrainingStaus: Info.ParseStatus(student["penalty_training_status"].Type == JTokenType.Null ? 0 : int.Parse(student["penalty_training_status"].ToString())),
                    penaltyTrainingStaus: bool.Parse(student["penalty_training_status"].ToString()),
                    penaltyLevel: bool.Parse(student["penalty_training_status"].ToString()) == true ? Info.ParseStatus((int)student["penalty_level"]) : " ",
//                    penaltyLevel: Info.ParseStatus((int)student["penalty_level"]),
//                    penaltyTrainingStaus: false,
//                    penaltyLevel: Info.ParseStatus(student["penalty_level"].Type == JTokenType.Null ? 0 : (int)student["penalty_level"]),
                    isSelected: false
                );

                JArray logs = student["point_histories"] as JArray;

                foreach(JObject log in logs)
                {
                    item.PunishLogs.Add(new PunishLogListViewModel(
                        score: (int) log["point"],
                        reason: log["reason"].ToString(),
                        time: DateTime.Parse(log["time"].ToString()).ToString("yyyy-MM-dd"),
                        pointType: log["point_type"].ToString() == null ? true : false                 
                    ));
                }

                listviewCollection.Add(item);
            }

            SearchList.Items.Refresh(); 
        }

        private void ApplyPointButton_Click(object sender, RoutedEventArgs e)
        {
            PointDialog pointDialog = new PointDialog((bool) GoodPunishCheck.IsChecked ? (int) Info.POINT_TYPE.GOOD : (int)Info.POINT_TYPE.BAD);

            if (!(bool)ShowModal(pointDialog))
            {
                return;
            }

            bool operationComplete = true;

            foreach (StudentListViewModel student in resultListCollection)
            {
                var requestDict = new Dictionary<string, object>
                {
                    { "id", student.ID },
                    { "rule_id", pointDialog.PunishmentID },
                    { "point", pointDialog.PunishmentScore },                    
                };

                var responseDict = Info.GenerateRequest("POST", Info.Server.MANAGING_POINT, Info.mainPage.AccessToken, requestDict);

                if ((HttpStatusCode)responseDict["status"] != HttpStatusCode.Created)
                {
                    operationComplete = false;
                    break;
                }
            }

            if (operationComplete)
            {
                MessageBox.Show("처리 완료");
            }
            else
            {
                MessageBox.Show("처리 실패");
            }

            resultListCollection.Clear();

            ResultList.ItemsSource = resultListCollection;
            ResultList.Items.Refresh();
            
            Update();
        }

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            var target = (StudentListViewModel) GetAncestorOfType<ListViewItem>(sender as Button).DataContext;
            resultListCollection.Remove(target);

            ResultList.ItemsSource = resultListCollection;
            ResultList.Items.Refresh();
        }

        private void SearchList_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var listView = sender as ListView;
            var gridView = listView.View as GridView;

            var workingWidth = listView.ActualWidth - 18;

            double[] columnRatio =
            {
                0.2,
                0.2,
                0.2,
                0.2,
                0.2,
            };

            foreach (var element in gridView.Columns)
                element.Width = workingWidth * columnRatio[gridView.Columns.IndexOf(element)];
        }

        /* private void HideAnimation(Panel target)
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

            ThicknessAnimation ShiftLeftAnimation =
                new ThicknessAnimation(new Thickness(0, 0, 0, target.Margin.Bottom), Duration)
                {
                    EasingFunction = new QuadraticEase()
                };

            Storyboard.SetTargetProperty(ShiftLeftAnimation, new PropertyPath(MarginProperty));
            Storyboard.SetTarget(ShiftLeftAnimation, target);

            HideStoryBoard.Children.Add(FadeInAnimation);
            HideStoryBoard.Children.Add(ShiftLeftAnimation);

            target.Visibility = Visibility.Visible;
            HideStoryBoard.Begin();
        } */

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
                        goodPoint: student["good_point"].Type == JTokenType.Null ? 0 : (int)student["good_point"],
                        badPoint: student["bad_point"].Type == JTokenType.Null ? 0 : (int)student["bad_point"],
                        penaltyTrainingStaus: bool.Parse(student["penalty_training_status"].ToString()),
                        penaltyLevel: bool.Parse(student["penalty_training_status"].ToString()) == true ? Info.ParseStatus((int)student["penalty_level"]) : " ",
                        isSelected: false));
                }
            }
        }

        private void SearchCommand_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                SearchButton_Click(sender, null);
        }

        private void ResultList_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var listView = sender as ListView;
            var gridView = listView.View as GridView;

            var workingWidth = listView.ActualWidth - 18;

            double[] columnRatio =
            {
                0.33,
                0.33,
                0.33
            };

            foreach (var element in gridView.Columns)
                element.Width = workingWidth * columnRatio[gridView.Columns.IndexOf(element)];
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (StudentListViewModel element in SearchList.SelectedItems)
            {
                resultListCollection.Add(
                    new StudentListViewModel(
                        id: element.ID,
                        name: element.Name,
                        classNumber: element.ClassNumber,
                        goodPoint: element.GoodPoint,
                        badPoint: element.BadPoint,
                        penaltyTrainingStaus: element.PenaltyTrainingStatus,
                        penaltyLevel: element.PenaltyLevel,
                        isSelected: false
                    )
                );
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

        private IEnumerable<StudentListViewModel> Deduplication(IEnumerable<StudentListViewModel> source)
        {
            return source.GroupBy(x => x.ClassNumber)
                .Select(y => y.First());
        }

        private void ResultList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
                DeleteSelectedItem();
        }

        private void DeleteSelectedItem()
        {
            // ResultList의 Selected 된 아이템 제거

            foreach (StudentListViewModel item in ResultList.SelectedItems)
            {
                resultListCollection.Remove(item);
            }

            // 이후 이를 ResultList의 ItemSource에 대입하고 Refresh

            ResultList.ItemsSource = resultListCollection;
            ResultList.Items.Refresh();
        }

        private bool? ShowModal(Window modal)
        {
            OpacityBox.Visibility = Visibility.Visible;

            var result = modal.ShowDialog();

            OpacityBox.Visibility = Visibility.Hidden;

            return result;
        }

        private void GradeCombobox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            filter = (sender as ComboBoxItem).Content.ToString();

            if (listviewCollection != null)
            {
                Update();
            }
        }

        private void SelectAllCheck_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var item in listviewCollection)
            {
                SearchList.SelectedItems.Add(item);
            }
        }

        private void SelectAllCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (var item in listviewCollection)
            {
                SearchList.SelectedItems.Remove(item);
            }
        }
        
        private void DownloadExcel_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog()
            {
                Filter = "Excel Files (*.xlsx)|*.xlsx"
            };

            if (!(bool) saveFileDialog.ShowDialog())
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
                    StringBuilder goodLogsBuilder = new StringBuilder();
                    StringBuilder badLogsBuilder = new StringBuilder();

                    foreach (PunishLogListViewModel log in item.PunishLogs)
                    {
                        if (log.Point > 0)
                        {
                            goodLogsBuilder.AppendFormat("[{0}] {1} ({2}점) \n", log.Time, log.Reason, log.Point);
                        }
                        else
                        {
                            badLogsBuilder.AppendFormat("[{0}] {1} ({2}점) \n", log.Time, log.Reason, -log.Point);
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
                            item.PenaltyTrainingStatus
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

        private void WatchLogButton_Click(object sender, RoutedEventArgs e)
        {
            if (SearchList.SelectedItems.Count != 1)
            {
                MessageBox.Show("잘못된 접근입니다");
                return;
            }

            var target = (StudentListViewModel) SearchList.SelectedItems[0];

            var punishmentLogDialog =
                new PunishmentLogDialog (
                    id: target.ID,
                    name: target.Name,
                    classNumber: target.ClassNumber,
                    goodPoint: target.GoodPoint,
                    badPoint: target.BadPoint,
                    currentStep: target.PenaltyLevel
                );

            ShowModal(punishmentLogDialog);

            resultListCollection.Clear();

            ResultList.ItemsSource = resultListCollection;
            ResultList.Items.Refresh();

            Update();
        }

        private void SearchCommand_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem target = (sender as ComboBox).SelectedItem as ComboBoxItem;

            filter = target.Content.ToString();

            if (listviewCollection != null)
            {
                Update();
            }
        }

        private void SearchList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}