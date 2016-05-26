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
            Main = this;
        }

        
    }
}
