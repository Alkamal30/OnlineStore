using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Core.Abstractions.Models;
using OnlineStore.Core.Abstractions.Services.Crud;
using OnlineStore.Infrastructure;

namespace OnlineStore.Core.Services.Crud;

public class UserCrudService : IUserCrudService {

    public UserCrudService(IDbContextFactory<OnlineStoreDbContext> dbContextFactory, IMapper mapper) {
        _dbContextFactory = dbContextFactory;
		_mapper = mapper;
    }


    private IDbContextFactory<OnlineStoreDbContext> _dbContextFactory;
	private IMapper _mapper;



    public IEnumerable<User> GetAll() {
		using var dbContext = _dbContextFactory.CreateDbContext();

		var users = _mapper.Map<List<User>>(
			dbContext.Users.ToList()
		);

		return users;
	}

    public User? GetById(int id) {
		using var dbContext = _dbContextFactory.CreateDbContext();

		var user = _mapper.Map<User>(
			dbContext.Users.FirstOrDefault(x => x.Id == id)
		);

		return user;
	}


    public void Add(User model) {
        if (model is null)
            return;

		using var dbContext = _dbContextFactory.CreateDbContext();

		dbContext.Users.Add(
			_mapper.Map<Infrastructure.Models.User>(model)
		);
        dbContext.SaveChanges();
    }

    public void Remove(User model) {
		if(model is null)
			return;

		using var dbContext = _dbContextFactory.CreateDbContext();

		dbContext.Users.Remove(
			_mapper.Map<Infrastructure.Models.User>(model)
		);
		dbContext.SaveChanges();
	}

    public void Update(User model) {
		if(model is null)
			return;

		using var dbContext = _dbContextFactory.CreateDbContext();

		dbContext.Users.Update(
			_mapper.Map<Infrastructure.Models.User>(model)
		);
		dbContext.SaveChanges();
	}



	public async Task<IEnumerable<User>> GetAllAsync() {
		using var dbContext = await _dbContextFactory.CreateDbContextAsync();

		var users = _mapper.Map<List<User>>(
			await dbContext.Users.ToListAsync()
		);

		return users;
	}

	public async Task<User?> GetByIdAsync(int id) {
		using var dbContext = await _dbContextFactory.CreateDbContextAsync();

		var user = _mapper.Map<User>(
			await dbContext.Users.FirstOrDefaultAsync(x => x.Id == id)
		);

		return user;
	}

	public async Task<User?> GetByLoginAsync(string login) {
		using var dbContext = await _dbContextFactory.CreateDbContextAsync();

		var user = _mapper.Map<User>(
			await dbContext.Users.FirstOrDefaultAsync(x => x.Login == login)
		);

		return user;
	}


    public async Task AddAsync(User model) {
		if(model is null)
			return;

		using var dbContext = await _dbContextFactory.CreateDbContextAsync();

		await dbContext.Users.AddAsync(
			_mapper.Map<Infrastructure.Models.User>(model)	
		);
		await dbContext.SaveChangesAsync();
	}

    public async Task RemoveAsync(User model) {
		if(model is null)
			return;

		using var dbContext = await _dbContextFactory.CreateDbContextAsync();

		dbContext.Users.Remove(
			_mapper.Map<Infrastructure.Models.User>(model)
		);
		await dbContext.SaveChangesAsync();
	}

    public async Task UpdateAsync(User model) {
		if(model is null)
			return;

		using var dbContext = await _dbContextFactory.CreateDbContextAsync();

		dbContext.Users.Update(
			_mapper.Map<Infrastructure.Models.User>(model)
		);
		await dbContext.SaveChangesAsync();
	}
}