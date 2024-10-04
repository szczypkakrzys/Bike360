using Bike360.Application.Exceptions;
using Swashbuckle.AspNetCore.Filters;

namespace Bike360.Api.SwaggerExamples.Exceptions;

public class NotFoundExceptionResponseExample : IExamplesProvider<NotFoundExceptionResponse>
{
    public NotFoundExceptionResponse GetExamples()
    {
        return new NotFoundExceptionResponse()
        {
            Type = nameof(NotFoundException),
            Title = "Object with ID = {id} was not found.",
            Status = 404,
            Errors = new Dictionary<string, string[]>()
        };
    }
}

public class NotFoundExceptionResponse
{
    public string? Type { get; set; }
    public string? Title { get; set; }
    public int? Status { get; set; }
    public IDictionary<string, string[]>? Errors { get; set; }
}
