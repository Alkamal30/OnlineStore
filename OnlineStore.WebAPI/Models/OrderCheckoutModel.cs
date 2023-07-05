namespace OnlineStore.WebAPI.Models;

public class OrderCheckoutModel {
	public DateTime FormationDate { get; set; }
	public ICollection<OrderItemCheckoutModel> OrderItems { get; set; } = new List<OrderItemCheckoutModel>();
}
