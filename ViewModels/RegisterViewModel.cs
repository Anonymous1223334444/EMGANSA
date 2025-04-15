using System.ComponentModel.DataAnnotations;

namespace EMGANSA.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "L'adresse email est obligatoire")]
        [EmailAddress(ErrorMessage = "Adresse email invalide")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Le nom est obligatoire")]
        [Display(Name = "Nom")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Le prénom est obligatoire")]
        [Display(Name = "Prénom")]
        public string Prenom { get; set; }

        [Required(ErrorMessage = "Le mot de passe est obligatoire")]
        [StringLength(100, ErrorMessage = "Le {0} doit comporter au moins {2} caractères.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmer le mot de passe")]
        [Compare("Password", ErrorMessage = "Le mot de passe et sa confirmation ne correspondent pas.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Le rôle est obligatoire")]
        [Display(Name = "Rôle")]
        public string Role { get; set; }
    }
}