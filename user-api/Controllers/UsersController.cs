using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using user_api.database;
using user_api.Models;
namespace user_api.Controllers
{

  [Route("api/[controller]")]
  [ApiController]
  public class UsersController : ControllerBase
  {
    private readonly ApplicationDbContext _context;
    private readonly ILogger _logger;

    public UsersController(ApplicationDbContext context, ILogger<User> logger)
    {
      _context = context;
      _logger = logger;
    }


    [HttpGet]
    public IActionResult GetUsers()
    {
      _logger.LogInformation("GET /api/users/");
      var users = _context.Users;

      if (users.Count() == 0) return NoContent();

      _logger.LogInformation("Users found: {0}", users.Count());
      return Ok(users);
    }

    [HttpGet("{id}")]
    public IActionResult GetUser(String id)
    {
      _logger.LogInformation("GET /api/users/{0}", id);

      var user = _context.Users.Find(id);

      if (user == null)
      {
        _logger.LogWarning("User does not exists");
        return NotFound();
      }

      _logger.LogInformation("User found: {user}", user.ToString());
      return Ok(user);
    }

    // api/users
    [HttpPost]
    public IActionResult CreateUser(User user)
    {
      _logger.LogInformation("POST /api/users/");
      _logger.LogInformation("Request body: {0}\n", user);

      _context.Users.Add(user);
      _context.SaveChanges();

      _logger.LogWarning("User successfully created: {0}", user);
      return CreatedAtAction("GetUser", new User { id = user.id }, user);
    }

    // api/users/1
    [HttpPut("{id}")]
    public IActionResult UpdateUser(String id, UpdateUserModel updateUserModel)
    {
      _logger.LogInformation("PUT /api/users/{id}", id);
      _logger.LogInformation("Request body: {0}\n", updateUserModel);


      var userFound = _context.Users.Find(id);
      if (userFound == null)
      {
        _logger.LogWarning("User does not exists");
        return NotFound();
      }

      if (updateUserModel.firstName != null)
      {
        userFound.firstName = updateUserModel.firstName;
      }

      if (updateUserModel.surname != null)
      {
        userFound.surname = updateUserModel.surname;
      }

      if (updateUserModel.age != null)
      {
        userFound.age = updateUserModel.age.GetValueOrDefault();
      }

      _context.Users.Update(userFound);
      _context.SaveChanges();

      _logger.LogWarning("User successfully updated: {0}", userFound);

      return CreatedAtAction("GetUser", new User { id = userFound.id }, userFound);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(String id)
    {
      _logger.LogInformation("DELETE /api/users/{0}", id);

      var user = _context.Users.Find(id);
      if (user == null) return NotFound();

      _context.Users.Remove(user);
      _context.SaveChanges();

      _logger.LogInformation("User successfuly deleted");
      return NoContent();
    }
  }
}