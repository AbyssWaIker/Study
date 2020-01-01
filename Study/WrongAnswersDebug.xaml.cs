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
    /// Логика взаимодействия для WrongAnswersDebug.xaml
    /// </summary>
    public partial class WrongAnswersDebug : Window
    {

        List<WrongAnswerModel> wl = new List<WrongAnswerModel>();

        public WrongAnswersDebug()
        {
            InitializeComponent();

            wl.Add(new WrongAnswerModel("да"));
            wl.Add(new WrongAnswerModel("нет"));
            wl.Add(new WrongAnswerModel("наверное"));

            AddedQuestions.ItemsSource = null;
            AddedQuestions.ItemsSource = wl;
            AddedQuestions.DisplayMemberPath = "TextOfWrongAnswer";

        }

        private void WrongAnswer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (WrongAnswer.Text == "Введите неправильный ответ")
            {
                WrongAnswer.Text = "";
            }
        }

        private void AddAnswer_Click(object sender, RoutedEventArgs e)
        {
            WrongAnswerModel wm = new WrongAnswerModel(WrongAnswer.Text);
            wl.Add(wm);

            AddedQuestions.ItemsSource = null;
            AddedQuestions.ItemsSource = wl;
        }
    }
}
