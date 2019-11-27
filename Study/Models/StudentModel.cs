using System;
using System.Collections.Generic;
using System.Text;

namespace Study
{
    public class StudentModel
    {

        /// <summary>
        /// представляет уникальный идентификатор студента
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// представляет имя студента
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// представляет группу студента
        /// </summary>
        public String Group { get; set; }

        /// <summary>
        /// представляет оценки студента
        /// </summary>
        public List<GradeModel> grades { get; set; } = new List<GradeModel>();


        public StudentModel()
        {

        }


        public StudentModel(string name, string group)
        {
            Name = name;
            Group = group;
        }



    }
}
