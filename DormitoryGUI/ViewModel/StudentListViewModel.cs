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
        private string currentStep;
        private bool isSelected;

        public string ID { get => id; set => id = value; }
        public string ClassNumber { get => classNumber; set => classNumber = value; }
        public string Name { get => name; set => name = value; }
        public int GoodPoint { get => goodPoint; set => goodPoint = value; }
        public int BadPoint { get => badPoint; set => badPoint = value; }
        public string CurrentStep { get => currentStep; set => currentStep = value; }
        public bool IsSelected { get => isSelected; set => isSelected = value; }

        public StudentListViewModel(string id, string classNumber, string name, int goodPoint, int badPoint, string currentStep, bool isSelected)
        {
            this.id = id;
            this.name = name;
            this.classNumber = classNumber;
            this.goodPoint = goodPoint;
            this.badPoint = badPoint;
            this.currentStep = currentStep;
            this.isSelected = isSelected;
        }
    }
}
