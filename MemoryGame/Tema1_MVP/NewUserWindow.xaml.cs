//using MyClasses;
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
using static System.Net.Mime.MediaTypeNames;

namespace Tema1_MVP
{
    /// <summary>
    /// Interaction logic for NewUserWindow.xaml
    /// </summary>
    public partial class NewUserWindow : Window
    {
        private List<string> avatarImagePaths = new List<string>();
        private static int avatarImageIndex = 0;
        private const int numberOfAvatars = 4;
        private MainWindow mainWindow = new MainWindow();
        public NewUserWindow(MainWindow mainWindow)
        {
            InitializeComponent();

            avatarImagePaths.Add("C:\\facultate\\Anul2\\Sem2\\MVP\\Tema1_MVP\\Tema1_MVP\\images.png");
            avatarImagePaths.Add("C:\\facultate\\Anul2\\Sem2\\MVP\\Tema1_MVP\\Tema1_MVP\\AvatarImage2.jpg");
            avatarImagePaths.Add("C:\\facultate\\Anul2\\Sem2\\MVP\\Tema1_MVP\\Tema1_MVP\\AvatarImage3.png");
            avatarImagePaths.Add("C:\\facultate\\Anul2\\Sem2\\MVP\\Tema1_MVP\\Tema1_MVP\\AvatarImage4.jpg");
            avatarImagePaths.Add("C:\\facultate\\Anul2\\Sem2\\MVP\\Tema1_MVP\\Tema1_MVP\\AvatarImage5.jpg");

            this.mainWindow = mainWindow;
        }

        private void SerializeUser(string name, string avatarImagePath)
        {
            User newUser = new User
            {
                Name = name,
                AvatarImagePath = avatarImagePath
            };

            string filePath = "user.xml";
            List<User> users = new List<User>();
            users = (List<User>)SerializationActions.DeserializeFromXml<List<User>>("user.xml");
            users.Add(newUser);
            SerializationActions.SerializeToXml1(users, filePath);
        }
        private void Button_SignIn(object sender, RoutedEventArgs e)
        {
            string name = NameTextBox.Text;
            string avatarImagePath = AvatarImage.Source.ToString();

            if(name!="")
            {
                List<User> currentUsers = new List<User>();
                currentUsers = (List<User>)SerializationActions.DeserializeFromXml<List<User>>("user.xml");
                bool isValid = true;

                for(int i = 0;i<currentUsers.Count;i++)
                {
                    if (currentUsers[i].Name == name)
                    {
                        MessageBox.Show("This username already exists");
                        isValid = false;
                        break;
                    }
                }

                if(isValid)
                {
                    SerializeUser(name, avatarImagePath);
                    NameTextBox.Text = "";
                    MessageBox.Show("Signed in successful");

                    List<User> users = new List<User>();
                    users = (List<User>)SerializationActions.DeserializeFromXml<List<User>>("user.xml");

                    mainWindow.userListView.ItemsSource = null;
                    mainWindow.userListView.ItemsSource = users;

                }
                
            }
        }

        private void Button_Previous(object sender, RoutedEventArgs e)
        {
            if(avatarImageIndex>0)
            {
                avatarImageIndex--;
                string newImagePath = avatarImagePaths[avatarImageIndex];
                BitmapImage newBitmapImage = new BitmapImage(new Uri(newImagePath)); 
                AvatarImage.Source = newBitmapImage; 
            }
        }
        private void Button_Next(object sender, RoutedEventArgs e)
        {
            if (avatarImageIndex < numberOfAvatars)
            {
                avatarImageIndex++;
                string newImagePath = avatarImagePaths[avatarImageIndex];
                BitmapImage newBitmapImage = new BitmapImage(new Uri(newImagePath)); 
                AvatarImage.Source = newBitmapImage; 
            }
        }

        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
