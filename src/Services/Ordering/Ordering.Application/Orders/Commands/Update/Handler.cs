namespace Ordering.Application.Orders.Commands.Update;

public class Handler(IApplicationDbContext dbContext)
    : ICommandHandler<Command, Result>
{
    public async Task<Result> Handle(Command command, CancellationToken cancellationToken)
    {
        //Update Order entity from command object
        //save to database
        //return result

        var orderId = OrderId.Of(command.Order.Id);
        var order = await dbContext.Orders
            .FindAsync([orderId], cancellationToken: cancellationToken);

        if (order is null)
            throw new OrderNotFoundException(command.Order.Id);

        order.UpdateFromDto(command.Order);

        dbContext.Orders.Update(order);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new Result(true);
    }
}
