namespace Basket.API.Basket.Delete;

public record Command(string UserName) : ICommand<Result>;
public record Result(bool IsSuccess);

public class Validator : AbstractValidator<Command>
{
    public Validator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required");
    }
}

public class CommandHandler(IBasketRepository repository) : ICommandHandler<Command, Result>
{
    public async Task<Result> Handle(Command command, CancellationToken cancellationToken)
    {
        // TODO: delete basket from cache
        await repository.DeleteBasket(command.UserName, cancellationToken);

        return new Result(true);
    }
}