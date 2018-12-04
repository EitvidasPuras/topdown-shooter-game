using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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
using GameClient;
using GameClient.Models;
using GameClientWpf;
using GameServer.Models;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Player MyPlayer { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            this.Content = new MainMenu();
        }

        private async void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MyPlayer != null)
            {
                var statusCode = await new RequestsController().DeletePlayerAsync(MyPlayer.Id);
            }
        }
    }
}
