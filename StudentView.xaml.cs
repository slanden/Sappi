using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for StudentView.xaml
    /// </summary>
    public partial class StudentView : UserControl
    {
        public StudentView()
        {
            InitializeComponent();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            if (studentInfo_contentArea.Content == null)
            {
                studentInfo_contentArea.Content = new StudentForm();
                editButton.Content = "Done";
                //disable datagrid so that studentform doesn't visually overlap
                
            }
            else
            {
                studentInfo_contentArea.Content = null;
                editButton.Content = "Edit";
            }
        }

    }
}
