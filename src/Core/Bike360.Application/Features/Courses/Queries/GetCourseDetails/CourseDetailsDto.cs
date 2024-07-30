namespace Bike360.Application.Features.Courses.Queries.GetCourseDetails;

public class CourseDetailsDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string TimeStart { get; set; }
    public string TimeEnd { get; set; }
    public double EntireCost { get; set; }
    public double AdvancePaymentCost { get; set; }
    public string EntireAmountPaymentDeadline { get; set; }
    public string AdvancePaymentDeadline { get; set; }
    public string Status { get; set; }
}
