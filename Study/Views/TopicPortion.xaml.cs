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
    /// Логика взаимодействия для TopicPortion.xaml
    /// </summary>
    public partial class TopicPortion : Window
    {
        int i;
        TopicModel tm1;
        StudentModel stm;
        ITopicResultsRequestor caller;
        public TopicPortion(TopicModel tm,StudentModel stml, ITopicResultsRequestor callingForm)
        {
            InitializeComponent();
            TopicPortionNameValue.Text = tm.topicName;
            TopicPortionTextValue.Text = "Чтобы начать изучение темы нажмите кнопку \"далее\" ";

            i = 0;
            tm1 = tm;
            stm = stml;
            caller = callingForm;
        }

        private void ShowTopicProtion()
        {
            TopicPortionNameValue.Text = tm1.TopicPortions.ElementAt(i).TopicPortionName; 
            TopicPortionTextValue.Text = tm1.TopicPortions.ElementAt(i).TopicPortionText;
            i = i + 1;
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (i == tm1.TopicPortions.Count)
            {
                MessageBox.Show("Это был последний раздел. ");
                string doYouContinue = "Чтобы успешно завершить тему вам надо успешно ответить на половину или больше дальнейших вопросов.\n" +
                    "У вас есть только одна попытка.\n" +
                    "Продолжить и отвечать на вопросы (да) или начать чтение главы заново (нет)?";
                if (MessageBox.Show(doYouContinue, "Продолжить?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    TopicQuestion tq = new TopicQuestion(tm1, stm, caller);
                    tq.Show();
                    this.Close();
                }
                else
                {
                    i = 0;
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
