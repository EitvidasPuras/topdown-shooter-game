/**
 * @(#) IBuilder.cs
 */

namespace GameServer
{
    namespace Interfaces
    {
        public interface IBuilder
        {
            IBuilder startNew(int id, string name);
            IBuilder addAmmo(int ammo);

            IBuilder addDamage(int damage);

            IBuilder addFireRate(double fireRate);

            IBuilder addCordinates(double PosX, double PosY);

            GameServer.Models.Weapon buildWeapon();

        }

    }

}
