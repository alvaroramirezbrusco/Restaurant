using Domain.Constants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");
            builder.HasKey(category => category.Id);

            builder.Property(category => category.Name)
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(category => category.Description)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(category => category.Order)
                .IsRequired()
                .HasColumnName("Order");

            builder.HasData(
                new Category { Id = CategoryIds.Entradas, Name = CategoryNames.Entradas, Description = CategoryDescription.Entradas, Order = CategoryOrders.Entradas },
                new Category { Id = CategoryIds.Ensaladas, Name = CategoryNames.Ensaladas, Description = CategoryDescription.Ensaladas, Order = CategoryOrders.Ensaladas },
                new Category { Id = CategoryIds.Minutas, Name = CategoryNames.Minutas, Description = CategoryDescription.Minutas, Order = CategoryOrders.Minutas },
                new Category { Id = CategoryIds.Pastas, Name = CategoryNames.Pastas, Description = CategoryDescription.Pastas, Order = CategoryOrders.Pastas },
                new Category { Id = CategoryIds.Parrilla, Name = CategoryNames.Parrilla, Description = CategoryDescription.Parrilla, Order = CategoryOrders.Parrilla },
                new Category { Id = CategoryIds.Pizzas, Name = CategoryNames.Pizzas, Description = CategoryDescription.Pizzas, Order = CategoryOrders.Pizzas },
                new Category { Id = CategoryIds.Sandwiches, Name = CategoryNames.Sandwiches, Description = CategoryDescription.Sandwiches, Order = CategoryOrders.Sandwiches },
                new Category { Id = CategoryIds.Bebidas, Name = CategoryNames.Bebidas, Description = CategoryDescription.Bebidas, Order = CategoryOrders.Bebidas },
                new Category { Id = CategoryIds.CervezaArtesanal, Name = CategoryNames.CervezaArtesanal, Description = CategoryDescription.CervezaArtesanal, Order = CategoryOrders.CervezaArtesanal },
                new Category { Id = CategoryIds.Postres, Name = CategoryNames.Postres, Description = CategoryDescription.Postres, Order = CategoryOrders.Postres }
            );
        }
    }
}