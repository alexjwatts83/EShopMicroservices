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

public class CommandHandler : ICommandHandler<Command, Result>
{
    public async Task<Result> Handle(Command command, CancellationToken cancellationToken)
    {
        // TODO: delete basket from database and cache       

        return new Result(true);
    }
}