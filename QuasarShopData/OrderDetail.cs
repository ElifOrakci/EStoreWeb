using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuasarShopData;

public  class OrderDetail
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public int DiscountRate { get; set; }
    

    public virtual Order? Order { get; set; }   
    public virtual Product? Product { get; set; }


    [NotMapped]

    public decimal DiscountedPrice => Price - (Price * DiscountRate / 100.0m);


    [NotMapped]
    public decimal LineTotal => DiscountedPrice * Quantity;


}


public class OrderDetailEntityTypeConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {

        builder
            .Property(p => p.Price)
            .HasPrecision(18, 4);


    }
}