using DormitoryGUI.ViewModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
    /// CustomComboBox.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CustomComboBox : UserControl
    {
        private JArray ruleList;

        public CustomComboBox()
        {
            InitializeComponent();

            DataContext = this;

            InitializePunishmentList();
        }

        public object SelectedItem
        {
            get => comboBox.SelectedItem;
        }

        private int punishmentType = 0;
        public int PunishmentType
        {
            get => punishmentType;
            set
            {
                punishmentType = value;
                InitializePunishmentList();
            }
        }

        private void InitializePunishmentList()
        {
            var punishmentList = Resources["PunishmentListKey"] as ViewModel.PunishmentList;

            ruleList = Info.MultiJson(Info.Server.GET_RULE_DATA, "") as JArray;

            punishmentList.Clear();
            foreach (var element in ruleList)
            {
                if (int.Parse(element["POINT_TYPE"].ToString()) == PunishmentType)
                {
                    punishmentList.Add(new PunishmentListViewModel(
                        punishmentName: element["POINT_MEMO"].ToString(),
                        minimumPoint: int.Parse(element["POINT_MIN"].ToString()),
                        maximumPoint: int.Parse(element["POINT_MAX"].ToString()),
                        pointUUID: int.Parse(element["POINT_UUID"].ToString()),
                        isChecked: false));
                }
            }
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
