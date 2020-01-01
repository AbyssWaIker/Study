using System;
using System.Collections.Generic;
using System.Text;

namespace Study
{
    public class QuestionModel
    {
        public int id { get; set; }

        public int Topicid { get; set; }
        public String QuestionText { get; set; }
        public String CorrectAnswer { get; set; }
        public int timeToAnswer { get; set; }

        public List<WrongAnswerModel> wrongAnswers = new List<WrongAnswerModel>();
    }
}
