using System;
using System.ComponentModel.DataAnnotations;
namespace GameServer.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        public bool Full { get; set; }
        public bool IsMapReady { get; set; }
        public Game()
        {
        }
    }
}
