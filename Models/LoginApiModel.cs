using System.ComponentModel.DataAnnotations;

namespace EMGANSA.Models
{
    public class LoginApiModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}