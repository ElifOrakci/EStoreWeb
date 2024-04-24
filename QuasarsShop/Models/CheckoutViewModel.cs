using QuasarShopData;

namespace QuasarShop.Models;

public class CheckoutViewModel
{
    public IEnumerable<ShoppingCartItem> ShoppingCartItems { get; set; }
    public decimal GrandTotal => ShoppingCartItems.Sum(p => p.LineTotal);
    public decimal BaseGrandTotal => ShoppingCartItems.Sum(p => p.BaseTotal);

    public decimal Earning => BaseGrandTotal - GrandTotal;
}
