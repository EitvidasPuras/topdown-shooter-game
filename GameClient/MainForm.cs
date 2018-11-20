using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameClient;
using GameClient.Models;
using GameServer.Models;
using System.IO;

namespace GameClient
{
    public partial class MainForm : Form
    {
        private System.Drawing.Graphics formGraphics;

        RequestsController requestController = new RequestsController();
        Player myPlayer = new Player();
        String url1 = "";
        List<GameServer.Models.Player> listOfPlayers = new List<GameServer.Models.Player>();
        List<GameServer.Models.Weapon> listOfWeapons = new List<GameServer.Models.Weapon>();
        List<GameServer.Interfaces.ISkin> listOfDrawers = new List<GameServer.Interfaces.ISkin>();
        ObserverController observer = new ObserverController();
        public MainForm()
        {
            InitializeComponent();
            formGraphics = this.CreateGraphics();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
        }

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
                PosX = rnd.Next(10, this.Width),
                PosY = rnd.Next(10, this.Height)
            };

            var url = await requestController.CreatePlayerAsync(myPlayer);

            url1 = url.PathAndQuery;
            myPlayer = await requestController.GetPlayerAsync(url.PathAndQuery);

            this.Form1_PaintPlayer((int)myPlayer.PosX, (int)myPlayer.PosY);

            playersList = await requestController.GetAllPlayerAsync(requestController.client.BaseAddress.PathAndQuery);
            timer1.Enabled = true;
        }

        // paint dot
        private void Form1_PaintPlayer(int x, int y)
        {
            Image image = Image.FromFile(@"..\\..\\assets\\person.png");
            formGraphics.DrawImage(image, x, y);
        }

        private void Form1_PaintWeapons(int x, int y, string weaponName)
        {
            if (weaponName.Contains("AK47"))
            {
                Image image = Image.FromFile(@"..\\..\\assets\\ak47.png");
                formGraphics.DrawImage(image, x, y);
            }
            else if (weaponName.Contains("M4A1"))
            {
                Image image = Image.FromFile(@"..\\..\\assets\\m4a1.png");
                formGraphics.DrawImage(image, x, y);
            }
            else if (weaponName.Contains("DesertEagle"))
            {
                Image image = Image.FromFile(@"..\\..\\assets\\deserteagle.png");
                formGraphics.DrawImage(image, x, y);
            }
            else if (weaponName.Contains("P250"))
            {
                Image image = Image.FromFile(@"..\\..\\assets\\p250.png");
                formGraphics.DrawImage(image, x, y);
            }
            else if (weaponName.Contains("Grenade"))
            {
                Image image = Image.FromFile(@"..\\..\\assets\\grenade.png");
                formGraphics.DrawImage(image, x, y);
            }
        }

        //deletint playeri?
        private async void Form1_FormClosingAsync(object sender, FormClosingEventArgs e)
        {
            //Delete the player
            //Console.WriteLine("4.3)\tDelete the player");
            //var statusCode = await requestController.DeletePlayerAsync(myPlayer.Id);
            //Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");
        }

        //timeris getina visus playerius ir atvaizduoja pagal koordinates
        private async void timer1_TickAsync(object sender, EventArgs e)
        {
<<<<<<< Updated upstream
            bool updated = await observer.CheckIfChangedAsync(requestController.client.BaseAddress.ToString(), myPlayer);
            if (updated)
=======
            this.Invalidate();
            ICollection<Player> playersList = await requestController.GetAllPlayerAsync(requestController.client.BaseAddress.PathAndQuery);
            ICollection<Weapon> weaponsList = await requestController.GetAllWeaponsAsync(requestController.client.BaseAddress.PathAndQuery);

            Random rnd = new Random();
            PaintEventArgs paintEventArgs = null;

            foreach (Player p in playersList)
>>>>>>> Stashed changes
            {
                //this.Invalidate();
                ICollection<Player> playersList = await requestController.GetAllPlayerAsync(requestController.client.BaseAddress.PathAndQuery);
                ICollection<Weapon> weaponsList = await requestController.GetAllWeaponsAsync(requestController.client.BaseAddress.PathAndQuery);

                foreach (Player p in playersList)
                {
<<<<<<< Updated upstream
                    Player oldPlayer = listOfPlayers.Find(i => i.Id == p.Id);
                    if (oldPlayer != null)
                    {
                        if (!oldPlayer.checkEquality(p))
                        {
                            if (p.Id == myPlayer.Id)
                            {
                                this.Form1_PaintDot((int)p.PosX, (int)p.PosY, Color.Blue);
                            }
                            else
                            {
                                this.Form1_PaintDot((int)p.PosX, (int)p.PosY, Color.Red);
                            }
                        }
                    }
                    
=======
                    this.Form1_PaintPlayer((int)p.PosX, (int)p.PosY);
>>>>>>> Stashed changes
                }

                foreach (Weapon w in weaponsList)
                {
<<<<<<< Updated upstream
                    this.Form1_PaintDot((int)w.PosX, (int)w.PosY, Color.Yellow);
                }
            }
=======
                    this.Form1_PaintPlayer((int)p.PosX, (int)p.PosY);
                }
            }

            foreach (Weapon w in weaponsList)
            {
                this.Form1_PaintWeapons((int)w.PosX, (int)w.PosY, w.Name);
            }
>>>>>>> Stashed changes
        }

        //reaguoja į paspaustus mygtukus
        private async void Form1_KeyPressAsync(object sender, KeyPressEventArgs e)
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

            switch (e.KeyChar)
            {
                case 'w':
                    coordinates.PosY -= 15;
                    patchStatusCode = await requestController.PatchPlayerAsync(coordinates);
                    break;
                case 'a':
                    coordinates.PosX -= 15;
                    patchStatusCode = await requestController.PatchPlayerAsync(coordinates);
                    break;
                case 's':
                    coordinates.PosY += 15;
                    patchStatusCode = await requestController.PatchPlayerAsync(coordinates);
                    break;
                case 'd':
                    coordinates.PosX += 15;
                    patchStatusCode = await requestController.PatchPlayerAsync(coordinates);
                    break;
                case 'x':
                    var statusCode = await requestController.DeletePlayerAsync(myPlayer.Id);
                    this.Close();
                    break;
            }
        }
    }
}
