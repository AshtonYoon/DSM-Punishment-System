using DormitoryGUI.ViewModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
    /// CustomComboBox.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CustomComboBox : UserControl
    {
        private JArray rules;

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
            PunishmentList punishmentList = Resources["PunishmentListKey"] as PunishmentList;

            var responseDict = Info.GenerateRequest("GET", Info.Server.MANAGING_RULE, Info.mainPage.AccessToken, "");

            if ((HttpStatusCode)responseDict["status"] != HttpStatusCode.OK)
            {
                return;
            }

            rules = JArray.Parse(responseDict["body"].ToString());

            punishmentList.Clear();

            foreach (JObject rule in rules)
            {
                if (PunishmentType == 0)
                {
                    if (int.Parse(rule["min_point"].ToString()) > 0)
                    {
                        punishmentList.Add(new PunishmentListViewModel(
                        id: rule["id"].ToString(),
                        name: rule["name"].ToString(),
                        minPoint: int.Parse(rule["min_point"].ToString()),
                        maxPoint: int.Parse(rule["max_point"].ToString())));
                    }
                }

                else if (PunishmentType == 1)
                {
                    if (int.Parse(rule["min_point"].ToString()) < 0)
                    {
                        punishmentList.Add(new PunishmentListViewModel(
                        id: rule["id"].ToString(),
                        name: rule["name"].ToString(),
                        minPoint: int.Parse(rule["min_point"].ToString()),
                        maxPoint: int.Parse(rule["max_point"].ToString())));
                    }
                }
            }
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
