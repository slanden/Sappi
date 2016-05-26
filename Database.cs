using System.Collections.Generic;

namespace Sappi
{
    public class Database
    {
        List<StudentData> _db;
        public List<StudentData> items
        {
            get { return _db; }
            set { _db = value; }
        }

        public Database()
        {
            _db = new List<StudentData>();
        }

    }
}
