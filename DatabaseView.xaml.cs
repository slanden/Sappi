using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Sappi
{
    /// <summary>
    /// Interaction logic for DatabaseView.xaml
    /// </summary>
    public partial class DatabaseView : UserControl
    {
        //public static readonly DependencyProperty CurrentColumnProperty = DependencyProperty.Register(
        //"CurrentColumn", typeof(string), typeof(DatabaseView), new PropertyMetadata("Item 1"));
        ComboBox statusBox = new ComboBox();

        public string CurrentColumn { get; set; }
        
        public DatabaseView()
        {
            InitializeComponent();
            //root.DataContext = this;
            dg.MouseRightButtonUp += Context_MouseRightButtonUp;
            statusBox.SelectionChanged += SetStatus;
            CurrentColumn = " ";
            //NameScope.SetNameScope(ContextMenu, NameScope.GetNameScope(this));
        }
        

        private void DatabaseView_Loaded(object sender, RoutedEventArgs e)
        {
            //student_contentArea.Content = new StudentView();
            //set the databinding for DataGrid
            dg.ItemsSource = App.db.items;
            statusBox.ItemsSource = App.formData.status.Keys;
            statusBox.IsEnabled = false;
            PopulateDatagrid(dg);
        }

        /// <summary>
        /// Commands for context menu
        /// </summary>
        private void RowEditCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Edit();
        }
        private void RowEditCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (dg.SelectedItems.Count == 1);
        }
        private void CellEditCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DataGridCell cell = (DataGridCell)e.OriginalSource;

            //edit status only
            (cell.DataContext as StudentData).status = (int)e.Parameter;
            dg.Items.Refresh();
        }
        private void CellEditCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (CurrentColumn == "Status");
        }
        private void RowDeleteCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //are you sure you want to delete?
            Delete();
        }
        private void RowDeleteCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (dg.SelectedItems != null);
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

        private void Edit()
        {
            MainWindow.ChangeState(App.Page.StudentForm, dg.SelectedItem);
        }

        private void Delete()
        {
            for (int i = 0; i < dg.SelectedItems.Count; ++i)
            {
                App.db.items.Remove((StudentData) dg.SelectedItems[i]);
            }
            App.db.items.Remove((StudentData)dg.SelectedItem);
            dg.Items.Refresh();
        }

        private void SetStatus(object sender, SelectionChangedEventArgs e)
        {
            //(dg.SelectedItem as StudentData).status = e.OriginalSource;
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
            if(e.ChangedButton == MouseButton.Left)
                Edit();
        }
        private void GoBack(object sender, RoutedEventArgs e)
        {
            //can't set equal to App.previous because if entered a StudentForm then
            //at this point App.previous is equal to DatabaseView, going no where
            MainWindow.Main.ContentArea.Content = new StartUp();
        }

        private void Datagrid_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Delete)
            {
                Delete();
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
            DependencyObject dep = (DependencyObject)e.OriginalSource;
            if (dep != null)
            {
                //if (dep is DataGridCell)
                //{
                //    DataGridCell cell = dep as DataGridCell;
                //    cell.Focus();
                //    Console.WriteLine(cell.Name);
                //}
                DataGridCell cell = (DataGridCell)(dep as TextBlock).Parent;
                if(cell != null)
                    CurrentColumn = cell.Column.Header.ToString();
            }

            //MouseButtonEventArgs arg = new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, MouseButton.Left);
            //arg.RoutedEvent = MouseLeftButtonDownEvent;
            //dg.RaiseEvent(arg);

            //HitTestResult htr = dg.InputHitTest(e.GetPosition(dg));// .HitTest(dg, e.GetPosition(dg));
            //DataGridRow row = htr.VisualHit.DependencyObjectType<DataGridRow>();
            //if (e.OriginalSource != null)
            //{
            //    StudentData s = ((e.OriginalSource as TextBlock).DataContext) as StudentData;
            //}
            StudentData ss = (StudentData)dg.CurrentItem;


            var row = (sender as DataGrid).SelectedItem;
            //dg.SelectedItem = row;
            //Console.WriteLine(ss.name);
        }

        private void contextMenu_Opened(object sender, RoutedEventArgs e)
        {
            ContextMenu menu = (ContextMenu)e.OriginalSource;
            
            MenuItem item1_lvl1 = (MenuItem)menu.Items[0];
            item1_lvl1.Header = "Edit " + CurrentColumn;

            if(item1_lvl1.Items.Count == 0)
            {
                for (int i = 0; i < App.formData.status.Count; ++i)
                {
                    MenuItem item_lvl2 = new MenuItem();                    
                    item_lvl2.Header = App.formData.status[i];
                    item_lvl2.Command = ContextCommands.EditCell;
                    item_lvl2.CommandParameter = i;

                    item1_lvl1.Items.Add(item_lvl2);
                }
            }
            
        }
        private void contextMenu_Closed(object sender, RoutedEventArgs e)
        {
            ContextMenu menu = (ContextMenu)e.OriginalSource;
            MenuItem item1_lvl1 = (MenuItem)menu.Items[0];

            item1_lvl1.Items.Clear();
        }
    }
}
