namespace OnlineStore.Core.Services.Crud;

public interface ICrudAsync<T> {
	Task<IEnumerable<T>> GetAllAsync();
	Task<T?> GetByIdAsync(int id);

	Task AddAsync(T model);
	Task UpdateAsync(T model);
	Task RemoveAsync(T model);
}