using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace Directories
{
    public class SelectedDirectories
    {
        public static List<string> Directories { get; private set; }

        public static void Clear()
        {
            Directories = new List<string>();
        }

        public static void Load(string filename)
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<string>));
            TextReader reader = new StreamReader(filename);
            Directories = (List<string>)ser.Deserialize(reader);
        }

        public static void SaveCheckedDirectories(string directoryName)
        {
            DirectoryInfo di = new DirectoryInfo(directoryName);
            if (di.Attributes.HasFlag(FileAttributes.System))
                return;
            Directories.Add(di.FullName);
            try
            {
                foreach (var d in di.GetDirectories())
                {
                    SaveCheckedDirectories(d.FullName);
                }
            }
            catch (Exception)
            {
            }
        }

        public static void Save(string filename)
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<string>));
            TextWriter writer = new StreamWriter(filename);
            ser.Serialize(writer, Directories);
        }
    }
}
