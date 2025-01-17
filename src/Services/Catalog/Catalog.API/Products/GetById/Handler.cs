
namespace Catalog.API.Products.GetById;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
public record GetProductByIdResult(Product Product);

public class GetByIdHandler(IDocumentSession session, ILogger<GetByIdHandler> logger) 
    : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("Products.GetByIdHandler called with {@Query}", query);

        var product = await session.LoadAsync<Product>(query.Id, cancellationToken);

        return product is null 
            ? throw new ProductNotFoundException() 
            : new GetProductByIdResult(product);
    }
}
