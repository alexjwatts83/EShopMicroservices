namespace Catalog.API.Products.Update;

public record UpdateCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<UpdateResult>;
public record UpdateResult(bool IsSuccess);

internal class UpdateHandler(IDocumentSession session, ILogger<UpdateHandler> logger) 
    : ICommandHandler<UpdateCommand, UpdateResult>
{
    public async Task<UpdateResult> Handle(UpdateCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Products.UpdateHandler called with {@Command}", command);

        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

        if (product == null)
            throw new ProductNotFoundException(command.Id);

        product.Name = command.Name;
        product.Category = command.Category;
        product.Description = command.Description;
        product.ImageFile = command.ImageFile;
        product.Price = command.Price;

        session.Update(product);

        await session.SaveChangesAsync(cancellationToken);

        return new UpdateResult(true);
    }
}
