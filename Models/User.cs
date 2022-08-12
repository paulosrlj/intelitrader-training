using System.ComponentModel.DataAnnotations.Schema;
namespace user_api.Models;


public class User
{

  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public string id { get; set; }
  public string firstName { get; set; } = string.Empty;
  public string? surname { get; set; } = string.Empty;
  public int age { get; set; }
  public DateTime creationDate { get; set; }


  public User() {
    this.id = Guid.NewGuid().ToString();
    this.creationDate = DateTime.UtcNow;
  }
}
