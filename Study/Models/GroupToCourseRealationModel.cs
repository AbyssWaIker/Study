using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study.Models
{
    public class GroupToCourseRealationModel
    {
        public int id { get; set; }
        public int Courseid { get; set; }
        public int Groupid { get; set; }
        public int Access { get; set; }
        public string GroupName
        {
            get
            {
                return GlobalConfig.connection.GetGroupName(Groupid);
            }
        }
        public bool AccessBool
        {
            get
            {
                if(Access == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public GroupToCourseRealationModel()
        {

        }

        public GroupToCourseRealationModel(int cid, int sid)
        {
            Courseid = cid;
            Groupid = sid;
            Access = 0;
        }
    }
}
