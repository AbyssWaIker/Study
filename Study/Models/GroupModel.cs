using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study.Models
{
    public class GroupModel
    {
        public int id { get; set; }
        public String GroupName { get; set; }

        public bool access { get; set; }

        public GroupModel()
        {

        }

        public GroupModel(string n)
        {
            GroupName = n;
        }

        public GroupModel(int i, string n)
        {
            id = i;
            GroupName = n;
        }
    }
}
