/**
 * @(#) PrimaryWeaponBuilder.cs
 */

namespace GameServer
{
    namespace Models
    {
        using Interfaces = GameServer.Interfaces;

        public class PrimaryWeaponBuilder : Interfaces.IBuilder
        {
            Weapon w = null;

            public Interfaces.IBuilder addAmmo(int ammo)
            {
                w.Ammo = ammo;
                return this;
            }

            public Interfaces.IBuilder addCordinates(double PosX, double PosY)
            {
                w.PosX = PosX;
                w.PosY = PosY;
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
                if (name.Equals("M4A1"))
                {
                    w = new M4A1(id, name + id);
                }
                else if (name.Equals("AK47"))
                {
                    w = new AK47(id, name + id);
                }

                return this;
            }
        }

    }

}
