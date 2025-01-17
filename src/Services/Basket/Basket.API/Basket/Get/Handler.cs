namespace Basket.API.Basket.Get;

public record Query(string UserName) : IQuery<Result>;
public record Result(ShoppingCart Cart);

public class Handler : IQueryHandler<Query, Result>
{
    public async Task<Result> Handle(Query query, CancellationToken cancellationToken)
    {
        // TODO: get from db

        return new Result(new ShoppingCart("swn"));
    }
}
