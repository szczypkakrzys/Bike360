using Bike360.Domain.Common;

namespace Bike360.Domain.DivingSchool;

public class DivingCourse : CustomerActivity
{
    public ICollection<DivingSchoolCustomer> Participants { get; set; }
    public ICollection<CustomersDivingCoursesRelations> DivingCourseRelations { get; set; }
}
