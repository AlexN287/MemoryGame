using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfXMLSerialization.MyClasses;

namespace Tema1_MVP
{
    /// <summary>
    /// Interaction logic for Statistics.xaml
    /// </summary>
    public partial class Statistics : Window
    {
        private User loggedPlayer;
        private PlayWindow playWindow;
        public Statistics(PlayWindow playWindow)
        {
            InitializeComponent();
            this.playWindow = playWindow;
            List<SaveGame> savings = new List<SaveGame>();
            savings = (List<SaveGame>)SerializationActions.DeserializeFromXml<List<SaveGame>>("savings.xml");

            int gamesPlayed = 0;
            int gamesWon = 0;

            for (int i = 0; i < savings.Count; i++)
            {
                if (savings[i].Name == loggedPlayer.Name.ToString())
                {
                    gamesPlayed = savings[i].GamesPlayed;
                    gamesWon = savings[i].GamesWon;
                    break;
                }
            }

            nameLabel.Content = "Name" + loggedPlayer.Name.ToString();
            gamesPlayedLabel.Content = "Games Played " + gamesPlayed;
            gamesWonLabel.Content = "Games Won " + gamesWon;
        }
    }
}
