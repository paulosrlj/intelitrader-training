using System.Net;
using Xunit;
using Xunit.Abstractions;
using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

using user_api.Controllers;
using user_api.Models;
using user_api.database;
using Test.MockData;

namespace Test.Controllers;



public class UsersControllerTest
{

  private readonly ITestOutputHelper _testOutputHelper;
  public UsersControllerTest(ITestOutputHelper testOutputHelper)
  {
    _testOutputHelper = testOutputHelper;
  }

  [Fact]
  public void MustGetAllUsersAndReturn200()
  {
    // Arrange
    var userRepository = new Mock<ApplicationDbContext>();
    userRepository.Setup(_ => _.Users).Returns(UserMockData.GetUsers().Object);

    var logMock = new Mock<ILogger<User>>();

    var sut = new UsersController(userRepository.Object, logMock.Object);

    // Act
    var result = sut.GetUsers();
   
    // Assert  
    result.GetType().Should().Be(typeof(OkObjectResult));
    result.Should().BeOfType<OkObjectResult>()
    .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
  }
}