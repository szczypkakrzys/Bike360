using AntDesign;
using Bike360.UI.Contracts;
using Bike360.UI.Models;
using Bike360.UI.Models.DivingSchool;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace Bike360.UI.Pages.Courses;

[Authorize(Policy = Policies.DivingSchool)]
public partial class Details
{
    [Inject]
    public NavigationManager NavManager { get; set; }

    [Inject]
    ICourseService Course { get; set; }

    [Inject]
    IMessageService _message { get; set; }

    [Parameter]
    public int Id { get; set; }

    CourseDetailsVM course = new();
    public string Message { get; set; } = string.Empty;

    protected override async Task OnParametersSetAsync()
    {
        course = await Course.GetCourseDetails(Id);
    }
    protected void EditCourse(int id)
    {
        NavManager.NavigateTo($"/divingschool/courses/edit/{id}");
    }

    protected async void DeleteCourse(int id)
    {
        var response = await Course.DeleteCourse(id);
        if (response.IsSuccess)
        {
            NavManager.NavigateTo("/divingschool/courses/");
            _message.Info("Poprawnie usuniêto kurs");
        }
        else
        {
            Message = response.Message;
            _message.Error("Nie uda³o siê usun¹æ kursu");
        }
    }
}