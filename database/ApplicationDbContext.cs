using Microsoft.EntityFrameworkCore;

namespace user_api.database {
  public class ApplicationDbContext: DbContext {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) {}
  }
}