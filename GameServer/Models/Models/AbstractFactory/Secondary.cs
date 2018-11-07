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

            public override void shoot()
            {
                throw new System.NotImplementedException();
            }
        }

    }

}
