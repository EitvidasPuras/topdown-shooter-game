

using System;
/**
* @(#) GoodImplementor.cs
*/
namespace GameServer.Models
{
	public class GoodImplementor : Implementor
	{
		public override void react( Player player )
		{
            Console.WriteLine("If a player touches this obstacle he's okay");
        }
		
	}
	
}
