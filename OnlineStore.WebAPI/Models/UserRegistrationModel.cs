using System.ComponentModel.DataAnnotations;

namespace OnlineStore.WebAPI.Models;

public class UserRegistrationModel {

	[Required]
	public string Login { get; set; } = "";

	[Required]
	public string Password { get; set; } = "";

	[Required]
	[Compare(nameof(Password))]
	public string ConfirmPassword { get; set; } = "";
}