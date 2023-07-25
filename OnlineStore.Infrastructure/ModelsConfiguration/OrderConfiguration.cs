using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Infrastructure.Models;

namespace OnlineStore.Infrastructure.ModelsConfiguration;

public class OrderConfiguration : IEntityTypeConfiguration<Order> {
	public void Configure(EntityTypeBuilder<Order> builder) {
		builder.HasData(
			new Order {
				Id = 1,
				CustomerId = 2,
				FormationDate = DateTime.UtcNow
			}	
		);
	}
}
