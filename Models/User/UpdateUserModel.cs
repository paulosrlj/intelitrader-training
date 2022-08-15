namespace user_api.Models;


public class UpdateUserModel
{

  public string? firstName { get; set; }
  public string? surname { get; set; }
  public int? age { get; set; }

  public override string ToString()
  {
    return String.Format("User: \n[firstName: {0}\nsurname: {1}\nage: {2}\n]",
    firstName, surname, age);
  }
}
