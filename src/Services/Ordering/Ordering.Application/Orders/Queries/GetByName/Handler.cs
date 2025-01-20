using Microsoft.EntityFrameworkCore;

namespace Ordering.Application.Orders.Queries.GetByName;
public class Handler(IApplicationDbContext dbContext) : IQueryHandler<Query, Result>
{
    public async Task<Result> Handle(Query query, CancellationToken cancellationToken)
    {
        // get orders by name using dbContext
        // return result
        var orders = await dbContext.Orders
            .Include(o => o.OrderItems)
            .AsNoTracking()
            // TODO consider contains ignore case
            .Where(o => o.OrderName.Value.Contains(query.Name))
            .OrderBy(o => o.OrderName.Value)
            .ToListAsync(cancellationToken);
        
        var orderDtos = orders.ToOrderDtoList();

        return new Result(orderDtos);
    }
}

