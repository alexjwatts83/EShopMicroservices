using GetByCustomer = Ordering.Application.Orders.Queries.GetByCustomer;

namespace Ordering.API.Endpoints;

//- Accepts a customer ID.
//- Uses a GetByCustomer.Query to fetch orders.
//- Returns the list of orders for that customer.

public record GetByCustomerResponse(IEnumerable<OrderDto> Orders);

public class GetByCustomerEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/customer/{customerId}", async (Guid customerId, ISender sender) =>
        {
            var result = await sender.Send(new GetByCustomer.Query(customerId));

            var response = result.Adapt<GetByCustomerResponse>();

            return Results.Ok(response);
        })
        .WithName("GetOrdersByCustomer")
        .Produces<GetByCustomerResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Orders By Customer")
        .WithDescription("Get Orders By Customer");
    }
}
