using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
//using MyClasses;
using System.Collections.ObjectModel;
using System.Runtime.ConstrainedExecution;
using WpfXMLSerialization.MyClasses;
using Tema1_MVP;

namespace WpfXMLSerialization.MyClasses
{
    public class SerializationActions
    {
        public static void SerializeToXml1(List<User> users, string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<User>), new XmlRootAttribute("ArrayOfUser"));
            using (TextWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, users);
            }
        }

        public static void SerializeToXml2(List<SaveGame> users, string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<SaveGame>), new XmlRootAttribute("ArrayOfSaveGame"));
            using (TextWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, users);
            }
        }

        public static T DeserializeFromXml<T>(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (TextReader reader = new StreamReader(filePath))
            {
                return (T)serializer.Deserialize(reader);
            }
        }
    }
}
