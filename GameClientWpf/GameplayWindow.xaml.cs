﻿using GameClient;
using GameClient.Models;
using GameClientWpf;
using GameServer.Models;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class GameplayWindow : UserControl
    {
        Player myPlayer;
        RequestsController requestController = new RequestsController();
        Line laser;

        SolidColorBrush darkBrush = new SolidColorBrush();
        SolidColorBrush whiteBrush = new SolidColorBrush();

        System.Windows.Threading.DispatcherTimer laserTimer = new System.Windows.Threading.DispatcherTimer();

        public GameplayWindow(Player myPlayer, string url)
        {
            InitializeComponent();
            this.myPlayer = myPlayer;
            myPlayer.setState();
            url1 = url;
        }

        string url1 = "";
        List<GameServer.Models.Player> listOfPlayers = new List<GameServer.Models.Player>();
        //List<GameServer.Models.Weapon> listOfWeapons = new List<GameServer.Models.Weapon>();
        ICollection<Obstacle> obstacleList;
        ICollection<Weapon> weaponsList;
        ICollection<Player> playersList;
        List<GameServer.Interfaces.ISkin> listOfDrawers = new List<GameServer.Interfaces.ISkin>();
        bool moved = false;

        Dictionary<long, GameImage> UIPlayers = new Dictionary<long, GameImage>();
        Dictionary<long, GameImage> UIWeapons = new Dictionary<long, GameImage>();
        Dictionary<long, Label> UIPlayerNames = new Dictionary<long, Label>();

        ObserverController observer = new ObserverController();
        FlyweightFactory _flyweightFactory = new FlyweightFactory();

        // pagetina visus playerius ir juos atvaizduoja, vėliau sukuria playeri esančiam klientui
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {

            Console.WriteLine("0)\tGet all player");

            playersList = await requestController.GetAllPlayerAsync(requestController.client.BaseAddress.PathAndQuery);
            weaponsList = await requestController.GetAllWeaponsAsync(requestController.client.BaseAddress.PathAndQuery);
            obstacleList = await requestController.GetAllObstacleAsync(requestController.client.BaseAddress.PathAndQuery);


            foreach (Weapon w in weaponsList)
            {
                Form1_PaintWeapons(w);
            }

            Random rnd = new Random();


            foreach (Player p in playersList)
            {
                listOfPlayers.Add(p);
                Form1_PaintPlayer(p);
            }

            foreach (Obstacle o in obstacleList)
            {
                Shape obst = null;
                if (o is GameServer.Models.Circle)
                {
                    obst = new System.Windows.Shapes.Ellipse();     
                }
                else if (o is GameServer.Models.Rectangle)
                {
                    obst = new System.Windows.Shapes.Rectangle();
                }
                obst.Stroke = Brushes.Blue;
                obst.StrokeThickness = 2;
                obst.RenderTransform = new TranslateTransform(o.PosX - 400, o.PosY - 300);
                obst.Fill = new SolidColorBrush(Colors.DarkGreen);
                obst.Width = o.Width;
                obst.Height = o.Height;
                LayoutRoot.Children.Add(obst);
            }

            // Create a Line  
            laser = new Line();
            laser.X1 = 50;
            laser.Y1 = 50;
            laser.X2 = 200;
            laser.Y2 = 200;

            whiteBrush.Color = Colors.GhostWhite;
            darkBrush.Color = Colors.Black;

            // Set Line's width and color  
            laser.StrokeThickness = 1;
            laser.Stroke = whiteBrush;

            // Add line to the Grid.  
            LayoutRoot.Children.Add(laser);

            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += timer1_TickAsync;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 150);
            dispatcherTimer.Start();

            laserTimer.Tick += laserColorTimer;
            laserTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000);

            Focus();
        }

        private void Form1_PaintPlayer(Player player)
        {
            GameImage img;
            Label playerName = null;
            if (UIPlayers.TryGetValue(player.Id, out img)
                && UIPlayerNames.TryGetValue(player.Id, out playerName))
            {
                img.Move((int)player.PosX, (int)player.PosY);
                playerName.RenderTransform = new TranslateTransform(player.PosX - 400, player.PosY - 320);
            }
            else
            {
                img = (GameImage)_flyweightFactory.GetFlyweight("Player").Clone();
                img.Move((int)player.PosX, (int)player.PosY);
                LayoutRoot.Children.Add(img);
                UIPlayers.Add(player.Id, img);
                playerName = CreateNameLabel(player.Name);
                UIPlayerNames.Add(player.Id, playerName);
                LayoutRoot.Children.Add(playerName);
                playerName.RenderTransform = new TranslateTransform((int)player.PosX - 400, (int)player.PosY - 320);
                
            }
            if (player.GetEquippedWeapon() != null)
            {
                UpdateWeaponPos(player.GetEquippedWeapon());
            }

            if (laser != null && player.Id == myPlayer.Id)
            {
                laser.X1 = player.PosX;
                laser.Y1 = player.PosY;
                laser.RenderTransform = new TranslateTransform();
            }
        }

        private Label CreateNameLabel(string name)
        {
            Label label = new Label();
            label.Width = 100;
            label.Height = 30;
            label.FontSize = 10;
            label.Content = name;
            label.FontFamily = new FontFamily("Consolas");
            label.Foreground = Brushes.Black;
            return label;
        }

        private void Form1_PaintWeapons(Weapon weapon)
        {
            GameImage img = (GameImage)_flyweightFactory.GetFlyweight(weapon.GetType().Name).Clone();
            LayoutRoot.Children.Add(img);
            img.Move((int)weapon.PosX, (int)weapon.PosY);
            UIWeapons.Add(weapon.Id, img);
        }

        private void UpdateWeaponPos(Weapon weapon)
        {
            GameImage img;
            if (UIWeapons.TryGetValue(weapon.Id, out img))
            {
                bool visible = false;
                if (!weapon.isOnTheGround)
                {
                    foreach (Player p in playersList)
                    {
                        if (p.GetEquippedWeapon() != null && p.GetEquippedWeapon().Id == weapon.Id)
                        {
                            if (p.Id == myPlayer.Id)
                                img.Move((int)myPlayer.PosX, (int)myPlayer.PosY - 10);
                            //img.RenderTransform = new TranslateTransform(myPlayer.PosX - 400, myPlayer.PosY - 300 - 10);
                            else
                                img.Move((int)p.PosX, (int)p.PosY);
                          //  img.RenderTransform = new TranslateTransform(p.PosX - 400, p.PosY - 300 - 10);
                            visible = true;
                            img.Visibility = Visibility.Visible;
                            break;
                        }
                    }
                }
                if (!visible)
                {
                    visible = weapon.isOnTheGround;
                    if (visible)
                    {
                        img.Visibility = Visibility.Visible;
                        img.Move((int)weapon.PosX, (int)weapon.PosY);
                        //img.RenderTransform = new TranslateTransform(weapon.PosX - 400, weapon.PosY - 300);
                    }
                    else
                    {
                        img.Visibility = Visibility.Hidden;
                    }
                }
            }
        }

        //timeris getina visus playerius ir atvaizduoja pagal koordinates
        private async void timer1_TickAsync(object sender, EventArgs e)
        {
            if (moved)
            {
                var patchStatusCode = await requestController.UpdatePlayerAsync(myPlayer);
                moved = false;
            }
            
            bool updated = await observer.CheckIfChangedAsync(requestController.client.BaseAddress.ToString(), myPlayer);
            if (updated)
            {
                weaponsList = await requestController.GetAllWeaponsAsync(requestController.client.BaseAddress.PathAndQuery);
                playersList = await requestController.GetAllPlayerAsync(requestController.client.BaseAddress.PathAndQuery);

                foreach (Player p in playersList)
                {
                    Player oldPlayer = listOfPlayers.Find(i => i.Id == p.Id);
                    if (oldPlayer != null && p.Health > 0)
                    {
                        if (!oldPlayer.checkEquality(p))
                        {
                            if (p.Id == myPlayer.Id)
                            {
                                myPlayer = p;
                                myPlayer.setState();

                            }
                            else
                            {
                                this.Form1_PaintPlayer(p);
                            }
                            

                            int index = listOfPlayers.FindIndex(i => i.Id == p.Id);
                            listOfPlayers[index] = p;
                        }
                    }
                    else
                    {
                        if(p.Id == myPlayer.Id && p.Health <= 0)
                        {
                            Application.Current.MainWindow.Content = new DeathWindow();
                            //Application.Current.Shutdown();
                        }

                        GameImage img;
                        if (UIPlayers.TryGetValue(oldPlayer.Id, out img))
                        {
                            //img.RenderTransform = new TranslateTransform(-900,-900);
                            LayoutRoot.Children.Remove(img);
                            if (oldPlayer.GetEquippedWeapon() != null && UIWeapons.TryGetValue(oldPlayer.GetEquippedWeapon().Id, out img))
                            {
                                LayoutRoot.Children.Remove(img);
                            }

                            Label name;
                            if(UIPlayerNames.TryGetValue(oldPlayer.Id, out name)){
                                UIPlayerNames.Remove(oldPlayer.Id);
                                LayoutRoot.Children.Remove(name);
                            }
                        }
                        //this.Form1_PaintPlayer(p);
                    }
                }
                foreach (Weapon w in weaponsList)
                {
                   // if (w.isOnTheGround)
                        UpdateWeaponPos(w);
                }
            }


        }

        //reaguoja į paspaustus mygtukus
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

            long moveStep = (long)myPlayer.state.getMovementStep();
            
            switch (e.Key)
            {
                case Key.W:
                    if (!CheckIfTouchesObstacle(myPlayer, 'w', moveStep))
                    {
                        myPlayer.PosY -= moveStep;
                        CheckIfWeaponNearby();
                        moved = true;
                        Form1_PaintPlayer(myPlayer);
                    }
                    break;
                case Key.A:
                    if (!CheckIfTouchesObstacle(myPlayer, 'a', moveStep))
                    {
                        myPlayer.PosX -= moveStep;
                        CheckIfWeaponNearby();
                        moved = true;
                        Form1_PaintPlayer(myPlayer);
                    }
                    break;
                case Key.S:
                    if (!CheckIfTouchesObstacle(myPlayer, 's', moveStep))
                    {
                        myPlayer.PosY += moveStep;
                        CheckIfWeaponNearby();
                        moved = true;
                        Form1_PaintPlayer(myPlayer);
                    }
                    break;
                case Key.D:
                    if (!CheckIfTouchesObstacle(myPlayer, 'd', moveStep))
                    {
                        myPlayer.PosX += moveStep;
                        CheckIfWeaponNearby();
                        Form1_PaintPlayer(myPlayer);
                        moved = true;
                    }
                    break;
                case Key.D1:
                    myPlayer.equipPrimary();
                    moved = true;
                    break;
                case Key.D2:
                    myPlayer.equipSecondary();
                    moved = true;
                    break;
                case Key.D3:
                    myPlayer.equipGrenade();
                    moved = true;
                    break;
            }
        }

        private async void CheckIfWeaponNearby()
        {
            ///myPlayer = await requestController.GetPlayerAsync(url1);
            foreach (var weapon in weaponsList)
            {
                if (weapon.isOnTheGround)
                {
                    if (Math.Abs(weapon.PosX - myPlayer.PosX) <= 20 && Math.Abs(weapon.PosY - myPlayer.PosY) <= 20)
                    {
                        if (weapon is Primary)
                        {
                            if (myPlayer.pickupPrimary((Primary)weapon))
                            {
                                await requestController.UpdateWeaponIsOnTheGroundStatusAsync(weapon, myPlayer);
                            }
                        }
                        else if (weapon is Secondary)
                        {
                            if (myPlayer.pickupSecondary((Secondary)weapon))
                            {
                                await requestController.UpdateWeaponIsOnTheGroundStatusAsync(weapon, myPlayer);
                            }
                        }
                        else
                        {
                            if (myPlayer.pickupGrenade((GrenadeAdapter)weapon))
                            {
                                await requestController.UpdateWeaponIsOnTheGroundStatusAsync(weapon, myPlayer);
                            }
                        }
                    }
                }
            }

        }

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (laser != null)
            {
                laser.X1 = myPlayer.PosX;
                laser.Y1 = myPlayer.PosY;
                laser.X2 = e.GetPosition(LayoutRoot).X;
                laser.Y2 = e.GetPosition(LayoutRoot).Y;
                laser.RenderTransform = new TranslateTransform();
            }

            //Image img;
            //if (UIPlayers.TryGetValue(myPlayer.Id, out img))
            //{
            //    var somePoint = e.GetPosition(LayoutRoot);
            //    double X = somePoint.X;
            //    double Y = somePoint.Y;

            //    var newX = Math.Abs(X - myPlayer.PosX - 400);
            //    var newY = Math.Abs(Y - myPlayer.PosY - 300);
            //    var powX = Math.Pow(newX, 2);
            //    var powY = Math.Pow(newY, 2);
            //    var distance = Math.Sqrt(powX + powY);
            //    var result = newX / distance;

            //    double Angle = Math.Asin(result) * (180.0 / Math.PI);
            //    RotateTransform rotateTransform = new RotateTransform(Angle);
            //    TransformGroup bbz = new TransformGroup();
            //    bbz.Children.Add(rotateTransform);
            //    img.RenderTransform = bbz;
            //}
        }

        private async void Form1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            laser.Stroke = darkBrush;
            laserTimer.Stop();
            laserTimer.Start();

            dynamic damageDone = await requestController
                  .ShootRequest(
                        requestController.client.BaseAddress.PathAndQuery,
                        (int)myPlayer.PosX,
                        (int)myPlayer.PosY,
                        (int)e.GetPosition(LayoutRoot).X,
                        (int)e.GetPosition(LayoutRoot).Y,
                        myPlayer.Id);
            if ((int)damageDone.damage > 0)
            {
                damageText.Text = (string)damageDone.damage;
                damageText.RenderTransform = new TranslateTransform((int)damageDone.x - 400, (int)damageDone.y - 300);
                damageText.Visibility = Visibility.Visible;


                Storyboard sb = this.FindResource("PlayAnimation") as Storyboard;
                Storyboard.SetTarget(sb, this.damageText);
                sb.Begin();
            }
        }

        private void laserColorTimer(object sender, EventArgs e)
        {
            laser.Stroke = whiteBrush;
            laser.RenderTransform = new TranslateTransform();
            laserTimer.Stop();
        }

        private bool CheckIfTouchesObstacle(Player player, char move, long moveStep)
        {
            long newPosX = player.PosX;
            long newPosY = player.PosY;

            switch (move)
            {
                case 'w':
                    newPosY -= moveStep;
                    break;
                case 'a':
                    newPosX -= moveStep;
                    break;
                case 's':
                    newPosY += moveStep;
                    break;
                case 'd':
                    newPosX += moveStep;
                    break;
            }
            foreach (var obstacle in obstacleList)
            {
                if (obstacle.PosX - (obstacle.Width / 2) - 20 < newPosX && newPosX < obstacle.PosX + (obstacle.Width / 2) + 20)
                {
                    if (obstacle.PosY - (obstacle.Height / 2) - 10 < newPosY && newPosY < obstacle.PosY + (obstacle.Height / 2) + 10)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
