using FluentValidation;

namespace Catalog.API.Products.Delete;

public record DeleteCommand(Guid Id) : ICommand<DeleteResult>;
public record DeleteResult(bool IsSuccess);

public class DeleteValidator : AbstractValidator<DeleteCommand>
{
    public DeleteValidator()
    {
        // Test using 00000000-0000-0000-0000-000000000000
        RuleFor(x => x.Id).NotEmpty().WithMessage("Product ID is required");
    }
}
internal class DeleteHandler(IDocumentSession session, ILogger<DeleteHandler> logger)
    : ICommandHandler<DeleteCommand, DeleteResult>
{
    public async Task<DeleteResult> Handle(DeleteCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Products.DeleteHandler called with {@Command}", command);

        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

        if (product == null)
            throw new ProductNotFoundException(command.Id);

        session.Delete(product);

        await session.SaveChangesAsync(cancellationToken);

        return new DeleteResult(true);
    }
}
