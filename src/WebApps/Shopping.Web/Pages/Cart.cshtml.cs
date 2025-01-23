namespace Shopping.Web.Pages;

public class CartModel(IBasketService basketService, ILogger<CartModel> logger) : PageModel
{
    // TODO get the user name from the http context or from a safeuser service        
    private readonly string _userName = "swn05";

    public ShoppingCartModel Cart { get; set; } = new ShoppingCartModel();

    public async Task<IActionResult> OnGetAsync()
    {
        

        Cart = await basketService.LoadUserBasket(_userName);

        return Page();
    }

    public async Task<IActionResult> OnPostRemoveToCartAsync(Guid productId)
    {
        logger.LogInformation("Remove to cart button clicked");

        Cart = await basketService.LoadUserBasket(_userName);

        Cart.Items.RemoveAll(x => x.ProductId == productId);

        await basketService.StoreBasket(new StoreBasketRequest(Cart));

        return RedirectToPage();
    }
}
