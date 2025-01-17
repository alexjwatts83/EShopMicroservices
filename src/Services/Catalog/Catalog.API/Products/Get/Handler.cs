namespace Catalog.API.Products.Get;

public record GetQuery() : IQuery<GetResult>;
public record GetResult(IEnumerable<Product> Products);

internal class GetHandler(IDocumentSession session) : IQueryHandler<GetQuery, GetResult>
{
    public async Task<GetResult> Handle(GetQuery query, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>().ToListAsync(cancellationToken);

        return new GetResult(products);
    }
}
