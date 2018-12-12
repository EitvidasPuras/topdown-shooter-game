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

        private DamageCounter Counter1;
        private DamageCounter Counter2;
        private DamageCounter Counter3;
        private DamageCounter Counter4;
        private DamageCounter EmptyCounter;
        private DamageCounter MainDamageCounter;

        public int Qty { get; set; } = 0;
        public int maxPlayers = 2;
        //private bool mapIsBeingGenerated = false;
        //private bool mapGenerated = false;
        //private bool roomIsFull = false;

        public GameController(GameContext game, PlayerContext context, ObstacleContext obstacle, WeaponContext weapon)
        {
            Counter1 = new Counter(3, 50);
            Counter2 = new Counter(6, 30);
            Counter3 = new Counter(9, 25);
            Counter4 = new Counter(12, 20);
            EmptyCounter = new EmptyCounter();

            Counter1.SetNextChain(Counter2);
            Counter2.SetNextChain(Counter3);
            Counter3.SetNextChain(Counter4);
            Counter4.SetNextChain(EmptyCounter);
            EmptyCounter.SetNextChain(null);

            MainDamageCounter = Counter1;


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

        // PUT api/game/shoot
        [HttpPut("shoot")]
        public ActionResult<Object> ShootMethod([FromBody] List<int> parameters)
        {
            int x = parameters[0];
            int y = parameters[1];
            int px = parameters[2];
            int py = parameters[3];
            long shooterId = (long)parameters[4];

            var information = new
            {
                x = 0,
                y = 0,
                damage = 0
            };

            List<int> posx = new List<int>();
            List<int> posy = new List<int>();

            int ydiff = y - py;
            int xdiff = x - px;
            double slope = (double)(y - py) / (x - px);
            double xx, yy;
            int number = (int)System.Math.Sqrt(ydiff * ydiff + xdiff * xdiff);
            for (double i = 0; i < number; i++)
            {
                yy = slope == 0 ? 0 : ydiff * (i / number);
                xx = slope == 0 ? xdiff * (i / number) : yy / slope;
                posx.Add(((int)System.Math.Round(xx) + px));
                posy.Add(((int)System.Math.Round(yy) + py));
            }

            posx.Add(x);
            posy.Add(y);

            MainDamageCounter.EmptyDamageSum();

            for (int i = 0; i < posx.Count; i++)
            {
                foreach (var player in _playerContext.Players)
                {
                    if (player.PosX - 12 < posx[i] && posx[i] < player.PosX + 12 && player.Id != shooterId)
                    {
                        if (player.PosY - 12 < posy[i] && posy[i] < player.PosY + 12)
                        {
                            //MainDamageCounter.DamageSum = 0;
                            double distance = GetDistanceLength(posx[i], posy[i], (int)player.PosX, (int)player.PosY);
                            MainDamageCounter.Calculate(distance);
                            player.Health -= (int)MainDamageCounter.GetDamageSum();

                            if (player.Health <= 0)
                            {
                                foreach (Player contextplayer in _playerContext.Players)
                                {
                                    contextplayer.ChangedStatus = true;
                                    _playerContext.Players.Update(contextplayer);
                                }
                            }

                            var information1 = new
                            {
                                x = player.PosX,
                                y = player.PosY,
                                damage = (int)MainDamageCounter.GetDamageSum()
                            };
                            
                            _playerContext.Players.Update(player);
                            _playerContext.SaveChanges();
                            return information1;
                            //return Ok();
                            //return (int)MainDamageCounter.GetDamageSum();
                        }
                    }
                }

                i++;
            }
            return information;
            //return Ok();
            //return (int)MainDamageCounter.GetDamageSum();
        }

        [HttpGet("reset")]
        public ActionResult<bool> ResetGame()
        {
            foreach (var player in _playerContext.Players)
            {
                _playerContext.Players.Remove(player);
            }
            _playerContext.SaveChanges();
            
            return true;
        }

        public bool AllPlayersReady()
        {
            foreach (Player player in _playerContext.Players)
            {
                if (!player.IsReady)
                {
                    return false;
                }
            }
            return true;
        }

        public double GetDistanceLength(int x, int y, int px, int py)
        {
            double D = Math.Sqrt(Math.Pow(px - x, 2) + Math.Pow(py - y, 2));
            return D;
        }

    }
}




