using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryGUI.ViewModel
{
    class RuleList : ObservableCollection<RuleList>
    {

    }

    class RuleViewModel
    {
        /*
         "POINT_UUID":1,
		"POINT_TYPE":1, //type가 1이면 벌점
		"POINT_MEMO":"넌 벌점이야1",
		"POINT_MIN":1,
		"POINT_MAX":6
             */
        private int pointUUID;
        private string ruleName;
        private int pointType;
        private int minPoint;
        private int maxPoint;

        public int PointUUID { get => pointUUID; set => pointUUID = value; }
        public string RuleName { get => ruleName; set => ruleName = value; }
        public int PointType { get => pointType; set => pointType = value; }
        public int MinPoint { get => minPoint; set => minPoint = value; }
        public int MaxPoint { get => maxPoint; set => maxPoint = value; }
    }
}
