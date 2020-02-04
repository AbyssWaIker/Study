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
    public static class LearningMaterialInsert
    {

        public static bool isCourseNew { get; set; }

        //переменная для хранения информации - мы вводим новую тему или изменяем старую?
        public static bool newTopic { get; set; }

        public static bool newQuestion { get; set; }
        public static bool newPortion { get; set; }

        //переменная для хранения оригинала раздела, который мы изменим в отдельном окне
        public static TopicPortionModel topicPortionForChange { get; set; }

        //переменная для хранения оригинала вопроса, который мы изменим в отдельном окне
        public static QuestionModel questionForChange { get; set; }

        public static TopicModel CurrentTopic { get; set; }
        public static ObservableCollection<TopicPortionModel> PortionList { get; set; } = new ObservableCollection<TopicPortionModel>();
        public static ObservableCollection<QuestionModel> QuestionList { get; set; } = new ObservableCollection<QuestionModel>();

        public static ObservableCollection<String> WrongAnswerText { get; set; } = new ObservableCollection<String>();


        public static WrongAnswerModel WrongAnswer { get; set; } = new WrongAnswerModel();


        //список идентификаторов вопросов, которые удалятся после нажатие кнопки "изменить тему"
        public static List<int> QuestionsToDelete { get; set; } = new List<int>();

        //список идентификаторов вопросов, которые удалятся после нажатие кнопки "изменить тему"
        public static List<int> TopicportionsToDelete { get; set; } = new List<int>();

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

        public static void LoadTopic(bool isTopicNew, TopicModel model)
        {
            CurrentTopic = model;
            newTopic = isTopicNew;
            if (!isTopicNew)
            {
                PortionList = new ObservableCollection<TopicPortionModel>(GlobalConfig.connection.GetTopicPortions_bytopic(CurrentTopic.id));
                QuestionList = new ObservableCollection<QuestionModel>(GlobalConfig.connection.GetQuestions_byTopic(CurrentTopic.id));
            }
        }
        public static void LoadTopicPortion(bool isPortionNew, TopicPortionModel model)
        {
            newPortion = isPortionNew;
            topicPortionForChange = model;
        }
        public static void LoadTopicQuestion(bool isQuestionNew, QuestionModel model)
        {
            newQuestion = isQuestionNew;
            questionForChange = model;
        }

        public static void CreateNewCourse()
        {
            CourseModel course = new CourseModel(UsersDataControl.currentTeacher.id);
            GlobalConfig.connection.createCourse(course);
            UsersDataControl.setCurrentCourse(course);
            isCourseNew = true;

            List<GroupModel> groups = GlobalConfig.connection.GetGroups_All();
            foreach(GroupModel group in groups)
            {
                GroupToCourseRealationModel model = new GroupToCourseRealationModel(UsersDataControl.CurrentCourse.id, group.id);
                GlobalConfig.connection.CreateGroupToCourseRealation(model);
            }

            //на всякий случай очищаем список 
            UsersDataControl.CurrentCourse.topics = new List<TopicModel>();

        }

        public static void EditCourse(CourseModel course)
        {
            UsersDataControl.setCurrentCourse(course);
            isCourseNew = false;

            //получаем список тем
            List<TopicModel> tml = GlobalConfig.connection.GetTopicModels_byCourseID(UsersDataControl.CurrentCourse.id);

            //сортируем список тем по порядку
            UsersDataControl.CurrentCourse.topics = tml.OrderBy(x => x.TopicOrderNumber).ToList();
        }
        public static void FinishCourse(string newCourseName)
        {
            //Обновляем имя курса
            UsersDataControl.CurrentCourse.Name = newCourseName;
            //Обновляем имя курса в базе данных
            GlobalConfig.connection.updateCourseName(UsersDataControl.CurrentCourse);

            if (isCourseNew) //Если это новый курс, то
            {
                //добавляем его в список курсов учителя
                UsersDataControl.currentTeacher.courses.Add(UsersDataControl.CurrentCourse);
            }

            UsersDataControl.CurrentCourse = null;
        }

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

            UsersDataControl.CurrentCourse.topics = GlobalConfig.connection.GetTopicModels_byCourseID(UsersDataControl.CurrentCourse.id);
        }

        //добавляем новую тему в базу данных
        public static void AddNewTopic(TopicModel topic)
        {
            topic.Courseid = UsersDataControl.CurrentCourse.id;
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

        //меняем данные темы в базе данных и удаляем из нее лишние разделы и вопросы
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

        //Заносим в систему данные о том, что этот вопрос надо удалить (но не вносим изменения в базу данных, пока пользователь не нажмет кнопку "сохранить изменения")
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

        //Заносим в систему данные о том, что этот раздел надо удалить (но не вносим изменения в базу данных, пока пользователь не нажмет кнопку "сохранить изменения")
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



        public static void DeleteTopic(TopicModel topic)
        {
            GlobalConfig.connection.deleteGradeWithTopic(topic.id);
            GlobalConfig.connection.deleteQuestionWithTopic(topic.id);
            GlobalConfig.connection.deleteTopicPortionWithTopic(topic.id);
            GlobalConfig.connection.deleteTopic(topic.id);

            //обновляем порядок
            UsersDataControl.CurrentCourse.topics.Remove(topic);
            foreach (TopicModel tm in UsersDataControl.CurrentCourse.topics)
            {
                tm.TopicOrderNumber = UsersDataControl.CurrentCourse.topics.IndexOf(tm) + 1;
                GlobalConfig.connection.UpdateTopicOrder(tm);
            }
            UsersDataControl.CurrentCourse.topics.OrderBy(x => x.TopicOrderNumber);
        }

        public static void ChangeTopicOrder(int order, TopicModel selected)
        {
            UsersDataControl.CurrentCourse.topics.Remove(selected);
            UsersDataControl.CurrentCourse.topics.Insert(order - 1, selected);
            foreach (TopicModel topic in UsersDataControl.CurrentCourse.topics)
            {
                topic.TopicOrderNumber = UsersDataControl.CurrentCourse.topics.IndexOf(topic) + 1;
                GlobalConfig.connection.UpdateTopicOrder(topic);
            }
            UsersDataControl.CurrentCourse.topics.OrderBy(x => x.TopicOrderNumber);
        }

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

        public static void addWrongAnswer(string text)
        {
            WrongAnswer = new WrongAnswerModel(text);
            questionForChange.wrongAnswers.Add(WrongAnswer);
            WrongAnswerText.Add(text);
        }

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
