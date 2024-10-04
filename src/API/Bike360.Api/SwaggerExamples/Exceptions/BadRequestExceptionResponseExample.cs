using Bike360.Application.Exceptions;
using Swashbuckle.AspNetCore.Filters;

namespace Bike360.Api.SwaggerExamples.Exceptions;

public class BadRequestExceptionResponseExample : IExamplesProvider<BadRequestExceptionResponse>
{
    public BadRequestExceptionResponse GetExamples()
    {
        var errors = new Dictionary<string, string[]>
        {
            { "Property 1", ["Error 1", "Error 2"] },
            { "Property 2", ["Error 1", "Error 2"] }
        };

        return new BadRequestExceptionResponse()
        {
            Type = nameof(BadRequestException),
            Title = "Invalid Object",
            Status = 400,
            Errors = errors
        };
    }
}

public class BadRequestExceptionResponse
{
    public string? Type { get; set; }
    public string? Title { get; set; }
    public int? Status { get; set; }
    public IDictionary<string, string[]>? Errors { get; set; }
}
