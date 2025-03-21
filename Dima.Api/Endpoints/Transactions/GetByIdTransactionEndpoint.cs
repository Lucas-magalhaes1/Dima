using System.Security.Claims;
using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;

namespace Dima.Api.Endpoints.Transactions;

public class GetByIdTransactionEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandlerAsync)
            .WithName("transaction Get by Id")
            .WithDescription("Pega uma Transação.")
            .WithSummary("Pega uma Transação.")
            .WithOrder(4)
            .Produces<Response<Transaction?>>();

    private static async Task<IResult> HandlerAsync(ClaimsPrincipal user,ITransactionHandler handler,long id)
    {
        var request = new GetTransactionByIdRequest()
        {
            UserId = user.Identity?.Name ?? string.Empty,
            Id = id
        };
        var result = await handler.GetByIdAsync(request);
        return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(new { message = result});
    }
    
}