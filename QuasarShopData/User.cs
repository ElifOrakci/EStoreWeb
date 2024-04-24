using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using QuasarShopData;

namespace QuasarShopData
{
    public class User : IdentityUser<Guid>
    {
        public string Name { get; set; }

    }

    public class Manager : User
    {
        public ICollection<Catalog> Catalogs { get; set; } = new HashSet<Catalog>();
        public ICollection<CarouselImage> CarouselImages { get; set; } = new HashSet<CarouselImage>();
        public ICollection<Product> Products { get; set; } = new HashSet<Product>();


    }

    public class ManagerEntityTypeConfiguration : IEntityTypeConfiguration<Manager>
    {
        public void Configure(EntityTypeBuilder<Manager> builder)
        {
            builder
                .ToTable("Managers");

            builder
                  .HasMany(p => p.CarouselImages)
                  .WithOne(p => (Manager)p.User!)
                  .HasForeignKey(p => p.UserId)
                  .OnDelete(DeleteBehavior.Restrict);
            builder
                  .HasMany(p => p.Catalogs)
                  .WithOne(p => (Manager)p.User!)
                  .HasForeignKey(p => p.UserId)
                  .OnDelete(DeleteBehavior.Restrict);
            builder
              .HasMany(p => p.Products)
              .WithOne(p => (Manager)p.User!)
              .HasForeignKey(p => p.UserId)
              .OnDelete(DeleteBehavior.Restrict);

        }
    }
    public class Customer : User
    {
        public DateTime? DateOfBirth { get; set; }
        public virtual ICollection<UserAddress> Addresses { get; set; } = new HashSet<UserAddress>();
        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        public virtual ICollection<Order> Orders { get; set; } = new HashSet<Order>();
        public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; } = new HashSet<ShoppingCartItem>();
        public ICollection<Favorite> Favorites { get; set; } = new HashSet<Favorite>();


    }
}
public class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder
            .ToTable("Customers");
        builder
             .HasMany(p => p.Addresses)
              .WithOne(p => (Customer)p.User!)
              .HasForeignKey(p => p.UserId)
              .OnDelete(DeleteBehavior.Restrict);
        builder
           .HasMany(p => p.Comments)
            .WithOne(p => (Customer)p.User!)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        builder
          .HasMany(p => p.Orders)
           .WithOne(p => (Customer)p.User!)
           .HasForeignKey(p => p.UserId)
           .OnDelete(DeleteBehavior.Restrict);
        builder
          .HasMany(p => p.ShoppingCartItems)
           .WithOne(p => (Customer)p.User!)
           .HasForeignKey(p => p.UserId)
           .OnDelete(DeleteBehavior.Restrict);
        builder
             .HasMany(p => p.Favorites)
             .WithOne(p => (Customer)p.User!)
             .HasForeignKey(p => p.UserId)
             .OnDelete(DeleteBehavior.Restrict);

    }
}



