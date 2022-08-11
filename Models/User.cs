namespace user_api.Models;


public class User
{

  public string id { get; set; } = string.Empty;
  public string firstName { get; set; } = string.Empty;
  public string? surname { get; set; } = string.Empty;
  public int age { get; set; }
  public DateTime creationDate { get; set; }


  public User() {
    this.creationDate = DateTime.UtcNow;
  }
}
