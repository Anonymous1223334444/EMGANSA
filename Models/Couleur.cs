using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EMGANSA.Models
{
    public class Couleur
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom de la couleur est obligatoire")]
        [StringLength(30, ErrorMessage = "Le nom ne peut pas dépasser 30 caractères")]
        public string Nom { get; set; }

        [StringLength(10)]
        public string? CodeHex { get; set; }

        // Navigation property
        public virtual ICollection<Voiture> Voitures { get; set; } = new List<Voiture>();
    }
}