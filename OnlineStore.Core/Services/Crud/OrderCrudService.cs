using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Core.Abstractions.Models;
using OnlineStore.Core.Abstractions.Services.Crud;
using OnlineStore.Infrastructure;

namespace OnlineStore.Core.Services.Crud;

public class OrderCrudService : IOrderCrudService {

	public OrderCrudService(IDbContextFactory<OnlineStoreDbContext> dbContextFactory, IMapper mapper) {
		_dbContextFactory = dbContextFactory;
		_mapper = mapper;
	}


	private IDbContextFactory<OnlineStoreDbContext> _dbContextFactory;
	private IMapper _mapper;


	public IEnumerable<Order> GetAll() {
		using var dbContext = _dbContextFactory.CreateDbContext();

		var orders = _mapper.Map<IEnumerable<Order>>(
			dbContext.Orders
				.Include(x => x.Customer)
				.Include(x => x.OrderItems)
					.ThenInclude(y => y.Product)
				.ToList()
		);

		return orders;
	}

	public Order? GetById(int id) {
		using var dbContext = _dbContextFactory.CreateDbContext();

		var order = _mapper.Map<Order>(
			dbContext.Orders
				.Include(x => x.Customer)
				.Include(x => x.OrderItems)
					.ThenInclude(y => y.Product)
				.FirstOrDefault(x => x.Id == id)
		);

		return order;
	}

	public IEnumerable<Order> GetByUserLogin(string login) {
		using var dbContext = _dbContextFactory.CreateDbContext();

		var order = _mapper.Map<IEnumerable<Order>>(
			dbContext.Orders
				.Include(x => x.Customer)
				.Include(x => x.OrderItems)
					.ThenInclude(y => y.Product)
				.Where(x => x.Customer.Login == login)
				.ToList()
		);

		return order;
	}


	public void Add(Order model) {
		using var dbContext = _dbContextFactory.CreateDbContext();

		dbContext.Orders.Add(
			_mapper.Map<Infrastructure.Models.Order>(model)
		);
		dbContext.SaveChanges();
	}

	public void Update(Order model) {
		using var dbContext = _dbContextFactory.CreateDbContext();

		dbContext.Orders.Add(
			_mapper.Map<Infrastructure.Models.Order>(model)
		);
		dbContext.SaveChanges();
	}

	public void Remove(Order model) {
		using var dbContext = _dbContextFactory.CreateDbContext();

		dbContext.Orders.Add(
			_mapper.Map<Infrastructure.Models.Order>(model)
		);
		dbContext.SaveChanges();
	}



	public async Task<IEnumerable<Order>> GetAllAsync() {
		using var dbContext = await _dbContextFactory.CreateDbContextAsync();

		var orders = _mapper.Map<IEnumerable<Order>>(
			await dbContext.Orders
				.Include(x => x.Customer)
				.Include(x => x.OrderItems)
					.ThenInclude(y => y.Product)
				.ToListAsync()
		);

		return orders;
	}

	public async Task<Order?> GetByIdAsync(int id) {
		using var dbContext = await _dbContextFactory.CreateDbContextAsync();

		var order = _mapper.Map<Order>(
			await dbContext.Orders
				.Include(x => x.Customer)
				.Include(x => x.OrderItems)
					.ThenInclude(y => y.Product)
				.FirstOrDefaultAsync(x => x.Id == id)
		);

		return order;
	}

	public async Task<IEnumerable<Order>> GetByUserLoginAsync(string userLogin) {
		using var dbContext = await _dbContextFactory.CreateDbContextAsync();

		var orders = _mapper.Map<IEnumerable<Order>>(
			await dbContext.Orders
				.Include(x => x.Customer)
				.Include(x => x.OrderItems)
					.ThenInclude(y => y.Product)
				.Where(x => x.Customer.Login == userLogin)
				.ToListAsync()
		);

		return orders;
	}


	public async Task AddAsync(Order model) {
		using var dbContext = await _dbContextFactory.CreateDbContextAsync();

		var mapModel = _mapper.Map<Infrastructure.Models.Order>(model);
		mapModel.Customer = dbContext.Users.FirstOrDefault(x => x.Login == model.Customer.Login);

		foreach(var orderItem in mapModel.OrderItems) {
			dbContext.Entry(orderItem.Product).State = EntityState.Unchanged;
		}

		await dbContext.Orders.AddAsync(mapModel);
		await dbContext.SaveChangesAsync();
	}

	public async Task UpdateAsync(Order model) {
		using var dbContext = await _dbContextFactory.CreateDbContextAsync();

		dbContext.Orders.Update(
			_mapper.Map<Infrastructure.Models.Order>(model)
		);
		await dbContext.SaveChangesAsync();
	}

	public async Task RemoveAsync(Order model) {
		using var dbContext = await _dbContextFactory.CreateDbContextAsync();

		dbContext.Orders.Remove(
			_mapper.Map<Infrastructure.Models.Order>(model)
		);
		await dbContext.SaveChangesAsync();
	}
}
