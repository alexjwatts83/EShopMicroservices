using Basket.API.Basket.Dtos;

namespace Basket.API.Basket.Checkout;

public record Request(CheckoutDto CheckoutDto);
public record Response(bool IsSuccess);

public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket/checkout", async (Request request, ISender sender) =>
        {
            var command = request.Adapt<Command>();

            var result = await sender.Send(command);

            var response = result.Adapt<Response>();

            return Results.Ok(response);
        })
        .WithName("CheckoutBasket")
        .Produces<Response>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Checkout Basket")
        .WithDescription("Checkout Basket");
    }
}
