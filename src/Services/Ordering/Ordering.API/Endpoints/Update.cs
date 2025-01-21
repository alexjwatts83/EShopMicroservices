using Update = Ordering.Application.Orders.Commands.Update;

namespace Ordering.API.Endpoints;

//- Accepts a UpdateRequest.
//- Maps the request to an Update.Command.
//- Sends the command for processing.
//- Returns a success or error response based on the outcome.

public record UpdateRequest(OrderDto Order);
public record UpdateResponse(bool IsSuccess);

public class UpdateEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/orders", async (UpdateRequest request, ISender sender) =>
        {
            var command = request.Adapt<Update.Command>();

            var result = await sender.Send(command);

            var response = result.Adapt<UpdateResponse>();

            return Results.Ok(response);
        })
        .WithName("UpdateOrder")
        .Produces<UpdateResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update Order")
        .WithDescription("Update Order");
    }
}
