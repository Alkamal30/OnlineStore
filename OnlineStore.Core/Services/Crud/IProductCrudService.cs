using OnlineStore.Core.Models;

namespace OnlineStore.Core.Services.Crud;

public interface IProductCrudService : ICrud<Product>, ICrudAsync<Product> { }