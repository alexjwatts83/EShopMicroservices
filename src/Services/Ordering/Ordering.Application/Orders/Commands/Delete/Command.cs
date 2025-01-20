namespace Ordering.Application.Orders.Commands.Delete;

public record Command(Guid OrderId) : ICommand<Result>;

public record Result(bool IsSuccess);

public class CommandValidator : AbstractValidator<Command>
{
    public CommandValidator()
    {
        RuleFor(x => x.OrderId).NotEmpty().WithMessage("OrderId is required");
    }
}
