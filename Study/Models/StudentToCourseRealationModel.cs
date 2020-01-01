using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study.Models
{
    public class StudentToCourseRealationModel
    {
        public int id { get; set; }
        public int Courseid { get; set; }
        public int Studentid { get; set; }

        public StudentToCourseRealationModel()
        {

        }

        public StudentToCourseRealationModel(int cid, int sid)
        {
            Courseid = cid;
            Studentid = sid;
        }
    }
}
