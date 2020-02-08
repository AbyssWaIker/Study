using Study.Logic;
using System;
using System.Windows;
using System.Windows.Input;

namespace Study.Views
{
    /// <summary>
    /// Чтобы минимизировать открытие/закрытие новых окон, я сделал стартовое окно, пустой оболочкой, в которую помещаются все UserContro-экраны.
    /// Для навигации между ними, используются команды, которые могут активироватся внутри других экранах и пузырьком всплывать в корневое окно, где и обрабатываются
    /// </summary>
    public partial class StartWindowShell : Window
    {
        /// <summary>
        /// определение комманды, загружающей экран стартового выбора (регистрация/вход)
        /// </summary>
        public static RoutedCommand LoadChoiceScreen = new RoutedCommand("LoadChoiceScreen", typeof(StartWindowShell));

        /// <summary>
        /// определение комманды, загружающей экран входа
        /// </summary>
        public static RoutedCommand LoadLoginScreen = new RoutedCommand("LoadLoginScreen", typeof(StartWindowShell));

        /// <summary>
        /// определение комманды, загружающей экран регистрации
        /// </summary>
        public static RoutedCommand LoadRegistrationScreen = new RoutedCommand("LoadRegistrationScreen", typeof(StartWindowShell));

        /// <summary>
        /// определение комманды, загружающей экран регистрации учителя
        /// </summary>
        public static RoutedCommand LoadTeacherRegistrationScreen = new RoutedCommand("LoadTeacherRegistrationScreen", typeof(StartWindowShell));

        /// <summary>
        /// определение комманды, срабатывающей, при нажатии "назад" на экране регистрации учителя
        /// </summary>
        public static RoutedCommand ReturnFromTeacherRegistrationScreen = new RoutedCommand("ReturnFromTeacherRegistrationScreen", typeof(StartWindowShell));

        /// <summary>
        /// определение комманды, загружающей экран списка предметов учителя
        /// </summary>
        public static RoutedCommand LoadTeacherCourseScreen = new RoutedCommand("LoadTeacherCourseScreen", typeof(StartWindowShell));

        /// <summary>
        /// определение комманды, загружающей экран списка тем учителя
        /// </summary>
        public static RoutedCommand LoadTeacherTopicListScreen = new RoutedCommand("LoadTeacherTopicListScreen", typeof(StartWindowShell));

        /// <summary>
        /// определение комманды, загружающей экран учителя, создания/изменения темы
        /// </summary>
        public static RoutedCommand LoadTeacherAddTopicScreen = new RoutedCommand("LoadTeacherAddTopicScreen", typeof(StartWindowShell));

        /// <summary>
        /// определение комманды, загружающей экран учителя, создания/изменения раздела темы 
        /// </summary>
        public static RoutedCommand LoadTeacherAddTopicPortionScreen = new RoutedCommand("LoadTeacherAddTopicPortionScreen", typeof(StartWindowShell));

        /// <summary>
        /// определение комманды, загружающей экран учителя, создания/изменения вопроса темы 
        /// </summary>
        public static RoutedCommand LoadTeacherAddTopicQuestionScreen = new RoutedCommand("LoadTeacherAddTopicQuestionScreen", typeof(StartWindowShell));

        /// <summary>
        /// определение комманды, загружающей экран учителя, предоставления доступа студентам к курсам
        /// </summary>
        public static RoutedCommand LoadTeacherGiveAccessScreen = new RoutedCommand("LoadTeacherGiveAccessScreen", typeof(StartWindowShell));

        /// <summary>
        /// определение комманды, загружающей экран курсов студента
        /// </summary>
        public static RoutedCommand LoadStudentCourseListScreen = new RoutedCommand("LoadStudentCourseListScreen", typeof(StartWindowShell));

        /// <summary>
        /// определение комманды, загружающей экран тем студента
        /// </summary>
        public static RoutedCommand LoadStudentTopicListScreen = new RoutedCommand("LoadStudentTopicListScreen", typeof(StartWindowShell));

        /// <summary>
        /// определение комманды, загружающей экран раздела выбранной темы студента
        /// </summary>
        public static RoutedCommand LoadStudentTopicPortionScreen = new RoutedCommand("LoadStudentTopicPortionScreen", typeof(StartWindowShell));

        /// <summary>
        /// определение комманды, загружающей экран вопросов выбранной темы студента
        /// </summary>
        public static RoutedCommand LoadStudentTopicQuestionScreen = new RoutedCommand("LoadStudentTopicQuestionScreen", typeof(StartWindowShell));

        /// <summary>
        /// определение комманды, загружающей экран результатов студента
        /// </summary>
        public static RoutedCommand LoadStudentResultScreen = new RoutedCommand("LoadStudentResultScreen", typeof(StartWindowShell));

        /// <summary>
        /// определение комманды, загружающей экран результатов учителя
        /// </summary>
        public static RoutedCommand LoadTeacherResultScreen = new RoutedCommand("LoadTeacherResultScreen", typeof(StartWindowShell));

        /// <summary>
        /// При запуске программы, она подключается к базе данных и загружет стартовый экран 
        /// </summary>
        public StartWindowShell()
        {
            InitializeComponent();
            
            //Подключение к базе данных
            GlobalConfig.InitializeConnection(DataBaseType.sqlite);

            //Отображение экрана стартового выбора (регистрация/вход)
            DisplayedPage.Content = new NewOrOld();
        }

        /// <summary>
        /// обработка комманды, загружающей экран стартового выбора (регистрация/вход)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadChoiceScreen_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Отображение экрана стартового выбора (регистрация/вход)
            DisplayedPage.Content = new NewOrOld();
        }

        //обработка комманды, загружающей экран входа
        private void LoadLoginScreen_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Отображение экрана входа
            DisplayedPage.Content = new LoginUserControl();
        }

        //обработка комманды, загружающей экран регистрации
        private void LoadRegistrationScreen_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Отображение экрана регистрации
            DisplayedPage.Content = new StudentRegistrationUserControl();
        }


        //обработка комманды, загружающей экран регистрации учителя
        private void LoadTeacherRegistrationScreen_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Отображение экрана регистрации учителя
            DisplayedPage.Content = new TeacherRegistrationUserControl();
        }


        //обработка комманды, срабатывающей, при нажатии "назад" на экране регистрации учителя
        private void ReturnFromTeacherRegistrationScreen_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //проверка - зашел ли в систему преподователь (и это он добавляет нового учителя) или нет (и это "план Б" (на случай, если в базе данных пусто) добавление со стартового экрана)
            if(UsersDataControl.currentTeacher!=null)
            {
                //возврат на стартовый экран преподавателя
                LoadTeacherCourseScreen_Executed(sender, e);
            }
            else
            {
                //возврат на стартовый экран
                DisplayedPage.Content = new LoginUserControl();
            }
        }


        //обработка комманды, загружающей экран регистрации учителя
        private void LoadTeacherCourseScreen_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Отображение экрана регистрации учителя
            DisplayedPage.Content = new TeacherCourseListUserControl();
            //добавляем место для информации и кнопки выйти
            Height += 20;
            LoggedInfoRow.Height = new GridLength(20);
            //Отображаем информацию и кнопку выйти
            LoggedInfo.Visibility = Visibility.Visible;

            //Отображаем имя преподавателя
            NameOfUser.Text = UsersDataControl.currentTeacher.Name;
        }

        //обработка комманды, загружающей экран регистрации учителя
        private void LoadTeacherTopicListScreen_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //на случай, если мы попали сюда, кликнув "отмена" при создании/изменеии темы, чистим память
            if(LearningMaterialInsert.PortionList.Count!=0)
            {
                LearningMaterialInsert.PortionList.Clear();
            }
            if (LearningMaterialInsert.QuestionList.Count != 0)
            {
                LearningMaterialInsert.QuestionList.Clear();
            }

                //Отображение экрана регистрации учителя
                DisplayedPage.Content = new TeacherTopicListUserControl();
        }

        //определение комманды, загружающей экран учителя, создания/изменения темы 
        private void LoadTeacherAddTopicScreen_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Отображение экрана создания/изменения темы 
            DisplayedPage.Content = new AddTopicUserControl();
        }

        //определение комманды, загружающей экран учителя, создания/изменения раздела темы
        private void LoadTeacherAddTopicPortionScreen_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Отображение экрана создания/изменения раздела темы
            DisplayedPage.Content = new AddTopicPortionUserControl();
        }

        //определение комманды, загружающей экран учителя, создания/изменения вопроса темы
        private void LoadTeacherAddTopicQuestionScreen_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Отображение экрана создания/изменения вопроса темы
            DisplayedPage.Content = new AddTopicQuestionUserControl();
        }

        //обработка комманды, загружающей экран учителя, предоставления доступа студентам к курсам
        private void LoadTeacherGiveAccessScreen_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Отображение экрана предоставления доступа студентам к курсам
            DisplayedPage.Content = new GiveAccessUserControl();
        }

        //обработка комманды, загружающей экран курсов студента
        private void LoadStudentCourseListScreen_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Отображение экрана курсов студента
            DisplayedPage.Content = new StudentCoursesListUserControl();
            //добавляем место для информации и кнопки выйти
            Height += 20;
            LoggedInfoRow.Height = new GridLength(20);
            //Отображаем информацию и кнопку выйти
            LoggedInfo.Visibility = Visibility.Visible;

            //Отображаем имя преподавателя
            NameOfUser.Text = UsersDataControl.currentStudent.StudentName;
        }
        
        //обработка комманды, загружающей экран тем студента
        private void LoadStudentTopicListScreen_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Отображение экрана тем студента
            DisplayedPage.Content = new StudentTopicListUserControl();
        }

        //обработка комманды, загружающей экран раздела выбранной темы студента
        private void LoadStudentTopicPortionScreen_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Отображение экрана раздела выбранной темы студента
            DisplayedPage.Content = new StudentTopicPortionUserControl();
        }

        //обработка комманды, загружающей экран вопросов выбранной темы студента
        private void LoadStudentTopicQuestionScreen_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Отображение экрана раздела выбранной темы студента
            DisplayedPage.Content = new StudentTopicQuestionUserControl();
        }

        //обработка комманды, загружающей экран результатов студента
        private void LoadStudentResultScreen_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Отображение экрана результатов студента
            DisplayedPage.Content = new StudentResultUserControl();
        }

        //обработка комманды, загружающей экран результатов учителя
        private void LoadTeacherResultScreen_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Отображение экрана результатов учителя
            DisplayedPage.Content = new TeacherResultsUserControl();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            //Отображение экрана стартового выбора (регистрация/вход)
            DisplayedPage.Content = new NewOrOld();
            //убираем место для информации и кнопки выйти
            Height -= 20;
            LoggedInfoRow.Height = new GridLength(0);
            //прячем информацию о вошедшем пользователе и кнопку выйти
            LoggedInfo.Visibility = Visibility.Collapsed;

            //Очистка данных о залогинившемся пользователе
            UsersDataControl.currentStudent = null;
            UsersDataControl.currentTeacher = null;
        }
    }
}
