﻿using GameClientWpf;
using GameServer.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for LobbyWindow.xaml
    /// </summary>
    public partial class LobbyWindow : UserControl
    {
        RequestsController requestController = new RequestsController();
        Player myPlayer;
        Uri url;
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        public LobbyWindow()
        {
            InitializeComponent();
            Focus();
        }

        private async void ListView_Loaded(object sender, RoutedEventArgs e)
        {
            if(!await requestController.GameIsStarted(requestController.client.BaseAddress.PathAndQuery))
            {
                url = await requestController.JoinGame();
                myPlayer = await requestController.GetPlayerAsync(url.PathAndQuery);
                myPlayer.SetPlayerTemplate(myPlayer);
                if (myPlayer.IsHost)
                {
                    startgameButton.IsEnabled = false;
                }
                (Application.Current.MainWindow as MainWindow).MyPlayer = myPlayer;

                //System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
                dispatcherTimer.Tick += timer11_TickAsync;
                dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 800);
                dispatcherTimer.Start();
            }
            else
            {
                Application.Current.Shutdown();
            }
            
        }

        private async void timer11_TickAsync(object sender, EventArgs e)
        {
            ICollection<Player> playersList = await requestController.GetAllPlayerAsync(requestController.client.BaseAddress.PathAndQuery);

            bool allPlayersReady = true;
            foreach (Player player in playersList)
            {
                if (player.Id == myPlayer.Id)
                {
                    myPlayer = player;
                    myPlayer.SetPlayerTemplate(myPlayer);
                    ReadyCheckBox.IsChecked = myPlayer.IsReady;
                    ReadyCheckBox.IsEnabled = !myPlayer.IsReady;
                }

                CheckBox checkBox = playerListView.Items.Cast<CheckBox>().Where(i => i.Name == "CheckBox" + player.Id).FirstOrDefault();
                if (checkBox == null)
                {
                    playerListView.Items.Add(new CheckBox
                    {
                        Name = "CheckBox" + player.Id,
                        Content = player.Name,
                        IsChecked = player.IsReady,
                        IsEnabled = false
                    });
                }
                else
                {
                    checkBox.IsChecked = player.IsReady;
                }

                if (!player.IsReady)
                {
                    allPlayersReady = false;
                }
            }

            List<CheckBox> boxestoremove = new List<CheckBox>();
            foreach (CheckBox box in playerListView.Items)
            {
                if (playersList.Where(i => "CheckBox" + i.Id == box.Name).FirstOrDefault() == null)
                {
                    boxestoremove.Add(box);
                }
            }

            foreach (CheckBox box in boxestoremove)
            {
                playerListView.Items.Remove(box);
            }

            if (allPlayersReady)
            {
                if (await requestController.GameIsStarted(requestController.client.BaseAddress.PathAndQuery))
                {
                    dispatcherTimer.Stop();
                    Application.Current.MainWindow.Content = new GameplayWindow(myPlayer, url.ToString());
                }

                if (myPlayer.IsHost)
                {
                    if (await requestController.GameIsFull(requestController.client.BaseAddress.PathAndQuery))
                    {
                        if (await requestController.GameIsReady(requestController.client.BaseAddress.PathAndQuery))
                        {
                            startgameButton.IsEnabled = true;
                        }
                    }

                }
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await myPlayer.Ready();
        }

        private async void startgameButton_Click(object sender, RoutedEventArgs e)
        {
            await requestController.StartGame(requestController.client.BaseAddress.PathAndQuery);
        }
    }
}
