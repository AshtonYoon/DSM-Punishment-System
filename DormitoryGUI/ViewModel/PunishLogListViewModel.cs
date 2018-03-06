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
        private int score;
        private string reason;
        private string time;

        public int Score { get => score; set => score = value; }
        public string Reason { get => reason; set => reason = value; }
        public string Time { get => time; set => time = value; }

        public PunishLogListViewModel(int score, string reason, string time)
        {
            this.score = score;
            this.reason = reason;
            this.time = time;
        }
    }

    class PunishLogList : ObservableCollection<PunishLogListViewModel>
    {
    }
}
