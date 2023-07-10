using OnlineStore.Core.Abstractions.Models;

namespace OnlineStore.Core.Abstractions.Services.Authorization;

public interface ITokenGenerator {
	string GenerateToken(User user);
}