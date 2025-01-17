namespace Basket.API.Basket.Store;

public record Request(ShoppingCart Cart);
public record Response(string UserName);

public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket", async (Request request, ISender sender) =>
        {
            var command = request.Adapt<Command>();

            var result = await sender.Send(command);

            var response = result.Adapt<Response>();

            return Results.Created($"/basket/{response.UserName}", response);
        })
        .WithName("StoreBasket")
        .Produces<Response>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Store Basket")
        .WithDescription("Store Basket");
    }
}

