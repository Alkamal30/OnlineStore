using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Core.Abstractions.Services.Crud;
using OnlineStore.Core.Abstractions.Models;
using OnlineStore.Infrastructure;
using OnlineStore.Core.Abstractions.Services.Redis;

namespace OnlineStore.Core.Services.Crud;

public class ProductCrudService : IProductCrudService {

    public ProductCrudService(IDbContextFactory<OnlineStoreDbContext> dbContextFactory, IRedisService redisService, IMapper mapper) {
        _dbContextFactory = dbContextFactory;
		_redisService = redisService;
		_mapper = mapper;
    }


    private readonly IDbContextFactory<OnlineStoreDbContext> _dbContextFactory;
	private readonly IRedisService _redisService;
	private readonly IMapper _mapper;

	private readonly string _redisKey = "products";


    public IEnumerable<Product> GetAll() {
		var cachedProductsList = _redisService.GetObject<List<Product>>(_redisKey);
		if(cachedProductsList is not null)
			return cachedProductsList;

		using var dbContext = _dbContextFactory.CreateDbContext();

		var productsList = _mapper.Map<IEnumerable<Product>>(dbContext.Products.ToList());
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

		using var dbContext = _dbContextFactory.CreateDbContext();

		var product = _mapper.Map<Product>(dbContext.Products.FirstOrDefault(x => x.Id == id));
		if(product is null)
			return null;

		_redisService.SetObject(key, product);
		return product;
	}


    public void Add(Product model) {
		if(model is null)
			return;

		using var dbContext = _dbContextFactory.CreateDbContext();

		dbContext.Products.Add(
			_mapper.Map<Infrastructure.Models.Product>(model)	
		);
		dbContext.SaveChanges();

		UpdateCache(dbContext);
	}

	public void Update(Product model) {
		if(model is null)
			return;

		using var dbContext = _dbContextFactory.CreateDbContext();

		var modelDTO = _mapper.Map<Infrastructure.Models.Product>(model);
		dbContext.Products.Update(modelDTO);
		dbContext.SaveChanges();
		UpdateCache(dbContext);

		var key = _redisKey + modelDTO.Id.ToString();
		if(_redisService.GetObject<Product>(key) is not null)
			_redisService.SetObject(key, modelDTO);
	}

	public void Remove(Product model) {
		if(model is null)
			return;

		using var dbContext = _dbContextFactory.CreateDbContext();

		var modelDTO = _mapper.Map<Infrastructure.Models.Product>(model);
		dbContext.Products.Remove(modelDTO);
		dbContext.SaveChanges();
		UpdateCache(dbContext);

		_redisService.DistributedCache.Remove(_redisKey + modelDTO.Id.ToString());
	}



	public async Task<IEnumerable<Product>> GetAllAsync() {
		var cachedProducts = await _redisService.GetObjectAsync<List<Product>>(_redisKey);
		if(cachedProducts is not null)
			return cachedProducts;

		using var dbContext = await _dbContextFactory.CreateDbContextAsync();

		var productsList = _mapper.Map<IEnumerable<Product>>(await dbContext.Products.ToListAsync());
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

		using var dbContext = await _dbContextFactory.CreateDbContextAsync();

		var product = _mapper.Map<Product>(await dbContext.Products.FirstOrDefaultAsync(x => x.Id == id));
		if(product is null)
			return null;

		await _redisService.SetObjectAsync(key, product);
		return product;
	}


    public async Task AddAsync(Product model) {
		if(model is null)
			return;

		using var dbContext = await _dbContextFactory.CreateDbContextAsync();

		await dbContext.Products.AddAsync(
			_mapper.Map<Infrastructure.Models.Product>(model)
		);
		await dbContext.SaveChangesAsync();
		await UpdateCacheAsync(dbContext);
	}

	public async Task UpdateAsync(Product model) {
		if(model is null)
			return;

		using var dbContext = await _dbContextFactory.CreateDbContextAsync();

		var modelDTO = _mapper.Map<Infrastructure.Models.Product>(model);
		dbContext.Products.Update(modelDTO);
		await dbContext.SaveChangesAsync();
		await UpdateCacheAsync(dbContext);

		var key = _redisKey + modelDTO.Id.ToString();
		if(await _redisService.GetObjectAsync<Product>(key) is not null)
			await _redisService.SetObjectAsync(key, modelDTO);
	}

	public async Task RemoveAsync(Product model) {
		if(model is null)
			return;

		using var dbContext = await _dbContextFactory.CreateDbContextAsync();

		var modelDTO = _mapper.Map<Infrastructure.Models.Product>(model);
		dbContext.Products.Remove(modelDTO);
		await dbContext.SaveChangesAsync();
		await UpdateCacheAsync(dbContext);

		await _redisService.DistributedCache.RemoveAsync(_redisKey + modelDTO.Id.ToString());
	}


	private void UpdateCache(OnlineStoreDbContext dbContext) {
		if(_redisService.GetObject<List<UserRequests>>(_redisKey) is null)
			return;

		var productsList = dbContext.Products.ToList();
		if(productsList is null)
			return;

		_redisService.SetObject(_redisKey, productsList);
	}

	private async Task UpdateCacheAsync(OnlineStoreDbContext dbContext) {
		if(await _redisService.GetObjectAsync<List<UserRequests>>(_redisKey) is null)
			return;

		var productsList = await dbContext.Products.ToListAsync();
		if(productsList is null)
			return;

		await _redisService.SetObjectAsync(_redisKey, productsList);
	}
}