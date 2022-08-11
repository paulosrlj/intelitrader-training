using Microsoft.AspNetCore.Mvc;
using user_api.database;
using user_api.Models;

namespace user_api.Controllers {
  
  [Route("api/[controller]")]
  [ApiController]
  public class UsersController : ControllerBase {

    private readonly ApplicationDbContext _context;

    public UsersController(ApplicationDbContext context) {
      _context = context;
    }


    [HttpGet]
    public ActionResult<IEnumerable<User>> GetUsers() {
      return _context.Users;
    }

    [HttpGet("/{id}")]
    public ActionResult<User> GetUser(int id) {
      var user = _context.Users.Find(id);

      if (user == null) return NotFound();
    
      return user;
    }
  }
}