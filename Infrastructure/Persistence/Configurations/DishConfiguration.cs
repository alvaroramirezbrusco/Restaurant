using Domain.Constants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class DishConfiguration : IEntityTypeConfiguration<Dish>
    {
        public void Configure(EntityTypeBuilder<Dish> builder)
        {
            builder.ToTable("Dish");
            builder.HasKey(dish => dish.DishId);

            builder.Property(dish => dish.Name)
                .IsRequired()
                .HasMaxLength(DishConstraints.NameMaxLength);

            builder.HasIndex(dish => dish.Name)
                .IsUnique();

            builder.Property(dish => dish.Description)
                .IsRequired()
                .HasColumnType("varchar(MAX)");

            builder.Property(dish => dish.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(dish => dish.Available)
                .IsRequired();

            builder.Property(dish => dish.ImageUrl)
                .IsRequired()
                .HasColumnType("varchar(MAX)");

            builder.Property(dish => dish.CreateDate)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(dish => dish.UpdateDate)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.HasOne(dish => dish.CategoryNavigator)
                .WithMany(category => category.Dishes)
                .HasForeignKey(category => category.Category)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
