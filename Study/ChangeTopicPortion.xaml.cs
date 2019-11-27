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
    /// Логика взаимодействия для ChangeTopicPortion.xaml
    /// </summary>
    public partial class ChangeTopicPortion : Window
    {
        TopicPortionModel tpm;
        IChangedTopicPortinonRequestor caller;
        public ChangeTopicPortion(TopicPortionModel model, IChangedTopicPortinonRequestor callingForm)
        {
            InitializeComponent();
            TopicportionNameValue.Text = model.TopicPortionName;
            TopicportionTextValue.Text = model.TopicPortionText;
            tpm = model;
            caller = callingForm;
        }

        private void ChangeTopicPortionButton_Click(object sender, RoutedEventArgs e)
        {
            if (TopicportionNameValue.Text != "" && TopicportionTextValue.Text != "")
            {
                tpm.TopicPortionName = TopicportionNameValue.Text;
                tpm.TopicPortionText = TopicportionTextValue.Text;
                caller.TopicPortionChanged(tpm);
                this.Close();
            }
            else { MessageBox.Show("Введите назавние и/или текст раздела", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
    }
}
