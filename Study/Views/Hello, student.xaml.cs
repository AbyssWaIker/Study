using Study.Views;
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
            if (StudentName.Text != "" && StudentGroup.Text != "" && StudentUserName.Text != "" && StudentPassword.Text != "" && ConfirmPassword.Text != "" )
            {
                if(StudentPassword.Text == ConfirmPassword.Text)
                {
                    bool free = GlobalConfig.connection.CheckifUsernameIsFree(StudentUserName.Text);

                    if (free)
                    {
                        StudentModel st = new StudentModel(StudentName.Text, StudentGroup.Text, StudentUserName.Text, StudentPassword.Text);
                        GlobalConfig.connection.createStudent(st);

                        StudentCoursesList tl = new StudentCoursesList(st);
                        tl.Show();
                        this.Close();
                    }
                    else { MessageBox.Show("Никнейм уже занят"); }
                }
                else { MessageBox.Show("Пароли не совпадают", "ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }

            }
            else { MessageBox.Show("Введите свои данные", "ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
    }
}
