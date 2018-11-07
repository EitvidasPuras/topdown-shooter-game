

using System;
/**
* @(#) GoodImplementor.cs
*/
namespace GameServer.Models
{
	public class GoodImplementor : Implementor
	{
        public GoodImplementor() { }
		public override string react(  )
		{
            return "If a player touches this obstacle he's okay";
        }
		
	}
	
}
