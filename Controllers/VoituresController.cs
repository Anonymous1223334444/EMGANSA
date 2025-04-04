using EMGANSA.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EMGANSA.Controllers
{
    public class VoituresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VoituresController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var voitures = await _context.Voitures
                .Include(v => v.Modele)
                .ThenInclude(m => m.Marque)
                .Include(v => v.Photos.Where(p => p.EstPrincipale))
                .ToListAsync();

            return View(voitures);
        }
    }
}