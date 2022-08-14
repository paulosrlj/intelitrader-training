using System.Net;
using Xunit;
using Xunit.Abstractions;
using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using user_api.Controllers;
using user_api.Models;
using user_api.database;
using Test.MockData;

namespace Test.Controllers;



public class UsersControllerTest : IDisposable
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

  public void Dispose() {
    _context.Database.EnsureDeleted();
    _context.Dispose();
  }

  [Fact]
  public void MustGetAllUsersAndReturn200()
  {
    // Arrange
    _context.Users.AddRange(UserMockData.GetUsers());
    _context.SaveChanges();
    // var userRepository = new Mock<ApplicationDbContext>();
    // userRepository.Setup(_ => _.Users).Returns(UserMockData.GetUsers().Object);

    var logMock = new Mock<ILogger<User>>();

    var sut = new UsersController(_context, logMock.Object);

    // Act
    var result = sut.GetUsers() as OkObjectResult;
   
    // Assert  
    result.Value.Should().BeEquivalentTo(UserMockData.GetUsers());
    result.GetType().Should().Be(typeof(OkObjectResult));
    result.Should().BeOfType<OkObjectResult>()
    .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
  }
}