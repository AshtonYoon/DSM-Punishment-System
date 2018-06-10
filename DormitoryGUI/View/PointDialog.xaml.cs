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
    public partial class PointDialog : Window
    {
        private string punishmentID;
        public string PunishmentID { get => punishmentID; set => punishmentID = value; }

        private int punishmentScore;
        public int PunishmentScore { get => punishmentScore; set => punishmentScore = value; }

        public PointDialog(int type)
        {

            InitializeComponent();

            PunishmentComboBox.PunishmentType = type;
            PunishmentComboBox.comboBox.SelectionChanged += PunishmentComboBox_SelectionChanged;

            PointType.Content = type == (int)Info.POINT_TYPE.GOOD ? "상점" : "벌점";
        }

        private void PunishmentComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var target = (PunishmentListViewModel) PunishmentComboBox.SelectedItem;
            ScoreText.Text = target.MinPoint.ToString();
        }

        private void Click_OK(object sender, RoutedEventArgs e)
        {
            var target = PunishmentComboBox.SelectedItem as PunishmentListViewModel;

            if (target == null)
            {
                MessageBox.Show("항목이 선택되지 않음");
                DialogResult = false;

                return;
            }

            punishmentID = target.ID;
            punishmentScore = int.Parse(ScoreText.Text);

            DialogResult = true;
            Close();
        }

        private void Click_Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
