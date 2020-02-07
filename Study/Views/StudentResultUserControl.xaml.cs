using Study.Logic;
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
    /// Логика взаимодействия для StudentResultUserControl.xaml
    /// </summary>
    public partial class StudentResultUserControl : UserControl
    {
        public StudentResultUserControl()
        {
            InitializeComponent();

            StudentsNameValue.Text = UsersDataControl.currentStudent.StudentName;
            StudentsGroupValue.Text = GlobalConfig.connection.GetGroupName(UsersDataControl.currentStudent.StudentGroupid);
            FinishedCourseValue.Text = UsersDataControl.currentCourse.Name;
            GradesResults.ItemsSource = UsersDataControl.currentStudent.grades;
        }


        private void Print_Click(object sender, RoutedEventArgs e)
        {
            List<String> text = UsersDataControl.printResultDocumentForStudent();
            System.IO.File.WriteAllLines(@".\Results\Student.txt", text);
            MessageBox.Show("Информация сохранена");
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (UsersDataControl.currentTeacher != null)
            {
                CommandBinding openProffesorResult = new CommandBinding(StartWindowShell.LoadTeacherResultScreen);
                openProffesorResult.Command.Execute("placeholder object");
            } 
            else
            {
                CommandBinding openStudentTopicList = new CommandBinding(StartWindowShell.LoadStudentTopicListScreen);
                openStudentTopicList.Command.Execute("placeholder object");
            }
        }
    }
}
