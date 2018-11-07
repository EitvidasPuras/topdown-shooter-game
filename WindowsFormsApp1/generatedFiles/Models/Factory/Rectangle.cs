/**
 * @(#) Rectangle.cs
 */

namespace GameServer.Models
{
    public class Rectangle : Obstacle
    {
        public Rectangle(int id, double PosX, double PosY, double Width, double Height) : base(id, PosX, PosY, Width, Height)
        { }
    }

}
