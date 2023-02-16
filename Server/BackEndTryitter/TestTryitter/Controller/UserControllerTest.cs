using BackEndTryitter.Contracts.User;
using BackEndTryitter.Controllers;
using BackEndTryitter.Models;
using BackEndTryitter.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTryitter.Controller
{
    public class UserControllerTest
    {
        [Fact]
        public void GetUserById_Returns_OkObjectResult_With_Correct_Response()
        {
            //Arrange
            Guid userId = Guid.NewGuid();
            User user = new()
            {
                UserId = userId,
                FullName = "Test User",
                Username = "testUser",
                StatusMessage = "",
                Email = "test@test.com",
                CurrentModule = 1,
                Password = "123456",
            };

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(repo => repo.GetUserById(userId)).Returns(user);

            UserController controller = new(mockUserRepository.Object);

            //Act
            var result = controller.GetUserById(userId);

            //Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<GetUserByIdResponse>(okObjectResult.Value);

            Assert.Equal(userId, response.Id);
            Assert.Equal("Test User", response.FullName);
            Assert.Equal("testUser", response.Username);
        }

        [Fact]
        public void GetUserById_Returns_NotFound()
        {
            //Arrange
            Guid userId = new();
            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(repo => repo.GetUserById(userId));

            UserController controller = new(mockUserRepository.Object);

            //Act
            var result = controller.GetUserById(userId);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void FindUsersByUsername_Returns_OkObjectResult_With_Correct_Response()
        {
            //Arrange
            Guid userId = Guid.NewGuid();
            User user = new()
            {
                UserId = userId,
                FullName = "Test User",
                Username = "testUser",
                StatusMessage = "",
                Email = "test@test.com",
                CurrentModule = 1,
                Password = "123456",
            };

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(repo => repo.FindUsersByUsername("testUser")).Returns(new List<User>() { user });

            UserController controller = new(mockUserRepository.Object);

            //Act
            var result = controller.FindUsersByUsername("testUser");

            //Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var response = okObjectResult.Value as IEnumerable<GetUserByIdResponse>;

            Assert.Equal(userId, response.First().Id);
        }
    }
}
