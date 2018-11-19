

using GameServer.Models;
/**
* @(#) IBuilder.cs
*/
namespace GameServer
{
    namespace Interfaces
    {
        public interface IClonable
        {
            Weapon Clone();
        }

    }

}
