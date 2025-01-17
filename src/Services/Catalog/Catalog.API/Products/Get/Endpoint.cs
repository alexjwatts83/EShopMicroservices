namespace Catalog.API.Products.Get;

//public record GetProductsRequest();
public record GetResponse(IEnumerable<Product> Products);

public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender) =>
        {
            //var query = request.Adapt<GetProductsQuery>();

            var result = await sender.Send(new GetQuery());

            var response = result.Adapt<GetResponse>();

            return Results.Ok(response);
        })
        .WithName("GetProducts")
        .Produces<GetResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products")
        .WithDescription("Get Products");
    }
}
