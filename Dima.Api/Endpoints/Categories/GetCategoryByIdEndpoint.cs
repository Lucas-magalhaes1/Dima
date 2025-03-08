using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;

namespace Dima.Api.Endpoints.Categories;

public class GetCategoryByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandlerAsync)
            .WithName("Categories Get by Id")
            .WithDescription("Pega uma categoria.")
            .WithSummary("Pega uma categoria.")
            .WithOrder(4)
            .Produces<Response<Category?>>();

    private static async Task<IResult> HandlerAsync(ICategoryHandler handler,long id)
    {
        var request = new GetCategoryByIdRequest()
        {
            UserId = "test@lucas",
            Id = id
        };
        var result = await handler.GetByIdAsync(request);
        return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(new { message = result});
    }
}
