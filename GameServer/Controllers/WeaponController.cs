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
    public class WeaponController : ControllerBase
    {
        private readonly WeaponContext _context;

        private readonly PlayerContext _playerContext;

        public WeaponController(WeaponContext context, PlayerContext playerContext)
        {
            _context = context;
            _playerContext = playerContext;
        }

        // GET: api/Weapon
        [HttpGet]
        public ActionResult<IEnumerable<Weapon>> GetWeapons()
        {
            return _context.Weapons.ToList();
            //return _context.M4A1.ToList()
            //    .Cast<DesertEagle>().Concat(_context.DesertEagle.ToList())
            //    .Cast<AK47>().Concat(_context.AK47.ToList())
            //    .Cast<P250>().Concat(_context.P250.ToList())
            //    .Cast<GrenadeAdapter>().Concat(_context.GrenadeAdapter.ToList())
            //    .ToList();
        }

        //// GET: api/Weapon/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetWeapon([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var weapon = await _context.Weapon.FindAsync(id);

        //    if (weapon == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(weapon);
        //}

        //// PUT: api/Weapon/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutWeapon([FromRoute] int id, [FromBody] Weapon weapon)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != weapon.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(weapon).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!WeaponExists(id))
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

        //// POST: api/Weapon
        //[HttpPost]
        //public async Task<IActionResult> PostWeapon([FromBody] Weapon weapon)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.Weapon.Add(weapon);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetWeapon", new { id = weapon.Id }, weapon);
        //}

        //// DELETE: api/Weapon/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteWeapon([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var weapon = await _context.Weapon.FindAsync(id);
        //    if (weapon == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Weapon.Remove(weapon);
        //    await _context.SaveChangesAsync();

        //    return Ok(weapon);
        //}

        //private bool WeaponExists(int id)
        //{
        //    return _context.Weapon.Any(e => e.Id == id);
        //}

        [HttpPut("picked-up/{id}")]
        public IActionResult Update(int id, [FromBody] long pid)
        {
            var ww = _context.Weapons.Find(id);
            if (ww == null)
            {
                return NotFound();
            }

            if (ww.IsOnTheGround)
            {
                ww.IsOnTheGround = false;

                _context.Weapons.Update(ww);
                _context.SaveChanges();

                var pp = _playerContext.Players.Find(pid);
                if (pp == null)
                {
                    return NotFound();
                }

                if (ww is Primary)
                {
                    if (pp.pickupPrimary((Primary)ww))
                    {
                        _playerContext.Players.Update(pp);
                        _playerContext.SaveChanges();
                        return Ok();
                    }
                }
                else if (ww is Secondary)
                {
                    if (pp.pickupSecondary((Secondary)ww))
                    {
                        _playerContext.Players.Update(pp);
                        _playerContext.SaveChanges();
                        return Ok();
                    }
                }
                else
                {
                    if (pp.pickupGrenade((GrenadeAdapter)ww))
                    {
                        _playerContext.Players.Update(pp);
                        _playerContext.SaveChanges();
                        return Ok();
                    }

                }

                _playerContext.Players.Update(pp);
                _playerContext.SaveChanges();
            }

            return Ok(); //NoContent();
        }
    }
}