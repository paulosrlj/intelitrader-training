using Microsoft.EntityFrameworkCore;
using user_api.Models;

namespace user_api.database
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
  }
}