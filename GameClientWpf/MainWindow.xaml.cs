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
        private int moveRightDist = 0;
        private int moveDownDist = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MoveRight(object sender, RoutedEventArgs e)
        {

            this.moveRightDist++;
            this.moveDownDist++;
            crosshair.RenderTransform = new TranslateTransform(this.moveRightDist, this.moveDownDist);

        }

        private void LayoutRoot_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {

        }

        //private void Window_KeyDown(object sender, KeyEventArgs e)
        //{
        //    switch (e.Key)
        //    {
        //        case Key.W:
        //            moveDownDist -= 15;

        //            break;
        //        case Key.A:
        //            moveRightDist -= 15;

        //            break;
        //        case Key.S:
        //            moveDownDist += 15;

        //            break;
        //        case Key.D:
        //            moveRightDist += 15;

        //            break;
        //            //case 'x':
        //            //    var statusCode = await requestController.DeletePlayerAsync(myPlayer.Id);
        //            //    this.Close();
        //    }
        //    //crosshair2.RenderTransform = new TranslateTransform(this.moveRightDist, this.moveDownDist);
        //    image.RenderTransform = new TranslateTransform(this.moveRightDist, this.moveDownDist);
        //}


        //private System.Drawing.Graphics formGraphics;
        //private System.Drawing.Graphics formGraphics2;

        RequestsController requestController = new RequestsController();
        Player myPlayer = new Player();
        String url1 = "";
        List<GameServer.Models.Player> listOfPlayers = new List<GameServer.Models.Player>();
        List<GameServer.Models.Weapon> listOfWeapons = new List<GameServer.Models.Weapon>();
        List<GameServer.Interfaces.ISkin> listOfDrawers = new List<GameServer.Interfaces.ISkin>();
        ObserverController observer = new ObserverController();

        // pagetina visus playerius ir juos atvaizduoja, vėliau sukuria playeri esančiam klientui
        private async void Form1_LoadAsync(object sender, EventArgs e)
        {
            Console.WriteLine("0)\tGet all player");
            //requestController.client.BaseAddress = new Uri("https://topdown-shooter.azurewebsites.net/");
            requestController.client.BaseAddress = new Uri("http://localhost:47850/");
            requestController.client.DefaultRequestHeaders.Accept.Clear();
            requestController.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(requestController.mediaType));

            ICollection<Player> playersList = await requestController.GetAllPlayerAsync(requestController.client.BaseAddress.PathAndQuery);

            Random rnd = new Random();


            foreach (Player p in playersList)
            {
                listOfPlayers.Add(p);
                //Coordinates coordinates = new Coordinates
                //{
                //    Id = p.Id,
                //    PosX = rnd.Next(10, this.Width),
                //    PosY = rnd.Next(10, this.Height)
                //};
                //var patchStatusCode = await requestController.PatchPlayerAsync(coordinates);
                this.Form1_PaintPlayer((int)p.PosX, (int)p.PosY);
            }

            // Create a new player
            Console.WriteLine("1.1)\tCreate the player");
            myPlayer = new Player
            {
                Name = "Studentas-" + playersList.Count.ToString(),
                Score = 100,
                PosX = rnd.Next(10, (int)this.Width),
                PosY = rnd.Next(10, (int)this.Height)
            };

            var url = await requestController.CreatePlayerAsync(myPlayer);

            url1 = url.PathAndQuery;
            myPlayer = await requestController.GetPlayerAsync(url.PathAndQuery);
            listOfPlayers.Add(myPlayer);

            this.Form1_PaintPlayer((int)myPlayer.PosX, (int)myPlayer.PosY);

            playersList = await requestController.GetAllPlayerAsync(requestController.client.BaseAddress.PathAndQuery);

            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += timer1_TickAsync;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

            //timer1.Enabled = true;
        }

        // paint dot
        private void Form1_PaintPlayer(int x, int y)
        {
            //Image image = Image.FromFile(@"..\\..\\assets\\basicPlayer.png");
            //formGraphics.DrawImage(image, x, y);
            //formGraphics.DrawImage()
        }

        private void Form1_PaintWeapons(int x, int y, string weaponName)
        {
            if (weaponName.Contains("AK47"))
            {
                //Image image = Image.FromFile(@"..\\..\\assets\\ak47.png");
                //formGraphics.DrawImage(image, x, y);
            }
            else if (weaponName.Contains("M4A1"))
            {
                //Image image = Image.FromFile(@"..\\..\\assets\\m4a1.png");
                //formGraphics.DrawImage(image, x, y);
            }
            else if (weaponName.Contains("DesertEagle"))
            {
                //Image image = Image.FromFile(@"..\\..\\assets\\deserteagle.png");
                //formGraphics.DrawImage(image, x, y);
            }
            else if (weaponName.Contains("P250"))
            {
                //Image image = Image.FromFile(@"..\\..\\assets\\p250.png");
                //formGraphics.DrawImage(image, x, y);
            }
            else if (weaponName.Contains("Grenade"))
            {
                //Image image = Image.FromFile(@"..\\..\\assets\\grenade.png");
                //formGraphics.DrawImage(image, x, y);
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

            bool updated = await observer.CheckIfChangedAsync(requestController.client.BaseAddress.ToString(), myPlayer);
            if (updated)
            {
                //this.Invalidate();
                ICollection<Player> playersList = await requestController.GetAllPlayerAsync(requestController.client.BaseAddress.PathAndQuery);
                ICollection<Weapon> weaponsList = await requestController.GetAllWeaponsAsync(requestController.client.BaseAddress.PathAndQuery);

                foreach (Player p in playersList)
                {
                    Player oldPlayer = listOfPlayers.Find(i => i.Id == p.Id);
                    if (oldPlayer != null)
                    {
                        if (!oldPlayer.checkEquality(p))
                        {
                            if (p.Id == myPlayer.Id)
                            {
                                //Image image = Image.FromFile(@"..\\..\\assets\\empty.png");
                                //formGraphics.DrawImage(image, (int)oldPlayer.PosX, (int)oldPlayer.PosY);

                                this.Form1_PaintPlayer((int)p.PosX, (int)p.PosY);
                            }
                            else
                            {
                                //Image image = Image.FromFile(@"..\\..\\assets\\empty.png");
                                //formGraphics.DrawImage(image, (int)oldPlayer.PosX, (int)oldPlayer.PosY);

                                this.Form1_PaintPlayer((int)p.PosX, (int)p.PosY);
                            }

                            int index = listOfPlayers.FindIndex(i => i.Id == p.Id);
                            listOfPlayers[index] = p;
                        }
                    }
                }


                foreach (Weapon w in weaponsList)
                {
                    this.Form1_PaintWeapons((int)w.PosX, (int)w.PosY, w.Name);
                }
            }
        }

        //reaguoja į paspaustus mygtukus
        private async void Window_KeyDown(object sender, KeyEventArgs e)
        {
            //atnaujina kliento objektą iš duombazes
            myPlayer = await requestController.GetPlayerAsync(url1);
            Coordinates coordinates = new Coordinates
            {
                Id = myPlayer.Id,
                PosX = myPlayer.PosX,
                PosY = myPlayer.PosY
            };
            var patchStatusCode = (Object)null;

            switch (e.Key)
            {
                case Key.W:
                    coordinates.PosY -= 15;
                    patchStatusCode = await requestController.PatchPlayerAsync(coordinates);
                    break;
                case Key.A:
                    coordinates.PosX -= 15;
                    patchStatusCode = await requestController.PatchPlayerAsync(coordinates);
                    break;
                case Key.S:
                    coordinates.PosY += 15;
                    patchStatusCode = await requestController.PatchPlayerAsync(coordinates);
                    break;
                case Key.D:
                    coordinates.PosX += 15;
                    patchStatusCode = await requestController.PatchPlayerAsync(coordinates);
                    break;
                case Key.X:
                    var statusCode = await requestController.DeletePlayerAsync(myPlayer.Id);
                    this.Close();
                    break;
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            //Pen pen = new Pen(Color.Azure);
            //formGraphics.DrawLine(pen, e.X, e.Y, myPlayer.PosX + 25, myPlayer.PosY + 25);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            //Pen pen = new Pen(Color.Black);
            //formGraphics.DrawLine(pen, e.X, e.Y, myPlayer.PosX + 25, myPlayer.PosY + 25);
            //this.Invalidate()
        }
    }
}
