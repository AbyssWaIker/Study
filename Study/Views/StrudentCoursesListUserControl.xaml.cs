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
    /// Логика взаимодействия для StrudentCoursesListUserControl.xaml
    /// </summary>
    public partial class StudentCoursesListUserControl : UserControl
    {
        public StudentCoursesListUserControl()
        {
            InitializeComponent(); 

            AllCourses.ItemsSource = Logic.DisplayedLearningMaterial.coursesAvailableTostudent();
            AllCourses.DisplayMemberPath = "Name";
        }

        private void StartСourse_Click(object sender, RoutedEventArgs e)
        {
            CourseModel course = (CourseModel)AllCourses.SelectedItem;

            if (course != null)
            {
                Logic.DisplayedLearningMaterial.courseStarted(course);

                CommandBinding openTopicList = new CommandBinding(StartWindowShell.LoadStudentTopicListScreen);
                openTopicList.Command.Execute("placeholder object");
            }
        }
    }
}
