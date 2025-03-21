using System.Security.Claims;
using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;

namespace Dima.Api.Endpoints.Transactions;

public class DeleteTransactionEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandlerAsync)
            .WithName("Transaction Delete")
            .WithDescription("Deletar uma Transação")
            .WithSummary("Deletar uma Transação.")
            .WithOrder(2)
            .Produces<Response<Transaction?>>();

    private static async Task<IResult> HandlerAsync(ClaimsPrincipal user,ITransactionHandler handler,long id)
    {
        var request = new DeleteTransactionRequest()
        {
            UserId = user.Identity?.Name ?? string.Empty,
            Id = id
        };
        var result = await handler.DeleteAsync(request);
        return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(new { message = result});
    }
}