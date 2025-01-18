namespace Basket.API.Basket.Store;

public record Command(ShoppingCart Cart) : ICommand<Result>;
public record Result(string UserName);

public class Validator : AbstractValidator<Command>
{
    public Validator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Cart can not be null");
        RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("UserName is required");
    }
}

public class Handler(IBasketRepository repository) : ICommandHandler<Command, Result>
{
    public async Task<Result> Handle(Command command, CancellationToken cancellationToken)
    {
        var cart = command.Cart;

        await repository.StoreBasket(cart, cancellationToken);

        // TODO: update cache

        return new Result(cart.UserName);
    }
}
