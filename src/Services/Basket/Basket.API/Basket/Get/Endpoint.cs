namespace Basket.API.Basket.Get;

//public record GetRequest(string UserName); 
public record Response(ShoppingCart Cart);

public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{userName}", async (string userName, ISender sender) =>
        {
            var result = await sender.Send(new Query(userName));

            var respose = result.Adapt<Response>();

            return Results.Ok(respose);
        })
        .WithName("GetBasket")
        .Produces<Response>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Basket")
        .WithDescription("Get Basket");
    }
}

