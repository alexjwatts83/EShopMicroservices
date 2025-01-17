namespace Catalog.API.Products.Create;

public record CreateRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);

public record CreateResponse(Guid Id);

public class Endpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (CreateRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<CreateResponse>();

            return Results.Created($"/products/{response.Id}", response);
        })
        .WithName("CreateProduct")
        .Produces<CreateResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Creates Product")
        .WithDescription("Creates Product");
    }
}
