namespace Ordering.Application.Extensions;

internal static class PaymentExtensions
{
    internal static PaymentDto ToDto(this Payment entity)
        => new(entity.CardName!, entity.CardNumber, entity.Expiration, entity.CVV, entity.PaymentMethod);
}