using GameServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.generatedFiles.Models
{
    abstract class PlayerTemplate
    {
        Player player { get; set; }
        RequestsController RC = new RequestsController();

        public void SetPlayer(Player x)
        {
            this.player = x;
        }

        public async Task<bool> Ready()
        {
            if (IsHost())
            {
                var IsFull = await RC.GameIsFull("http://localhost:47850/");
                if (IsFull)
                {
                    return await RC.GameIsReady("http://localhost:47850/");
                }

                return false;
            }

            if (IsReady() && !IsHost())
            {
                return await RC.GameIsStarted("http://localhost:47850/");
            }
            else if (!IsReady())
            {
                var code = await RC.UpdatePlayerReadyState("http://localhost:47850/", player.Id);
            }

            return false;
        }

        public abstract bool IsHost();

        public abstract bool IsReady();
    }
}
