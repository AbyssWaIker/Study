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
using Study.Models;

namespace Study.Views
{
    /// <summary>
    /// Логика взаимодействия для StudentCoursesList.xaml
    /// </summary>
    public partial class StudentCoursesList : Window
    {
        List<StudentToCourseRealationModel> access = new List<StudentToCourseRealationModel>();
        private List<CourseModel> courses = new List<CourseModel>();
        private StudentModel stm = new StudentModel();
        bool freeAccess;

        public StudentCoursesList(StudentModel st)
        {
            InitializeComponent();
            InitializeComponent();

            access = GlobalConfig.connection.GetStudentToCourseRelationWithStudentid(st.id);
            freeAccess = Properties.Settings.Default.FreeAccess;
            if (freeAccess)
            {
                courses = GlobalConfig.connection.GetCourseModelsAll();
            }
            else
            {
                foreach(StudentToCourseRealationModel StC_R in access)
                {
                    CourseModel availableCourse = GlobalConfig.connection.GetCourse(StC_R.Courseid);
                    courses.Add(availableCourse);
                }
            }


            AllCourses.ItemsSource = courses;
            AllCourses.DisplayMemberPath = "Name";
            stm = st;
        }

        private void StartСourse_Click(object sender, RoutedEventArgs e)
        {
            CourseModel course = (CourseModel)AllCourses.SelectedItem;

            if (course != null)
            {
                //Если в настройках указан свободный доступ, то чтобы чтобы студент мог завершить курс, при смене настроек...
                if (freeAccess)
                {
                    //проверяем есть ли у него доступ к курсу
                    access = GlobalConfig.connection.GetStudentToCourseRelationWithStudentid(stm.id);
                    bool avialbleCourse = access.Exists(x => x.Studentid == stm.id);
                    if (!avialbleCourse)
                    {
                        //если нет доступак курсу, в который входит студент, то предоставляем 
                        StudentToCourseRealationModel model = new StudentToCourseRealationModel(course.id, stm.id);
                        GlobalConfig.connection.CreateStudentToCourseRealation(model);
                    }
                }


                TopicList tpl = new TopicList(stm, course);
                tpl.Show();
                this.Close();
            }
        }
    }
}
