using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Core.Abstractions.Models;
using OnlineStore.Core.Abstractions.Services.Authorization;
using OnlineStore.Infrastructure;

namespace OnlineStore.Core.Services.Authorization;

public class AuthorizationService : IAuthorizationService {

    public AuthorizationService(IDbContextFactory<OnlineStoreDbContext> dbContextFactory, ITokenGenerator tokenGenerator, IMapper mapper) {
        _dbContextFactory = dbContextFactory;
        _tokenGenerator = tokenGenerator;
        _mapper = mapper;
    }


    private IDbContextFactory<OnlineStoreDbContext> _dbContextFactory;
    private ITokenGenerator _tokenGenerator;
    private IMapper _mapper;



    public async Task<UserAuthorizationResponseModel> AuthorizeAsync(UserAuthorizationModel viewModel) {
		User? user = null;
		if((user = await TryAuthorizeUserAsync(viewModel)) is null)
			return null;

        return new UserAuthorizationResponseModel {
               Login = user.Login,
               Password = user.Password,
               Role = user.Role,
               JwtToken = _tokenGenerator.GenerateToken(user)
		};
	}

    private async Task<User?> TryAuthorizeUserAsync(UserAuthorizationModel viewModel) {
        using var dbContext = await _dbContextFactory.CreateDbContextAsync();

        if (viewModel is null)
            return null;

        var foundUser = _mapper.Map<User>(await dbContext.Users.FirstOrDefaultAsync(x => x.Login == viewModel.Login));
        if (foundUser is null)
            return null;

        if (foundUser.Password != viewModel.Password)
            return null;

        return foundUser;
    }



    public async Task<string> RegisterAsync(UserRegistrationModel viewModel) {
        try {
            using var dbContext = await _dbContextFactory.CreateDbContextAsync();

            if (viewModel is null)
                return "";

            var foundUser = await dbContext.Users.FirstOrDefaultAsync(x => x.Login == viewModel.Login);
            if (foundUser is not null)
                return "";

            var user = new User
            {
                Login = viewModel.Login,
                Password = viewModel.Password
            };

            await dbContext.Users.AddAsync(
                _mapper.Map<Infrastructure.Models.User>(user)    
            );
            await dbContext.SaveChangesAsync();

            return "";
        }
        catch {
            return "";
        }
    }
}