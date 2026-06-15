using System;
using System.Data;
using System.Data.OleDb;
using SchedulerDBManager.DataAccess.Database.Access;
using Xunit;

namespace SchedulerDBManager.Tests;

public class AccessDatabaseTests
{
    [Fact]
    public void Constructor_ValidPath_CreatesInstanceSuccessfully()
    {
        // Act
        var db = new AccessDatabase("MyTestDB.accdb");

        // Assert
        Assert.NotNull(db);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_InvalidPath_ThrowsArgumentException(string invalidPath)
    {
        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => new AccessDatabase(invalidPath));
        Assert.Contains("Путь к файлу базы данных не может быть пустым", ex.Message);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void ExecuteSelect_EmptyQuery_ThrowsArgumentException(string emptyQuery)
    {
        // Arrange
        var db = new AccessDatabase("test.accdb");

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => db.ExecuteSelect(emptyQuery));
        Assert.Contains("Запрос не может быть пустым", ex.Message);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void ExecuteNonQuery_EmptyQuery_ThrowsArgumentException(string emptyQuery)
    {
        // Arrange
        var db = new AccessDatabase("test.accdb");

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() => db.ExecuteNonQuery(emptyQuery));
        Assert.Contains("Запрос не может быть пустым", ex.Message);
    }

    [Fact]
    public void CheckConnection_AttemptToConnect_ThrowsTypeInitOrOleDbException()
    {
        // Arrange
        var db = new AccessDatabase("nonexistent.accdb");

        // Act & Assert
        Assert.ThrowsAny<Exception>(() => db.CheckConnection());
    }
}