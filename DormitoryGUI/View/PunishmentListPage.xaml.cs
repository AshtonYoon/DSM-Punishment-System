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
        private JArray ruleList;

        private PunishmentList punishmentGoodList;
        private PunishmentList punishmentBadList;

        private readonly MainWindow mainWindow;

        private HttpWebResponse webResponse;

        public PunishmentListPage(MainWindow mainWindow)
        {
            InitializeComponent();

            DataContext = this;

            punishmentGoodList = Resources["PunishmentGoodListKey"] as PunishmentList;
            punishmentBadList = Resources["PunishmentBadListKey"] as PunishmentList;

            webResponse = Info.GenerateRequest("GET", Info.Server.MANAGING_RULE, Info.mainPage.AccessToken, " ");

            InitializePunishmentList();

            BackButton.Click += (s, e) => { NavigationService.GoBack(); };

            this.mainWindow = mainWindow;
        }

        private void AddPushimentListButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(CheckNameValue() && CheckSliderValue()))
                return;

            JObject rule = new JObject
            {
                {"name", PunishmentName.Text},
                {"min_point", (bool) GoodPoint.IsChecked ? MinimumPoint.SliderValue : 0 - MinimumPoint.SliderValue},
                {"max_point", (bool) GoodPoint.IsChecked ? MaximumPoint.SliderValue : 0 - MaximumPoint.SliderValue}
            };

//            Info.MultiJson(Info.Server.ADD_RULE_DATA, rule);
            webResponse = Info.GenerateRequest("POST", Info.Server.MANAGING_RULE, Info.mainPage.AccessToken, " ");

            if (webResponse.StatusCode == HttpStatusCode.OK)
                MessageBox.Show("항목 추가가 완료되었습니다.");

            UpdatePunishmentList();
        }

        private void DelPunishmentListButton_Click(object sender, EventArgs e)
        {
            if (selectedItem != null)
            {
                JObject jobj = new JObject
                {
                    {"rule_id", selectedItem.PunishId}
                };
                Info.GenerateRequest("DELETE", Info.Server.MANAGING_RULE, Info.mainPage.AccessToken, jobj);
//                Info.MultiJson(Info.Server.DELETE_RULE_DATA, jobj);

                MessageBox.Show("항목 삭제 완료");

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

                JObject jobj = new JObject
                {                    
                    {"runle_id", selectedItem.PunishId},
                    {"name", PunishmentName.Text},
                    {"min_point", MinimumPoint.SliderValue},
                    {"max_point", MaximumPoint.SliderValue}
                };

                webResponse = Info.GenerateRequest("PATCH", Info.Server.MANAGING_RULE, Info.mainPage.AccessToken, " ");
                if (webResponse.StatusCode == HttpStatusCode.OK)
                    MessageBox.Show("항목 수정 완료");

                selectedItem = null;

                UpdatePunishmentList();
            }
        }

        private bool CheckSliderValue()
        {
            if ((bool) GoodPoint.IsChecked && MinimumPoint.SliderValue <= MaximumPoint.SliderValue ||
                !(bool) GoodPoint.IsChecked && MinimumPoint.SliderValue >= MaximumPoint.SliderValue)
            {
                return true;
            }
            else
            {
                MessageBox.Show("최소 벌점은 최대 벌점값을 넘을 수 없습니다.");
                return false;
            }
        }

        private bool CheckNameValue()
        {
            if (PunishmentName.Text == string.Empty)
            {
                MessageBox.Show("항목의 이름은 비워둘 수 없습니다.");
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

                if (target.MaximumPoint >= 0)
                {
                    GoodPoint.IsChecked = true;
                    MinimumPoint.SliderValue = target.MinimumPoint;
                    MaximumPoint.SliderValue = target.MaximumPoint;
                }
                else if (target.MaximumPoint <= 0)
                {
                    BadPoint.IsChecked = true;
                    MinimumPoint.SliderValue = Math.Abs(target.MinimumPoint);
                    MaximumPoint.SliderValue = Math.Abs(target.MaximumPoint);
                }

                PunishmentName.Text = target.PunishmentName;

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
            punishmentGoodList.Clear();
            punishmentBadList.Clear();

            foreach (var element in ruleList)
            {
                if (int.Parse(element["min_point"].ToString()) >= 0) // min_point가 0보다 크면 상점
                {
                    punishmentGoodList.Add(new PunishmentListViewModel(
                        name: element["name"].ToString(),
                        minPoint: int.Parse(element["minPoint"].ToString()),
                        maxPoint: int.Parse(element["maxPoint"].ToString()),
                        id: int.Parse(element["id"].ToString())));
                }

                else if (int.Parse(element["min_point"].ToString()) <= 0) // min_point가 0보다 작으면 벌점
                {
                    punishmentBadList.Add(new PunishmentListViewModel(
                        name: element["name"].ToString(),
                        minPoint: Math.Abs(int.Parse(element["minPoint"].ToString())),
                        maxPoint: Math.Abs(int.Parse(element["maxPoint"].ToString())),
                        id: int.Parse(element["Id"].ToString())));
                }
            }

            GoodList.Items.Refresh();
            BadList.Items.Refresh();
        }

        private void UpdatePunishmentList()
        {
            //            ruleList = Info.MultiJson(Info.Server.GET_RULE_DATA, "") as JArray;
            webResponse = Info.GenerateRequest("GET", Info.Server.MANAGING_RULE, Info.mainPage.AccessToken, " ");
            InitializePunishmentList();
        }
    }
}