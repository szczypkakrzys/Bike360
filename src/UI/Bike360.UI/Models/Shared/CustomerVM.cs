using System.ComponentModel.DataAnnotations;

namespace Bike360.UI.Models.Shared;

public class CustomerVM
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Imię jest wymagane")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Nazwisko jest wymagane")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Adres e-mail jest wymagany")]
    [EmailAddress(ErrorMessage = "Podany adres e-mail nie jest w poprawnym formacie")]
    public string EmailAddress { get; set; } = string.Empty;

    [Required(ErrorMessage = "Numer telefonu jest wymagany")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Data urodzin jest wymagana")]
    public DateTime DateOfBirth { get; set; }
}
