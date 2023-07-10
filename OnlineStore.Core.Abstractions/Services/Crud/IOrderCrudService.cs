using OnlineStore.Core.Abstractions.Models;

namespace OnlineStore.Core.Abstractions.Services.Crud;

public interface IOrderCrudService : ICrud<Order>, ICrudAsync<Order> {

	IEnumerable<Order> GetByUserLogin(string userLogin);

	Task<IEnumerable<Order>> GetByUserLoginAsync(string userLogin);
}
