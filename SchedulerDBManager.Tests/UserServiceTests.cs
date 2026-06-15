using Moq;
using SchedulerDBManager.BusinessLogic.Services;
using SchedulerDBManager.DataAccess.Models;
using SchedulerDBManager.DataAccess.Repositories;
using Xunit;

namespace SchedulerDBManager.Tests;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepoMock;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _userRepoMock = new Mock<IUserRepository>();
        _userService = new UserService(_userRepoMock.Object);
    }

    #region CreateUser Tests

    [Fact]
    public void CreateUser_ValidUser_SavesToRepository()
    {
        // Arrange
        var user = new User { Login = "newUser", Password = "password", Role = 1 };
        _userRepoMock.Setup(r => r.GetByLogin(user.Login)).Returns((User)null!);

        // Act
        _userService.CreateUser(user);

        // Assert
        _userRepoMock.Verify(r => r.Add(It.Is<User>(u => u.Login == "newUser")), Times.Once);
    }

    [Fact]
    public void CreateUser_NullUser_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _userService.CreateUser(null!));
    }

    [Theory]
    [InlineData("", "password", 1)]
    [InlineData("  ", "password", 1)]
    [InlineData("user", "", 1)]
    [InlineData("user", "123", 1)] // слишком короткий пароль
    [InlineData("user", "password", 0)] // некорректна€ роль (меньше 1)
    [InlineData("user", "password", 4)] // некорректна€ роль (больше 3)
    public void CreateUser_InvalidData_ThrowsArgumentException(string login, string password, int role)
    {
        // Arrange
        var user = new User { Login = login, Password = password, Role = role };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _userService.CreateUser(user));
    }

    [Fact]
    public void CreateUser_DuplicateLogin_ThrowsException()
    {
        // Arrange
        var user = new User { Login = "existingUser", Password = "password", Role = 1 };
        _userRepoMock.Setup(r => r.GetByLogin("existingUser")).Returns(new User());

        // Act & Assert
        var exception = Assert.Throws<Exception>(() => _userService.CreateUser(user));
        Assert.Equal("ѕользователь с таким логином уже существует.", exception.Message);
    }

    [Fact]
    public void CreateUser_TrimsLoginBeforeSaving()
    {
        // Arrange
        var user = new User { Login = "  trimmedUser  ", Password = "password", Role = 1 };
        _userRepoMock.Setup(r => r.GetByLogin("trimmedUser")).Returns((User)null!);

        // Act
        _userService.CreateUser(user);

        // Assert
        Assert.Equal("trimmedUser", user.Login);
    }

    #endregion

    #region GetAllUsers Tests

    [Fact]
    public void GetAllUsers_ReturnsAllUsersFromRepository()
    {
        // Arrange
        var expectedUsers = new List<User>
        {
            new() { UserId = 1, Login = "user1" },
            new() { UserId = 2, Login = "user2" }
        };
        _userRepoMock.Setup(r => r.GetAll()).Returns(expectedUsers);

        // Act
        var result = _userService.GetAllUsers().ToList();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal("user1", result[0].Login);
        _userRepoMock.Verify(r => r.GetAll(), Times.Once);
    }

    #endregion

    #region UpdateUser Tests

    [Fact]
    public void UpdateUser_ValidUser_UpdatesInRepository()
    {
        // Arrange
        var user = new User { UserId = 1, Login = "updatedUser", Password = "password", Role = 2 };

        // Act
        _userService.UpdateUser(user);

        // Assert
        _userRepoMock.Verify(r => r.Update(It.Is<User>(u => u.Login == "updatedUser")), Times.Once);
    }

    [Fact]
    public void UpdateUser_InvalidUser_ThrowsArgumentException()
    {
        // Arrange
        var invalidUser = new User { UserId = 1, Login = "", Password = "password", Role = 2 }; // пустой логин

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _userService.UpdateUser(invalidUser));
        _userRepoMock.Verify(r => r.Update(It.IsAny<User>()), Times.Never);
    }

    #endregion

    #region RemoveUser Tests

    [Fact]
    public void RemoveUser_CallsRepositoryDelete()
    {
        // Arrange
        int userIdToRemove = 42;

        // Act
        _userService.RemoveUser(userIdToRemove);

        // Assert
        _userRepoMock.Verify(r => r.Delete(userIdToRemove), Times.Once);
    }

    #endregion

    #region Authenticate Tests

    [Fact]
    public void Authenticate_ValidCredentials_ReturnsUser()
    {
        // Arrange
        var login = "user";
        var password = "password";
        var expectedUser = new User { Login = login, Password = password };
        _userRepoMock.Setup(r => r.GetByLogin(login)).Returns(expectedUser);

        // Act
        var result = _userService.Authenticate(login, password);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(login, result!.Login);
    }

    [Fact]
    public void Authenticate_InvalidPassword_ReturnsNull()
    {
        // Arrange
        _userRepoMock.Setup(r => r.GetByLogin("user"))
            .Returns(new User { Login = "user", Password = "correctPassword" });

        // Act
        var result = _userService.Authenticate("user", "wrongPassword");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void Authenticate_UserDoesNotExist_ReturnsNull()
    {
        // Arrange
        string login = "nonExistentUser";
        _userRepoMock.Setup(r => r.GetByLogin(login)).Returns((User)null!);

        // Act
        var result = _userService.Authenticate(login, "anyPassword");

        // Assert
        Assert.Null(result);
    }

    #endregion
}