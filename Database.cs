using System.Collections.Generic;
using System.Xml.Serialization;

namespace Sappi
{
    [XmlRoot("studentDb")]
    public class Database
    {
        [XmlArray]
        List<StudentData> _db;// = new List<StudentData>();
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
