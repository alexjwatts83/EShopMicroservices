namespace Catalog.API.Products.GetByCategory;

//public record GetProductByCategoryRequest();
public record GetByCategoryResponse(IEnumerable<Product> Products);

public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}",
            async (string category, ISender sender) =>
            {
                var result = await sender.Send(new GetByCategoryQuery(category));

                var response = result.Adapt<GetByCategoryResponse>();

                return Results.Ok(response);
            })
        .WithName("GetProductByCategory")
        .Produces<GetByCategoryResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product By Category")
        .WithDescription("Get Product By Category");
    }
}
