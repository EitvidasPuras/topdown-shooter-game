/**
 * @(#) AnyWeaponFactory.cs
 */

namespace GameServer
{
	namespace Models
	{
		public class AnyWeaponFactory : AbstractWeaponFactory
		{
			BuilderDirector director  = new BuilderDirector();
			
			public override Weapon createWeapon(int id, string family, string type )
			{
                Weapon w = null;

                if (family.Equals("P"))
                {
                    if (type.Equals("M4A1"))
                    {
                        w = director.getWeapon(id,family, type);
                        //w = new M4A1(id, "M4A1", 55, 60, true, 50);
                    }
                    else
                    if (type.Equals("AK47"))
                    {
                        w = director.getWeapon(id, family, type);
                        //w = new AK47(id, "AK47", 60, 70, true, 50);
                    }
                }
                else
                if (family.Equals("S"))
                {
                    if (type.Equals("DesertEagle"))
                    {
                        w = director.getWeapon(id, family, type);
                        //w = new DesertEagle(id, "DesertEagle", 30, 60, true, 50);
                    }
                    else
                    if (type.Equals("P250"))
                    {
                        w = director.getWeapon(id, family, type);
                        //w = new P250(id, "P250", 30, 35, true, 50);
                    }

                }
                else
                if (family.Equals("G"))
                {
                    if (type.Equals("Grenade"))
                    {
                        w = director.getWeapon(id, family, type);
                        //w = new DesertEagle(id, "DesertEagle", 30, 60, true, 50);
                    }

                }
                return w;
            }
			
		}
		
	}
	
}
