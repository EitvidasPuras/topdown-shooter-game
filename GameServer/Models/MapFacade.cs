/**
 * @(#) MapGenerator.cs
 */

namespace GameServer
{
    namespace Models
    {
        public class MapFacade
        {
            ObstacleFactory obstacleFactory;
            AnyWeaponFactory weaponFactory;

            ObstacleContext obstacleContext;
            WeaponContext weaponContext;

            public MapFacade(ObstacleContext obstacleContext, WeaponContext weaponContext)
            {
                weaponFactory = new AnyWeaponFactory();
                obstacleFactory = new ObstacleFactory();
                this.obstacleContext = obstacleContext;
                this.weaponContext = weaponContext;
            }

            public void generateMap()
            {
                int count = 1;
                for (int i = 1; i < 6; i++)
                {
                    Obstacle obs = null;
                    Weapon weapon = null;
                    if (i % 2 == 0)
                    {
                        obs = obstacleFactory.createObstacle(count, "C", "Dangerous");
                        obstacleContext.Add(obs);
                        obs = obstacleFactory.createObstacle(count + 1, "R", "Dangerous");
                        obstacleContext.Add(obs);

                        weapon = weaponFactory.createWeapon(count, "P", "M4A1");
                        weaponContext.Add(weapon);
                        weapon = weaponFactory.createWeapon(count + 1, "S", "P250");                       
                        weaponContext.Add(weapon);
                    }
                    else
                    {
                        obs = obstacleFactory.createObstacle(count, "R", "Good");
                        obstacleContext.Add(obs);
                        obs = obstacleFactory.createObstacle(count + 1, "C", "Good");
                        obstacleContext.Add(obs);

                        weapon = weaponFactory.createWeapon(count, "S", "DesertEagle");
                        weaponContext.Add(weapon);
                        weapon = weaponFactory.createWeapon(count + 1, "P", "AK47");
                        weaponContext.Add(weapon);
                    }

                    count += 2;
                }
                weaponContext.SaveChanges();
                obstacleContext.SaveChanges();

            }

        }

    }

}
