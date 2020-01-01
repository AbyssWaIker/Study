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
    /// Логика взаимодействия для StudentResult.xaml
    /// </summary>
    public partial class StudentResult : Window
    {
        StudentModel student;
        public StudentResult(StudentModel st)
        {
            InitializeComponent();
            StudentsNameValue.Text = st.StudentName;
            StudentsGroupValue.Text = st.StudentGroup;
            GradesResults.ItemsSource = st.grades;
            student = st;
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            List<string> text = new List<string>();
            text.Add("\n****************************************************************************\n" +
                     "****************************************************************************\n");
            text.Add($"Результаты студента: {StudentsNameValue.Text} \n" +
                $"Из группы {StudentsGroupValue.Text} \n\n");
            foreach(GradeModel g in student.grades)
            {
                text.Add($"\n{g.getGradeInfoForStudent()}\n");
            }

            System.IO.File.WriteAllLines(@".\Student.txt", text);
            MessageBox.Show("Информация сохранена");
        }
    }
}
