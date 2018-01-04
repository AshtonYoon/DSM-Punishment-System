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
using DormitoryGUI.ViewModel;

namespace DormitoryGUI.View
{
    /// <summary>
    /// CustomDialog.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CustomDialog : Window
    {
        public CustomDialog(int type)
        {
            InitializeComponent();
            PunishmentComboBox.PunishmentType = type;
        }

        private void Click_OK(object sender, RoutedEventArgs e)
        {
            PunishmentComboBox.PunishmentType = (int)Info.POINT_TYPE.GOOD;
            var target = ((PunishmentListViewModel)PunishmentComboBox.SelectedItem);
        }

        private void Click_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
