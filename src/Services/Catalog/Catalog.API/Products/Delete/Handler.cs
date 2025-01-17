namespace Catalog.API.Products.Delete;

public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
public record DeleteProductResult(bool IsSuccess);

internal class DeleteHandler(IDocumentSession session, ILogger<DeleteHandler> logger)
    : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Products.DeleteHandler called with {@Command}", command);

        // TODO: Update later to get the product and figure out if it exists or not and then
        // throw a ProductNotFoundExceltion if not found

        session.Delete<Product>(command.Id);

        await session.SaveChangesAsync(cancellationToken);

        return new DeleteProductResult(true);
    }
}
