using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study.Logic
{
    /// <summary>
    /// Класс, отображающий вопросы для студента и оценивающий ответы, составляя оценку
    /// </summary>
    public static class Testing
    {
        /// <summary>
        /// скрытая переменная, хранящая позицию правильного ответа в отображаемом списке
        /// </summary>
        /// 
        private static int correctAnswerPosition { get; set; }
        /// <summary>
        /// Номер текущего вопроса в списке вопросов темы
        /// </summary>
        public static int currentQuestion { get; set; }

        /// <summary>
        /// Количество вопросов, на которые студент ответил правильно
        /// </summary>
        public static int correctlyAnsweredQuestion { get; set; }

        /// <summary>
        /// Метод инициализирующий/сбрасывающий значения всех переменных для начала тестов новой темы
        /// </summary>
        public static void StartQuestions()
        {
            correctAnswerPosition = 0;
            currentQuestion = 0;
            correctlyAnsweredQuestion = 0;
        }


        /// <summary>
        /// Отображение списка ответов на вопрос, распологающий правильный ответ на случайном месте
        /// </summary>
        /// <param name="question">Текущий вопрос, ответы которого надо отобразить</param>
        /// <returns></returns>
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

        /// <summary>
        /// Метод вызваемый при ответе на вопрос. 
        /// Проверяет правильно ли студент ответил на вопрос (обновляющяя колчичество правильных ответов и номер текущего вопроса) и ответил ли пользователь на все вопросы
        /// </summary>
        /// <param name="SelectedIndex">Индекс ответа, выбранный студентом. Сравнивается с индексом правильного ответа и если они совпадают, увеличиает количество правильных ответов</param>
        /// <returns></returns>
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

        /// <summary>
        /// Сохранение оценки
        /// </summary>
        public static GradeModel createGrade()
        {

            GradeModel grade = new GradeModel
            {
                Topicid = DisplayedLearningMaterial.CurrentTopic.id,
                studentid = UsersDataControl.currentStudent.id,
                QuestionAnsweredCorrectly = correctlyAnsweredQuestion,
                QuestionAnswered = currentQuestion
            };

            GlobalConfig.connection.createGrade(grade);
            UsersDataControl.currentStudent.grades.Add(grade);

            DisplayedLearningMaterial.swapTopic(DisplayedLearningMaterial.CurrentTopic);

            return grade;
        }
    }
}
