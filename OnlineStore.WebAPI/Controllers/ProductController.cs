using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Core.Services.Crud;
using OnlineStore.WebAPI.Filters;
using OnlineStore.WebAPI.Models;

namespace OnlineStore.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
//[ServiceFilter(typeof(RequestsLimitFilter))]
public class ProductController : ControllerBase {
		
	public ProductController(IProductCrudService crudService, IMapper mapper) {
		_crudService = crudService;
		_mapper = mapper;
	}


	private IProductCrudService _crudService;
	private IMapper _mapper;



	[HttpGet]
	[Authorize(Roles = "User,Admin")]
	public async Task<IEnumerable<ProductModel>> GetAll() {
		try {
			return _mapper.Map<IEnumerable<ProductModel>>(await _crudService.GetAllAsync());
		}
		catch {
			throw new NullReferenceException();
		}
	}

	[HttpGet("{id}")]
	[Authorize(Roles = "User,Admin")]
	public async Task<ProductModel?> GetById(int id) {
		try {
			return _mapper.Map<ProductModel>(await _crudService.GetByIdAsync(id));
		}
		catch {
			throw new NullReferenceException();
		}
	}

	[HttpPost]
	[Authorize(Roles = "Admin")]
	public async Task Add(ProductPostModel product) {
		try {
			await _crudService.AddAsync(
				_mapper.Map<Core.Models.Product>(product)	
			);
		}
		catch { }
	}

	[HttpPut]
	[Authorize(Roles = "Admin")]
	public async Task Update(ProductModel product) {
		try {
			await _crudService.UpdateAsync(
				_mapper.Map<Core.Models.Product>(product)
			);
		}
		catch { }
	}

	[HttpDelete]
	[Authorize(Roles = "Admin")]
	public async Task Remove(ProductModel product) {
		try {
			await _crudService.RemoveAsync(
				_mapper.Map<Core.Models.Product>(product)
			);
		}
		catch { }
	}

	[HttpDelete("{id}")]
	[Authorize(Roles = "Admin")]
	public async Task RemoveById(int id) {
		try {
			await _crudService.RemoveAsync(
				new Core.Models.Product() {
					Id = id
				}
			);
		}
		catch {
			throw new NullReferenceException();
		}
	}
}
