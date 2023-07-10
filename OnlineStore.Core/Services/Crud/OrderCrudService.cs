using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Core.Abstractions.Models;
using OnlineStore.Core.Abstractions.Services.Crud;
using OnlineStore.Infrastructure;

namespace OnlineStore.Core.Services.Crud;

public class OrderCrudService : IOrderCrudService {

	public OrderCrudService(OnlineStoreDbContext dbContext, IMapper mapper) {
		_dbContext = dbContext;
		_mapper = mapper;
	}


	private OnlineStoreDbContext _dbContext;
	private IMapper _mapper;


	public IEnumerable<Order> GetAll() {
		return _mapper.Map<IEnumerable<Order>>(
			_dbContext.Orders
				.Include(x => x.Customer)
				.Include(x => x.OrderItems)
					.ThenInclude(y => y.Product)
				.ToList()
		);
	}

	public Order? GetById(int id) {
		return _mapper.Map<Order>(
			_dbContext.Orders
				.Include(x => x.Customer)
				.Include(x => x.OrderItems)
					.ThenInclude(y => y.Product)
				.FirstOrDefault(x => x.Id == id)
		);
	}

	public IEnumerable<Order> GetByUserLogin(string login) {
		return _mapper.Map<IEnumerable<Order>>(
			_dbContext.Orders
				.Include(x => x.Customer)
				.Include(x => x.OrderItems)
					.ThenInclude(y => y.Product)
				.Where(x => x.Customer.Login == login)
				.ToList()
		);
	}


	public void Add(Order model) {
		_dbContext.Orders.Add(
			_mapper.Map<Infrastructure.Models.Order>(model)
		);
		_dbContext.SaveChanges();
	}

	public void Update(Order model) {
		_dbContext.Orders.Add(
			_mapper.Map<Infrastructure.Models.Order>(model)
		);
		_dbContext.SaveChanges();
	}

	public void Remove(Order model) {
		_dbContext.Orders.Add(
			_mapper.Map<Infrastructure.Models.Order>(model)
		);
		_dbContext.SaveChanges();
	}



	public async Task<IEnumerable<Order>> GetAllAsync() {
		return _mapper.Map<IEnumerable<Order>>(
			await _dbContext.Orders
				.Include(x => x.Customer)
				.Include(x => x.OrderItems)
					.ThenInclude(y => y.Product)
				.ToListAsync()
		);
	}

	public async Task<Order?> GetByIdAsync(int id) {
		return _mapper.Map<Order>(
			await _dbContext.Orders
				.Include(x => x.Customer)
				.Include(x => x.OrderItems)
					.ThenInclude(y => y.Product)
				.FirstOrDefaultAsync(x => x.Id == id)
		);
	}

	public async Task<IEnumerable<Order>> GetByUserLoginAsync(string userLogin) {
		return _mapper.Map<IEnumerable<Order>>(
			await _dbContext.Orders
				.Include(x => x.Customer)
				.Include(x => x.OrderItems)
					.ThenInclude(y => y.Product)
				.Where(x => x.Customer.Login == userLogin)
				.ToListAsync()
		);
	}


	public async Task AddAsync(Order model) {
		var mapModel = _mapper.Map<Infrastructure.Models.Order>(model);
		mapModel.Customer = _dbContext.Users.FirstOrDefault(x => x.Login == model.Customer.Login);

		foreach(var orderItem in mapModel.OrderItems) {
			_dbContext.Entry(orderItem.Product).State = EntityState.Unchanged;
		}

		await _dbContext.Orders.AddAsync(mapModel);
		await _dbContext.SaveChangesAsync();
	}

	public async Task UpdateAsync(Order model) {
		_dbContext.Orders.Update(
			_mapper.Map<Infrastructure.Models.Order>(model)
		);
		await _dbContext.SaveChangesAsync();
	}

	public async Task RemoveAsync(Order model) {
		_dbContext.Orders.Remove(
			_mapper.Map<Infrastructure.Models.Order>(model)
		);
		await _dbContext.SaveChangesAsync();
	}
}
