using System.ComponentModel.DataAnnotations;

namespace Bike360.UI.Models;

public class LoginVM
{
    [Required(ErrorMessage = "Adres e-mail jest wymagany")]
    [EmailAddress(ErrorMessage = "Podany adres e-mail nie jest w poprawnym formacie")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Hasło jest wymagane")]
    public string Password { get; set; } = string.Empty;
}
