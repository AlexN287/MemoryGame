using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfXMLSerialization.MyClasses;

namespace Tema1_MVP
{
    public class SaveGame
    {
        public string Name { get; set; }
        public List<Card> Cards { get; set; }
        public int GamesPlayed { get; set; }
        public int GamesWon { get; set; }
    }
}
