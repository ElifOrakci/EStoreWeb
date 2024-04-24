using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuasarShopData;

public enum DeliveryStatus : byte
{
    New, OnDelivery, Shipped, 
}
public class Order
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime Date { get; set; }
    public string? CargoTrackingNumber { get; set; }
    public DeliveryStatus Status { get; set; } = DeliveryStatus.New;

    public virtual User? User { get; set; }
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new HashSet<OrderDetail>();


    [NotMapped]
    public decimal GrandTotal => OrderDetails.Sum(p => p.LineTotal);
}


public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder
            .HasIndex(p => new { p.Date })
            .IsDescending();

        builder
            .Property(p => p.Date)
            .HasColumnType("smalldatetime");

        builder
            .HasMany(p => p.OrderDetails)
            .WithOne(p => p.Order)
            .HasForeignKey(p => p.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}