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
        private string id;
        private string name;
        private int maxPoint;
        private int minPoint;

        public string ID { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }        
        public int MaxPoint { get => maxPoint; set => maxPoint = value; }
        public int MinPoint { get => minPoint; set => minPoint = value; }

        public PunishmentListViewModel(string id, string name, int minPoint, int maxPoint)
        {
            this.id = id;
            this.name = name;
            this.minPoint = minPoint;
            this.maxPoint = maxPoint;
        }
    }

    class PunishmentList : ObservableCollection<PunishmentListViewModel>
    {

    }
}