/**
* @(#) Weapon.cs
*/
namespace GameServer
{
	namespace Models
	{
        public abstract class Weapon
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public double FireRate { get; set; }

            public int Damage { get; set; }
			
			public bool isOnTheGround { get; set; }
			
			public int Ammo { get; set; }

            public Weapon(int id, string name, double fireRate, int damage, bool IsOnTheGround, int ammo) {
                this.Id = id;
                this.Name = name;
                this.FireRate = fireRate;
                this.Damage = damage;
                this.isOnTheGround = IsOnTheGround;
                this.Ammo = ammo;
            }
			public void equip(  )
			{
				
			}
			
		}
		
	}
	
}
