using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sappi
{
    public class Database
    {
        List<StudentData> _db = new List<StudentData>();
        public List<StudentData> items
        {
            get { return _db; }
            set { _db = value; }
        }

        public void Save()
        {

            ////convert masterList(dictionary) to a list to access the string's index
            //List<string> vals = choices.masterList.Values.ToList();
            //for (int i = 0; i < groupBoxes.Count; ++i)
            //{
            //    if (groupBoxes[i].SelectedItem == null)
            //    {
            //        app.groupBoxes[i] = -1;
            //    }
            //    else
            //        app.groupBoxes[i] = vals.IndexOf(groupBoxes[i].SelectedItem.ToString());
            //}
            //db.applications.Add(app);
        }
    }
}
