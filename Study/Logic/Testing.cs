using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study.Logic
{
    public static class Testing
    {
        private static int correctAnswerPosition { get; set; }
        public static int currentQuestion { get; set; }
        public static int correctlyAnsweredQuestion { get; set; }

        public static void StartQuestions()
        {
            correctAnswerPosition = 0;
            currentQuestion = 0;
            correctlyAnsweredQuestion = 0;
        }


        public static List<String> Answers(QuestionModel question)
        {
            List<String> Answers = new List<string>();
            question.wrongAnswers = GlobalConfig.connection.GetWrongAnswerModels_byQuestionid(question.id);
            foreach (WrongAnswerModel wrongAnswer in question.wrongAnswers)
            {
                Answers.Add(wrongAnswer.WrongAnswerText);
            }

            Random rnd = new Random();
            correctAnswerPosition = rnd.Next(0, Answers.Count + 1);
            Answers.Insert(correctAnswerPosition, question.CorrectAnswer);

            return Answers;
        }

        public static bool isAllQuestionAnswered(int SelectedIndex)
        {
            if (SelectedIndex == correctAnswerPosition)
            {
                correctlyAnsweredQuestion++;
            }
            currentQuestion++;

            if (currentQuestion == DisplayedLearningMaterial.CurrentTopic.Questions.Count)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public static GradeModel createGrade()
        {

            GradeModel grade = new GradeModel();
            grade.Topicid = DisplayedLearningMaterial.CurrentTopic.id;
            grade.setStudentID(UsersDataControl.currentStudent.id);
            grade.QuestionAnsweredCorrectly = correctlyAnsweredQuestion;
            grade.QuestionAnswered = currentQuestion;

            GlobalConfig.connection.createGrade(grade);
            UsersDataControl.currentStudent.grades.Add(grade);

            DisplayedLearningMaterial.swapTopic(DisplayedLearningMaterial.CurrentTopic);

            return grade;
        }
    }
}
