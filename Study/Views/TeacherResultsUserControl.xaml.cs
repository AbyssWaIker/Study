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
using Study.Logic;

namespace Study.Views
{
    /// <summary>
    /// Логика взаимодействия для TeacherResultsUserControl.xaml
    /// </summary>
    public partial class TeacherResultsUserControl : UserControl
    {
        public TeacherResultsUserControl()
        {
            InitializeComponent();

            Results.ItemsSource = UsersDataControl.getStudentsWhoHaveAccessToCourse();
        }


        private void Details_Click(object sender, RoutedEventArgs e)
        {
            StudentModel s = (StudentModel)Results.SelectedItem;
            if (s != null)
            {
                UsersDataControl.currentStudent = s;

                CommandBinding showStudentResults = new CommandBinding(StartWindowShell.LoadStudentResultScreen);
                showStudentResults.Command.Execute("placeholder object");
            }
        }

        private void PrintAll_Click(object sender, RoutedEventArgs e)
        {
            List<string> text = UsersDataControl.printResultDocumentForTeacher();
            System.IO.File.WriteAllLines(@".\Results\InfoForProffessor.txt", text);
            MessageBox.Show("Информация сохранена");
        }
    }
}
