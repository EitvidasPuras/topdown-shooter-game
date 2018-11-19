/**
 * @(#) Decorator.cs
 */

namespace GameServer.Models
{
	using Interfaces = GameServer.Interfaces;
	
	public abstract class Decorator : Interfaces.ISkin
	{
		protected Interfaces.ISkin skin = null;

        public Decorator(Interfaces.ISkin skin)
        {
            this.skin = skin;
        }

        public void draw(  )
		{
			
		}
		
	}
	
}
