using System.Windows;

namespace Sappi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal static MainWindow Main;
        public MainWindow()
        {
            InitializeComponent();

            //set the start up content
            ContentArea.Content = new StartUp();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //use this to access ContentArea.Content for "State-switching"
            Main = this;
        }

        public static void ChangeState(App.Page ToPage,object studentFormParam = null, 
                                       object startUpParam = null, object databaseParam = null)
        {
            //first check to see which UserControl is the current
            //then set the previous UserControl to that.
            switch (Main.ContentArea.Content.GetType().Name)
            {
                case "StartUp":
                {
                    App.previous = new StartUp();
                }
                    break;
                case "StudentForm":
                {
                    App.previous = new StudentForm();
                }
                    break;
                case "DatabaseView":
                {
                    App.previous = new DatabaseView();
                }
                    break;
            }

            //set the current UserControl to the desired destination
            switch (ToPage)
            {
                case App.Page.StartUp:
                    {
                        Main.ContentArea.Content = new StartUp();
                    }
                    break;
                case App.Page.StudentForm:
                    {
                        Main.ContentArea.Content = new StudentForm(studentFormParam as StudentData);
                    }
                    break;
                case App.Page.DatabaseView:
                    {
                        Main.ContentArea.Content = new DatabaseView();
                    }
                    break;
            }
        }

    }
}
