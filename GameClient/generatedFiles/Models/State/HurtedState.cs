

using GameServer.Interfaces;
/**
* @(#) Circle.cs
*/
namespace GameServer.Models
{
    public class HurtedState : IState
    {
        public int getMovementStep()
        {
            return 3;
        }
    }
}
