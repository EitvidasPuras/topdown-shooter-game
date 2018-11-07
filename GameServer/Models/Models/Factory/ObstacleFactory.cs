/**
 * @(#) ObstacleFactory.cs
 */


namespace GameServer.Models
{
    using System;
    using GameClient;
    using Interfaces = GameServer.Interfaces;

    public class ObstacleFactory : Interfaces.IObstacleFactory
    {
        Random random = new Random();
        public int MaxWidth = 800;
        public int MaxHeight = 600;
        public Obstacle createObstacle(int id, string type, string impType)
        {
            Obstacle obstacle = null;

            if (type.Equals("C"))
            {
                obstacle = new Circle(id, random.Next(10, MaxWidth - 10),
                    random.Next(10, MaxHeight - 10), random.Next(10, 50), random.Next(10, 50));
                Implementor implementor = null;
                if (impType.Equals("Good"))
                {
                    implementor = new GoodImplementor();
                    obstacle.setImplementor(implementor);
                }
                else if (impType.Equals("Dangerous"))
                {
                    implementor = new DangerousImplementor();
                    obstacle.setImplementor(implementor);
                }
            }
            else if (type.Equals("R"))
            {
                obstacle = new Rectangle(id, random.Next(10, MaxWidth - 10),
                    random.Next(10, MaxHeight - 10), random.Next(10, 50), random.Next(10, 50));
                Implementor implementor = null;
                if (impType.Equals("Good"))
                {
                    implementor = new GoodImplementor();
                    obstacle.setImplementor(implementor);
                }
                else if (impType.Equals("Dangerous"))
                {
                    implementor = new DangerousImplementor();
                    obstacle.setImplementor(implementor);
                }
            }
            return obstacle;
        }

    }

}
