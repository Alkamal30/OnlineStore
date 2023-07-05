using OnlineStore.Core.Models;

namespace OnlineStore.Core.Services.Crud;

public interface IOrderCrudService : ICrud<Order>, ICrudAsync<Order> {

	IEnumerable<Order> GetByUserLogin(string userLogin);

	Task<IEnumerable<Order>> GetByUserLoginAsync(string userLogin);
}
