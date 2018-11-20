

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
            public long Id { get; set; }
            public string Name { get; set; }
            public long Score { get; set; }
            public long PosX { get; set; }
            public long PosY { get; set; }
            public int Health { get; set; }

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
                if (this.PosX != newData.PosX || this.PosY != newData.PosY)
                {
                    return false;
                }
                return true;
                //TODO: add more equality checks
            }
			
		}
		
	}
	
}
