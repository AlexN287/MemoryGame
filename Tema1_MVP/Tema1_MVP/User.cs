using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WpfXMLSerialization.MyClasses
{
    [Serializable]
    //[XmlRoot(Namespace = "")]
    public class User
    {
        public string Name { get; set; }
        public string AvatarImagePath { get; set; }
    }
}
