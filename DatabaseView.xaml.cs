using System;
using System.Collections.Generic;
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
            dg.MouseRightButtonUp += Context_MouseRightButtonUp;
        }

        private void DatabaseView_Loaded(object sender, RoutedEventArgs e)
        {
            //student_contentArea.Content = new StudentView();
            //set the databinding for DataGrid
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

            //this is used so the full name doesn't need to be entered
            //in the search bar before finding results
            int inputLength = searchBar.Text.Length;

            //loop through rows (students)
            for (int i = 0; i < dg.Items.Count; ++i)
            {
                //get the Name cell in each row
                DataGridCell cell = DataGridHelper.GetCell(dg, i, 0);

                //get text content
                var cellContent = (TextBlock)cell.Content;

                if (cellContent.Text.Length < inputLength)
                    break;

                string cellVal = cellContent.Text.Substring(0, inputLength);
                //searching is not case-sensitive
                if (searchBar.Text == cellVal ||
                    searchBar.Text == cellVal.ToLower())
                    dg.SelectedItem = dg.Items[i];
            }
        }
        //empty the text box of default text when clicked
        private void Textbox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= Textbox_GotFocus;
            tb.TextChanged += Search;
        }

        private void EditStudentData(object sender, MouseButtonEventArgs e)
        {
            //MainWindow.Main.ContentArea.Content = new StudentForm(dg.SelectedItem as StudentData);
            MainWindow.ChangeState(App.Page.StudentForm, dg.SelectedItem);
        }
        private void GoBack(object sender, RoutedEventArgs e)
        {
            //can't set equal to App.previous because if entered a StudentForm then
            //at this point App.previous is equal to DatabaseView, going no where
            MainWindow.Main.ContentArea.Content = new StartUp();
        }

        private void DeleteItem(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Delete)
            {
                App.db.items.Remove((StudentData)dg.SelectedItem);
                dg.Items.Refresh();
            }
        }

        //private void editButton_Click(object sender, RoutedEventArgs e)
        //{
        //    if (studentInfo_contentArea.Content == null)
        //    {
        //        studentInfo_contentArea.Content = new StudentForm();
        //        editButton.Content = "Done";
        //        //disable datagrid so that studentform doesn't visually overlap

        //    }
        //    else
        //    {
        //        studentInfo_contentArea.Content = null;
        //        editButton.Content = "Edit";
        //    }
        //}

        private void Context_MouseRightButtonUp(object sender, MouseEventArgs e)
        {
            //DependencyObject dep = (DependencyObject)e.OriginalSource;
            //if (dep != null)
            //{
            //    if (dep is DataGridCell)
            //    {
            //        DataGridCell cell = dep as DataGridCell;
            //        cell.Focus();
            //        cell.co
            //    }
            //}
            Console.WriteLine(dg.SelectedIndex);
        }
    }
}
