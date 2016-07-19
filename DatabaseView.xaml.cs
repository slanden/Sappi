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
        public string CurrentColumn { get; set; }
        
        public DatabaseView()
        {
            InitializeComponent();
            //root.DataContext = this;
            dg.MouseRightButtonUp += Context_MouseRightButtonUp;
            CurrentColumn = " ";
        }        

        private void DatabaseView_Loaded(object sender, RoutedEventArgs e)
        {
            //set the databinding for DataGrid
            dg.ItemsSource = App.db.items;
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
        }

        private void Edit()
        {
            MainWindow.ChangeState(App.Page.StudentForm, dg.SelectedItem);
        }

        private void Delete()
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", 
                                                "Delete Confirmation", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                for (int i = 0; i < dg.SelectedItems.Count; ++i)
                {
                    App.db.items.Remove((StudentData)dg.SelectedItems[i]);
                }
                App.db.items.Remove((StudentData)dg.SelectedItem);
                dg.Items.Refresh();
            }            
        }

        private void Search(object sender, TextChangedEventArgs e)
        {
            if (searchBar.Text == "" || searchBar.Text == "(search)")
                return;
            
            int inputLength = searchBar.Text.Length;

            //loop through rows
            for (int i = 0; i < dg.Items.Count; ++i)
            {
                //get the "Name" cell in each row
                DataGridCell cell = DataGridHelper.GetCell(dg, i, 0);

                //get text content
                var cellContent = (TextBlock)cell.Content;
                //check for out of range exception
                if (cellContent.Text.Length < inputLength)
                    break;

                string cellVal = cellContent.Text.Substring(0, inputLength);
                //searching is not case-sensitive
                if (searchBar.Text == cellVal ||
                    searchBar.Text == cellVal.ToLower())
                    dg.SelectedItem = dg.Items[i];
            }
        }
        
        private void Textbox_GotFocus(object sender, RoutedEventArgs e)
        {
            //empty the text box of default text when clicked
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= Textbox_GotFocus;
            tb.TextChanged += Search;
        }

        private void EditStudentData(object sender, MouseButtonEventArgs e)
        {
            Edit();
        }

        private void Datagrid_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Delete)
                Delete();
        }

        private void Context_MouseRightButtonUp(object sender, MouseEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;
            if (dep is TextBlock)
            {
                DataGridCell cell = (DataGridCell)(dep as TextBlock).Parent;
                if(cell != null)
                    CurrentColumn = cell.Column.Header.ToString();
            }
        }
        
        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            //     Can't set equal to App.previous because if user entered a StudentForm,
            // at this point App.previous == current (DatabaseView). Perhaps in the
            // future, save a history of previous states so that if App.previous[0] ==
            // current, set content = App.previous[1]
            MainWindow.Main.ContentArea.Content = new StartUp();
        }

        private void newAppButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.ChangeState(App.Page.StudentForm);
        }

        private void contextMenu_Opening(object sender, ContextMenuEventArgs e)
        {
            DataGridRow row;
            if(dg.SelectedItem != null)
            {
                row = DataGridHelper.GetRow(dg, dg.SelectedIndex);

                // |item_lvl1|
                // |item_lvl1| -> |item_lvl2|
                //                |item_lvl2|
                MenuItem item1_lvl1 = (MenuItem)row.ContextMenu.Items[0];
                item1_lvl1.Header = "Edit " + CurrentColumn;

                if (CurrentColumn == "Status")
                {
                    item1_lvl1.IsEnabled = true;
                    //add submenu items
                    if (item1_lvl1.Items.Count == 0)
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
                else
                {
                    item1_lvl1.IsEnabled = false;
                    item1_lvl1.Items.Clear();
                }
            }
        }
    }

}
