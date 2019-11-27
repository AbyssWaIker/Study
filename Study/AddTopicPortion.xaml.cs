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
    /// Логика взаимодействия для AddTopicPortion.xaml
    /// </summary>
    public partial class AddTopicPortion : Window
    {
        ITopicPortionRequest caller;
        public AddTopicPortion(ITopicPortionRequest callingForm)
        {
            InitializeComponent();
            caller = callingForm;
        }


        private void AddTopicPortionButton_Click(object sender, RoutedEventArgs e)
        {
            if (TopicportionNameValue.Text != "" && TopicportionTextValue.Text != "")
            {
                TopicPortionModel tp = new TopicPortionModel();
                tp.TopicPortionName = TopicportionNameValue.Text;
                tp.TopicPortionText = TopicportionTextValue.Text;
                caller.TopicPortionComplete(tp);
                this.Close();
            }
            else { MessageBox.Show("Введите назавние и/или текст раздела", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
    }
}
