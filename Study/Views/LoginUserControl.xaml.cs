using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Study.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginUserControl.xaml
    /// </summary>
    public partial class LoginUserControl : UserControl
    {
        public LoginUserControl()
        {
            InitializeComponent();
        }

        private void StartLearning_Click(object sender, RoutedEventArgs e)
        {
            //проверка данных, которые ввел студент
            int result = Logic.UsersDataControl.Login(UserName.Text, Password.Password);

            switch (result)
            {
                //в случае, если в базе данных пусто, при вводе admin в поле логина и пароля, открывается экран регистрации учителя
                case 0:
                    CommandBinding TeacherRegistration = new CommandBinding(StartWindowShell.LoadTeacherRegistrationScreen);
                    TeacherRegistration.Command.Execute("placeholder object");
                    break;


                //в случае, если пользователь ввел верный логин и пароль (студента), открывается экран курсов студента
                case 1:
                    CommandBinding openStudentCoursesList = new CommandBinding(StartWindowShell.LoadStudentCourseListScreen);
                    openStudentCoursesList.Command.Execute("placeholder object");
                    break;

                //в случае, если пользователь ввел верный логин и пароль (преподавателя), открывается экран курсов учителя
                case 2:
                    CommandBinding openTeacherCourseScreen = new CommandBinding(StartWindowShell.LoadTeacherCourseScreen);
                    openTeacherCourseScreen.Command.Execute("placeholder object");
                    break;

                //в случае, если пользователь ввел неверный логин, всплывает соответствующее сообщение
                case 3:
                    MessageBox.Show("Никнейм отсутствует в базе данных", "ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;

                //в случае, если пользователь ввел неверный пароль, всплывает соответствующее сообщение
                case 4:
                    MessageBox.Show("Введен неверный пароль", "ошибка", MessageBoxButton.OK, MessageBoxImage.Error); ;
                    break;

                //в случае, если пользователь не ввел свои данные, всплывает соответствующее сообщение
                case 5:
                    MessageBox.Show("Введите свои данные", "ошибка", MessageBoxButton.OK, MessageBoxImage.Error); ;
                    break;

            }
        }
    }
}
