using Study.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Study.Logic
{
    /// <summary>
    /// Класс хранящий отображаемую информацию, по курсу, который просматривает студент
    /// </summary>
    public static class DisplayedLearningMaterial
    {

        /// <summary>
        /// список непройденных тем текущего курса
        /// </summary>
        public static ObservableCollection<TopicModel> UnfinishedTopics { get; set; } = new ObservableCollection<TopicModel>();

        /// <summary>
        /// список пройденных тем текущего курса
        /// </summary>
        public static ObservableCollection<TopicModel> FinishedTopics { get; set; } = new ObservableCollection<TopicModel>();

        /// <summary>
        /// флажок, пройдены ли все темы
        /// </summary>
        public static bool allTopicsFinished { get; set; } = false;

        /// <summary>
        /// информация, о текущей теме, которую проходит студент
        /// </summary>
        public static TopicModel CurrentTopic { get; set; }

        /// <summary>
        /// номер текущего раздела, темы, которую проходит студент
        /// </summary>
        public static int CurrentTopicPortionNumber { get; set; }
        /// <summary>
        /// Модель хранящая информацию по просматриваемому курсу
        /// </summary>
        public static CourseModel currentCourseForStudent;

        /// <summary>
        /// получение списка тем доступных студенту
        /// </summary>
        /// <returns></returns>
        public static List<CourseModel> coursesAvailableTostudent()
        {
            List<CourseModel> courses = new List<CourseModel>();
            //проверяем свободный ли доступ к курсам
            bool freeAccess = Properties.Settings.Default.FreeAccess;
            if (freeAccess)
            {
                //если он свободный, то возвращаем список всех курсов
                courses = GlobalConfig.connection.GetCourseModelsAll();
                return courses;
            }
            else
            {
                //если нет, то загружаем список, какие темы доступны какой группе
                List<GroupToCourseRealationModel> access = GlobalConfig.connection.GetGroupToCourseRelationWithGroupid(UsersDataControl.currentStudent.StudentGroupid);

                //по нему заполняем список курсов
                foreach (GroupToCourseRealationModel relation in access)
                {
                    //проверяем есть ли группы студента доступ к курсу
                    if (relation.AccessBool)
                    {
                        //если доступ есть, то получаем информацию курса
                        CourseModel availableCourse = GlobalConfig.connection.GetCourse(relation.Courseid);
                        courses.Add(availableCourse);
                    }
                }
                return courses;
            }
        }


        /// <summary>
        /// функция, которая выполняется, когда студент открывает курс
        /// </summary>
        /// <param name="course">Модель курса, которая задается текущей и по которой получается информация</param>
        public static void courseStarted(CourseModel course)
        {
            //установка курса текущим
            currentCourseForStudent = course;
            //получение списка тем
            UnfinishedTopics = new ObservableCollection<TopicModel>(GlobalConfig.connection.GetTopicModels_byCourseID(course.id));

            //получаем информацию, прошел ли студент какие-то темы
            UsersDataControl.currentStudent.grades = GlobalConfig.connection.GetGrades_byStudent(UsersDataControl.currentStudent.id);
            //если студент уже прошел какие-то темы
            if (UsersDataControl.currentStudent.grades.Count != 0)
            {
                //переносим пройденные темы в соответствующий список
                FirstTopicsSwap();
            }
        }

        /// <summary>
        /// Первый перенос тем из списка непройденных тем, в список пройденных тем
        /// </summary>
        public static void FirstTopicsSwap()
        {
            List<TopicModel> FullTopicList = UnfinishedTopics.ToList();
            //переносим пройденные темы в соответствующий список
            for (int i = 0; i < UsersDataControl.currentStudent.grades.Count; i++)
            {
                //проверяем из нужного ли курса эта оценка студента
                bool topicInCourse = FullTopicList.Exists(t1 => t1.id == UsersDataControl.currentStudent.grades.ElementAt(i).Topicid);
                if (topicInCourse)
                {
                    //если оценка по теме из нужного курса, выделяем ее
                    TopicModel topicToSwap = FullTopicList.Find(t1 => t1.id == UsersDataControl.currentStudent.grades.ElementAt(i).Topicid);

                    //и переносим ее в список пройденных тем
                    swapTopic(topicToSwap);
                }
                else
                {
                    //если не из нужного, то убираем ее из списка для текущего курса
                    UsersDataControl.currentStudent.grades.Remove(UsersDataControl.currentStudent.grades.ElementAt(i));
                    i--;
                }
            }
        }


        /// <summary>
        /// перенос пройденной темы из списка непройденных тем, в список пройденных тем
        /// </summary>
        /// <param name="topic">Тема, которую мы переносим</param>
        public static void swapTopic(TopicModel topic)
        {
            //убираем ее из списка непройденных тем
            UnfinishedTopics.Remove(topic);
            //и добавляем в список пройденных
            FinishedTopics.Add(topic);

            //если в списке непройденных тем ничего не осталось
            if (UnfinishedTopics.Count == 0)
            {
                allTopicsFinished = true;
            }
        }

        /// <summary>
        /// получение информации по теме
        /// </summary>
        /// <param name="topic">тема, которая устанавливается текущей и по которой получается информация</param>
        public static void GetTopicInfo(TopicModel topic)
        {
            topic.Questions = GlobalConfig.connection.GetQuestions_byTopic(topic.id);
            topic.TopicPortions = GlobalConfig.connection.GetTopicPortions_bytopic(topic.id);
            CurrentTopic = topic;
        }

        /// <summary>
        /// Обновление отображаемого раздела темы. 
        /// Элемент CurrentTopicPortionNumber - это индекс раздела, в списке. 
        /// Создается со значением 0 и при каждом вызове метода, возвращает раздел с таким индексом и увеличивает CurrentTopicPortionNumber на 1
        /// </summary>
        /// <returns>Возвращает отображаемый раздел темы</returns>
        public static TopicPortionModel GetCurrentTopicPortion()
        {
            TopicPortionModel currentTopicPortion = CurrentTopic.TopicPortions.ElementAt(CurrentTopicPortionNumber);
            CurrentTopicPortionNumber++;
            return currentTopicPortion;
        }
    }
}
