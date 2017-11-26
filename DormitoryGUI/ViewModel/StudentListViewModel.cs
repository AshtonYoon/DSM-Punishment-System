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
        private string roomNumber;
        private string name;
        private string classNumber;
        private int goodPoint;
        private int badPoint;
        private int currentStep;
        private bool isChecked;
        private int userUUID;

        public string ClassNumber { get => classNumber; set => classNumber = value; }
        public string Name { get => name; set => name = value; }
        public string RoomNumber { get => roomNumber; set => roomNumber = value; }
        public int GoodPoint { get => goodPoint; set => goodPoint = value; }
        public int BadPoint { get => badPoint; set => badPoint = value; }
        public int CurrentStep { get => currentStep; set => currentStep = value; }
        public bool IsChecked { get => isChecked; set => isChecked = value; }
        public int UserUUID { get => userUUID; set => userUUID = value; }

        public StudentListViewModel(bool isChecked, string roomNumber, string name, string classNumber, int goodPoint, int badPoint, int currentStep, int userUUID)
        {
            this.roomNumber = roomNumber;
            this.name = name;
            this.classNumber = classNumber;
            this.goodPoint = goodPoint;
            this.badPoint = badPoint;
            this.currentStep = currentStep;
            this.isChecked = isChecked;
            this.userUUID = userUUID;
        }
    }
}
