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
    /// Логика взаимодействия для ProffessorResults.xaml
    /// </summary>
    public partial class ProffessorResults : Window
    {
        List<StudentModel> sl = new List<StudentModel>();
        public ProffessorResults(CourseModel cm)
        {
            InitializeComponent();
            sl = GlobalConfig.connection.GetStudentsByCourseid(cm.id);

            foreach (StudentModel sm in sl)
            {
                sm.grades = GlobalConfig.connection.GetGrades_byStudent(sm.id);
            }

            Results.ItemsSource = sl;

        }

        private void Details_Click(object sender, RoutedEventArgs e)
        { 
            StudentModel s = (StudentModel)Results.SelectedItem;
            if (s != null)
            {
                StudentResult sr = new StudentResult(s);
                sr.Show();
            }
        }

        private void PrintAll_Click(object sender, RoutedEventArgs e)
        {
            List<string> text = new List<string>();
            
            foreach(StudentModel sm in sl)
            {
                text.Add("\n****************************************************************************\n" +
                     "****************************************************************************\n");
                text.Add($"Результаты студента: {sm.StudentName} \n" +
                    $"Из группы {sm.StudentGroup} \n\n");
                foreach (GradeModel g in sm.grades)
                {
                    text.Add($"\n{g.getGradeInfoForStudent()}\n");
                }
            }

            System.IO.File.WriteAllLines(@".\InfoForProffessor.txt", text);
            MessageBox.Show("Информация сохранена");
        }

        private void Results_Selected(object sender, RoutedEventArgs e)
        {

        }
    }
}
