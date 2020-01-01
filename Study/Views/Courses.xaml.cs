using System;
using System.Collections.Generic;
using System.Configuration;
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
using Study;

namespace Study.Views
{
    /// <summary>
    /// Логика взаимодействия для Courses.xaml
    /// </summary>
    public partial class Courses : Window, ICourseRequestor
    {
        List<CourseModel> courses = new List<CourseModel>();
        TeacherModel tm = new TeacherModel();
        public Courses(TeacherModel t)
        {
            InitializeComponent();
            courses = GlobalConfig.connection.GetCourseModelsByTeacherID(t.id);
            AllCourses.ItemsSource = courses;
            AllCourses.DisplayMemberPath = "Name";
            tm = t;
            bool accces = Properties.Settings.Default.FreeAccess;
            if(accces)
            {
                ChangeAccess.Content = "Доступ к курсам:\n Свободный";
            }
            else
            {
                ChangeAccess.Content = "Доступ к курсам:\n По разрешению";
            }
        }

        private void AllCourses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CourseModel course = (CourseModel)AllCourses.SelectedItem;
            if(course != null)
            {
                ChangeCourseButton.IsEnabled = true;
                DeleteCourseButton.IsEnabled = true;
            }
            else
            {
                ChangeCourseButton.IsEnabled = false;
                DeleteCourseButton.IsEnabled = false;
            }
        }

        private void ChangeCourseButton_Click(object sender, RoutedEventArgs e)
        {
            CourseModel course = (CourseModel)AllCourses.SelectedItem;
            if (course != null)
            {
                bool isTopicNew = false;
                Proffessor_s_start ps = new Proffessor_s_start(course, this, isTopicNew);
                ps.Show();
            }
        }

        private void AddCourse_Click(object sender, RoutedEventArgs e)
        {
            CourseModel c = new CourseModel(tm.id);

            GlobalConfig.connection.createCourse(c);
            bool isTopicNew = true;
            Proffessor_s_start ps = new Proffessor_s_start(c, this, isTopicNew);
            ps.Show();

        }

        public void courseChanged()
        {
            AllCourses.ItemsSource = null;
            AllCourses.ItemsSource = courses;
        }

        public void courseCreated(CourseModel c)
        {
            //обновляем список
            courses.Add(c);
            AllCourses.ItemsSource = null;
            AllCourses.ItemsSource = courses;
        }

        private void DeleteCourseButton_Click(object sender, RoutedEventArgs e)
        {
            CourseModel course = (CourseModel)AllCourses.SelectedItem;
            if(course!=null)
            {
                if (MessageBox.Show("Вы точно хотите удалить этот курс?", "Вы уверенны?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    course.topics = GlobalConfig.connection.GetTopicModels_byCourseID(course.id);
                    foreach(TopicModel topic in course.topics)
                    {
                        topic.Questions = GlobalConfig.connection.GetQuestions_byTopic(topic.getID());
                        foreach(QuestionModel question in topic.Questions)
                        {
                            GlobalConfig.connection.deleteWrongAnswerWithQuestion(question.id);
                        }
                        GlobalConfig.connection.deleteQuestionWithTopic(topic.getID());
                        GlobalConfig.connection.deleteTopicPortionWithTopic(topic.getID());
                        GlobalConfig.connection.deleteGradeWithTopic(topic.getID());
                    }
                    GlobalConfig.connection.deleteTopicWithCourse(course.id);
                    GlobalConfig.connection.deleteCourseToStudentRelationWithCourse(course.id);
                    GlobalConfig.connection.deleteCourse(course.id);

                    courses.Remove(course);
                    AllCourses.ItemsSource = null;
                    AllCourses.ItemsSource = courses;

                }
            }
        }

        private void AddTeacher_Click(object sender, RoutedEventArgs e)
        {
            CreateTeacher ct = new CreateTeacher();
            ct.Show(); 
        }

        private void ChangeAcces_Click(object sender, RoutedEventArgs e)
        {
            bool access = Properties.Settings.Default.FreeAccess;
            if(access)
            {
                Properties.Settings.Default.FreeAccess = false;
                Properties.Settings.Default.Save();
                ChangeAccess.Content = "Доступ к курсам:\n По разрешению";
            }
            else
            {
                Properties.Settings.Default.FreeAccess = true;
                Properties.Settings.Default.Save();
                ChangeAccess.Content = "Доступ к курсам:\n Свободный";
            }
        }
    }
}
