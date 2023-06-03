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
    public partial class PlayWindow : Window
    {
        public User loggedPlayer = new User();
        private MainWindow mainWindow = new MainWindow();
        public PlayWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            loggedPlayer = mainWindow.userListView.SelectedItem as User;
        }

        private void Button_Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_NewGame(object sender, RoutedEventArgs e)
        {
            GameWindow gameWindow = new GameWindow(this);
            gameWindow.Show();
        }

        private void Button_Statistics(object sender, RoutedEventArgs e)
        {
            Statistics statistics = new Statistics(this);
            statistics.Show();
        }

        private void Button_CustomGame(object sender, RoutedEventArgs e)
        {
            CustomGameMenu customGameMenu = new CustomGameMenu();
            customGameMenu.Show();
        }
    }
}
