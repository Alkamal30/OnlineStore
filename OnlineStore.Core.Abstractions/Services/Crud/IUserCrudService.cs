using OnlineStore.Core.Abstractions.Models;

namespace OnlineStore.Core.Abstractions.Services.Crud;

public interface IUserCrudService : ICrud<User>, ICrudAsync<User> {

	Task<User?> GetByLoginAsync(string login);
}