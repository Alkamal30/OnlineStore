namespace OnlineStore.WebAPI.Models;

public class ProductModel {
	public int Id { get; set; }
	public string Title { get; set; } = "";
	public float Price { get; set; }
	public string Description { get; set; } = "";
	public string Image { get; set; } = "";
}
