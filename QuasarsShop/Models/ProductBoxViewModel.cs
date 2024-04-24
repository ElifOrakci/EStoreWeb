namespace QuasarShop.Models;

public class ProductBoxViewModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }
    public string Image { get; set; }
    public decimal  Price { get; set; }   
    public int DiscountRate { get; set; }
    public decimal DiscountedPrice { get; set; }
    public bool IsInFavorites { get; set; }
}
