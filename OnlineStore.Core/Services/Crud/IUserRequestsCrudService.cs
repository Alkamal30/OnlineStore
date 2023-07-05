using OnlineStore.Core.Models;

namespace OnlineStore.Core.Services.Crud;

public interface IUserRequestsCrudService : ICrud<UserRequests>, ICrudAsync<UserRequests> {

	public UserRequests? GetByLogin(string login);
	public Task<UserRequests?> GetByLoginAsync(string login);
}