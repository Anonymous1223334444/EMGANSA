using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EMGANSA.Models
{
    public class Marque
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom de la marque est obligatoire")]
        [StringLength(50, ErrorMessage = "Le nom ne peut pas dépasser 50 caractères")]
        public string Nom { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        // Navigation property
        public virtual ICollection<Modele> Modeles { get; set; } = new List<Modele>();
    }
}