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
    /// Interaction logic for StudentForm.xaml
    /// </summary>
    public partial class StudentForm : UserControl
    {
        public List<ComboBox> groupBoxes = new List<ComboBox>();

        public StudentForm()
        {
            InitializeComponent();
            groupBoxes = FillForm(App.formData);
        }

        List<ComboBox> FillForm(FormData fData)
        {
            for (int i = 0; i < fData.groups.Count; ++i)
            {
                groupBoxes.Add(FindName(fData.groups.ElementAt(i).Key + "Box") as ComboBox);

                int groupStart = fData.groups.ElementAt(i).Value + 1;
                int nextGroup;


                if (i == fData.groups.Count - 1)
                    nextGroup = fData.masterList.Count;
                else
                    nextGroup = fData.groups.ElementAt(i + 1).Value;

                for (int j = 0; j < nextGroup - groupStart; ++j)
                {
                    int num = groupStart + j;
                    groupBoxes[i].Items.Add(fData.masterList[num]);
                }
            }
            return groupBoxes;
        }

        //a duplicate of this function exists elsewhere
        private void Textbox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= Textbox_GotFocus;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            StudentData sd = new StudentData(App.formData.groups.Count);

            //name is used for searching so name fields must be filled in
            if (nameBox1.Text == "" || nameBox2.Text == "")
            {
                MessageBox.Show("Must enter full name.");
                return;
            }

            sd.name = nameBox1.Text + " " + nameBox2.Text;

            //convert masterList(dictionary) to a list to access the string's index
            List<string> vals = App.formData.masterList.Values.ToList();
            for (int i = 0; i < groupBoxes.Count; ++i)
            {
                if (groupBoxes[i].SelectedItem == null)
                {
                    sd.groupBoxes[i] = -1;
                }
                else
                    sd.groupBoxes[i] = vals.IndexOf(groupBoxes[i].SelectedItem.ToString());
            }
            App.db.items.Add(sd);

            //back to home
            MainWindow.Main.ContentArea.Content = new StartUp();
        }
    }
}
