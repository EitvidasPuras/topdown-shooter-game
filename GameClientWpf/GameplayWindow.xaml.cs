using GameClient;
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
        public GameplayWindow(Player myPlayer, string url)
        {
            InitializeComponent();
            this.myPlayer = myPlayer;
            url1 = url;
        }

        string url1 = "";
        List<GameServer.Models.Player> listOfPlayers = new List<GameServer.Models.Player>();
        List<GameServer.Models.Weapon> listOfWeapons = new List<GameServer.Models.Weapon>();
        ICollection<Weapon> weaponsList;
        ICollection<Player> playersList;
        List<GameServer.Interfaces.ISkin> listOfDrawers = new List<GameServer.Interfaces.ISkin>();
        bool moved = false;

        Dictionary<long, Image> UIPlayers = new Dictionary<long, Image>();
        Dictionary<long, Image> UIWeapons = new Dictionary<long, Image>();

        ObserverController observer = new ObserverController();

        // pagetina visus playerius ir juos atvaizduoja, vėliau sukuria playeri esančiam klientui
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("0)\tGet all player");

            playersList = await requestController.GetAllPlayerAsync(requestController.client.BaseAddress.PathAndQuery);
            weaponsList = await requestController.GetAllWeaponsAsync(requestController.client.BaseAddress.PathAndQuery);

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

            // Create a Line  
            laser = new Line();
            laser.X1 = 50;
            laser.Y1 = 50;
            laser.X2 = 200;
            laser.Y2 = 200;

            // Create a red Brush  
            SolidColorBrush whiteBrush = new SolidColorBrush();
            whiteBrush.Color = Colors.GhostWhite;

            // Set Line's width and color  
            laser.StrokeThickness = 1;
            laser.Stroke = whiteBrush;

            // Add line to the Grid.  
            LayoutRoot.Children.Add(laser);


            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += timer1_TickAsync;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 300);
            dispatcherTimer.Start();
            Focus();
        }

        private void Form1_PaintPlayer(Player player)
        {
            Image img;
            if (UIPlayers.TryGetValue(player.Id, out img))
            {
                img.RenderTransform = new TranslateTransform(player.PosX - 400, player.PosY - 300);
            }
            else
            {
                img = new Image();
                LayoutRoot.Children.Add(img);
                img.Source = new BitmapImage(new Uri("/images/basicPlayer.png", UriKind.Relative));
                img.Margin = new Thickness(0, 0, 0, 0);
                img.Height = 50;
                UIPlayers.Add(player.Id, img);
                img.RenderTransform = new TranslateTransform(player.PosX - 400, player.PosY - 300);
            }
            if (player.GetEquippedWeapon() != null)
            {
                UpdateWeaponPos(player.GetEquippedWeapon());
            }
        }

        private void Form1_PaintWeapons(Weapon weapon)
        {
            Image img = new Image();

            LayoutRoot.Children.Add(img);
            if (weapon.Name.Contains("AK47"))
            {
                img.Source = new BitmapImage(new Uri("/images/ak47.png", UriKind.Relative));
                img.Height = 15;
            }
            else if (weapon.Name.Contains("M4A1"))
            {
                img.Source = new BitmapImage(new Uri("/images/m4a1.png", UriKind.Relative));
                img.Height = 15;
            }
            else if (weapon.Name.Contains("DesertEagle"))
            {
                img.Source = new BitmapImage(new Uri("/images/deserteagle.png", UriKind.Relative));
                img.Height = 15;
            }
            else if (weapon.Name.Contains("P250"))
            {
                img.Source = new BitmapImage(new Uri("/images/p250.png", UriKind.Relative));
                img.Height = 15;
            }
            else if (weapon.Name.Contains("Grenade"))
            {
                img.Source = new BitmapImage(new Uri("/images/grenade.png", UriKind.Relative));
                img.Height = 15;
            }
            img.Margin = new Thickness(0, 0, 0, 0);
            img.RenderTransform = new TranslateTransform(weapon.PosX - 400, weapon.PosY - 300);
            UIWeapons.Add(weapon.Id, img);
        }

        private void UpdateWeaponPos(Weapon weapon)
        {
            Image img;
            if (UIWeapons.TryGetValue(weapon.Id, out img))
            {
                bool visible = false;
                if (!weapon.isOnTheGround)
                {
                    foreach (Player p in playersList)
                    {
                        if (p.GetEquippedWeapon() != null && p.GetEquippedWeapon().Id == weapon.Id)
                        {
                            img.RenderTransform = new TranslateTransform(p.PosX - 400, p.PosY - 300 - 10);
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
                        img.RenderTransform = new TranslateTransform(weapon.PosX - 400, weapon.PosY - 300);
                    }
                    else
                    {
                        img.Visibility = Visibility.Hidden;
                        img.RenderTransform = new TranslateTransform(8000,  8800);
                    }
                }
            }
        }

        //deletint playeri?
        //private async void Form1_FormClosingAsync(object sender, FormClosingEventArgs e)
        //{
        //    //Delete the player
        //    //Console.WriteLine("4.3)\tDelete the player");
        //    //var statusCode = await requestController.DeletePlayerAsync(myPlayer.Id);
        //    //Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");
        //}

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
                playersList = await requestController.GetAllPlayerAsync(requestController.client.BaseAddress.PathAndQuery);
                weaponsList = await requestController.GetAllWeaponsAsync(requestController.client.BaseAddress.PathAndQuery);

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
                                //Image image = Image.FromFile(@"..\\..\\assets\\empty.png");
                                //formGraphics.DrawImage(image, (int)oldPlayer.PosX, (int)oldPlayer.PosY);

                                this.Form1_PaintPlayer(p);
                            }
                            else
                            {
                                //Image image = Image.FromFile(@"..\\..\\assets\\empty.png");
                                //formGraphics.DrawImage(image, (int)oldPlayer.PosX, (int)oldPlayer.PosY);

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
                            Application.Current.Shutdown();
                        }

                        Image img;
                        if (UIPlayers.TryGetValue(oldPlayer.Id, out img))
                        {
                            //img.RenderTransform = new TranslateTransform(-900,-900);
                            LayoutRoot.Children.Remove(img);
                            if (oldPlayer.GetEquippedWeapon() != null && UIWeapons.TryGetValue(oldPlayer.GetEquippedWeapon().Id, out img))
                            {
                                LayoutRoot.Children.Remove(img);
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
        private async void Window_KeyDown(object sender, KeyEventArgs e)
        {
            //atnaujina kliento objektą iš duombazes
           // myPlayer = await requestController.GetPlayerAsync(url1);
            Coordinates coordinates = new Coordinates
            {
                Id = myPlayer.Id,
                PosX = myPlayer.PosX,
                PosY = myPlayer.PosY
            };

            switch (e.Key)
            {
                case Key.W:
                    myPlayer.PosY -= 15;
                    CheckIfWeaponNearby();
                    moved = true;
                    Form1_PaintPlayer(myPlayer);
                    break;
                case Key.A:
                    myPlayer.PosX -= 15;
                    CheckIfWeaponNearby();
                    moved = true;
                    Form1_PaintPlayer(myPlayer);
                    break;
                case Key.S:
                    myPlayer.PosY += 15;
                    CheckIfWeaponNearby();
                    moved = true;
                    Form1_PaintPlayer(myPlayer);
                    break;
                case Key.D:
                    myPlayer.PosX += 15;
                    CheckIfWeaponNearby();
                    Form1_PaintPlayer(myPlayer);
                    moved = true;
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
                    if (Math.Abs(weapon.PosX - myPlayer.PosX) <= 20 || Math.Abs(weapon.PosY - myPlayer.PosY) <= 20)
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
            await requestController
                  .ShootRequest(
                        requestController.client.BaseAddress.PathAndQuery,
                        (int)myPlayer.PosX,
                        (int)myPlayer.PosY,
                        (int)e.GetPosition(LayoutRoot).X,
                        (int)e.GetPosition(LayoutRoot).Y,
                        myPlayer.Id);

            //Pen pen = new Pen(Color.Black);
            //formGraphics.DrawLine(pen, e.X, e.Y, myPlayer.PosX + 25, myPlayer.PosY + 25);
            //this.Invalidate()
        }
    }
}
