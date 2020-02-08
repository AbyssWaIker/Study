using Study.Models;
using Study.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Study.Logic
{
    /// <summary>
    /// Класс хранящий логику для вставки учителем учебного материала в базу данных
    /// </summary>
    public static class LearningMaterialInsert
    {
        /// <summary>
        /// Флажок показывающий, создается ли новый курс или просматривается старый
        /// </summary>
        public static bool isCourseNew { get; set; }

        /// <summary>
        /// Флажок показывающий, мы вводим новую тему или изменяем старую?
        /// </summary>
        public static bool newTopic { get; set; }

        /// <summary>
        /// Флажок показывающий, мы вводим новый вопрос темы или изменяем старый?
        /// </summary>
        public static bool newQuestion { get; set; }

        /// <summary>
        /// Флажок показывающий, мы вводим новый раздел темы или изменяем старый?
        /// </summary>
        public static bool newPortion { get; set; }

        /// <summary>
        /// переменная для хранения оригинала раздела, который мы изменим
        /// </summary>
        public static TopicPortionModel topicPortionForChange { get; set; }

        /// <summary>
        /// переменная для хранения оригинала вопроса, который мы изменим
        /// </summary>
        public static QuestionModel questionForChange { get; set; }

        /// <summary>
        /// переменная для хранения информации, по текущей теме
        /// </summary>
        public static TopicModel CurrentTopic { get; set; }

        /// <summary>
        /// Отображаемый список разделов текущей темы
        /// </summary>
        public static ObservableCollection<TopicPortionModel> PortionList { get; set; } = new ObservableCollection<TopicPortionModel>();

        /// <summary>
        /// Отображаемый список вопросов текущей темы
        /// </summary>
        public static ObservableCollection<QuestionModel> QuestionList { get; set; } = new ObservableCollection<QuestionModel>();

        /// <summary>
        /// переменная для хранения информации, по текущему вопросу
        /// </summary>
        public static WrongAnswerModel WrongAnswer { get; set; } = new WrongAnswerModel();

        /// <summary>
        /// Отображаемый список неправильных ответов текущего вопросв
        /// </summary>
        public static ObservableCollection<String> WrongAnswerText { get; set; } = new ObservableCollection<String>();

        /// <summary>
        /// Модель хранящая информацию, про изменяемый курс
        /// </summary>
        public static CourseModel currentCourseForTeacher { get; set; }


        /// <summary>
        /// список идентификаторов вопросов, которые удалятся после нажатие кнопки "сохранить изменения"
        /// </summary>
        public static List<int> QuestionsToDelete { get; set; } = new List<int>();

        /// <summary>
        /// список идентификаторов вопросов, которые удалятся после нажатие кнопки "сохранить изменения"
        /// </summary>
        public static List<int> TopicportionsToDelete { get; set; } = new List<int>();

        /// <summary>
        /// Метод изменяющий или создающий раздел темы
        /// </summary>
        /// <param name="topicPortion">раздел</param>
        /// <param name="name">имя раздела</param>
        /// <param name="text">текст раздела</param>
        public static void WriteNewInfoIntoTopicPortion(TopicPortionModel topicPortion, string name, string text)
        {
            topicPortion.TopicPortionName = name;
            topicPortion.TopicPortionText = text;

            if (newPortion)
            {
                //добавляем завершенный раздел в список
                PortionList.Add(topicPortionForChange);
            }


        }

        /// <summary>
        /// Метод изменяющий или создающий вопрос темы
        /// </summary>
        /// <param name="text">текст вопроса</param>
        /// <param name="correctAnswer">Правильный ответ</param>
        /// <param name="timeToAnswer">Время на ответ</param>
        public static void WriteNewInfoIntoQuestion(string text, string correctAnswer, int timeToAnswer)
        {
            questionForChange.QuestionText = text;
            questionForChange.CorrectAnswer = correctAnswer;

            questionForChange.wrongAnswers.Clear();

            foreach (String wrongAnswer in WrongAnswerText)
            {
                questionForChange.wrongAnswers.Add(new WrongAnswerModel(questionForChange.id, wrongAnswer));
            }
            questionForChange.timeToAnswer = timeToAnswer;

            //если это новый вопрос
            if (newQuestion)
            { //добавляем вопрос в список
                QuestionList.Add(questionForChange);
            }

        }

        /// <summary>
        /// Метод загружающий информацию о теме для отображения
        /// </summary>
        /// <param name="isTopicNew">переменная показываеющая создаем ли мы новую тему или изменяем старую</param>
        /// <param name="model">тема</param>
        public static void LoadTopic(bool isTopicNew, TopicModel model)
        {
            CurrentTopic = model;
            newTopic = isTopicNew;
            if (!isTopicNew)
            {
                PortionList = new ObservableCollection<TopicPortionModel>(GlobalConfig.connection.GetTopicPortions_bytopic(CurrentTopic.id));
                QuestionList = new ObservableCollection<QuestionModel>(GlobalConfig.connection.GetQuestions_byTopic(CurrentTopic.id));
            }
            else
            {
                PortionList = new ObservableCollection<TopicPortionModel>();
                QuestionList = new ObservableCollection<QuestionModel>();
            }
        }

        /// <summary>
        /// Метод загружающий информацию о разделе темы для отображения
        /// </summary>
        /// <param name="isPortionNew">переменная показываеющая создаем ли мы новый раздел или изменяем старый</param>
        /// <param name="model">раздел</param>
        public static void LoadTopicPortion(bool isPortionNew, TopicPortionModel model)
        {
            newPortion = isPortionNew;
            topicPortionForChange = model;
        }
        /// <summary>
        /// Метод загружающий информацию о вопросе темы для отображения
        /// </summary>
        /// <param name="isQuestionNew">переменная показываеющая создаем ли мы новый вопрос или изменяем старый</param>
        /// <param name="model">вопрос</param>
        public static void LoadTopicQuestion(bool isQuestionNew, QuestionModel model)
        {
            newQuestion = isQuestionNew;
            questionForChange = model;
        }

        /// <summary>
        /// Метод создающий новый курс для отображения
        /// </summary>
        public static void CreateNewCourse()
        {
            CourseModel course = new CourseModel(UsersDataControl.currentTeacher.id);
            GlobalConfig.connection.createCourse(course);
            currentCourseForTeacher = course;
            isCourseNew = true;

            List<GroupModel> groups = GlobalConfig.connection.GetGroups_All();
            foreach(GroupModel group in groups)
            {
                GroupToCourseRealationModel model = new GroupToCourseRealationModel(currentCourseForTeacher.id, group.id);
                GlobalConfig.connection.CreateGroupToCourseRealation(model);
            }

        }

        /// <summary>
        /// Метод загружающий информацию о курсе для отображения
        /// </summary>
        public static void EditCourse(CourseModel course)
        {
            currentCourseForTeacher = course;
            isCourseNew = false;

            //получаем список тем
            course.topics = GlobalConfig.connection.GetTopicModels_byCourseID(currentCourseForTeacher.id);

            //на всякий случай сортируем список тем по порядку
            currentCourseForTeacher.topics = course.topics.OrderBy(x => x.TopicOrderNumber).ToList();
        }

        /// <summary>
        /// По факту - метод присваивающий новое имя курсу
        /// </summary>
        /// <param name="newNameOfCourse">Без комментариев.</param>
        public static void FinishCourse(string newNameOfCourse)
        {
            //Обновляем имя курса
            currentCourseForTeacher.Name = newNameOfCourse;
            //Обновляем имя курса в базе данных
            GlobalConfig.connection.updateCourseName(currentCourseForTeacher);

            if (isCourseNew) //Если это новый курс, то
            {
                //добавляем его в список курсов учителя
                UsersDataControl.currentTeacher.courses.Add(currentCourseForTeacher);
            }

            currentCourseForTeacher = null;
        }

        /// <summary>
        /// Метод сохраняющий курс (По факту - метод присваивающий новое имя теме, который отдает все реальные действия другим методам, чтобы код выглядел покрасивее)
        /// </summary>
        /// <param name="TopicName"></param>
        public static void SaveTopic(string TopicName)
        {
            //сохраняем всю информацию по теме в отдельный класс
            TopicModel topic = new TopicModel(CurrentTopic.id, TopicName, PortionList.ToList(), QuestionList.ToList());

            if (newTopic)//если это новая тема, то 
            {
                //добавляем новую тему в базу данных
                AddNewTopic(topic);
            }
            else //если это старая тема то 
            {
                //меняем данные темы в базе данных и удаляем из нее лишние разделы и вопросы
                ChangeTopic(topic);
            }

            PortionList.Clear();
            QuestionList.Clear();

            currentCourseForTeacher.topics = GlobalConfig.connection.GetTopicModels_byCourseID(currentCourseForTeacher.id);
        }

        /// <summary>
        /// Метод добавляющий новую тему в базу данных
        /// </summary>
        /// <param name="topic">Новая тема</param>
        public static void AddNewTopic(TopicModel topic)
        {
            topic.Courseid = currentCourseForTeacher.id;
            //ставим его по порядку на номер (всего тем + 1)
            int NumberofTopics = GlobalConfig.connection.GetNumberofTopicsByCouriseid(topic.Courseid);
            topic.TopicOrderNumber = NumberofTopics + 1;

            //и создаем новые записи в базе данных
            GlobalConfig.connection.createTopic(topic);
            foreach (TopicPortionModel topicPortion in topic.TopicPortions)
            {
                topicPortion.TopicId = topic.id;
                GlobalConfig.connection.createTopicPortionModel(topicPortion);
            }
            foreach (QuestionModel question in topic.Questions)
            {
                question.Topicid = topic.id;
                GlobalConfig.connection.createQuestionModel(question);
                foreach (WrongAnswerModel wam in question.wrongAnswers)
                {
                    wam.QuestionId = question.id;
                    GlobalConfig.connection.createWrongAnswerModel(wam);
                }
            }
        }

        /// <summary>
        /// Метод сохраняющий изменения в теме (и удаляющий из нее лишние разделы и вопросы)
        /// </summary>
        /// <param name="topic">изменяемая тема</param>
        public static void ChangeTopic(TopicModel topic)
        {
            //проверяем есть ли вопросы для удаления и, при наличии, стираем их из базы
            if (QuestionsToDelete.Count != 0)
            {
                foreach (int id in QuestionsToDelete)
                {
                    GlobalConfig.connection.deleteQuestion(id);
                    GlobalConfig.connection.deleteWrongAnswerWithQuestion(id);
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

            //обновляем название теме
            GlobalConfig.connection.updateTopicName(topic);

            //обновляем старые и добавляем новые разделы
            foreach (TopicPortionModel topicPortion in topic.TopicPortions)
            {
                if (topicPortion.id != 0) //обновляем старые
                {
                    GlobalConfig.connection.updateTopicPortion(topicPortion);
                }
                else //добавляем новые
                {
                    topicPortion.TopicId = topic.id;
                    GlobalConfig.connection.createTopicPortionModel(topicPortion);
                }
            }

            //обновляем старые и добавляем новые вопросы
            foreach (QuestionModel question in topic.Questions)
            {
                question.Topicid = topic.id;
                if (topic.id != 0)//обновляем старые
                {
                    GlobalConfig.connection.updateQuestion(question);
                    GlobalConfig.connection.deleteQuestionWrongAnswerbyQuestionid(question.id);
                    foreach (WrongAnswerModel wam in question.wrongAnswers)
                    {
                        GlobalConfig.connection.createWrongAnswerModel(wam);
                    }
                }
                else //добавляем новые
                {
                    question.Topicid = topic.id;
                    GlobalConfig.connection.createQuestionModel(question);
                    foreach (WrongAnswerModel wrongAnswer in question.wrongAnswers)
                    {
                        wrongAnswer.QuestionId = question.id;
                        GlobalConfig.connection.createWrongAnswerModel(wrongAnswer);
                    }
                }
            }
        }

        /// <summary>
        /// Метод, заносящий в систему данные о том, какой вопрос надо удалить (но изменения в базу данных не вносятся, пока пользователь не нажмет кнопку "сохранить изменения")
        /// </summary>
        /// <param name="question">Удаляемый вопрос</param>
        public static void DeleteQuestionWithoutSavingChanges(QuestionModel question)
        {
            if (newTopic)
            {
                //убираем вопрос из списка
                QuestionList.Remove(question);
            }
            else
            {
                //если это не новая тема, то еще доавляем идентификатор вопроса в список на удаление
                QuestionsToDelete.Add(question.id);

                //убираем вопрос из списка
                QuestionList.Remove(question);
            }
        }

        /// <summary>
        /// Метод, заносящий в систему данные о том, какой раздел надо удалить (но изменения в базу данных не вносятся, пока пользователь не нажмет кнопку "сохранить изменения")
        /// </summary>
        /// <param name="topicPortion">Удаляемый раздел</param>
        public static void DeleteTopicPortionWithoutSavingChanges(TopicPortionModel topicPortion)
        {
            if (newTopic)
            {
                //убираем раздел из списка
                PortionList.Remove(topicPortion);
            }
            else
            {
                //если это не новая тема, то еще доавляем идентификатор вопроса в список на удаление
                TopicportionsToDelete.Add(topicPortion.id);

                //убираем раздел из списка
                PortionList.Remove(topicPortion);
            }
        }

        /// <summary>
        /// Метод, удаляющий тему. Реликт со времен, когда я не знал про триггеры в SQl
        /// </summary>
        /// <param name="topic">Удаляемая тема</param>
        public static void DeleteTopic(TopicModel topic)
        {
            GlobalConfig.connection.deleteGradeWithTopic(topic.id);
            GlobalConfig.connection.deleteQuestionWithTopic(topic.id);
            GlobalConfig.connection.deleteTopicPortionWithTopic(topic.id);
            GlobalConfig.connection.deleteTopic(topic.id);

            //обновляем порядок
            currentCourseForTeacher.topics.Remove(topic);
            foreach (TopicModel tm in currentCourseForTeacher.topics)
            {
                tm.TopicOrderNumber = currentCourseForTeacher.topics.IndexOf(tm) + 1;
                GlobalConfig.connection.UpdateTopicOrder(tm);
            }
            currentCourseForTeacher.topics.OrderBy(x => x.TopicOrderNumber);
        }

        /// <summary>
        /// Метод, удаляющий курс. Реликт со времен, когда я не знал про триггеры в SQl
        /// </summary>
        /// <param name="course">Удаляемый вопрос</param>
        public static void deleteCourse(CourseModel course)
        {
            course.topics = GlobalConfig.connection.GetTopicModels_byCourseID(course.id);
            foreach (TopicModel topic in course.topics)
            {
                topic.Questions = GlobalConfig.connection.GetQuestions_byTopic(topic.id);
                foreach (QuestionModel question in topic.Questions)
                {
                    GlobalConfig.connection.deleteWrongAnswerWithQuestion(question.id);
                }
                GlobalConfig.connection.deleteQuestionWithTopic(topic.id);
                GlobalConfig.connection.deleteTopicPortionWithTopic(topic.id);
                GlobalConfig.connection.deleteGradeWithTopic(topic.id);
            }
            GlobalConfig.connection.deleteTopicWithCourse(course.id);
            GlobalConfig.connection.deleteCourseToStudentRelationWithCourse(course.id);
            GlobalConfig.connection.deleteCourse(course.id);
            UsersDataControl.currentTeacher.courses.Remove(course);
        }

        /// <summary>
        /// Метод изменяющий порядок выбранной темы
        /// </summary>
        /// <param name="order">Новый номер порядка</param>
        /// <param name="selected">Выбранная тема</param>
        public static void ChangeTopicOrder(int order, TopicModel selected)
        {
            //Проверяем - вдруг тема итак имеет нужный порядок
            if (currentCourseForTeacher.topics.IndexOf(selected) != (order - 1))
            {
                //если нет, то убираем ее из списка и вставляем под нужным порядком
                currentCourseForTeacher.topics.Remove(selected);
                currentCourseForTeacher.topics.Insert(order - 1, selected);

                //после этого обновляем порядок тем курса
                foreach (TopicModel topic in currentCourseForTeacher.topics)
                {
                    topic.TopicOrderNumber = currentCourseForTeacher.topics.IndexOf(topic) + 1;
                    GlobalConfig.connection.UpdateTopicOrder(topic);
                }
                //и на всякий случай сортируем темы по порядку
                currentCourseForTeacher.topics.OrderBy(x => x.TopicOrderNumber);
            }
        }

        /// <summary>
        /// Метод вносящий введенный учителем неправльный ответ в отображаемый список
        /// </summary>
        /// <param name="text">текст ответа</param>
        public static void addWrongAnswer(string text)
        {
            WrongAnswer = new WrongAnswerModel(text);
            questionForChange.wrongAnswers.Add(WrongAnswer);
            WrongAnswerText.Add(text);
        }

        /// <summary>
        /// Метод отображающий неправильные ответы изменяемого вопроса
        /// </summary>
        public static void getWrongAnswers()
        {
            questionForChange.wrongAnswers = GlobalConfig.connection.GetWrongAnswerModels_byQuestionid(questionForChange.id);
            foreach (WrongAnswerModel wrongAnswer in questionForChange.wrongAnswers)
            {
                WrongAnswerText.Add(wrongAnswer.WrongAnswerText);
            }
        }
    }
}
