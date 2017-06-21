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

        public PunishmentListPage()
        {
            InitializeComponent();

            DataContext = this;

            var punishmentGoodList = Resources["PunishmentGoodListKey"] as ViewModel.PunishmentList;
            var punishmentBadList = Resources["PunishmentBadListKey"] as ViewModel.PunishmentList;

            ruleList = Info.multiJson(Info.Server.GET_RULE_DATA, "") as JArray;

            foreach(var element in ruleList)
            {
                if (int.Parse(element["POINT_TYPE"].ToString()) == 1)
                {
                    punishmentGoodList.Add(new PunishmentListViewModel(
                        punishmentName: element["POINT_MEMO"].ToString(),
                        minimumPoint: int.Parse(element["POINT_MIN"].ToString()),
                        maximumPoint: int.Parse(element["POINT_MAX"].ToString())));
                }
                else if(int.Parse(element["POINT_TYPE"].ToString()) == 2)
                {
                    punishmentBadList.Add(new PunishmentListViewModel(
                        punishmentName: element["POINT_MEMO"].ToString(),
                        minimumPoint: int.Parse(element["POINT_MIN"].ToString()),
                        maximumPoint: int.Parse(element["POINT_MAX"].ToString())));
                }
            }
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
            /*
             "DEST":123 //추가하는 사람의 TEACHER_UUID
            "RULE":{
                "POINT_TYPE":1 //1이면 벌점, 0이면 상점
                "POINT_MEMO":"넌 ~~~해서 상/벌점이야1",
                "POINT_MIN":4, //줄수있는 최소 점수
                "POINT_MAX":23 //줄수있는 최대 점수
            }*/

            
            //상벌점 종류
            if ((bool)GoodPoint.IsChecked)
                rule.Add("POINT_TYPE", 1);
            else
                rule.Add("POINT_TYPE", 2);

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
            if (e.Key == Key.Delete)
            {
            }
        }
    }
}
