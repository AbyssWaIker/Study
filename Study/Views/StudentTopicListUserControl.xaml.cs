using Study.Logic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Study.Views
{
    /// <summary>
    /// Логика взаимодействия для StudentTopicListUserControl.xaml
    /// </summary>
    public partial class StudentTopicListUserControl : UserControl
    {
        public StudentTopicListUserControl()
        {
            InitializeComponent();


            finishedTopicList.ItemsSource = DisplayedLearningMaterial.FinishedTopics;
            UnfinishedTopicList.ItemsSource = DisplayedLearningMaterial.UnfinishedTopics;

            if (DisplayedLearningMaterial.allTopicsFinished)
            {
                UnfinishedTopicLabel.Text = "Все темы пройденны";

                ViewResultButton.IsEnabled = true;
                StartTopicButton.IsEnabled = false;
            }
        }

        private void StartTopic_Click(object sender, RoutedEventArgs e)
        {
            TopicModel tm = (TopicModel)UnfinishedTopicList.SelectedItem;
            if (tm != null)
            {
                if (UnfinishedTopicList.SelectedIndex == 0)
                {
                    DisplayedLearningMaterial.GetTopicInfo(tm);

                    CommandBinding openTopic = new CommandBinding(StartWindowShell.LoadStudentTopicPortionScreen);
                    openTopic.Command.Execute("placeholder object");
                }
                else
                {
                    MessageBox.Show("проходи темы по порядку :)");
                }
            }
        }
    }
}
