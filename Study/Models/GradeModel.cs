using System;
using System.Collections.Generic;
using System.Text;

namespace Study
{
    /// <summary>
    /// представляет модель оценки полученной студентом
    /// </summary>
    public class GradeModel
    {

        /// <summary>
        /// представляет уникальный идентификатор оценки
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// представляет студента, который получил оценку
        /// </summary>
        public int studentid { get; set; }
        /// <summary>
        /// представляет тему, по которой получена оценка
        /// </summary>
        public int Topicid { get; set; }
        public int QuestionAnsweredCorrectly { get; set; }
        public int QuestionAnswered { get; set; }

        public bool isSuccesful
        {
            get
            {
                TopicModel tm = GlobalConfig.connection.getTopicById(Topicid);
                double howGood = QuestionAnsweredCorrectly / QuestionAnswered;
                if (howGood > (QuestionAnswered / 2) / 2)
                {
                    return true;
                }
                else return false;
            }
        }

        public String GradeInfoForStudent
        {
            get
            {
                TopicModel tm = GlobalConfig.connection.getTopicById(Topicid);
                if (isSuccesful)
                {
                    return $"В теме №{tm.TopicOrderNumber}: {tm.topicName} было правильно отвечено на {QuestionAnsweredCorrectly} из {QuestionAnswered} вопросов.\nТема завершена успешно";
                } else return $"В теме №{tm.TopicOrderNumber}: {tm.topicName} было правильно отвечено на {QuestionAnsweredCorrectly} из {QuestionAnswered} вопросов.\nТема провалена";
            }
        }


        public String FullGradeInfoForProffessor
        {
            get
            {
                StudentModel st = GlobalConfig.connection.getStudentByid(studentid);
                TopicModel tm = GlobalConfig.connection.getTopicById(Topicid);
                if (isSuccesful)
                {
                    return $"Студент:{st.Name} \t | группа:{st.Group} \t | \n Тема №{tm.TopicOrderNumber}: {tm.topicName} \t | {QuestionAnsweredCorrectly} из {QuestionAnswered} вопросов правильно \t | \nТема завершена успешно";
                }
                else return $"Студент:{st.Name} \t | группа:{st.Group} \t | \n Тема №{tm.TopicOrderNumber}: {tm.topicName} \t | {QuestionAnsweredCorrectly} из {QuestionAnswered} вопросов правильно \t | \nТема провалена";

            }
        } 

    }
    
}
