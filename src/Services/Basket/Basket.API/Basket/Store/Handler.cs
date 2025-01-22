using Discount.Grpc;
using DiscounClient = Discount.Grpc.DiscountProtoService.DiscountProtoServiceClient;

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

public class Handler(IBasketRepository repository, DiscounClient discountClient)
    : ICommandHandler<Command, Result>
{
    public async Task<Result> Handle(Command command, CancellationToken cancellationToken)
    {
        var cart = command.Cart;

        await DeductDiscount(discountClient, cart, cancellationToken);

        await repository.StoreBasket(cart, cancellationToken);

        return new Result(cart.UserName);
    }

    private static async Task DeductDiscount(DiscounClient discountClient, ShoppingCart cart, CancellationToken cancellationToken = default)
    {
        // TODO: probably better to return ShoppingCart to make it a pure function

        // Communicate with Discount.Grpc and calculate lastest prices of products into sc
        foreach (var item in cart.Items)
        {
            var request = new GetDiscountRequest { ProductName = item.ProductName };
            var coupon = await discountClient.GetDiscountAsync(request, cancellationToken: cancellationToken);
            item.Price -= coupon.Amount;
        }
    }
}
