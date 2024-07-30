using MediatR;

namespace Bike360.Application.Features.Courses.Commands.CreateCourse;

public class CreateCourseCommand : IRequest<int>
{
    public string Name { get; set; }
    public DateTime TimeStart { get; set; }
    public DateTime TimeEnd { get; set; }
    public double EntireCost { get; set; }
    public double AdvancePaymentCost { get; set; }
    public DateTime EntireAmountPaymentDeadline { get; set; }
    public DateTime AdvancePaymentDeadline { get; set; }
    public string Status { get; set; }
}
