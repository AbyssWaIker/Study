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
    /// Логика взаимодействия для TopicList.xaml
    /// </summary>
    public partial class TopicList : Window, ITopicResultsRequestor
    {
        private StudentModel stm;
        List<TopicModel> tml1 = new List<TopicModel>();
        public TopicList(StudentModel st)
        {
            InitializeComponent();
            List<TopicModel> tml = GlobalConfig.connection.GetTopicModels_All();

            int i = 1;
            while (i <= tml.Count)
            {
                for (int j = 0; j < tml.Count; j++)
                {
                    int o = tml.ElementAt(j).TopicOrderNumber;
                    if (o == i)
                    {
                        tml1.Add(tml.ElementAt(j));
                        i = i + 1;
                    }
                }
            }


            UnFinishedTopics.ItemsSource = tml1;
            stm = st;


        }

        public void gradecomplete(TopicModel model)
        {
            if (model != null)
            {
                tml1.Remove(model);
                UnFinishedTopics.ItemsSource = null;
                UnFinishedTopics.ItemsSource = tml1;

                FinishedTopics.Items.Add(model);
                if (UnFinishedTopics.Items.Count == 0)
                {
                    StartUnfinishedTopic.IsEnabled = false;
                    GetResults.IsEnabled = true;
                }
            }
        }

        private void StartUnfinishedTopic_Click(object sender, RoutedEventArgs e)
        {
            TopicModel tm = (TopicModel)UnFinishedTopics.SelectedItem;
            if (tm != null)
            {
                if (UnFinishedTopics.SelectedIndex == 0)
                {
                    int tmid = tm.id;
                    tm.Questions = GlobalConfig.connection.GetQuestions_byTopic(tmid);
                    tm.TopicPortions = GlobalConfig.connection.GetTopicPortions_bytopic(tmid);
                    int i = 0;
                    TopicPortion tpv = new TopicPortion(tm, i, stm, this);
                    tpv.Show();
                }
                else
                {
                    MessageBox.Show("проходи темы по порядку :)");
                }
            }
        }
    }
}

