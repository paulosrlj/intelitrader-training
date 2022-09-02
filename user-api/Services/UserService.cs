
using user_api.Models;
using user_api.Repositories;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace user_api.Services;

public class UserService : Hub, IUserService<User>
{
  private IUserRepository<User> _userRepository;

  private readonly ILogger<UserService> _logger;

  public UserService(ILogger<UserService> logger,
  IUserRepository<User> userRepository)
  {
    _logger = logger;
    _userRepository = userRepository;
  }

  public IEnumerable<User> GetAll()
  {
    var users = _userRepository.GetAll();
    _logger.LogInformation("Users found: {0}", users.Count());
    return users;
  }
  public User? GetById(String id)
  {
    var user = _userRepository.GetById(id);
    if (user == null)
    {
      _logger.LogWarning("User does not exists");
    }
    else
    {
      _logger.LogInformation("User found: {user}", user.ToString());
    }

    return user;
  }

  public void Add(User entity)
  {
    _userRepository.Add(entity);
    _logger.LogWarning("User successfully created: {0}", entity);
  }

  public void Remove(String id)
  {
    var user = _userRepository.GetById(id);
    if (user != null)
    {
      _userRepository.Remove(user);
      _logger.LogInformation("User successfuly deleted");
    }
    else
    {
      _logger.LogInformation("User not found");
    }
  }

  public void Update(String id, UpdateUserModel userModel)
  {
    var user = _userRepository.GetById(id);
    if (user != null)
    {
      if (userModel.firstName != null)
      {
        user.firstName = userModel.firstName;
      }

      if (userModel.surname != null)
      {
        user.surname = userModel.surname;
      }

      if (userModel.age != null)
      {
        user.age = userModel.age.GetValueOrDefault();
      }

      _userRepository.Update(user);
      _logger.LogWarning("User successfully updated: {0}", user);
    }
    else
    {
      _logger.LogInformation("User not found");
    }
  }

  public async Task RetrieveHubUsers()
  {
    var users = await Task.FromResult<IEnumerable<User>>(GetAll());
    await Clients.All.SendAsync("Users", JsonSerializer.Serialize(users));
  }
}