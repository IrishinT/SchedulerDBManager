using System;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using Moq;
using SchedulerDBManager.DataAccess.Database;
using SchedulerDBManager.DataAccess.Models;
using SchedulerDBManager.DataAccess.Repositories.Access;
using Xunit;

namespace SchedulerDBManager.Tests;

public class AccessUserRepositoryTests
{
    private readonly Mock<IDatabase> _dbMock;
    private readonly AccessUserRepository _repository;

    public AccessUserRepositoryTests()
    {
        _dbMock = new Mock<IDatabase>();
        _repository = new AccessUserRepository(_dbMock.Object);
    }

    [Fact]
    public void GetAll_ReturnsMappedUsers()
    {
        // Arrange
        var dt = new DataTable();
        dt.Columns.Add("user_id", typeof(int));
        dt.Columns.Add("login", typeof(string));
        dt.Columns.Add("password", typeof(string));
        dt.Columns.Add("role", typeof(int));

        dt.Rows.Add(1, "admin", "pass123", 3);
        dt.Rows.Add(2, "editor", "pass456", 2);

        _dbMock.Setup(d => d.ExecuteSelect("SELECT * FROM users", It.IsAny<OleDbParameter[]>())).Returns(dt);

        // Act
        var result = _repository.GetAll().ToList();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal(1, result[0].UserId);
        Assert.Equal("admin", result[0].Login);
        Assert.Equal("pass123", result[0].Password);
        Assert.Equal(3, result[0].Role);
    }

    [Fact]
    public void GetByLogin_UserExists_ReturnsUser()
    {
        // Arrange
        var dt = new DataTable();
        dt.Columns.Add("user_id", typeof(int));
        dt.Columns.Add("login", typeof(string));
        dt.Columns.Add("password", typeof(string));
        dt.Columns.Add("role", typeof(int));
        dt.Rows.Add(1, "admin", "pass123", 3);

        OleDbParameter[] capturedParams = null!;
        _dbMock.Setup(d => d.ExecuteSelect(It.Is<string>(q => q.Contains("WHERE login")), It.IsAny<OleDbParameter[]>()))
               .Callback<string, OleDbParameter[]>((q, p) => capturedParams = p)
               .Returns(dt);

        // Act
        var result = _repository.GetByLogin("admin");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("admin", result!.Login);
        Assert.NotNull(capturedParams);
        Assert.Single(capturedParams);
        Assert.Equal("admin", capturedParams[0].Value);
    }

    [Fact]
    public void GetByLogin_UserDoesNotExist_ReturnsNull()
    {
        // Arrange
        var dt = new DataTable(); // Пустая таблица результатов
        _dbMock.Setup(d => d.ExecuteSelect(It.IsAny<string>(), It.IsAny<OleDbParameter[]>())).Returns(dt);

        // Act
        var result = _repository.GetByLogin("ghost");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void Add_WithValues_ExecutesInsertWithCorrectParameters()
    {
        // Arrange
        var u = new User { Login = "newUser", Password = "password123", Role = 1 };
        OleDbParameter[] capturedParams = null!;

        _dbMock.Setup(d => d.ExecuteNonQuery(It.Is<string>(q => q.Contains("INSERT INTO users")), It.IsAny<OleDbParameter[]>()))
               .Callback<string, OleDbParameter[]>((q, p) => capturedParams = p);

        // Act
        _repository.Add(u);

        // Assert
        _dbMock.Verify(d => d.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<OleDbParameter[]>()), Times.Once);
        Assert.NotNull(capturedParams);
        Assert.Equal(3, capturedParams.Length);
        Assert.Equal("newUser", capturedParams[0].Value);
        Assert.Equal("password123", capturedParams[1].Value);
        Assert.Equal(1, capturedParams[2].Value);
    }

    [Fact]
    public void Add_WithNullValues_ExecutesInsertWithDBNull()
    {
        // Arrange
        var u = new User { Login = null!, Password = null!, Role = 1 };
        OleDbParameter[] capturedParams = null!;

        _dbMock.Setup(d => d.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<OleDbParameter[]>()))
               .Callback<string, OleDbParameter[]>((q, p) => capturedParams = p);

        // Act
        _repository.Add(u);

        // Assert
        Assert.NotNull(capturedParams);
        Assert.Equal(DBNull.Value, capturedParams[0].Value);
        Assert.Equal(DBNull.Value, capturedParams[1].Value);
    }

    [Fact]
    public void Update_ExecutesUpdateWithCorrectParameters()
    {
        // Arrange
        var u = new User { UserId = 10, Login = "updatedUser", Password = "password321", Role = 2 };
        OleDbParameter[] capturedParams = null!;

        _dbMock.Setup(d => d.ExecuteNonQuery(It.Is<string>(q => q.Contains("UPDATE users")), It.IsAny<OleDbParameter[]>()))
               .Callback<string, OleDbParameter[]>((q, p) => capturedParams = p);

        // Act
        _repository.Update(u);

        // Assert
        _dbMock.Verify(d => d.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<OleDbParameter[]>()), Times.Once);
        Assert.NotNull(capturedParams);
        Assert.Equal(4, capturedParams.Length);
        Assert.Equal("updatedUser", capturedParams[0].Value);
        Assert.Equal("password321", capturedParams[1].Value);
        Assert.Equal(2, capturedParams[2].Value);
        Assert.Equal(10, capturedParams[3].Value);
    }

    [Fact]
    public void Delete_ExecutesDeleteWithId()
    {
        // Arrange
        int userId = 99;
        OleDbParameter[] capturedParams = null!;

        _dbMock.Setup(d => d.ExecuteNonQuery(It.Is<string>(q => q.Contains("DELETE FROM users")), It.IsAny<OleDbParameter[]>()))
               .Callback<string, OleDbParameter[]>((q, p) => capturedParams = p);

        // Act
        _repository.Delete(userId);

        // Assert
        _dbMock.Verify(d => d.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<OleDbParameter[]>()), Times.Once);
        Assert.NotNull(capturedParams);
        Assert.Single(capturedParams);
        Assert.Equal(99, capturedParams[0].Value);
    }
}