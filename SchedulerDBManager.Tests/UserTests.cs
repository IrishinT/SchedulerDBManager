using SchedulerDBManager.DataAccess.Models;
using Xunit;

namespace SchedulerDBManager.Tests;

public class UserTests
{
    [Theory]
    [InlineData(1, false, "Читатель")]
    [InlineData(2, true, "Редактор")]
    [InlineData(3, true, "Администратор")]
    public void CanEditData_And_RoleName_BasedOnRole_ReturnExpectedValues(int role, bool expectedCanEdit, string expectedRoleName)
    {
        // Arrange
        var user = new User { Role = role };

        // Act & Assert
        Assert.Equal(expectedCanEdit, user.CanEditData);
        Assert.Equal(expectedRoleName, user.RoleName);
    }

    [Theory]
    [InlineData(1, false)]
    [InlineData(2, false)]
    [InlineData(3, true)]
    public void IsAdmin_BasedOnRole_ReturnsExpectedValue(int role, bool expectedIsAdmin)
    {
        // Arrange
        var user = new User { Role = role };

        // Act & Assert
        Assert.Equal(expectedIsAdmin, user.IsAdmin);
    }

    [Fact]
    public void RoleName_UnknownRole_ReturnsUnknownText()
    {
        // Arrange
        var user = new User { Role = 99 }; // Несуществующая роль (пограничный случай)

        // Act & Assert
        Assert.Equal("Неизвестно", user.RoleName);
    }
}