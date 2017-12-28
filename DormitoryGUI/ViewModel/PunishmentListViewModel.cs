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
        private int punishId;       

        public string PunishmentName { get => punishmentName; set => punishmentName = value; }        
        public int MaximumPoint { get => maximumPoint; set => maximumPoint = value; }
        public int MinimumPoint { get => minimumPoint; set => minimumPoint = value; }
        public int PunishId { get => punishId; set => punishId = value; }        

        public PunishmentListViewModel(string punishmentName, int maximumPoint, int minimumPoint, int punishId)
        {
            this.punishmentName = punishmentName;            
            this.maximumPoint = maximumPoint;
            this.minimumPoint = minimumPoint;
            this.punishId = punishId;            
        }
    }

    class PunishmentList : ObservableCollection<PunishmentListViewModel>
    {

    }
}