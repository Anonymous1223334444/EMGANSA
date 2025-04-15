using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EMGANSA.Data;
using EMGANSA.Models;
using EMGANSA.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EMGANSA.Controllers
{
    public class VoituresController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly PhotoService _photoService;

        public VoituresController(
            ApplicationDbContext context, 
            IWebHostEnvironment webHostEnvironment,
            PhotoService photoService)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _photoService = photoService;
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

        [Authorize(Roles = "Administrateur")]
        public IActionResult Create()
        {
            ViewData["ModeleId"] = new SelectList(_context.Modeles
                .Include(m => m.Marque)
                .OrderBy(m => m.Marque.Nom)
                .ThenBy(m => m.Nom), "Id", "NomComplet");
            
            return View(new VoitureViewModel
            {
                DateAcquisition = DateTime.Today,
                Statut = StatutVoiture.Disponible
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrateur")]
        public async Task<IActionResult> Create(VoitureViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Créer la voiture à partir du ViewModel
                var voiture = new Voiture
                {
                    ModeleId = viewModel.ModeleId,
                    Annee = viewModel.Annee,
                    Kilometrage = viewModel.Kilometrage,
                    Prix = viewModel.Prix,
                    DateAcquisition = viewModel.DateAcquisition,
                    Description = viewModel.Description,
                    Statut = viewModel.Statut
                };
                
                _context.Add(voiture);
                await _context.SaveChangesAsync();
                
                // Traiter les photos si elles existent
                if (viewModel.Photos != null && viewModel.Photos.Count > 0)
                {
                    await TraiterPhotosAsync(voiture.Id, viewModel.Photos);
                }
                
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["ModeleId"] = new SelectList(_context.Modeles
                .Include(m => m.Marque)
                .OrderBy(m => m.Marque.Nom)
                .ThenBy(m => m.Nom), "Id", "NomComplet", viewModel.ModeleId);
            
            return View(viewModel);
        }

        // Méthode auxiliaire pour traiter les photos
        private async Task TraiterPhotosAsync(int voitureId, List<IFormFile> photos)
        {
            var webRootPath = _webHostEnvironment.WebRootPath;
            var uploadsFolder = Path.Combine(webRootPath, "images", "voitures");
            
            // Créer le dossier s'il n'existe pas
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            
            bool estPrincipale = !_context.PhotosVoitures.Any(p => p.VoitureId == voitureId && p.EstPrincipale);
            
            foreach (var photo in photos)
            {
                if (photo.Length > 0)
                {
                    // Générer un nom de fichier unique
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
                    var filePath = Path.Combine(uploadsFolder, fileName);
                    
                    // Enregistrer le fichier
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await photo.CopyToAsync(stream);
                    }
                    
                    // Créer l'entrée dans la base de données
                    var photoVoiture = new PhotoVoiture
                    {
                        VoitureId = voitureId,
                        CheminImage = "/images/voitures/" + fileName,
                        Titre = Path.GetFileNameWithoutExtension(photo.FileName),
                        EstPrincipale = estPrincipale
                    };
                    
                    _context.Add(photoVoiture);
                    
                    // Seule la première photo est principale
                    estPrincipale = false;
                }
            }
            
            await _context.SaveChangesAsync();
        }

        // GET: Voitures/Edit/5
        [Authorize(Roles = "Administrateur")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voiture = await _context.Voitures
                .Include(v => v.Modele)
                .Include(v => v.Photos)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (voiture == null)
            {
                return NotFound();
            }
            
            var viewModel = new VoitureViewModel
            {
                Id = voiture.Id,
                ModeleId = voiture.ModeleId,
                Annee = voiture.Annee,
                Kilometrage = voiture.Kilometrage,
                Prix = voiture.Prix,
                DateAcquisition = voiture.DateAcquisition,
                Description = voiture.Description,
                Statut = voiture.Statut
            };

            ViewData["ModeleId"] = new SelectList(_context.Modeles
                .Include(m => m.Marque)
                .OrderBy(m => m.Marque.Nom)
                .ThenBy(m => m.Nom), "Id", "NomComplet", voiture.ModeleId);
            
            return View(viewModel);
        }

        // POST: Voitures/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrateur")]
        public async Task<IActionResult> Edit(int id, VoitureViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var voiture = await _context.Voitures.FindAsync(id);
                    if (voiture == null)
                    {
                        return NotFound();
                    }
                    
                    // Mettre à jour les propriétés
                    voiture.ModeleId = viewModel.ModeleId;
                    voiture.Annee = viewModel.Annee;
                    voiture.Kilometrage = viewModel.Kilometrage;
                    voiture.Prix = viewModel.Prix;
                    voiture.DateAcquisition = viewModel.DateAcquisition;
                    voiture.Description = viewModel.Description;
                    voiture.Statut = viewModel.Statut;
                    
                    _context.Update(voiture);
                    await _context.SaveChangesAsync();
                    
                    // Traiter les photos si elles existent
                    if (viewModel.Photos != null && viewModel.Photos.Count > 0)
                    {
                        await _photoService.EnregistrerPhotosAsync(voiture.Id, viewModel.Photos);
                    }
                    
                    // Supprimer les photos sélectionnées
                    if (viewModel.PhotosASupprimer != null && viewModel.PhotosASupprimer.Count > 0)
                    {
                        foreach (var photoId in viewModel.PhotosASupprimer)
                        {
                            await _photoService.SupprimerPhotoAsync(photoId);
                        }
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VoitureExists(viewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["ModeleId"] = new SelectList(_context.Modeles
                .Include(m => m.Marque)
                .OrderBy(m => m.Marque.Nom)
                .ThenBy(m => m.Nom), "Id", "NomComplet", viewModel.ModeleId);
            
            return View(viewModel);
        }

        private bool VoitureExists(int id)
        {
            return _context.Voitures.Any(e => e.Id == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrateur")]
        public async Task<IActionResult> SetMainPhoto(int photoId, int voitureId)
        {
            await _photoService.DefinirPhotoPrincipaleAsync(photoId, voitureId);
            return RedirectToAction(nameof(Edit), new { id = voitureId });
        }
    }
}