using OnlineStore.Core.Models;

namespace OnlineStore.Core.Services.Authorization;

public interface IAuthorizationService {
    Task<UserAuthorizationResponseModel> AuthorizeAsync(UserAuthorizationModel viewModel);
    Task<string> RegisterAsync(UserRegistrationModel viewModel);
}