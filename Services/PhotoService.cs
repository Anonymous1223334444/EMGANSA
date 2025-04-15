using EMGANSA.Data;
using EMGANSA.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EMGANSA.Services
{
    public class PhotoService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PhotoService(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<List<PhotoVoiture>> EnregistrerPhotosAsync(int voitureId, List<IFormFile> photos)
        {
            var webRootPath = _webHostEnvironment.WebRootPath;
            var uploadsFolder = Path.Combine(webRootPath, "images", "voitures");
            
            // Créer le dossier s'il n'existe pas
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            
            bool estPrincipale = !_context.PhotosVoitures.Any(p => p.VoitureId == voitureId && p.EstPrincipale);
            var photosVoiture = new List<PhotoVoiture>();
            
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
                    photosVoiture.Add(photoVoiture);
                    
                    // Seule la première photo est principale
                    estPrincipale = false;
                }
            }
            
            await _context.SaveChangesAsync();
            return photosVoiture;
        }

        public async Task SupprimerPhotoAsync(int photoId)
        {
            var photo = await _context.PhotosVoitures.FindAsync(photoId);
            if (photo != null)
            {
                // Supprimer le fichier physique
                var webRootPath = _webHostEnvironment.WebRootPath;
                var filePath = Path.Combine(webRootPath, photo.CheminImage.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                
                // Supprimer l'entrée de la base de données
                _context.PhotosVoitures.Remove(photo);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DefinirPhotoPrincipaleAsync(int photoId, int voitureId)
        {
            // Réinitialiser toutes les photos de cette voiture
            var photos = _context.PhotosVoitures.Where(p => p.VoitureId == voitureId);
            foreach (var photo in photos)
            {
                photo.EstPrincipale = false;
            }
            
            // Définir la nouvelle photo principale
            var photoPrincipale = await _context.PhotosVoitures.FindAsync(photoId);
            if (photoPrincipale != null)
            {
                photoPrincipale.EstPrincipale = true;
            }
            
            await _context.SaveChangesAsync();
        }
    }
}