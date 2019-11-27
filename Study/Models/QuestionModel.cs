using System;
using System.Collections.Generic;
using System.Text;

namespace Study
{
    public class QuestionModel
    {
        public int id;

        public int Topicid;
        public String QuestionText { get; set; }
        public String CorrectAnswer { get; set; }
        public String WrongAnswer1 { get; set; }

        public String WrongAnswer2 { get; set; }

        public int timeToAnswer { get; set; }

    }
}
