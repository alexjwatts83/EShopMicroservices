using GetByName = Ordering.Application.Orders.Queries.GetByName;

namespace Ordering.API.Endpoints;

//- Accepts a name parameter.
//- Constructs a GetByName.Query.
//- Retrieves and returns matching orders.

public record GetOrdersByNameResponse(IEnumerable<OrderDto> Orders);

public class GetByNameEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/{orderName}", async (string orderName, ISender sender) =>
        {
            var result = await sender.Send(new GetByName.Query(orderName));

            var response = result.Adapt<GetOrdersByNameResponse>();

            return Results.Ok(response);
        })
        .WithName("GetOrdersByName")
        .Produces<GetOrdersByNameResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Orders By Name")
        .WithDescription("Get Orders By Name");
    }
}