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
    /// Логика взаимодействия для GiveAccess.xaml
    /// </summary>
    public partial class GiveAccess : Window
    {
        List<StudentToCourseRealationModel> AccessList = new List<StudentToCourseRealationModel>();
        List<StudentModel> Students = new List<StudentModel>();

        CourseModel Course = new CourseModel();

        public GiveAccess(CourseModel course)
        {
            InitializeComponent();
            Students = GlobalConfig.connection.getStudents_All();
            AccessList = GlobalConfig.connection.GetStudentToCourseRelationWithCourseID(course.id);

            foreach(StudentToCourseRealationModel access in AccessList)
            {
                StudentModel student = Students.Find(x => x.id == access.Studentid);
                student.access = true;
            }

            AcceessShow.ItemsSource = Students;


            Course = course;

        }

        bool add;
        private void AcceessShow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StudentModel st = (StudentModel)AcceessShow.SelectedItem;
            if(st!=null)
            {
                add = !st.access;
                if (add)
                {
                    GiveAccessButton.Content = "Предоставить доступ";
                    GiveAccessButton.IsEnabled = true;
                }
                else
                {
                    GiveAccessButton.Content = "Заблокировать доступ";
                    GiveAccessButton.IsEnabled = true;
                }
            }
            else
            {
                GiveAccessButton.IsEnabled = false;
            }
        }

        private void GiveAccessButton_Click(object sender, RoutedEventArgs e)
        {
            StudentModel st = (StudentModel)AcceessShow.SelectedItem;
            if (st != null)
            {
                add = !st.access;
                if (add)
                {
                    StudentToCourseRealationModel model = new StudentToCourseRealationModel(Course.id, st.id);
                    GlobalConfig.connection.CreateStudentToCourseRealation(model);

                    AccessList = GlobalConfig.connection.GetStudentToCourseRelationWithCourseID(Course.id);
                    st.access = true;

                }
                else
                {
                    StudentToCourseRealationModel StC_R = AccessList.Find(x => x.Studentid == st.id);
                    GlobalConfig.connection.deleteCourseToStudentRelation(StC_R.id);


                    AccessList = GlobalConfig.connection.GetStudentToCourseRelationWithCourseID(Course.id);
                    st.access = false;
                }
                AcceessShow.ItemsSource = null;
                AcceessShow.ItemsSource = Students;

                AcceessShow.SelectedItem = null;
            }
        }

        private void Finish_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
