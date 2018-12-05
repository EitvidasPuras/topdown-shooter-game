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
        public int maxPlayers = 2;
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
                Game g = new Game { Id = 1, Full = false, IsMapReady = false, IsMapStarted = false };
                _gameContext.Add(g);
                _gameContext.SaveChanges();
            }
        }

        [HttpGet("is-full")]
        public ActionResult<bool> IsFull()
        {
            if(_playerContext.Players.Count() >= maxPlayers)
            {
                //if (AllPlayersReady())
                //{
                    var game = _gameContext.Game.Find(1);
                    game.Full = true;
                    _gameContext.Game.Update(game);
                    _gameContext.SaveChanges();

                    if (!game.IsMapReady)
                    {
                        game.IsMapReady = true;
                        _gameContext.Game.Update(game);
                        _gameContext.SaveChanges();

                        MapFacade mapFacade = new MapFacade(_obstacleContext, _weaponContext);
                        mapFacade.generateMap();
                    }

                    return true;
                //}
                //else
                //{
                //    return false;
                //}
            }
            return false;
        }

        [HttpGet("is-ready")]
        public ActionResult<bool> IsReady()
        {
            var game = _gameContext.Game.Find(1);

            if (game.Full && game.IsMapReady)
            {
                return true;
            }
            return false;
        }

        [HttpGet("start")]
        public ActionResult<bool> StartGame()
        {
            var game = _gameContext.Game.Find(1);

            if (game.Full && game.IsMapReady)
            {
                game.IsMapStarted = true;
                _gameContext.Game.Update(game);
                _gameContext.SaveChanges();

                return true;
            }
            return false;
        }

        [HttpGet("is-started")]
        public ActionResult<bool> IsStarted()
        {
            var game = _gameContext.Game.Find(1);

            

            if (_playerContext.Players.Count() < 1)
            {
                game.IsMapReady = false;
                game.Full = false;
                game.IsMapStarted = false;

                foreach (var weapon in _weaponContext.Weapons)
                {
                    _weaponContext.Weapons.Remove(weapon);
                }

                foreach (var obstacles in _obstacleContext.Obstacles)
                {
                    _obstacleContext.Obstacles.Remove(obstacles);
                }

                foreach (var rectangles in _obstacleContext.Rectangles)
                {
                    _obstacleContext.Rectangles.Remove(rectangles);
                }

                _gameContext.Game.Update(game);
                _gameContext.SaveChanges();
                _weaponContext.SaveChanges();
                _obstacleContext.SaveChanges();

                return false;
            }

            if (game.Full && game.IsMapReady && game.IsMapStarted)
            {
                return true;
            }

            return false;
            
        }

        public bool AllPlayersReady()
        {
            foreach(Player player in _playerContext.Players)
            {
                if (!player.IsReady)
                {
                    return false;
                }
            }
            return true;
        }
    }
}




