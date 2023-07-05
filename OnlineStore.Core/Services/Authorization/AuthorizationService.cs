using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnlineStore.Core.Models;
using OnlineStore.Infrastructure;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnlineStore.Core.Services.Authorization;

public class AuthorizationService : IAuthorizationService {

    public AuthorizationService(OnlineStoreDbContext dbContext, ITokenGenerator tokenGenerator, IMapper mapper) {
        _dbContext = dbContext;
        _tokenGenerator = tokenGenerator;
        _mapper = mapper;
    }


    private OnlineStoreDbContext _dbContext;
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
        if (viewModel is null)
            return null;

        var foundUser = _mapper.Map<User>(await _dbContext.Users.FirstOrDefaultAsync(x => x.Login == viewModel.Login));
        if (foundUser is null)
            return null;

        if (foundUser.Password != viewModel.Password)
            return null;

        return foundUser;
    }



    public async Task<string> RegisterAsync(UserRegistrationModel viewModel) {
        try {
            if (viewModel is null)
                return "";

            var foundUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Login == viewModel.Login);
            if (foundUser is not null)
                return "";

            var user = new User
            {
                Login = viewModel.Login,
                Password = viewModel.Password
            };

            await _dbContext.Users.AddAsync(
                _mapper.Map<Infrastructure.Models.User>(user)    
            );
            await _dbContext.SaveChangesAsync();
            return "";
        }
        catch {
            return "";
        }
    }
}