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
        //Данные студента, которые хранит его страница
        private StudentModel stm;

        //список тем
        List<TopicModel> tml1 = new List<TopicModel>();
        CourseModel course;

        //Студент входит
        public TopicList(StudentModel st, CourseModel c)
        {
            InitializeComponent();

            //получение списка тем
            List<TopicModel> tml = GlobalConfig.connection.GetTopicModels_byCourseID(c.id);

            //сортировка списка тем по порядку
            tml1 = tml;

            //если студент уже прошел какие-то темы
            if (st.grades.Count!=0)
            {
                //переносимпройденные темы в соответствующий список
                foreach (GradeModel g in st.grades)
                {
                    bool topicInCourse = tml1.Exists(t1 => t1.getID() == g.Topicid);
                    if (topicInCourse)
                    {
                        //находим нужную тему
                        TopicModel t = tml1.Find(t1 => t1.getID() == g.Topicid);

                        //перемещаем ее в другой список
                        swapTopic(t);
                    }
                    else
                    {
                        st.grades.Remove(g);
                    }
                }
            }

            //отбражаем непройденные темы
            UnFinishedTopics.ItemsSource = tml1;
            //запоминаем данные о студенте
            stm = st;
            course = c;

        }

        //перемещаем пройденную тему в нужный список список
        private void swapTopic(TopicModel t)
        {
            //убираем ее из списка непройденных тем
            tml1.Remove(t);

            UnFinishedTopics.ItemsSource = null;
            UnFinishedTopics.ItemsSource = tml1;
            //и добавляем в список пройденных
            FinishedTopics.Items.Add(t);

            //если в списке непройденных тем ничего не осталось
            if (UnFinishedTopics.Items.Count == 0)
            {
                // отключаем кнопку "пройти тему"
                StartUnfinishedTopic.IsEnabled = false;
                StartUnfinishedTopic.IsDefault = false;

                //открываем доступ к просмотру результатов 
                GetResults.IsEnabled = true;
                GetResults.IsDefault = true;
            }
        }

        //когда тема пройдена и по ней получена оценка
        public void gradecomplete(TopicModel model)
        {
            //на всякий случай проверяем получили ли мы информацию о ней
            if (model != null)
            {
                //перемещаем ее в другой список
                swapTopic(model);
            }
        }

        private void StartUnfinishedTopic_Click(object sender, RoutedEventArgs e)
        {
            TopicModel tm = (TopicModel)UnFinishedTopics.SelectedItem;
            if (tm != null)
            {
                if (UnFinishedTopics.SelectedIndex == 0)
                {
                    int tmid = tm.getID();
                    tm.Questions = GlobalConfig.connection.GetQuestions_byTopic(tmid);
                    tm.TopicPortions = GlobalConfig.connection.GetTopicPortions_bytopic(tmid);
                    TopicPortion tpv = new TopicPortion(tm, stm, this);
                    tpv.Show();
                }
                else
                {
                    MessageBox.Show("проходи темы по порядку :)");
                }
            }
        }

        private void GetResults_Click(object sender, RoutedEventArgs e)
        {
            StudentResult sr = new StudentResult(stm, course);
            sr.Show();
            this.Close();
        }
    }
}

