using System.ComponentModel.DataAnnotations;

namespace Bike360.UI.Models.DivingSchool;

public class CourseDetailsVM
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Nazwa wycieczki jest wymagana")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Data rozpoczęcia jest wymagana")]
    public DateTime TimeStart { get; set; }

    [Required(ErrorMessage = "Data zakończenia jest wymagana")]
    public DateTime TimeEnd { get; set; }

    [Required(ErrorMessage = "Całkowiy koszt jest wymagany")]
    public double EntireCost { get; set; }

    [Required(ErrorMessage = "Koszt zaliczki jest wymagany")]
    public double AdvancePaymentCost { get; set; }

    [Required(ErrorMessage = "Termin wpłaty jest wymagany")]
    public DateTime EntireAmountPaymentDeadline { get; set; }

    [Required(ErrorMessage = "Termin wpłaty zaliczki jest wymagany")]
    public DateTime AdvancePaymentDeadline { get; set; }

    [Required(ErrorMessage = "Status jest wymagany")]
    public string Status { get; set; }
}
