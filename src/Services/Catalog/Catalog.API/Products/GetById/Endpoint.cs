namespace Catalog.API.Products.GetById;

//public record GetProductByIdRequest();
public record GetByIdResponse(Product Product);

public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetByIdQuery(id));

            var response = result.Adapt<GetByIdResponse>();

            return Results.Ok(response);
        })
        .WithName("GetProductById")
        .Produces<GetByIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product By Id")
        .WithDescription("Get Product By Id");
    }
}
