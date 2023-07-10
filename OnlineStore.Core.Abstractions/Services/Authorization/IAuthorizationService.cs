using OnlineStore.Core.Abstractions.Models;

namespace OnlineStore.Core.Abstractions.Services.Authorization;

public interface IAuthorizationService {
    Task<UserAuthorizationResponseModel> AuthorizeAsync(UserAuthorizationModel viewModel);
    Task<string> RegisterAsync(UserRegistrationModel viewModel);
}