/**
 * @(#) AnyWeaponFactory.cs
 */

namespace GameServer
{
	namespace Models
	{
		public class AnyWeaponFactory : AbstractWeaponFactory
		{
			BuilderDirector director;
			
			public Weapon createWeapon( byte family, String type )
			{
				return null;
			}
			
		}
		
	}
	
}
