namespace Ordering.Application.Extensions;

internal static class AddressDtoExtensions
{
    internal static Address FromDto(this AddressDto dto) 
        => Address.Of(dto.FirstName, dto.LastName, dto.EmailAddress, dto.AddressLine, dto.Country, dto.State, dto.ZipCode);
}
