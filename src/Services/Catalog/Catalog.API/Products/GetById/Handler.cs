
namespace Catalog.API.Products.GetById;

public record GetByIdQuery(Guid Id) : IQuery<GetByIdResult>;
public record GetByIdResult(Product Product);

public class GetByIdHandler(IDocumentSession session, ILogger<GetByIdHandler> logger) 
    : IQueryHandler<GetByIdQuery, GetByIdResult>
{
    public async Task<GetByIdResult> Handle(GetByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("Products.GetByIdHandler called with {@Query}", query);

        var product = await session.LoadAsync<Product>(query.Id, cancellationToken);

        return product is null 
            ? throw new ProductNotFoundException(query.Id) 
            : new GetByIdResult(product);
    }
}
