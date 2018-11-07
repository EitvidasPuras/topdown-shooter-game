using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GameServer.Models
{
    public class WeaponContext : DbContext
    {
        public WeaponContext (DbContextOptions<WeaponContext> options)
            : base(options)
        {
        }

        public DbSet<GameServer.Models.Weapon> Weapons { get; set; }
        public DbSet<GameServer.Models.M4A1> M4A1 { get; set; }
        public DbSet<GameServer.Models.AK47> AK47 { get; set; }
        public DbSet<GameServer.Models.P250> P250 { get; set; }
        public DbSet<GameServer.Models.DesertEagle> DesertEagle { get; set; }
        public DbSet<GameServer.Models.GrenadeAdapter> GrenadeAdapter { get; set; }

    }
}
