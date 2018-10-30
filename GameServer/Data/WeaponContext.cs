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

        public DbSet<GameServer.Models.Weapon> Weapon { get; set; }
    }
}
