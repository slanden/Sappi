using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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

namespace Sappi
{
    /// <summary>
    /// Interaction logic for DatabaseView.xaml
    /// </summary>
    public partial class DatabaseView : UserControl
    {
        public DatabaseView()
        {
            InitializeComponent();
        }

        private void DatabaseView_Loaded(object sender, RoutedEventArgs e)
        {
            if (App.db.items == null)
                Console.WriteLine("items NULL");
            else
                Console.WriteLine(App.db.items.Count);

            dg.ItemsSource = App.db.items;
            PopulateDatagrid(dg);

            Console.WriteLine("Column count: " + dg.Columns.Count);
            Console.WriteLine("Display index: ");
        }

        private void PopulateDatagrid(DataGrid grid)
        {
            dg.Columns.Add(new DataGridTextColumn() {Header = "Name", Binding = new Binding("name")});
            dg.Columns.Add(new DataGridTextColumn() { Header = "Campus", Binding = new Binding("masterList") });
            dg.Columns.Add(new DataGridTextColumn() { Header = "Program", Binding = new Binding("masterList") });

            //for (int i = 0; i < App.formData.groups.Count; ++i)
            //{
            //    DataGridColumn c;
            //    c.Header = 
            //    dg.Columns.Add(c);
            //    for (int j = 0; j < App.db.items.Count; ++j)
            //    {

            //    }
            //}
        }
    }
}
