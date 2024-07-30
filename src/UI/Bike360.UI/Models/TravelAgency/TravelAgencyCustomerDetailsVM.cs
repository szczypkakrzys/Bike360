using Bike360.UI.Models.Shared;

namespace Bike360.UI.Models.TravelAgency;

public class TravelAgencyCustomerDetailsVM : CustomerVM
{
    public int Id { get; set; }
    public AddressVM Address { get; set; }
}
