using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Models.Tests
{
    [TestClass()]
    public class ImplementorTests
    {
        [TestMethod()]
        public void GoodImplementorReactTest()
        {
            Player player = new Player
            {
                Name = "Studentas1",
                Score = 100,
                PosX = 49,
                PosY = 49,
                Health = 100
            };

            ObstacleFactory obstacleFactory = new ObstacleFactory();
            Obstacle obstacle = obstacleFactory.createObstacle(1, "R", "Good");
            obstacle.PosX = 50;
            obstacle.PosY = 50;

            if(Math.Abs(player.PosX - obstacle.PosX) < 11 && Math.Abs(player.PosY - obstacle.PosY) < 11)
            {
                obstacle.getImplementor().react(player);
                Assert.IsTrue(player.Health == 100);
            }
        }

        [TestMethod()]
        public void DangerousImplementorReactTest()
        {
            Player player = new Player
            {
                Name = "Studentas1",
                Score = 100,
                PosX = 49,
                PosY = 49,
                Health = 100
            };

            ObstacleFactory obstacleFactory = new ObstacleFactory();
            Obstacle obstacle = obstacleFactory.createObstacle(1, "R", "Dangerous");
            obstacle.PosX = 50;
            obstacle.PosY = 50;

            if (Math.Abs(player.PosX - obstacle.PosX) < 11 && Math.Abs(player.PosY - obstacle.PosY) < 11)
            {
                obstacle.getImplementor().react(player);
                Assert.IsTrue(player.Health != 100);
            }
        }
    }
}