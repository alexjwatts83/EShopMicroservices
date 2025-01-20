namespace Ordering.Application.Extensions;

internal static class AddressExtensions
{
    internal static AddressDto ToDto(this Address entity)
        => new(entity.FirstName, entity.LastName, entity.EmailAddress!, entity.AddressLine, entity.Country, entity.State, entity.ZipCode);
}
