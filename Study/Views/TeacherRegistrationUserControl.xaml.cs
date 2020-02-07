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
    /// Логика взаимодействия для TeacherRegistrationUserControl.xaml
    /// </summary>
    public partial class TeacherRegistrationUserControl : UserControl
    {
        public TeacherRegistrationUserControl()
        {
            InitializeComponent();
        }

        private void AddTeacher_Click(object sender, RoutedEventArgs e)
        {
            int result = UsersDataControl.CreateTeacher(tName.Text, Position.Text, UserName.Text, Password.Password, ConfirmPassword.Password);

            switch (result)
            {
                //если все введенные данные верны и регистрация прошла успешно, открывается экран входа 
                //(или стартовый экран преподавателя, если раскоменьтить присваивание только что зарегистрированного преподавателя переменной currentTeacher в методе UsersDataControl.CreateTeacher)
                case 0:
                    CommandBinding returnToLogin = new CommandBinding(StartWindowShell.ReturnFromTeacherRegistrationScreen);
                    returnToLogin.Command.Execute(1);
                    break;

                //если логин занят, всплывает соответствующее сообщение
                case 1:
                    MessageBox.Show("Логин уже занят");
                    break;

                //если пароли не совпадают, всплывает соответствующее сообщение
                case 2:
                    MessageBox.Show("Пароли не совпадают", "ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;

                //если пользователь не ввел свои данные , всплывает соответствующее сообщение
                case 3:
                    MessageBox.Show("Введите свои данные", "ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }
        }
    }
}
