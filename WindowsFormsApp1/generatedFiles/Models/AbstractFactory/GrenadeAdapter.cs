/**
 * @(#) GrenadeAdapter.cs
 */

namespace GameServer.Models
{
	public class GrenadeAdapter : Weapon
	{
		Grenade grenade;
        public GrenadeAdapter(int id, string name, Grenade grenade, double fireRate = 0, int damage = 0, bool IsOnTheGround = true, int ammo = 0) : base(id, name, fireRate, damage, IsOnTheGround, ammo)
        {
            this.grenade = grenade;
        }

        public override void shoot(  )
		{
            this.grenade.throwGrenade();
		}
		
	}
	
}
