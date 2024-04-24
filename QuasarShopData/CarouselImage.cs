using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuasarShopData.Infrastructure;

namespace QuasarShopData;

public  class CarouselImage : EntityBase 
{
  
    public string Image { get; set; }
    public DateTime? DateFirst { get; set; }
    public DateTime? DateEnd { get; set;}

    
 }

public class CarouselImageEntityTypeConfiguration : IEntityTypeConfiguration<CarouselImage>
{
    public void Configure(EntityTypeBuilder<CarouselImage> builder)
    {
        builder
            .Property(p => p.Image)
            .IsUnicode(false);
    }
}
