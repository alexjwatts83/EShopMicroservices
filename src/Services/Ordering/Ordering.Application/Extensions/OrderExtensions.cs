namespace Ordering.Application.Extensions;

internal static class OrderExtensions
{
    // TODO consider returning the Order object so that its a pure function
    internal static void UpdateFromDto(this Order order, OrderDto orderDto)
    {
        var updatedShippingAddress = orderDto.ShippingAddress.FromDto();
        var updatedBillingAddress = orderDto.BillingAddress.FromDto();
        var updatedPayment = orderDto.Payment.FromDto();

        order.Update(
            orderName: OrderName.Of(orderDto.OrderName),
            shippingAddress: updatedShippingAddress,
            billingAddress: updatedBillingAddress,
            payment: updatedPayment,
            status: orderDto.Status
        );
    }
}