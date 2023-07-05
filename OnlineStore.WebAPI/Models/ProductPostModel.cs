namespace OnlineStore.WebAPI.Models;

public class ProductPostModel {
	public string Title { get; set; } = "";
	public float Price { get; set; }
	public string Description { get; set; } = "";
	public string Image { get; set; } = "";
}
