using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMGANSA.Models
{
    public class PhotoVoiture
    {
        public int Id { get; set; }

        [ForeignKey("Voiture")]
        public int VoitureId { get; set; }

        [Required(ErrorMessage = "Le chemin de l'image est obligatoire")]
        [StringLength(255)]
        public string CheminImage { get; set; }

        [StringLength(100)]
        public string? Titre { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        public bool EstPrincipale { get; set; } = false;

        // Navigation property
        public virtual Voiture Voiture { get; set; }
    }
}