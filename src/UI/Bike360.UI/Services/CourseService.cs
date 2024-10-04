using AutoMapper;
using Blazored.LocalStorage;
using Bike360.UI.Contracts;
using Bike360.UI.Models.DivingSchool;
using Bike360.UI.Models.Shared;
using Bike360.UI.Services.Base;

namespace Bike360.UI.Services;

public class CourseService : BaseHttpService, ICourseService
{
    private readonly IMapper _mapper;

    public CourseService(
        IClient client,
        ILocalStorageService localStorageService,
        IMapper mapper) : base(client, localStorageService)
    {
        _mapper = mapper;
    }

    public async Task<Response<Guid>> CreateCourse(CourseDetailsVM course)
    {
        try
        {
            await AddBearerToken();
            var createCourseCommand = _mapper.Map<CreateCourseCommand>(course);
            await _client.CoursesPOSTAsync(createCourseCommand);
            return new Response<Guid>() { IsSuccess = true };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public async Task<Response<Guid>> DeleteCourse(int id)
    {
        try
        {
            await AddBearerToken();
            await _client.CoursesDELETEAsync(id);
            return new Response<Guid>() { IsSuccess = true };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }

    public async Task<List<ActivityVM>> GetAllCourses()
    {
        await AddBearerToken();
        var tours = await _client.CoursesAllAsync();
        return _mapper.Map<List<ActivityVM>>(tours);
    }

    public async Task<CourseDetailsVM> GetCourseDetails(int id)
    {
        await AddBearerToken();
        var course = await _client.CoursesGETAsync(id);
        return _mapper.Map<CourseDetailsVM>(course);
    }

    public async Task<List<ActivityParticipantVM>> GetCourseParticipants(int courseId)
    {
        await AddBearerToken();
        var participants = await _client.ParticipantsAsync(courseId);
        return _mapper.Map<List<ActivityParticipantVM>>(participants);
    }

    public async Task<Response<Guid>> UpdateCourse(int id, CourseDetailsVM course)
    {
        try
        {
            await AddBearerToken();
            var updateCourseCommand = _mapper.Map<UpdateCourseCommand>(course);
            await _client.CoursesPUTAsync(id.ToString(), updateCourseCommand);
            return new Response<Guid>() { IsSuccess = true };
        }
        catch (ApiException ex)
        {
            return ConvertApiExceptions<Guid>(ex);
        }
    }
}
