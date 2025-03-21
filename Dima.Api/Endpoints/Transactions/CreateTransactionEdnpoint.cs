using System.Security.Claims;
using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;

namespace Dima.Api.Endpoints.Transactions;

public class CreateTransactionEdnpoint : IEndpoint                                      
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandlerAsync)
            .WithName("Trasactions Create")
            .WithDescription("Criar uma nova Transação.")
            .WithSummary("Criar uma nova Transação.")
            .WithOrder(1)
            .Produces<Response<Transaction?>>();
            
    private static async Task<IResult> HandlerAsync(ClaimsPrincipal user,ITransactionHandler handler, CreateTransactionRequest request)
    {
        request.UserId = user.Identity?.Name ?? string.Empty;
        var result = await handler.CreateAsync(request);
        return result.IsSuccess ? TypedResults.Created($"/{result.Data?.Id}", result) : TypedResults.BadRequest(new { message = result});
    }
}