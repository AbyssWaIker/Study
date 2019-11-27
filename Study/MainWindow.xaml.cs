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

namespace Study
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GlobalConfig.InitializeConnection(DataBaseType.sql);
        }

        private void StudentPage_Click(object sender, RoutedEventArgs e)
        {
            Hello__student HelloStudent = new Hello__student();
            HelloStudent.Show();
            this.Close();
        }

        private void ProffesorsPage_Click(object sender, RoutedEventArgs e)
        {


            if (ProfessorPassword.Password == "prof")
            {
                Proffessor_s_start ps = new Proffessor_s_start();
                ps.Show();
                this.Close();
            }
            else if (ProfessorPassword.Password == "")
            { MessageBox.Show("Введите пароль", "ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
            else
            {
                MessageBox.Show("Вы не преподаватель", "ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                ProfessorPassword.Password = "";
            }

        }
    }
}
