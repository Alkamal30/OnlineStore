namespace OnlineStore.WebAPI.Models;

public class OrderPostModel {
	public UserPostOrderModel Customer { get; set; } = null!;
	public DateTime FormationDate { get; set; }
	public ICollection<OrderItemModel> OrderItems { get; set; } = new List<OrderItemModel>();
}