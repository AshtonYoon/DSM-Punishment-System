using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace DormitoryGUI.ViewModel
{
    class PunishmentListViewModel
    {
        private string punishmentName;
        private int maximumPoint;
        private int minimumPoint;

        public string PunishmentName { get => punishmentName; set => punishmentName = value; }
        public int MaximumPoint { get => maximumPoint; set => maximumPoint = value; }
        public int MinimumPoint { get => minimumPoint; set => minimumPoint = value; }

        public PunishmentListViewModel(string punishmentName, int maximumPoint, int minimumPoint)
        {
            this.punishmentName = punishmentName;
            this.maximumPoint = maximumPoint;
            this.minimumPoint = minimumPoint;
        }
    }

    class PunishmentList : ObservableCollection<PunishmentListViewModel>
    {

    }
}