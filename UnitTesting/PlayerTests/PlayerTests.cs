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
        public void PlayerMoveUpTest()
        {
            Player player = new Player
            {
                Name = "Studentas1",
                Score = 100,
                PosX = 50,
                PosY = 50
            };

            player.move("up");
            Assert.IsTrue(player.PosY != 50 && player.PosX == 50);
        }

        [TestMethod()]
        public void PlayerMoveDownTest()
        {
            Player player = new Player
            {
                Name = "Studentas1",
                Score = 100,
                PosX = 50,
                PosY = 50
            };

            player.move("down");
            Assert.IsTrue(player.PosY != 50 && player.PosX == 50);
        }

        [TestMethod()]
        public void PlayerMoveLeftTest()
        {
            Player player = new Player
            {
                Name = "Studentas1",
                Score = 100,
                PosX = 50,
                PosY = 50
            };

            player.move("left");
            Assert.IsTrue(player.PosY == 50 && player.PosX != 50);
        }

        [TestMethod()]
        public void PlayerMoveRightTest()
        {
            Player player = new Player
            {
                Name = "Studentas1",
                Score = 100,
                PosX = 50,
                PosY = 50
            };

            player.move("right");
            Assert.IsTrue(player.PosY == 50 && player.PosX != 50);
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
            Player player3 = new Player
            {
                Name = "Studentas3",
                Score = 100,
                PosX = 50,
                PosY = 50
            };
            Player player4 = new Player
            {
                Name = "Studentas4",
                Score = 100,
                PosX = 50,
                PosY = 50
            };
            Player player5 = new Player
            {
                Name = "Studentas5",
                Score = 100,
                PosX = 50,
                PosY = 50
            };
            Player player6 = new Player
            {
                Name = "Studentas6",
                Score = 100,
                PosX = 50,
                PosY = 50
            };
            AnyWeaponFactory factory = new AnyWeaponFactory();
            Weapon weapon = factory.createWeapon(1, "P", "AK47");
            Weapon weapon2 = factory.createWeapon(1, "S", "P250");
            Weapon weapon3 = factory.createWeapon(1, "P", "M4A1");
            Weapon weapon4 = factory.createWeapon(1, "S", "DesertEagle");
            Weapon weapon5 = factory.createWeapon(1, "G", "Grenade");
            player3.pickupSecondary(weapon2);
            player3.equipSecondary();
            player4.pickupSecondary(weapon4);
            player4.equipSecondary();

            player.pickupPrimary(weapon);
            player.equipPrimary();
            player5.pickupPrimary(weapon3);
            player5.equipPrimary();

            player6.pickupGrenade(weapon5);
            player6.equipGrenade();

            Assert.IsTrue(player.shoot(300, 300, player2));
            Assert.IsTrue(player.shoot(600, 600, player2));
            Assert.IsFalse(player.shoot(0, 0, player2));
            Assert.IsFalse(player.shoot(50, 50, player2));
            Assert.IsFalse(player.shoot(100, 50, player2));
            Assert.IsFalse(player.shoot(50, 100, player2));

            Assert.IsTrue(player3.shoot(300, 300, player2));
            Assert.IsTrue(player3.shoot(600, 600, player2));
            Assert.IsFalse(player3.shoot(0, 0, player2));
            Assert.IsFalse(player3.shoot(50, 50, player2));
            Assert.IsFalse(player3.shoot(100, 50, player2));
            Assert.IsFalse(player3.shoot(50, 100, player2));

            Assert.IsTrue(player4.shoot(300, 300, player2));
            Assert.IsTrue(player4.shoot(600, 600, player2));
            Assert.IsFalse(player4.shoot(0, 0, player2));
            Assert.IsFalse(player4.shoot(50, 50, player2));
            Assert.IsFalse(player4.shoot(100, 50, player2));
            Assert.IsFalse(player4.shoot(50, 100, player2));

            Assert.IsTrue(player5.shoot(300, 300, player2));
            Assert.IsTrue(player5.shoot(600, 600, player2));
            Assert.IsFalse(player5.shoot(0, 0, player2));
            Assert.IsFalse(player5.shoot(50, 50, player2));
            Assert.IsFalse(player5.shoot(100, 50, player2));
            Assert.IsFalse(player5.shoot(50, 100, player2));

            Assert.IsTrue(player6.shoot(200, 200, player2));
            Assert.IsFalse(player6.shoot(600, 600, player2));
            Assert.IsFalse(player6.shoot(0, 0, player2));
            Assert.IsFalse(player6.shoot(50, 50, player2));
            Assert.IsFalse(player6.shoot(100, 50, player2));
            Assert.IsFalse(player6.shoot(50, 100, player2));
        }
    }
}