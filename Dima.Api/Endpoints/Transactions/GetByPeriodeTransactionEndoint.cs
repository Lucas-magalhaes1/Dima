using Dima.Api.Common.Api;
using Dima.Core;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.Endpoints.Transactions;

public class GetByPeriodeTransactionEndoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandlerAsync)
            .WithName("Transactions Get All")
            .WithDescription("Pega Todas as Transações.")
            .WithSummary("Pega Todas as Transações.")
            .WithOrder(5)
            .Produces<PagedResponse<List<Transaction>?>>();

    private static async Task<IResult> HandlerAsync(ITransactionHandler handler,[FromQuery]DateTime? startDate, [FromQuery] DateTime? endDate,[FromQuery]int pagenumber = Configuration.PageNumber, [FromQuery]int pagesize = Configuration.PageSize)
    {
        var request = new GetTransactionsByPeriodRequest()
        {
            UserId = "test@lucas",
            PageNumber = pagenumber,
            PageSize = pagesize,
            StartDate = startDate,
            EndDate = endDate, 
        };
        var result = await handler.GetByPeriodAsync(request);
        return result.IsSuccess ? TypedResults.Ok(result) : TypedResults.BadRequest(new { message = result});
    }
}