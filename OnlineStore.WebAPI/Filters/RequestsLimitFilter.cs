using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OnlineStore.Core.Abstractions.Services.Crud;
using OnlineStore.WebAPI.Models;

namespace OnlineStore.WebAPI.Filters;

public class RequestsLimitFilter : ActionFilterAttribute {

	public RequestsLimitFilter(IUserRequestsCrudService userRequestsCrudService, IConfiguration configuration, IMapper mapper) {
		_userRequestsCrudService = userRequestsCrudService;
		_configuration = configuration;
		_mapper = mapper;
	}


	private readonly IUserRequestsCrudService _userRequestsCrudService;
	private readonly IConfiguration _configuration;
	private readonly IMapper _mapper;


	public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) {
		try {
			var login = context.HttpContext.User.Identity?.Name;
			var userRequests = _mapper.Map<UserRequests>(
				await _userRequestsCrudService.GetByLoginAsync(login)
			);

			if(userRequests is null || userRequests.RequestsCount > int.Parse(_configuration["MaxRequests"])) {
				context.Result = new StatusCodeResult(StatusCodes.Status429TooManyRequests);
				return;
			}

			await next();
		}
		catch {
			throw new NullReferenceException();
		}
	}
}
