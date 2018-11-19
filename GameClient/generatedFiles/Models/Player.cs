

using System;
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

            Weapon Grenade;
			
			Weapon EquippedWeapon;
			
			bool ChangedStatus;
			
			public void equipPrimary()
			{
                if (PrimaryWeapon != null)
                {
                    EquippedWeapon = PrimaryWeapon;
                }
			}
			
			public void equipSecondary()
			{
                if (SecondaryWeapon != null)
                {
                    EquippedWeapon = SecondaryWeapon;
                }
            }
			
			public void pickupPrimary(Weapon gun)
			{
                if (PrimaryWeapon == null)
                {
                    PrimaryWeapon = gun;
                    gun.equip();
                }
			}
			
			public void pickupSecondary(Weapon gun)
			{
                if (SecondaryWeapon == null)
                {
                    SecondaryWeapon = gun;
                    gun.equip();
                }
            }
			
			public bool checkEquality(Player newData)
			{
                if (this.PosX != newData.PosX)
                {
                    return true;
                }
                return false;
                //TODO: add more equality checks
            }
			
		}
		
	}
	
}
