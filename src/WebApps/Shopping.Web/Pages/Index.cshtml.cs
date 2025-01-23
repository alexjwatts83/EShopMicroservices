namespace Shopping.Web.Pages;

public class IndexModel(ICatalogService catalogService, IBasketService basketService, ILogger<IndexModel> logger) : PageModel
{
    public IEnumerable<ProductModel> ProductList { get; set; } = [];

    public async Task<IActionResult> OnGetAsync()
    {
        logger.LogInformation("Index page visited");

        var result = await catalogService.GetProducts();

        ProductList = result.Products;

        return Page();
    }

    public async Task<IActionResult> OnPostAddToCartAsync(Guid productId)
    {
        logger.LogInformation("Add to cart button clicked");

        var productResponse = await catalogService.GetProduct(productId);

        // TODO get the user name from the http context or from a safeuser service
        var userName = "swn05";
        var basket = await basketService.LoadUserBasket(userName);

        basket.Items.Add(new ShoppingCartItemModel
        {
            ProductId = productId,
            ProductName = productResponse.Product.Name,
            Price = productResponse.Product.Price,
            Quantity = 1,
            Color = "Black"
        });

        await basketService.StoreBasket(new StoreBasketRequest(basket));

        return RedirectToPage("Cart");
    }
}
