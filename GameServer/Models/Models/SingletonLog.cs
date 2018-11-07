

using System;
/**
* @(#) SingletonLog.cs
*/
namespace GameServer
{
	namespace Models
	{
		public class SingletonLog
		{
            static SingletonLog instance;
			
			public String logText;
			
			public static SingletonLog getInstance(  )
			{
                if (instance == null)
                {
                    instance = new SingletonLog();
                }

                return instance;
            }

		}
		
	}
	
}
