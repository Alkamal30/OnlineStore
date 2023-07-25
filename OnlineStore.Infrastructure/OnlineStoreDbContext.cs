using Microsoft.EntityFrameworkCore;
using OnlineStore.Infrastructure.Models;
using OnlineStore.Infrastructure.ModelsConfiguration;

namespace OnlineStore.Infrastructure;

public class OnlineStoreDbContext : DbContext {

	public OnlineStoreDbContext(DbContextOptions<OnlineStoreDbContext> options) 
		: base(options) { }


	public DbSet<User> Users { get; set; }
	public DbSet<Product> Products { get; set; }
	public DbSet<OrderItem> OrderItems { get; set; }
	public DbSet<Order> Orders { get; set; }

	public DbSet<UserRequests> UserRequests { get; set; }


	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		modelBuilder.ApplyConfiguration(new UserConfiguration());
		modelBuilder.ApplyConfiguration(new ProductConfiguration());
		modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
		modelBuilder.ApplyConfiguration(new OrderConfiguration());
	}
}