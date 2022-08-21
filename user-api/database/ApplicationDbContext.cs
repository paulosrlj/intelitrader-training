using Microsoft.EntityFrameworkCore;
using user_api.Models;


namespace user_api.database
{
  public class ApplicationDbContext : DbContext
  {

    public ApplicationDbContext() {}

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public virtual DbSet<User> Users { get; set; }
  }
}