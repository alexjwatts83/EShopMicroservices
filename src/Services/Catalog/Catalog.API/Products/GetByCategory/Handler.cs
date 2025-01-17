
namespace Catalog.API.Products.GetByCategory;

public record GetByCategoryQuery(string Category) : IQuery<GetByCategoryResult>;
public record GetByCategoryResult(IEnumerable<Product> Products);

internal class GetByCategoryHandler(IDocumentSession session)
    : IQueryHandler<GetByCategoryQuery, GetByCategoryResult>
{
    public async Task<GetByCategoryResult> Handle(GetByCategoryQuery query, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>()
            .Where(p => p.Category.Contains(query.Category))
            .ToListAsync(cancellationToken);

        return new GetByCategoryResult(products);
    }
}
