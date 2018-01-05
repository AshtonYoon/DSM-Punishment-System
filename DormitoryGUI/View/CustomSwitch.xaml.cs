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
    /// CustomSwitch.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CustomSwitch : UserControl
    {
        private int pointType = (int)Info.POINT_TYPE.GOOD;
        public int PointType
        {
            get => pointType;
            set
            {
                pointType=value;
                Update();
            }
        }

        public CustomSwitch()
        {
            InitializeComponent();
            Update();
        }

        private void Update()
        {
            SwitchToggle.Content = !Convert.ToBoolean(pointType) ? "상점" : "벌점";
            Grid.SetColumn(SwitchToggle, pointType);
        }

        private void SwitchToggle_Click(object sender, EventArgs e)
        {
            PointType = (Convert.ToInt32(!Convert.ToBoolean(pointType)));
            Update();
        }
    }
}
