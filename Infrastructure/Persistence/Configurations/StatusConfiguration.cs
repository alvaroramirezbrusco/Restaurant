using Domain.Constants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class StatusConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.ToTable("Status");
            builder.HasKey(status => status.Id);

            builder.Property(status => status.Name)
                .IsRequired()
                .HasMaxLength(StatusContraints.NameMaxLength);

            builder.HasData(
                new Status { Id = StatusIds.Pending, Name = StatusNames.Pending },
                new Status { Id = StatusIds.InProgress, Name = StatusNames.InProgress },
                new Status { Id = StatusIds.Ready, Name = StatusNames.Ready },
                new Status { Id = StatusIds.Delivery, Name = StatusNames.Delivery },
                new Status { Id = StatusIds.Closed, Name = StatusNames.Closed }
            );
        }
    }
}