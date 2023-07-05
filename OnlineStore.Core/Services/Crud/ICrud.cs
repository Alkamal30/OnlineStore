namespace OnlineStore.Core.Services.Crud;

public interface ICrud<T> {

    IEnumerable<T> GetAll();
    T? GetById(int id);

    void Add(T model);
    void Update(T model);
    void Remove(T model);
}