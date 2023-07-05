using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Core.Services.Crud;
using OnlineStore.WebAPI.Filters;
using OnlineStore.WebAPI.Models;

namespace OnlineStore.WebAPI.Controllers;


[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase {

	public UserController(IUserCrudService crudService, IMapper mapper) {
		_crudService = crudService;
		_mapper = mapper;
	}


	private IUserCrudService _crudService;
	private IMapper _mapper;



	[HttpGet]
	[Authorize(Roles = "User,Admin")]
	[ServiceFilter(typeof(RequestsLimitFilter))]
	public async Task<IEnumerable<UserModel>> GetAll() {
		try {
			return _mapper.Map<IEnumerable<UserModel>>(
				await _crudService.GetAllAsync()
			);
		}
		catch {
			throw new NullReferenceException();
		}
	}

	[HttpGet("{id}")]
	[Authorize(Roles = "User,Admin")]
	public async Task<UserModel?> GetById(int id) {
		try {
			return _mapper.Map<UserModel>(
				await _crudService.GetByIdAsync(id)
			);
		}
		catch {
			throw new NullReferenceException();
		}
	}

	[HttpPost]
	[Authorize(Roles = "Admin")]
	public async Task Add(UserPostModel user) {
		try {
			await _crudService.AddAsync(
				_mapper.Map<Core.Models.User>(user)
			);
		}
		catch {
			throw new NullReferenceException();
		}
	}

	[HttpPut]
	[Authorize(Roles = "Admin")]
	public async Task Update(UserModel user) {
		try {
			await _crudService.UpdateAsync(
				_mapper.Map<Core.Models.User>(user)
			);
		}
		catch { }
	}

	[HttpDelete]
	[Authorize(Roles = "Admin")]
	public async Task Remove(UserModel user) {
		try {
			await _crudService.RemoveAsync(
				_mapper.Map<Core.Models.User>(user)
			);
		}
		catch { }
	}

	[HttpDelete("{id}")]
	[Authorize(Roles = "Admin")]
	public async Task RemoveById(int id) {
		try {
			await _crudService.RemoveAsync(
				_mapper.Map<Core.Models.User>(
					new UserModel { Id = id }
				)
			);
		}
		catch { }
	}
}
