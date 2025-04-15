using EMGANSA.Data;
using EMGANSA.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMGANSA.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoituresApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VoituresApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/VoituresApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Voiture>>> GetVoitures()
        {
            return await _context.Voitures
                .Include(v => v.Modele)
                .ThenInclude(m => m.Marque)
                .Include(v => v.Photos)
                .ToListAsync();
        }

        // GET: api/VoituresApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Voiture>> GetVoiture(int id)
        {
            var voiture = await _context.Voitures
                .Include(v => v.Modele)
                .ThenInclude(m => m.Marque)
                .Include(v => v.Photos)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (voiture == null)
            {
                return NotFound();
            }

            return voiture;
        }

        // PUT: api/VoituresApi/5
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrateur")]
        public async Task<IActionResult> PutVoiture(int id, Voiture voiture)
        {
            if (id != voiture.Id)
            {
                return BadRequest();
            }

            _context.Entry(voiture).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoitureExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/VoituresApi
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrateur")]
        public async Task<ActionResult<Voiture>> PostVoiture(Voiture voiture)
        {
            _context.Voitures.Add(voiture);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVoiture), new { id = voiture.Id }, voiture);
        }

        // PATCH: api/VoituresApi/5/status
        [HttpPatch("{id}/status")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrateur")]
        public async Task<IActionResult> UpdateVoitureStatus(int id, [FromBody] StatutVoiture statut)
        {
            var voiture = await _context.Voitures.FindAsync(id);
            if (voiture == null)
            {
                return NotFound();
            }

            voiture.Statut = statut;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/VoituresApi/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrateur")]
        public async Task<IActionResult> DeleteVoiture(int id)
        {
            var voiture = await _context.Voitures.FindAsync(id);
            if (voiture == null)
            {
                return NotFound();
            }

            _context.Voitures.Remove(voiture);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VoitureExists(int id)
        {
            return _context.Voitures.Any(e => e.Id == id);
        }
    }
}