namespace OnlineStore.Infrastructure.Models;

public class Order {
	public int Id { get; set; }

	public int CustomerId { get; set; }
	public User Customer { get; set; } = null!;

	public DateTime FormationDate { get; set; }

	public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
