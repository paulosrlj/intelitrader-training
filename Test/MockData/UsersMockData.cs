using user_api.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
namespace Test.MockData;

public class UserMockData
{

  public static List<User> GetUsers()
  {

    var data = new List<User>
    {
      new User
      {
        id = "8eec7eec-c5e2-41aa-9979-b6ace994ab9d",
        firstName = "Paulo",
        age = 21,
        creationDate = DateTime.Parse("2022-08-12T15:57:43.804586")
      },
      new User
      {
        id = "6db8af3f-f20b-4ade-95f9-245094719c33",
        firstName = "Lilia",
        age = 19,
        creationDate = DateTime.Parse("2022-08-12T00:32:01.355183")
      },
      new User
      {
        id = "46a00ffa-abec-4c3f-9ab7-502578eb4f99",
        firstName = "James",
        surname = "Rocket",
        age = 30,
        creationDate = DateTime.Parse("2022-08-12T00:31:11.461767")
      }
  };

    // var mockSet = new Mock<DbSet<User>>();
    // mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
    // mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
    // mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
    // mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

    return data;
  }

}