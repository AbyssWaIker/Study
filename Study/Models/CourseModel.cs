using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Study.Models;

namespace Study
{
    public class CourseModel
    {
        public int id { get; set; }
        public int Teacherid { get; set; }
        public String Name { get; set; }
        public List<TopicModel> topics { get; set; } = new List<TopicModel>();
        public List<StudentModel> students { get; set; } = new List<StudentModel>();
        public List<GroupToCourseRealationModel> studentToCourseRealations { get; set; } = new List<GroupToCourseRealationModel>();
        public CourseModel()
        {
        }
        public CourseModel(int teacherid)
        {
            Teacherid = teacherid;
            Name = "Добавьте имя";
        }
    }
}
