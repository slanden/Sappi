using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

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
            dg.Columns.Add(new DataGridTextColumn() { Header = "Name", Binding = new Binding("name") });
            dg.Columns.Add(new DataGridTextColumn()
            {
                Header = "Campus",
                Binding = new Binding("groupBoxes[0]")
                { Converter = new IntToKeyConverter() }
            });
            dg.Columns.Add(new DataGridTextColumn()
            {
                Header = "Program",
                Binding = new Binding("groupBoxes[1]")
                { Converter = new IntToKeyConverter() }
            });
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

            for (int i = 0; i < dg.Items.Count; ++i)
            {
                DataGridCell cell = DataGridHelper.GetCell(dg, i, 0);

                if (cell != null)
                {
                    //get text content
                    var cellContent = (TextBlock)cell.Content;
                    string cellVal = cellContent.Text;

                    if (searchBar.Text == cellVal.Substring(0, inputLength))
                        dg.SelectedItem = dg.Items[i];
                }
            }
        }

        private void Textbox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= Textbox_GotFocus;
            tb.TextChanged += Search;
        }

        private void EditStudentData(object sender, MouseButtonEventArgs e)
        {
            MainWindow.Main.ContentArea.Content = new StudentForm(dg.SelectedItem as StudentData);
        }
    }
}
