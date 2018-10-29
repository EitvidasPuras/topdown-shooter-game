/**
 * @(#) Player.cs
 */

namespace GameServer
{
	namespace Models
	{
		public class Player
		{
			private int Id;

            String Name;
			
			int Score;
			
			double PosX;
			
			double PosY;
			
			int Health;
			
			Weapon PrimaryWeapon;
			
			Weapon SecondaryWeapon;
			
			Weapon EquippedWeapon;
			
			boolean ChangedStatus;
			
			public void equipPrimary(  )
			{
				
			}
			
			public void equipSecondary(  )
			{
				
			}
			
			public void pickupPrimary( Weapon gun )
			{
				
			}
			
			public void pickupSecondary( Weapon gun )
			{
				
			}
			
			public boolean checkEquality( Player newData )
			{
				return null;
			}
			
		}
		
	}
	
}
