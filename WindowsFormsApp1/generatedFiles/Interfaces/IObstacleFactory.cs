/**
 * @(#) IObstacleFactory.cs
 */

namespace GameServer
{
	namespace Interfaces
	{
		public interface IObstacleFactory
		{
			GameServer.Models.Obstacle createObstacle( String type );
			
		}
		
	}
	
}
