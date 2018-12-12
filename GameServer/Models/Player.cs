

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

            public bool IsHost { get; set; }

            public bool IsReady { get; set; }

            public Primary PrimaryWeapon { get; set; }

            public Secondary SecondaryWeapon { get; set; }

            public GrenadeAdapter Grenade { get; set; }

            public int EquippedWeaponID { get; set; }

            public bool ChangedStatus { get; set; }

            public Player()
            {

                //Random rnd = new Random();
                //PrimaryWeapon = new AK47(rnd.Next(-1000, -1), "-1");
                //SecondaryWeapon = new DesertEagle(rnd.Next(-1000, -1), "-1");
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

            public bool pickupPrimary(Primary gun)
            {
                if (PrimaryWeapon == null || PrimaryWeapon.Id < 0)
                {
                    gun.IsOnTheGround = false;
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
                    gun.IsOnTheGround = false;
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
                    gun.IsOnTheGround = false;
                    Grenade = gun;
                    //Equip grenade?
                    return true;
                }

                return false;
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

            public Weapon GetEquippedWeapon()
            {
                if (PrimaryWeapon.Id == EquippedWeaponID) return PrimaryWeapon;
                if (SecondaryWeapon.Id == EquippedWeaponID) return SecondaryWeapon;
                if (Grenade.Id == EquippedWeaponID) return Grenade;
                return null;
            }

        }

    }

}

