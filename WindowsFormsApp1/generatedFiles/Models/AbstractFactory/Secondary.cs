/**
 * @(#) Secondary.cs
 */

namespace GameServer
{
	namespace Models
	{
		public class Secondary : Weapon
		{
            public Secondary(int id, string name, double fireRate, int damage, bool IsOnTheGround, int ammo) : base(id, name, fireRate, damage, IsOnTheGround, ammo)
            {
            }
        }
		
	}
	
}
