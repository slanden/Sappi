using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Sappi
{
    public class FormData
    {
        public Dictionary<int, string> masterList = new Dictionary<int, string>();
        public Dictionary<string, int> groups = new Dictionary<string, int>();

        public FormData()
        {
            ////path to the bin folder
            //string path = Path.GetFullPath(Path.Combine(AppDomain.
            //              CurrentDomain.BaseDirectory, "..\\"));
            //path += "strings.txt";

            ////read strings from file and put them
            //// into lists and create groups
            //int count = 0;
            //foreach (string s in File.ReadLines(path))
            //{
            //    if (!string.IsNullOrWhiteSpace(s))
            //    {
            //        //check if string should be a group
            //        if (s.StartsWith("#"))
            //            groups.Add(s.Substring(1).ToLower(), count);

            //        //add the string to the master list of strings
            //        masterList.Add(count, s);
            //        count++;
            //    }
            //}
        }
    }
}
