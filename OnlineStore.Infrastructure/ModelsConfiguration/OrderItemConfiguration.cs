using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.Infrastructure.Models;

namespace OnlineStore.Infrastructure.ModelsConfiguration;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem> {
	public void Configure(EntityTypeBuilder<OrderItem> builder) {
		builder.HasData(
			new OrderItem {
				Id = 1,
				OrderId = 1,
				ProductId = 1,
				Count = 1,
			}	
		);
	}
}
