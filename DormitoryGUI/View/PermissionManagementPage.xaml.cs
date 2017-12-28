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
    /// PermissionManagementPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PermissionManagementPage : Page
    {
        private readonly MainWindow mainWindow;

        public PermissionManagementPage(MainWindow mainWindow)
        {
            InitializeComponent();

            /*
            InitializePermission();

            BackButton.Click += (s, e) => {
                if (MessageBox.Show("설정을 저장하시겠습니까?", "알림", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    NavigationService.GoBack();
                }
                else
                {
                    SavePermission();
                    NavigationService.GoBack();
                }
            };
            */

            this.mainWindow = mainWindow;
        }

        /* private void InitializePermission()
        {
            JArray array = (JArray)Info.MultiJson(Info.Server.GET_PERMISSION_URL, "");
            foreach (JObject obj in array)
            {
                switch (Int32.Parse(obj["PERMISSION_TYPE"].ToString()))
                {
                    case 0:
                        ManagerPoint.IsChecked = obj["SCORE_MANAGE"].ToString().Contains("1");
                        ManagerStudent.IsChecked = obj["STUDENT_MANAGE"].ToString().Contains("1");
                        break;
                    case 1:
                        CoachPoint.IsChecked = obj["SCORE_MANAGE"].ToString().Contains("1");
                        CoachStudent.IsChecked = obj["STUDENT_MANAGE"].ToString().Contains("1");
                        break;
                    case 2:
                        TeacherPoint.IsChecked = obj["SCORE_MANAGE"].ToString().Contains("1");
                        TeacherStudent.IsChecked = obj["STUDENT_MANAGE"].ToString().Contains("1");
                        break;
                }
            }
        }

        private void SavePermission()
        {
             json = {
                    DEST = "UUID",
                    DATA = [
                        {
                            "PERMISSION_TYPE":1,
                            "SCORE_MANAGE":1,
                            "STUDENT_MANAGE":1
                        },
                        {
                            "PERMISSION_TYPE":2,
                            "SCORE_MANAGE":0,
                            "STUDENT_MANAGE":1
                        }
                    ]
                }

            JObject root = new JObject();

            JArray post = new JArray();
            JArray inner = new JArray
            {
                (bool)ManagerPoint.IsChecked ? 1 : 0,
                (bool)ManagerStudent.IsChecked ? 1 : 0,
                0
            };
            post.Add(inner);

            inner = new JArray
            {
                (bool)CoachPoint.IsChecked ? 1 : 0,
                (bool)CoachStudent.IsChecked ? 1 : 0,
                1
            };
            post.Add(inner);

            inner = new JArray
            {
                (bool)TeacherPoint.IsChecked ? 1 : 0,
                (bool)TeacherStudent.IsChecked ? 1 : 0,
                2
            };
            post.Add(inner);

            root.Add("DEST", Info.mainPage.TeacherUUID);
            root.Add("DATA", post);

            Info.MultiJson(Info.Server.SET_PERMSSION_URL, root);
        } */
    }
}
