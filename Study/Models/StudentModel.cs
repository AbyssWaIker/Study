using Study.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Study
{
    public class StudentModel
    {

        //представляет уникальный идентификатор студента
        public int id { get; set; }

        // представляет имя студента
        public String StudentName { get; set; }

        // представляет группу студента
        public String StudentGroup { get; set; }

        // представляет оценки студента
        public List<GradeModel> grades { get; set; } = new List<GradeModel>();

        //представляет список курсов к которым у студента есть доступ
        public List<StudentToCourseRealationModel> studentToCourseRealations = new List<StudentToCourseRealationModel>();

        private String userName;
        private String StudentPassword;

        public bool access { get; set; }

        public String SuccesfulTopicsNumber
        {
            get
            {
                if(grades.Count!=0)
                {
                    int correct = 0;
                    foreach (GradeModel g in grades)
                    {

                        if (g.isSuccesful)
                        {
                            correct++;
                        }
                    }
                    return $"Успешно пройдено {correct} из {grades.Count} тем";
                }
                else return "Прохождение тем не начато";
            }
        }

        public StudentModel()
        {

        }
        public StudentModel(string name, string group, string username,string password)
        {
            StudentName = name;
            StudentGroup = group;
            userName = username;
            StudentPassword = password;
        }

        public String getUserName()
        {
            return userName;
        }

        public string getPassword()
        {
            return StudentPassword;
        }
    }
}
