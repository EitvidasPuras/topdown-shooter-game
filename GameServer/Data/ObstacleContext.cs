using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GameServer.Models
{
    public class ObstacleContext : DbContext
    {
        public ObstacleContext (DbContextOptions<ObstacleContext> options)
            : base(options)
        {
        }

        public DbSet<GameServer.Models.Obstacle> Obstacle { get; set; }
    }
}
