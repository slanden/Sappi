using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

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
            db.items = XmlSerial.Read<StudentData>("students.xml");
        }
    }
}
