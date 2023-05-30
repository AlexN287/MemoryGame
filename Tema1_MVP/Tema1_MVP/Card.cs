using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema1_MVP
{
    public class Card
    {
        public string cardImagePath {  get; set; } 
        public string cardBackFace { get; set; }
        public bool isMatched;

        public Card(string cardImagePath, string cardBackFace)
        {
            this.cardImagePath = cardImagePath;
            this.cardBackFace = cardBackFace;
            isMatched = false;
        }
    }
}
