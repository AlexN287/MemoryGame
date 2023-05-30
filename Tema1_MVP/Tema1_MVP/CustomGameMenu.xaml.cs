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

namespace Tema1_MVP
{
    /// <summary>
    /// Interaction logic for CustomGameMenu.xaml
    /// </summary>
    public partial class CustomGameMenu : Window
    {
        public int rows, columns;
        public CustomGameMenu()
        {
            InitializeComponent();
        }

        private void Button_Play(object sender, RoutedEventArgs e)
        {
            bool result = int.TryParse(rowsTextbox.Text, out rows);
            if (!result)
            {
                MessageBox.Show("Invalid input");
            }

            bool result2 = int.TryParse(columnsTextBox.Text, out columns);
            if (!result)
            {
                MessageBox.Show("Invalid input");
            }

            if((rows*columns)%2==1)
            {
                MessageBox.Show("Invalid input");
            }
            else
            {
                CustomGameWindow customGameWindow = new CustomGameWindow(rows, columns);
                customGameWindow.Show();
            }
        }
    }
}
