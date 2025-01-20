namespace Ordering.Application.Extensions;

internal static class OrderDtoExtensions
{
    internal static Order FromDto(this OrderDto dto)
    {
        var shippingAddress = dto.ShippingAddress.FromDto();
        var billingAddress = dto.BillingAddress.FromDto();
        var payment = dto.Payment.FromDto();

        var newOrder = Order.Create(
            id: OrderId.Of(Guid.NewGuid()),
            customerId: CustomerId.Of(dto.CustomerId),
            orderName: OrderName.Of(dto.OrderName),
            shippingAddress,
            billingAddress,
            payment
        );

        foreach (var item in dto.OrderItems)
        {
            newOrder.AddItem(ProductId.Of(item.ProductId), item.Quantity, item.Price);
        }

        return newOrder;
    }
}
