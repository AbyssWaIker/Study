using Study.Logic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Study.Views
{
    /// <summary>
    /// Логика взаимодействия для StudentTopicQuestionUserControl.xaml
    /// </summary>
    public partial class StudentTopicQuestionUserControl : UserControl
    {

        private int timeLeft;
        public StudentTopicQuestionUserControl()
        {
            InitializeComponent();

            Testing.StartQuestions();
            RandomizeQuestionAnswer();

            Timer();
        }


        private void RandomizeQuestionAnswer()
        {
            QuestionText.Text = DisplayedLearningMaterial.CurrentTopic.Questions.ElementAt(Testing.currentQuestion).QuestionText;
            dispatcherTimer.IsEnabled = true;
            Timer();


            QuestionModel question = DisplayedLearningMaterial.CurrentTopic.Questions.ElementAt(Testing.currentQuestion);
            List<String> Answers = Testing.Answers(question);

            AnswersList.ItemsSource = null;
            AnswersList.ItemsSource = Answers;

        }

        private DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        private void Timer()
        {
            int timetoAnswer = DisplayedLearningMaterial.CurrentTopic.Questions.ElementAt(Testing.currentQuestion).timeToAnswer;
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

        private void Continue_Button_Click(object sender, EventArgs e)
        {

            dispatcherTimer.Stop();
            dispatcherTimer.IsEnabled = false;


            bool thatWasTheLastQuestion = Testing.isAllQuestionAnswered(AnswersList.SelectedIndex);

            if (thatWasTheLastQuestion)
            {
                GradeModel grade = Testing.createGrade();
                MessageBox.Show($"Вы правильно ответили на {grade.QuestionAnsweredCorrectly} из {grade.QuestionAnswered} вопроса");

                CommandBinding openTopicList = new CommandBinding(StartWindowShell.LoadStudentTopicListScreen);
                openTopicList.Command.Execute("placeholder object");
            }
            else
            {
                RandomizeQuestionAnswer();
            }
        }
    }
}
