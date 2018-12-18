

using GameServer.Interfaces;
/**
* @(#) Obstacle.cs
*/
namespace GameServer.Models
{
    public class HealthyState : IState
    {
        public int getMovementStep()
        {
            return 5;
        }

    }

}
