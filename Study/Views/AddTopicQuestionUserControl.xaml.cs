using Study.Logic;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Study.Views
{
    /// <summary>
    /// Логика взаимодействия для AddTopicQuestionUserControl.xaml
    /// </summary>
    public partial class AddTopicQuestionUserControl : UserControl
    {
        public AddTopicQuestionUserControl()
        {
            InitializeComponent();

            if (!LearningMaterialInsert.newQuestion)
            {
                AddOrChange.Text = "Изменить вопрос";

                QuestionTextValue.Text = LearningMaterialInsert.questionForChange.QuestionText;
                CorrectAnswerTextValue.Text = LearningMaterialInsert.questionForChange.CorrectAnswer;
                TimeToAnswerValue.Text = LearningMaterialInsert.questionForChange.timeToAnswer.ToString();

                LearningMaterialInsert.getWrongAnswers();
                wrongAnswers.ItemsSource = LearningMaterialInsert.WrongAnswerText;
            }
        }

        private void addWrongAnswer_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(WrongAnswerTextValue.Text))
            {
                LearningMaterialInsert.addWrongAnswer(WrongAnswerTextValue.Text);

                WrongAnswerTextValue.Text = "";


                wrongAnswers.ItemsSource = null;
                wrongAnswers.ItemsSource = LearningMaterialInsert.WrongAnswerText;
            }
        }

        private void deleteWrongAnswer_Click(object sender, RoutedEventArgs e)
        {
            String selectedAnswer = (String)wrongAnswers.SelectedItem;
            if (selectedAnswer != null)
            {
                LearningMaterialInsert.WrongAnswerText.Remove(selectedAnswer);
            }
        }



        private void ConfirmQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            //получаем информацию о времени на ответ вопроса (и не записал ли польователь в строку, что-то кроме цифр)
            bool userEnteredNumberInToTimeField = int.TryParse(TimeToAnswerValue.Text, out int TimeToanswer);

            if (!string.IsNullOrWhiteSpace(QuestionTextValue.Text) && !string.IsNullOrWhiteSpace(CorrectAnswerTextValue.Text) && wrongAnswers.Items.Count != 0 && !string.IsNullOrWhiteSpace(TimeToAnswerValue.Text))
            {
                if (userEnteredNumberInToTimeField)
                {
                    LearningMaterialInsert.WriteNewInfoIntoQuestion(QuestionTextValue.Text, CorrectAnswerTextValue.Text, TimeToanswer);

                    CommandBinding returnToTheTopicScreen = new CommandBinding(StartWindowShell.LoadTeacherAddTopicScreen);
                    returnToTheTopicScreen.Command.Execute("placeholder object");
                }
                else MessageBox.Show("Введите правильное время вопроса", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Введите данные вопроса", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
