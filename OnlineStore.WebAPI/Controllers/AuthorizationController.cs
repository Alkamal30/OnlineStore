using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Core.Abstractions.Services.Authorization;
using OnlineStore.WebAPI.Models;

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
						_mapper.Map<Core.Abstractions.Models.UserAuthorizationModel>(viewModel)
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
				_mapper.Map<Core.Abstractions.Models.UserRegistrationModel>(viewModel)
			);
		}
		catch { }
	}
}
