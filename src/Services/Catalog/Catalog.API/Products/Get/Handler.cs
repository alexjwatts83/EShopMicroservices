using Marten.Pagination;

namespace Catalog.API.Products.Get;

public record GetQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetResult>;
public record GetResult(IEnumerable<Product> Products);

internal class GetHandler(IDocumentSession session) : IQueryHandler<GetQuery, GetResult>
{
    public async Task<GetResult> Handle(GetQuery query, CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>()
            .ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 10, cancellationToken);

        return new GetResult(products);
    }
}