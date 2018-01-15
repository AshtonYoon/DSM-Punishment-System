using Newtonsoft.Json.Linq;
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

namespace DormitoryGUI.View
{
    /// <summary>
    /// PunishmentLogDialog.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PunishmentLogDialog : Window
    {
        private string studentID;

        public PunishmentLogDialog(string id, string name, string classNumber, int goodPoint, int badPoint, string currentStep)
        {
            InitializeComponent();

            StudentName.Content = name;
            ClassNumber.Content = classNumber;

            TotalGoodPoint.Content = goodPoint.ToString();
            TotalBadPoint.Content = badPoint.ToString();
            TotalPunishStep.Content = currentStep;

            studentID = id;
            SetLogData(id);
        }

        private void SetLogData(string id)
        {
            var responseDict = Info.GenerateRequest("GET", $"{Info.Server.MANAGING_POINT}?id={id}", Info.mainPage.AccessToken, "");

            if ((HttpStatusCode)responseDict["status"] != HttpStatusCode.OK)
            {
                MessageBox.Show("상벌점 내역 조회 실패");
                return;
            }

            JArray logs = JArray.Parse(responseDict["body"].ToString());

            Timeline.Children.Clear();

            for (int i = logs.Count - 1; i >= 0; i--)
            {
                JObject log = (JObject)logs[i];

                Timeline.Children.Add
                (
                    new TimelineBlock
                    (
                        isGood: (int) log["point"] > 0,
                        createTime: DateTime.Parse(log["time"].ToString()).ToLongDateString() + " " + DateTime.Parse(log["time"].ToString()).ToLongTimeString(),
                        pointValue: Math.Abs((int) log["point"]).ToString(),
                        pointCause: log["reason"].ToString()
                    )
                );
            }
        }
    }
}
