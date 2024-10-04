using AntDesign;
using Bike360.UI.Contracts;
using Bike360.UI.Models;
using Bike360.UI.Models.DivingSchool;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace Bike360.UI.Pages.Courses;

[Authorize(Policy = Policies.DivingSchool)]
public partial class Create
{
    [Inject]
    public NavigationManager NavManager { get; set; }

    [Inject]
    public ICourseService Course { get; set; }

    [Inject]
    IMessageService _message { get; set; }

    public string Message { get; set; } = string.Empty;
    public string ValidationErrors { get; set; }

    public CourseDetailsVM Model { get; set; } = new CourseDetailsVM
    {
        Status = "Oczekuje na rozpoczêcie"
    };

    async Task CreateCourse()
    {
        var response = await Course.CreateCourse(Model);
        if (response.IsSuccess)
        {
            NavManager.NavigateTo("/divingschool/courses/");
            _message.Success("Poprawnie dodano nowy kurs");
        }
        else
        {
            Message = response.Message;
            _message.Error("Nie uda³o siê dodaæ nowego kursu");
        }
    }
}