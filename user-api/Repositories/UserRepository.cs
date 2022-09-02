using user_api.database;
using user_api.Models;
using Microsoft.EntityFrameworkCore;

namespace user_api.Repositories;

public class UserRepository : IUserRepository<User>
{
  private readonly DbContext _context;

  public UserRepository(DbContext context)
  {
    _context = context;
  }

  public IEnumerable<User> GetAll()
  {
    var users = _context.Set<User>();
    return users;
  }
  public User? GetById(String id)
  {
    var user = _context.Find<User>(id);
    return user;
  }

  public void Add(User entity)
  {
    _context.Add<User>(entity);
    _context.SaveChanges();
  }

  public void Remove(User entity)
  {
    _context.Remove<User>(entity);
    _context.SaveChanges();
  }

  public void Update(User entity)
  {
    _context.Update<User>(entity);
    _context.SaveChanges();
  }
}