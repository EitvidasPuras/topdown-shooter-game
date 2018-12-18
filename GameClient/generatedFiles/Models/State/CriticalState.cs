

using GameServer.Interfaces;
/**
* @(#) Obstacle.cs
*/
namespace GameServer.Models
{
    public class CriticalState : IState
    {
        public int getMovementStep()
        {
            return 2;
        }

    }
}
