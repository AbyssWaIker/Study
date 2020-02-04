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
        //определение комманды, загружающей экран стартового выбора (регистрация/вход)
        public static RoutedCommand LoadChoiceScreen = new RoutedCommand("LoadChoiceScreen", typeof(StartWindowShell));

        //определение комманды, загружающей экран входа
        public static RoutedCommand LoadLoginScreen = new RoutedCommand("LoadLoginScreen", typeof(StartWindowShell));

        //определение комманды, загружающей экран регистрации
        public static RoutedCommand LoadRegistrationScreen = new RoutedCommand("LoadRegistrationScreen", typeof(StartWindowShell));

        //определение комманды, загружающей экран регистрации учителя
        public static RoutedCommand LoadTeacherRegistrationScreen = new RoutedCommand("LoadTeacherRegistrationScreen", typeof(StartWindowShell));

        //определение комманды, срабатывающей, при нажатии "назад" на экране регистрации учителя
        public static RoutedCommand ReturnFromTeacherRegistrationScreen = new RoutedCommand("ReturnFromTeacherRegistrationScreen", typeof(StartWindowShell));

        //определение комманды, загружающей экран списка предметов учителя
        public static RoutedCommand LoadTeacherCourseScreen = new RoutedCommand("LoadTeacherCourseScreen", typeof(StartWindowShell));

        //определение комманды, загружающей экран списка тем учителя
        public static RoutedCommand LoadTeacherTopicListScreen = new RoutedCommand("LoadTeacherTopicListScreen", typeof(StartWindowShell));

        //определение комманды, загружающей экран учителя, создания/изменения темы
        public static RoutedCommand LoadTeacherAddTopicScreen = new RoutedCommand("LoadTeacherAddTopicScreen", typeof(StartWindowShell));

        //определение комманды, загружающей экран учителя, создания/изменения раздела темы 
        public static RoutedCommand LoadTeacherAddTopicPortionScreen = new RoutedCommand("LoadTeacherAddTopicPortionScreen", typeof(StartWindowShell));

        //определение комманды, загружающей экран учителя, создания/изменения вопроса темы 
        public static RoutedCommand LoadTeacherAddTopicQuestionScreen = new RoutedCommand("LoadTeacherAddTopicQuestionScreen", typeof(StartWindowShell));

        //определение комманды, загружающей экран учителя, предоставления доступа студентам к курсам
        public static RoutedCommand LoadTeacherGiveAccessScreen = new RoutedCommand("LoadTeacherGiveAccessScreen", typeof(StartWindowShell));

        //определение комманды, загружающей экран курсов студента
        public static RoutedCommand LoadStudentCourseListScreen = new RoutedCommand("LoadStudentCourseListScreen", typeof(StartWindowShell));

        //определение комманды, загружающей экран тем студента
        public static RoutedCommand LoadStudentTopicListScreen = new RoutedCommand("LoadStudentTopicListScreen", typeof(StartWindowShell));

        //определение комманды, загружающей экран раздела выбранной темы студента
        public static RoutedCommand LoadStudentTopicPortionScreen = new RoutedCommand("LoadStudentTopicPortionScreen", typeof(StartWindowShell));

        //определение комманды, загружающей экран вопросов выбранной темы студента
        public static RoutedCommand LoadStudentTopicQuestionScreen = new RoutedCommand("LoadStudentTopicQuestionScreen", typeof(StartWindowShell));

        //определение комманды, загружающей экран результатов студента
        public static RoutedCommand LoadStudentResultScreen = new RoutedCommand("LoadStudentResultScreen", typeof(StartWindowShell));

        //определение комманды, загружающей экран результатов учителя
        public static RoutedCommand LoadTeacherResultScreen = new RoutedCommand("LoadTeacherResultScreen", typeof(StartWindowShell));

        public StartWindowShell()
        {
            InitializeComponent();
            
            //Подключение к базе данных
            GlobalConfig.InitializeConnection(DataBaseType.sqlite);

            //Отображение экрана стартового выбора (регистрация/вход)
            DispayedPage.Content = new NewOrOld();
        }

        //обработка комманды, загружающей экран стартового выбора (регистрация/вход)
        private void LoadChoiceScreen_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Отображение экрана стартового выбора (регистрация/вход)
            DispayedPage.Content = new NewOrOld();
        }

        //обработка комманды, загружающей экран входа
        private void LoadLoginScreen_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Отображение экрана входа
            DispayedPage.Content = new LoginUserControl();
        }

        //обработка комманды, загружающей экран регистрации
        private void LoadRegistrationScreen_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Отображение экрана регистрации
            DispayedPage.Content = new StudentRegistrationUserControl();
        }


        //обработка комманды, загружающей экран регистрации учителя
        private void LoadTeacherRegistrationScreen_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Отображение экрана регистрации учителя
            DispayedPage.Content = new TeacherRegistrationUserControl();
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
                DispayedPage.Content = new LoginUserControl();
            }
        }


        //обработка комманды, загружающей экран регистрации учителя
        private void LoadTeacherCourseScreen_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Отображение экрана регистрации учителя
            DispayedPage.Content = new TeacherCourseListUserControl();
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
            //Отображение экрана регистрации учителя
            DispayedPage.Content = new TeacherTopicListUserControl();
        }

        //определение комманды, загружающей экран учителя, создания/изменения темы 
        private void LoadTeacherAddTopicScreen_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Отображение экрана создания/изменения темы 
            DispayedPage.Content = new AddTopicUserControl();
        }

        //определение комманды, загружающей экран учителя, создания/изменения раздела темы
        private void LoadTeacherAddTopicPortionScreen_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Отображение экрана создания/изменения раздела темы
            DispayedPage.Content = new AddTopicPortionUserControl();
        }

        //определение комманды, загружающей экран учителя, создания/изменения вопроса темы
        private void LoadTeacherAddTopicQuestionScreen_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Отображение экрана создания/изменения вопроса темы
            DispayedPage.Content = new AddTopicQuestionUserControl();
        }

        //обработка комманды, загружающей экран учителя, предоставления доступа студентам к курсам
        private void LoadTeacherGiveAccessScreen_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Отображение экрана предоставления доступа студентам к курсам
            DispayedPage.Content = new GiveAccessUserControl();
        }

        //обработка комманды, загружающей экран курсов студента
        private void LoadStudentCourseListScreen_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Отображение экрана курсов студента
            DispayedPage.Content = new StudentCoursesListUserControl();
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
            DispayedPage.Content = new StudentTopicListUserControl();
        }

        //обработка комманды, загружающей экран раздела выбранной темы студента
        private void LoadStudentTopicPortionScreen_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Отображение экрана раздела выбранной темы студента
            DispayedPage.Content = new StudentTopicPortionUserControl();
        }

        //обработка комманды, загружающей экран вопросов выбранной темы студента
        private void LoadStudentTopicQuestionScreen_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Отображение экрана раздела выбранной темы студента
            DispayedPage.Content = new StudentTopicQuestionUserControl();
        }

        //обработка комманды, загружающей экран результатов студента
        private void LoadStudentResultScreen_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Отображение экрана результатов студента
            DispayedPage.Content = new StudentResultUserControl();
        }

        //обработка комманды, загружающей экран результатов учителя
        private void LoadTeacherResultScreen_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //Отображение экрана результатов учителя
            DispayedPage.Content = new TeacherResultsUserControl();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            //Отображение экрана стартового выбора (регистрация/вход)
            DispayedPage.Content = new NewOrOld();
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
