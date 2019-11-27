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
        public ProffessorResults()
        {
            InitializeComponent();
            List<StudentModel> sl = GlobalConfig.connection.getStudents_All();
            Students.ItemsSource = sl;

        }

        private void Details_Click(object sender, RoutedEventArgs e)
        {
            StudentModel s = (StudentModel)Students.SelectedItem;
            if (s != null)
            {
                StudentResult sr = new StudentResult(s);
                sr.Show();
            }
        }
    }
}
