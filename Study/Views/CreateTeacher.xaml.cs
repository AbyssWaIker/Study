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

namespace Study.Views
{
    /// <summary>
    /// Логика взаимодействия для CreateTeacher.xaml
    /// </summary>
    public partial class CreateTeacher : Window
    {
        public CreateTeacher()
        {
            InitializeComponent();
        }

        private void AddTeacher_Click(object sender, RoutedEventArgs e)
        {
            if (tName.Text != "" && Position.Text != "" && UserName.Text != "" && Password.Text != "" && ConfirmPassword.Text != "")
            {
                if (Password.Text == ConfirmPassword.Text)
                {
                    bool free = GlobalConfig.connection.CheckifTeacherUsernameIsFree(UserName.Text);

                    if (free)
                    {
                        TeacherModel tm = new TeacherModel(tName.Text, Position.Text, UserName.Text, Password.Text);
                        GlobalConfig.connection.createTeacher(tm);
                        this.Close();
                    }
                    else 
                    { 
                        MessageBox.Show("Никнейм уже занят"); 
                    }
                }
                else 
                { 
                    MessageBox.Show("Пароли не совпадают", "ошибка", MessageBoxButton.OK, MessageBoxImage.Error); 
                }

            }
            else 
            { 
                MessageBox.Show("Введите свои данные", "ошибка", MessageBoxButton.OK, MessageBoxImage.Error); 
            }
        }
    }
}
