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

    internal static OrderDto ToDto(this Order order)
    {
        return new OrderDto(
            Id: order.Id.Value,
            CustomerId: order.CustomerId.Value,
            OrderName: order.OrderName.Value,
            ShippingAddress: order.ShippingAddress.ToDto(),
            BillingAddress: order.BillingAddress.ToDto(),
            Payment: order.Payment.ToDto(),
            Status: order.Status,
            OrderItems: order.OrderItems.Select(item => item.ToDto()).ToList()
        );
    }

    internal static IEnumerable<OrderDto> ToOrderDtoList(this IEnumerable<Order> orders)
    {
        return orders.Select(order => order.ToDto());
    }
}