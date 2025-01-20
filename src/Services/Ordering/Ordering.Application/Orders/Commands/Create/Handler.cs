namespace Ordering.Application.Orders.Commands.Create;

public class Handler(IApplicationDbContext dbContext) : ICommandHandler<Command, Result>
{
    public async Task<Result> Handle(Command command, CancellationToken cancellationToken)
    {
        var order = command.Order.FromDto();

        dbContext.Orders.Add(order);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new Result(order.Id.Value);
    }
}
