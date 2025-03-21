using System.Security.Claims;
using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;

namespace Dima.Api.Endpoints.Categories;

public class CreateCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandlerAsync)
            .WithName("Categories Create")
            .WithDescription("Criar uma nova categoria.")
            .WithSummary("Criar uma nova categoria.")
            .WithOrder(1)
            .Produces<Response<Category?>>();
            
    private static async Task<IResult> HandlerAsync(ClaimsPrincipal user,ICategoryHandler handler, CreateCategoryRequest request)
    {
        request.UserId = user.Identity?.Name ?? string.Empty;
        var result = await handler.CreateAsync(request);
        return result.IsSuccess ? TypedResults.Created($"/{result.Data?.Id}", result) : TypedResults.BadRequest(new { message = result});
    }
    
}