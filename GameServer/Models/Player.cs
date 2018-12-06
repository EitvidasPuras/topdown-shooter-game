

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

            public Weapon EquippedWeapon { get; set; }

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

            public bool pickupPrimary(Primary gun)
            {
                if (PrimaryWeapon == null || PrimaryWeapon.Id < 0)
                {
                    PrimaryWeapon = gun;
                    gun.equip();
                    return true;
                }

                return false;
            }

            public bool pickupSecondary(Secondary gun)
            {
                if (SecondaryWeapon == null || SecondaryWeapon.Id < 0)
                {
                    SecondaryWeapon = gun;
                    gun.equip();
                    return true;
                }

                return false;
            }

            public bool pickupGrenade(GrenadeAdapter gun)
            {
                if (Grenade == null)
                {
                    Grenade = gun;
                    gun.equip();
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

        }

    }

}

