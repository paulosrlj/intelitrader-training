namespace user_api.Repositories;

public interface IUserRepository<T> where T : class {
  IEnumerable<T> GetAll();
  T? GetById(String id);

  void Add(T entity);

  void Remove(T entity);

  void Update(T entity);
}
