

using System;
/**
* @(#) DangerousImplementor.cs
*/
namespace GameServer.Models
{
	public class DangerousImplementor : Implementor
	{
        public DangerousImplementor() { }
		public override string react(  )
		{
            return "If a player touches this obstacle he explodes";
		}
		
	}
	
}
