using AutoMapper;
using Bike360.Application.Features.Courses.Commands.CreateCourse;
using Bike360.Application.Features.Courses.Commands.UpdateCourse;
using Bike360.Application.Features.Courses.Queries.GetAllCourseParticipants;
using Bike360.Application.Features.Courses.Queries.GetAllCourses;
using Bike360.Application.Features.Courses.Queries.GetCourseDetails;
using Bike360.Domain.DivingSchool;

namespace Bike360.Application.MappingProfiles;

public class CourseProfile : Profile
{
    public CourseProfile()
    {
        CreateMap<DivingCourse, CourseDto>();
        CreateMap<DivingCourse, CourseDetailsDto>();
        CreateMap<CreateCourseCommand, DivingCourse>();
        CreateMap<UpdateCourseCommand, DivingCourse>();
        CreateMap<DivingSchoolCustomer, CourseParticipantDto>();
    }
}
