using Bike360.Domain.Common;

namespace Bike360.Domain.DivingSchool;

public class DivingSchoolCustomer : Customer
{
    public string DivingCertificationLevel { get; set; }
    public ICollection<DivingCourse> DivingCourses { get; set; }
    public ICollection<CustomersDivingCoursesRelations> DivingCoursesRelations { get; set; }

}
