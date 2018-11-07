/**
 * @(#) Circle.cs
 */

namespace GameServer.Models
{
    public class Circle : Obstacle
    {
        public Circle(int id, double PosX, double PosY, double Width, double Height) : base(id, PosX, PosY, Width, Height)
        { }
    }

}
