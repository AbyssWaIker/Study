using System;
using System.Collections.Generic;
using System.Text;

namespace Study
{
    // представляет модель оценки полученной студентом
    public class GradeModel
    {
        // представляет уникальный идентификатор оценки
        private int id { get; set; }
        // представляет студента, который получил оценку
        public int studentid { get; set; }
        // представляет тему, по которой получена оценка
        public int Topicid { get; set; }

        //при необходимости, получает название темы, по которой получена оценка
        public String TopicName
        {
            get
            {
                return GlobalConfig.connection.getTopicNameById(Topicid);
            }
        }

        public int TopicOrderNumber
        {
            get
            {
                return GlobalConfig.connection.GetTopicOrderNumberByid(Topicid);
            }
        }
        public int Courseid
        {
            get
            {
                return GlobalConfig.connection.getCourseidByTopicid(Topicid);
            }
        }

        public int QuestionAnsweredCorrectly { get; set; }
        public int QuestionAnswered { get; set; }
        public bool isSuccesful
        {
            get
            {
                double QAC = QuestionAnsweredCorrectly;
                double QA = QuestionAnswered;
                double howGood = QAC / QA;
                double required = (QA / 2) / QA;
                if (howGood >= required)
                {
                    return true;
                }
                else return false;
            }
        }
        public String getGradeInfoForStudent()
        {
            TopicModel tm = GlobalConfig.connection.getTopicById(Topicid);
            if (isSuccesful)
            {
                return $"В теме №{tm.TopicOrderNumber}: {tm.topicName}, курса {GlobalConfig.connection.getCourseNamebyId(Courseid)}  \nбыло правильно отвечено " +
                    $"на {QuestionAnsweredCorrectly} из {QuestionAnswered} вопросов.\nТема завершена успешно\n";
            } 
            else 
                return $"В теме №{tm.TopicOrderNumber}: {tm.topicName}, курса {GlobalConfig.connection.getCourseNamebyId(Courseid)}  \nбыло правильно отвечено " +
                    $"на {QuestionAnsweredCorrectly} из {QuestionAnswered} вопросов.\nТема провалена\n";
            
        }

        public void setID(int i)
        {
            id = i;
        }
        public int getStudentID()
        {
            return studentid;
        }
        public void setStudentID(int si)
        {
            studentid = si;
        }

    }
    
}
