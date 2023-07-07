using OnlineStore.Common.Enums;

namespace OnlineStore.WebAPI.Models;

public class UserPostModel {
	public string Login { get; set; } = "";
	public string Password { get; set; } = "";
	public UserRole Role { get; set; }
}
