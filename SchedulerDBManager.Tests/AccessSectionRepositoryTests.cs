using System.Data;
using System.Data.OleDb;
using Moq;
using SchedulerDBManager.DataAccess.Database;
using SchedulerDBManager.DataAccess.Models;
using SchedulerDBManager.DataAccess.Repositories.Access;
using Xunit;

namespace SchedulerDBManager.Tests;

public class AccessSectionRepositoryTests
{
    private readonly Mock<IDatabase> _dbMock;
    private readonly AccessSectionRepository _repository;

    public AccessSectionRepositoryTests()
    {
        _dbMock = new Mock<IDatabase>();
        _repository = new AccessSectionRepository(_dbMock.Object);
    }

    [Fact]
    public void GetAll_ReturnsMappedSections()
    {
        // Arrange
        var dt = new DataTable();
        dt.Columns.Add("section_id", typeof(int));
        dt.Columns.Add("address", typeof(string));
        dt.Columns.Add("department_id", typeof(int));
        dt.Columns.Add("department_name", typeof(string));
        dt.Columns.Add("phone", typeof(string));

        dt.Rows.Add(1, "\"ул. Маршала Буденного, 10\"", 10, "Бухгалтерия", "+79998887766");

        _dbMock.Setup(d => d.ExecuteSelect(It.Is<string>(q => q.Contains("sections sec")), It.IsAny<OleDbParameter[]>())).Returns(dt);

        // Act
        var result = _repository.GetAll().ToList();

        // Assert
        Assert.Single(result);
        Assert.Equal(1, result[0].SectionId);
        Assert.Equal("ул. Маршала Буденного, 10", result[0].Address); // Проверяем кавычки
        Assert.Equal(10, result[0].DepartmentId);
        Assert.Equal("Бухгалтерия", result[0].DepartmentName);
        Assert.Equal("+79998887766", result[0].Phone);
    }

    [Fact]
    public void SearchByAddress_ReturnsMatchingSections()
    {
        // Arrange
        var dt = new DataTable();
        dt.Columns.Add("section_id", typeof(int));
        dt.Columns.Add("address", typeof(string));
        dt.Columns.Add("department_id", typeof(int));
        dt.Columns.Add("department_name", typeof(string));
        dt.Columns.Add("phone", typeof(string));

        dt.Rows.Add(1, "ул. Маршала Буденного, 10", 10, "Бухгалтерия", "+79998887766");

        OleDbParameter[] capturedParams = null!;
        _dbMock.Setup(d => d.ExecuteSelect(It.Is<string>(q => q.Contains("address LIKE")), It.IsAny<OleDbParameter[]>()))
               .Callback<string, OleDbParameter[]>((q, p) => capturedParams = p)
               .Returns(dt);

        // Act
        var result = _repository.SearchByAddress("Буден").ToList();

        // Assert
        Assert.Single(result);
        Assert.Equal("ул. Маршала Буденного, 10", result[0].Address);
        Assert.NotNull(capturedParams);
        Assert.Single(capturedParams);
        Assert.Equal("%Буден%", capturedParams[0].Value);
    }

    [Fact]
    public void Add_ExecutesInsertCommandWithParameters()
    {
        // Arrange
        var section = new Section { Address = "Адрес", DepartmentId = 10, Phone = "+7999" };
        OleDbParameter[] capturedParams = null!;

        _dbMock.Setup(d => d.ExecuteNonQuery(It.Is<string>(q => q.Contains("INSERT INTO sections")), It.IsAny<OleDbParameter[]>()))
               .Callback<string, OleDbParameter[]>((q, p) => capturedParams = p);

        // Act
        _repository.Add(section);

        // Assert
        _dbMock.Verify(d => d.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<OleDbParameter[]>()), Times.Once);
        Assert.NotNull(capturedParams);
        Assert.Equal(3, capturedParams.Length);
        Assert.Equal("Адрес", capturedParams[0].Value);
        Assert.Equal(10, capturedParams[1].Value);
        Assert.Equal("+7999", capturedParams[2].Value);
    }

    [Fact]
    public void Update_ExecutesUpdateCommandWithParameters()
    {
        // Arrange
        var section = new Section { SectionId = 12, Address = "Адрес", DepartmentId = 10, Phone = "+7999" };
        OleDbParameter[] capturedParams = null!;

        _dbMock.Setup(d => d.ExecuteNonQuery(It.Is<string>(q => q.Contains("UPDATE sections")), It.IsAny<OleDbParameter[]>()))
               .Callback<string, OleDbParameter[]>((q, p) => capturedParams = p);

        // Act
        _repository.Update(section);

        // Assert
        _dbMock.Verify(d => d.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<OleDbParameter[]>()), Times.Once);
        Assert.NotNull(capturedParams);
        Assert.Equal(4, capturedParams.Length);
        Assert.Equal("Адрес", capturedParams[0].Value);
        Assert.Equal(10, capturedParams[1].Value);
        Assert.Equal("+7999", capturedParams[2].Value);
        Assert.Equal(12, capturedParams[3].Value);
    }

    [Fact]
    public void Delete_ExecutesDeleteCommandWithId()
    {
        // Arrange
        int sectionId = 5;
        OleDbParameter[] capturedParams = null!;

        _dbMock.Setup(d => d.ExecuteNonQuery(It.Is<string>(q => q.Contains("DELETE FROM sections")), It.IsAny<OleDbParameter[]>()))
               .Callback<string, OleDbParameter[]>((q, p) => capturedParams = p);

        // Act
        _repository.Delete(sectionId);

        // Assert
        _dbMock.Verify(d => d.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<OleDbParameter[]>()), Times.Once);
        Assert.NotNull(capturedParams);
        Assert.Single(capturedParams);
        Assert.Equal(5, capturedParams[0].Value);
    }
}