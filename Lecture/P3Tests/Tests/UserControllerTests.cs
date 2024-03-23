using Microsoft.AspNetCore.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using P3.Models;
using System.Net;

namespace P3Tests.Tests
{
    [TestClass]
    public class UserControllerTests
    {
        private APIBuilder _apiBuilder;

        [TestInitialize]
        public void Setup()
        {
            _apiBuilder = new APIBuilder();
        }

        [TestMethod]
        public async Task GetUsers_ReturnsListOfUsers()
        {
            // Act
            var response = await _apiBuilder.GetRequest<List<User>>("api/users");

            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(2, response.Count);
        }
        [TestMethod]
        public async Task GetUser_ExistingId_ReturnsUser()
        {
            // Arrange
            int userId = 1;

            // Act
            var response = await _apiBuilder.GetRequest<User>($"api/users/{userId}");

            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(userId, response.Id);            
        }

        [TestMethod]
        public async Task GetUser_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            int nonExistingUserId = 999;

            // Act
            var response = await _apiBuilder.GetRequestReturnResponse($"api/users/{nonExistingUserId}");

            // Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);            
        }

        [TestMethod]
        public async Task CreateUser_ValidUser_ReturnsCreatedAtAction()
        {
            // Arrange
            var newUser = new User { Id = 3, Name = "Jane" };

            // Act
            await _apiBuilder.PostRequest("api/users", newUser);
            var actualUser = await _apiBuilder.GetRequest<User>($"api/users/{newUser.Id}");

            // Assert
            Assert.AreEqual(newUser.Id, actualUser.Id);
            Assert.AreEqual(newUser.Name,actualUser.Name);
        }

        [TestMethod]
        public async Task UpdateUser_ExistingId_ReturnsNoContent()
        {
            // Arrange
            int userId = 1;
            var updatedUser = new User { Id = userId, Name = "UpdatedName" };

            // Act
            var response = await _apiBuilder.PutRequest($"api/users/{userId}", updatedUser);
            var actualUser = await _apiBuilder.GetRequest<User>($"api/users/{updatedUser.Id}");

            // Assert
            Assert.AreEqual(updatedUser.Name, actualUser.Name);            
        }

        [TestMethod]
        public async Task UpdateUser_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            int nonExistingUserId = 999;
            var updatedUser = new User { Id = nonExistingUserId, Name = "UpdatedName" };

            // Act
            var response = await _apiBuilder.PuttRequestReturnResponse($"api/users/{nonExistingUserId}", updatedUser);

            // Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);
        }

        [TestMethod]
        public async Task DeleteUser_ExistingId_ReturnsNoContent()
        {
            // Arrange
            int userId = 1;
            int expectedUsersCount = 1;
            // Act
            await _apiBuilder.DeleteRequest($"api/users/{userId}");
            var users = await _apiBuilder.GetRequest<List<User>>("api/users");

            // Assert
            Assert.AreEqual(expectedUsersCount, users.Count);            
        }

        [TestMethod]
        public async Task DeleteUser_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            int nonExistingUserId = 999;

            // Act
            var response = await _apiBuilder.DeleteRequestReturnResponse($"api/users/{nonExistingUserId}");

            // Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NotFound);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _apiBuilder.Dispose();
        }

    }
}
