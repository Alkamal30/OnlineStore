using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Core.Abstractions.Services.Crud;
using OnlineStore.Core.Abstractions.Models;
using OnlineStore.Infrastructure;
using OnlineStore.Core.Abstractions.Services.Redis;

namespace OnlineStore.Core.Services.Crud;

public class ProductCrudService : IProductCrudService {

    public ProductCrudService(OnlineStoreDbContext dbContext, IRedisService redisService, IMapper mapper) {
        _dbContext = dbContext;
		_redisService = redisService;
		_mapper = mapper;
    }


    private readonly OnlineStoreDbContext _dbContext;
	private readonly IRedisService _redisService;
	private readonly IMapper _mapper;

	private readonly string _redisKey = "products";


    public IEnumerable<Product> GetAll() {
		var cachedProductsList = _redisService.GetObject<List<Product>>(_redisKey);
		if(cachedProductsList is not null)
			return cachedProductsList;

		var productsList = _mapper.Map<IEnumerable<Product>>(_dbContext.Products.ToList());
		if(productsList is null)
			return null;

		_redisService.SetObject(_redisKey, productsList);
		return productsList;
	}

    public Product? GetById(int id) {
		var key = _redisKey + id.ToString();
		var cachedProduct = _redisService.GetObject<Product>(key);
		if(cachedProduct is not null)
			return cachedProduct;

		var product = _mapper.Map<Product>(_dbContext.Products.FirstOrDefault(x => x.Id == id));
		if(product is null)
			return null;

		_redisService.SetObject(key, product);
		return product;
	}


    public void Add(Product model) {
		if(model is null)
			return;

		_dbContext.Products.Add(
			_mapper.Map<Infrastructure.Models.Product>(model)	
		);
		_dbContext.SaveChanges();

		UpdateCache();
	}

	public void Update(Product model) {
		if(model is null)
			return;

		var modelDTO = _mapper.Map<Infrastructure.Models.Product>(model);
		_dbContext.Products.Update(modelDTO);
		_dbContext.SaveChanges();
		UpdateCache();

		var key = _redisKey + modelDTO.Id.ToString();
		if(_redisService.GetObject<Product>(key) is not null)
			_redisService.SetObject(key, modelDTO);
	}

	public void Remove(Product model) {
		if(model is null)
			return;

		var modelDTO = _mapper.Map<Infrastructure.Models.Product>(model);
		_dbContext.Products.Remove(modelDTO);
		_dbContext.SaveChanges();
		UpdateCache();

		_redisService.DistributedCache.Remove(_redisKey + modelDTO.Id.ToString());
	}



	public async Task<IEnumerable<Product>> GetAllAsync() {
		var cachedProducts = await _redisService.GetObjectAsync<List<Product>>(_redisKey);
		if(cachedProducts is not null)
			return cachedProducts;

		var productsList = _mapper.Map<IEnumerable<Product>>(await _dbContext.Products.ToListAsync());
		if(productsList is null)
			return null;

		await _redisService.SetObjectAsync(_redisKey, productsList);
		return productsList;
	}

	public async Task<Product?> GetByIdAsync(int id) {
		var key = _redisKey + id.ToString();
		var cachedProduct = await _redisService.GetObjectAsync<Product>(key);
		if(cachedProduct is not null)
			return cachedProduct;

		var product = _mapper.Map<Product>(await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id));
		if(product is null)
			return null;

		await _redisService.SetObjectAsync(key, product);
		return product;
	}


    public async Task AddAsync(Product model) {
		if(model is null)
			return;

		await _dbContext.Products.AddAsync(
			_mapper.Map<Infrastructure.Models.Product>(model)
		);
		await _dbContext.SaveChangesAsync();
		await UpdateCacheAsync();
	}

	public async Task UpdateAsync(Product model) {
		if(model is null)
			return;

		var modelDTO = _mapper.Map<Infrastructure.Models.Product>(model);
		_dbContext.Products.Update(modelDTO);
		await _dbContext.SaveChangesAsync();
		await UpdateCacheAsync();

		var key = _redisKey + modelDTO.Id.ToString();
		if(await _redisService.GetObjectAsync<Product>(key) is not null)
			await _redisService.SetObjectAsync(key, modelDTO);
	}

	public async Task RemoveAsync(Product model) {
		if(model is null)
			return;

		var modelDTO = _mapper.Map<Infrastructure.Models.Product>(model);
		_dbContext.Products.Remove(modelDTO);
		await _dbContext.SaveChangesAsync();
		await UpdateCacheAsync();

		await _redisService.DistributedCache.RemoveAsync(_redisKey + modelDTO.Id.ToString());
	}


	private void UpdateCache() {
		if(_redisService.GetObject<List<UserRequests>>(_redisKey) is null)
			return;

		var productsList = _dbContext.Products.ToList();
		if(productsList is null)
			return;

		_redisService.SetObject(_redisKey, productsList);
	}

	private async Task UpdateCacheAsync() {
		if(await _redisService.GetObjectAsync<List<UserRequests>>(_redisKey) is null)
			return;

		var productsList = await _dbContext.Products.ToListAsync();
		if(productsList is null)
			return;

		await _redisService.SetObjectAsync(_redisKey, productsList);
	}
}