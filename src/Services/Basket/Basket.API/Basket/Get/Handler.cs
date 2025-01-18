namespace Basket.API.Basket.Get;

public record Query(string UserName) : IQuery<Result>;
public record Result(ShoppingCart Cart);

public class Handler(IBasketRepository repository) : IQueryHandler<Query, Result>
{
    public async Task<Result> Handle(Query query, CancellationToken cancellationToken)
    {
        var basket = await repository.GetBasket(query.UserName);

        return new Result(basket);
    }
}
