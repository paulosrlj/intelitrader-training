using Microsoft.AspNetCore.Mvc;
using user_api.Models;
using user_api.Services;


namespace user_api.Controllers
{

  [Route("api/[controller]")]
  [ApiController]
  public class UsersController : Controller
  {
    private readonly ILogger _logger;
    private readonly IUserService<User> _userService;

    public UsersController(
    ILogger<User> logger,
    IUserService<User> userService)
    {
      _logger = logger;
      _userService = userService;
    }

    [HttpGet]
    public IActionResult GetUsers()
    {
      _logger.LogInformation("GET /api/users/");
      var users = _userService.GetAll();
      if (users.Count() == 0) return NoContent();

      return Ok(users);
    }

    [HttpGet("{id}")]
    public IActionResult GetUser(String id)
    {
      _logger.LogInformation("GET /api/users/{0}", id);

      var user = _userService.GetById(id);

      if (user == null) return NotFound();

      return Ok(user);
    }

    // api/users
    [HttpPost]
    public IActionResult CreateUser(User user)
    {
      _logger.LogInformation("POST /api/users/");
      _logger.LogInformation("Request body: {0}\n", user);

      _userService.Add(user);

      return CreatedAtAction("GetUser", new User { id = user.id }, user);
    }

    // api/users/1
    [HttpPut("{id}")]
    public IActionResult UpdateUser(String id, UpdateUserModel updateUserModel)
    {
      _logger.LogInformation("PUT /api/users/{id}", id);
      _logger.LogInformation("Request body: {0}\n", updateUserModel);

      _userService.Update(id, updateUserModel);

      // return CreatedAtAction("GetUser", new User { id = userFound.id }, userFound);
      return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(String id)
    {
      _logger.LogInformation("DELETE /api/users/{0}", id);

      _userService.Remove(id);

      return NoContent();
    }
  }
}