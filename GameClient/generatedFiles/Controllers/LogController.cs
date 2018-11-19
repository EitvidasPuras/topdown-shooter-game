

using GameServer.Models;
using System;
/**
* @(#) LogController.cs
*/
namespace GameServer
{
	namespace Controllers
	{
		public class LogController
		{
			public String getLog()
			{
                return SingletonLog.getInstance().logText;
			}
			
		}
		
	}
	
}
