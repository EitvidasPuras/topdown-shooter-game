/**
 * @(#) Primary.cs
 */
using System.Collections.Generic;
namespace GameServer
{
    namespace Models
    {
        public class Primary : Weapon
        {
            public Primary(int id, string name, double fireRate, int damage, bool IsOnTheGround, int ammo, double PosX = 0, double PosY = 0) : base(id, name, fireRate, damage, IsOnTheGround, ammo, PosX, PosY)
            {
            }

            public override bool shoot(int x, int y, int px, int py, Player player)
            {
                List<int> posx = new List<int>();
                List<int> posy = new List<int>();
                for (int i = 0; i < 1000; i++)
                { 
                    if (x - px > 0)
                    {
                        posx.Add(px + 1);
                        px++;
                    }
                    else
                    {
                        posx.Add(px - 1);
                        px--;
                    }
                    if (y - py > 0)
                    {
                        posy.Add(py + 1);
                        py++;
                    }
                    else
                    {
                        posy.Add(py - 1);
                        py--;
                    }
                }
                for (int i = 0; i < posx.Count; i++)
                {
                    if (player.PosX - 12 < posx[i] && posx[i] < player.PosX + 12)
                    {
                        if (player.PosY - 12 < posy[i] && posy[i] < player.PosY + 12)
                        {
                            return true;
                        }
                    }
                    i++;
                }
                return false;
            }
        }

    }

}
