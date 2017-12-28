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
using System.Windows.Shapes;

namespace DormitoryGUI.View
{
    /// <summary>
    /// CheckPunishmentTarget.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CheckPunishmentTarget : Page
    {
        public CheckPunishmentTarget()
        {
            InitializeComponent();

            BackButton.Click += (s, e) => {
                this.NavigationService.GoBack();
            };

        }

        private void StudentList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void StudentList_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            var listView = sender as ListView;
            var gridView = listView.View as GridView;

            var workingWidth = listView.ActualWidth - 18;

            double[] columnRatio =
            {
                0.3,
                0.35,
                0.4
            };

            foreach (var element in gridView.Columns)
                element.Width = workingWidth * columnRatio[gridView.Columns.IndexOf(element)];
        }
    }
}
