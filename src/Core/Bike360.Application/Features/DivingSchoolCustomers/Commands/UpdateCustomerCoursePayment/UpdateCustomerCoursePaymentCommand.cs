using MediatR;

namespace Bike360.Application.Features.DivingSchoolCustomers.Commands.UpdateCustomerCoursePayment;

public class UpdateCustomerCoursePaymentCommand : IRequest<Unit>
{
    public int CustomerId { get; set; }
    public int CourseId { get; set; }
    public double PaymentAmount { get; set; }
}
