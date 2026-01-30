using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");
            builder.HasKey(order => order.OrderId);

            builder.Property(order => order.OrderId)
                   .UseIdentityColumn(1001, 1);

            builder.Property(order => order.DeliveryTo)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(order => order.Notes)
                .IsRequired()
                .HasColumnType("varchar(MAX)");

            builder.Property(order => order.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(order => order.CreateDate)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(order => order.UpdateDate)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.HasOne(order => order.DeliveryTypeNavigator)
                .WithMany(deliveryType => deliveryType.Orders)
                .HasForeignKey(order => order.DeliveryType)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(order => order.OverallStatusNavigation)
                .WithMany(overrallStatus => overrallStatus.Orders)
                .HasForeignKey(order => order.OverallStatus)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}