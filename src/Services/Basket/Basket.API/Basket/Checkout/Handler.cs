using Basket.API.Basket.Dtos;
using Infrastructure.Messaging.Events;
using MassTransit;
using MassTransit.Transports;

namespace Basket.API.Basket.Checkout;

public record Command(CheckoutDto CheckoutDto) : ICommand<Result>;
public record Result(bool IsSuccess);

public class CommandValidator: AbstractValidator<Command>
{
    public CommandValidator()
    {
        RuleFor(x => x.CheckoutDto).NotNull().WithMessage("BasketCheckoutDto can't be null");
        RuleFor(x => x.CheckoutDto.UserName).NotEmpty().WithMessage("UserName is required");
    }
}

public class Handler(IBasketRepository repository, IPublishEndpoint publishEndpoint) : ICommandHandler<Command, Result>
{
    public async Task<Result> Handle(Command command, CancellationToken cancellationToken)
    {
        // get existing basket with total price
        // Set totalprice on basketcheckout event message
        // send basket checkout event to rabbitmq using masstransit
        // delete the basket

        var basket = await repository.GetBasket(command.CheckoutDto.UserName, cancellationToken);

        // TODO return not found potentially
        if (basket == null)
            return new Result(false);

        var eventMessage = command.CheckoutDto.Adapt<BasketCheckoutEvent>();
        eventMessage.TotalPrice = basket.TotalPrice;

        await publishEndpoint.Publish(eventMessage, cancellationToken);

        await repository.DeleteBasket(command.CheckoutDto.UserName, cancellationToken);

        return new Result(true);
    }
}
