using System;
using System.Collections.Generic;
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
        public List<CourseModel> courses = new List<CourseModel>();

        public TeacherModel()
        {

        }

        public TeacherModel(String n, String un, String pw, String p)
        {
            Name = n;
            userName = un;
            Password = pw;
            Position = p;
        }

    }
}
