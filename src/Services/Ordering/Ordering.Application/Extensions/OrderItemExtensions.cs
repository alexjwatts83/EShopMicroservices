namespace Ordering.Application.Extensions;

internal static class OrderItemExtensions
{
    internal static OrderItemDto ToDto(this OrderItem entity)
        => new(entity.OrderId.Value, entity.ProductId.Value, entity.Quantity, entity.Price);
}