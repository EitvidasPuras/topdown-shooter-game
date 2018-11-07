using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameServer.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GameServer.Controllers
{
    [Route("api/game")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly GameContext _gameContext;
        private readonly PlayerContext _playerContext;
        private readonly ObstacleContext _obstacleContext;
        private readonly WeaponContext _weaponContext;
        public int Qty { get; set; } = 0;
        public int maxPlayers = 5;
        //private bool mapIsBeingGenerated = false;
        //private bool mapGenerated = false;
        //private bool roomIsFull = false;

        public GameController(GameContext game, PlayerContext context, ObstacleContext obstacle, WeaponContext weapon)
        {
            _gameContext = game;
            _playerContext = context;
            _obstacleContext = obstacle;
            _weaponContext = weapon;

            if (_gameContext.Game.Count() == 0)
            {
                // Create a new Game if collection is empty
                Game g = new Game { Id = 1, Full = false, IsMapReady = false };
                _gameContext.Add(g);
                _gameContext.SaveChanges();
            }
        }

        [HttpGet("is-full")]
        public ActionResult<bool> GetFull()
        {
            if(_playerContext.Players.Count() >= maxPlayers)
            {
                var game = _gameContext.Game.Find(1);
                game.Full = true;
                _gameContext.Game.Update(game);
                _gameContext.SaveChanges();
                //roomIsFull = true;

                //if (!mapIsBeingGenerated)
                //{
                //    mapIsBeingGenerated = true;
                    
                //    MapFacade mapFacade = new MapFacade(_obstacleContext, _weaponContext);
                //    mapFacade.generateMap();

                //    mapGenerated = true;
                //}

                if (!game.IsMapReady)
                {
                    game.IsMapReady = true;
                    _gameContext.Game.Update(game);
                    _gameContext.SaveChanges();

                    MapFacade mapFacade = new MapFacade(_obstacleContext, _weaponContext);
                    mapFacade.generateMap();
                }

                return true;
            }
            return false;
        }

        [HttpGet("is-ready")]
        public ActionResult<bool> GetReady()
        {
            var game = _gameContext.Game.Find(1);

            if (game.Full && game.IsMapReady)
            {
                return true;
            }
            return false;
        }
    }
}




