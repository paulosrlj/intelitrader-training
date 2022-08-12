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

    public UsersController(ApplicationDbContext context)
    {
      _context = context;
    }


    [HttpGet]
    public ActionResult<IEnumerable<User>> GetUsers()
    {
      return _context.Users;
    }

    [HttpGet("{id}")]
    public ActionResult<User> GetUser(String id)
    {
      var user = _context.Users.Find(id);

      if (user == null) return NotFound();

      return user;
    }

    // api/users
    [HttpPost]
    public ActionResult<User> CreateUser(User user)
    {
      _context.Users.Add(user);
      _context.SaveChanges();

      return CreatedAtAction("GetUser", new User { id = user.id }, user);
    }

    // api/users/1
    [HttpPut("{id}")]
    public ActionResult<User> UpdateUser(String id, UpdateUserModel updateUserModel)
    {
      var userFound = _context.Users.Find(id);
      if (userFound == null) return NotFound();

      if (updateUserModel.firstName != null) {
        userFound.firstName = updateUserModel.firstName;
      }

      if (updateUserModel.surname != null) {
        userFound.surname = updateUserModel.surname;
      }

      if (updateUserModel.age != null) {
        userFound.age = updateUserModel.age.GetValueOrDefault();
      }

      _context.Users.Update(userFound);

      // _context.Entry(userFound).State = EntityState.Modified;
      _context.SaveChanges();

      return CreatedAtAction("GetUser", new User { id = userFound.id }, userFound);
    }

    [HttpDelete("{id}")]
    public ActionResult<User> DeleteUser(String id) {
      var user = _context.Users.Find(id);
      if (user == null) return NotFound();

      _context.Users.Remove(user);
      _context.SaveChanges();

      return NoContent();
    }
  }
}