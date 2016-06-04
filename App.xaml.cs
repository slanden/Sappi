using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Sappi
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Database db;
        public static FormData formData;
        public static string pathToDatabase = "..\\students.xml";
        public enum Page { StartUp, StudentForm, DatabaseView , length};
        //public static int previousPage;
        public static UserControl previous;


        public App()
        {
            InitializeComponent();
            db = new Database();
            formData = new FormData();
            previous = new StartUp();

            //read in student info from database file
            db.items = XmlSerial.Read<StudentData>(pathToDatabase);
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            string error = "Exception during runtime: " + e.Exception.ToString();
            StreamWriter file = new StreamWriter("Errors.txt");
            file.Write(error);
            file.Flush();
            file.Close();
            file.Dispose();
        }
    }
}
