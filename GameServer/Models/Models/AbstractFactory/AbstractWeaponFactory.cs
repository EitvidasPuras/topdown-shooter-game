/**
 * @(#) AbstractWeaponFactory.cs
 */

namespace GameServer
{
	namespace Models
	{
		public abstract class AbstractWeaponFactory
		{
            public abstract Weapon createWeapon(int id, string family, string type);
			
		}
		
	}
	
}
