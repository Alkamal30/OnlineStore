namespace OnlineStore.Core.Models;

public class Order {
	public int Id { get; set; }
	public User? Customer { get; set; }
	public DateTime FormationDate { get; set; }
	public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
