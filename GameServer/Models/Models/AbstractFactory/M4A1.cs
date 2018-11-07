/**
 * @(#) M4A1.cs
 */

namespace GameServer
{
    namespace Models
    {
        public class M4A1 : Primary
        {
            public M4A1(int id, string name, double fireRate = 0, int damage = 0, bool IsOnTheGround = true, int ammo = 0, double PosX = 0, double PosY = 0) : base(id, name, fireRate, damage, IsOnTheGround, ammo, PosX, PosY)
            {
            }

        }

    }

}
