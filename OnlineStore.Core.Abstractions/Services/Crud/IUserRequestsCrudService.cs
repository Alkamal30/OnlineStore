using OnlineStore.Core.Abstractions.Models;

namespace OnlineStore.Core.Abstractions.Services.Crud;

public interface IUserRequestsCrudService : ICrud<UserRequests>, ICrudAsync<UserRequests> {

	public UserRequests? GetByLogin(string login);
	public Task<UserRequests?> GetByLoginAsync(string login);
}