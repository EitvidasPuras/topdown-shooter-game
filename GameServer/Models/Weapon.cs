

using System;
using System.ComponentModel.DataAnnotations;
/**
* @(#) Weapon.cs
*/
namespace GameServer
{
	namespace Models
	{
		public abstract class Weapon
		{
            [Key]
            public int Id { get; set; }

            public String Name { get; set; }

            public double FireRate { get; set; }

            public int Damage { get; set; }

            public bool isOnTheGround { get; set; }

            public int Ammo { get; set; }

            public void equip(  )
			{
				
			}
			
		}
		
	}
	
}
