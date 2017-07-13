using DormitoryGUI.ViewModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// PunishmentListPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PunishmentListPage : Page
    {
        private JArray ruleList;

        private PunishmentList punishmentGoodList;
        private PunishmentList punishmentBadList;

        public PunishmentListPage()
        {
            InitializeComponent();

            DataContext = this;

            punishmentGoodList = Resources["PunishmentGoodListKey"] as PunishmentList;
            punishmentBadList = Resources["PunishmentBadListKey"] as PunishmentList;

            ruleList = Info.multiJson(Info.Server.GET_RULE_DATA, "") as JArray;

            InitializePunishmentList();

            BackButton.Click += (s, e) => {
                this.NavigationService.GoBack();
            };
        }

        private void AddPushimentListButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(CheckNameValue() && CheckSliderValue()))
                return;

            JObject obj = new JObject();

            JObject rule = new JObject();
            
            //상벌점 종류
            if ((bool)GoodPoint.IsChecked)
                rule.Add("POINT_TYPE", 1);
            else
                rule.Add("POINT_TYPE", 0);

            //항목 이름
            rule.Add("POINT_MEMO", PunishmentName.Text);

            //최소, 최대 점수
            rule.Add("POINT_MIN", MinimunPoint.SliderValue);
            rule.Add("POINT_MAX", MaximumPoint.SliderValue);

            //TEACHER_UUID
            obj.Add("DEST", "1");
            obj.Add("RULE", rule);

            Info.multiJson(Info.Server.ADD_RULE_DATA, obj);

            MessageBox.Show("항목 추가가 완료되었습니다.");

            InitializePunishmentList();
        }

        private bool CheckSliderValue()
        {
            if(MinimunPoint.SliderValue <= MaximumPoint.SliderValue)
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

        private void SearchList_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var listView = sender as ListView;
            var gridView = listView.View as GridView;

            var workingWidth = listView.ActualWidth - 18;

            double[] columnRatio =
            {
                0.1,
                0.6,
                0.15,
                0.15
            };

            foreach (var element in gridView.Columns)
                element.Width = workingWidth * columnRatio[gridView.Columns.IndexOf(element)];
        }

        private void SearchList_KeyUp(object sender, KeyEventArgs e)
        {
            var target = sender as ListView;
            if (e.Key == Key.Delete)
            {
                if (target.Name.Contains("Good"))
                    DeleteSelectedItem(target, punishmentGoodList);
                else if(target.Name.Contains("Bad"))
                    DeleteSelectedItem(target, punishmentBadList);
            }
        }

        private void DeleteSelectedItem(ListView listView, PunishmentList collection)
        {
            listView.ItemsSource = collection.Where(x => !x.IsChecked);
            listView.Items.Refresh();
        }

        private void InitializePunishmentList()
        {
            punishmentGoodList.Clear();
            punishmentBadList.Clear();

            foreach (var element in ruleList)
            {
                if (int.Parse(element["POINT_TYPE"].ToString()) == 1)
                {
                    punishmentGoodList.Add(new PunishmentListViewModel(
                        punishmentName: element["POINT_MEMO"].ToString(),
                        minimumPoint: int.Parse(element["POINT_MIN"].ToString()),
                        maximumPoint: int.Parse(element["POINT_MAX"].ToString()),
                        pointUUID: int.Parse(element["POINT_UUID"].ToString()),
                        isChecked: false));
                }
                else if (int.Parse(element["POINT_TYPE"].ToString()) == 0)
                {
                    punishmentBadList.Add(new PunishmentListViewModel(
                        punishmentName: element["POINT_MEMO"].ToString(),
                        minimumPoint: int.Parse(element["POINT_MIN"].ToString()),
                        maximumPoint: int.Parse(element["POINT_MAX"].ToString()),
                        pointUUID: int.Parse(element["POINT_UUID"].ToString()),
                        isChecked: false));
                }
            }

            GoodList.Items.Refresh();
            BadList.Items.Refresh();
        }
    }
}
