using Microsoft.EntityFrameworkCore;
using System;
namespace GameServer.Models
{
    public class GameContext : DbContext
    {
        public GameContext(DbContextOptions<GameContext> options)
            : base(options)
        {
        }

        public DbSet<Game> Game { get; set; }
    }
}

