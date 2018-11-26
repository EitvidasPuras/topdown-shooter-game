/**
 * @(#) Obstacle.cs
 */

namespace GameServer.Models
{
    public abstract class Obstacle
    {
        public int Id { get; set; }

        public double PosX { get; set; }

        public double PosY { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }

        public Implementor imp {get;set;}

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

        public Implementor getImplementor()
        {
            return this.imp;
        }
    }

}
