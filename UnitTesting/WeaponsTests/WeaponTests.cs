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
    public class WeaponTests
    {
        [TestMethod()]
        public void CloneTest()
        {
            AnyWeaponFactory factory = new AnyWeaponFactory();
            Weapon weapon = factory.createWeapon(1, "P", "M4A1");
            Weapon weaponClone = weapon.Clone();
            Assert.AreEqual(weapon.Name, weaponClone.Name);
            Assert.AreEqual(weapon.PosX, weaponClone.PosX);
            Assert.AreEqual(weapon.PosY, weaponClone.PosY);
            Assert.IsTrue(weapon.isOnTheGround);
            Assert.IsTrue(weaponClone.isOnTheGround);
            Assert.AreEqual(weapon.Damage, weaponClone.Damage);
            Assert.AreEqual(weapon.Ammo, weaponClone.Ammo);
            Assert.AreEqual(weapon.FireRate, weaponClone.FireRate);
            Assert.AreEqual(weapon.Id, weaponClone.Id);
        }
    }
}