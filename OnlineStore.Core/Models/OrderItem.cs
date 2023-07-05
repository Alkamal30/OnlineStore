﻿namespace OnlineStore.Core.Models;

public class OrderItem {

	public int Id { get; set; }

	public int OrderId { get; set; }
	public Order Order { get; set; } = null!;

	public int ProductId { get; set; }
	public Product Product { get; set; } = null!;

	public int Count { get; set; }
}
