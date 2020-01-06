using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Study
{
    /// <summary>
    /// Логика взаимодействия для TopicQuestion.xaml
    /// </summary>
    public partial class TopicQuestion : Window
    {
        public int timeLeft;
        private int correctAnswerPosition;
        private TopicModel tpm;
        private int currentQuestion;
        private int correctlyAnsweredQuestion;
        StudentModel stm;
        ITopicResultsRequestor caller;
        public TopicQuestion(TopicModel tm, StudentModel stml, ITopicResultsRequestor callingform)
        {
            InitializeComponent();

            tpm = tm;
            currentQuestion = 0;
            correctlyAnsweredQuestion = 0;
            stm = stml;
            caller = callingform;

            RandomizeQuestionAnswer();

            Timer(tm, currentQuestion);
        } 

        private void RandomizeQuestionAnswer()
        {
            QuestionText.Text = tpm.Questions.ElementAt(currentQuestion).QuestionText;
            dispatcherTimer.IsEnabled = true;
            Timer(tpm, currentQuestion);


            List<String> Answers = new List<string>();
            QuestionModel question = tpm.Questions.ElementAt(currentQuestion);
            question.wrongAnswers = GlobalConfig.connection.GetWrongAnswerModels_byQuestionid(question.id);
            foreach (WrongAnswerModel wrongAnswer in question.wrongAnswers)
            {
                Answers.Add(wrongAnswer.WrongAnswerText);
            }

            Random rnd = new Random();
            correctAnswerPosition = rnd.Next(0, Answers.Count+1);
            Answers.Insert(correctAnswerPosition, question.CorrectAnswer);

            AnswersList.ItemsSource = null;
            AnswersList.ItemsSource = Answers;

        }

        DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        private void Timer(TopicModel tm, int currentQuestion)
        {
            int timetoAnswer = tm.Questions.ElementAt(currentQuestion).timeToAnswer;
            timeLeft = timetoAnswer;
            TimeToAnswerValue.Text = timeLeft.ToString();
            dispatcherTimer.Tick -= dispatcherTimer_Tick;
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            int time = timeLeft;
            if (timeLeft > 1)
            {
                timeLeft = timeLeft - 1;
                TimeToAnswerValue.Text = timeLeft.ToString();

            }
            else
            {
                MessageBox.Show("Время истекло");
                Continue_Button_Click(sender, e);
                dispatcherTimer.Stop();
            }

        }

        private void Check()
        {
            if (AnswersList.SelectedIndex == correctAnswerPosition)
            {
                currentQuestion++;
                correctlyAnsweredQuestion++;
            }
            else
            {
                currentQuestion++;
            }
        }

        private void Continue_Button_Click(object sender, EventArgs e)
        {

            dispatcherTimer.Stop();
            dispatcherTimer.IsEnabled = false;
            Check();
            if (currentQuestion == tpm.Questions.Count)
            {
                GradeModel grade = new GradeModel();
                grade.Topicid = tpm.getID();
                grade.setStudentID(stm.id);
                grade.QuestionAnsweredCorrectly = correctlyAnsweredQuestion;
                grade.QuestionAnswered = tpm.Questions.Count;
                GlobalConfig.connection.createGrade(grade);
                stm.grades.Add(grade);

                caller.gradecomplete(tpm);
                MessageBox.Show($"Вы правильно ответили на {grade.QuestionAnsweredCorrectly} из {grade.QuestionAnswered} вопроса");
                this.Close();
            }
            else
            {
                RandomizeQuestionAnswer();
            }
        }
    }
}
