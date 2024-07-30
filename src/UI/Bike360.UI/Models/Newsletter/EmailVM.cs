using System.ComponentModel.DataAnnotations;

namespace Bike360.UI.Models.Newsletter;

public class EmailVM
{
    [Required]
    public List<string> ReceiversAddresses { get; set; }

    [Required]
    public string Subject { get; set; }

    [Required]
    public string EmailContent { get; set; }
}
