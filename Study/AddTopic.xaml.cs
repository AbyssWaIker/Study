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
    /// Логика взаимодействия для AddTopic.xaml
    /// </summary>
    public partial class AddTopic : Window, ITopicPortionRequest, ITopicQuestionRequestor, IChangedTopicPortinonRequestor, IChangeQuestion
    {
        //переменная для хранения оригинала раздела, который мы изменим в отдельном окне
        private TopicPortionModel topicPortionForChange;
        //переменная для хранения оригинала вопроса, который мы изменим в отдельном окне
        private QuestionModel questionForChange;
        //переменная для адреса предудыщего окна. В него, после создания/изменения темы, отправляется сигнал на обновление списка 
        private ITopicRequestor caller;

        //переменная для хранения информации - мы вводим новую тему или изменяем старую?
        bool newTopic;

        //переменная для хранения оригинала изменяемой темы
        //TopicModel TopicForChange;

        //конструктор для создания новой темы 
        public AddTopic(ITopicRequestor callingForm)
        {
            InitializeComponent();
            //сохраняем адресс предыдущего окна адрес
            caller = callingForm;

            //это новая тема
            newTopic = true;
        }

        /// <summary>
        /// так как при использовании itemsSourse нельзя напрмую убирать/добавлять элементы в выводимый список, создаем дублирующие списки, которые будут нашим itemSource
        /// </summary>
        List<TopicPortionModel> PortionList = new List<TopicPortionModel>();
        List<QuestionModel> QuestionList = new List<QuestionModel>();

        /// <summary>
        /// id изменяемой темы
        /// </summary>
        int id;

        //конструктор для изменения старой темы 
        public AddTopic(ITopicRequestor callingForm, TopicModel tm)
        {
            InitializeComponent();
            //сохраняем адресс предыдущего окна адрес
            caller = callingForm;
            //это изменение старой темы
            newTopic = false;

            //получаем и выводим информацию для изменения
            TopicNameValue.Text = tm.topicName;
            id = tm.id;

            PortionList = GlobalConfig.connection.GetTopicPortions_bytopic(tm.id);
            AddedPortions.ItemsSource = PortionList;

            QuestionList = GlobalConfig.connection.GetQuestions_byTopic(tm.id);
            AddedQuestions.ItemsSource = QuestionList;

            // :)
            this.Title = "Изменить тему";
            AddTopicButton.Content = "Изменить тему";
        }

        private void AddTopicPortion_Button_Click(object sender, RoutedEventArgs e)
        {
            //открываем окно создания раздела, передавая ему адресс этого окна
            AddTopicPortion atp = new AddTopicPortion(this);
            atp.Show();
        }

        public void TopicPortionComplete(TopicPortionModel model)
        {
            //добавляем завершенный раздел в список
            PortionList.Add(model);


            AddedPortions.ItemsSource = null;
            AddedPortions.ItemsSource = PortionList;
        }

        private void AddQuestion_Button_Click(object sender, RoutedEventArgs e)
        {
            //открываем окно создания вопроса, передавая ему адресс этого окна
            AddTopicQuestion atq = new AddTopicQuestion(this);
            atq.Show();
        }

        public void QuestionCreated(QuestionModel q)
        {
            //добавляем завершенный вопрос в список
            QuestionList.Add(q);

            //обновляем список
            AddedQuestions.ItemsSource = null;
            AddedQuestions.ItemsSource = QuestionList;
        }

        private void ChangeTopicPortion_Click(object sender, RoutedEventArgs e)
        {
            //проверям выбран ли какой-то раздел, при нажатии кнопки "изменить выбранный раздел"
            topicPortionForChange = (TopicPortionModel)AddedPortions.SelectedItem;
            if (topicPortionForChange != null)
            {
                //открываем окно изменения раздела, передавая ему адресс этого окна
                ChangeTopicPortion ctp = new ChangeTopicPortion(topicPortionForChange, this);
                ctp.Show();
            }
        }
        public void TopicPortionChanged(TopicPortionModel model)
        {
            //вставялем измененный раздел на место старого
            int ind = PortionList.IndexOf(topicPortionForChange);

            //меняем элемент в списке
            PortionList.Remove(topicPortionForChange);
            PortionList.Insert(ind, model);

            //обновляем отображаемый списке
            AddedPortions.ItemsSource = null;
            AddedPortions.ItemsSource = PortionList;
        }


        private void ChangeSelectedQuestion_Click(object sender, RoutedEventArgs e)
        {
            //проверям выбран ли какой-то вопрос, при нажатии кнопки "изменить выбранный вопрос"
            questionForChange = (QuestionModel)AddedQuestions.SelectedItem;
            if (questionForChange != null)
            {
                //открываем окно изменения вопроса, передавая ему адресс этого окна
                ChangeQuestion cq = new ChangeQuestion(questionForChange, this);
                cq.Show();
            }
        }
        public void QuestionChanged(QuestionModel model)
        {
            //вставялем измененный вопрос на место старого
            int ind = QuestionList.IndexOf(questionForChange);

            //меняем элемент в списке
            QuestionList.Remove(questionForChange);
            QuestionList.Insert(ind, model);

            //обновляем отображаемый списке
            AddedQuestions.ItemsSource = null;
            AddedQuestions.ItemsSource = QuestionList;
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

        //список идентификаторов вопросов, которые удалятся после нажатие кнопки "изменить тему"
        List<int> TopicportionsToDelete = new List<int>();
        private void DeleteSelectedTopicButton_Click(object sender, RoutedEventArgs e)
        {
            //еще раз проверяем выбран ли какой-то вопрос перед удалением
            TopicPortionModel tp = (TopicPortionModel)AddedPortions.SelectedItem;
            if (tp != null)
            {
                //уточняем у пользователя, точно ли он хочет удалить выбранный раздел
                if (MessageBox.Show("Вы точно хотите удалить этот раздел?", "Вы уверенны?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (newTopic)
                    {
                        //убираем раздел из списка
                        PortionList.Remove(tp);

                        //обновляем отображаемый список
                        AddedPortions.ItemsSource = null;
                        AddedPortions.ItemsSource = PortionList;
                    }
                    else
                    {
                        //если это не новая тема, то еще доавляем идентификатор вопроса в список на удаление
                        TopicportionsToDelete.Add(tp.id);

                        //убираем раздел из списка
                        PortionList.Remove(tp);

                        //обновляем отображаемый список
                        AddedPortions.ItemsSource = null;
                        AddedPortions.ItemsSource = PortionList;
                    }

                }
            }
        }

        //список идентификаторов разделов, которые удалятся после нажатие кнопки "изменить тему"
        List<int> QuestionsToDelete = new List<int>();
        private void DeleteSelectedQuestion_Button_Click(object sender, RoutedEventArgs e)
        {
            //еще раз проверяем выбран ли какой-то раздел.
            QuestionModel q = (QuestionModel)AddedQuestions.SelectedItem;
            if (q != null)
            {
                //уточняем у пользователя, точно ли он хочет удалить выбранный вопрос
                if (MessageBox.Show("Вы точно хотите удалить этот вопрос?", "Вы уверенны?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (newTopic)
                    {
                        //убираем вопрос из списка
                        QuestionList.Remove(q);
                        //обновляем отображаемый список
                        AddedQuestions.ItemsSource = null;
                        AddedQuestions.ItemsSource = QuestionList;
                    }
                    else
                    {
                        //если это не новая тема, то еще доавляем идентификатор вопроса в список на удаление
                        QuestionsToDelete.Add(q.id);

                        //убираем вопрос из списка
                        QuestionList.Remove(q);
                        //обновляем отображаемый список
                        AddedQuestions.ItemsSource = null;
                        AddedQuestions.ItemsSource = QuestionList;
                    }

                }
            }
        }

        private void AddTopicButton_Click(object sender, RoutedEventArgs e)
        {
            //после нажития на кнопку "сохранить тему" проверяем есть ли в ней информация
            if (TopicNameValue.Text != "" && AddedPortions.Items.Count != 0 && AddedQuestions.Items.Count != 0)
            {
                //сохраняем всю информацию по теме в отдельный класс
                TopicModel tm = new TopicModel();
                tm.topicName = TopicNameValue.Text;
                tm.TopicPortions = PortionList;
                tm.Questions = QuestionList;
                tm.id = id;
                if (newTopic)
                {
                    //если это новый класс то ставим его по порядку на номер (всего тем + 1)
                    int order;
                    {
                        List<TopicModel> tml = GlobalConfig.connection.GetTopicModels_All();
                        order = tml.Count + 1;
                    }//фигурные скобки нужны, чтобы List<TopicModel> tml не засорял память и удалился из нее как только мы узнаем количество и он станет не нужынм  
                    tm.TopicOrderNumber = order;

                    //и создаем новые записи в базе данных
                    GlobalConfig.connection.createTopic(tm);
                    foreach (TopicPortionModel tpm in tm.TopicPortions)
                    {
                        tpm.TopicId = tm.id;
                        GlobalConfig.connection.createTopicPortionModel(tpm);
                    }
                    foreach (QuestionModel qm in tm.Questions)
                    {
                        qm.Topicid = tm.id;
                        GlobalConfig.connection.createQuestionModel(qm);
                    }

                    //сообщаем предыдущему окну о создании новой темы и закрываем это
                    caller.Topiccomplete();
                    this.Close();
                }
                else
                {
                    //проверяем есть ли вопросы для удаления и, при наличии, стираем их из базы
                    if (QuestionsToDelete.Count != 0)
                    {
                        foreach (int id in QuestionsToDelete)
                        {
                            GlobalConfig.connection.deleteQuestion(id);
                        }
                    }
                    //проверяем есть ли разделы для удаления и, при наличии, стираем их из базы
                    if (TopicportionsToDelete.Count != 0)
                    {
                        foreach (int id in TopicportionsToDelete)
                        {
                            GlobalConfig.connection.deleteTopicPortion(id);
                        }
                    }

                    //обновляем информацию о теме
                    GlobalConfig.connection.updateTopicName(tm);

                    //обновляем старые и добавляем новые разделы
                    foreach (TopicPortionModel tpm in tm.TopicPortions)
                    {
                        if (tpm.id != 0) //обновляем старые
                        {
                            GlobalConfig.connection.updateTopicPortion(tpm);
                        }
                        else //добавляем новые
                        {
                            tpm.TopicId = tm.id;
                            GlobalConfig.connection.createTopicPortionModel(tpm);
                        }
                    }

                    //обновляем старые и добавляем новые вопросы
                    foreach (QuestionModel qm in tm.Questions)
                    {
                        qm.Topicid = tm.id;
                        if (tm.id != 0)//обновляем старые
                        {
                            GlobalConfig.connection.updateQuestion(qm);
                        }
                        else //добавляем новые
                        {
                            qm.Topicid = tm.id;
                            GlobalConfig.connection.createQuestionModel(qm);
                        }
                    }


                    //сообщаем предыдущему окну о создании новой темы и закрываем это
                    caller.Topiccomplete();
                    this.Close();

                }
            }
            //при остутсвии инофрмации выводим ошибку
            else MessageBox.Show("Добавте данные темы", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

        }
    }
}