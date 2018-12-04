using GameServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.generatedFiles.Models
{
    public abstract class PlayerTemplate
    {
        Player player { get; set; }
        RequestsController RC = new RequestsController();

        public void SetPlayerTemplate(Player x)
        {
            this.player = x;
        }

        public async Task<bool> Ready()
        {
            if (CheckIfHost())
            {
                var IsFull = await RC.GameIsFull("http://localhost:47850/");
                if (IsFull)
                {
                    return await RC.GameIsReady("http://localhost:47850/");
                }

                return false;
            }

            if (CheckIfReady() && !CheckIfHost())
            {
                return await RC.GameIsStarted("http://localhost:47850/");
            }
            else if (!CheckIfReady())
            {
                var code = await RC.UpdatePlayerReadyState("http://localhost:47850/", player.Id);
            }

            return false;
        }

        public abstract bool CheckIfHost();

        public abstract bool CheckIfReady();
    }
}
