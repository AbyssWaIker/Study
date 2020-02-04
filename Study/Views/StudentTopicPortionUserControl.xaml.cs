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

namespace Study.Views
{
    /// <summary>
    /// Логика взаимодействия для StudentTopicPortionUserControl.xaml
    /// </summary>
    public partial class StudentTopicPortionUserControl : UserControl
    {
        public StudentTopicPortionUserControl()
        {
            InitializeComponent();

            TopicPortionNameValue.Text = Logic.DisplayedLearningMaterial.CurrentTopic.topicName;
            TopicPortionTextValue.Text = "Чтобы начать изучение темы нажмите кнопку \"далее\" ";
        }

        private void ShowTopicProtion()
        {
            TopicPortionModel currentTopicPortion = Logic.DisplayedLearningMaterial.GetCurrentTopicPortion();
            TopicPortionNameValue.Text = currentTopicPortion.TopicPortionName;
            TopicPortionTextValue.Text = currentTopicPortion.TopicPortionText;
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (Logic.DisplayedLearningMaterial.CurrentTopicPortionNumber == Logic.DisplayedLearningMaterial.CurrentTopic.TopicPortions.Count)
            {
                MessageBox.Show("Это был последний раздел. ");
                string doYouContinue = "Чтобы успешно завершить тему вам надо успешно ответить на половину или больше дальнейших вопросов.\n" +
                    "У вас есть только одна попытка.\n" +
                    "Продолжить (да) и отвечать на вопросы или вернуться (нет), чтобы перечитать главу?";
                if (MessageBox.Show(doYouContinue, "Продолжить?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    CommandBinding openQuestions = new CommandBinding(StartWindowShell.LoadStudentTopicQuestionScreen);
                    openQuestions.Command.Execute("placeholder object");
                }
                else
                {
                    Logic.DisplayedLearningMaterial.CurrentTopicPortionNumber = 0;
                    ShowTopicProtion();
                }
            }
            else
            {
                ShowTopicProtion();
            }
        }
    }
}
