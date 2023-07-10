using OnlineStore.Core.Abstractions.Models;

namespace OnlineStore.Core.Abstractions.Services.Crud;

public interface IProductCrudService : ICrud<Product>, ICrudAsync<Product> { }