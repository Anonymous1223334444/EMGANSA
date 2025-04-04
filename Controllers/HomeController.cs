using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EMGANSA.Models;
using EMGANSA.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EMGANSA.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // Récupérer les 3 dernières voitures disponibles
        var voituresRecentes = await _context.Voitures
            .Where(v => v.Statut == StatutVoiture.Disponible)
            .Include(v => v.Modele)
            .ThenInclude(m => m.Marque)
            .Include(v => v.Photos)
            .OrderByDescending(v => v.DateAcquisition)
            .Take(3)
            .ToListAsync();

        return View(voituresRecentes);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}