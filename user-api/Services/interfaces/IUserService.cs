using user_api.Models;

namespace user_api.Services;

public interface IUserService<T> {
  IEnumerable<T> GetAll();
  T? GetById(String id);

  void Add(T entity);

  void Remove(String id);

  void Update(String id, UpdateUserModel userModel);

  Task RetrieveHubUsers();
}
