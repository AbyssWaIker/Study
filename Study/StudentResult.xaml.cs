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
        public StudentResult(StudentModel st)
        {
            InitializeComponent();
            StudentsNameValue.Text = st.Name;
            StudentsGroupValue.Text = st.Group;
            GradesResults.ItemsSource = st.grades;
        }
    }
}
