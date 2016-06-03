using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Sappi
{
    /// <summary>
    /// Interaction logic for StudentForm.xaml
    /// </summary>
    public partial class StudentForm : UserControl
    {
        public List<ComboBox> groupBoxes = new List<ComboBox>();

        public StudentForm(StudentData sd = null)
        {
            InitializeComponent();
            groupBoxes = FillForm(App.formData, sd);
        }

        List<ComboBox> FillForm(FormData fData, StudentData sd)
        {
            if (sd == null)
            {
                //fill custom-group combobox items
                for (int i = 0; i < fData.groups.Count; ++i)
                {
                    groupBoxes.Add(FindName(fData.groups.ElementAt(i).Key + "Box") as ComboBox);

                    int groupStart = fData.groups.ElementAt(i).Value + 1;
                    int nextGroup = 0;

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
                //fill non-custom group combobox items


            }
            else
            {
                //for compiler
                string[] name = new string[0];

                name = sd.name.Split(' ');
                nameBox1.Text = name[0];
                nameBox2.Text = name[1];
                //status.selected = sd.status;
                //possibly redundant check. Research optimization
                separatemailingaddressBox.SelectedIndex = 
                    sd.separateMailAddress ? 1 : 0;
                resaddressBox1.Text = sd.addresses[0];
                resaddressBox2.Text = sd.addresses[1];
                resaddressBox3.Text = sd.addresses[2];
                if(separatemailingaddressBox.SelectedIndex == 1)
                {
                    mailaddressBox1.Text = sd.addresses[3];
                    mailaddressBox2.Text = sd.addresses[4];
                    mailaddressBox3.Text = sd.addresses[5];
                }
                emailBox.Text = sd.email;
                cellphoneBox.Text = sd.cellPhoneNum;
                homephoneBox.Text = sd.homePhoneNum;
                schoolnameBox.Text = sd.schoolName;
                genderBox.SelectedIndex = sd.gender ? 1 : 0;
                supportrequiredBox.SelectedIndex = sd.supportRequired ? 1 : 0;
                newsletterBox.SelectedIndex = sd.newsletterSub ? 1 : 0;
                willprovideinfoBox.SelectedIndex = sd.willProvideThisInfo ? 1 : 0;
                initialsBox.Text = sd.initials[0].ToString() + sd.initials[1];

                for (int i = 0; i < fData.groups.Count; ++i)
                {
                    #region same as null version
                    groupBoxes.Add(FindName(fData.groups.ElementAt(i).Key + "Box") as ComboBox);

                    int groupStart = fData.groups.ElementAt(i).Value + 1;
                    int nextGroup = 0;


                    if (i == fData.groups.Count - 1)
                        nextGroup = fData.masterList.Count;
                    else
                        nextGroup = fData.groups.ElementAt(i + 1).Value;

                    for (int j = 0; j < nextGroup - groupStart; ++j)
                    {
                        int num = groupStart + j;
                        groupBoxes[i].Items.Add(fData.masterList[num]);
                    }
                    #endregion

                    //select values to represent the StudentData
                    groupBoxes[i].SelectedItem = fData.masterList[sd.groupBoxes[i]];
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
            StudentData sd;
            if (App.db.items.Find(s => s.name == nameBox1.Text + " " + nameBox2.Text) != null)
            {
                sd = App.db.items.Find(s => s.name == nameBox1.Text + " " + nameBox2.Text);
            }
            else
                sd = new StudentData(App.formData.groups.Count);

            //name is used for searching so name fields must be filled in
            if (nameBox1.Text == "" || nameBox2.Text == "" ||
                nameBox1.Text[0] == '(' || nameBox2.Text[0] == '(')
            {
                MessageBox.Show("Must enter full name.");
                return;
            }
            sd.name = nameBox1.Text + " " + nameBox2.Text;
            //possibly redundant check. Research optimization
            sd.separateMailAddress = (separatemailingaddressBox.SelectedIndex == 1) ? true : false;
            sd.addresses[0] = resaddressBox1.Text;
            sd.addresses[1] = resaddressBox2.Text;
            sd.addresses[2] = resaddressBox3.Text;
            if (sd.separateMailAddress == true)
            {
                sd.addresses[3] = mailaddressBox1.Text;
                sd.addresses[4] = mailaddressBox2.Text;
                sd.addresses[5] = mailaddressBox3.Text;
            }
            sd.email = emailBox.Text;
            sd.cellPhoneNum = cellphoneBox.Text;
            sd.homePhoneNum = homephoneBox.Text;
            sd.schoolName = schoolnameBox.Text;
            sd.gender = (genderBox.SelectedIndex == 1) ? true : false;
            sd.supportRequired = (supportrequiredBox.SelectedIndex == 1) ? true : false;
            sd.newsletterSub = (newsletterBox.SelectedIndex == 1) ? true : false;
            sd.willProvideThisInfo = (willprovideinfoBox.SelectedIndex == 1) ? true : false;            
            sd.initials = initialsBox.Text.ToCharArray();

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
            //serialize data
            XmlSerial.Write(App.db.items, App.pathToDatabase);
            //back to database
            MainWindow.Main.ContentArea.Content = new DatabaseView();
        }
        private void Cancel(object a_Sender, RoutedEventArgs a_E)
        {
            MainWindow.Main.ContentArea.Content = new DatabaseView();
        }

        private void DisableMailingAddress(object sender, RoutedEventArgs e)
        {
            mailAddressTextBlock.Visibility = Visibility.Hidden;
            //mailaddressBox1.Visibility = Visibility.Hidden;
            //mailaddressBox2.Visibility = Visibility.Hidden;
            //mailaddressBox3.Visibility = Visibility.Hidden;
        }

        private void EnableMailingAddress(object sender, RoutedEventArgs e)
        {
            mailAddressTextBlock.Visibility = Visibility.Visible;
            //mailaddressBox1.Visibility = Visibility.Visible;
            //mailaddressBox2.Visibility = Visibility.Visible;
            //mailaddressBox3.Visibility = Visibility.Visible;
        }
    }
}
