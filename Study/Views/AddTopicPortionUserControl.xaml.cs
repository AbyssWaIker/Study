using Study.Logic;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Study.Views
{
    /// <summary>
    /// Логика взаимодействия для AddTopicPortionUserControl.xaml
    /// </summary>
    public partial class AddTopicPortionUserControl : UserControl
    {
        public AddTopicPortionUserControl()
        {
            InitializeComponent();

            if (!LearningMaterialInsert.newPortion)
            {
                AddOrChange.Text = "Изменить раздел";

                TopicPortionNameValue.Text = LearningMaterialInsert.topicPortionForChange.TopicPortionName;
                TopicPortionTextValue.Text = LearningMaterialInsert.topicPortionForChange.TopicPortionText;
            }
        }


        private void AddTopicPortionButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TopicPortionNameValue.Text) && !string.IsNullOrWhiteSpace(TopicPortionTextValue.Text))
            {
                LearningMaterialInsert.WriteNewInfoIntoTopicPortion(LearningMaterialInsert.topicPortionForChange, TopicPortionNameValue.Text, TopicPortionTextValue.Text);

                CommandBinding returnToTheTopicScreen = new CommandBinding(StartWindowShell.LoadTeacherAddTopicScreen);
                returnToTheTopicScreen.Command.Execute("placeholder object");
            }
            else
            {
                MessageBox.Show("Введите название и/или текст раздела", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
