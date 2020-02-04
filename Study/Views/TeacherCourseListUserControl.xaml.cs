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
    /// Логика взаимодействия для TeacherCourseListUserControl.xaml
    /// </summary>
    public partial class TeacherCourseListUserControl : UserControl
    {
        public TeacherCourseListUserControl()
        {
            InitializeComponent();

            AllCourses.ItemsSource = UsersDataControl.currentTeacher.courses;
            AllCourses.DisplayMemberPath = "Name";

            bool accces = Properties.Settings.Default.FreeAccess;
            if (accces)
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
            if (course != null)
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
                LearningMaterialInsert.EditCourse(course);


                CommandBinding openCourse = new CommandBinding(StartWindowShell.LoadTeacherTopicListScreen);
                openCourse.Command.Execute("placeholder object");
            }
        }

        private void AddCourse_Click(object sender, RoutedEventArgs e)
        {
            LearningMaterialInsert.CreateNewCourse();


            CommandBinding openCourse = new CommandBinding(StartWindowShell.LoadTeacherTopicListScreen);
            openCourse.Command.Execute("placeholder object");
        }

        private void DeleteCourseButton_Click(object sender, RoutedEventArgs e)
        {
            CourseModel course = (CourseModel)AllCourses.SelectedItem;
            if (course != null)
            {
                if (MessageBox.Show("Вы точно хотите удалить этот курс?", "Вы уверенны?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    LearningMaterialInsert.deleteCourse(course);
                }
            }
        }

        private void ChangeAcces_Click(object sender, RoutedEventArgs e)
        {
            bool access = Properties.Settings.Default.FreeAccess;
            if (access)
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

