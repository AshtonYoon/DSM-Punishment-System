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
    /// SideMenuButton.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SideMenuButton : UserControl
    {
        public event RoutedEventHandler Click;

        public Brush ButtonBackground
        {
            get
            {
                return button.Background;
            }
            set
            {
                button.Background = value;
            }
        }

        public ImageSource ImageSource
        {
            get
            {
                return (Resources["image"] as Image).Source;
            }
            set
            {
                (Resources["image"] as Image).Source = value;
            }
        }

        public SideMenuButton()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Click?.Invoke(this, e);
        }
    }
}
