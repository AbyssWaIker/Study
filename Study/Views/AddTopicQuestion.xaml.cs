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

namespace Study
{
    /// <summary>
    /// Логика взаимодействия для AddTopicQuestion.xaml
    /// </summary>
    public partial class AddTopicQuestion : Window
    {

        bool newQuestion;

        /// <summary>
        /// конструктор для создания нового вопроса
        /// </summary>
        ITopicQuestionRequestor caller;
        public AddTopicQuestion(ITopicQuestionRequestor callingForm)
        {
            InitializeComponent();
            caller = callingForm;
            newQuestion = true;
            TimeToAnswerValue.Text = "30";
        }

        /// <summary>
        /// конструктор для изменения вопроса
        /// </summary>
        private List<String> WrongAnswerText = new List<String>();

        private QuestionModel q = new QuestionModel();

        IChangeQuestion callerForChange;
        public AddTopicQuestion(QuestionModel qm, IChangeQuestion callingForm)
        {
            InitializeComponent();

            QuestionTextValue.Text = qm.QuestionText;
            CorrectAnswerTextValue.Text = qm.CorrectAnswer;
            qm.wrongAnswers = GlobalConfig.connection.GetWrongAnswerModels_byQuestionid(qm.id);
            foreach (WrongAnswerModel wm in qm.wrongAnswers)
            {
                WrongAnswerText.Add(wm.WrongAnswerText);
            }
            wrongAnswers.ItemsSource = WrongAnswerText;
            TimeToAnswerValue.Text = qm.timeToAnswer.ToString();

            q = qm;
            callerForChange = callingForm;
            newQuestion = false;
        }

        private WrongAnswerModel wam = new WrongAnswerModel();
        private void addWrongAnswer_Click(object sender, RoutedEventArgs e)
        {
            WrongAnswerModel wa = new WrongAnswerModel(WrongAnswerTextValue.Text);
            wam = wa;
            q.wrongAnswers.Add(wam);
            WrongAnswerText.Add(WrongAnswerTextValue.Text);
            WrongAnswerTextValue.Text = "";
            wrongAnswers.ItemsSource = null;
            wrongAnswers.ItemsSource = WrongAnswerText;
        }

        private void deleteWrongAnswer_Click(object sender, RoutedEventArgs e)
        {
            String selectedAnswer = (String)wrongAnswers.SelectedItem;
            if(selectedAnswer!=null)
            {
                WrongAnswerText.Remove(selectedAnswer);
                wrongAnswers.ItemsSource = null;
                wrongAnswers.ItemsSource = WrongAnswerText;

            }
        }



        private void ConfirmQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            int TimeToanswer = 0;
            bool timeIsValid = int.TryParse(TimeToAnswerValue.Text, out TimeToanswer);

            bool ThereIsInfo = false;

            if (QuestionTextValue.Text != "" && CorrectAnswerTextValue.Text != "" && wrongAnswers.Items.Count!=0 && TimeToAnswerValue.Text != "")
            {
                ThereIsInfo = true;
            }

            if (ThereIsInfo)
            {
                if (timeIsValid)
                {
                    q.QuestionText = QuestionTextValue.Text;
                    q.CorrectAnswer = CorrectAnswerTextValue.Text;
                    q.wrongAnswers.Clear();
                    foreach(String wm in WrongAnswerText)
                    {
                        q.wrongAnswers.Add(new WrongAnswerModel(q.id, wm)); 
                    }
                    q.timeToAnswer = TimeToanswer;
                    if (newQuestion) {caller.QuestionCreated(q);} //создаем новый вопрос
                    else {callerForChange.QuestionChanged(q);} //или изменяем старый
                    this.Close();
                }
                else MessageBox.Show("Введите правильное время вопроса в секундах", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Введите данные вопроса", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
