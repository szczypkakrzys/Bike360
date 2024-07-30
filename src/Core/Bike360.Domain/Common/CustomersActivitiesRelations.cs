namespace Bike360.Domain.Common;

public class CustomersActivitiesRelations : BaseEntity
{
    public DateOnly EnrollmentDate { get; set; }
    public DateOnly? AdvancedPaymentDate { get; set; }
    public DateOnly? EntireAmountPaymentDate { get; set; }
    public double AdvancedPaymentLeftToPay { get; set; }
    public double EntireCostLeftToPay { get; set; }
}
