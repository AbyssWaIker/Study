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
        private int rightAnswer;
        private TopicModel tpm;
        private int nextQ;
        private int CAQ;
        StudentModel stm;
        ITopicResultsRequestor caller;
        public TopicQuestion(TopicModel tm, int currentQuestion, int correctlyAnsweredQuestion, StudentModel stml, ITopicResultsRequestor callingform)
        {
            InitializeComponent();

            tpm = tm;
            nextQ = currentQuestion + 1;
            CAQ = correctlyAnsweredQuestion;
            stm = stml;
            caller = callingform;

            QuestionText.Text = tm.Questions.ElementAt(currentQuestion).QuestionText;

            RandomizeAnswersPlaces(tm, currentQuestion);

            Timer(tm, currentQuestion);
        }

        private void RandomizeAnswersPlaces(TopicModel tm, int currentQuestion)
        {
            Random rnd = new Random();
            int position = rnd.Next(1, 4);
            rightAnswer = position;
            if (position == 1)
            {
                AnswerText1.Text = tm.Questions.ElementAt(currentQuestion).CorrectAnswer;
                AnswerText2.Text = tm.Questions.ElementAt(currentQuestion).WrongAnswer1;
                AnswerText3.Text = tm.Questions.ElementAt(currentQuestion).WrongAnswer2;
            }

            if (position == 2)
            {
                AnswerText1.Text = tm.Questions.ElementAt(currentQuestion).WrongAnswer1;
                AnswerText2.Text = tm.Questions.ElementAt(currentQuestion).CorrectAnswer;
                AnswerText3.Text = tm.Questions.ElementAt(currentQuestion).WrongAnswer2;
            }

            if (position == 3)
            {
                AnswerText1.Text = tm.Questions.ElementAt(currentQuestion).WrongAnswer1;
                AnswerText2.Text = tm.Questions.ElementAt(currentQuestion).WrongAnswer2;
                AnswerText3.Text = tm.Questions.ElementAt(currentQuestion).CorrectAnswer;
            }
        }


        DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        private void Timer(TopicModel tm, int currentQuestion)
        {
            int timetoAnswer = tm.Questions.ElementAt(currentQuestion).timeToAnswer;
            timeLeft = timetoAnswer;
            TimeToAnswerValue.Text = timeLeft.ToString();
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
            }

        }

        private void AnswerCheck1_Checked(object sender, RoutedEventArgs e)
        {
            AnswerCheck2.IsChecked = false;
            AnswerCheck3.IsChecked = false;
        }

        private void AnswerCheck2_Checked(object sender, RoutedEventArgs e)
        {
            AnswerCheck1.IsChecked = false;
            AnswerCheck3.IsChecked = false;
        }

        private void AnswerCheck3_Checked(object sender, RoutedEventArgs e)
        {
            AnswerCheck1.IsChecked = false;
            AnswerCheck2.IsChecked = false;
        }

        private void Check()
        {
            if (rightAnswer == 1 && AnswerCheck1.IsChecked == true)
            {
                CAQ = CAQ + 1;
            }
            if (rightAnswer == 2 && AnswerCheck2.IsChecked == true)
            {
                CAQ = CAQ + 1;
            }

            if (rightAnswer == 3 && AnswerCheck3.IsChecked == true)
            {
                CAQ = CAQ + 1;
            }
        }

        private void Continue_Button_Click(object sender, EventArgs e)
        {
            dispatcherTimer.Stop();
            Check();
            if (nextQ == tpm.Questions.Count)
            {
                dispatcherTimer.Stop();
                GradeModel grade = new GradeModel();
                grade.Topicid = tpm.id;
                grade.studentid = stm.id;
                grade.QuestionAnsweredCorrectly = CAQ;
                grade.QuestionAnswered = tpm.Questions.Count;
                GlobalConfig.connection.createGrade(grade);
                stm.grades.Add(grade);

                caller.gradecomplete(tpm);
                MessageBox.Show($"Вы правильно ответили на {grade.QuestionAnsweredCorrectly} из {grade.QuestionAnswered} вопроса");
                this.Close();
            }
            else
            {
                TopicQuestion Tq = new TopicQuestion(tpm, nextQ, CAQ, stm, caller);
                Tq.Show();
                this.Close();

            }
        }
    }
}
