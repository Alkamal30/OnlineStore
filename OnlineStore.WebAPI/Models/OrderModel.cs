namespace OnlineStore.WebAPI.Models;

public class OrderModel {
	public int Id { get; set; }
	public UserOrderModel Customer { get; set; } = null!;
	public DateTime FormationDate { get; set; }
	public ICollection<OrderItemModel> OrderItems { get; set; } = new List<OrderItemModel>();
}
