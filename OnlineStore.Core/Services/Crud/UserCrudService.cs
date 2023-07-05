using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Core.Models;
using OnlineStore.Infrastructure;

namespace OnlineStore.Core.Services.Crud;

public class UserCrudService : IUserCrudService {

    public UserCrudService(OnlineStoreDbContext dbContext, IMapper mapper) {
        _dbContext = dbContext;
		_mapper = mapper;
    }


    private OnlineStoreDbContext _dbContext;
	private IMapper _mapper;



    public IEnumerable<User> GetAll() {
		return _mapper.Map<List<User>>(_dbContext.Users.ToList());
	}

    public User? GetById(int id) {
		return _mapper.Map<User>(_dbContext.Users.FirstOrDefault(x => x.Id == id));
	}


    public void Add(User model) {
        if (model is null)
            return;

        _dbContext.Users.Add(
			_mapper.Map<Infrastructure.Models.User>(model)
		);
        _dbContext.SaveChanges();
    }

    public void Remove(User model) {
		if(model is null)
			return;

		_dbContext.Users.Remove(
			_mapper.Map<Infrastructure.Models.User>(model)
		);
		_dbContext.SaveChanges();
	}

    public void Update(User model) {
		if(model is null)
			return;

		_dbContext.Users.Update(
			_mapper.Map<Infrastructure.Models.User>(model)
		);
		_dbContext.SaveChanges();
	}



	public async Task<IEnumerable<User>> GetAllAsync() {
		return _mapper.Map<List<User>>(await _dbContext.Users.ToListAsync());
	}

	public async Task<User?> GetByIdAsync(int id) {
		return _mapper.Map<User>(await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id));
	}

	public async Task<User?> GetByLoginAsync(string login) {
		return _mapper.Map<User>(
			await _dbContext.Users.FirstOrDefaultAsync(x => x.Login == login)
		);
	}


    public async Task AddAsync(User model) {
		if(model is null)
			return;

		await _dbContext.Users.AddAsync(
			_mapper.Map<Infrastructure.Models.User>(model)	
		);
		await _dbContext.SaveChangesAsync();
	}

    public async Task RemoveAsync(User model) {
		if(model is null)
			return;

		_dbContext.Users.Remove(
			_mapper.Map<Infrastructure.Models.User>(model)
		);
		await _dbContext.SaveChangesAsync();
	}

    public async Task UpdateAsync(User model) {
		if(model is null)
			return;

		_dbContext.Users.Update(
			_mapper.Map<Infrastructure.Models.User>(model)
		);
		await _dbContext.SaveChangesAsync();
	}
}