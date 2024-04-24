using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QuasarShopData;

public  class Favorite
{
public Guid  UserId { get; set; }
public Guid ProductId { get; set; }

public User? User { get; set; }
public Product? Product { get; set; }

}
public class FavoriteEntityTypeConfiguration : IEntityTypeConfiguration<Favorite>
{
    public void Configure(EntityTypeBuilder<Favorite> builder)
    {
       builder 
            .HasKey(p => new {p.UserId, p.ProductId });
    }
}
