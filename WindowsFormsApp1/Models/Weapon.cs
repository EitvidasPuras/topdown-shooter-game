using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.Models
{
    public class Weapon
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int FireRate { get; set; }
        public int Damage { get; set; }
        public bool IsOnTheGround { get; set; }
        public int Ammo { get; set; }

    }
}
