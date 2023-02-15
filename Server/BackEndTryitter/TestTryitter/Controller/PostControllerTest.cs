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
    public class PostControllerTest
    {
        [Fact]
        public void GetPostById_Returns_OkObjectResult()
        {
            //Arrange
            Guid postId = Guid.NewGuid();
            Post post = new()
            {
                PostId = postId,
                UserId = Guid.NewGuid(),
                Images = new List<Image>() { new Image() { ImageId = Guid.NewGuid(), ImageUrl = "testUrl" } },
                User = new User(),
                Text = "testeText"
            };

            var mockPostRepository = new Mock<IPostRepository>();
            mockPostRepository.Setup(repo => repo.GetPostById(postId)).Returns(post);

            PostController controller = new(mockPostRepository.Object);

            //Act
            var result = controller.GetPostById(postId);

            //Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<Post>(okObjectResult.Value);

            Assert.Equal(postId, response.PostId);
            Assert.Equal("testeText", response.Text);
        }

        [Fact]
        public void GetPostByUsername_Returns_OkObjectResult()
        {
            //Arrange
            Guid postId = Guid.NewGuid();
            Post post = new()
            {
                PostId = postId,
                UserId = Guid.NewGuid(),
                Images = new List<Image>() { new Image() { ImageId = Guid.NewGuid(), ImageUrl = "testUrl" } },
                User = new User() { Username = "Teste" },
                Text = "testeText"
            };

            var mockPostRepository = new Mock<IPostRepository>();
            mockPostRepository.Setup(repo => repo.GetAllPostsByUsername("Teste")).Returns(new List<Post>() { post });

            PostController controller = new(mockPostRepository.Object);

            //Act
            var result = controller.GetPostsByUsername("Teste");

            //Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<List<Post>>(okObjectResult.Value);

            Assert.Equal(postId, response[0].PostId);
            Assert.Equal("testeText", response[0].Text);
        }

        [Fact]
        public void GetAllPosts_Returns_OkObjectResult()
        {
            //Arrange
            Guid postId = Guid.NewGuid();
            Post post = new()
            {
                PostId = postId,
                UserId = Guid.NewGuid(),
                Images = new List<Image>() { new Image() { ImageId = Guid.NewGuid(), ImageUrl = "testUrl" } },
                User = new User() { Username = "Teste" },
                Text = "testeText"
            };

            var mockPostRepository = new Mock<IPostRepository>();
            mockPostRepository.Setup(repo => repo.GetAllPosts()).Returns(new List<Post>() { post });

            PostController controller = new(mockPostRepository.Object);

            //Act
            var result = controller.GetAllPosts();

            //Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<List<Post>>(okObjectResult.Value);

            Assert.Equal(postId, response[0].PostId);
            Assert.Equal("testeText", response[0].Text);
        }
    }
}
