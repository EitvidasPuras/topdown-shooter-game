using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using GameServer.Models;

namespace GameClient
{
 
    class Program
    {
        static HttpClient client = new HttpClient();
        static string requestUri = "api/player/";
        static string mediaType = "application/json";

        static void ShowProduct(Player player)
        {
            Console.WriteLine($"Id: {player.Id}\tName: {player.Name}\tScore: " +
                              $"{player.Score}\tposX: {player.PosX}\tposY: {player.PosY}");
        }

        static async Task<Uri> CreatePlayerAsync(Player player)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                requestUri, player);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            Player player2 = await response.Content.ReadAsAsync<Player>();
            if(player2 != null){
                ShowProduct(player2);
            } 

            // return URI of the created resource.
            return response.Headers.Location;
        }

        static async Task<ICollection<Player>> GetAllPlayerAsync(string path)
        {
            ICollection<Player> players = null;
            HttpResponseMessage response = await client.GetAsync(path + "api/player");
            if (response.IsSuccessStatusCode)
            {
                players = await response.Content.ReadAsAsync<ICollection<Player>>();
            }
            return players;
        }

        static async Task<Player> GetPlayerAsync(string path)
        {
            Player player = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                player = await response.Content.ReadAsAsync<Player>();
            }
            return player;
        }

        static async Task<HttpStatusCode> UpdatePlayerAsync(Player player)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
                requestUri + $"{player.Id}", player);
            response.EnsureSuccessStatusCode();

            return response.StatusCode;
        }

        static async Task<HttpStatusCode> PatchPlayerAsync(Coordinates coordinates)
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

        static async Task<HttpStatusCode> DeletePlayerAsync(long id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                requestUri + $"{id}");
            return response.StatusCode;
        }

        static void Main()
        {
            Console.WriteLine("Web API Client says: \"Hello World!\"");
            RunAsync().GetAwaiter().GetResult();
        }

        static async Task RunAsync()
        {
            // Update port # in the following line.
            //client.BaseAddress = new Uri("https://gameserver.azurewebsites.net/"); //api /player/");
            client.BaseAddress = new Uri("https://localhost:44371/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(mediaType));

            try
            {
                // Get all players
                Console.WriteLine("0)\tGet all player");
                ICollection<Player> playersList = await GetAllPlayerAsync(client.BaseAddress.PathAndQuery);
                foreach (Player p in playersList)
                {
                    ShowProduct(p);
                }


                // Create a new player
                Console.WriteLine("1.1)\tCreate the player");
                Player player = new Player
                {
                    Name = "Studentas-" + playersList.Count.ToString(),
                    Score = 100,
                    PosX = 20,
                    PosY = 30
                };

                var url = await CreatePlayerAsync(player);
                Console.WriteLine($"Created at {url}");

                // Get the created player
                Console.WriteLine("1.2)\tGet created player");
                player = await GetPlayerAsync(url.PathAndQuery);
                ShowProduct(player);

                Console.WriteLine("2.1)\tFull Update the player's score");
                player.Score = 80;
                var updateStatusCode = await UpdatePlayerAsync(player);
                Console.WriteLine($"Updated (HTTP Status = {(int)updateStatusCode})");

                // Get the full updated player
                Console.WriteLine("2.2)\tGet updated the player");
                player = await GetPlayerAsync(url.PathAndQuery);
                ShowProduct(player);

                //Partial update - patch - of the player
                Console.WriteLine("3.1)\tPatch Update the player's score");
                Coordinates coordinates = new Coordinates
                {
                    Id = player.Id,
                    PosX = player.PosX + 10,
                    PosY = player.PosY + 15
                };
                var patchStatusCode = await PatchPlayerAsync(coordinates);
                Console.WriteLine($"Patched (HTTP Status = {(int)patchStatusCode})");

                // Get the patched  player
                Console.WriteLine("3.2)\tGet patched player");
                player = await GetPlayerAsync(url.PathAndQuery);
                ShowProduct(player);

                //Create player for deletion
                Console.WriteLine("4.1)\tCreate the player for deletion");
                Player delPlayer = new Player
                {
                    Name = "StudentasDel-" + (playersList.Count+1).ToString(),
                    Score = 444,
                    PosX = 444,
                    PosY = 444
                };
                var url4Del = await CreatePlayerAsync(delPlayer);
                Console.WriteLine($"Created at {url4Del}");

                //Show created player for deletion
                Console.WriteLine("4.2)\tGet the player created for deletion ");
                delPlayer = await GetPlayerAsync(url4Del.PathAndQuery);
                ShowProduct(delPlayer);

                // Delete the player
                Console.WriteLine("4.3)\tDelete the player");
                var statusCode = await DeletePlayerAsync(delPlayer.Id);
                Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");

                //check if deletion was successful
                Console.WriteLine("4.4)\tCheck if deletion was successful");
                delPlayer = await GetPlayerAsync(url4Del.PathAndQuery);
                ShowProduct(delPlayer);

                Console.WriteLine("Web API Client says: \"GoodBy!\"");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}