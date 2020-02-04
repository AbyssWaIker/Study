using Study.Models;
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
    /// Логика взаимодействия для RegistrationUserControl.xaml
    /// </summary>
    public partial class StudentRegistrationUserControl : UserControl
    {
        public StudentRegistrationUserControl()
        {
            InitializeComponent();
            List<GroupModel> Groups = GlobalConfig.connection.GetGroups_All();
            if (Groups.Count != 0)
            {
                StudentGroup.ItemsSource = Groups;
                StudentGroup.DisplayMemberPath = "GroupName";
            }
            else
            {
                MessageBox.Show("Невозможно зарегистрироватся, потому что нет групп, к которым можно присоединиться\n " +
                    "Обратитесь к своему преподавателю, чтобы добавить свою группу в программу", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                CommandBinding returnToOldOrNew = new CommandBinding(StartWindowShell.LoadChoiceScreen);
                returnToOldOrNew.Command.Execute("placeholder object");

            }
        }

        private void StartLearning_Click(object sender, RoutedEventArgs e)
        {
            GroupModel selectedGroup = (GroupModel)StudentGroup.SelectedItem;
            int result = Logic.UsersDataControl.CreateStudent(StudentName.Text, selectedGroup, StudentUserName.Text, StudentPassword.Password, ConfirmPassword.Password);

            switch (result)
            {
                case 0:
                    CommandBinding openStudentCoursesScreen = new CommandBinding(StartWindowShell.LoadStudentCourseListScreen);
                    openStudentCoursesScreen.Command.Execute("placeholder object");
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
