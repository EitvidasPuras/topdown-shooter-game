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
    public class PlayerTests
    {
        [TestMethod()]
        public void checkEqualityTest()
        {
            Player player = new Player
            {
                Name = "Studentas1",
                Score = 100,
                PosX = 50,
                PosY = 50
            };

            Player player2 = new Player
            {
                Name = "Studentas2",
                Score = 100,
                PosX = 50,
                PosY = 50
            };

            player.move("up");
            Assert.IsFalse(player.checkEquality(player2));
            Assert.IsTrue(player2.checkEquality(player2));
        }

        [TestMethod()]
        public void checkShootTest()
        {
            Player player = new Player
            {
                Name = "Studentas1",
                Score = 100,
                PosX = 50,
                PosY = 50
            };

            Player player2 = new Player
            {
                Name = "Studentas2",
                Score = 100,
                PosX = 300,
                PosY = 300
            };
            AnyWeaponFactory factory = new AnyWeaponFactory();
            Weapon weapon = factory.createWeapon(1, "P", "AK47");

            player.pickupPrimary(weapon);
            player.equipPrimary();

            Assert.IsTrue(player.shoot(300, 300, player2));
            Assert.IsFalse(player.shoot(0, 0, player2));
        }
    }
}