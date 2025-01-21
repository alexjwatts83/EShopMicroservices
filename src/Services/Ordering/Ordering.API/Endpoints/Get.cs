using Infrastructure.Pagination;
using Get = Ordering.Application.Orders.Queries.Get;

namespace Ordering.API.Endpoints;

//- Accepts pagination parameters.
//- Constructs a Get.Query with these parameters.
//- Retrieves the data and returns it in a paginated format.

public record GetResponse(PaginatedResult<OrderDto> Orders);

public class GetEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders", async ([AsParameters] PaginationRequest request, ISender sender) =>
        {
            var result = await sender.Send(new Get.Query(request));

            var response = result.Adapt<GetResponse>();

            return Results.Ok(response);
        })
        .WithName("GetOrders")
        .Produces<GetResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Orders")
        .WithDescription("Get Orders");
    }
}
