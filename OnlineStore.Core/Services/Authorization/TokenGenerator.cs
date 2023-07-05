using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnlineStore.Core.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnlineStore.Core.Services.Authorization;

public class TokenGenerator : ITokenGenerator {

	public TokenGenerator(IConfiguration configuration) {
		_configuration = configuration;
	}


	private IConfiguration _configuration;


	public string GenerateToken(User user) {
		var handler = new JwtSecurityTokenHandler();
		var tokenDescriptor = CreateSecurityTokenDescriptor(user);
		var token = handler.CreateToken(tokenDescriptor);

		return handler.WriteToken(token);
	}

	private SecurityTokenDescriptor CreateSecurityTokenDescriptor(User user) {
		return new SecurityTokenDescriptor() {
			Issuer = _configuration["JwtSettings:Issuer"],
			Audience = _configuration["JwtSettings:Audience"],
			Subject = new ClaimsIdentity(
				new List<Claim>() {
					new Claim(ClaimTypes.Name, user.Login),
					new Claim(ClaimTypes.Role, user.Role.ToString())
				}	
			),
			SigningCredentials = new SigningCredentials(
				new SymmetricSecurityKey(
					Encoding.UTF8.GetBytes(
						_configuration["JwtSettings:SecretKey"]
					)
				),
				SecurityAlgorithms.HmacSha512Signature
			)
		};
	}
}