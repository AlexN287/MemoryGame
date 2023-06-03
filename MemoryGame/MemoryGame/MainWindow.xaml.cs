using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfXMLSerialization.MyClasses;

namespace Tema1_MVP
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            List<User> users = new List<User>();
            users = (List<User>)SerializationActions.DeserializeFromXml<List<User>>("user.xml");
            userListView.ItemsSource = users;
            userListView.SelectionChanged += UserListView_SelectionChanged;
        }

        private void UserListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            User selectedUser = userListView.SelectedItem as User;

            if (selectedUser != null)
            {
                AvatarImage.Source = new BitmapImage(new Uri(selectedUser.AvatarImagePath));
            }
        }
        private void Button_Play(object sender, RoutedEventArgs e)
        {
            if (userListView.SelectedItem == null)
            {
                MessageBox.Show("You need to login first");
            }
            else
            {
                PlayWindow playWindow = new PlayWindow(this);
                playWindow.Show();
            }
        }
        private void Button_NewUser(object sender, RoutedEventArgs e)
        {
            NewUserWindow newUserWindow = new NewUserWindow(this);
            newUserWindow.Show();
        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            var selectedItem = userListView.SelectedItem as User;
            List<User> users = new List<User>();
            users = (List<User>)SerializationActions.DeserializeFromXml<List<User>>("user.xml");
     
            for(int i = 0; i < users.Count; i++)
            {
                if (users[i].Name == selectedItem.Name)
                {
                    users.RemoveAt(i);
                }
            }

           
            string filePath = "user.xml";
            SerializationActions.SerializeToXml1(users, filePath);
            userListView.ItemsSource = null;
            userListView.ItemsSource = users;
        }

        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
