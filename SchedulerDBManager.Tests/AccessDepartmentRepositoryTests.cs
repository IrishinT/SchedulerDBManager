using System.Data;
using System.Data.OleDb;
using Moq;
using SchedulerDBManager.DataAccess.Database;
using SchedulerDBManager.DataAccess.Models;
using SchedulerDBManager.DataAccess.Repositories.Access;
using Xunit;

namespace SchedulerDBManager.Tests;

public class AccessDepartmentRepositoryTests
{
    private readonly Mock<IDatabase> _dbMock;
    private readonly AccessDepartmentRepository _repository;

    public AccessDepartmentRepositoryTests()
    {
        _dbMock = new Mock<IDatabase>();
        _repository = new AccessDepartmentRepository(_dbMock.Object);
    }

    [Fact]
    public void GetAll_ReturnsMappedDepartments()
    {
        // Arrange
        var dt = new DataTable();
        dt.Columns.Add("department_id", typeof(int));
        dt.Columns.Add("department_name", typeof(string));
        dt.Columns.Add("head_fullname", typeof(string));

        dt.Rows.Add(10, "Бухгалтерия", "Иванова И.И.");
        dt.Rows.Add(20, "ОТК", "Петров П.П.");

        _dbMock.Setup(d => d.ExecuteSelect("SELECT * FROM department", It.IsAny<OleDbParameter[]>())).Returns(dt);

        // Act
        var result = _repository.GetAll().ToList();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal(10, result[0].DepartmentId);
        Assert.Equal("Бухгалтерия", result[0].DepartmentName);
        Assert.Equal("Иванова И.И.", result[0].HeadFullName);
    }

    [Fact]
    public void SearchByName_ReturnsMatchingDepartments()
    {
        // Arrange
        var dt = new DataTable();
        dt.Columns.Add("department_id", typeof(int));
        dt.Columns.Add("department_name", typeof(string));
        dt.Columns.Add("head_fullname", typeof(string));
        dt.Rows.Add(10, "Бухгалтерия", "Иванова И.И.");

        string searchName = "Бух";
        OleDbParameter[] capturedParams = null!;

        _dbMock.Setup(d => d.ExecuteSelect(It.Is<string>(s => s.Contains("LIKE")), It.IsAny<OleDbParameter[]>()))
               .Callback<string, OleDbParameter[]>((q, p) => capturedParams = p)
               .Returns(dt);

        // Act
        var result = _repository.SearchByName(searchName).ToList();

        // Assert
        Assert.Single(result);
        Assert.Equal("Бухгалтерия", result[0].DepartmentName);
        Assert.NotNull(capturedParams);
        Assert.Single(capturedParams);
        Assert.Equal("%Бух%", capturedParams[0].Value);
    }

    [Fact]
    public void Add_ExecutesInsertCommandWithParameters()
    {
        // Arrange
        var dept = new Department { DepartmentName = "IT", HeadFullName = "Сидоров С.С." };
        OleDbParameter[] capturedParams = null!;

        _dbMock.Setup(d => d.ExecuteNonQuery(It.Is<string>(q => q.Contains("INSERT INTO department")), It.IsAny<OleDbParameter[]>()))
               .Callback<string, OleDbParameter[]>((q, p) => capturedParams = p);

        // Act
        _repository.Add(dept);

        // Assert
        _dbMock.Verify(d => d.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<OleDbParameter[]>()), Times.Once);
        Assert.NotNull(capturedParams);
        Assert.Equal(2, capturedParams.Length);
        Assert.Equal("IT", capturedParams[0].Value);
        Assert.Equal("Сидоров С.С.", capturedParams[1].Value);
    }

    [Fact]
    public void Update_ExecutesUpdateCommandWithParameters()
    {
        // Arrange
        var dept = new Department { DepartmentId = 15, DepartmentName = "HR", HeadFullName = "Смирнова А.А." };
        OleDbParameter[] capturedParams = null!;

        _dbMock.Setup(d => d.ExecuteNonQuery(It.Is<string>(q => q.Contains("UPDATE department")), It.IsAny<OleDbParameter[]>()))
               .Callback<string, OleDbParameter[]>((q, p) => capturedParams = p);

        // Act
        _repository.Update(dept);

        // Assert
        _dbMock.Verify(d => d.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<OleDbParameter[]>()), Times.Once);
        Assert.NotNull(capturedParams);
        Assert.Equal(3, capturedParams.Length);
        Assert.Equal("HR", capturedParams[0].Value);
        Assert.Equal("Смирнова А.А.", capturedParams[1].Value);
        Assert.Equal(15, capturedParams[2].Value);
    }

    [Fact]
    public void Delete_ExecutesDeleteCommandWithId()
    {
        // Arrange
        int deptId = 42;
        OleDbParameter[] capturedParams = null!;

        _dbMock.Setup(d => d.ExecuteNonQuery(It.Is<string>(q => q.Contains("DELETE FROM department")), It.IsAny<OleDbParameter[]>()))
               .Callback<string, OleDbParameter[]>((q, p) => capturedParams = p);

        // Act
        _repository.Delete(deptId);

        // Assert
        _dbMock.Verify(d => d.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<OleDbParameter[]>()), Times.Once);
        Assert.NotNull(capturedParams);
        Assert.Single(capturedParams);
        Assert.Equal(42, capturedParams[0].Value);
    }
}