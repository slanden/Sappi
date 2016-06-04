﻿using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

namespace Sappi
{
    public class XmlSerial
    {
        public static void Write<T>(T val, string path)
        {
            XmlSerializer x = new XmlSerializer(val.GetType());
            StreamWriter writer = new StreamWriter(path);
            x.Serialize(writer, val);
        }

        public static List<T> Read<T>(string path)
        {
            XmlSerializer x = new XmlSerializer(typeof(List<T>));
            StreamReader reader = new StreamReader(path);
            List<T> res = (List<T>)x.Deserialize(reader);
            return res;
        }
    }
}
