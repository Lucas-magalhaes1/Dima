using System.Security.Claims;
using Dima.Api.Common.Api;
using Dima.Core;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.Endpoints.Categories;

public class GetAllCategoriesEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandlerAsync)
            .WithName("Categories Get All")
            .WithDescription("Pega Todas as categoria.")
            .WithSummary("Pega Todas as categoria.")
            .WithOrder(5)
            .Produces<PagedResponse<List<Category>?>>();

    private static async Task<IResult> HandlerAsync(ClaimsPrincipal user,ICategoryHandler handler,[FromQuery]int pagenumber = Configuration.PageNumber, [FromQuery]int pagesize = Configuration.PageSize)
    {
        var request = new GetAllCategoriesRequest()
        {
            UserId = user.Identity?.Name ?? string.Empty,
            PageNumber = pagenumber,
            PageSize = pagesize,
        };
        var result = await handler.GetAllAsync(request);
        return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(new { message = result});
    }
}