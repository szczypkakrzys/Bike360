using System.ComponentModel.DataAnnotations;

namespace Bike360.UI.Models.Shared;

public class AddressVM
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Kraj jest wymagany")]
    public string Country { get; set; }
    [Required(ErrorMessage = "Województwo jest wymagane")]
    public string Voivodeship { get; set; }
    [Required(ErrorMessage = "Kod pocztowy jest wymagany")]
    public string PostalCode { get; set; }
    [Required(ErrorMessage = "Miasto jest wymagane")]
    public string City { get; set; }
    [Required(ErrorMessage = "Ulica jest wymagana")]
    public string Street { get; set; }
    [Required(ErrorMessage = "Numer budynku/mieszkania jest wymagany")]
    public string HouseNumber { get; set; }
}
