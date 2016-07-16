using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
        bool scaleButtonPressed = false;

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

        private void Scale(object sender, RoutedEventArgs e)
        {
            newAppButton.RenderTransformOrigin = new Point(0.5,0.5);
            if (scaleButtonPressed == false)
            {
                (sender as Button).Content = "Reset";

                ScaleTransform st = new ScaleTransform();
                st.ScaleX = 5;
                st.ScaleY = 5;

                //newAppButton.Template

                newAppButton.RenderTransform = st;
                scaleButtonPressed = true;
            }
            else
            {
                (sender as Button).Content = "Scale Up";

                ScaleTransform st = new ScaleTransform();
                st.ScaleX = 1;
                st.ScaleY = 1;

                newAppButton.RenderTransform = st;
                scaleButtonPressed = false;
            }
        }
    }
}
