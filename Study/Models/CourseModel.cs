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
        public List<TopicModel> topics = new List<TopicModel>();
        public List<StudentModel> students = new List<StudentModel>();
        public List<StudentToCourseRealationModel> studentToCourseRealations = new List<StudentToCourseRealationModel>();
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
