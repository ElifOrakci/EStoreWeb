using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using QuasarShopData.Infrastructure;

namespace QuasarShopData;

public  class Comment : EntityBase
{
    public Guid ProductId { get; set; }
   
    public string Text { get; set; }
    public DateTime Date { get; set; }
    public int Rate { get; set; }
   

    public virtual  Product? Product { get; set; }

}


public class CommentEntityTypeConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder
              .HasIndex(p => new { p.Date })
              .IsDescending();

        builder
            .Property(p => p.Date)
            .HasColumnType("smalldatetime");
          
      
            
    }
}