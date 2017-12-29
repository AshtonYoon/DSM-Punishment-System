using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using DormitoryGUI.ViewModel;
using Newtonsoft.Json.Linq;

namespace DormitoryGUI.View
{    
    /// <summary>
    /// CheckPunishmentTarget.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CheckPunishmentTargetPage : Page
    {
        private StudentList listviewCollection;

        private JArray studentList;

        private readonly MainWindow mainWindow;

        public CheckPunishmentTargetPage(MainWindow mainWindow)
        {
            InitializeComponent();

            BackButton.Click += (s, e) => {
                this.NavigationService.GoBack();
            };
            listviewCollection = Resources["StudentListKey"] as ViewModel.StudentList;


            this.mainWindow = mainWindow;

            Update();
        }

        public void Update()
        {
            var responseDict = Info.GenerateRequest("GET", Info.Server.MANAGING_STUDENT, Info.mainPage.AccessToken, "");

            if ((HttpStatusCode)responseDict["status"] != HttpStatusCode.OK)
            {
                return;
            }

            studentList = JArray.Parse(responseDict["body"].ToString());

            foreach (JObject student in studentList)
            {
                listviewCollection.Add(new ViewModel.StudentListViewModel(
                    id: student["id"].ToString(),
                    classNumber: student["number"].ToString(),
                    name: student["name"].ToString(),
                    goodPoint: student["good_point"].Type == JTokenType.Null
                        ? 0
                        : int.Parse(student["good_point"].ToString()),
                    badPoint: student["bad_point"].Type == JTokenType.Null
                        ? 0
                        : int.Parse(student["bad_point"].ToString()),
                    currentStep: student["penalty_training_status"].Type == JTokenType.Null
                        ? 0
                        : int.Parse(student["penalty_training_status"].ToString()),
                    isChecked: false
                ));
            }
                    
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
                0.4,
                0.3
            };

            foreach (var element in gridView.Columns)
                element.Width = workingWidth * columnRatio[gridView.Columns.IndexOf(element)];
        }
    }
}
