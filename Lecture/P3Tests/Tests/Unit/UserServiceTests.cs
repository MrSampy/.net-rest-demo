using Moq;
using P3.Interfaces;
using P3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3Tests.Tests.Unit
{
    [TestClass]
    public class UserServiceTests
    {
        private Mock<IUserService> _userServiceMock;
        [TestInitialize]
        public void Initialize()
        {
            _userServiceMock = new Mock<IUserService>();
        }

        [TestMethod]
        public void GetUsers_ReturnsListOfUsers()
        {
            // Arrange
            var expectedUsers = new List<User>
            {
                new User { Id = 1, Name = "John" },
                new User { Id = 2, Name = "Alice" }
            };
            _userServiceMock.Setup(service => service.GetUsers()).Returns(expectedUsers);

            // Act
            var actualUsers = _userServiceMock.Object.GetUsers();

            // Assert
            CollectionAssert.AreEqual(expectedUsers, actualUsers);
        }

        [TestMethod]
        public void Update_CallsUpdateMethodWithCorrectUser()
        {
            // Arrange
            var userToUpdate = new User { Id = 1, Name = "UpdatedName" };

            // Act
            _userServiceMock.Object.Update(userToUpdate);

            // Assert
            _userServiceMock.Verify(service => service.Update(userToUpdate), Times.Once);
        }

        [TestMethod]
        public void Delete_CallsDeleteMethodWithCorrectId()
        {
            // Arrange
            int userIdToDelete = 1;

            // Act
            _userServiceMock.Object.Delete(userIdToDelete);

            // Assert
            _userServiceMock.Verify(service => service.Delete(userIdToDelete), Times.Once);
        }
    }
}
