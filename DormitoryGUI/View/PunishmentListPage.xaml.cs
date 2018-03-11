using DormitoryGUI.ViewModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// PunishmentListPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PunishmentListPage : Page
    {
        private PunishmentListViewModel selectedItem;
        private JArray rules;

        private PunishmentList punishmentGoodList;
        private PunishmentList punishmentBadList;

        private readonly MainWindow mainWindow;

        public PunishmentListPage(MainWindow mainWindow)
        {
            InitializeComponent();

            DataContext = this;


            punishmentGoodList = Resources["PunishmentGoodListKey"] as PunishmentList;
            punishmentBadList = Resources["PunishmentBadListKey"] as PunishmentList;

            InitializePunishmentList();

            BackButton.Click += (s, e) => { NavigationService.GoBack(); };

            this.mainWindow = mainWindow;
        }

        private void AddPushimentListButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(CheckNameValue() && CheckSliderValue()))
                return;

            var requestDict = new Dictionary<string, object>
            {
                {"name", PunishmentName.Text},
                {"min_point", (bool) GoodPoint.IsChecked ? MinimumPoint.SliderValue : -1 * MinimumPoint.SliderValue},
                {"max_point", (bool) GoodPoint.IsChecked ? MaximumPoint.SliderValue : -1 * MaximumPoint.SliderValue}
            };
            
            var responseDict = Info.GenerateRequest("POST", Info.Server.MANAGING_RULE, Info.mainPage.AccessToken, requestDict);

            if ((HttpStatusCode)responseDict["status"] == HttpStatusCode.Created)
            {
                MessageBox.Show("항목 추가 완료");
            }
            else
            {
                MessageBox.Show("항목 추가 실패");
            }

            UpdatePunishmentList();
        }

        private void DelPunishmentListButton_Click(object sender, EventArgs e)
        {
            if (selectedItem != null)
            {
                var requestDict = new Dictionary<string, object>
                {
                    {"rule_id", selectedItem.ID}
                };

                var responseDict = Info.GenerateRequest("DELETE", Info.Server.MANAGING_RULE, Info.mainPage.AccessToken, requestDict);

                if ((HttpStatusCode)responseDict["status"] == HttpStatusCode.OK)
                {
                    MessageBox.Show("항목 삭제 완료");
                }
                else
                {
                    MessageBox.Show("항목 삭제 실패");
                }

                selectedItem = null;
                UpdatePunishmentList();
            }
        }

        private void EditPunishmentListButton_Click(object sender, EventArgs e)
        {
            if (selectedItem != null)
            {
                if (!(CheckNameValue() && CheckSliderValue()))
                    return;
                
                var requestDict = new Dictionary<string, object>
                {                    
                    {"rule_id", selectedItem.ID},
                    {"name", PunishmentName.Text},
                    {"min_point", (bool)GoodPoint.IsChecked ? MinimumPoint.SliderValue : -1 * MinimumPoint.SliderValue},
                    {"max_point", (bool)GoodPoint.IsChecked ? MaximumPoint.SliderValue : -1 * MaximumPoint.SliderValue}
                };

                var responseDict = Info.GenerateRequest("PATCH", Info.Server.MANAGING_RULE, Info.mainPage.AccessToken, requestDict);

                if ((HttpStatusCode)responseDict["status"] == HttpStatusCode.OK)
                {
                    MessageBox.Show("항목 수정 완료");
                }
                else
                {
                    MessageBox.Show("항목 수정 실패");
                }

                selectedItem = null;

                UpdatePunishmentList();
            }
        }

        private bool CheckSliderValue()
        {
            if (MinimumPoint.SliderValue <= MaximumPoint.SliderValue)
            {
                return true;
            }
            else
            {
                MessageBox.Show("최솟값은 최댓값을 초과할 수 없음");
                return false;
            }
        }

        private bool CheckNameValue()
        {
            if (PunishmentName.Text == string.Empty)
            {
                MessageBox.Show("항목의 이름은 비워둘 수 없음");
                return false;
            }
            else
            {
                return true;
            }
        }

        private void SearchList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 특정 상벌점 항목 선택 시 유형, 이름, 최소, 최대 점수 출력

            if (e.AddedItems.Count != 0)
            {
                var target = (PunishmentListViewModel) e.AddedItems[0];
                
                if (punishmentGoodList.Contains(target))
                {
                    GoodPoint.IsChecked = true;
                }

                else if (punishmentBadList.Contains(target))
                {
                    BadPoint.IsChecked = true;
                }

                PunishmentName.Text = target.Name;

                MinimumPoint.SliderValue = target.MinPoint;
                MaximumPoint.SliderValue = target.MaxPoint;

                selectedItem = target;
            }
        }

        private void SearchList_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var listView = sender as ListView;
            var gridView = listView.View as GridView;

            var workingWidth = listView.ActualWidth - 18;

            double[] columnRatio =
            {
                0.6,
                0.2,
                0.2
            };

            foreach (var element in gridView.Columns)
                element.Width = workingWidth * columnRatio[gridView.Columns.IndexOf(element)];
        }

        private void InitializePunishmentList()
        {
            var responseDict = Info.GenerateRequest("GET", Info.Server.MANAGING_RULE, Info.mainPage.AccessToken, "");

            if ((HttpStatusCode)responseDict["status"] != HttpStatusCode.OK)
            {
                MessageBox.Show("상벌점 항목 조회 실패");
                return;
            }

            rules = JArray.Parse(responseDict["body"].ToString());

            punishmentGoodList.Clear();
            punishmentBadList.Clear();

            foreach (var element in rules)
            {
                if (int.Parse(element["min_point"].ToString()) > 0) // min_point가 0보다 크면 상점
                {
                    punishmentGoodList.Add(new PunishmentListViewModel(
                        id: element["id"].ToString(),
                        name: element["name"].ToString(),
                        minPoint: int.Parse(element["min_point"].ToString()),
                        maxPoint: int.Parse(element["max_point"].ToString())));
                }

                else if (int.Parse(element["min_point"].ToString()) < 0) // min_point가 0보다 작으면 벌점
                {
                    punishmentBadList.Add(new PunishmentListViewModel(
                        id: element["id"].ToString(),
                        name: element["name"].ToString(),
                        minPoint: -1 * int.Parse(element["min_point"].ToString()),
                        maxPoint: -1 * int.Parse(element["max_point"].ToString())));
                }
            }

            GoodList.Items.Refresh();
            BadList.Items.Refresh();
        }

        private void UpdatePunishmentList()
        {
            InitializePunishmentList();
        }
    }
}