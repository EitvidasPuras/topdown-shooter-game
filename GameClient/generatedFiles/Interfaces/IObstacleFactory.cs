/**
 * @(#) IObstacleFactory.cs
 */

namespace GameServer.Interfaces
{
    public interface IObstacleFactory
    {
        GameServer.Models.Obstacle createObstacle(int id, string type, string impType);

    }

}
