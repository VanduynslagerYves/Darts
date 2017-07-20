using System.ComponentModel.DataAnnotations;

namespace Darts.Models.ViewModels.AccountViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name ="Gebruikersnaam")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Wachtwoord")]
        public string Password { get; set; }

        [Display(Name = "Onthouden?")]
        public bool RememberMe { get; set; }
    }
}
