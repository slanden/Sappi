﻿
namespace Sappi
{
    public class StudentData
    {
        public string name { get; set; }
        public int[] groupBoxes { get; set; }
        public string residentialAddress;
        public string email;
        public string cellPhoneNum;
        public string homePhoneNum;
        public string schoolName;
        public bool separateMailAddress;
        public bool willProvideThisInfo;
        public bool gender;
        public bool supportRequired;
        public bool newsletterSub;
        public char[] initials;

        public StudentData(int numGroups)
        {
            groupBoxes = new int[numGroups];
        }
    }
}
