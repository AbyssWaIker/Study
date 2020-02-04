using Study.Logic;
using Study.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Study.Views
{
    /// <summary>
    /// Логика взаимодействия для GiveAccessUserControl.xaml
    /// </summary>
    public partial class GiveAccessUserControl : UserControl
    {
        public GiveAccessUserControl()
        {
            InitializeComponent();

            AcceessShow.ItemsSource = UsersDataControl.LoadAccessPage();

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
                GroupToCourseRealationModel g = (GroupToCourseRealationModel)AcceessShow.SelectedItem;
                if (g != null)
                {
                    add = !g.AccessBool;
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
            GroupToCourseRealationModel g = (GroupToCourseRealationModel)AcceessShow.SelectedItem;
            if (g != null)
            {
                UsersDataControl.changeGroupAccess(g);

                AcceessShow.ItemsSource = null;
                AcceessShow.ItemsSource = UsersDataControl.LoadAccessPage();

                AcceessShow.SelectedItem = null;
            }
        }


        bool isChangeFromUser = true;
        bool StringIsEmpty = true;
        private void NewGroupName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (isChangeFromUser)
            {
                if (string.IsNullOrWhiteSpace(NewGroupName.Text))
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
                NewGroupName.Visibility = Visibility.Visible;
                isTextBoxVisible = true;
                NewGroupName.Text = string.Empty;
                StringIsEmpty = true;
                addGroup.Content = "X";
            }
            else
            {
                if (!StringIsEmpty)
                {
                    bool nameIsFree = UsersDataControl.GroupNameIsFree(NewGroupName.Text);
                    if (nameIsFree)
                    {
                        UsersDataControl.CreateGroup(NewGroupName.Text);

                        isChangeFromUser = false;
                        NewGroupName.Text = string.Empty;
                        isChangeFromUser = true;
                    }
                    else
                    {
                        MessageBox.Show("Группа С таким именем уже существует", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

                NewGroupName.Visibility = Visibility.Collapsed;
                isTextBoxVisible = false;
                addGroup.Content = "Новая группа";

                AcceessShow.ItemsSource = null;
                AcceessShow.ItemsSource = UsersDataControl.LoadAccessPage();
            }
        }

        /*private void deleteGroup_Click(object sender, RoutedEventArgs e)
        {
            GroupToCourseRealationModel model = (GroupToCourseRealationModel)AcceessShow.SelectedItem;
            if (model != null)
            {
                //уточняем у пользователя, точно ли он хочет удалить выбранную группу
                if (MessageBox.Show("Вы точно хотите удалить эту группу?", "Вы уверенны?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {

                }
            }
        }*/
    }
    
}
