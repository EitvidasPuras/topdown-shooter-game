

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

            public double Width { get; set; }

            public double Height { get; set; }

            public Implementor imp = null;
            public Obstacle() { }
            public Obstacle(int id, double PosX, double PosY, double Width, double Height)
            {
                this.Id = id;
                this.PosX = PosX;
                this.PosY = PosY;
                this.Width = Width;
                this.Height = Height;
            }
            
            public void setImplementor(Implementor imp)
            {
                this.imp = imp;
            }

        }
		
	}
	
}
