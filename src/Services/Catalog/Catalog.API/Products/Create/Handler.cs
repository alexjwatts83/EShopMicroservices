using FluentValidation;

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

internal class CommandHandler(IDocumentSession session, IValidator<CreateCommand> validator) : ICommandHandler<CreateCommand, CreateResult>
{
    public async Task<CreateResult> Handle(CreateCommand command, CancellationToken cancellationToken)
    {
        var result = await validator.ValidateAsync(command, cancellationToken);
        if (result.Errors.Count != 0)
        {
            throw new ValidationException(result.Errors.Select(x => x.ErrorMessage).ToList().FirstOrDefault());
        }
        // TODO: change to use AutoMapper
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
