

using GameClient;
using System;
/**
* @(#) BuilderDirector.cs
*/
namespace GameServer
{
    namespace Models
    {
        public class BuilderDirector
        {
            GameServer.Interfaces.IBuilder builder;
            Random random = new Random();
            public int MaxWidth = 800;
            public int MaxHeight = 600;
            public Weapon getWeapon(int id, string family, string type)
            {
                Weapon w = null;
                double PosX = random.Next(10, MaxWidth - 10);
                double PosY = random.Next(10, MaxHeight - 10);
                if (family.Equals("P"))
                {
                    builder = new PrimaryWeaponBuilder();


                    if (type.Equals("M4A1"))
                    {
                        //w = new M4A1(id, "M4A1", 55, 60, true, 50);
                        return builder.startNew(id, "M4A1").addAmmo(60).addDamage(40).addFireRate(69).addCordinates(PosX, PosY).buildWeapon();
                    }
                    else if (type.Equals("AK47"))
                    {
                        //w = new AK47(id, "AK47", 60, 70, true, 50);
                        return builder.startNew(id, "AK47").addAmmo(30).addDamage(60).addFireRate(60).addCordinates(PosX, PosY).buildWeapon();

                    }
                }
                else if (family.Equals("S"))
                {
                    builder = new SecondaryWeaponBuilder();
                    if (type.Equals("DesertEagle"))
                    {
                        //w = new DesertEagle(id, "DesertEagle", 30, 60, true, 50);
                        return builder.startNew(id, "DesertEagle").addAmmo(21).addDamage(60).addFireRate(25).addCordinates(PosX, PosY).buildWeapon();

                    }
                    else if (type.Equals("P250"))
                    {
                        //w = new P250(id, "P250", 30, 35, true, 50);
                        return builder.startNew(id, "P250").addAmmo(30).addDamage(20).addFireRate(35).addCordinates(PosX, PosY).buildWeapon();

                    }

                }
                else if (family.Equals("G"))
                {
                    builder = new GrenadeBuilder();
                    if (type.Equals("Grenade"))
                    {
                        //w = new DesertEagle(id, "DesertEagle", 30, 60, true, 50);
                        return builder.startNew(id, "Grenade").addAmmo(1).addDamage(60).addCordinates(PosX, PosY).buildWeapon();

                    }

                }

                return w;
            }

        }

    }

}
