using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study
{
    public class WrongAnswerModel
    {
        public int id { get; set; }
        public int QuestionId { get; set; }
        public String WrongAnswerText { get; set; }

        public WrongAnswerModel()
        {

            id = 0;
            QuestionId = 0;
            WrongAnswerText = "";
        }

        public WrongAnswerModel(String wat)
        {
            id = 0;
            QuestionId = 0;
            WrongAnswerText = wat;
        }

        public WrongAnswerModel(int qid,String wat)
        {
            id = 0;
            QuestionId = qid;
            WrongAnswerText = wat;
        }
    }
}
