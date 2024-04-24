using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuasarShopData;

public class ShoppingCartItem

{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }

    public virtual Product? Product { get; set; }
    public virtual User? User { get; set; }

    [NotMapped]
    public decimal LineTotal => Quantity * Product!.DiscountedPrice;
    [NotMapped]
    public decimal BaseTotal => Quantity * Product!.Price;

 }

public class ShoppingCartItemEntityTypeConfiguration : IEntityTypeConfiguration<ShoppingCartItem>
{
    public void Configure(EntityTypeBuilder<ShoppingCartItem> builder)
    {
      
    }
}
