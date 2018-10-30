

using System.ComponentModel.DataAnnotations;
/**
* @(#) Obstacle.cs
*/
namespace GameServer
{
	namespace Models
	{
		public abstract class Obstacle
		{
            [Key]
			public int Id { get; set; }

            public double PosX { get; set; }

            public double PosY { get; set; }

        }
		
	}
	
}
