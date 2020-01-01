using Study.Views;
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
    /// Логика взаимодействия для Proffessor_s_start.xaml
    /// </summary>
    public partial class Proffessor_s_start : Window, ITopicRequestor
    {
        //обьявляем список тем
        List<TopicModel> tml1 = new List<TopicModel>();
        CourseModel cm = new CourseModel();
        ICourseRequestor ic;
        bool isTopicNew;
        public Proffessor_s_start(CourseModel c, ICourseRequestor i, bool newtopic)
        {
            InitializeComponent();

            CourseNameText.Text = c.Name;

            //получаем список тем
            List<TopicModel> tml = GlobalConfig.connection.GetTopicModels_byCourseID(c.id);

            //сортируем список тем по порядку
            tml1 = tml.OrderBy(x => x.TopicOrderNumber).ToList();

            //выводим список тем
            AllTopics.ItemsSource = tml1;

            cm = c;
            ic = i;
            isTopicNew = newtopic;

            bool access = Properties.Settings.Default.FreeAccess;
            if (access)
            {
                GiveAccess.Visibility = Visibility.Hidden;
            }
        }


        //Сортировка списка тем по порядку



        private void ChangeOrderButton_Click(object sender, RoutedEventArgs e)
        {
            //проверям выбрана ли какая-то тема, при нажатии кнопки "изменить порядок выбранной темы"
            TopicModel selected = (TopicModel)AllTopics.SelectedItem;
            if (selected != null)
            {
                int order;

                bool isNumber = int.TryParse(OrderNumberValue.Text, out order);
                //проверям не ввел ли пользователь (например свое имя) не число в поле №темы
                if (isNumber)
                {
                    //проверям не ввел ли пользователь №темы 5 миллиардов, когда тем всего 4
                    if (order <= tml1.Count)
                    {
                        tml1.Remove(selected);
                        tml1.Insert(order - 1, selected);
                        foreach (TopicModel tm in tml1)
                        {
                            tm.TopicOrderNumber = tml1.IndexOf(tm) + 1;
                            GlobalConfig.connection.UpdateTopicOrder(tm);
                        }
                        AllTopics.ItemsSource = null;
                        AllTopics.ItemsSource = tml1;

                    }
                    else //если пользователь ввел (например) №темы 5 миллиардов, когда тем всего 4, выводим соответствующую ошибку
                    {
                        MessageBox.Show("Число больше количества тем", "ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else //если пользователь ввел (например) свое имя, в поле №темы, выводим соответствующую ошибку
                {
                    MessageBox.Show("Введите число", "ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void AddTopic_Click(object sender, RoutedEventArgs e)
        {
            //открываем окно создания тем
            AddTopic at = new AddTopic(this, cm.id);
            at.Show();
        }

        public void Topiccomplete()
        {
            // после получения сигнала о добавлении темы, обновляем список тем
            tml1 = GlobalConfig.connection.GetTopicModels_byCourseID(cm.id);
            AllTopics.ItemsSource = null;
            AllTopics.ItemsSource = tml1;
        }

        private void AllTopics_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //проверяем выбраны ли темы и если да, то включаем кнопки "удалить выбранную тему" и "изменить выбранную тему" (и наоборот)
            TopicModel s = (TopicModel)AllTopics.SelectedItem;
            if (s != null)
            {
                ChangeTopicButton.IsEnabled = true;
                DeleteTopicButton.IsEnabled = true;
            }
            else
            {
                ChangeTopicButton.IsEnabled = false;
                DeleteTopicButton.IsEnabled = false;

            }
        }

        private void DeleteTopicButton_Click(object sender, RoutedEventArgs e)
        {
            //проверяем выбрана ли какая-то тема при нажатии кнопки "удалить выбранную тему"
            TopicModel s = (TopicModel)AllTopics.SelectedItem;
            if (s != null)
            {
                if (MessageBox.Show("Вы точно хотите удалить эту тему?", "Вы уверенны?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    //если пользователь подтверждает, то удаляем
                    int id = s.getID();
                    GlobalConfig.connection.deleteGradeWithTopic(id);
                    GlobalConfig.connection.deleteQuestionWithTopic(id);
                    GlobalConfig.connection.deleteTopicPortionWithTopic(id);
                    GlobalConfig.connection.deleteTopic(id);

                    //обновляем порядок
                    tml1.Remove(s);
                    foreach (TopicModel tm in tml1)
                    {
                        tm.TopicOrderNumber = tml1.IndexOf(tm) + 1;
                        GlobalConfig.connection.UpdateTopicOrder(tm);
                    }
                    //обновляем список тем
                    tml1.Clear();
                    List<TopicModel> tml = GlobalConfig.connection.GetTopicModels_byCourseID(cm.id);

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

                    AllTopics.ItemsSource = null;
                    AllTopics.ItemsSource = tml1;
                }

            }
        }

        private void ChangeTopicButton_Click(object sender, RoutedEventArgs e)
        {
            //еще раз проверяем выбрана ли тема
            TopicModel s = (TopicModel)AllTopics.SelectedItem;
            if (s != null)
            {
                AddTopic at = new AddTopic(this, s);
                at.Show();
            }
        }

        private void FinishChecking_Click(object sender, RoutedEventArgs e)
        {
            cm.Name = CourseNameText.Text;
            if(isTopicNew)
            {
                ic.courseCreated(cm);
            }
            else
            {
                GlobalConfig.connection.updateCourseName(cm); 
                ic.courseChanged();
            }
            this.Close();
        }

        private void CheckResults_Click(object sender, RoutedEventArgs e)
        {
            ProffessorResults pr = new ProffessorResults(cm);
            pr.Show();
            this.Close();
        }

        private void GiveAccess_Click(object sender, RoutedEventArgs e)
        {
            GiveAccess ga = new GiveAccess(cm);
            ga.Show();
        }
    }
}
