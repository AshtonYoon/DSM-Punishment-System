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
        private readonly Color GoodTintColor = Color.FromRgb(131, 169, 255);
        private readonly Color GoodBackgroundColor = Color.FromRgb(131, 169, 255);

        private readonly Color BadTintColor = Color.FromRgb(255, 127, 127);
        private readonly Color BadBackgroundColor = Color.FromRgb(255, 127, 127);

        public TimelineBlock(bool isGood, string createTime, string pointValue, string pointCause)
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

            CreateTime.Content = createTime;

            PointValue.Content = string.Format("{0}점", pointValue);
//            PointCause.FontSize = 30 * 9 / pointCause.Length * 1.3;

            PointCause.FontSize = pointCause.Length <= 6 ? 40 : 30 * 9 / pointCause.Length;
            PointCause.Content = pointCause;
        }
    }
}