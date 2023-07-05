using OnlineStore.Core.Models;

namespace OnlineStore.Core.Services.Crud;

public interface IUserCrudService : ICrud<User>, ICrudAsync<User> {

	Task<User?> GetByLoginAsync(string login);
}