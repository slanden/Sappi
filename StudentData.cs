
namespace Sappi
{
    public class StudentData
    {
        public string name { get; set; }
        public int[] groupBoxes { get; set; }
        public int[] DOB;
        public int status { get; set; }
        public int educationYear;
        public string[] addresses { get; set; }
        public string email { get; set; }
        public string cellPhoneNum { get; set; }
        public string homePhoneNum { get; set; }
        public string schoolName { get; set; }
        public bool separateMailAddress { get; set; }
        public bool gender { get; set; }
        public bool supportRequired { get; set; }
        public bool newsletterSub { get; set; }
        public bool willProvideThisInfo;
        public char[] initials;

        //parameterless constructor needed for xmlSerializer
        public StudentData(){}
        public StudentData(int numGroups)
        {
            groupBoxes = new int[numGroups];
            addresses = new string[6];
            DOB = new int[3];
        }
    }
}
