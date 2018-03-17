using DormitoryGUI.ViewModel;
using Newtonsoft.Json.Linq;
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

namespace DormitoryGUI.View
{
    /// <summary>
    /// PointManageDialog.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PointManageDialog : Window
    {
        private PunishmentList listviewCollection;
        private PunishmentListViewModel selectedItem;

        private int pointType = (int)Info.POINT_TYPE.GOOD;
        public int PointType
        {
            get => pointType;
            set {
                pointType = value;
            }
        }

        public PointManageDialog()
        {
            InitializeComponent();
            listviewCollection = Resources["PunishmentListKey"] as PunishmentList;

            Update();
        }


        private void Update()
        {
            listviewCollection.Clear();

            var responseDict = Info.GenerateRequest("GET", Info.Server.MANAGING_RULE, Info.mainPage.AccessToken, "");
            
            if ((HttpStatusCode) responseDict["status"] != HttpStatusCode.OK)
            {
                MessageBox.Show("상벌점 항목 조회 실패");
                return;
            }

            var rules = JArray.Parse(responseDict["body"].ToString());
            
            foreach (JObject rule in rules)
            {
                int minPoint = rule["min_point"].Type == JTokenType.Null ? 0 : (int) rule["min_point"];
                int maxPoint = rule["max_point"].Type == JTokenType.Null ? 0 : (int) rule["max_point"];

                if ((pointType == 0 && minPoint < 0) || (pointType == 1 && minPoint > 0))
                {
                    continue;
                }

                listviewCollection.Add(new PunishmentListViewModel(
                    id: (string) rule["id"],
                    name: (string) rule["name"],
                    minPoint: Math.Abs(minPoint),
                    maxPoint: Math.Abs(maxPoint)));
            }

            PointList.ItemsSource = listviewCollection;           
            PointList.Items.Refresh();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem target = (sender as ComboBox).SelectedItem as ComboBoxItem;
            pointType = target.Content.ToString() == "상점" ? (int)Info.POINT_TYPE.GOOD : (int)Info.POINT_TYPE.BAD;

            if (listviewCollection != null)
            {
                Update();
            }
        }

        private void SearchList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                var target = (e.AddedItems[0] as PunishmentListViewModel);

                PointName.Text = target.Name;
                PointScore.Text = target.MinPoint.ToString();
//                PointTypeSwitch.PointType = pointType;
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
                0.75,
                0.25
            };

            foreach (var element in gridView.Columns)
            {
                element.Width = workingWidth * columnRatio[gridView.Columns.IndexOf(element)];
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            int point = int.Parse(PointScore.Text) * (GoodPunishCheck.IsChecked == true ? 1 : -1);

            var requestDict = new Dictionary<string, object>
            {
                { "name", PointName.Text },
                { "min_point", point },
                { "max_point", point },
                {"point_type", GoodPunishCheck.IsChecked == true ? true : false}
            };

            var responseDict = Info.GenerateRequest("POST", Info.Server.MANAGING_RULE, Info.mainPage.AccessToken, requestDict);

            MessageBox.Show((HttpStatusCode)responseDict["status"] == HttpStatusCode.Created ? "항목 추가 성공" : "항목 추가 실패");

            Update();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedItem == null)
            {
                MessageBox.Show("항목이 선택되지 않음");
                return;
            }

            int point = int.Parse(PointScore.Text) * (GoodPunishCheck.IsChecked == true ? 1 : -1);

            var requestDict = new Dictionary<string, object>
            {
                { "rule_id", selectedItem.ID },
                { "name", PointName.Text },
                { "min_point", point },
                { "max_point", point },
                { "point_type", GoodPunishCheck.IsChecked == true ? true : false }
            };

            var responseDict = Info.GenerateRequest("PATCH", Info.Server.MANAGING_RULE, Info.mainPage.AccessToken, requestDict);

            MessageBox.Show((HttpStatusCode)responseDict["status"] == HttpStatusCode.OK ? "항목 수정 성공" : "항목 수정 실패");

            Update();
        }

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedItem == null)
            {
                MessageBox.Show("항목이 선택되지 않음");
                return;
            }

            var requestDict = new Dictionary<string, object>
            {
                { "rule_id", selectedItem.ID }
            };

            var responseDict = Info.GenerateRequest("DELETE", Info.Server.MANAGING_RULE, Info.mainPage.AccessToken, requestDict);

            MessageBox.Show((HttpStatusCode)responseDict["status"] == HttpStatusCode.OK ? "항목 삭제 성공" : "항목 삭제 실패");

            Update();
        }
    }
}
