

using System.Collections.Generic;
/**
* @(#) Secondary.cs
*/
namespace GameServer
{
    namespace Models
    {
        public class Secondary : Weapon
        {
            public Secondary(int id, string name, double fireRate, int damage, bool IsOnTheGround, int ammo, double PosX = 0, double PosY = 0) : base(id, name, fireRate, damage, IsOnTheGround, ammo, PosX, PosY)
            {
            }

            public override bool shoot(int x, int y, int px, int py, Player player)
            {
                List<int> posx = new List<int>();
                List<int> posy = new List<int>();

                int ydiff = y - py;
                int xdiff = x - px;
                double slope = (double)(y - py) / (x - px);
                double xx, yy;
                int number = (int)System.Math.Sqrt(ydiff * ydiff + xdiff * xdiff);
                for (double i = 0; i < number; i++)
                {
                    yy = slope == 0 ? 0 : ydiff * (i / number);
                    xx = slope == 0 ? xdiff * (i / number) : yy / slope;
                    posx.Add(((int)System.Math.Round(xx) + px));
                    posy.Add(((int)System.Math.Round(yy) + py));
                }

                posx.Add(x);
                posy.Add(y);

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
