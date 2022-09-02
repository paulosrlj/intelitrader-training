using System.Net;
using Xunit;
using Xunit.Abstractions;
using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using user_api.Services;
using user_api.Controllers;
using user_api.Models;
using user_api.database;
using Test.MockData;

namespace Test.Controllers;

public class UsersControllerTest
{

  private readonly ApplicationDbContext _context;
  private readonly ITestOutputHelper _testOutputHelper;
  public UsersControllerTest(ITestOutputHelper testOutputHelper)
  {
    var options = new DbContextOptionsBuilder<ApplicationDbContext>()
    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

    _context = new ApplicationDbContext(options);
    _testOutputHelper = testOutputHelper;

    _context.Database.EnsureCreated();
  }

  public void Dispose()
  {
    _context.Database.EnsureDeleted();
    _context.Dispose();
  }

  [Fact]
  public void MustGetAllUsersAndReturn200()
  {
    // Arrange
    _context.Users.AddRange(UserMockData.GetUsers());
    _context.SaveChanges();

    var logMock = new Mock<ILogger<User>>();
    var serviceMock = new Mock<IUserService<User>>();
    serviceMock.Setup(s => s.GetAll()).Returns(UserMockData.GetUsers());

    var sut = new UsersController(logMock.Object, serviceMock.Object);

    // Act
    var result = sut.GetUsers() as OkObjectResult;

    // Assert  
    result.Value.Should().BeEquivalentTo(UserMockData.GetUsers());
    result.GetType().Should().Be(typeof(OkObjectResult));
    result.Should().BeOfType<OkObjectResult>()
    .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
  }

  [Fact]
  public void MustReturn204IfNoUsersAreFound()
  {
    // Arrange

    var logMock = new Mock<ILogger<User>>();
    var serviceMock = new Mock<IUserService<User>>();
    serviceMock.Setup(s => s.GetAll()).Returns(new List<User>());

    var sut = new UsersController(logMock.Object, serviceMock.Object);

    // Act
    var result = sut.GetUsers();

    // Assert  
    result.Should().BeOfType<NoContentResult>()
    .Which.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
  }

  [Fact]
  public void MustReturn404IfASingleUserIsNotFound()
  {
    // Arrange
    _context.Users.AddRange(UserMockData.GetUsers());
    _context.SaveChanges();

    var logMock = new Mock<ILogger<User>>();
    var serviceMock = new Mock<IUserService<User>>();
    serviceMock.Setup(s => s.GetById(It.IsAny<String>())).Returns<User>(null);

    var sut = new UsersController(logMock.Object, serviceMock.Object);

    // Act
    var result = sut.GetUser(Guid.NewGuid().ToString()) as NotFoundResult;

    // Assert  
    result.Should().BeOfType<NotFoundResult>()
    .Which.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
  }

  [Fact]
  public void MustReturn200IfASingleUserIsFound()
  {
    // Arrange
    _context.Users.AddRange(UserMockData.GetUsers());
    _context.SaveChanges();

    var logMock = new Mock<ILogger<User>>();
    var serviceMock = new Mock<IUserService<User>>();
    var user = new User
    {
      id = "6db8af3f-f20b-4ade-95f9-245094719c33",
      firstName = "Lilia",
      age = 19,
      creationDate = DateTime.Parse("2022-08-12T00:32:01.355183")
    };

    serviceMock.Setup(s => s.GetById(It.IsAny<String>())).Returns(user);

    var sut = new UsersController(logMock.Object, serviceMock.Object);

    // Act
    var result = sut.GetUser("6db8af3f-f20b-4ade-95f9-245094719c33") as OkObjectResult;

    // Assert  
    result.Value.Should().BeEquivalentTo(user);
    result.Should().BeOfType<OkObjectResult>()
    .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
  }

  [Fact]
  public void MustReturn200IfAUserIsSuccessfullyUpdated()
  {
    // Arrange
    _context.Users.AddRange(UserMockData.GetUsers());
    _context.SaveChanges();
    var logMock = new Mock<ILogger<User>>();
    var serviceMock = new Mock<IUserService<User>>();

    var dataToUpdate = new UpdateUserModel { firstName = "Lillia Santos" };
    var expectedData = new User
    {
      id = "6db8af3f-f20b-4ade-95f9-245094719c33",
      firstName = "Lillia Santos",
      age = 19,
      creationDate = DateTime.Parse("2022-08-12T00:32:01.355183")
    };

    var sut = new UsersController(logMock.Object, serviceMock.Object);

    // Act
    var result = sut.UpdateUser("6db8af3f-f20b-4ade-95f9-245094719c33", dataToUpdate)
    as NoContentResult;

    // Assert  
    result.Should().BeOfType<NoContentResult>();

    result.Should().BeOfType<NoContentResult>()
    .Which.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
  }

  [Fact]
  public void MustReturn404IfAUserIsNotFoundOnUpdate()
  {
    // Arrange
    _context.Users.AddRange(UserMockData.GetUsers());
    _context.SaveChanges();
    var logMock = new Mock<ILogger<User>>();
    var serviceMock = new Mock<IUserService<User>>();

    var dataToUpdate = new UpdateUserModel { firstName = "Lillia Santos" };
    var expectedData = new User
    {
      id = "6db8af3f-f20b-4ade-95f9-245094719c33",
      firstName = "Lillia Santos",
      age = 19,
      creationDate = DateTime.Parse("2022-08-12T00:32:01.355183")
    };

    var sut = new UsersController(logMock.Object, serviceMock.Object);
    // Act
    var result = sut.UpdateUser("6db8af3f-f20b-4ade-95f9-125769122", dataToUpdate)
    as NoContentResult;

    // Assert  
    result.Should().BeOfType<NoContentResult>()
    .Which.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
  }

  [Fact]
  public void MustReturn200IfAUserIsSuccessfullyCreated()
  {
    // Arrange
    var logMock = new Mock<ILogger<User>>();
    var serviceMock = new Mock<IUserService<User>>();
    var dataToCreate = new User
    {
      firstName = "Lillia Santos",
      age = 19,
    };

    var expectedData = new User
    {
      id = dataToCreate.id,
      firstName = "Lillia Santos",
      age = 19,
      creationDate = dataToCreate.creationDate
    };

    var sut = new UsersController(logMock.Object, serviceMock.Object);

    // Act
    var result = sut.CreateUser(dataToCreate) as CreatedAtActionResult;

    // Assert  
    result.Value.Should().BeEquivalentTo(expectedData);
    result.Should().BeOfType<CreatedAtActionResult>()
    .Which.StatusCode.Should().Be((int)HttpStatusCode.Created);
  }

  [Fact]
  public void MustReturn204IfUserIsSuccessfullyDeleted()
  {
    // Arrange
    _context.Users.AddRange(UserMockData.GetUsers());
    _context.SaveChanges();
    var logMock = new Mock<ILogger<User>>();
    var serviceMock = new Mock<IUserService<User>>();
    var sut = new UsersController(logMock.Object, serviceMock.Object);

    // Act
    var result = sut.DeleteUser("6db8af3f-f20b-4ade-95f9-245094719c33") as NoContentResult;

    // Assert  
    result.Should().BeOfType<NoContentResult>()
    .Which.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
  }

  [Fact]
  public void MustReturn404IfUserIsNotFoundOnDelete()
  {
    // Arrange
    _context.Users.AddRange(UserMockData.GetUsers());
    _context.SaveChanges();
    var logMock = new Mock<ILogger<User>>();

    var serviceMock = new Mock<IUserService<User>>();
    var sut = new UsersController(logMock.Object, serviceMock.Object);

    // Act
    var result = sut.DeleteUser("6db8af3f-f20b-4ade-95f9-7256262633") as NoContentResult;

    // Assert  
    result.Should().BeOfType<NoContentResult>()
    .Which.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
  }
}
