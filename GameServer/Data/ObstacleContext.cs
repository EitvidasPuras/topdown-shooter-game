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

        public DbSet<GameServer.Models.Obstacle> Obstacles { get; set; }
        public DbSet<GameServer.Models.Circle> Circles { get; set; }
        public DbSet<GameServer.Models.Rectangle> Rectangles { get; set; }
    }
}
