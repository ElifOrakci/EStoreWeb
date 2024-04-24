using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using QuasarShopData.Infrastructure;
using System.ComponentModel.DataAnnotations.Schema;
using QuasarShopData.Seed;

namespace QuasarShopData;

public  class Product : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int DiscountRate { get; set; }
    public string? Image { get ; set; }

    public int  Stocks { get; set; }


    public  ICollection<Catalog> Catalogs { get; set; } = new HashSet<Catalog>();   

    public  ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();

    public  ICollection<OrderDetail>OrderDetails { get; set; } = new HashSet<OrderDetail>();
    public  ICollection<ProductImage> ProductImages { get; set; } = new HashSet<ProductImage>();

    public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; } = new HashSet<ShoppingCartItem>();
    public ICollection<Favorite> Favorites { get; set; } = new HashSet<Favorite>();


    [NotMapped]

    public decimal DiscountedPrice => Price - (Price * DiscountRate / 100.0m);

  
}

public class  ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
{
    public ProductSeed ProductSeed => new ProductSeed();
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder
              .HasIndex(p => new { p.Name });


        builder
                .Property(p => p.Price)
                .HasPrecision(18, 4);

        builder 
            .Property(p => p.Image)
            .IsUnicode(false);
        builder
              .HasMany(p => p.Comments)
              .WithOne(p => p.Product)
              .HasForeignKey(p => p.ProductId)
              .OnDelete(DeleteBehavior.Cascade);
        builder
              .HasMany(p => p.OrderDetails)
              .WithOne(p => p.Product)
              .HasForeignKey(p => p.ProductId)
              .OnDelete(DeleteBehavior.Restrict);
        builder
             .HasMany(p => p.ProductImages)
             .WithOne(p => p.Product)
             .HasForeignKey(p => p.ProductId)
             .OnDelete(DeleteBehavior.Cascade);

        builder
             .HasMany(p => p.ShoppingCartItems)
             .WithOne(p => p.Product)
             .HasForeignKey(p => p.ProductId)
             .OnDelete(DeleteBehavior.Cascade);

        builder
             .HasMany(p => p.Favorites)
             .WithOne(p => p.Product)
             .HasForeignKey(p => p.ProductId)
             .OnDelete(DeleteBehavior.Cascade);
    }
}
