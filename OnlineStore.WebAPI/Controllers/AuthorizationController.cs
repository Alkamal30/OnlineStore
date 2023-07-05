using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OnlineStore.Core.Services.Authorization;
using OnlineStore.WebAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace OnlineStore.WebAPI.Controllers;

[ApiController]
[Route("api/")]
public class AuthorizationController : Controller {

	public AuthorizationController(IAuthorizationService authorizationService, IMapper mapper) {
		_authorizationService = authorizationService;
		_mapper = mapper;
	}


	private IAuthorizationService _authorizationService;
	private IMapper _mapper;



	[HttpPost]
	[Route("Authorize")]
	public async Task<IActionResult> Authorize(UserAuthorizationModel viewModel) {
		try {
			if(!ModelState.IsValid)
				return Json("Data is not valid!");

			return Json(
				_mapper.Map<UserAuthorizationResponseModel>(
					await _authorizationService.AuthorizeAsync(
						_mapper.Map<Core.Models.UserAuthorizationModel>(viewModel)
					)
				)
			);
		}
		catch {
			throw new NullReferenceException();
		}
	}

	[HttpPost]
	[Route("Register")]
	public async Task Register(UserRegistrationModel viewModel) {
		try {
			if(!ModelState.IsValid)
				return;

			await _authorizationService.RegisterAsync(
				_mapper.Map<Core.Models.UserRegistrationModel>(viewModel)
			);
		}
		catch { }
	}
}
