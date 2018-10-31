/**
 * @(#) AK47.cs
 */

namespace GameServer
{
	namespace Models
	{
		public class AK47 : Primary
		{
            public AK47(int id, string name, double fireRate = 0, int damage = 0, bool IsOnTheGround = true, int ammo = 0) : base(id, name, fireRate, damage, IsOnTheGround, ammo)
            {
            }
        }
		
	}
	
}
