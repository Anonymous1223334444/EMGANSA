using System.ComponentModel.DataAnnotations;

namespace EMGANSA.Models
{
    public class StatutChangeViewModel
    {
        public int VoitureId { get; set; }
        
        [Required(ErrorMessage = "Le statut est obligatoire")]
        [Display(Name = "Nouveau statut")]
        public StatutVoiture NouveauStatut { get; set; }
        
        [Display(Name = "Commentaire (optionnel)")]
        public string? Commentaire { get; set; }
    }
}