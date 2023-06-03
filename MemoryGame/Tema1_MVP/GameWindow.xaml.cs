using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using WpfXMLSerialization.MyClasses;

namespace Tema1_MVP
{
    public partial class GameWindow : Window
    {
        private User loggedPlayer = new User();
        private PlayWindow playWindow = null;

        private int level = 1;
        private int countDown = 200;
        private int countDownLevel2 = 120;
        private int countDownLevel3 = 60;
        private bool isWin = false;
        private bool isStartPressed = false;
        public List<Card> ButtonMatrix { get; set; }

        private void initializeCards(List<Card> cards)
        {
            for (int i = 0; i < 2; i++)
                for (int j = 1; j <= 8; j++)
                {
                    cards.Add(new Card("C:\\facultate\\Anul2\\Sem2\\MVP\\Tema1_MVP\\Tema1_MVP\\CardImage" + j + ".png", "C:\\facultate\\Anul2\\Sem2\\MVP\\Tema1_MVP\\Tema1_MVP\\CardsBackFace.jpg"));
                }
        }
        public GameWindow(PlayWindow playWindow)
        {
            InitializeComponent();
            this.playWindow = playWindow;
            loggedPlayer = playWindow.loggedPlayer;
            PlayerNameLabel.Content = "Player name:" + loggedPlayer.Name;
            AvatarImage.Source = new BitmapImage(new Uri(loggedPlayer.AvatarImagePath));

            ButtonMatrix = new List<Card>();
            initializeCards(ButtonMatrix);
            sortRandom(ButtonMatrix);
            sortRandom(ButtonMatrix);
            DataContext = this;

            gameLevel.Content = "Level: "+ level;
            timerLabel.Content = "Timer" + countDown;
        }

        public class ButtonData
        {
            public string ImagePath { get; set; }
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
        private void MenuItem_About(object sender, RoutedEventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.Show();
        }

        private void MenuItem_Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        DispatcherTimer timer;
        
        private void Timer_Tick(object sender, EventArgs e)
        {
            countDown--;
            timerLabel.Content=countDown.ToString();

            if(countDown == 0)
            {
                timer.Stop();
                MessageBox.Show("You run out of time! Please try again.");
                List<Card> newList = new List<Card>();

                initializeCards(newList);

                ButtonMatrix = null;
                ButtonMatrix = newList;
       
                sortRandom(ButtonMatrix);
                sortRandom(ButtonMatrix);

                itemsControl.ItemsSource = null;
                itemsControl.ItemsSource = ButtonMatrix;

                DataContext = this;
            }
        }

        private void startTimer()
        {
            if (timer != null)
            {
                timer.Stop();
            }
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Button_Start(object sender, RoutedEventArgs e)
        {
            startTimer();
            isStartPressed = true;
        }

        private void SerializeSaveGame(string name, List<Card> cards, int gamesPlayed, int gamesWon)
        {
            SaveGame newSaving = new SaveGame
            {
                Name = name,
                Cards = cards,
                GamesPlayed = gamesPlayed,
                GamesWon = gamesWon
            };

            string filePath = "savings.xml";
            List<SaveGame> savings = new List<SaveGame>();
            savings = (List<SaveGame>)SerializationActions.DeserializeFromXml<List<SaveGame>>("savings.xml");
            savings.Add(newSaving);
            SerializationActions.SerializeToXml2(savings, filePath);
        }
        private void MenuItem_SaveGame(object sender, RoutedEventArgs e)
        {
            List<SaveGame> savings = new List<SaveGame>();
            savings = (List<SaveGame>)SerializationActions.DeserializeFromXml<List<SaveGame>>("savings.xml");

            int gamesPlayed = 0;
            int gamesWon = 0;

            for(int i=0; i < savings.Count;i++)
            {
                if (savings[i].Name == loggedPlayer.Name.ToString())
                {
                    gamesPlayed = savings[i].GamesPlayed + 1;
                    gamesWon = savings[i].GamesWon + 1;
                }
            }

            SerializeSaveGame(loggedPlayer.Name.ToString(), ButtonMatrix, gamesPlayed, gamesWon);
        }
        private void Button_NextLevel(object sender, RoutedEventArgs e)
        {
            if(isWin)
            {
                if(level<=3)
                {
                    List<Card> newList = new List<Card>();

                    
                    initializeCards(newList);
                    ButtonMatrix = null;
                    ButtonMatrix = newList;

                    sortRandom(ButtonMatrix);
                    sortRandom(ButtonMatrix);

                    itemsControl.ItemsSource = null;
                    itemsControl.ItemsSource = ButtonMatrix;

                    DataContext = this;

                    if (level == 2)
                        countDown = countDownLevel2;
                    else
                        countDown = countDownLevel3;

                    gameLevel.Content = "Level: " + level;
                }
                isWin = false;
            }
            else
            {
                MessageBox.Show("First you should finish the current level");
            }
        }

        private Card firstCard = null;

        private Button previousButton; 

        private int numberOfPairs = 0;
        private async void Button_Cards(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var cardButton = (Card)button.DataContext;

            if (cardButton.isMatched == false && isStartPressed == true)
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

            if(numberOfPairs == 8)
            {
                MessageBox.Show("Congratulations, you win!");
                level++;
                isWin = true;
                timer.Stop();
                isStartPressed = false;
                numberOfPairs = 0;
            }

            if(level==3)
            {
                MessageBox.Show("Congratulations, you win the game!");
            }
        }


    }
}
