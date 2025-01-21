using Delete = Ordering.Application.Orders.Commands.Delete;

namespace Ordering.API.Endpoints;

//- Accepts the order ID as a parameter.
//- Constructs a Delete.Command.
//- Sends the command using MediatR.
//- Returns a success or not found response.

public record DeleteResponse(bool IsSuccess);

public class DeleteEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/orders/{id}", async (Guid Id, ISender sender) =>
        {
            var result = await sender.Send(new Delete.Command(Id));

            var response = result.Adapt<DeleteResponse>();

            return Results.Ok(response);
        })
        .WithName("DeleteOrder")
        .Produces<DeleteResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Delete Order")
        .WithDescription("Delete Order");
    }
}
