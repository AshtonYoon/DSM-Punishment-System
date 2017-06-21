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
    /// CustomSlider.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CustomSlider : UserControl
    {
        public int SliderValue
        {
            get
            {
                return (int)slider.Value;
            }
            set
            {
                slider.Value = value;
            }
        }

        public CustomSlider()
        {
            InitializeComponent();
        }
    }
}
