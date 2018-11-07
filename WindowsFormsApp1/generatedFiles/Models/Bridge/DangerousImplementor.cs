

using System;
/**
* @(#) DangerousImplementor.cs
*/
namespace GameServer.Models
{
	public class DangerousImplementor : Implementor
	{
		public override void react(  )
		{
            Console.WriteLine("If a player touches this obstacle he explodes");
		}
		
	}
	
}
