using System.Windows;
using System.Windows.Controls;

namespace Sappi
{
    /// <summary>
    /// Interaction logic for StartUp.xaml
    /// </summary>
    public partial class StartUp : UserControl
    {
        public StartUp()
        {
            InitializeComponent();
        }

        private void newAppButton_Click(object sender, RoutedEventArgs e)
        {
            //MainWindow.Main.ContentArea.Content = new StudentForm();
            MainWindow.ChangeState(App.Page.StudentForm);
        }

        private void DatabaseButton_Click(object sender, RoutedEventArgs e)
        {
            //MainWindow.Main.ContentArea.Content = new DatabaseView();
            MainWindow.ChangeState(App.Page.DatabaseView);
        }
    }
}
