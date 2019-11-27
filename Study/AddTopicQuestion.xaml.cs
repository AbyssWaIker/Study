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
        ITopicQuestionRequestor caller;
        public AddTopicQuestion(ITopicQuestionRequestor callingForm)
        {
            InitializeComponent();
            caller = callingForm;
        }

        private void ConfirmQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            int TimeToanswer = 0;
            bool timeIsValid = int.TryParse(TimeToAnswerValue.Text, out TimeToanswer);

            bool ThereIsInfo = false;

            if (QuestionTextValue.Text != "" && CorrectAnswerTextValue.Text != "" && WrongAnswer1TextValue.Text != "" && WrongAnswer2TextValue.Text != "" && TimeToAnswerValue.Text != "")
            {
                ThereIsInfo = true;
            }

            if (ThereIsInfo)
            {
                if (timeIsValid)
                {
                    QuestionModel q = new QuestionModel();
                    q.QuestionText = QuestionTextValue.Text;
                    q.CorrectAnswer = CorrectAnswerTextValue.Text;
                    q.WrongAnswer1 = WrongAnswer1TextValue.Text;
                    q.WrongAnswer2 = WrongAnswer2TextValue.Text;
                    q.timeToAnswer = TimeToanswer;

                    caller.QuestionCreated(q);

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
