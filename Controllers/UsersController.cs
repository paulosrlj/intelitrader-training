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
    public ActionResult<IEnumerable<User>> GetUsers()
    {
      _logger.LogInformation("GET /api/users/");
      return _context.Users;
    }

    [HttpGet("{id}")]
    public ActionResult<User> GetUser(String id)
    {
      _logger.LogInformation("GET /api/users/{id}");

      var user = _context.Users.Find(id);

      if (user == null)
      {
        _logger.LogWarning("User does not exists");
        return NotFound();
      }

      // Console.WriteLine(user.ToString());
      _logger.LogInformation("User found: {user}", user.ToString());
      return user;
    }

    // api/users
    // Colocar informações da chamada
    [HttpPost]
    public ActionResult<User> CreateUser(User user)
    {
      _logger.LogInformation("POST /api/users/");
      _context.Users.Add(user);
      _context.SaveChanges();

      _logger.LogWarning("User created");
      return CreatedAtAction("GetUser", new User { id = user.id }, user);
    }

    // api/users/1
    [HttpPut("{id}")]
    public ActionResult<User> UpdateUser(String id, UpdateUserModel updateUserModel)
    {
      _logger.LogInformation("PUT /api/users/{id}");

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

      _logger.LogInformation("User successfuly updated");
      return CreatedAtAction("GetUser", new User { id = userFound.id }, userFound);
    }

    [HttpDelete("{id}")]
    public ActionResult<User> DeleteUser(String id)
    {
      _logger.LogInformation("DELETE /api/users/{id}");

      var user = _context.Users.Find(id);
      if (user == null) return NotFound();

      _context.Users.Remove(user);
      _context.SaveChanges();

      _logger.LogInformation("User successfuly deleted");
      return NoContent();
    }
  }
}