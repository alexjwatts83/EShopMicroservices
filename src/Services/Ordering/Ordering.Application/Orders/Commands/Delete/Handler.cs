﻿namespace Ordering.Application.Orders.Commands.Delete;

public class Handler(IApplicationDbContext dbContext) : ICommandHandler<Command, Result>
{
    public async Task<Result> Handle(Command command, CancellationToken cancellationToken)
    {
        //Delete Order entity from command object
        //save to database
        //return result

        var orderId = OrderId.Of(command.OrderId);
        var order = await dbContext.Orders
            .FindAsync([orderId], cancellationToken: cancellationToken);

        if (order is null)
            throw new OrderNotFoundException(command.OrderId);

        dbContext.Orders.Remove(order);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new Result(true);
    }
}
