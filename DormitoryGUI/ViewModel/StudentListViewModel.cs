using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryGUI.ViewModel
{
    class StudentList : ObservableCollection<StudentListViewModel>
    {

    }

    class StudentListViewModel
    {
        private string id;
        private string classNumber;
        private string name;
        private int goodPoint;
        private int badPoint;
        private bool penaltyTrainingStatus;
        private string penaltyLevel;
        private bool isSelected;

        private PunishLogList punishLogs;
        
        public string ID { get => id; set => id = value; }
        public string ClassNumber { get => classNumber; set => classNumber = value; }
        public string Name { get => name; set => name = value; }
        public int GoodPoint { get => goodPoint; set => goodPoint = value; }
        public int BadPoint { get => badPoint; set => badPoint = value; }
        public bool PenaltyTrainingStatus { get => penaltyTrainingStatus; set => penaltyTrainingStatus = value; }
        public bool IsSelected { get => isSelected; set => isSelected = value; }

        public PunishLogList PunishLogs { get => punishLogs; set => punishLogs = value; }
        public string PenaltyLevel { get => penaltyLevel; set => penaltyLevel = value; }

        public StudentListViewModel(string id, string classNumber, string name, int goodPoint, int badPoint, bool penaltyTrainingStaus, bool isSelected,string penaltyLevel)
        {
            this.id = id;
            this.name = name;
            this.classNumber = classNumber;
            this.goodPoint = goodPoint;
            this.badPoint = badPoint;
            this.penaltyTrainingStatus = penaltyTrainingStaus;
            this.penaltyLevel = penaltyLevel;
            this.isSelected = isSelected;
            this.punishLogs = new PunishLogList();
        }
    }
}
