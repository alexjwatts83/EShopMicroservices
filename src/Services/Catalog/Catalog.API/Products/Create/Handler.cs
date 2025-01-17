namespace Catalog.API.Products.Create;

public record CreateCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<CreateResult>;

public record CreateResult(Guid Id);

public class CreateValidator : AbstractValidator<CreateCommand>
{
    public CreateValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
        RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}

internal class CreateHandler(IDocumentSession session) : ICommandHandler<CreateCommand, CreateResult>
{
    public async Task<CreateResult> Handle(CreateCommand command, CancellationToken cancellationToken)
    {
        // TODO: change to use AutoMapper/Mapster
        var product = new Product
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price
        };

        session.Store(product);

        await session.SaveChangesAsync(cancellationToken);

        return new CreateResult(product.Id);
    }
}
