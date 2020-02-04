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
            int result = Logic.UsersDataControl.CreateTeacher(tName.Text, Position.Text, UserName.Text, Password.Password, ConfirmPassword.Password);

            switch (result)
            {
                case 0:
                    CommandBinding returnToLogin = new CommandBinding(StartWindowShell.ReturnFromTeacherRegistrationScreen);
                    returnToLogin.Command.Execute(1);
                    break;
                case 1:
                    MessageBox.Show("Никнейм уже занят");
                    break;
                case 2:
                    MessageBox.Show("Пароли не совпадают", "ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                case 3:
                    MessageBox.Show("Введите свои данные", "ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }
        }
    }
}
