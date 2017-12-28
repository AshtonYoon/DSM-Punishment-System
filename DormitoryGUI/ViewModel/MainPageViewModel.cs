using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitoryGUI.ViewModel
{
    class MainPageViewModel
    {
        #region Fields  
        private bool canEditStudent, canEditScore;
        private string name;
        private int teacherUUID;
        private string accessToken;
        private string refreshToken;
        #endregion

        #region Properties
        public int TeacherUUID { get => teacherUUID; set => teacherUUID = value; }

        public string AccessToken { get => accessToken; set => accessToken = value; }

        public string RefreshToken { get => refreshToken; set => refreshToken = value; }

        public string Name { get => name; set => name = value; }

        public KeyValuePair<bool, bool> PermissionData
        {
            get => new KeyValuePair<bool, bool>(canEditStudent, canEditScore);
            set
            {
                canEditStudent = value.Key;
                canEditScore = value.Value;
            }
        }
        #endregion
    }
}
