namespace Catalog.API.Products.Get;

public record GetRequest(int? PageNumber = 1, int? PageSize = 10);
public record GetResponse(IEnumerable<Product> Products);

public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async ([AsParameters] GetRequest request, ISender sender) =>
        {
            var query = request.Adapt<GetQuery>();

            var result = await sender.Send(query);

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
