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
    /// Класс, Отвечающий за хранение информации о вошедшем пользователе и доступных ему курсах
    /// </summary>
    public static class UsersDataControl
    {
        /// <summary>
        /// Модель хранящая информация вошедшего в систему студента
        /// </summary>
        public static StudentModel currentStudent { get; set; }

        /// <summary>
        /// Модель хранящая информация вошедшего в систему преподавателя
        /// </summary>
        public static TeacherModel currentTeacher { get; set; }

        /// <summary>
        /// Список хранящий информацию, о том, какой группе, доступен, какой курс 
        /// </summary>
        public static List<GroupToCourseRealationModel> AccessList { get; set; } = new List<GroupToCourseRealationModel>();

        /// <summary>
        /// Переменная, которая возвращает текущий курс для показа результатов студенту или преподавателю
        /// </summary>
        public static CourseModel currentCourse
        {
            get
            {
                if (DisplayedLearningMaterial.currentCourseForStudent != null)
                {
                    return DisplayedLearningMaterial.currentCourseForStudent;
                }
                else
                {
                    return LearningMaterialInsert.currentCourseForTeacher;
                }
            }
        }


        /// <summary>
        /// Метод принимающий информацию студента, проверяющий ее и производящий регистрирацию (или возвращающий номер ошибки для отображения)
        /// </summary>
        /// <param name="name"> Имя студента</param>
        /// <param name="group">Группа Студента</param>
        /// <param name="username">Логин Студента</param>
        /// <param name="password">Введеный пароль</param>
        /// <param name="confirmpassword">Введенной подтверждение пароля</param>
        /// <returns>После проверки информации, метод 
        /// или выполняет регистрацию и возвращает сигнал перехода на следующий экран (0) 
        /// или возвращает на информацию, на какой этап проверки информация не прошла
        /// При возврате 1 интерфейс выводит сообщение, что логин занят
        /// При возврате 2 интерфейс выводит сообщение, что пароли не совпадают
        /// При возврате 3 интерфейс выводит сообщение, что пользователь не ввел какие-то данные</returns>
        public static int CreateStudent(string name, GroupModel group, string username, string password, string confirmpassword)
        {
            //проверяем ввел ли пользователь нужные данные (если ни одна из строк не пустая - продолжаем)
            bool UserEnteredSomeData = !string.IsNullOrWhiteSpace(name) && group != null && !string.IsNullOrWhiteSpace(username) 
                                        && !string.IsNullOrWhiteSpace(password) && !string.IsNullOrWhiteSpace(confirmpassword);
            if (UserEnteredSomeData)
            {
                //проверяем совпадают ли пароли
                if (password == confirmpassword)
                {
                    //проверяем свободен ли логин
                    bool free = GlobalConfig.connection.CheckifTeacherUsernameIsFree(username) && GlobalConfig.connection.CheckifUsernameIsFree(username);
                    if (free)
                    {
                        //если все введенные данные верные, регистрируем студента
                        currentStudent = new StudentModel(name, group.id, username, password);
                        GlobalConfig.connection.createStudent(currentStudent);
                        return 0;
                    }
                    else
                    {
                        //если логин занят, всплывает соответствующее сообщение
                        return 1;
                    }
                }
                else
                {
                    //если пароли не совпадают, всплывает соответствующее сообщение
                    return 2;
                }

            }
            else
            {
                //если пользователь не ввел какие-то данные, всплывает соответствующее сообщение
                return 3;
            }
        }


        /// <summary>
        /// Метод принимающий информацию про преподавателя, проверяющий эту информацию и производящий регистрирацию (или возвращающий номер ошибки для отображения)
        /// </summary>
        /// <param name="name">имя преподавателя</param>
        /// <param name="position">Должность преподавателя</param>
        /// <param name="username">Логин преподавателя</param>
        /// <param name="password">Введеный пароль преподавателя</param>
        /// <param name="confirmpassword">Введенное подтверждение пароля</param>
        /// <returns>После проверки информации, метод 
        /// или выполняет регистрацию и возвращает сигнал перехода на следующий экран (0) 
        /// или возвращает на информацию, на какой этап проверки информация не прошла
        /// При возврате 1 интерфейс выводит сообщение, что логин занят
        /// При возврате 2 интерфейс выводит сообщение, что пароли не совпадают
        /// При возврате 3 интерфейс выводит сообщение, что пользователь не ввел какие-то данные</returns>
        public static int CreateTeacher(string name, string position, string username, string password, string confirmpassword)
        {
            //проверяем ввел ли пользователь нужные данные (если ни одна из строк не пустая - продолжаем)
            bool UserEnteredSomeData = !string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(position) && !string.IsNullOrWhiteSpace(username)
                                            && !string.IsNullOrWhiteSpace(password) && !string.IsNullOrWhiteSpace(confirmpassword);
            if (UserEnteredSomeData)
            {
                //проверяем совпадают ли пароли
                if (password == confirmpassword)
                {
                    //проверяем свободен ли логин
                    bool free = GlobalConfig.connection.CheckifTeacherUsernameIsFree(username)&& GlobalConfig.connection.CheckifUsernameIsFree(username);
                    if (free)
                    {
                        //если все введенные данные верные, регистрируем преподавателя
                        TeacherModel tm = new TeacherModel(name, position, username, password);
                        GlobalConfig.connection.createTeacher(tm);

                        //Разкоменьтите следующие строки, если хотите, чтобы система автоматически логинилась в аккаунт только что зарегистрированного преподавателя
                        ////и сообщаем системе, что он вошел. 
                        //currentTeacher = tm;
                        return 0;
                    }
                    else
                    {
                        //если логин занят, всплывает соответствующее сообщение
                        return 1;
                    }
                }
                else
                {
                    //если пароли не совпадают, всплывает соответствующее сообщение
                    return 2;
                }

            }
            else
            {
                //если пользователь не ввел какие-то данные, всплывает соответствующее сообщение
                return 3;
            }
        }

        /// <summary>
        /// Метод принимающий логин и пароль пользователя и производящий вход или возвращающий
        /// </summary>
        /// <param name="username">Введенный логин</param>
        /// <param name="enteredPassword">Введенный пароль</param>
        /// <returns>После проверки информации, метод 
        /// или выполняет "план Б", на случай пустоты базы данных и возвращает 0, говоря интерфейсу вызвать экран регистрации преподавателя
        /// или определяет, что логин и пароль студента правильные и возвращает 1, говоря говоря интерфейсу вызвать стартовый экран студента 
        /// или определяет, что логин и пароль учителя правильные и возвращает 2, говоря говоря интерфейсу вызвать стартовый экран учителя 
        /// 
        /// или возвращает информацию, какой этап проверки информация не прошла:
        /// При возврате 3 интерфейс выводит сообщение, что пользователь ввел неверный логин
        /// При возврате 4 интерфейс выводит сообщение, что пользователь ввел неверный пароль
        /// При возврате 5 интерфейс выводит сообщение, что пользователь не ввел какие-то данные</returns>
        public static int Login(string username, string enteredPassword)
        {
            //проверяем ввел ли пользователь нужные данные (если ни одна из строк не пустая - продолжаем)
            if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(enteredPassword))
            {
                //проверяем есть ли такой логин у студента
                bool user = !GlobalConfig.connection.CheckifUsernameIsFree(username);
                if (user)
                {
                    //проверяем правильный ли пароль
                    bool PassWordMatch = GlobalConfig.connection.CheckPasswordByUserName(username, enteredPassword);
                    if (PassWordMatch)
                    {
                        //в случае, если пользователь ввел верный логин и пароль (студента), открывается экран курсов студента
                        currentStudent = GlobalConfig.connection.GetStudentByUserName(username);
                        return 1;
                    }
                    else
                    {
                        //в случае, если пользователь ввел неверный пароль, всплывает соответствующее сообщение
                        return 4;
                    }
                }
                else
                {
                    //если введеный логин - не студента, проверяем есть ли в базе данных информация преподавателей
                    bool DataBaseisEmpty = !GlobalConfig.connection.checkIfThereIsaTeachersData();
                    if (DataBaseisEmpty)
                    {
                        //если база данных пуста, для добавления туда информации, есть одноразовый "план б", через ввод логина и пароля "admin"
                        if (username == "admin" && enteredPassword == "admin")
                        {
                            //в случае, если пользователь ввел admin в поле логина и пароля (при пустой базе данных), открывается экран регистрации учителя
                            return 0;
                        }
                        else
                        {
                            //в случае, если пользователь ввел неверный логин, всплывает соответствующее сообщение
                            return 3;
                        }
                    }
                    else
                    {
                        //если в базе данных есть информация преподавателей, проверяем, подходит ли логин
                        bool userNameIsFree = !GlobalConfig.connection.CheckifTeacherUsernameIsFree(username);
                        if (userNameIsFree)
                        {

                            //если в логин подходит, проверяем, подходит ли пароль
                            bool PassWordMatch = GlobalConfig.connection.CheckTeacherPasswordByUserName(username, enteredPassword);
                            if (PassWordMatch)
                            {
                                //в случае, если пользователь ввел верный логин и пароль (преподавателя), загружаются данные учителя и открывается экран его/ее курсов
                                currentTeacher = GlobalConfig.connection.GetTeacherByUserName(username);
                                currentTeacher.courses = new ObservableCollection<CourseModel>(GlobalConfig.connection.GetCourseModelsByTeacherID(currentTeacher.id));
                                return 2;
                            }
                            else
                            {
                                //в случае, если пользователь ввел неверный пароль, всплывает соответствующее сообщение
                                return 4;
                            }
                        }
                        else
                        {
                            //в случае, если пользователь ввел неверный логин, всплывает соответствующее сообщение
                            return 3;
                        }
                    }
                }
            }
            else
            {
                //в случае, если пользователь не ввел свои данные, всплывает соответствующее сообщение
                return 5;
            }
        }

        /// <summary>
        /// Абсолютно лишний метод, проверки свободно ли имя группы, через загрузку всех групп и поиск группы с совпадающем именем. 
        /// Его я создал в последний момент, когда мне было уже слишком лень писать отдельную хранимую процедуру на SQL и querry для SQLite, для нормальной проверки)
        /// Если программу кто-то будет поддерживать после меня - замените этот костыль, на что-то поприличнее
        /// </summary>
        /// <param name="groupName">Имя группы, которое мы проверяем на занятость</param>
        /// <returns></returns>
        public static bool GroupNameIsFree(string groupName)
        { 
            //загружаем (опять) список всех групп (абсолютно лишний расход ресурсов памяти
            List<GroupModel> groups = GlobalConfig.connection.GetGroups_All();

            //проверяем свободно ли имя
            bool free = !groups.Exists(x => x.GroupName == groupName);
            return free;
        }

        /// <summary>
        /// Метод создающий группу и добавляющий связь со всеми курсами (по умолчанию - запрещающую доступ)
        /// </summary>
        /// <param name="groupName"> Имя создаваемой группы</param>
        public static void CreateGroup(string groupName)
        {
            //создаем новую группу с полученным именем
            GroupModel group = new GroupModel(groupName);
            GlobalConfig.connection.createGroup(group);

            //получаем список всех существующих курсов
            List<CourseModel> courses = GlobalConfig.connection.GetCourseModelsAll();
            //создаем связь (по умолчанию - запрещающую доступ) с каждым из них
            foreach (CourseModel course in courses)
            {
                GroupToCourseRealationModel gtc = new GroupToCourseRealationModel(course.id, group.id);
                GlobalConfig.connection.CreateGroupToCourseRealation(gtc);
            }
        }

        /// <summary>
        /// Метод разрешающий/запрещающий группе просматривать курс
        /// </summary>
        /// <param name="relation">Связь между нужными группой и курсом</param>
        public static void changeGroupAccess(GroupToCourseRealationModel relation)
        {
            //проверяем имеет ли доступ группа
            if (!relation.AccessBool)
            {
                // если группа не имеет доступ, предоставляем
                relation.Access=1;
                GlobalConfig.connection.updateGroupToCourseRelation(relation);

                //обновляем отображаемый список
                AccessList = GlobalConfig.connection.GetGroupToCourseRelationWithCourseID(LearningMaterialInsert.currentCourseForTeacher.id);

            }
            else
            {
                // если группа  имеет доступ, закрываем
                relation.Access = 0;
                GlobalConfig.connection.updateGroupToCourseRelation(relation);

                //обновляем отображаемый список
                AccessList = GlobalConfig.connection.GetGroupToCourseRelationWithCourseID(LearningMaterialInsert.currentCourseForTeacher.id);
            }
        }

        /// <summary>
        /// Метод возвращающий список студентов имеющий доступ к курсу, для просмотра их оценок
        /// </summary>
        /// <returns></returns>
        public static List<StudentModel> getStudentsWhoHaveAccessToCourse()
        {
            //проверяем доступны ли все курсы всем студентам
            if (Properties.Settings.Default.FreeAccess)
            {
                //если все курсы, доступны всем студентам, загружаем список всех студентов
                List<StudentModel> StudentList = GlobalConfig.connection.getStudents_All(); 
                
                //загружаем оценки каждого
                foreach (StudentModel student in StudentList)
                {
                    student.grades = GlobalConfig.connection.GetStudentGradesforCourse(student.id, LearningMaterialInsert.currentCourseForTeacher.id);
                }

                return StudentList;
            }
            else
            {
                //если доступ только по разрешению, получаем список студентов, у которых есть доступ к курсу
                List<StudentModel> StudentList = GlobalConfig.connection.GetStudentsByCourseid(LearningMaterialInsert.currentCourseForTeacher.id);

                //загружаем оценки каждого
                foreach (StudentModel student in StudentList)
                {
                    student.grades = GlobalConfig.connection.GetStudentGradesforCourse(student.id, LearningMaterialInsert.currentCourseForTeacher.id);
                }
                return StudentList;
            }
        }

        /// <summary>
        /// Метод возвращающий список какой курс доступен какой группе
        /// </summary>
        /// <returns></returns>
        public static List<GroupToCourseRealationModel> LoadAccessPage()
        {
            //получаем список кому разрешен (а кому нет) доступ к курсу
            return GlobalConfig.connection.GetGroupToCourseRelationWithCourseID(LearningMaterialInsert.currentCourseForTeacher.id);
        }

        /// <summary>
        /// Метод записывающий результаты текущего студента
        /// </summary>
        /// <returns>Возвращает текст, посвященный текущему студенту, который печатается программой или обьединяется с другими текстами перед печатью</returns>
        public static List<string> printResultDocumentForStudent()
        {
            //создаем оформление
            List<string> text = new List<string>();
            text.Add("\n****************************************************************************\n" +
                     "****************************************************************************\n");
            //добавляем ифнормацию о текущем студенте
            text.Add($"Результаты студента: {currentStudent.StudentName} \n" +
                $"Из группы {currentStudent.StudentGroupName} \n \n");
            //добавляем информацию о каждой пройденной теме
            foreach (GradeModel grade in currentStudent.grades)
            {
                text.Add("\n" + $"{grade.getGradeInfoForStudent()}\n");
            }
            return text;
        }

        /// <summary>
        /// Метод записывающий результаты всех студентов
        /// </summary>
        /// <returns>Возвращает финальный текст, посвященный всем студентам, который печатается программой</returns>
        public static List<String> printResultDocumentForTeacher()
        {

            List<string> finalText = new List<string>();

            //получаем список всех студентов
            List<StudentModel> studentList = getStudentsWhoHaveAccessToCourse(); 
            //записываем информацию каждого
            foreach (StudentModel student in studentList)
            {
                //устанавливаем студента текущим
                currentStudent = student;
                //проверяем, начал ли он проходить темы курса
                if (student.grades.Count != 0)
                {
                    //если начал, то сортируем оценки, по порядку тем
                    student.grades.OrderBy(x => x.TopicOrderNumber);
                    //записываем результаты текущего студента
                    finalText.AddRange(printResultDocumentForStudent());
                } else
                {
                    //если не начал, то записываем это
                    finalText.Add("\n" + "****************************************************************************\n" +
                        "****************************************************************************\n");
                    finalText.Add($"Результаты студента: {student.StudentName} \n" +
                        $"Из группы {student.StudentGroupName} \n");
                    finalText.Add("\n"+"Прохождение тем не начато"+"\n");
                }
            }
            return finalText;
        }

    }
}
