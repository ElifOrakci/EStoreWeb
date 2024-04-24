using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace QuasarShopData;

public class AppDbContext : IdentityDbContext<User, Role, Guid>
{
    public AppDbContext (DbContextOptions options)
        : base(options)
    { 

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }

      
    public virtual DbSet<CarouselImage > CarouselImages { get; set;}
    public virtual DbSet<Catalog> Catalogs { get; set;}
    public virtual DbSet<ProductImage> ProductImages { get; set;}
    public virtual DbSet<Comment> Comments { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<OrderDetail> OrderDetails { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    public virtual DbSet<UserAddress> UserAddresses { get; set; }
    public virtual DbSet<Favorite> Favorites { get; set; }

}
