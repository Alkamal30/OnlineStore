namespace OnlineStore.Core.Models;

public class Product {
	public int Id { get; set; }
	public string? Title { get; set; }
	public float Price { get; set; }
	public string? Description { get; set; }
	public string? Image { get; set; }

	public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
