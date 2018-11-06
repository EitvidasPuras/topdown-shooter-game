/**
 * @(#) SecondaryWeaponBuilder.cs
 */

namespace GameServer
{
	namespace Models
	{
		using Interfaces = GameServer.Interfaces;

        public class GrenadeBuilder : Interfaces.IBuilder
        {
            Weapon w = null;

            public Interfaces.IBuilder addAmmo(int ammo)
            {
                w.Ammo = ammo;
                return this;
            }

            public Interfaces.IBuilder addDamage(int damage)
            {
                w.Damage = damage;
                return this;
            }

            public Interfaces.IBuilder addFireRate(double fireRate)
            {
                w.FireRate = fireRate;
                return this;
            }

            public Weapon buildWeapon()
            {
                return w;
            }

            public Interfaces.IBuilder startNew(int id, string name)
            {
                if (name.Equals("Grenade"))
                {
                    Grenade grenade = new Grenade();
                    w = new GrenadeAdapter(id, name + id, grenade);
                }
                return this;
            }
        }

    }
	
}
