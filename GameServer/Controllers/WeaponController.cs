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

        public WeaponController(WeaponContext context)
        {
            _context = context;
        }

        // GET: api/Weapon
        [HttpGet]
        public IEnumerable<Weapon> GetWeapons()
        {
            //return _context.Weapons.ToList();
            return _context.M4A1.ToList()
                .Cast<Weapon>().Concat(_context.DesertEagle.ToList())
                .Cast<Weapon>().Concat(_context.AK47.ToList())
                .Cast<Weapon>().Concat(_context.P250.ToList())
                .Cast<Weapon>().Concat(_context.GrenadeAdapter.ToList())
                .ToList();
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
    }
}