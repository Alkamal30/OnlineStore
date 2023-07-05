namespace OnlineStore.WebAPI.Models;

public class OrderItemCheckoutModel {
	public ProductModel Product { get; set; } = null!;
	public int Count { get; set; }
}
