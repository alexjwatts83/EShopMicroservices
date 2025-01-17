namespace Catalog.API.Products.GetByCategory;

public record GetByCategoryRequest(int? PageNumber = 1, int? PageSize = 10);
public record GetByCategoryResponse(IEnumerable<Product> Products);

public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}",
            async (string category, [AsParameters] GetByCategoryRequest request, ISender sender) =>
            {
                var query = new GetByCategoryQuery(category)
                {
                    PageSize = request.PageSize,
                    PageNumber = request.PageNumber
                };
                var result = await sender.Send(query);

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
