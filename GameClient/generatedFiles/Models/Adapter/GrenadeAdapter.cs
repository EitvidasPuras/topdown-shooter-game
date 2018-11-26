/**
 * @(#) GrenadeAdapter.cs
 */

namespace GameServer.Models
{
    public class GrenadeAdapter : Weapon
    {
        Grenade grenade;
        public GrenadeAdapter(int id, string name, Grenade grenade, double fireRate = 0, int damage = 0, bool IsOnTheGround = true, int ammo = 0, double PosX = 0, double PosY = 0) : base(id, name, fireRate, damage, IsOnTheGround, ammo, PosX, PosY)
        {
            this.grenade = grenade;
        }

        public override bool shoot(int x, int y, int px, int py, Player player)
        {
            return this.grenade.throwGrenade(x, y, px, py, player);
        }

    }

}
