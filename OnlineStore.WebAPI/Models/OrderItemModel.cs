namespace OnlineStore.WebAPI.Models;

public class OrderItemModel {
	public int Id { get; set; }

	public OrderModel Order { get; set; } = null!;
	public ProductModel Product { get; set; } = null!;

	public int Count { get; set; }
}
