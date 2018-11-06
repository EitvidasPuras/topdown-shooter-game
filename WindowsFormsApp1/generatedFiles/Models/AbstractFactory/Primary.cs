/**
 * @(#) Primary.cs
 */

namespace GameServer
{
    namespace Models
    {
        public class Primary : Weapon
        {
            public Primary(int id, string name, double fireRate, int damage, bool IsOnTheGround, int ammo) : base(id, name, fireRate, damage, IsOnTheGround, ammo)
            {
            }

            public override void shoot()
            {
                throw new System.NotImplementedException();
            }
        }

    }

}
