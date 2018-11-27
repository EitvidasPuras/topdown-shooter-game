

using System;
/**
* @(#) DangerousImplementor.cs
*/
namespace GameServer.Models
{
	public class DangerousImplementor : Implementor
	{
		public override void react(Player player)
		{
            player.Health = player.Health - 10;
		}
		
	}
	
}
