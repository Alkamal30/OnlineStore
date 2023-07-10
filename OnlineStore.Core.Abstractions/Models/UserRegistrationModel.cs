namespace OnlineStore.Core.Abstractions.Models;

public class UserRegistrationModel {

	public string? Login { get; set; }
	public string? Password { get; set; }
	public string? ConfirmPassword { get; set; }
}