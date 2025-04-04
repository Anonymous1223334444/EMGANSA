using EMGANSA.Data;
using EMGANSA.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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

        // GET: Voitures - Affiche la liste des voitures
        public async Task<IActionResult> Index(string searchString, StatutVoiture? statut)
        {
            var voituresQuery = _context.Voitures
                .Include(v => v.Modele)
                .ThenInclude(m => m.Marque)
                .Include(v => v.Photos.Where(p => p.EstPrincipale))
                .AsQueryable();

            // Filtrer par statut si spécifié
            if (statut.HasValue)
            {
                voituresQuery = voituresQuery.Where(v => v.Statut == statut);
            }

            // Recherche textuelle
            if (!string.IsNullOrEmpty(searchString))
            {
                voituresQuery = voituresQuery.Where(v => 
                    v.Modele.Nom.Contains(searchString) || 
                    v.Modele.Marque.Nom.Contains(searchString) ||
                    v.Description.Contains(searchString));
            }

            // Ordonner par date d'acquisition (les plus récentes d'abord)
            voituresQuery = voituresQuery.OrderByDescending(v => v.DateAcquisition);

            var voitures = await voituresQuery.ToListAsync();
            return View(voitures);
        }

        // GET: Voitures/Details/5 - Affiche les détails d'une voiture
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voiture = await _context.Voitures
                .Include(v => v.Modele)
                .ThenInclude(m => m.Marque)
                .Include(v => v.Photos)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (voiture == null)
            {
                return NotFound();
            }

            return View(voiture);
        }
    }
}