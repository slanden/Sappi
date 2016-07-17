using System;
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

        private void FillNumbers(ComboBox c, int endPt, int startPt = 0)
        {
            int length = Math.Abs(endPt - startPt);
            if (endPt < startPt)
            {
                for (int i = 0; i < length +1; ++i)
                    c.Items.Add((startPt - i).ToString());
            }
            else
            {
                for (int i = 0; i < length +1; ++i)
                    c.Items.Add((startPt + i).ToString());
            }
        }

        List<ComboBox> FillForm(FormData fData, StudentData sd)
        {
            if (sd == null)
            {
                //fill non-hardcoded group combobox items
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
                //fill hardcoded group combobox items
                int currentYr = DateTime.Now.Year;
                FillNumbers(educationyearBox, currentYr - 100, currentYr);

            }
            else
            {
                string[] name;

                name = sd.name.Split(' ');
                nameBox1.Text = name[0];
                nameBox2.Text = name[1];
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
                initialsBox.Text = (sd.initials.Length == 2) ? sd.initials[0].ToString() + sd.initials[1] : null;

                //set Date of Birth text
                if(sd.DOB != null)
                {
                    dobDayBox.Text = sd.DOB[0].ToString();
                    dobMonthBox.Text = sd.DOB[1].ToString();
                    dobYearBox.Text = sd.DOB[2].ToString();
                }
                
                //fill non-custom group combobox items
                int currentYr = DateTime.Now.Year;
                FillNumbers(educationyearBox, currentYr - 100, currentYr);
                educationyearBox.SelectedItem = sd.educationYear.ToString();

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
                    if(sd.groupBoxes[i] != -1)
                        groupBoxes[i].SelectedItem = fData.masterList[sd.groupBoxes[i]];

                }
            }

            return groupBoxes;
        }

        //a duplicate of this function exists elsewhere.
        //find a way to re-use the code
        private void Textbox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= Textbox_GotFocus;
        }

        private void Textbox_MoveToNext(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            //don't do anything if the user did not click on the textbox
            if (!tb.IsFocused)
                return;

            //only allow 2 characters for MM or DD textboxes
            if(tb.Text.Length == 2 && tb.Text != string.Empty)
            {
                //textboxes are inside a grid element in order to move
                //from one child to the next without needing their names
                int currentChild = dobGrid.Children.IndexOf(tb);
                if(dobGrid.Children[currentChild + 1] != null)
                    dobGrid.Children[currentChild + 1].Focus();
            }
        }
        private void Textbox_LimitCharacters(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;

            if (!tb.IsFocused)
                return;

            if (tb.Text.Length > 2 && tb.Text != string.Empty)
            {            
                tb.Text = tb.Text.Remove(2);
                tb.CaretIndex = tb.Text.Length;
            }
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            StudentData sd = new StudentData(App.formData.groups.Count);

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
            sd.educationYear = (educationyearBox.SelectedItem == null) ? 0 : Convert.ToInt32(educationyearBox.SelectedItem);
            sd.gender = (genderBox.SelectedIndex == 1) ? true : false;
            sd.supportRequired = (supportrequiredBox.SelectedIndex == 1) ? true : false;
            sd.newsletterSub = (newsletterBox.SelectedIndex == 1) ? true : false;
            sd.willProvideThisInfo = (willprovideinfoBox.SelectedIndex == 1) ? true : false;            
            sd.initials = initialsBox.Text.ToCharArray();

            //Date of Birth int array in the format: DOB[0]dd / DOB[1]mm / DOB[2]yyyy
            if(dobDayBox.Text != "DD" && dobMonthBox.Text != "MM" && dobYearBox.Text != "YYYY")
            {
                sd.DOB[0] = Convert.ToInt32(dobDayBox.Text);
                sd.DOB[1] = Convert.ToInt32(dobMonthBox.Text);
                sd.DOB[2] = Convert.ToInt32(dobYearBox.Text);
            }            

            //convert masterList(dictionary) to a list to access the string's index
            List<string> vals = App.formData.masterList.Values.ToList();
            for (int i = 0; i < groupBoxes.Count; ++i)
            {
                if (groupBoxes[i].SelectedItem == null)
                    sd.groupBoxes[i] = -1;
                else
                    sd.groupBoxes[i] = vals.IndexOf(groupBoxes[i].SelectedItem.ToString());
            }
            
            //check if student exists in database to avoid duplicates
            bool isExistingStudent = false;
            for (int i = 0; i < App.db.items.Count; ++i)
            {
                if (App.db.items[i].name == sd.name)
                {
                    App.db.items[i] = sd;
                    isExistingStudent = true;
                }
            }
            if(!isExistingStudent)
                App.db.items.Add(sd);

            //serialize data
            XmlSerial.Write(App.db.items, App.pathToDatabase);
            //back to database
            MainWindow.Main.ContentArea.Content = new DatabaseView();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            //foreach control-> if content changed,
            //ask, "are you sure you want to cancel?"
            MainWindow.Main.ContentArea.Content = App.previous;
            //Console.WriteLine("-- " + GetType().Name);
            //MainWindow.GoToPreviousPage();
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
