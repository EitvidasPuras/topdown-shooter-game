/**
 * @(#) ObstacleFactory.cs
 */

namespace GameServer
{
	namespace Models
	{
		using Interfaces = GameServer.Interfaces;
		
		public class ObstacleFactory : Interfaces.IObstacleFactory
		{
			public Obstacle createObstacle( String type )
			{
				return null;
			}
			
		}
		
	}
	
}
