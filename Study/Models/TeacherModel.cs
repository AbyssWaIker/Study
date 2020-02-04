using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study
{
    public class TeacherModel
    {
        public int id { get; set; }
        public String Name { get; set; }
        public String userName { get; set; }
        public String Password { get; set; }
        public String Position { get; set; }
        public ObservableCollection<CourseModel> courses { get; set; }  = new ObservableCollection<CourseModel>();

        public TeacherModel()
        {

        }

        public TeacherModel(String n, String p, String un, String pw)
        {
            Name = n;
            Position = p;
            userName = un;
            Password = pw;
        }

    }
}
