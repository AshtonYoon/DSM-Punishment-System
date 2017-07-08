using DormitoryGUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryGUI.Model
{
    class DistinctStudentComparer : IEqualityComparer<StudentListViewModel>
    {
        public bool Equals(StudentListViewModel x, StudentListViewModel y)
        {
            return x.ClassNumber.Equals(y.ClassNumber);
        }

        public int GetHashCode(StudentListViewModel obj)
        {
            return obj.GetHashCode();
        }
    }
}
