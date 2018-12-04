using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameServer.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GameServer.Controllers
{
    [Route("api/player")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly PlayerContext _context;
   
        private readonly GameContext _context2;
        public int Qty { get; set; } = 0;

        // GET: /<controller>/
        //public IActionResult Index()
        //{
        //    return View("this in index");
        //}

        public PlayerController(PlayerContext context)
        {
            _context = context;
            
            if (_context.Players.Count() == 0)
            {
                // Create a new Player if collection is empty,
                // which means you can't delete all Players.
                for (int i = 0; i < 5; i++)
                {
                    Qty++;
                    Player p = new Player { Name = "Player-" + Qty, Score = 0, PosX = 0, PosY = 0 };
                    _context.Players.Add(p);
                }
            
                _context.SaveChanges();
            }
        }

        [Route("updated/{id}")]
        public ActionResult<bool> GetUpdated(long id)
        {
            Player p = _context.Players.Find(id);
            if (p == null)
            {
                return false;
            }
            else
            {
                if (p.ChangedStatus)
                {
                    p.ChangedStatus = false;
                    _context.Players.Update(p);
                    _context.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        // GET api/player
        [HttpGet]
        public ActionResult<IEnumerable<Player>> GetAll()
        {
            return _context.Players.ToList();
        }

        // GET api/player/5
        [HttpGet("{id}", Name = "GetPlayer")]
        public ActionResult<Player> GetById(long id)
        {
            Player p = _context.Players.Find(id);
            if(p == null){
                return  NotFound("player not found");
            }
            return p;
        }

        // POST api/player
        [HttpPost]
        //public string Create(Player player)
        public ActionResult<Player> Create([FromBody] Player player)
        {
            if(_context.Players.Count() == 0)
            {
                player.IsHost = true;
                player.IsReady = true;
            }
            else
            {
                player.IsHost = false;
                player.IsReady = false;
            }

            _context.Players.Add(player);
            _context.SaveChanges();

            //return Ok(); //"created - ok"; 
            return CreatedAtRoute("GetPlayer", new { id = player.Id }, player);
        }


        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Player p)
        {
            var pp = _context.Players.Find(id);
            if (pp == null)
            {
                return NotFound();
            }

            pp.Name = p.Name;
            pp.PosX = p.PosX;
            pp.PosY = p.PosY;
            pp.Score = p.Score;

            _context.Players.Update(pp);
            _context.SaveChanges();

            return Ok(); //NoContent();
        }

        [HttpPatch]
        public IActionResult PartialUpdate([FromBody] Coordinates request)
        {
            var player = _context.Players.Find(request.Id);
            if (player == null)
            {
                return NotFound();
            }
            else
            {
                player.PosX = request.PosX;
                player.PosY = request.PosY;

                foreach (Player contextplayer in _context.Players)
                {
                    contextplayer.ChangedStatus = true;
                    _context.Players.Update(contextplayer);
                }

                _context.Players.Update(player);
                _context.SaveChanges();
            }
            return Ok();
            //return CreatedAtRoute("GetPlayer", new { id = player.Id }, player);
        }

        [HttpPatch("ready/{id}")]
        public IActionResult UpdateReadyState(long id)
        {
            var player = _context.Players.Find(id);
            var players = _context.Players;
            if (player == null)
            {
                return NotFound();
            }
            else
            {
                player.IsReady = !player.IsReady;

                _context.Players.Update(player);
                _context.SaveChanges();
            }

            return Ok();
            //return CreatedAtRoute("GetPlayer", new { id = player.Id }, player);
        }

        // DELETE api/values/5
        /*[HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.Players.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Players.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }

    }
}




