
using Marten.Pagination;

namespace Catalog.API.Products.GetByCategory;

public record GetByCategoryQuery(string Category, int? PageNumber = 1, int? PageSize = 10) : IQuery<GetByCategoryResult>;
public record GetByCategoryResult(IEnumerable<Product> Products);

internal class GetByCategoryHandler(IDocumentSession session)
    : IQueryHandler<GetByCategoryQuery, GetByCategoryResult>
{
    public async Task<GetByCategoryResult> Handle(GetByCategoryQuery query, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>()
            .Where(p => p.Category.Contains(query.Category))
            .ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 10, cancellationToken);

        return new GetByCategoryResult(products);
    }
}
