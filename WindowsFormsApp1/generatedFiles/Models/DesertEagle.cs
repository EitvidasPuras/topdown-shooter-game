/**
 * @(#) DesertEagle.cs
 */

namespace GameServer
{
	namespace Models
	{
		public class DesertEagle : Secondary
		{
            public DesertEagle(int id, string name, double fireRate = 0, int damage = 0, bool IsOnTheGround = true, int ammo = 0) : base(id, name, fireRate, damage, IsOnTheGround, ammo)
            {
            }
        }
		
	}
	
}
