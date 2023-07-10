using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Core.Abstractions.Services.Crud;
using OnlineStore.Core.Abstractions.Services.RabbitMq;
using OnlineStore.WebAPI.Models;

namespace OnlineStore.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : Controller {
		
	public OrderController(
		IMapper mapper, 
		IOrderCrudService orderCrudService, 
		IUserCrudService userCrudService, 
		IRabbitMqService rabbitMqService) {

		_mapper = mapper;
		_orderCrudService = orderCrudService;
		_userCrudService = userCrudService;
		_rabbitMqService = rabbitMqService;
	}


	private IMapper _mapper;
	private IOrderCrudService _orderCrudService;
	private IUserCrudService _userCrudService;
	private IRabbitMqService _rabbitMqService;



	[HttpGet]
	[Authorize(Roles = "Admin")]
	public async Task<IEnumerable<OrderModel>> GetAll() {
		try {
			return _mapper.Map<IEnumerable<OrderModel>>(
				await _orderCrudService.GetAllAsync()
			);
		}
		catch(Exception exception) {
			throw new NullReferenceException();
		}
	}

	[HttpGet("{id}")]
	[Authorize(Roles = "Admin")]
	public async Task<OrderModel> GetById(int id) {
		try {
			return _mapper.Map<OrderModel>(
				await _orderCrudService.GetByIdAsync(id)
			);
		}
		catch(Exception exception) {
			throw new NullReferenceException();
		}
	}

	[HttpGet("own")]
	[Authorize(Roles = "User,Admin")]
	public async Task<IEnumerable<OrderModel>> GetOwn() {
		try {
			var res = _mapper.Map<IEnumerable<OrderModel>>(
				await _orderCrudService.GetByUserLoginAsync(
					User.Identity?.Name ?? ""
				)
			);

			return res;
		}
		catch(Exception exception) {
			throw new NullReferenceException();
		}
	}

	[HttpPost("checkout")]
	[Authorize(Roles = "User,Admin")]
	public async Task<IActionResult> Checkout(OrderCheckoutModel model) {
		try {
			var mappedOrder = _mapper.Map<Core.Abstractions.Models.Order>(model);
			mappedOrder.Customer = new Core.Abstractions.Models.User {
				Login = User.Identity.Name
			};

			await _orderCrudService.AddAsync(mappedOrder);

			_rabbitMqService.SendMessage(mappedOrder);

			return Ok();
		}
		catch(Exception exception) {
			throw new NullReferenceException();
		}
	}


	[HttpPost]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> Add(OrderPostModel model) {
		try {
			await _orderCrudService.AddAsync(
				_mapper.Map<Core.Abstractions.Models.Order>(model)
			);

			return Ok();
		}
		catch(Exception exception) {
			throw new NullReferenceException();
		}
	}

	[HttpPut]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> Update(OrderModel model) {
		try {
			await _orderCrudService.UpdateAsync(
				_mapper.Map<Core.Abstractions.Models.Order>(model)
			);

			return Ok();
		}
		catch(Exception exception) {
			throw new NullReferenceException();
		}
	}

	[HttpDelete]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> Remove(OrderModel model) {
		try {
			await _orderCrudService.RemoveAsync(
				_mapper.Map<Core.Abstractions.Models.Order>(model)
			);

			return Ok();
		}
		catch(Exception exception) {
			throw new NullReferenceException();
		}
	}

	[HttpDelete("{id}")]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> RemoveById(int id) {
		try {
			await _orderCrudService.RemoveAsync(
				_mapper.Map<Core.Abstractions.Models.Order>(
					new OrderModel { Id = id }
				)
			);

			return Ok();
		}
		catch(Exception exception) {
			throw new NullReferenceException();
		}
	}
}
