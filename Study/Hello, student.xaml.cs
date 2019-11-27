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
    /// Логика взаимодействия для Hello__student.xaml
    /// </summary>
    public partial class Hello__student : Window
    {
        public Hello__student()
        {
            InitializeComponent();
        }

        private void StartLearning_Click(object sender, RoutedEventArgs e)
        {
            bool ThereIsData = false;
            if (StudentName.Text != "" && StudentGroup.Text != "")
            {
                ThereIsData = true;
            }

            if (ThereIsData)
            {
                StudentModel st = new StudentModel();
                st.Name = StudentName.Text;
                st.Group = StudentGroup.Text;
                GlobalConfig.connection.createStudent(st);

                TopicList tl = new TopicList(st);
                tl.Show();
                this.Close();
            }
            else { MessageBox.Show("ВВедите свои данные"); }
        }
    }
}
