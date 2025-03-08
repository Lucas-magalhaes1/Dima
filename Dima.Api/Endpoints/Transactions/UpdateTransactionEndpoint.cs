using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;

namespace Dima.Api.Endpoints.Transactions;

public class UpdateTransactionEndpoint : IEndpoint
{ 
    public static void Map(IEndpointRouteBuilder app)
            => app.MapPut("/{id}", HandlerAsync)
                .WithName("Transaction Update")
                .WithDescription("Atulizar uma Transação.")
                .WithSummary("Atulizar uma Transação.")
                .WithOrder(3)
                .Produces<Response<Transaction?>>();

    private static async Task<IResult> HandlerAsync(ITransactionHandler handler, UpdateTransactionRequest request, long id)
    {
        request.UserId = "test@lucas";
        request.Id = id;
        var result = await handler.UpdateAsync(request);
        return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(new { message = result });
    }

}