

using GameServer.Models;
using System.Net.Http;
using System.Threading.Tasks;
/**
* @(#) ObserverController.cs
*/
namespace GameClient
{
	public class ObserverController
	{
        public HttpClient client = new HttpClient();
        public string requestUri = "api/player/";
        public string mediaType = "application/json";

        public async Task<bool> CheckIfChangedAsync(string path, Player player)
        {
            bool updated = false;
            HttpResponseMessage response = await client.GetAsync(path + "api/player/updated/" + player.Id);
            if (response.IsSuccessStatusCode)
            {
                updated = await response.Content.ReadAsAsync<bool>();
            }
            return updated;
        }

    }
	
}
