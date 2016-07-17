using System.Windows;
using System.Windows.Controls;

namespace Sappi
{
    /// <summary>
    /// Interaction logic for StudentView.xaml
    /// </summary>
    public partial class StudentView : UserControl
    {
        public StudentView()
        {
            InitializeComponent();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            if (studentInfo_contentArea.Content == null)
            {
                studentInfo_contentArea.Content = new StudentForm();
                editButton.Content = "Done";       
            }
            else
            {
                studentInfo_contentArea.Content = null;
                editButton.Content = "Edit";
            }
        }

    }
}
