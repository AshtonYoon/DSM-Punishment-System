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
    /// TimelineBlock.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class TimelineBlock : UserControl
    {
        private readonly Color GoodTintColor = Color.FromRgb(61, 182, 61);
        private readonly Color GoodBackgroundColor = Color.FromRgb(199, 237, 199);
        
        private readonly Color BadTintColor = Color.FromRgb(255, 86, 86);
        private readonly Color BadBackgroundColor = Color.FromRgb(237, 205, 199);

        public TimelineBlock(bool isGood)
        {
            InitializeComponent();

            if (isGood)
            {
                TintCircle.Stroke = new SolidColorBrush(GoodTintColor);
                ContentBorder.Background = new SolidColorBrush(GoodBackgroundColor);
            }
            else
            {
                TintCircle.Stroke = new SolidColorBrush(BadTintColor);
                ContentBorder.Background = new SolidColorBrush(BadBackgroundColor);
            }
        }
    }
}
