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
        List<GroupToCourseRealationModel> AccessList = new List<GroupToCourseRealationModel>();
        List<GroupModel> Groups = new List<GroupModel>();

        CourseModel Course = new CourseModel();
        public GiveAccess(CourseModel course)
        {
            InitializeComponent();
            Groups = GlobalConfig.connection.GetGroups_All();
            AccessList = GlobalConfig.connection.GetGroupToCourseRelationWithCourseID(course.id);

            foreach(GroupToCourseRealationModel access in AccessList)
            {
                GroupModel group = Groups.Find(x => x.id == access.Groupid);
                group.access = true;
            }

            AcceessShow.ItemsSource = Groups;

            Course = course;

            if (Properties.Settings.Default.FreeAccess)
            {
                GiveAccessButton.Content = "Доступ свободный";
            }

        }

        bool add;
        private void AcceessShow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!Properties.Settings.Default.FreeAccess)
            {
                GroupModel group = (GroupModel)AcceessShow.SelectedItem;
                if (group != null)
                {
                    add = !group.access;
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
        }

        private void GiveAccessButton_Click(object sender, RoutedEventArgs e)
        {
            GroupModel g = (GroupModel)AcceessShow.SelectedItem;
            if (g != null)
            {
                add = !g.access;
                if (add)
                {
                    GroupToCourseRealationModel model = new GroupToCourseRealationModel(Course.id, g.id);
                    GlobalConfig.connection.CreateGroupToCourseRealation(model);

                    AccessList = GlobalConfig.connection.GetGroupToCourseRelationWithCourseID(Course.id);
                    g.access = true;

                }
                else
                {
                    GroupToCourseRealationModel StC_R = AccessList.Find(x => x.Groupid == g.id);
                    GlobalConfig.connection.deleteCourseToStudentRelation(StC_R.id);


                    AccessList = GlobalConfig.connection.GetGroupToCourseRelationWithCourseID(Course.id);
                    g.access = false;
                }
                AcceessShow.ItemsSource = null;
                AcceessShow.ItemsSource = Groups;

                AcceessShow.SelectedItem = null;
            }
        }

        private void Finish_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        bool isChangeFromUser = true;
        bool StringIsEmpty = true;
        private void NewGroupName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (isChangeFromUser)
            {
                if (NewGroupName.Text == "")
                {
                    StringIsEmpty = true;
                    addGroup.Content = "X";
                }
                else
                {
                    StringIsEmpty = false;
                    addGroup.Content = "+";
                }
            }
        }
        bool isTextBoxVisible = false;
        private void addGroup_Click(object sender, RoutedEventArgs e)
        {
            if (!isTextBoxVisible)
            {
                NewGroupName.Width = 190;
                NewGroupName.Visibility = Visibility.Visible;
                isTextBoxVisible = true;
                NewGroupName.Text = "";
                StringIsEmpty = true;
                addGroup.Content = "X";
            }
            else
            {
                if(!StringIsEmpty)
                {
                    GroupModel model = new GroupModel(NewGroupName.Text);
                    GlobalConfig.connection.createGroup(model);
                    isChangeFromUser = false;
                    NewGroupName.Text = "";
                    isChangeFromUser = true;
                }

                NewGroupName.Width = 1;
                NewGroupName.Visibility = Visibility.Hidden;
                isTextBoxVisible = false;
                addGroup.Content = "Добавить новую группу";
            }
        }
    }
}
