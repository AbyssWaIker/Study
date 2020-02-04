using Study.Logic;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Study.Views
{
    /// <summary>
    /// Логика взаимодействия для AddTopicuserControl.xaml
    /// </summary>
    public partial class AddTopicUserControl : UserControl
    {
        public AddTopicUserControl()
        {
            InitializeComponent();

            //получаем и выводим информацию для изменения
            TopicNameValue.Text = LearningMaterialInsert.CurrentTopic.topicName;
            AddedPortions.ItemsSource = LearningMaterialInsert.PortionList;
            AddedQuestions.ItemsSource = LearningMaterialInsert.QuestionList;
        }

        private void AddTopicPortion_Button_Click(object sender, RoutedEventArgs e)
        {
            //сообщаем программе, что раздел новый (true) и передаем ей пустой раздел для заполнения
            LearningMaterialInsert.LoadTopicPortion(true, new TopicPortionModel());

            //открываем экран создания раздела
            CommandBinding openPortionScreen = new CommandBinding(StartWindowShell.LoadTeacherAddTopicPortionScreen);
            openPortionScreen.Command.Execute("Placeholder object");
        }

        private void ChangeTopicPortion_Click(object sender, RoutedEventArgs e)
        {
            //проверям выбран ли какой-то раздел, при нажатии кнопки "изменить выбранный раздел"
            TopicPortionModel currentPortion = (TopicPortionModel)AddedPortions.SelectedItem;
            if (currentPortion != null)
            {
                //сообщаем программе, что раздел не новый (false) и передаем ей раздел для изменения
                LearningMaterialInsert.LoadTopicPortion(false, currentPortion);

                //открываем экран изменения раздела
                CommandBinding openPortionScreen = new CommandBinding(StartWindowShell.LoadTeacherAddTopicPortionScreen);
                openPortionScreen.Command.Execute("Placeholder object");
            }
        }
        private void DeleteSelectedTopicButton_Click(object sender, RoutedEventArgs e)
        {
            //еще раз проверяем выбран ли какой-то вопрос перед удалением
            TopicPortionModel topicPortionToDelete = (TopicPortionModel)AddedPortions.SelectedItem;
            if (topicPortionToDelete != null)
            {
                //уточняем у пользователя, точно ли он хочет удалить выбранный раздел
                if (MessageBox.Show("Вы точно хотите удалить этот раздел?", "Вы уверенны?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    //Заносим в систему данные о том, что этот раздел надо удалить (но не вносим изменения в базу данных, пока пользователь не нажмет кнопку "сохранить изменения")
                    LearningMaterialInsert.DeleteTopicPortionWithoutSavingChanges(topicPortionToDelete);
                }
            }
        }


        private void AddQuestion_Button_Click(object sender, RoutedEventArgs e)
        {
            //сообщаем программе, что вопрос новый (true) и передаем ей новый вопрос для заполнения
            LearningMaterialInsert.LoadTopicQuestion(true, new QuestionModel());

            //открываем экран создания вопроса
            CommandBinding openQuestionScreen = new CommandBinding(StartWindowShell.LoadTeacherAddTopicQuestionScreen);
            openQuestionScreen.Command.Execute("Placeholder object");
        }

        private void ChangeSelectedQuestion_Click(object sender, RoutedEventArgs e)
        {
            //проверям выбран ли какой-то вопрос, при нажатии кнопки "изменить выбранный вопрос"
            QuestionModel currentQuestion = (QuestionModel)AddedQuestions.SelectedItem;
            if (currentQuestion != null)
            {
                //сообщаем программе, что вопрос не новый (false) и передаем ей вопрос для изменения
                LearningMaterialInsert.LoadTopicQuestion(false, currentQuestion);

                //открываем экран создания вопроса
                CommandBinding openQuestionScreen = new CommandBinding(StartWindowShell.LoadTeacherAddTopicQuestionScreen);
                openQuestionScreen.Command.Execute("Placeholder object");
            }
        }

        private void DeleteSelectedQuestion_Button_Click(object sender, RoutedEventArgs e)
        {
            //еще раз проверяем выбран ли какой-то раздел.
            QuestionModel questionToDelete = (QuestionModel)AddedQuestions.SelectedItem;
            if (questionToDelete != null)
            {
                //уточняем у пользователя, точно ли он хочет удалить выбранный вопрос
                if (MessageBox.Show("Вы точно хотите удалить этот вопрос?", "Вы уверенны?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    //Заносим в систему данные о том, что этот вопрос надо удалить (но не вносим изменения в базу данных, пока пользователь не нажмет кнопку "сохранить изменения")
                    LearningMaterialInsert.DeleteQuestionWithoutSavingChanges(questionToDelete);
                }
            }
        }

        private void AddedPortions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //проверяем выбран ли какой-то раздел. Если да, то включаем кнопки "удалить выбранный раздел" и "изменить выбранный раздел" (и наоборот)
            TopicPortionModel tp = (TopicPortionModel)AddedPortions.SelectedItem;
            if (tp != null)
            {
                ChangeSelectedTopicButton.IsEnabled = true;
                DeleteSelectedTopicButton.IsEnabled = true;
            }
            else
            {
                ChangeSelectedTopicButton.IsEnabled = false;
                DeleteSelectedTopicButton.IsEnabled = false;
            }
        }

        private void AddedQuestions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //проверяем выбран ли какой-то вопрос. Если да, то включаем кнопки "удалить выбранный вопрос" и "изменить выбранный вопрос" (и наоборот)
            QuestionModel q = (QuestionModel)AddedQuestions.SelectedItem;
            if (q != null)
            {
                ChangeSelectedQuestion.IsEnabled = true;
                DeleteSelectedQuestion_Button.IsEnabled = true;
            }
            else
            {
                ChangeSelectedQuestion.IsEnabled = false;
                DeleteSelectedQuestion_Button.IsEnabled = false;
            }
        }


        private void AddTopicButton_Click(object sender, RoutedEventArgs e)
        {
            //после нажития на кнопку "сохранить тему" проверяем есть ли в ней информация
            if (!string.IsNullOrWhiteSpace(TopicNameValue.Text)  && AddedPortions.Items.Count != 0 && AddedQuestions.Items.Count != 0)
            {
                //сохраняем изменения в базе данных и возвращаемся на экран курса (просмотра списка тем)
                LearningMaterialInsert.SaveTopic(TopicNameValue.Text);

                //закрываем этот экран, возвращаясь к списку тем
                CommandBinding openCourse = new CommandBinding(StartWindowShell.LoadTeacherTopicListScreen);
                openCourse.Command.Execute("placeholder object");
            }
            //при остутсвии инофрмации выводим ошибку
            else MessageBox.Show("Добавьте данные темы", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

        }

        private void TopicNameValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            LearningMaterialInsert.CurrentTopic.topicName = TopicNameValue.Text;
        }
    }
}
