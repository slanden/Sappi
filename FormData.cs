using System;
using System.Collections.Generic;
using System.IO;

namespace Sappi
{
    public class FormData
    {
        public Dictionary<int, string> masterList { get; set; }
        public Dictionary<string, int> groups = new Dictionary<string, int>();

        public FormData()
        {
            masterList = new Dictionary<int, string>();
            //path to the bin folder
            string path = Path.GetFullPath(Path.Combine(AppDomain.
                          CurrentDomain.BaseDirectory, "..\\"));
            path += "strings.txt";

            Console.WriteLine(path);
            //read strings from file and put them
            // into lists and create groups
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
        }
    }
}
