using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace Sappi
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Database db;
        public static FormData formData;
         
        public App()
        {
            db = new Database();
            formData = new FormData();

            //read in student info from database file
            db.items = XmlSerial.Read<StudentData>("..\\students.xml");
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
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
