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
    public class WeaponFactoryTest
    {
        [TestMethod()]
        public void createWeaponTest_AK47()
        {
            AnyWeaponFactory factory = new AnyWeaponFactory();
            Weapon weapon = factory.createWeapon(1, "P", "AK47");
            Assert.IsInstanceOfType(weapon, typeof(AK47));
        }
        [TestMethod()]
        public void createWeaponTest_M4A1()
        {
            AnyWeaponFactory factory = new AnyWeaponFactory();
            Weapon weapon = factory.createWeapon(1, "P", "M4A1");
            Assert.IsInstanceOfType(weapon, typeof(M4A1));
        }
        [TestMethod()]
        public void createWeaponTest_P250()
        {
            AnyWeaponFactory factory = new AnyWeaponFactory();
            Weapon weapon = factory.createWeapon(1, "S", "DesertEagle");
            Assert.IsInstanceOfType(weapon, typeof(DesertEagle));
        }
        [TestMethod()]
        public void createWeaponTest_DesertEagle()
        {
            AnyWeaponFactory factory = new AnyWeaponFactory();
            Weapon weapon = factory.createWeapon(1, "S", "P250");
            Assert.IsInstanceOfType(weapon, typeof(P250));
        }
        [TestMethod()]
        public void createWeaponTest_Grenade()
        {
            AnyWeaponFactory factory = new AnyWeaponFactory();
            Weapon weapon = factory.createWeapon(1, "G", "Grenade");
            Assert.IsInstanceOfType(weapon, typeof(GrenadeAdapter));
        }
    }
}