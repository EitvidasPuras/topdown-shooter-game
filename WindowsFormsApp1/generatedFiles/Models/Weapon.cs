/**
 * @(#) Weapon.cs
 */

namespace GameServer
{
	namespace Models
	{
		public abstract class Weapon
		{
			int Id;
			
			String Name;
			
			double FireRate;
			
			int Damage;
			
			boolean isOnTheGround;
			
			int Ammo;
			
			public void equip(  )
			{
				
			}
			
		}
		
	}
	
}
