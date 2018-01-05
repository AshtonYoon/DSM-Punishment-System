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

                PointTypeSwitch.PointType = pointType;
            }
        }

        private void SearchList_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var listView = sender as ListView;
            var gridView = listView.View as GridView;

            var workingWidth = listView.ActualWidth - 18;

            double[] columnRatio =
            {
                0.5,
                0.5
            };

            foreach (var element in gridView.Columns)
                element.Width = workingWidth * columnRatio[gridView.Columns.IndexOf(element)];
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            int point = int.Parse(PointScore.Text) * PointTypeSwitch.PointType == 0 ? 1 : -1;

            var requestDict = new Dictionary<string, object>
            {
                { "name", PointName.Text },
                { "min_point", point },
                { "max_point", point }
            };

            var responseDict = Info.GenerateRequest("GET", Info.Server.MANAGING_RULE, Info.mainPage.AccessToken, requestDict);
        }

        private void Click_OK(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Click_Cancel(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
