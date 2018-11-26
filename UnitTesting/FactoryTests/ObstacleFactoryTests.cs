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
    public class ObstacleFactoryTests
    {
        [TestMethod()]
        public void createObstacleTest_DangerousCircle()
        { 
            ObstacleFactory of = new ObstacleFactory();
            Obstacle circle = of.createObstacle(1, "C", "Dangerous");

            Assert.AreNotEqual(circle, null);            
            Assert.IsInstanceOfType(circle.imp, typeof(DangerousImplementor)); 
        }

        [TestMethod()]
        public void createObstacleTest_GoodCircle()
        {
            ObstacleFactory of = new ObstacleFactory();
            Obstacle circle = of.createObstacle(1, "C", "Good");

            Assert.AreNotEqual(circle, null);
            Assert.IsInstanceOfType(circle.imp, typeof(GoodImplementor));
        }

        [TestMethod()]
        public void createObstacleTest_DangerousRectangle()
        {
            ObstacleFactory of = new ObstacleFactory();
            Obstacle rectangle = of.createObstacle(1, "R", "Dangerous");

            Assert.AreNotEqual(rectangle, null);
            Assert.IsInstanceOfType(rectangle.getImplementor(), typeof(DangerousImplementor));
        }

        [TestMethod()]
        public void createObstacleTest_GoodRectangle()
        {
            ObstacleFactory of = new ObstacleFactory();
            Obstacle rectangle = of.createObstacle(1, "R", "Good");

            Assert.AreNotEqual(rectangle, null);
            Assert.IsInstanceOfType(rectangle.getImplementor(), typeof(GoodImplementor));
        }
    }
}