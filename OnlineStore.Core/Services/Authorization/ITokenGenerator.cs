using OnlineStore.Core.Models;

namespace OnlineStore.Core.Services.Authorization;

public interface ITokenGenerator {
	string GenerateToken(User user);
}