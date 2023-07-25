using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Infrastructure.Models;

namespace OnlineStore.Infrastructure.ModelsConfiguration;

public class ProductConfiguration : IEntityTypeConfiguration<Product> {
	public void Configure(EntityTypeBuilder<Product> builder) {
		builder.HasData(
			new Product {
				Id = 1,
				Title = "TestProductTitle",
				Description = "TestProductDescription",
				Price = 6.9f,
				Image = "TestProductImage"
			}
		);
	}
}
