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
            sd.status = 2;
            sd.groupBoxes[0] = 2;
            sd.groupBoxes[1] = 6;
            sd.groupBoxes[2] = 9;
            sd.groupBoxes[3] = 12;
            sd.groupBoxes[4] = 14;
            sd.groupBoxes[5] = 17;
            sd.groupBoxes[6] = 23;
            sd.groupBoxes[7] = 26;
            sd.groupBoxes[8] = 32;
            sd.groupBoxes[9] = 35;
            sd.separateMailAddress = false;
            sd.gender = true;
            sd.supportRequired = false;
            sd.newsletterSub = false;
            sd.willProvideThisInfo = true;
            sd.addresses[0] = "122 Smaller Hill";
            sd.addresses[1] = "Big Hill, LA 70508";
            sd.email = "j.marston@gmail.com";
            sd.cellPhoneNum = "111-112-1122";
            sd.homePhoneNum = "N/A";
            sd.schoolName = "DTHS";
            sd.initials = new char[2];
            sd.initials[0] = 'J';
            sd.initials[1] = 'M';

            StudentData sd2 = new StudentData(App.formData.groups.Count);
            sd2.name = "Jim Jammers";
            sd2.status = 1;
            sd2.groupBoxes[0] = 1;
            sd2.groupBoxes[1] = 4;
            sd2.groupBoxes[2] = 9;
            sd2.groupBoxes[3] = 12;
            sd2.groupBoxes[4] = 14;
            sd2.groupBoxes[5] = 19;
            sd2.groupBoxes[6] = 23;
            sd2.groupBoxes[7] = 28;
            sd2.groupBoxes[8] = 32;
            sd2.groupBoxes[9] = 35;
            sd2.separateMailAddress = false;
            sd2.supportRequired = true;
            sd2.newsletterSub = false;
            sd2.gender = true;
            sd2.willProvideThisInfo = true;
            sd2.addresses[0] = "101 NotBooked Ave.";
            sd2.addresses[1] = "Seattle, WA 98105";
            sd2.email = "jammers_on_my_jimmy@hotmail.com";
            sd2.cellPhoneNum = "112-122-1222";
            sd2.homePhoneNum = "N/A";
            sd2.schoolName = "DTHS";
            sd2.initials = new char[2];
            sd2.initials[0] = 'J';
            sd2.initials[1] = 'J';

            StudentData sd3 = new StudentData(App.formData.groups.Count);
            sd3.name = "Dana Disappears";
            sd3.status = 0;
            sd3.groupBoxes[0] = 2;
            sd3.groupBoxes[1] = 5;
            sd3.groupBoxes[2] = 10;
            sd3.groupBoxes[3] = 12;
            sd3.groupBoxes[4] = 14;
            sd3.groupBoxes[5] = 21;
            sd3.groupBoxes[6] = 24;
            sd3.groupBoxes[7] = 29;
            sd3.groupBoxes[8] = 33;
            sd3.groupBoxes[9] = 35;
            sd3.separateMailAddress = false;
            sd3.gender = false;
            sd3.supportRequired = false;
            sd3.newsletterSub = true;
            sd3.willProvideThisInfo = true;
            sd3.addresses[0] = "34 BrokenDoor Dr.";
            sd3.addresses[1] = "Mini China, LA 70803";
            sd3.email = "dananas@yahooligans.com";
            sd3.cellPhoneNum = "122-222-2223";
            sd3.homePhoneNum = "N/A";
            sd3.schoolName = "DTHS";
            sd3.initials = new char[2];
            sd3.initials[0] = 'D';
            sd3.initials[1] = 'D';

            StudentData sd4 = new StudentData(App.formData.groups.Count);
            sd4.name = "Zachary Powers";
            sd4.status = 5;
            sd4.groupBoxes[0] = 2;
            sd4.groupBoxes[1] = 6;
            sd4.groupBoxes[2] = 10;
            sd4.groupBoxes[3] = 12;
            sd4.groupBoxes[4] = 15;
            sd4.groupBoxes[5] = 18;
            sd4.groupBoxes[6] = 24;
            sd4.groupBoxes[7] = 30;
            sd4.groupBoxes[8] = 32;
            sd4.groupBoxes[9] = 35;
            sd4.separateMailAddress = true;
            sd4.gender = true;
            sd4.supportRequired = true;
            sd4.newsletterSub = true;
            sd4.willProvideThisInfo = true;
            sd4.addresses[0] = "150 Shmifty Rd.";
            sd4.addresses[1] = "Abb Villain, LA 70588";
            sd4.addresses[3] = "157 Hans Shmifty Seven Rd.";
            sd4.addresses[4] = "Abb Villain, LA 70588";
            sd4.email = "i_am_zpowers@youtube-mail.com";
            sd4.cellPhoneNum = "223-233-2333";
            sd4.homePhoneNum = "N/A";
            sd4.schoolName = "DTHS";
            sd4.initials = new char[2];
            sd4.initials[0] = 'Z';
            sd4.initials[1] = 'P';

            App.db.items.Add(sd);
            App.db.items.Add(sd2);
            App.db.items.Add(sd3);
            App.db.items.Add(sd4);

            dg.ItemsSource = App.db.items;
            PopulateDatagrid(dg);
        }

        private void PopulateDatagrid(DataGrid grid)
        {
            dg.Columns.Add(new DataGridTextColumn()
            { Header = "Name", Binding = new Binding("name") });
            dg.Columns.Add(new DataGridTextColumn()
            { Header = "Status", Binding = new Binding("status")
            {
                Converter = new IntToKeyConverter(),
                ConverterParameter = App.formData.status
                }
            });
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
            dg.Columns.Add(new DataGridTextColumn()
            {
                Header = "State",
                Binding = new Binding("groupBoxes[2]")
                { Converter = new IntToKeyConverter() }
            });
            dg.Columns.Add(new DataGridTextColumn()
            {
                Header = "Country",
                Binding = new Binding("groupBoxes[3]")
                { Converter = new IntToKeyConverter() }
            });
            dg.Columns.Add(new DataGridTextColumn()
            {
                Header = "Preferred Contact Method",
                Binding = new Binding("groupBoxes[4]")
                { Converter = new IntToKeyConverter() }
            });
            dg.Columns.Add(new DataGridTextColumn()
            {
                Header = "Education",
                Binding = new Binding("groupBoxes[5]")
                { Converter = new IntToKeyConverter() }
            });
            dg.Columns.Add(new DataGridTextColumn()
            {
                Header = "Ethnicity",
                Binding = new Binding("groupBoxes[6]")
                { Converter = new IntToKeyConverter() }
            });
            dg.Columns.Add(new DataGridTextColumn()
            {
                Header = "Race",
                Binding = new Binding("groupBoxes[7]")
                { Converter = new IntToKeyConverter() }
            });
            dg.Columns.Add(new DataGridTextColumn()
            {
                Header = "Referred By",
                Binding = new Binding("groupBoxes[8]")
                { Converter = new IntToKeyConverter() }
            });
            dg.Columns.Add(new DataGridTextColumn()
            {
                Header = "Will Provide Info By",
                Binding = new Binding("groupBoxes[9]")
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
