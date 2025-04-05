using System.ComponentModel.DataAnnotations;

namespace EMGANSA.Models
{
    public class ProfileViewModel
    {
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Nom")]
        public string Nom { get; set; }

        [Display(Name = "Prénom")]
        public string Prenom { get; set; }
    }
}