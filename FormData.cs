using System.Collections.Generic;
using System.IO;

namespace Sappi
{
    public class FormData
    {
        public Dictionary<int, string> masterList { get; set; }
        public Dictionary<string, int> groups = new Dictionary<string, int>();
        public Dictionary<int, string> status = new Dictionary<int, string>();

        public FormData()
        {
            masterList = new Dictionary<int, string>();
            //path to the bin folder
            //string path = Path.GetFullPath(Path.Combine(AppDomain.
            //              CurrentDomain.BaseDirectory, "..\\"));
            string path = "..\\strings.txt";

            //    read strings from file, put them
            // into lists, and create groups
            int count = 0;
            foreach (string s in File.ReadLines(path))
            {
                if (!string.IsNullOrWhiteSpace(s))
                {
                    //check if string should be a group
                    if (s.StartsWith("#"))
                        groups.Add(s.Substring(1).ToLower(), count);

                    //add the string to the master list of strings
                    masterList.Add(count, s);
                    count++;
                }
            }

            //"status" strings
            status.Add(0, "lost");
            status.Add(1, "offered");
            status.Add(2, "applied");
            status.Add(3, "interviewed");
            status.Add(4, "accepted");
            status.Add(5, "enrolled");
        }
    }
}
