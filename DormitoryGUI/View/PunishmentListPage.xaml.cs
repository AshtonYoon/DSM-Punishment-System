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
        public PunishmentListPage()
        {
            InitializeComponent();

            DataContext = this;
            var a = Resources["PunishmentListKey"] as ViewModel.PunishmentList;
            a.Add(new PunishmentListViewModel("1", 1, 0));

            BackButton.Click += (s, e) => {
                this.NavigationService.GoBack();
            };
        }

        private void AddPushimentListButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(CheckNameValue() && CheckSliderValue()))
                return;

            JObject obj = new JObject();

            //상벌점 종류
            if ((bool)GoodPoint.IsChecked)
                obj.Add("type", 1);
            else
                obj.Add("type", 2);

            //항목 이름
            obj.Add("memo", PunishmentName.Text);

            //최소, 최대 점수
            obj.Add("min", MinimunPoint.SliderValue);
            obj.Add("max", MaximumPoint.SliderValue);

            Info.multiJson(Info.Server.ADD_SCORE_INFO, obj);
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
    }
}
