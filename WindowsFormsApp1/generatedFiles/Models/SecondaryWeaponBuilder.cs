/**
 * @(#) SecondaryWeaponBuilder.cs
 */

namespace GameServer
{
	namespace Models
	{
		using Interfaces = GameServer.Interfaces;

        public class SecondaryWeaponBuilder : Interfaces.IBuilder
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
                if (name.Equals("P250"))
                {
                    w = new P250(id, name + id);
                }
                else if (name.Equals("DesertEagle"))
                {
                    w = new DesertEagle(id, name + id);
                }

                return this;
            }
        }

    }
	
}
