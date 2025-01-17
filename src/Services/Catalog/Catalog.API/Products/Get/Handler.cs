
namespace Catalog.API.Products.Get;

public record GetQuery() : IQuery<GetResult>;
public record GetResult(IEnumerable<Product> Products);

internal class GetHandler(IDocumentSession session, ILogger<GetHandler> logger) 
    : IQueryHandler<GetQuery, GetResult>
{
    public async Task<GetResult> Handle(GetQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("Products.QueryHandler called with {@Query}", query);

        var products = await session.Query<Product>().ToListAsync(cancellationToken);

        return new GetResult(products);
    }
}
