

using System.Collections.Generic;
/**
* @(#) Grenade.cs
*/
namespace GameServer.Models
{
	public class Grenade
	{
		public bool throwGrenade(int x, int y, int px, int py, Player player)
		{
            if (player.PosX - 150 < x && y < player.PosX + 150)
            {
                if (player.PosY - 150 < x && y < player.PosY + 150)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
