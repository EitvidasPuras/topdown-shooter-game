/**
 * @(#) GrenadeAdapter.cs
 */

namespace GameServer.Models
{
    public class GrenadeAdapter : Weapon
    {
        Grenade grenade = new Grenade();
        public GrenadeAdapter(int id, string name, double fireRate = 0, int damage = 0, bool IsOnTheGround = true, int ammo = 0, double PosX = 0, double PosY = 0) : base(id, name, fireRate, damage, IsOnTheGround, ammo, PosX, PosY)
        {
            //this.grenade = grenade;
        }

        //public void setGrenade(Grenade grenade)
        //{
        //    this.grenade = grenade;
        //}

        public override void shoot()
        {
            this.grenade.throwGrenade();
        }

    }

}
