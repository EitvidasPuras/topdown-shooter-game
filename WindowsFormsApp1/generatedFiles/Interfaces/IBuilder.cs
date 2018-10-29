/**
 * @(#) IBuilder.cs
 */

namespace GameServer
{
	namespace Interfaces
	{
		public interface IBuilder
		{
			IBuilder addAmmo(  );
			
			IBuilder addDamage(  );
			
			IBuilder addFireRate(  );
			
			GameServer.Models.Weapon buildWeapon(  );
			
		}
		
	}
	
}
