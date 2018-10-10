using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebsiteForm.Models;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private System.Drawing.Graphics formGraphics;

        Program1 p1 = new Program1();
        Player myPlayer = new Player();
        String url1 = "";
        public Form1()
        {
            InitializeComponent();
            formGraphics = this.CreateGraphics();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
        }

        // pagetina visus playerius ir juos atvaizduoja, vėliau sukuria playeri esančiam klientui
        private async void Form1_LoadAsync(object sender, EventArgs e)
        {
            Console.WriteLine("0)\tGet all player");
            p1.client.BaseAddress = new Uri("https://topdown-shooter.azurewebsites.net/");
            p1.client.DefaultRequestHeaders.Accept.Clear();
            p1.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(p1.mediaType));

            ICollection<Player> playersList = await p1.GetAllPlayerAsync(p1.client.BaseAddress.PathAndQuery);

            Random rnd = new Random();

            foreach (Player p in playersList)
            {
                //Coordinates coordinates = new Coordinates
                //{
                //    Id = p.Id,
                //    PosX = rnd.Next(10, this.Width),
                //    PosY = rnd.Next(10, this.Height)
                //};
                //var patchStatusCode = await p1.PatchPlayerAsync(coordinates);
                this.Form1_PaintDot((int)p.PosX, (int)p.PosY, Color.Red);
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

            var url = await p1.CreatePlayerAsync(myPlayer);

            url1 = url.PathAndQuery;
            myPlayer = await p1.GetPlayerAsync(url.PathAndQuery);

            this.Form1_PaintDot((int)myPlayer.PosX, (int)myPlayer.PosY, Color.Blue);

            playersList = await p1.GetAllPlayerAsync(p1.client.BaseAddress.PathAndQuery);          
            
        }

        // paint dot
        private void Form1_PaintDot(int x, int y, Color color)
        {
            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(color);
            Rectangle rec = new Rectangle(x, y, 10, 10);
            formGraphics.FillRectangle(myBrush, rec);
            myBrush.Dispose();
            
        }

        //deletint playeri?
        private async void Form1_FormClosingAsync(object sender, FormClosingEventArgs e)
        {
            //Delete the player
            //Console.WriteLine("4.3)\tDelete the player");
            //var statusCode = await p1.DeletePlayerAsync(myPlayer.Id);
            //Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");
        }

        //timeris getina visus playerius ir atvaizduoja pagal koordinates
        private async void timer1_TickAsync(object sender, EventArgs e)
        {
            this.Invalidate();
            ICollection<Player> playersList = await p1.GetAllPlayerAsync(p1.client.BaseAddress.PathAndQuery);

            Random rnd = new Random();

            foreach (Player p in playersList)
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

        //reaguoja į paspaustus mygtukus
        private async void Form1_KeyPressAsync(object sender, KeyPressEventArgs e)
        {        
            //atnaujina kliento objektą iš duombazes
            myPlayer = await p1.GetPlayerAsync(url1);
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
                    patchStatusCode = await p1.PatchPlayerAsync(coordinates);
                    break;
                case 'a':
                    coordinates.PosX -= 15;
                    patchStatusCode = await p1.PatchPlayerAsync(coordinates);
                    break;
                case 's':
                    coordinates.PosY += 15;
                    patchStatusCode = await p1.PatchPlayerAsync(coordinates);
                    break;
                case 'd':
                    coordinates.PosX += 15;
                    patchStatusCode = await p1.PatchPlayerAsync(coordinates);
                    break;
                case 'x':
                    var statusCode = await p1.DeletePlayerAsync(myPlayer.Id);
                    this.Close();
                    break;
            }
        }
    }
    class Program1
    {
        public HttpClient client = new HttpClient();
        public string requestUri = "api/player/";
        public string mediaType = "application/json";

        public void ShowProduct(Player player)
        {
            Console.WriteLine($"Id: {player.Id}\tName: {player.Name}\tScore: " +
                              $"{player.Score}\tposX: {player.PosX}\tposY: {player.PosY}");
        }

        public async Task<Uri> CreatePlayerAsync(Player player)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                requestUri, player);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            Player player2 = await response.Content.ReadAsAsync<Player>();
            if (player2 != null)
            {
                ShowProduct(player2);
            }

            // return URI of the created resource.
            return response.Headers.Location;
        }

        public async Task<ICollection<Player>> GetAllPlayerAsync(string path)
        {
            ICollection<Player> players = null;
            HttpResponseMessage response = await client.GetAsync(path + "api/player");
            if (response.IsSuccessStatusCode)
            {
                players = await response.Content.ReadAsAsync<ICollection<Player>>();
            }
            return players;
        }

        public async Task<Player> GetPlayerAsync(string path)
        {
            Player player = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                player = await response.Content.ReadAsAsync<Player>();
            }
            return player;
        }

        public async Task<HttpStatusCode> UpdatePlayerAsync(Player player)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
                requestUri + $"{player.Id}", player);
            response.EnsureSuccessStatusCode();

            return response.StatusCode;
        }

        public async Task<HttpStatusCode> PatchPlayerAsync(Coordinates coordinates)
        {
            string jsonString = JsonConvert.SerializeObject(coordinates);    //"{\"id\":1, \"posX\":777,\"posY\":777}";

            HttpContent httpContent = new StringContent(jsonString, System.Text.Encoding.UTF8, mediaType);
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = new HttpMethod("PATCH"),
                RequestUri = new Uri(client.BaseAddress + requestUri),
                Content = httpContent,
            };

            HttpResponseMessage response = await client.SendAsync(request);
            return response.StatusCode;

        }

        public async Task<HttpStatusCode> DeletePlayerAsync(long id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                requestUri + $"{id}");
            return response.StatusCode;
        }

        ////static void Main()
        ////{
        ////    Console.WriteLine("Web API Client says: \"Hello World!\"");
        ////    RunAsync().GetAwaiter().GetResult();
        ////}

        ////static async Task RunAsync()
        ////{
        ////    // Update port # in the following line.
        ////    //client.BaseAddress = new Uri("https://gameserver.azurewebsites.net/"); //api /player/");
        ////    client.BaseAddress = new Uri("https://localhost:44371/");
        ////    client.DefaultRequestHeaders.Accept.Clear();
        ////    client.DefaultRequestHeaders.Accept.Add(
        ////        new MediaTypeWithQualityHeaderValue(mediaType));

        ////    try
        ////    {
        ////        // Get all players
        ////        Console.WriteLine("0)\tGet all player");
        ////        ICollection<Player> playersList = await GetAllPlayerAsync(client.BaseAddress.PathAndQuery);
        ////        foreach (Player p in playersList)
        ////        {
        ////            ShowProduct(p);
        ////        }


        ////        // Create a new player
        ////        Console.WriteLine("1.1)\tCreate the player");
        ////        Player player = new Player
        ////        {
        ////            Name = "Studentas-" + playersList.Count.ToString(),
        ////            Score = 100,
        ////            PosX = 20,
        ////            PosY = 30
        ////        };

        ////        var url = await CreatePlayerAsync(player);
        ////        Console.WriteLine($"Created at {url}");

        ////        // Get the created player
        ////        Console.WriteLine("1.2)\tGet created player");
        ////        player = await GetPlayerAsync(url.PathAndQuery);
        ////        ShowProduct(player);

        ////        Console.WriteLine("2.1)\tFull Update the player's score");
        ////        player.Score = 80;
        ////        var updateStatusCode = await UpdatePlayerAsync(player);
        ////        Console.WriteLine($"Updated (HTTP Status = {(int)updateStatusCode})");

        ////        // Get the full updated player
        ////        Console.WriteLine("2.2)\tGet updated the player");
        ////        player = await GetPlayerAsync(url.PathAndQuery);
        ////        ShowProduct(player);

        ////        //Partial update - patch - of the player
        ////        Console.WriteLine("3.1)\tPatch Update the player's score");
        ////        Coordinates coordinates = new Coordinates
        ////        {
        ////            Id = player.Id,
        ////            PosX = player.PosX + 10,
        ////            PosY = player.PosY + 15
        ////        };
        ////        var patchStatusCode = await PatchPlayerAsync(coordinates);
        ////        Console.WriteLine($"Patched (HTTP Status = {(int)patchStatusCode})");

        ////        // Get the patched  player
        ////        Console.WriteLine("3.2)\tGet patched player");
        ////        player = await GetPlayerAsync(url.PathAndQuery);
        ////        ShowProduct(player);

        ////        //Create player for deletion
        ////        Console.WriteLine("4.1)\tCreate the player for deletion");
        ////        Player delPlayer = new Player
        ////        {
        ////            Name = "StudentasDel-" + (playersList.Count + 1).ToString(),
        ////            Score = 444,
        ////            PosX = 444,
        ////            PosY = 444
        ////        };
        ////        var url4Del = await CreatePlayerAsync(delPlayer);
        ////        Console.WriteLine($"Created at {url4Del}");

        ////        //Show created player for deletion
        ////        Console.WriteLine("4.2)\tGet the player created for deletion ");
        ////        delPlayer = await GetPlayerAsync(url4Del.PathAndQuery);
        ////        ShowProduct(delPlayer);

        ////        // Delete the player
        ////        Console.WriteLine("4.3)\tDelete the player");
        ////        var statusCode = await DeletePlayerAsync(delPlayer.Id);
        ////        Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");

        ////        //check if deletion was successful
        ////        Console.WriteLine("4.4)\tCheck if deletion was successful");
        ////        delPlayer = await GetPlayerAsync(url4Del.PathAndQuery);
        ////        ShowProduct(delPlayer);

        ////        Console.WriteLine("Web API Client says: \"GoodBy!\"");

        ////    }
        ////    catch (Exception e)
        ////    {
        ////        Console.WriteLine(e.Message);
        ////    }

        ////    Console.ReadLine();
        ////}
    }
}
