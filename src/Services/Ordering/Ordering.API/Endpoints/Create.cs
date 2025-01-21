using Create = Ordering.Application.Orders.Commands.Create;

namespace Ordering.API.Endpoints;

//- Accepts a Request object.
//- Maps the request to a Create.Command.
//- Uses MediatR to send the command to the corresponding handler.
//- Returns a response with the created order's ID.

public record CreateRequest(OrderDto Order);
public record CreateResponse(Guid Id);

public class CreateEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/orders", async (CreateRequest request, ISender sender) =>
        {
            var command = request.Adapt<Create.Command>();

            var result = await sender.Send(command);

            var response = result.Adapt<CreateResponse>();

            return Results.Created($"/orders/{response.Id}", response);
        })
        .WithName("CreateOrder")
        .Produces<CreateResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Order")
        .WithDescription("Create Order");
    }
}
