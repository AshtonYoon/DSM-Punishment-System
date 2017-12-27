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
        private int pointType;
        private int maximumPoint;
        private int minimumPoint;
        private int pointUUID;
        private bool isChecked;

        public string PunishmentName { get => punishmentName; set => punishmentName = value; }
        public int PointType { get => pointType; set => pointType = value; }
        public int MaximumPoint { get => maximumPoint; set => maximumPoint = value; }
        public int MinimumPoint { get => minimumPoint; set => minimumPoint = value; }
        public int PointUUID { get => pointUUID; set => pointUUID = value; }
        public bool IsChecked { get => isChecked; set => isChecked = value; }

        public PunishmentListViewModel(string punishmentName, int pointType, int maximumPoint, int minimumPoint, int pointUUID, bool isChecked)
        {
            this.punishmentName = punishmentName;
            this.pointType = pointType;
            this.maximumPoint = maximumPoint;
            this.minimumPoint = minimumPoint;
            this.pointUUID = pointUUID;
            this.isChecked = isChecked;
        }
    }

    class PunishmentList : ObservableCollection<PunishmentListViewModel>
    {

    }
}