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
        bool newPortion;
        TopicPortionModel tpm = new TopicPortionModel();

        /// <summary>
        /// Конструктор для создания нового раздела
        /// </summary>
        ITopicPortionRequest caller;
        public AddTopicPortion(ITopicPortionRequest callingForm)
        {
            InitializeComponent();
            caller = callingForm;
            newPortion = true;
        }

        /// <summary>
        /// конструктор для изменения старого раздела
        /// </summary>
        IChangedTopicPortinonRequestor callerforchange;
        public AddTopicPortion(TopicPortionModel model, IChangedTopicPortinonRequestor callingForm)
        {
            InitializeComponent();
            TopicportionNameValue.Text = model.TopicPortionName;
            TopicportionTextValue.Text = model.TopicPortionText;
            tpm = model;
            callerforchange = callingForm;
            newPortion = false;
        }



        private void AddTopicPortionButton_Click(object sender, RoutedEventArgs e)
        {
            if (TopicportionNameValue.Text != "" && TopicportionTextValue.Text != "")
            {
                tpm.TopicPortionName = TopicportionNameValue.Text;
                tpm.TopicPortionText = TopicportionTextValue.Text;
                if (newPortion) {caller.TopicPortionComplete(tpm); }  //создаем новый раздел
                else { callerforchange.TopicPortionChanged(tpm); } //или изменяем старый

                this.Close();
            }
            else { MessageBox.Show("Введите название и/или текст раздела", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
    }
}
