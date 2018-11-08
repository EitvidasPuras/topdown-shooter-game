using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameServer.Models;

namespace GameServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObstacleController : ControllerBase
    {
        private readonly ObstacleContext _context;

        public ObstacleController(ObstacleContext context)
        {
            _context = context;
        }

        // GET: api/Obstacle
        [HttpGet]
        public IEnumerable<Obstacle> GetObstacles()
        {
            return _context.Circles.ToList()
                .Cast<Obstacle>().Concat(_context.Rectangles.ToList())
                .ToList();
        }

        [HttpGet("{id}/react")]
        public ActionResult<string> react([FromRoute] int id)
        {
            return _context.Obstacles.Find(id).imp.react();
        }

        //// GET: api/Obstacle/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetObstacle([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var obstacle = await _context.Obstacle.FindAsync(id);

        //    if (obstacle == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(obstacle);
        //}

        //// PUT: api/Obstacle/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutObstacle([FromRoute] int id, [FromBody] Obstacle obstacle)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != obstacle.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(obstacle).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ObstacleExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Obstacle
        //[HttpPost]
        //public async Task<IActionResult> PostObstacle([FromBody] Obstacle obstacle)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.Obstacle.Add(obstacle);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetObstacle", new { id = obstacle.Id }, obstacle);
        //}

        //// DELETE: api/Obstacle/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteObstacle([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var obstacle = await _context.Obstacle.FindAsync(id);
        //    if (obstacle == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Obstacle.Remove(obstacle);
        //    await _context.SaveChangesAsync();

        //    return Ok(obstacle);
        //}

        //private bool ObstacleExists(int id)
        //{
        //    return _context.Obstacle.Any(e => e.Id == id);
        //}
    }
}