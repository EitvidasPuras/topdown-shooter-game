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

            public Weapon getWeapon(int id, string family, string type)
            {
                Weapon w = null;
                if (family.Equals("P"))
                {
                    builder = new PrimaryWeaponBuilder();


                    if (type.Equals("M4A1"))
                    {
                        //w = new M4A1(id, "M4A1", 55, 60, true, 50);
                        return builder.startNew(id, "M4A1").addAmmo(60).addDamage(40).addFireRate(69).buildWeapon();
                    }
                    else if (type.Equals("Ak47"))
                    {
                        //w = new AK47(id, "AK47", 60, 70, true, 50);
                        return builder.startNew(id, "AK47").addAmmo(30).addDamage(60).addFireRate(60).buildWeapon();

                    }
                }
                else if (family.Equals("S"))
                {
                    builder = new SecondaryWeaponBuilder();
                    if (type.Equals("DesertEagle"))
                    {
                        //w = new DesertEagle(id, "DesertEagle", 30, 60, true, 50);
                        return builder.startNew(id, "DesertEagle").addAmmo(21).addDamage(60).addFireRate(25).buildWeapon();

                    }
                    else if(type.Equals("P250"))
                    {
                        //w = new P250(id, "P250", 30, 35, true, 50);
                        return builder.startNew(id, "P250").addAmmo(30).addDamage(20).addFireRate(35).buildWeapon();

                    }

                }
                else if (family.Equals("G"))
                {
                    builder = new GrenadeBuilder();
                    if (type.Equals("Grenade"))
                    {
                        //w = new DesertEagle(id, "DesertEagle", 30, 60, true, 50);
                        return builder.startNew(id, "Grenade").addAmmo(1).addDamage(60).buildWeapon();

                    }

                }

                return w;
            }

        }

    }

}
