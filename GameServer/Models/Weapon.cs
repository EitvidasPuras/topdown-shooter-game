

using System;
using System.ComponentModel.DataAnnotations;
/**
* @(#) Weapon.cs
*/
namespace GameServer
{
	namespace Models
	{
        public abstract class Weapon : Decorator, Interfaces.IClonable
        {
            [Key]
            public int Id { get; set; }

            public string Name { get; set; }

            public double FireRate { get; set; }

            public int Damage { get; set; }

            public bool IsOnTheGround { get; set; }

            public int Ammo { get; set; }

            public double PosX { get; set; }

            public double PosY { get; set; }

            public Weapon(int id, string name, double fireRate, int damage, bool IsOnTheGround, int ammo, double PosX, double PosY):base(null)
            {
                this.Id = id;
                this.Name = name;
                this.FireRate = fireRate;
                this.Damage = damage;
                this.IsOnTheGround = IsOnTheGround;
                this.Ammo = ammo;
                this.PosX = PosX;
                this.PosY = PosY;
            }

            public Weapon Clone()
            {
                return (Weapon)this.MemberwiseClone();
            }

            public void setSkin(Interfaces.ISkin skin)
            {
                this.skin = skin;
            }
            public void equip()
            {

            }
            public void draw()
            {

            }

            public abstract void shoot();

        }

    }
	
}
