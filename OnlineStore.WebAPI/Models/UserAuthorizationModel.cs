using System.ComponentModel.DataAnnotations;

namespace OnlineStore.WebAPI.Models;

public class UserAuthorizationModel {

	[Required]
	public string Login { get; set; } = "";

	[Required]
	public string Password { get; set; } = "";
}