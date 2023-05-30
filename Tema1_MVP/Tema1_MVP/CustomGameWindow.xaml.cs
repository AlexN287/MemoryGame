using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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

namespace Tema1_MVP
{
    public partial class CustomGameWindow : Window
    {
        private int rows, columns;
        public List<Card> ButtonMatrix { get; set; }

        private void initializeCards(List<Card> cards)
        {
                for (int j = 1; j <= (columns * rows)/2; j++)
                {
                    cards.Add(new Card("C:\\facultate\\Anul2\\Sem2\\MVP\\Tema1_MVP\\Tema1_MVP\\CardImage" + j + ".png", "C:\\facultate\\Anul2\\Sem2\\MVP\\Tema1_MVP\\Tema1_MVP\\CardsBackFace.jpg"));
                    cards.Add(new Card("C:\\facultate\\Anul2\\Sem2\\MVP\\Tema1_MVP\\Tema1_MVP\\CardImage" + j + ".png", "C:\\facultate\\Anul2\\Sem2\\MVP\\Tema1_MVP\\Tema1_MVP\\CardsBackFace.jpg"));
                }
        }

        private void sortRandom(List<Card> list)
        {
            Random random = new Random();

            for (int i = 0; i < list.Count - 1; i++)
            {
                int index = random.Next(i, list.Count);
                Card tmp = list[index];
                list[index] = list[i];
                list[i] = tmp;
            }
        }
        public CustomGameWindow(int rows, int columns)
        {
            InitializeComponent();
            this.rows = rows;
            this.columns = columns;
            

            ButtonMatrix = new List<Card>();

            initializeCards(ButtonMatrix);
            sortRandom(ButtonMatrix);
            sortRandom(ButtonMatrix);
            DataContext = this;
        }

        private Card firstCard = null;

        private Button previousButton;

        private int numberOfPairs = 0;
        private async void Button_Cards(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var cardButton = (Card)button.DataContext;

            if (cardButton.isMatched == false)
            {
                BitmapImage newBitmapImage = new BitmapImage(new Uri(cardButton.cardImagePath));
                Image newImage = new Image() { Source = newBitmapImage };
                button.Content = newImage;

                if (firstCard == null)
                {
                    firstCard = cardButton;
                    previousButton = button;

                }
                else if (firstCard != cardButton && firstCard.cardImagePath == cardButton.cardImagePath)
                {
                    firstCard.isMatched = true;
                    cardButton.isMatched = true;
                    firstCard = null;
                    numberOfPairs++;
                }
                else
                {
                    await Task.Delay(500);

                    firstCard = null;
                    BitmapImage backBitmapImage = new BitmapImage(new Uri(cardButton.cardBackFace));
                    Image defaultImage = new Image() { Source = backBitmapImage };
                    button.Content = defaultImage;

                    if (previousButton != null)
                    {
                        previousButton.Content = new Image() { Source = backBitmapImage };
                    }
                    previousButton = button;
                }
            }

            if (numberOfPairs == (rows*columns)/2)
            {
                MessageBox.Show("Congratulations, you win!");
                
                numberOfPairs = 0;
            }

            
        }
    }
}
