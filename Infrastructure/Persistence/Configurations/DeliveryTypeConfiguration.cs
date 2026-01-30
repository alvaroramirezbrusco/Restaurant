using Domain.Constants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class DeliveryTypeConfiguration : IEntityTypeConfiguration<DeliveryType>
    {
        public void Configure(EntityTypeBuilder<DeliveryType> builder)
        {
            builder.ToTable("DeliveryType");
            builder.HasKey(deliveryType => deliveryType.Id);

            builder.Property(deliveryType => deliveryType.Name)
                .IsRequired()
                .HasMaxLength(25);

            builder.HasData(
                new DeliveryType { Id = DeliveryTypeIds.Delivery, Name = DeliveryTypeNames.Delivery },
                new DeliveryType { Id = DeliveryTypeIds.TakeAway, Name = DeliveryTypeNames.TakeAway },
                new DeliveryType { Id = DeliveryTypeIds.DineIn, Name = DeliveryTypeNames.DineIn }
            );
        }
    }
}