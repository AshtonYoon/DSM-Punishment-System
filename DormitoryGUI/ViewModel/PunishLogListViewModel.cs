using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryGUI.ViewModel
{
    class PunishLogListViewModel
    {
        private int point;
        private string reason;
        private string time;
        private bool pointType;

        public int Point { get => point; set => point = value; }
        public string Reason { get => reason; set => reason = value; }
        public string Time { get => time; set => time = value; }
        public bool PointType { get => pointType; set => pointType = value; }

        public PunishLogListViewModel(int score, string reason, string time, bool pointType)
        {
            this.point = score;
            this.reason = reason;
            this.time = time;
            this.pointType = pointType;
        }
    }

    class PunishLogList : ObservableCollection<PunishLogListViewModel>
    {
    }
}
