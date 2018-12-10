using GameClient.Models;
using GameServer.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using GameClient.Models;
using GameServer.Models;
using System.Net.Http.Headers;

namespace GameClientWpf
{
    class RequestsController
    {
        public HttpClient client = new HttpClient();
        public string requestUri = "api/player/";
        public string requestUriWeapons = "api/weapon/";
        public string mediaType = "application/json";

        public RequestsController()
        {
            client.BaseAddress = new Uri("http://localhost:47850/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(mediaType));
        }

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

        public async Task<Uri> JoinGame()
        {
            Random rnd = new Random();

            Player player = new Player
            {
                Name = "Studentas-" + rnd.Next(10, 100),
                Score = 100,
                Health = 100,
                PosX = rnd.Next(10, 800),
                PosY = rnd.Next(10, 600)
            };

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

        public async Task<ICollection<Weapon>> GetAllWeaponsAsync(string path)
        {
            ICollection<object> weps = null;
            ICollection<Weapon> weapons = new List<Weapon>();
            HttpResponseMessage response = await client.GetAsync(path + "api/weapon");
            if (response.IsSuccessStatusCode)
            {
                weps = await response.Content.ReadAsAsync<ICollection<object>>();
            }

            foreach (object wep in weps)
            {
                Weapon weapon = null;
                Newtonsoft.Json.Linq.JObject obj = JsonConvert.DeserializeObject<dynamic>(wep.ToString());
                string name = obj.GetValue("name").ToString();

                if (name.Contains("AK47"))
                    weapon = JsonConvert.DeserializeObject<AK47>(wep.ToString());
                else if (name.Contains("M4A1"))
                    weapon = JsonConvert.DeserializeObject<M4A1>(wep.ToString());
                else if(name.Contains("DesertEagle"))
                    weapon = JsonConvert.DeserializeObject<DesertEagle>(wep.ToString());
                else if(name.Contains("P250"))
                    weapon = JsonConvert.DeserializeObject<P250>(wep.ToString());
                else if(name.Contains("Grenade"))
                    weapon = JsonConvert.DeserializeObject<GrenadeAdapter>(wep.ToString());
                weapons.Add(weapon);
            }
            return weapons;
        }

        public async Task<HttpStatusCode> UpdateWeaponIsOnTheGroundStatusAsync(Weapon weapon, Player player)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
                requestUriWeapons + "picked-up/" + $"{weapon.Id}", player.Id);
            response.EnsureSuccessStatusCode();

            return response.StatusCode;
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

        public async Task<bool> GameIsFull(string path)
        {
            HttpResponseMessage response = await client.GetAsync(path + "api/game/is-full");
            return await response.Content.ReadAsAsync<bool>();
        }

        public async Task<bool> GameIsReady(string path)
        {
            HttpResponseMessage response = await client.GetAsync(path + "api/game/is-ready");
            return await response.Content.ReadAsAsync<bool>();
        }
        public async Task<bool> GameIsStarted(string path)
        {
            HttpResponseMessage response = await client.GetAsync(path + "api/game/is-started");
            return await response.Content.ReadAsAsync<bool>();
        }
        public async Task<bool> StartGame(string path)
        {
            HttpResponseMessage response = await client.GetAsync(path + "api/game/start");
            return await response.Content.ReadAsAsync<bool>();
        }


        public async Task<HttpStatusCode> UpdatePlayerReadyState(string path, long id)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = new HttpMethod("PATCH"),
                RequestUri = new Uri(path + "api/player/ready/" + id)
            };

            HttpResponseMessage response = await client.SendAsync(request);
            return response.StatusCode;
        }

        public async Task<HttpStatusCode> ShootRequest(string path, int x, int y, int px, int py, long playerId)
        {
            List<int> parameters = new List<int>();
            parameters.Add(x);
            parameters.Add(y);
            parameters.Add(px);
            parameters.Add(py);
            parameters.Add((int)playerId);
            HttpResponseMessage response = await client.PutAsJsonAsync(
                "api/game/shoot", parameters);
            response.EnsureSuccessStatusCode();

            return response.StatusCode;
        }



        public void getLogInformationAsync()
        {

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
