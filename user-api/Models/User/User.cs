using System.ComponentModel.DataAnnotations;
namespace user_api.Models;


public class User
{
  public string id { get; set; }
  [RequiredAttribute(ErrorMessage = "O nome é necessário")]
  public string firstName { get; set; } = string.Empty;

  public string? surname { get; set; } = string.Empty;

  [RequiredAttribute]
  [Range(1, int.MaxValue, ErrorMessage = "Valor inválido")]
  public int age { get; set; } = -1;
  public DateTime creationDate { get; set; }


  public User()
  {
    this.id = Guid.NewGuid().ToString();
    this.creationDate = DateTime.UtcNow;
  }

  public override string ToString()
  {
    return String.Format("User: \n[id: {0}\nfirstName: {1}\nsurname: {2}\nage: {3}\ncreationDate:{4}\n]",
    id, firstName, surname, age, creationDate);
  }

}
