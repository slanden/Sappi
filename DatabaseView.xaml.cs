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

            //default form vals for testing
            StudentData sd = new StudentData(App.formData.groups.Count);
            sd.name = "John Marston";
            sd.groupBoxes[0] = 2;
            sd.groupBoxes[1] = 6;

            StudentData sd2 = new StudentData(App.formData.groups.Count);
            sd2.name = "Jim Jammers";
            sd2.groupBoxes[0] = 1;
            sd2.groupBoxes[1] = 4;

            StudentData sd3 = new StudentData(App.formData.groups.Count);
            sd3.name = "Dana Disappears";
            sd3.groupBoxes[0] = 2;
            sd3.groupBoxes[1] = 5;

            StudentData sd4 = new StudentData(App.formData.groups.Count);
            sd4.name = "Zachary Powers";
            sd4.groupBoxes[0] = 2;
            sd4.groupBoxes[1] = 6;

            App.db.items.Add(sd);
            App.db.items.Add(sd2);
            App.db.items.Add(sd3);
            App.db.items.Add(sd4);

            dg.ItemsSource = App.db.items;
            PopulateDatagrid(dg);
        }

        private void PopulateDatagrid(DataGrid grid)
        {
            dg.Columns.Add(new DataGridTextColumn() { Header = "Name", Binding = new Binding("name")});
            dg.Columns.Add(new DataGridTextColumn() { Header = "Campus", Binding = new Binding("groupBoxes[0]")
                                                    { Converter = new IntToKeyConverter() }});
            dg.Columns.Add(new DataGridTextColumn() { Header = "Program", Binding = new Binding("groupBoxes[1]")
                                                    { Converter = new IntToKeyConverter() } });
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

        private void Search(object sender, TextChangedEventArgs e)
        {
            if (searchBar.Text == "" || searchBar.Text == "(search)")
                return;

            int inputLength = searchBar.Text.Length;
            Console.WriteLine("length: " + inputLength);
            for (int i = 0; i < dg.Items.Count; ++i)
            {
                Console.WriteLine("item: " + dg.SelectAllCells());
                
                //if (dg.Items[i].ToString() .Substring(0, inputLength)
                //    == searchBar.Text)
                //{
                //    Console.WriteLine("dg selected string: " + dg.Items[i].ToString().Substring(0, inputLength));
                //    dg.SelectedItem = dg.Items[i];
                //}
            }
        }

        private void Textbox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= Textbox_GotFocus;
            tb.TextChanged += Search;
        }
    }
}
