using Bike360.UI.Models.DivingSchool;
using Bike360.UI.Models.Shared;
using Bike360.UI.Services.Base;

namespace Bike360.UI.Contracts;

public interface ICourseService
{
    Task<List<ActivityVM>> GetAllCourses();
    Task<CourseDetailsVM> GetCourseDetails(int id);
    Task<Response<Guid>> CreateCourse(CourseDetailsVM course);
    Task<Response<Guid>> UpdateCourse(int id, CourseDetailsVM course);
    Task<Response<Guid>> DeleteCourse(int id);
    Task<List<ActivityParticipantVM>> GetCourseParticipants(int courseId);
}
