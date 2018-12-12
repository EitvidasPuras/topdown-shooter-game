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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : UserControl
    {
        public MainMenu()
        {
            InitializeComponent();
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(CustomNickname.Text.Length <= 0)
            {
                ErrorLabel.Visibility = Visibility.Visible;
                ErrorLabel.Foreground = Brushes.DarkRed;
                ErrorLabel.Content = "Please type in a nickname";
            }
            else
            {
                Application.Current.MainWindow.Content = new LobbyWindow(CustomNickname.Text);
            }
        }
    }
}
