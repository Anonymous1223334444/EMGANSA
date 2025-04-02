using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMGANSA.Models
{
    public class Voiture
    {
        public int Id { get; set; }

        [ForeignKey("Modele")]
        public int ModeleId { get; set; }

        [ForeignKey("Couleur")]
        public int CouleurId { get; set; }

        [Required(ErrorMessage = "L'année de fabrication est obligatoire")]
        [Range(2018, 2100, ErrorMessage = "L'année doit être 2018 ou plus récente")]
        public int Annee { get; set; }

        [Required(ErrorMessage = "Le kilométrage est obligatoire")]
        [Range(0, 500000, ErrorMessage = "Le kilométrage doit être entre 0 et 500 000 km")]
        public int Kilometrage { get; set; }

        [Required(ErrorMessage = "Le prix est obligatoire")]
        [Range(0, 10000000, ErrorMessage = "Le prix doit être entre 0 et 10 000 000")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Prix { get; set; }

        [Required(ErrorMessage = "La date d'acquisition est obligatoire")]
        [DataType(DataType.Date)]
        public DateTime DateAcquisition { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Le statut de la voiture est obligatoire")]
        public StatutVoiture Statut { get; set; } = StatutVoiture.Disponible;

        // Navigation properties
        public virtual Modele Modele { get; set; }
        public virtual Couleur Couleur { get; set; }
        public virtual ICollection<PhotoVoiture> Photos { get; set; } = new List<PhotoVoiture>();
    }
}