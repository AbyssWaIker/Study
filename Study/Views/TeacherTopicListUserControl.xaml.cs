using Study.Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для TeacherTopicListUserControl.xaml
    /// </summary>
    public partial class TeacherTopicListUserControl : UserControl
    {
        public TeacherTopicListUserControl()
        {
            InitializeComponent();

            CourseNameText.Text = UsersDataControl.currentCourse.Name;

            //выводим список тем
            AllTopics.ItemsSource = UsersDataControl.currentCourse.topics;
        }

        private void ChangeOrderButton_Click(object sender, RoutedEventArgs e)
        {
            //проверям выбрана ли какая-то тема, при нажатии кнопки "изменить порядок выбранной темы"
            TopicModel selected = (TopicModel)AllTopics.SelectedItem;
            if (selected != null)
            {
                bool isNumber = int.TryParse(OrderNumberValue.Text, out int order);
                //проверям не ввел ли пользователь (например свое имя) не число в поле №темы
                if (isNumber)
                {
                    //проверям не ввел ли пользователь (например) номер темы - 5 миллиардов (когда тем всего 4)
                    if (order <= UsersDataControl.currentCourse.topics.Count)
                    {
                        LearningMaterialInsert.ChangeTopicOrder(order, selected);

                        //обновляем отображение
                        AllTopics.ItemsSource = null;
                        AllTopics.ItemsSource = UsersDataControl.currentCourse.topics;

                    }
                    else //если пользователь ввел неверный номер, выводим соответствующую ошибку и сбрасываем содержимое строки
                    {
                        MessageBox.Show("Число больше количества тем", "ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        OrderNumberValue.Text = "1";
                    }
                }
                else //если пользователь ввел не число, выводим соответствующую ошибку и сбрасываем содержимое строки
                {
                    MessageBox.Show("Введите число", "ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    OrderNumberValue.Text = "1";
                }
            }
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

        private void AddTopic_Click(object sender, RoutedEventArgs e)
        {
            //сообщаем программе, что тема новая (true) и передаем ей пустую тему, для установки текущей
            LearningMaterialInsert.LoadTopic(true, new TopicModel());

            //открываем экран создания/изменения тем
            CommandBinding openTopicScreen = new CommandBinding(StartWindowShell.LoadTeacherAddTopicScreen);
            openTopicScreen.Command.Execute("Placeholder object");
        }
        private void ChangeTopicButton_Click(object sender, RoutedEventArgs e)
        {
            //еще раз проверяем выбрана ли тема
            TopicModel CurrentTopic = (TopicModel)AllTopics.SelectedItem;
            if (CurrentTopic != null)
            {
                //сообщаем программе, что тема не новая (false) и передаем ей тему, для установки текущей
                LearningMaterialInsert.LoadTopic(false, CurrentTopic);

                //открываем экран создания/изменения тем
                CommandBinding openTopicScreen = new CommandBinding(StartWindowShell.LoadTeacherAddTopicScreen);
                openTopicScreen.Command.Execute("Placeholder object");
            }
        }


        private void DeleteTopicButton_Click(object sender, RoutedEventArgs e)
        {
            //проверяем выбрана ли какая-то тема при нажатии кнопки "удалить выбранную тему"
            TopicModel selectedTopic = (TopicModel)AllTopics.SelectedItem;
            if (selectedTopic != null)
            {
                if (MessageBox.Show("Вы точно хотите удалить эту тему?", "Вы уверенны?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    //если пользователь подтверждает, то удаляем
                    LearningMaterialInsert.DeleteTopic(selectedTopic);
                }

            }
        }

        private void FinishChanges_Click(object sender, RoutedEventArgs e)
        {
            LearningMaterialInsert.FinishCourse(CourseNameText.Text);

            CommandBinding returnToCourseList = new CommandBinding(StartWindowShell.LoadTeacherCourseScreen);
            returnToCourseList.Command.Execute("placeholder object");
        }

        private void CourseNameText_TextChanged(object sender, TextChangedEventArgs e)
        {
            UsersDataControl.currentCourse.Name = CourseNameText.Text;
        }
    }
}
