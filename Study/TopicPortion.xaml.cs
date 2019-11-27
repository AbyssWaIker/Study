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
        int ij;
        TopicModel tm1;
        StudentModel stm;
        ITopicResultsRequestor caller;
        public TopicPortion(TopicModel tm, int i, StudentModel stml, ITopicResultsRequestor callingForm)
        {
            InitializeComponent();
            TopicPortionNameValue.Text = tm.TopicPortions.ElementAt(i).TopicPortionName;
            TopicPortionTextValue.Text = tm.TopicPortions.ElementAt(i).TopicPortionText;

            ij = i + 1;
            tm1 = tm;
            stm = stml;
            caller = callingForm;
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            int i = ij;
            if (i == tm1.TopicPortions.Count)
            {
                int CurrentQuestion = 0;
                int CorrectlyAnsweredQuestions = 0;
                TopicQuestion tq = new TopicQuestion(tm1, CurrentQuestion, CorrectlyAnsweredQuestions, stm, caller);
                tq.Show();
                this.Close();
            }
            else
            {
                TopicPortion tpv = new TopicPortion(tm1, i, stm, caller);
                tpv.Show();
                this.Close();
            }
        }
    }
}
