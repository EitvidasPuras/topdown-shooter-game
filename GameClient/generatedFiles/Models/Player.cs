

using GameClient.generatedFiles.Models;
using GameServer.Interfaces;
using System;
/**
* @(#) Player.cs
*/
namespace GameServer
{
	namespace Models
	{
		public class Player : PlayerTemplate
		{
            public long Id { get; set; }
            public string Name { get; set; }
            public long Score { get; set; }
            public long PosX { get; set; }
            public long PosY { get; set; }
            public int Health { get; set; }
            public bool IsHost { get; set; }
            public bool IsReady { get; set; }
            public Primary PrimaryWeapon { get; set; }

            public Secondary SecondaryWeapon { get; set; }

            public GrenadeAdapter Grenade { get; set; }

            public int EquippedWeaponID { get; set; }

            public bool ChangedStatus { get; set; }

            public IState state { get; set; }

            public void setState()
            {
                if(Health > 70)
                {
                    state = new HealthyState();
                }
                else if(Health > 50)
                {
                    state = new HurtedState();
                }
                else
                {
                    state = new CriticalState();
                }
            }
            public void SetFakeWeapons()
            {
                Random rnd = new Random();
                PrimaryWeapon = new AK47(rnd.Next(-1000, -1), "-1");
                SecondaryWeapon = new DesertEagle(rnd.Next(-1000, -1), "-1");
            }

            public void equipPrimary()
			{
                if (PrimaryWeapon != null)
                {
                    EquippedWeaponID = PrimaryWeapon.Id;
                }
			}
			
			public void equipSecondary()
			{
                if (SecondaryWeapon != null)
                {
                    EquippedWeaponID = SecondaryWeapon.Id;
                }
            }
            public void equipGrenade()
			{
                if (Grenade != null)
                {
                    EquippedWeaponID = Grenade.Id;
                }
            }

            public Weapon GetEquippedWeapon()
            {
                if (PrimaryWeapon != null && PrimaryWeapon.Id == EquippedWeaponID) return PrimaryWeapon;
                if (SecondaryWeapon != null && SecondaryWeapon.Id == EquippedWeaponID) return SecondaryWeapon;
                if (Grenade != null && Grenade.Id == EquippedWeaponID) return Grenade;
                return null;
            }

            public bool pickupPrimary(Primary gun)
            {
                if (PrimaryWeapon == null || PrimaryWeapon.Id < 0)
                {
                    PrimaryWeapon = gun;
                    equipPrimary();
                    return true;
                }

                return false;
            }

            public bool pickupSecondary(Secondary gun)
            {
                if (SecondaryWeapon == null || SecondaryWeapon.Id < 0)
                {
                    SecondaryWeapon = gun;
                    equipSecondary();
                    return true;
                }

                return false;
            }

            public bool pickupGrenade(GrenadeAdapter gun)
            {
                if (Grenade == null)
                {
                    Grenade = gun;
                    //Equip grenade?
                    return true;
                }

                return false;
            }

            public bool checkEquality(Player newData)
			{
                if (this.PosX != newData.PosX || this.PosY != newData.PosY || Health != newData.Health)
                {
                    return false;
                }
                return true;
                //TODO: add more equality checks
            }
			
            public void move(string Direction)
            {
                switch (Direction)
                {
                    case "up":
                        PosY -= 15;
                        break;
                    case "left":
                        PosX -= 15;
                        break;
                    case "down":
                        PosY += 15;
                        break;
                    case "right":
                        PosX += 15;
                        break;
                }
            }

            public bool shoot(int x, int y, Player player)
            {
                return GetEquippedWeapon().shoot(x, y, (int)this.PosX, (int)this.PosY, player);
            }

            public override bool CheckIfHost()
            {
                return this.IsHost;
            }

            public override bool CheckIfReady()
            {
                return this.IsReady;
            }
        }
		
	}
	
}
