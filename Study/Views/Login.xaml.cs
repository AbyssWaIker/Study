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
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void StartLearning_Click(object sender, RoutedEventArgs e)
        {
            if ( UserName.Text != "" && Password.Password != "")
            {
                bool user = !GlobalConfig.connection.CheckifUsernameIsFree(UserName.Text);
                if (user)
                {
                    bool PassWordMatch = GlobalConfig.connection.CheckPasswordByUserName(UserName.Text, Password.Password);


                    if (PassWordMatch)
                    {
                        StudentModel st = GlobalConfig.connection.GetStudentByUserName(UserName.Text);
                        StudentCoursesList cl = new StudentCoursesList(st);
                        cl.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Введен неверный пароль", "ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    bool DataBaseisEmpty = GlobalConfig.connection.checkIfThereIsaTeachersData();
                    if(DataBaseisEmpty)
                    {
                        if(UserName.Text=="admin"&&Password.Password=="admin")
                        {
                            TeacherModel t = GlobalConfig.connection.GetTeacherByUserName(UserName.Text);
                            Courses p = new Courses(t);
                            p.Show();
                            this.Close();
                        }
                    }
                    else
                    {
                        bool teacher = !GlobalConfig.connection.CheckifTeacherUsernameIsFree(UserName.Text);
                        if (teacher)
                        {
                            bool PassWordMatch = GlobalConfig.connection.CheckTeacherPasswordByUserName(UserName.Text, Password.Password);
                            if (PassWordMatch)
                            {
                                TeacherModel t = GlobalConfig.connection.GetTeacherByUserName(UserName.Text);
                                Courses p = new Courses(t);
                                p.Show();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Введен неверный пароль", "ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Никнейм отсутствует в базе данных", "ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            else 
            { 
                MessageBox.Show("Введите свои данные", "ошибка", MessageBoxButton.OK, MessageBoxImage.Error); 
            }
        }
    }
}
