using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMGANSA.Models
{
    public class Modele
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom du modèle est obligatoire")]
        [StringLength(50, ErrorMessage = "Le nom ne peut pas dépasser 50 caractères")]
        public string Nom { get; set; }

        [ForeignKey("Marque")]
        public int MarqueId { get; set; }
        
        // Navigation properties
        public virtual Marque Marque { get; set; }
        public virtual ICollection<Voiture> Voitures { get; set; } = new List<Voiture>();
    }
}