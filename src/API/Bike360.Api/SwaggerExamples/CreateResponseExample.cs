using Swashbuckle.AspNetCore.Filters;

namespace Bike360.Api.SwaggerExamples;

public class CreateResponseExample : IExamplesProvider<CreateResponse>
{
    public CreateResponse GetExamples()
    {
        return new CreateResponse { Id = 1 };
    }
}

public class CreateResponse
{
    public int Id { get; set; }
}