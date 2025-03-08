using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;

namespace Dima.Api.Endpoints.Categories;

public class UpdateCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandlerAsync)
            .WithName("Categories Update")
            .WithDescription("Atulizar uma categoria.")
            .WithSummary("Atulizar uma categoria.")
            .WithOrder(2)
            .Produces<Response<Category?>>();

    private static async Task<IResult> HandlerAsync(ICategoryHandler handler, UpdateCategoryRequest request, long id)
    {
        request.UserId = "test@lucas";
        request.Id = id;
        var result = await handler.UpdateAsync(request);
        return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(new { message = result});
    }
}