namespace Bike360.Application.Features.DivingSchoolCustomers.Queries.GetCustomerCourseDetails;

public class CustomerCourseDetailsDto
{
    public int Id { get; set; }
    public string EnrollmentDate { get; set; }
    public string? AdvancedPaymentDate { get; set; }
    public string? EntireAmountPaymentDate { get; set; }
    public double AdvancedPaymentLeftToPay { get; set; }
    public double EntireCostLeftToPay { get; set; }
}
