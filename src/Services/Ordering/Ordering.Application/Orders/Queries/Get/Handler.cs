using Infrastructure.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Ordering.Application.Orders.Queries.Get;
public class Handler(IApplicationDbContext dbContext)
    : IQueryHandler<Query, Result>
{
    public async Task<Result> Handle(Query query, CancellationToken cancellationToken)
    {
        // get orders with pagination
        // return result

        var pageNumber = query.PaginationRequest.PageNumber <= 0 
            ? 1 
            : query.PaginationRequest.PageNumber;
        var pageSize = query.PaginationRequest.PageSize;

        var totalCount = await dbContext.Orders.LongCountAsync(cancellationToken);

        var orders = await dbContext.Orders
                       .Include(o => o.OrderItems)
                       .OrderBy(o => o.OrderName.Value)
                       .Skip(pageSize * (pageNumber - 1))
                       .Take(pageSize)
                       .ToListAsync(cancellationToken);

        return new Result(
            new PaginatedResult<OrderDto>(
                pageNumber,
                pageSize,
                totalCount,
                orders.ToOrderDtoList()));
    }
}
