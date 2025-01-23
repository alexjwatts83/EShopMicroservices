namespace Shopping.Web.Pages;

public class CartModel(IBasketService basketService, ISafeUserService safeUserService, ILogger<CartModel> logger) : PageModel
{
    public ShoppingCartModel Cart { get; set; } = new ShoppingCartModel();

    public async Task<IActionResult> OnGetAsync()
    {
        Cart = await basketService.LoadUserBasket(safeUserService.GetUserName());

        return Page();
    }

    public async Task<IActionResult> OnPostRemoveToCartAsync(Guid productId)
    {
        logger.LogInformation("Remove to cart button clicked");

        Cart = await basketService.LoadUserBasket(safeUserService.GetUserName());

        Cart.Items.RemoveAll(x => x.ProductId == productId);

        await basketService.StoreBasket(new StoreBasketRequest(Cart));

        return RedirectToPage();
    }
}
