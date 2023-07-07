using OnlineStore.Common.Enums;

namespace OnlineStore.Core.Models;

public class UserAuthorizationResponseModel {

	public string? Login { get; set; }
	public string? Password { get; set; }
	public UserRole Role { get; set; }
	public string? JwtToken { get; set; }
}
