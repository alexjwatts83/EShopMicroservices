namespace Ordering.Application.Extensions;

internal static class PaymentDtoExtensions
{
    internal static Payment FromDto(this PaymentDto dto)
        => Payment.Of(dto.CardName, dto.CardNumber, dto.Expiration, dto.Cvv, dto.PaymentMethod);
}