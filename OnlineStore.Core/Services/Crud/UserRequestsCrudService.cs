using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Core.Models;
using OnlineStore.Core.Services.RedisCache;
using OnlineStore.Infrastructure;

namespace OnlineStore.Core.Services.Crud;


public class UserRequestsCrudService : IUserRequestsCrudService {

	public UserRequestsCrudService(OnlineStoreDbContext dbContext, IMapper mapper, IRedisService redisService) {
		_dbContext = dbContext;
		_mapper = mapper;
		_redisService = redisService;
	}


	private readonly OnlineStoreDbContext _dbContext;
	private readonly IMapper _mapper;
	private readonly IRedisService _redisService;

	private readonly string _redisKey = "user_requests";



	public IEnumerable<UserRequests> GetAll() {
		var cachedUserRequestsList = _redisService.GetObject<List<UserRequests>>(_redisKey);
		if(cachedUserRequestsList is not null)
			return cachedUserRequestsList;

		var userRequestsList = _mapper.Map<IEnumerable<UserRequests>>(
			_dbContext.UserRequests.ToList()
		);
		if(userRequestsList is null)
			return null;

		_redisService.SetObject(_redisKey, userRequestsList);
		return userRequestsList;
	}

	public UserRequests? GetById(int id) {
		var key = _redisKey + id.ToString();
		var cachedUserRequests = _redisService.GetObject<UserRequests>(key);
		if(cachedUserRequests is not null)
			return cachedUserRequests;

		var userRequests = _mapper.Map<UserRequests>(
			_dbContext.UserRequests.FirstOrDefault(x => x.Id == id)
		);
		if(userRequests is null)
			return null;

		_redisService.SetObject(key, userRequests);
		return userRequests;
	}

	public UserRequests? GetByLogin(string login) {
		var key = _redisKey + "_login_" + login;
		var cachedUserRequests = _redisService.GetObject<UserRequests>(key);
		if(cachedUserRequests is not null)
			return cachedUserRequests;

		var userRequests = _mapper.Map<UserRequests>(
			_dbContext.UserRequests.FirstOrDefault(x => x.Login == login)
		);
		if(userRequests is null)
			return null;

		_redisService.SetObject(key, userRequests);
		return userRequests;
	}


	public void Add(UserRequests model) {
		if(model is null)
			return;

		_dbContext.UserRequests.Add(
			_mapper.Map<Infrastructure.Models.UserRequests>(model)	
		);
		_dbContext.SaveChanges();

		UpdateCache();
	}

	public void Update(UserRequests model) {
		if(model is null)
			return;

		_dbContext.UserRequests.Update(
			_mapper.Map<Infrastructure.Models.UserRequests>(model)
		);
		_dbContext.SaveChanges();

		var key = _redisKey + model.Id.ToString();
		if(_redisService.GetObject<UserRequests>(key) is not null)
			_redisService.SetObject(key, model);

		UpdateCache();
	}

	public void Remove(UserRequests model) {
		if(model is null)
			return;

		_dbContext.UserRequests.Remove(
			_mapper.Map<Infrastructure.Models.UserRequests>(model)
		);
		_dbContext.SaveChanges();
		UpdateCache();

		_redisService.DistributedCache.Remove(_redisKey + model.Id.ToString());
	}



	public async Task<IEnumerable<UserRequests>> GetAllAsync() {
		var cachedUserRequestsList = await _redisService.GetObjectAsync<List<UserRequests>>(_redisKey);
		if(cachedUserRequestsList is not null)
			return cachedUserRequestsList;

		var userRequestsList = _mapper.Map<IEnumerable<UserRequests>>(
			await _dbContext.UserRequests.ToListAsync()
		);
		if(userRequestsList is null)
			return null;

		await _redisService.SetObjectAsync(_redisKey, userRequestsList);
		return userRequestsList;
	}

	public async Task<UserRequests?> GetByIdAsync(int id) {
		var key = _redisKey + id.ToString();
		var cachedUserRequests = await _redisService.GetObjectAsync<UserRequests>(key);
		if(cachedUserRequests is not null)
			return cachedUserRequests;

		var userRequests = _mapper.Map<UserRequests>(
			await _dbContext.UserRequests.FirstOrDefaultAsync(x => x.Id == id)
		);
		if(userRequests is null)
			return null;

		await _redisService.SetObjectAsync(key, userRequests);
		return userRequests;
	}

	public async Task<UserRequests?> GetByLoginAsync(string login) {
		var key = _redisKey + "_login_" + login;
		var cachedUserRequests = await _redisService.GetObjectAsync<UserRequests>(key);
		if(cachedUserRequests is not null)
			return cachedUserRequests;

		var userRequests = _mapper.Map<UserRequests>(
			await _dbContext.UserRequests.FirstOrDefaultAsync(x => x.Login == login)
		);
		if(userRequests is null)
			return null;

		await _redisService.SetObjectAsync(key, userRequests);
		return userRequests;
	}


	public async Task AddAsync(UserRequests model) {
		if(model is null)
			return;

		await _dbContext.UserRequests.AddAsync(
			_mapper.Map<Infrastructure.Models.UserRequests>(model)
		);

		await _dbContext.SaveChangesAsync();
		await UpdateCacheAsync();
	}

	public async Task UpdateAsync(UserRequests model) {
		if(model is null)
			return;

		_dbContext.UserRequests.Update(
			_mapper.Map<Infrastructure.Models.UserRequests>(model)
		);
		await _dbContext.SaveChangesAsync();
		await UpdateCacheAsync();

		var key = _redisKey + model.Id.ToString();
		if(await _redisService.GetObjectAsync<UserRequests>(key) is not null)
			await _redisService.SetObjectAsync(key, model);
	}

	public async Task RemoveAsync(UserRequests model) {
		if(model is null)
			return;

		_dbContext.UserRequests.Remove(
			_mapper.Map<Infrastructure.Models.UserRequests>(model)
		);

		await _dbContext.SaveChangesAsync();
		await UpdateCacheAsync();

		await _redisService.DistributedCache.RemoveAsync(_redisKey + model.Id.ToString());
	}



	private void UpdateCache() {
		if(_redisService.GetObject<List<UserRequests>>(_redisKey) is null)
			return;

		var userRequestsList = _dbContext.UserRequests.ToList();
		if(userRequestsList is null)
			return;

		_redisService.SetObject(_redisKey, userRequestsList);
	}

	private async Task UpdateCacheAsync() {
		if(_redisService.GetObjectAsync<List<UserRequests>>(_redisKey) is null)
			return;

		var userRequestsList = await _dbContext.UserRequests.ToListAsync();
		if(userRequestsList is null)
			return;

		await _redisService.SetObjectAsync(_redisKey, userRequestsList);
	}
}